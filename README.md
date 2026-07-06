# File Browser Desktop for macOS and Linux

<p align="center">
  <img src="src/Assets/app-logos/png/filebrowser.png" alt="File Browser logo" width="96">
</p>

<h3 align="center">A dedicated macOS/Linux desktop client for privately accessing File Browser over SSH.</h3>

<p align="center">
  <a href="https://github.com/paravaneai/filebrowser-desktop-macos-linux/tree/main">Source</a> |
  <a href="INSTALL.md">Installation</a> |
  <a href="SERVER_SETUP.md">Server setup</a> |
  <a href="SECURITY.md">Security</a> |
  <a href="https://github.com/paravaneai/filebrowser-desktop-macos-linux/releases">Releases</a> |
  <a href="CONTRIBUTING.md">Contributing</a>
</p>

<p align="center">
  <a href="https://github.com/paravaneai/filebrowser-desktop-macos-linux/releases">
    <img src="https://img.shields.io/github/v/release/paravaneai/filebrowser-desktop-macos-linux?include_prereleases&label=release" alt="Latest release">
  </a>
  <a href="LICENSE">
    <img src="https://img.shields.io/github/license/paravaneai/filebrowser-desktop-macos-linux" alt="License">
  </a>
  <img src="https://img.shields.io/badge/status-preview-7057ff" alt="Preview">
  <img src="https://img.shields.io/badge/platform-macOS%20%7C%20Linux-0078D4" alt="macOS and Linux">
</p>

<p align="center">
  <img src="src/Assets/app-logos/png/filebrowserdesktop-file-window-transfer-glossy-xl_transparent-polished.png" alt="File Browser Desktop preview">
</p>

## Overview

File Browser Desktop for macOS and Linux gives File Browser a focused desktop home. It starts a private SSH tunnel, opens File Browser in its own app window, and keeps the browser experience out of everyday browser tabs.

This edition intentionally does not run on Windows. Use the Windows edition for Windows desktops.

## Highlights

- Dedicated macOS/Linux desktop window for File Browser
- Configurable connection profiles for one or more servers
- SSH tunnel management through the user's installed `ssh`
- Support for SSH config, SSH keys, ssh-agent, and optional identity files
- First-run setup surface for existing or newly configured File Browser servers
- Safe server setup script that keeps File Browser bound to `127.0.0.1`
- Light and dark desktop shell themes
- Clean tunnel shutdown when the app closes

## Install

Download the latest macOS or Linux archive from [Releases](https://github.com/paravaneai/filebrowser-desktop-macos-linux/releases).

Linux packages are distributed as `.tar.gz` archives. macOS packages are distributed as `.app.tar.gz` archives.

For details, see [INSTALL.md](INSTALL.md).

## Connection Model

File Browser Desktop is designed around an SSH-only access path:

```text
macOS/Linux desktop app -> SSH tunnel -> server localhost File Browser
```

Recommended server binding:

```text
127.0.0.1:8080
```

Default desktop tunnel:

```text
127.0.0.1:18080 -> server 127.0.0.1:8080
```

Do not expose File Browser directly to the public internet. See [SECURITY.md](SECURITY.md) before using this with a production server.

## Profiles And Credentials

Connection profiles are stored locally.

macOS:

```text
~/Library/Application Support/FileBrowserDesktop/profiles.json
```

Linux:

```text
${XDG_CONFIG_HOME:-~/.config}/FileBrowserDesktop/profiles.json
```

Profiles store connection settings such as SSH host, SSH username, SSH port, and tunnel ports. They do not store passwords, passphrases, private keys, or File Browser login credentials.

Credential saving is disabled in this preview. macOS Keychain and Linux Secret Service support are planned before credential storage is enabled.

## Server Setup

If File Browser is already installed, the app can connect to it as long as it is reachable through SSH and bound privately on the server.

If you want help preparing a server, this repo includes:

```text
server/install-filebrowser-localhost.sh
```

The script installs or configures File Browser as a service, binds it to `127.0.0.1`, and does not open public firewall ports. See [SERVER_SETUP.md](SERVER_SETUP.md).

## Current Release

The current macOS/Linux preview release is [v001.001.000](https://github.com/paravaneai/filebrowser-desktop-macos-linux/releases/tag/v001.001.000).

This project is still early. Expect changes around packaging, credential storage, onboarding, and profile management before the first stable release.

## Relationship To File Browser

File Browser Desktop is an independent Paravane Labs project. Paravane Labs does not own File Browser and is not affiliated with, endorsed by, or sponsored by the File Browser project.

File Browser is a separate project run by its own maintainers. This desktop app is only a convenience client for connecting to a File Browser instance that you operate.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE).
