# Proposal

## Why

After process extraction, maintenance remains a collection of unrelated operations. Each needs a user-reviewed disposition before the legacy module can be retired.

## What Changes

- Evaluate bulk delete, auto number, calculated-field protection, carrier information, redundant-component removal, and PowerFx plugin filtering individually with the user.
- Record retain/migrate, replace, or remove decisions and their rationale before changing each feature.
- Create separately reviewed resource changes for retained functionality that needs migration; do not hide those implementations here.
- **BREAKING**: Remove approved obsolete commands and finally the `maintenance` branch/module, only after every feature has an approved disposition and retained functionality has migrated.
- Out of scope: unapproved feature removals, implementing process management, and unrelated analyzer redesign.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

- `cli-command-surface`: Retirement of the legacy maintenance surface after feature disposition.

## Impact

Touches maintenance commands, module references and DI, obsolete schemas/DTOs/tests, host registration, README, and migration guidance. Final module removal depends on `introduce-process-resource` and every additionally approved maintenance migration. Feature decisions are intentionally pending, not implicit deletion approval.
