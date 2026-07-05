# Changelog

All notable changes to this project will be documented in this file.

## 001.001.001 - 2026-07-05

- Repositioned the app as the macOS/Linux edition.
- Added a Windows runtime block with a user-facing message.
- Removed Windows Credential Manager storage from this edition.
- Disabled credential saving until macOS Keychain and Linux Secret Service support are implemented.
- Switched SSH execution to `ssh` and server setup streaming to `sh -s --`.
- Converted the server setup script to POSIX `sh` syntax.
- Removed Windows `.cmd` launch and release scripts.
- Removed Windows icon embedding and manifest assumptions.
- Added POSIX launch and release packaging scripts.
- Added Linux `.tar.gz` and macOS `.app.tar.gz` packaging paths.
- Updated CI and release workflow definitions for Ubuntu and macOS.
