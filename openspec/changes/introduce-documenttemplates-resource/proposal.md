# Proposal

## Why

Documenttemplate import and export are separated by technical verb. Existing import includes document rewriting, deactivation, and delete/recreate behavior that must be deliberately specified rather than copied into a new namespace.

## What Changes

- Consolidate import and export into `dgt.power.documenttemplates`.
- Expose `documenttemplates import` and `documenttemplates export`.
- Leave every unlisted template unchanged; do not offer cleanup.
- Export only active Word/Excel templates, with additional filters narrowing that selection, and activate every imported template. Do not expose a configurable status in the new manifest or CLI.
- Introduce a strict V1 manifest with portable identity, source IDs, explicit metadata, and document paths confined to the manifest directory.
- Update existing templates without changing target IDs or deleting them. Verify regular content updates before implementing the lifecycle; do not provide delete/recreate as a fallback.
- Validate used Word bindings and relationship paths against target metadata, including 1:N, N:1, and N:N relationships. Transfer structurally valid Excel files without content adaptation.
- Provide import preflight and dry-run, followed by best-effort execution across independently prepared templates.
- **BREAKING**: Remove `import documenttemplates` and `export documenttemplates` without compatibility aliases.
- **BREAKING**: Replace the legacy manifest and inline directives without compatibility parsing.
- Out of scope: cleanup, destructive replacement, draft-state transfer, other template resources, and general-purpose document processing.

## Capabilities

### New Capabilities

- `document-template-management`: Resource-owned transfer and explicit lifecycle handling of documenttemplates.

### Modified Capabilities

None.

## Impact

Touches template import/export, DTOs and file handling, host registration and DI, schemas, tests, and OpenXML dependency placement. Update README and the major-version migration guide. Required before legacy import/export retirement; independent of teamtemplates.
