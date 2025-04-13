# -*- coding: utf-8 -*-

import os
import re
import json
import subprocess
from datetime import datetime

def get_latest_commit_message():
    result = subprocess.run(["git", "log", "-1", "--pretty=%B"], capture_output=True, text=True)
    return result.stdout.strip()

def get_current_version():
    version = None
    config_path = "config.json"
    if os.path.exists(config_path):
        try:
            with open(config_path, "r", encoding="utf-8") as f:
                data = json.load(f)
                version = data.get("assembleConfiguration", {}).get("version")
        except Exception as e:
            print("Ошибка чтения config.json:", e)
    
    # Если версия не найдена в config, то ищем в README
    if not version and os.path.exists("README.md"):
        with open("README.md", "r", encoding="utf-8") as f:
            for line in f:
                if line.startswith("Версия: "):
                    version = line.split("Версия: ")[1].strip()
                    break
    if not version:
        raise ValueError("Не удалось найти текущую версию ни в config.json, ни в README.md")
    return version

def parse_version(version):
    # Ожидаемый формат версии: major.minor.patch-<tag>.micropatch
    pattern = r'(\d+)\.(\d+)\.(\d+)-([^.]+)\.(\d+)'
    m = re.match(pattern, version)
    if m:
        major, minor, patch, tag, micropatch = m.groups()
        return int(major), int(minor), int(patch), tag, int(micropatch)
    else:
        raise ValueError("Неверный формат версии: " + version)

def bump_version(current_version, commit_info):
    major, minor, patch, current_tag, micropatch = parse_version(current_version)
    significant = False

    # Если коммит соответствует Conventional, анализируем тип
    commit_type = commit_info.get("type")
    if commit_type in ["feat", "fix"] or commit_info.get("breaking", False):
        significant = True

    if significant:
        patch += 1
        micropatch = 0
        # Если присутствует state, используем его значение
        tag = commit_info.get("state") if commit_info.get("state") else current_tag
    else:
        # Для остальных обновляем только микропатч
        micropatch += 1
        tag = current_tag

    return f"{major}.{minor}.{patch}-{tag}.{micropatch}"

def parse_commit_message(msg):
    # Используем regex: ^(.+)\((.+)\)(?P<state>\[.+\])?(!?): (.+)$
    pattern = r'^(.+)\((.+)\)(?P<state>\[.+\])?(!?): (.+)$'
    m = re.match(pattern, msg)
    if m:
        commit_type = m.group(1).strip()
        scope = m.group(2).strip()
        state = m.group("state")
        if state:
            state = state.strip("[]")
        exclamation = m.group(4)
        breaking = True if exclamation == "!" else False
        title = m.group(5).strip()
        return {"type": commit_type, "scope": scope, "state": state, "breaking": breaking, "title": title, "full": msg}
    else:
        # Если не соответствует, считаем, что это неформальный коммит – обновляем только микропатч
        return {"type": None, "state": None, "breaking": False, "title": msg, "full": msg}

def update_readme(new_version, commit_info, commit_author, commit_date):
    filename = "README.md"
    if not os.path.exists(filename):
        print("README.md не найден, пропускаем его обновление")
        return

    with open(filename, "r", encoding="utf-8") as f:
        content = f.read()

    # Обновляем строку с версией, используя lambda для избежания конфликта с групповыми ссылками
    content = re.sub(r'(Версия: ).*', lambda m: m.group(1) + new_version, content)

    # Обновляем блок "# Последние изменения"
    new_changes = (
        f"{commit_info['title']}\n\n"
        f"{commit_info['full']}\n\n"
        f"{commit_author}\n{commit_date}\n"
    )
    content = re.sub(
        r'(# Последние изменения\n)(.*?)(\n#|\Z)',
        lambda m: m.group(1) + new_changes + m.group(3),
        content,
        flags=re.DOTALL
    )

    with open(filename, "w", encoding="utf-8") as f:
        f.write(content)
    print("README.md обновлён")

def update_config(new_version):
    filename = "config.json"
    if not os.path.exists(filename):
        print("config.json не найден, пропускаем его обновление")
        return

    try:
        with open(filename, "r", encoding="utf-8") as f:
            data = json.load(f)
    except Exception as e:
        print("Ошибка чтения config.json:", e)
        return

    if "assembleConfiguration" in data and "version" in data["assembleConfiguration"]:
        data["assembleConfiguration"]["version"] = new_version
        with open(filename, "w", encoding="utf-8") as f:
            json.dump(data, f, indent=2, ensure_ascii=False)
        print("config.json обновлён")
    else:
        print("Параметры версии не найдены в config.json, пропускаем его обновление")

def commit_changes(new_version):
    # Настраиваем данные для git
    subprocess.run(["git", "config", "user.name", "github-actions"], check=True)
    subprocess.run(["git", "config", "user.email", "github-actions@github.com"], check=True)

    # Добавляем файлы
    subprocess.run(["git", "add", "README.md"])
    if os.path.exists("config.json"):
        subprocess.run(["git", "add", "config.json"])

    # Делаем commit с [skip ci]
    commit_msg = f"chore(version): bump version to {new_version} [skip ci]"
    result = subprocess.run(["git", "commit", "-m", commit_msg])
    if result.returncode == 0:
        subprocess.run(["git", "push"])
        print("Новая версия зафиксирована и отправлена в репозиторий")
    else:
        print("Нет изменений для коммита")

def main():
    # Получаем последнее сообщение коммита
    commit_msg = get_latest_commit_message()
    print("Последнее сообщение коммита:", commit_msg)
    commit_info = parse_commit_message(commit_msg)
    print("Парсинг коммита:", commit_info)
    
    # Получаем текущую версию
    current_version = get_current_version()
    print("Текущая версия:", current_version)

    # Вычисляем новую версию по логике
    new_version = bump_version(current_version, commit_info)
    print("Новая версия:", new_version)

    # Получаем информацию об авторе и дате коммита
    commit_author = subprocess.run(["git", "log", "-1", "--pretty=%an"], capture_output=True, text=True).stdout.strip()
    commit_date = subprocess.run(["git", "log", "-1", "--pretty=%cd", "--date=short"], capture_output=True, text=True).stdout.strip()

    # Обновляем файлы
    update_readme(new_version, commit_info, commit_author, commit_date)
    update_config(new_version)

    # Совершаем commit и push изменений. Если изменений нет, commit не создаётся.
    commit_changes(new_version)

if __name__ == "__main__":
    main()
