# Proposal

## Why

JSON files written by the CLI encode non-ASCII text inconsistently. `process init` and `duplicaterules init` write German display names as `\u00E4` escapes; for example, a live capture produced `"Kontakte mit derselben gesch\u00E4ftlichen Telefonnummer"` and an escaped no-break space (`\u00A0`). Export and `FileService` use a different double-serialize and `Regex.Unescape` workaround, and teamtemplates uses the default encoder. Readers and reviewers of version-controlled configuration expect readable UTF-8 text, and each new resource module currently picks its own variant.

## What Changes

- Define one observable contract for JSON files the CLI writes: UTF-8 without escaping of printable non-ASCII characters, escaping only what JSON requires, and stable indentation.
- Provide one shared serialization entry point for generated JSON files and use it in resource capture writers (`process`, `duplicaterules`, `teamtemplates`, and `documenttemplates` once merged), export JSON output, and `FileService.ExportJsonFile`.
- Replace the double-serialize and `Regex.Unescape` workaround with the shared serializer after verifying its current behavior with backslashes, quotes, and control characters.
- Output-only change: readers continue to accept both escaped and unescaped input, so existing files remain valid. Byte-level output changes for files containing non-ASCII text, so version-controlled files show a one-time diff on the next capture.

## Capabilities

### New Capabilities

- `json-file-output`: Encoding and formatting guarantees for JSON files that CLI commands create or replace.

### Modified Capabilities

None. Existing capability specs do not state an encoding; resource specs keep owning their content and overwrite rules.

## Impact

- Code: `src/dgt.power.common` (new shared JSON output helper; `FileAccess/FileService.cs`), `src/modules/dgt.power.export/Base/ExportJson.cs`, `src/modules/dgt.power.process/Logic/ProcessConfigurationWriter.cs`, `src/modules/dgt.power.duplicaterules/Logic/DuplicateRuleConfigurationWriter.cs`, `src/modules/dgt.power.teamtemplates/Configuration/TeamTemplateConfiguration.cs`, and the documenttemplates writer if present.
- Out of scope: internal state and secret stores (`StateStore`, `ConnectionStore`, `SecretStore`), analyzer and lint reports unless analysis shows they are user-reviewed artifacts, JSON read paths, schemas, and console output.
- Coordination: lands after the parallel resource PRs into `marcel/planing2026` to avoid conflicts in the writers.
- No schema, CLI option, or configuration format change.
