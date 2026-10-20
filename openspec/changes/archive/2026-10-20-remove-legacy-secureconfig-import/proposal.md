# Proposal

## Why

`import secureconfigs` duplicates secure plugin-step configuration already supported by
`plugin step config set`. Removing the legacy path reduces maintenance and eliminates its
separate create/link/cleanup implementation without requiring feature parity.

## What Changes

- **BREAKING**: Remove `import secureconfigs` without an alias or compatibility adapter.
- **BREAKING**: Remove the legacy `SecureConfigImport` command type and exclusively used
  `dgt.power.dto.SecureConfig` DTO, together with dedicated tests.
- Preserve the existing `plugin step config set` implementation and options for secure and
  unsecure configuration; do not add display-name selection or legacy JSON parsing.
- Document migration from `plugin_step` and `secure_config` JSON fields to step-ID or
  composite-key selection and `--secure` / `--secure-file`.
- Update CLI regression coverage, README, major-version migration guidance, and the resolved
  technical-debt entry.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

- `cli-command-surface`: The legacy secure-configuration import is no longer registered,
  advertised, or accepted.
- `plugin-step-management`: Document the existing configuration command as the supported
  replacement, with its current selectors and raw-value/file input.

## Impact

Affected areas are `src/dgt.power/CommandTree.cs`, the import module, the shared legacy DTO,
CLI/import/plugin tests, `README.md`, `docs/migrations/2.x-to-3.x.md`, and `todo.md`.
The existing main plugin-step specification describes state operations only; the delta adds
the relevant existing configuration contract, not new command behavior.

The plan targets the existing 3.x breaking-change migration guide; confirm release targeting
before implementation if this removal will ship in a later major version. No dependency,
Dataverse data, or unrelated import/export changes are intended. No corresponding legacy
secure-configuration schema was found under `schemas/`, so no schema removal is currently
required. Historical changelogs, archives, and the Qodana baseline remain untouched.
