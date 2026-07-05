#!/bin/sh
set -eu

ROOT=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd)
PROJECT="$ROOT/src/FileBrowserDesktop.csproj"
DIST="$ROOT/dist"
STAGE_ROOT="$DIST/stage"
RID="${RID:-}"

detect_rid() {
  os=$(uname -s)
  arch=$(uname -m)

  case "$os:$arch" in
    Linux:x86_64|Linux:amd64) printf '%s\n' linux-x64 ;;
    Linux:aarch64|Linux:arm64) printf '%s\n' linux-arm64 ;;
    Darwin:x86_64|Darwin:amd64) printf '%s\n' osx-x64 ;;
    Darwin:arm64|Darwin:aarch64) printf '%s\n' osx-arm64 ;;
    *)
      echo "Unsupported build host: $os $arch" >&2
      exit 1
      ;;
  esac
}

copy_docs() {
  destination=$1
  mkdir -p "$destination"
  for file in README.md INSTALL.md LICENSE CHANGELOG.md CONTRIBUTING.md SECURITY.md SUPPORT.md SERVER_SETUP.md PACKAGING.md; do
    if [ -f "$ROOT/$file" ]; then
      cp "$ROOT/$file" "$destination/"
    fi
  done
}

if [ -z "$RID" ]; then
  RID=$(detect_rid)
fi

case "$RID" in
  linux-*|osx-*) ;;
  *)
    echo "Unsupported runtime identifier: $RID" >&2
    exit 1
    ;;
esac

PUBLISH="$ROOT/src/bin/Release/net8.0/$RID/publish"
rm -rf "$PUBLISH" "$STAGE_ROOT/$RID"
mkdir -p "$DIST" "$STAGE_ROOT"

dotnet publish "$PROJECT" -c Release -r "$RID" --self-contained false -o "$PUBLISH"

if [ ! -e "$PUBLISH/FileBrowserDesktop" ]; then
  echo "Publish completed, but FileBrowserDesktop was not found in $PUBLISH" >&2
  exit 1
fi

case "$RID" in
  linux-*)
    STAGE="$STAGE_ROOT/$RID/FileBrowserDesktop"
    ARCHIVE="$DIST/FileBrowserDesktop-$RID-framework-dependent.tar.gz"
    mkdir -p "$STAGE"
    cp -R "$PUBLISH"/. "$STAGE/"
    cp "$ROOT/run-filebrowser-desktop.sh" "$STAGE/"
    copy_docs "$STAGE/docs"
    chmod +x "$STAGE/FileBrowserDesktop" "$STAGE/run-filebrowser-desktop.sh"
    rm -f "$ARCHIVE"
    tar -czf "$ARCHIVE" -C "$STAGE" .
    ;;
  osx-*)
    APP_NAME="File Browser Desktop.app"
    STAGE="$STAGE_ROOT/$RID"
    APP_DIR="$STAGE/$APP_NAME"
    ARCHIVE="$DIST/FileBrowserDesktop-$RID-framework-dependent.app.tar.gz"
    mkdir -p "$APP_DIR/Contents/MacOS" "$APP_DIR/Contents/Resources/docs"
    cp -R "$PUBLISH"/. "$APP_DIR/Contents/MacOS/"
    copy_docs "$APP_DIR/Contents/Resources/docs"
    cat > "$APP_DIR/Contents/Info.plist" <<'PLIST'
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
  <key>CFBundleExecutable</key>
  <string>FileBrowserDesktop</string>
  <key>CFBundleIdentifier</key>
  <string>ai.paravane.filebrowserdesktop.macoslinux</string>
  <key>CFBundleName</key>
  <string>File Browser Desktop</string>
  <key>CFBundleDisplayName</key>
  <string>File Browser Desktop</string>
  <key>CFBundlePackageType</key>
  <string>APPL</string>
  <key>CFBundleShortVersionString</key>
  <string>1.1.1</string>
  <key>CFBundleVersion</key>
  <string>1.1.1</string>
</dict>
</plist>
PLIST
    chmod +x "$APP_DIR/Contents/MacOS/FileBrowserDesktop"
    rm -f "$ARCHIVE"
    tar -czf "$ARCHIVE" -C "$STAGE" "$APP_NAME"
    ;;
esac

echo
echo "Created:"
ls -1 "$DIST"/FileBrowserDesktop-*.tar.gz
