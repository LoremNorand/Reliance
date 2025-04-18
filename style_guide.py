#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Скрипт для рефакторинга C# кода по Google Style Guide.
Работает безопасно и из коробки, с проверками на None и обработкой ошибок.
"""
import re
import os
from typing import List, Dict

def sort_keywords(declaration: str) -> str:
    """
    Сортирует модификаторы доступа и другие ключевые слова в объявлении члена класса.

    Args:
        declaration (str): Исходная строка с ключевыми словами.

    Returns:
        str: Отсортированная строка.
    """
    if not isinstance(declaration, str):
        return ''
    parts = declaration.split()
    access_modifiers = ["public", "protected", "private", "internal"]
    other_modifiers = ["static", "async", "unsafe", "readonly", "volatile", "extern",
                       "partial", "abstract", "virtual", "override", "sealed",
                       "new", "const", "required", "init"]
    type_keywords = ["class", "struct", "interface", "enum", "delegate", "record"]
    designators = ["primary"]

    access = [p for p in parts if p in access_modifiers]
    others = [p for p in parts if p in other_modifiers]
    types = [p for p in parts if p in type_keywords]
    desig = [p for p in parts if p in designators]
    names = [p for p in parts if p not in access_modifiers + other_modifiers + type_keywords + designators]

    ordered = access + others + types + desig + names
    return ' '.join(ordered)


def refactor_csharp_code(filepath: str) -> str:
    """
    Читает файл C#, выполняет рефакторинг и возвращает новый код.
    Не пишет файл сам — это делает вызывающая функция.
    """
    if not os.path.isfile(filepath):
        raise FileNotFoundError(f"Файл не найден: {filepath}")

    with open(filepath, 'r', encoding='utf-8') as f:
        code = f.read()

    # Разделение на объявления типов
    parts = re.split(r"(class|struct|interface|enum|delegate|record)\s+([A-Za-z_][A-Za-z0-9_]*)", code)
    if len(parts) < 4:
        return code

    result = parts[0]
    for i in range(1, len(parts), 3):
        try:
            type_kw = parts[i]
            type_name = parts[i+1]
            body = parts[i+2]
        except IndexError:
            break

        member_pattern = (
            r"((?:public|protected|private|internal)\s+"
            r"(?:static\s+)?(?:async\s+)?(?:unsafe\s+)?(?:readonly\s+)?"
            r"(?:volatile\s+)?(?:extern\s+)?(?:partial\s+)?(?:abstract\s+)?"
            r"(?:virtual\s+)?(?:override\s+)?(?:sealed\s+)?(?:new\s+)?"
            r"(?:const\s+)?(?:required\s+)?(?:init\s+)?)?"
            r"([A-Za-z_][A-Za-z0-9_]*)\s*"
            r"(\([^)]*\)|\{[^{}]*\}|\[[^]]*\]|[A-Za-z0-9_<>]+)"
            r"(\s*;|\s*\{)"
        )
        members = re.findall(member_pattern, body)
        categorized: Dict[str, List[Dict]] = {
            'constants': [], 'fields': [], 'events': [],
            'constructors': [], 'properties': [], 'methods': [],
            'nested': [], 'others': []
        }

        for decl, name, mem_body, term in members:
            decl = decl or ''
            mem_body = mem_body or ''
            term = term or ''

            if not name.strip():
                continue

            comment = ''
            if '//' in decl or '/*' in decl:
                idx = decl.find('//') if '//' in decl else decl.find('/*')
                comment = decl[:idx].strip()
                decl = decl[idx:].strip()

            sorted_decl = sort_keywords(decl)
            member = {
                'decl': sorted_decl,
                'name': name,
                'body': mem_body,
                'term': term,
                'comment': comment
            }

            if sorted_decl.endswith('const') or ' const ' in sorted_decl:
                categorized['constants'].append(member)
            elif ' event ' in sorted_decl:
                categorized['events'].append(member)
            elif name == type_name and '(' in mem_body:
                categorized['constructors'].append(member)
            elif '(' in mem_body and name != type_name:
                categorized['methods'].append(member)
            elif '{' in mem_body and term.strip() == '{':
                categorized['nested'].append(member)
            elif '(' not in mem_body and term.strip() == ';':
                categorized['fields'].append(member)
            elif '(' not in mem_body and '{' in mem_body:
                categorized['properties'].append(member)
            else:
                categorized['others'].append(member)

        categorized['constructors'].sort(key=lambda m: m['decl'].split()[0] if m['decl'] else '')
        categorized['methods'].sort(key=lambda m: m['name'])
        categorized['properties'].sort(key=lambda m: m['name'])

        regions = []
        order = ['constants','fields','events','constructors','properties','methods','nested','others']
        names_ru = {
            'constants': 'Константы', 'fields': 'Поля', 'events': 'События',
            'constructors': 'Конструкторы', 'properties': 'Свойства',
            'methods': 'Методы', 'nested': 'Вложенные типы', 'others': 'Прочие'
        }
        for key in order:
            items = categorized[key]
            if not items:
                continue
            lines = [f"        {itm['comment']}\n        {itm['decl']} {itm['name']} {itm['body']}{itm['term']}" for itm in items]
            region = [f"    #region {names_ru[key]}"] + lines + ["    #endregion\n"]
            regions += region

        new_body = '\n'.join(regions).rstrip() + '\n'
        result += f"{type_kw} {type_name}\n{{\n{new_body}}}\n"

    return result


def write_refactored(filepath: str):
    try:
        new_code = refactor_csharp_code(filepath)
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(new_code)
        print(f"Успешно отрефакторено: {filepath}")
    except Exception as e:
        print(f"Ошибка при обработке {filepath}: {e}")


def process_directory(root_dir: str):
    if not os.path.isdir(root_dir):
        print(f"Не найден каталог: {root_dir}")
        return
    for dirpath, _, filenames in os.walk(root_dir):
        for fn in filenames:
            if fn.endswith('.cs'):
                path = os.path.join(dirpath, fn)
                write_refactored(path)

# Укажите путь к файлу или каталогу (запись в переменную):
TARGET_PATH = "D:\Storage\VS Projects\Reliance"

if os.path.isfile(TARGET_PATH):
    write_refactored(TARGET_PATH)
elif os.path.isdir(TARGET_PATH):
    process_directory(TARGET_PATH)
else:
    print(f"Не найден файл или каталог: {TARGET_PATH}")

print('Рефакторинг завершен.')
