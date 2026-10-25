# Design

## Context

`TeamTemplateImport` matches by GUID and deletes unlisted templates across its queried target set. `ShouldDeleteMissingTeamTemplate` explicitly asserts this behavior. Export records logical table names and resolves object type codes through metadata. Import and export use separate generic settings and base helpers.

## Goals / Non-Goals

**Goals:** Bring both directions under one resource owner, preserve GUID-based identity, define a portable file contract, and make destructive synchronization explicit and bounded.

**Non-Goals:** Share a generic template pipeline with documenttemplates or retire unrelated transfer commands.

## Decisions

### Resource ownership and integration

Introduce `src/modules/dgt.power.teamtemplates` and `tests/dgt.power.teamtemplates.tests`, owning both commands, settings, transfer models, schema, planning, and tests. Do not depend on the legacy import/export assemblies. Reuse shared file/configuration infrastructure rather than resource-specific legacy base classes.

Use `PowerCommand<TSettings>`, injected connection/file/configuration services as needed, and the injected console. Validate local input before acquiring the connection; complete acquisition before any work display. Keep Dataverse calls asynchronous and cancellation-aware.

Register only `teamtemplates import/export`. Shared host, solution, completion, and CLI-test changes must preserve the independent documenttemplates and duplicaterules work when integrated into `marcel/planing2026`; no shared generic template pipeline is required.

### Identity and conflict handling

The non-empty template GUID is the sole identity. Preserve it on creation and use it for updates; never match by name as a fallback. Names can change, but an existing GUID cannot move to a different table. Reject table/name collisions under different GUIDs, including collisions with protected system templates. Reject duplicate GUIDs and duplicate table/name pairs within the file before writes.

Keep table references portable through logical names. Resolve target object type codes from metadata rather than carrying source-environment codes in the file.

### Versioned transfer contract

Use a version 1 envelope and publish its schema at `schemas/teamtemplates/v1/schema.json`. Keep existing template field spellings inside the new envelope to avoid unrelated field renames:

```json
{
  "schemaVersion": 1,
  "templates": [
    {
      "teamtemplateid": "4e17461c-efec-43a0-835b-08fa78af1f83",
      "entity": "account",
      "teamtemplatename": "Account readers",
      "description": null,
      "defaultaccessrightsmask": 1
    }
  ]
}
```

Require GUID, logical table name, nonblank template name, and an explicit valid access-rights mask. Description is optional; omitted or null clears an existing description. This is desired-state input, not a patch format. Do not include `issystem` or environment-specific object type codes. Reject unsupported versions, legacy bare arrays, invalid fields, and unsupported access-rights combinations with actionable errors rather than silently converting them.

Validate rights against Dataverse-supported access-team template rights, not arbitrary integer values. Source/test implementation must establish the supported bit set from the SDK/platform contract and cover invalid bits.

### Selection, files, and empty input

Export all non-system templates unless explicitly filtered by logical table names. Import processes the entire file; do not add import selection or GUID/name filters. Keep export selection separate from cleanup scope. Export preserves GUIDs, logical table associations, descriptions, and rights in importable data.

Refuse to replace an existing export file unless `--overwrite` is supplied. Dry-run must not create or replace files. A valid empty `templates` list is a successful no-op without cleanup; with cleanup it is a preflight error, not a request to empty the target tables. Missing or invalid files always fail.

### Metadata and system-template protection

For each input table and cleanup table, require an existing table, a resolvable object type code, and `AutoCreateAccessTeams == true`; neither false nor null is sufficient. Do not enable access teams implicitly.

Writable intent consists of name, description, and access rights, plus table association on creation. `IsSystem` is not writable or transferable. Exclude system templates from exports and deletions; reject any import entry whose GUID targets a system template before writes.

### Preflight, dry-run, and mutation execution

Build and validate the complete create/update/unchanged/delete plan before the first mutation. Preflight covers the complete file, version/fields/rights, duplicate identities, target conflicts, protected templates, table metadata, and any cleanup selector. Validation failure means zero Dataverse writes, even if preceding entries are valid.

Dry-run uses the same preflight and reports planned creates, updates, unchanged entries, and deletes without Dataverse or export-file writes. It may read Dataverse and authenticate. It must not turn an invalid plan into a successful result.

Apply creates/updates first, skipping unchanged items, and only then cleanup. On the first runtime mutation failure, stop further mutations, return failure, and explicitly report completed and failed work. There is no transaction or rollback guarantee. Never begin cleanup after a create/update failure; stop remaining deletes after a deletion failure. Cancellation must not start additional mutations.

### Explicit table-scoped cleanup

Cleanup requires both an explicit cleanup request and a non-empty explicit list of logical table names. Reject absent, invalid, or global scopes before any mutation. Never derive the scope from file contents or an export filter. Only unlisted non-system templates in the named tables are deletion candidates; input entries outside the cleanup tables still import normally.

System templates and templates outside the explicit scope remain unchanged by cleanup. Empty input with cleanup is rejected even when the table selector is valid. The previous global delete-before-validation pass is removed rather than exposed as an alternative default.

## Risks / Trade-offs

- Existing deletion tests encode an unsafe default -> replace them with preservation and explicit-scoped-cleanup tests.
- Environments may contain equivalent names under different GUIDs -> fail preflight and require intentional file/target reconciliation; never silently remap identity.
- Access teams may be disabled -> test true, false, and null metadata values for creation, updates, and cleanup validation.
- Runtime failures and concurrent target changes can leave partial results -> state the lack of rollback, fail fast, and report actual completed mutations.
- Existing users must convert files and opt into overwriting/cleanup -> document executable examples and the breaking behavior in the migration guide.

## Migration Plan

Introduce the new module and tests, replace host paths, and remove only teamtemplate-owned legacy code after checking consumers. Retain the generated Dataverse entity and any models still used elsewhere. Legacy transfer module removal remains deferred to `retire-legacy-import-export`.

Update README and `docs/migrations/2.x-to-3.x.md` with before/after command and envelope examples. Migration wraps legacy entries, supplies required rights, and removes `issystem`; legacy arrays are not automatically accepted. Explain GUID conflicts, description clearing, protected templates, overwrite opt-in, empty input, cleanup limits, dry-run, and partial-failure guarantees.

Update current main specs to implemented behavior during implementation. Keep this change active through its PR into `marcel/planing2026`; archive only as a separate follow-up after a verified implementation merge into `beta` or `main`.

## Open Questions

None blocking implementation. The public-contract decisions above were explicitly confirmed during refinement.
