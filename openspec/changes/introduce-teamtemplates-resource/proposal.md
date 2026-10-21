# Proposal

## Why

Teamtemplate import and export are separated into verb-oriented modules, and import currently deletes every existing template omitted from the input. Resource ownership should make synchronization intent explicit.

## What Changes

- Consolidate import and export into `dgt.power.teamtemplates`.
- Expose `teamtemplates import` and `teamtemplates export`.
- Leave unlisted templates unchanged by default; retain cleanup only as an explicit operation with a clearly bounded target scope.
- **BREAKING**: Remove `import teamtemplates` and `export teamtemplates` without aliases; do not preserve destructive default behavior.
- Decide portable identity, file contract, and cleanup boundaries before implementation.
- Out of scope: unrelated template types and cleanup of the entire legacy import/export modules.

## Capabilities

### New Capabilities

- `team-template-management`: Resource-oriented transfer and explicit synchronization of teamtemplates.

### Modified Capabilities

None.

## Impact

Touches both template commands, DTOs, shared import/export base dependencies, host registration and DI, module/CLI tests, and applicable schemas. Update README and the major-version migration guide. Required before legacy import/export retirement; independent of documenttemplates.
