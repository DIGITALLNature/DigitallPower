# Proposal

## Why

After process extraction, maintenance remains a collection of unrelated operations. Each needs a user-reviewed disposition before the legacy module can be retired.

## What Changes

- Remove all six remaining maintenance commands, as individually approved by the user.
- Provide no replacement for bulk-delete execution, auto-number format updates, carrier-information export, or PowerFx plugin filtering.
- Preserve calculated-field protection and redundant-component pruning only as independent, deferred Solution feature requests, not required replacements.
- **BREAKING**: Remove the `maintenance` branch/module while preserving the completed resource-oriented process migration.
- Out of scope: implementing the deferred requests, changing existing Dataverse data/metadata, implementing process management, and unrelated analyzer redesign.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

- `cli-command-surface`: Retirement of the legacy maintenance surface after feature disposition.

## Impact

Touches maintenance commands, module references and DI, obsolete schemas/DTOs/tests, host registration, README, and migration guidance. Verify the completed `introduce-process-resource` migration; no other replacement must ship before maintenance retirement.
