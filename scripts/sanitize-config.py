# -*- coding: utf-8 -*-
# github.com/LoremNorand/Reliance
import json
import subprocess
import os

CONFIG_PATH = "./config.json"

def sanitize_config():
    if not os.path.exists(CONFIG_PATH):
        print("config.json не найден.")
        return

    with open(CONFIG_PATH, "r", encoding="utf-8") as f:
        data = json.load(f)

    modified = False

    try:
        private_key = data["externalPart"]["clusterConfiguration"].get("privateKey")
        if private_key:
            data["externalPart"]["clusterConfiguration"]["privateKey"] = {
                "header": "PLACEHOLDER",
                "jsonControlSum": "PLACEHOLDER",
                "libraryControlSum": "PLACEHOLDER",
                "row1": "PLACEHOLDER",
                "row2": "PLACEHOLDER"
            }
            modified = True

        node_name = data["externalPart"]["nodeConfiguration"].get("name")
        if node_name:
            data["externalPart"]["nodeConfiguration"]["name"] = "NODE_NAME_PLACEHOLDER"
            modified = True

        cluster_name = data["externalPart"]["clusterConfiguration"].get("name")
        if cluster_name:
            data["externalPart"]["clusterConfiguration"]["name"] = "CLUSTER_NAME_PLACEHOLDER"
            modified = True

    except KeyError as e:
        print(f"Ошибка доступа к ключу: {e}")
        return

    if modified:
        with open(CONFIG_PATH, "w", encoding="utf-8") as f:
            json.dump(data, f, indent=2, ensure_ascii=False)
        print("config.json очищен от чувствительных данных.")

        # Добавим изменения для будущего коммита
        subprocess.run(["git", "add", CONFIG_PATH], check=True)
    else:
        print("Нечего очищать — все данные уже в порядке.")

if __name__ == "__main__":
    sanitize_config()
