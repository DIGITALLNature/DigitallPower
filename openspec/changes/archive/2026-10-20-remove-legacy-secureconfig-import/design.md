# Design

## Context

See `proposal.md` for motivation. `CommandTree.Register` exposes the legacy import and the
replacement. Runtime and completion share this registration, so removing the registration
removes both surfaces without a separate completion implementation.

The legacy command and `tests/dgt.power.import.tests/SecureConfigImportTests.cs` are the
source consumers found for `src/dgt.power.common/DTO/SecureConfig.cs`. The DTO serializes
`PluginStep` as `plugin_step` and `Data` as `secure_config`, despite the backlog's shorthand
description. No corresponding schema was found in `schemas/`.

The plugin command already supports inline/file secure values, IDs/composite keys, and
unsecure configuration. Its secure-config repository creates and links in one transaction.
Existing tests cover creation, update with link preservation, selection, and unsecure input.

## Goals / Non-Goals

**Goals:** Delete the redundant execution path and its exclusive artifacts; preserve the
replacement's behavior and keep failures for the removed syntax at command parsing.

**Non-Goals:** No name-based compatibility selector, envelope converter, new export command,
batch API, authentication change, or unrelated analyzer/maintenance retirement. Do not modify
the generated Dataverse secure-config entity, shared import settings, or plugin repositories.

## Decisions

### Remove the command outright

Delete the registration, legacy command, exclusive DTO, and dedicated test class. Retain the
rest of the import module. Do not add a forwarding alias: name-based legacy selection cannot
be translated reliably into the replacement's selectors, and an adapter would perpetuate the
retired format.

### Preserve the existing replacement

Use `plugin step config set` as-is. Tests will lock down secure-file input and relevant edge
cases where coverage is missing; they will not introduce feature parity. The spec additions
describe the existing replacement contract rather than a new configuration subsystem.

### Keep migration details user-facing and exact

Remove the legacy README row and update the existing plugin-configuration guidance as needed.
Add a section to `docs/migrations/2.x-to-3.x.md`, following the existing planned major-release
migrations. Show a placeholder-only old JSON example with `plugin_step` and `secure_config`.
Explain that users must resolve the old display name to a step ID or complete composite key.

Show the preferred replacement:

```powershell
dgtp plugin step config set --step-id <guid> --secure-file .\secure-value.txt
```

The file contains only the intended configuration text, not the old JSON envelope. Include
a composite-key example, optional inline input, explicit empty versus omitted values, and
that `--filedir` / `--filename` / legacy `--inline` do not carry over. Keep real secrets out
of examples and warn that inline values can appear in process arguments or shell history.
No automatic migration or Dataverse-record deletion is needed.

### Keep cleanup proportional

Remove the resolved secure-import section from `todo.md`; preserve unrelated debt entries.
Leave generated changelogs, archived changes, and `baseline.sarif.json` unchanged. There is
no schema to delete; do not manufacture a schema change.

## Risks / Trade-offs

- Existing scripts stop working -> ship as a breaking change with explicit migration examples,
  not as an unannounced patch; use a Conventional Commit breaking marker when committing.
- External code may reference the public command or DTO -> document their removal in migration
  guidance; repository reference searches cannot establish external usage.
- Legacy step display names are environment-specific -> require explicit operator selection
  through an ID or composite key instead of guessing a match.
- An old JSON envelope could accidentally become the secret value -> explicitly document that
  `--secure-file` reads raw text and does not parse the legacy format.
- Removing dedicated tests could hide replacement gaps -> preserve existing plugin coverage
  and add focused missing scenarios before finishing.

## Migration Plan

1. Review and authorize this proposal before implementation.
2. Add CLI retirement regressions, remove legacy code/tests, and complete focused replacement
   coverage without changing its behavior.
3. Update README, migration guide, and backlog together with the removal.
4. Build the solution and run the CLI, import, and plugin test assemblies using `dotnet exec`
   on their built DLLs. Validate OpenSpec and synchronize main specs when implementation is
   complete; archive after acceptance and refresh generated OpenSpec instructions as required.
5. Release with the planned major-version breaking changes. If release targeting changes,
   move the migration section to the matching major-version guide before shipping.

Rollback is a code/release rollback, not a data rollback: this change itself performs no
Dataverse migration and does not delete existing secure configurations.
