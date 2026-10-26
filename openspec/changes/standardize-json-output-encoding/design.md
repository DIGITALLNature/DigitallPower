# Design

## Context

JSON output is produced in three ways today:

| Variant | Used by | Observed or suspected behavior |
|---------|---------|--------------------------------|
| `JsonSerializer.Serialize` with default encoder | `ProcessConfigurationWriter`, `DuplicateRuleConfigurationWriter`, `TeamTemplateConfiguration` | Escapes all non-ASCII as `\uXXXX` (confirmed live for duplicaterules) |
| Double `Serialize` + `Regex.Unescape` + `Trim('"')` | `ExportJson.GetJson`, `FileService.ExportJsonFile` | Intent unclear; correctness for backslashes, quotes, and control characters is unverified |
| Default encoder, single line | `AnalyzeResultWriter`, `SolutionLintCommand` (indented) | Not user-edited configuration; scope to be confirmed |

Atomic writing is already shared through `dgt.power.common.FileAccess.AtomicFileWriter`.

## Goals / Non-Goals

**Goals:**
- One shared serialization helper in `dgt.power.common` for user-facing JSON files.
- Characterization tests that pin current export output before replacing the `Regex.Unescape` workaround.

**Non-Goals:**
- Changing property naming, ordering, null handling, or indentation decided by each resource.
- Internal stores (`StateStore`, `ConnectionStore`, `SecretStore`) and code-generation config rewriting.

## Decisions

- **Encoder:** Use `JavaScriptEncoder.UnsafeRelaxedJsonEscaping`. `UnicodeRanges.All` still escapes `<`, `>`, `&`, and `'`, which conflicts with the requirement not to escape HTML-sensitive characters. The name only warns about embedding in HTML or JavaScript, which these files never are. Verified during implementation: every System.Text.Json encoder, including relaxed ones and `TextEncoderSettings.AllowCharacter`, still escapes U+00A0 (a space separator), and unpaired surrogates are replaced by U+FFFD rather than escaped. The helper therefore restores `\u00A0` escapes after serialization, consuming escape sequences pairwise so an escaped backslash followed by `u00A0` is untouched. A custom `JavaScriptEncoder` subclass was rejected because its abstract members are `unsafe`.
- **Shared helper over per-module options:** Each module passes its own `JsonSerializerOptions` for naming and null handling. `dgt.power.common.FileAccess.JsonFileOutput.Serialize` copies them and enforces the encoder, so modules cannot drift and callers' options (also used for reading) are never mutated. Alternative: a global DI `JsonSerializerOptions` singleton. Rejected because static writers do not use DI and because the DI singleton is also used for reading.
- **Export workaround:** Obsolete. The export module and its `ExportJson`/`FileService` writers were removed by `retire-legacy-import-export`, so there is no `Regex.Unescape` workaround left to replace or characterize.
- **Scope decision (call sites under `src/`):**

| Call site | Decision |
|-----------|----------|
| `ProcessConfigurationWriter`, `DuplicateRuleConfigurationWriter`, `TeamTemplateConfiguration.Serialize`, `DocumentTemplateManifest.ToJson` | In scope; use the helper |
| `AnalyzeResultWriter`, `SolutionLintCommand` reports | Out of scope; machine-oriented reports, not configuration input |
| `StateStore`, `ConnectionStore`, `SecretStore`, `CodeGenerationConfigLoader` | Out of scope (internal stores and config rewriting, see Non-Goals) |

## Risks / Trade-offs

- [One-time diff in version-controlled files] → Document in the PR and README that the next capture rewrites escaped characters; content is semantically unchanged.
- [Supplementary characters and unpaired surrogates] → Covered by tests; the encoder still escapes invalid sequences.
- [Parallel writer changes in the resource PRs] → Implement after those PRs are merged into `marcel/planing2026`.

## Open Questions

- Should analyzer and lint reports follow the same contract? Resolved: no, see the scope decision above.
