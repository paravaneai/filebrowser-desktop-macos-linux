#!/bin/sh
set -eu

ROOT=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd)

case "$(uname -s 2>/dev/null || printf unknown)" in
  MINGW*|MSYS*|CYGWIN*|Windows_NT)
    echo "File Browser Desktop Mac/Linux does not run on Windows. Use the Windows edition instead." >&2
    exit 1
    ;;
esac

if ! command -v dotnet >/dev/null 2>&1; then
  echo "File Browser Desktop requires .NET 8." >&2
  echo "Install .NET from: https://dotnet.microsoft.com/download/dotnet/8.0" >&2
  exit 1
fi

if [ -x "$ROOT/FileBrowserDesktop" ]; then
  exec "$ROOT/FileBrowserDesktop" "$@"
fi

if [ -x "$ROOT/Contents/MacOS/FileBrowserDesktop" ]; then
  exec "$ROOT/Contents/MacOS/FileBrowserDesktop" "$@"
fi

if [ -f "$ROOT/src/FileBrowserDesktop.csproj" ]; then
  exec dotnet run --project "$ROOT/src/FileBrowserDesktop.csproj" -- "$@"
fi

echo "FileBrowserDesktop executable was not found." >&2
exit 1
