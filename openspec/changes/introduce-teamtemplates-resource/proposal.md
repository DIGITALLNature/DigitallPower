# Proposal

## Why

Teamtemplate import and export are separated into verb-oriented modules, and import currently deletes every existing template omitted from the input. Resource ownership should make synchronization intent explicit.

## What Changes

- Consolidate import and export into `dgt.power.teamtemplates`.
- Expose `teamtemplates import` and `teamtemplates export`.
- Match templates by GUID only, rejecting table reassignment and table/name collisions rather than falling back to names.
- **BREAKING**: Replace the legacy JSON list with a versioned `schemaVersion`/`templates` envelope and a resource-owned JSON schema.
- Leave unlisted templates unchanged by default. Cleanup requires explicitly named tables, never a global scope, and runs only after successful creates/updates.
- Exclude system templates from export and protect them from import updates and cleanup.
- Validate the complete import before writes, provide a write-free dry-run, and stop on the first runtime mutation failure with explicit partial-result reporting.
- Export all non-system templates or filter by logical table names. Import the whole file. Existing export files require `--overwrite`.
- Accept empty input as a no-op without cleanup; reject it with cleanup.
- **BREAKING**: Remove `import teamtemplates` and `export teamtemplates` without aliases; do not preserve destructive default behavior.
- Out of scope: unrelated template types and cleanup of the entire legacy import/export modules.

## Capabilities

### New Capabilities

- `team-template-management`: Resource-oriented transfer and explicit synchronization of teamtemplates.

### Modified Capabilities

None.

## Impact

Touches both template commands, DTOs, shared file/configuration infrastructure, host registration and DI, solution/project wiring, module/CLI tests, and a new resource schema. Update README and `docs/migrations/2.x-to-3.x.md` with path, format, identity, overwrite, and cleanup changes. Required before legacy import/export retirement; independent of documenttemplates and duplicaterules, with shared host integration aligned on `marcel/planing2026`.
