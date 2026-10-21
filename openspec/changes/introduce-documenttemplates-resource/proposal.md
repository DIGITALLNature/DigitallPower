# Proposal

## Why

Documenttemplate import and export are separated by technical verb. Existing import includes document rewriting, deactivation, and delete/recreate behavior that must be deliberately specified rather than copied into a new namespace.

## What Changes

- Consolidate import and export into `dgt.power.documenttemplates`.
- Expose `documenttemplates import` and `documenttemplates export`.
- Leave unlisted templates unchanged by default; allow cleanup only explicitly within a clearly bounded target scope.
- Specify Word/Excel transfer, target metadata adaptation, status changes, and destructive replacement.
- **BREAKING**: Remove `import documenttemplates` and `export documenttemplates` without compatibility aliases.
- Out of scope: other template resources and general-purpose document processing.

## Capabilities

### New Capabilities

- `document-template-management`: Resource-owned transfer and explicit lifecycle handling of documenttemplates.

### Modified Capabilities

None.

## Impact

Touches template import/export, DTOs and file handling, host registration and DI, schemas, tests, and OpenXML dependency placement. Update README and the major-version migration guide. Required before legacy import/export retirement; independent of teamtemplates.
