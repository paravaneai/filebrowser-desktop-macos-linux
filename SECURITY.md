# Security

File Browser Desktop is designed to keep the File Browser web UI private. The intended model is:

```text
macOS/Linux desktop app -> SSH tunnel -> server localhost File Browser
```

Do not expose File Browser directly to the public internet.

## Server Exposure

File Browser should bind to localhost only, usually:

```text
127.0.0.1:8080
```

The desktop app forwards a local port to that private server port through SSH.

Good:

```text
server File Browser: 127.0.0.1:8080
desktop tunnel:      127.0.0.1:18080 -> server 127.0.0.1:8080
```

Avoid:

```text
server File Browser: 0.0.0.0:8080
public firewall:     allow 8080/tcp
```

The included server setup script configures File Browser for localhost binding and does not open firewall ports.

## SSH Tunnel Only

The app uses the user's installed `ssh`.

Supported authentication paths:

- Existing OpenSSH config
- Default SSH keys
- ssh-agent
- Optional per-profile identity-file path

The app does not store SSH passwords or SSH key passphrases. Use ssh-agent or your normal OpenSSH configuration for passphrase-protected keys.

The app does not disable host-key checking. First-time host trust and host-key changes are handled by OpenSSH.

## Stored Profile Data

Connection profiles are stored in:

macOS:

```text
~/Library/Application Support/FileBrowserDesktop/profiles.json
```

Linux:

```text
${XDG_CONFIG_HOME:-~/.config}/FileBrowserDesktop/profiles.json
```

Profiles may contain:

- Profile name
- SSH username
- SSH host
- SSH port
- Optional SSH identity-file path
- Local tunnel host/port
- Remote File Browser host/port

Profiles must not contain:

- Passwords
- SSH key passphrases
- Private key contents
- File Browser login credentials

## File Browser Credentials

Credential saving is disabled in this macOS/Linux preview.

Planned secure storage backends:

- macOS: Keychain
- Linux: Secret Service, GNOME Keyring, or KWallet

Until those backends are implemented, use File Browser's normal login flow and your operating system or password manager to store passwords.

## Remove Local App Data

Remove connection profiles and app settings:

macOS:

```sh
rm -rf "$HOME/Library/Application Support/FileBrowserDesktop"
```

Linux:

```sh
rm -rf "${XDG_CONFIG_HOME:-$HOME/.config}/FileBrowserDesktop"
```

## Server Cleanup

On a systemd-based Linux server, stop and disable File Browser:

```sh
sudo systemctl disable --now filebrowser.service
```

If you used the included setup script, the default database path is:

```text
/var/lib/filebrowser/filebrowser.db
```

Do not delete server data unless you understand what File Browser root and database paths are being used.
