# Install

File Browser Desktop for macOS and Linux runs on your desktop and connects to File Browser on a server through SSH.

This edition does not support Windows.

## Requirements

- .NET 8 Runtime: https://dotnet.microsoft.com/download/dotnet/8.0
- OpenSSH client available as `ssh`
- A graphical macOS or Linux desktop session

Linux distributions may require their normal WebView/native browser packages for embedded web content.

## Install On Linux

1. Download the Linux `.tar.gz` release archive.
2. Extract it somewhere writable, for example:

   ```sh
   mkdir -p "$HOME/Applications/FileBrowserDesktop"
   tar -xzf FileBrowserDesktop-linux-x64-framework-dependent.tar.gz -C "$HOME/Applications/FileBrowserDesktop"
   ```

3. Run:

   ```sh
   "$HOME/Applications/FileBrowserDesktop/run-filebrowser-desktop.sh"
   ```

## Install On macOS

1. Download the macOS `.app.tar.gz` release archive.
2. Extract it:

   ```sh
   tar -xzf FileBrowserDesktop-osx-arm64-framework-dependent.app.tar.gz
   ```

3. Move `File Browser Desktop.app` to `/Applications` or another app folder.
4. Open the app.

The preview app is not signed or notarized yet, so macOS may require explicit approval in System Settings before first launch.

## Run From Source

From the repository root:

```sh
sh ./run-filebrowser-desktop.sh
```

Or directly:

```sh
dotnet run --project src/FileBrowserDesktop.csproj
```

## First-Run Setup

The setup surface supports two paths:

- Connect to an existing File Browser instance.
- Help install/configure File Browser on a server over SSH.

The setup flow can:

- Test SSH
- Run the safe server setup script
- Test the SSH tunnel
- Save the profile and open File Browser

## Server Side

File Browser runs on the server. It should bind to localhost only:

```text
127.0.0.1:8080
```

Do not expose File Browser directly to the public internet. Use the desktop app's SSH tunnel.
