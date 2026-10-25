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

- **Encoder:** Use `JavaScriptEncoder.Create(UnicodeRanges.All)` or `JavaScriptEncoder.UnsafeRelaxedJsonEscaping`. `UnicodeRanges.All` still escapes `<`, `>`, `&`, and `'`, which conflicts with the requirement not to escape HTML-sensitive characters. `UnsafeRelaxedJsonEscaping` matches the spec. The name only warns about embedding in HTML or JavaScript, which these files never are. Preferred: `UnsafeRelaxedJsonEscaping`, with a comment explaining why it is safe here.
- **Shared helper over per-module options:** Each module passes its own `JsonSerializerOptions` for naming and null handling. The helper copies them and enforces the encoder, so modules cannot drift. Alternative: a global DI `JsonSerializerOptions` singleton. Rejected because static writers do not use DI and because the DI singleton is also used for reading.
- **Export workaround:** Replace it only after characterization tests show what it currently outputs for backslashes, quotes, and control characters. If it currently produces invalid JSON for such values, record that as a bug fix in the PR.

## Risks / Trade-offs

- [One-time diff in version-controlled files] → Document in the PR and README that the next capture rewrites escaped characters; content is semantically unchanged.
- [Supplementary characters and unpaired surrogates] → Covered by tests; the encoder still escapes invalid sequences.
- [Parallel writer changes in the resource PRs] → Implement after those PRs are merged into `marcel/planing2026`.

## Open Questions

- Should analyzer and lint reports follow the same contract? They are written by commands and may be reviewed, but are not configuration input. Decide during implementation by checking whether any of them contain user-facing non-ASCII text; this does not change the shared helper.
