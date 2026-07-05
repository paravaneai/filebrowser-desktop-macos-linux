# Packaging

The current macOS/Linux preview ships as framework-dependent archives.

Build the package for the current host with:

```sh
sh ./package-release.sh
```

Linux output:

```text
dist/FileBrowserDesktop-linux-x64-framework-dependent.tar.gz
```

macOS output:

```text
dist/FileBrowserDesktop-osx-arm64-framework-dependent.app.tar.gz
```

The exact runtime identifier depends on the build host. Set `RID` to override it:

```sh
RID=linux-x64 sh ./package-release.sh
RID=linux-arm64 sh ./package-release.sh
RID=osx-x64 sh ./package-release.sh
RID=osx-arm64 sh ./package-release.sh
```

## Runtime Decision

This project currently ships as framework-dependent.

Required on the user's machine:

- .NET 8 Runtime
- OpenSSH client available as `ssh`
- Normal macOS/Linux webview/native browser dependencies

Reasons for framework-dependent packaging:

- Smaller release archives
- Easier to inspect
- Keeps SSH host-key handling in OpenSSH

## Installer Options Later

Reasonable future package paths:

- macOS signed `.app` bundle and `.dmg`
- Linux AppImage
- Linux `.deb` and `.rpm`
- Flatpak

Recommended next step:

1. Keep `.tar.gz` and `.app.tar.gz` packages for early testers.
2. Add signing/notarization for macOS.
3. Add Linux AppImage or distro packages once the runtime dependencies are settled.
