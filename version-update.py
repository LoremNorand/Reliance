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
            print("������ ������ config.json:", e)
    
    # ���� version �� ������ � config, ���� � README
    if not version and os.path.exists("README.md"):
        with open("README.md", "r", encoding="utf-8") as f:
            for line in f:
                if line.startswith("������: "):
                    version = line.split("������: ")[1].strip()
                    break
    if not version:
        raise ValueError("�� ������� ����� ������� ������ �� � config.json, �� � README.md")
    return version

def parse_version(version):
    # ��������� ������: major.minor.patch-<tag>.micropatch
    pattern = r'(\d+)\.(\d+)\.(\d+)-([^.]+)\.(\d+)'
    m = re.match(pattern, version)
    if m:
        major, minor, patch, tag, micropatch = m.groups()
        return int(major), int(minor), int(patch), tag, int(micropatch)
    else:
        raise ValueError("�������� ������ ������: " + version)

def bump_version(current_version, commit_info):
    major, minor, patch, current_tag, micropatch = parse_version(current_version)
    significant = False

    # ���� ������ ������������� Conventional, ����������� ���
    commit_type = commit_info.get("type")
    if commit_type in ["feat", "fix"] or commit_info.get("breaking", False):
        significant = True

    if significant:
        patch += 1
        micropatch = 0
        # ���� ������������ state, ������ ���
        tag = commit_info.get("state") if commit_info.get("state") else current_tag
    else:
        # ��� ��������� ��������� ������ ���������
        micropatch += 1
        tag = current_tag

    return f"{major}.{minor}.{patch}-{tag}.{micropatch}"

def parse_commit_message(msg):
    # ���������� regex: ^(.+)\((.+)\)(?P<state>\[.+\])?(!?): (.+)$
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
        # ���� �� �������������, �������, ��� ��� ������������ ������ � ��������� ������ ���������
        return {"type": None, "state": None, "breaking": False, "title": msg, "full": msg}

def update_readme(new_version, commit_info, commit_author, commit_date):
    filename = "README.md"
    if not os.path.exists(filename):
        print("README.md �� ������, ���������� ��� ����������")
        return

    with open(filename, "r", encoding="utf-8") as f:
        content = f.read()

    # ��������� ������ � �������
    content = re.sub(r'(������: ).*', r'\1' + new_version, content)

    # ��������� ���� "# ��������� ���������"
    # ������������, ��� ���� ���������� � "# ��������� ���������" � �� ���������� ��������� ���� ����� �����
    new_changes = (
        f"{commit_info['title']}\n\n"
        f"{commit_info['full']}\n\n"
        f"{commit_author}\n{commit_date}\n"
    )
    content = re.sub(
        r'(# ��������� ���������\n)(.*?)(\n#|\Z)',
        r'\1' + new_changes + r'\3',
        content,
        flags=re.DOTALL
    )

    with open(filename, "w", encoding="utf-8") as f:
        f.write(content)
    print("README.md �������")

def update_config(new_version):
    filename = "config.json"
    if not os.path.exists(filename):
        print("config.json �� ������, ���������� ��� ����������")
        return

    try:
        with open(filename, "r", encoding="utf-8") as f:
            data = json.load(f)
    except Exception as e:
        print("������ ������ config.json:", e)
        return

    if "assembleConfiguration" in data and "version" in data["assembleConfiguration"]:
        data["assembleConfiguration"]["version"] = new_version
        with open(filename, "w", encoding="utf-8") as f:
            json.dump(data, f, indent=2, ensure_ascii=False)
        print("config.json �������")
    else:
        print("��������� ������ �� ������� � config.json, ���������� ��� ����������")

def commit_changes(new_version):
    # ����������� ������ ��� git
    subprocess.run(["git", "config", "user.name", "github-actions"], check=True)
    subprocess.run(["git", "config", "user.email", "github-actions@github.com"], check=True)

    # ��������� �����
    subprocess.run(["git", "add", "README.md"])
    if os.path.exists("config.json"):
        subprocess.run(["git", "add", "config.json"])

    # ������ commit � [skip ci]
    commit_msg = f"chore(version): bump version to {new_version} [skip ci]"
    result = subprocess.run(["git", "commit", "-m", commit_msg])
    if result.returncode == 0:
        subprocess.run(["git", "push"])
        print("����� ������ ������������� � ���������� � �����������")
    else:
        print("��� ��������� ��� �������")

def main():
    # �������� ��������� ������
    commit_msg = get_latest_commit_message()
    print("��������� ��������� �������:", commit_msg)
    commit_info = parse_commit_message(commit_msg)
    print("������� �������:", commit_info)
    
    # �������� ������� ������
    current_version = get_current_version()
    print("������� ������:", current_version)

    # ��������� ����� ������ �� ������
    new_version = bump_version(current_version, commit_info)
    print("����� ������:", new_version)

    # �������� ���������� �� ������ � ���� �������
    commit_author = subprocess.run(["git", "log", "-1", "--pretty=%an"], capture_output=True, text=True).stdout.strip()
    commit_date = subprocess.run(["git", "log", "-1", "--pretty=%cd", "--date=short"], capture_output=True, text=True).stdout.strip()

    # ��������� �����
    update_readme(new_version, commit_info, commit_author, commit_date)
    update_config(new_version)

    # ��������� commit � push ���������. ����, ��� ���� ������ ��� ��� ��������� ���, commit �� ��������.
    commit_changes(new_version)

if __name__ == "__main__":
    main()
