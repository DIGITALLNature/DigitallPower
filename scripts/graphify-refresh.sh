#!/usr/bin/env sh

cd "$(dirname "$0")/.." || exit 0

graphify_version=$(tr -d '\r\n' < graphify-version.txt 2>/dev/null)
if [ -z "$graphify_version" ] || ! command -v uvx >/dev/null 2>&1; then
    printf '%s\n' '[graphify] uvx or graphify-version.txt unavailable; refresh skipped' >&2
    exit 0
fi

if ! uvx --from "graphifyy==$graphify_version" graphify update .; then
    printf '%s\n' '[graphify] refresh failed; Git operation will continue' >&2
fi

exit 0