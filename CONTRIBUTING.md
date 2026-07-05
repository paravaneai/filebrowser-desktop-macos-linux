# Contributing

Thank you for considering a contribution to File Browser Desktop for macOS and Linux.

## Development Setup

Required on macOS or Linux:

- .NET 8 SDK
- OpenSSH client available as `ssh`
- Normal platform webview/native browser dependencies

Build the app:

```sh
dotnet build src/FileBrowserDesktop.csproj
```

Run from source:

```sh
sh ./run-filebrowser-desktop.sh
```

Create a release package for the current host:

```sh
sh ./package-release.sh
```

## Pull Requests

Please keep pull requests focused and include:

- A clear description of the change.
- Any security impact, especially for SSH, credentials, profile storage, or server setup.
- Manual verification steps.
- Screenshots for user-interface changes when helpful.

## Security-Sensitive Changes

Treat these areas with extra care:

- SSH command construction and host-key behavior.
- Future macOS Keychain or Linux Secret Service credential storage.
- File Browser login prefilling.
- Server installation and systemd service configuration.
- Any change that could expose File Browser beyond localhost.

See `SECURITY.md` before proposing changes in these areas.
