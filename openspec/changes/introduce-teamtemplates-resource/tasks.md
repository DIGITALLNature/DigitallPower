# Tasks

The contract is approved. Implementation uses GUID identity, a versioned envelope, full preflight, write-free dry-run, and explicit table-scoped cleanup. No blocking contract questions remain.

## 1. Approved contract

- [x] 1.1 Resolve identity, file format, selection, overwrite, empty input, and writable fields with the user; verify the confirmed contract and envelope example are captured in design/specs.
- [x] 1.2 Resolve explicit cleanup tables, system protection, preflight, dry-run, and failure guarantees; verify acceptance scenarios cover rejected scopes, zero-write validation failures, and partial runtime failures.
- [x] 1.3 Validate the refined artifacts with `pnpm exec openspec validate introduce-teamtemplates-resource --strict`; verify no unresolved implementation gates remain.

## 2. Resource ownership and file contract

- [x] 2.1 Add resource source/test projects, solution wiring, and resource-owned commands/settings/models without legacy assembly dependencies; verify focused builds and document the new module in README.
- [x] 2.2 Implement the version 1 envelope and `schemas/teamtemplates/v1/schema.json`; verify valid round-trip fixtures and rejection of bare arrays, unsupported versions, missing/invalid fields, duplicate GUIDs, and duplicate table/name pairs.
- [x] 2.3 Define and validate supported access-team rights from the SDK/platform contract, required GUID/table/name/rights, and optional description clearing; verify invalid rights and missing/null description tests.
- [x] 2.4 Document file fields, versioning, and a schema-valid example in README; verify documentation examples against executable fixtures.

## 3. Export and metadata resolution

- [x] 3.1 Resolve logical table names and target object type codes, requiring access-team enablement for input/cleanup tables; verify different environment codes, unknown tables, absent codes, and true/false/null enablement.
- [x] 3.2 Export all non-system templates or explicitly selected tables into the envelope; verify filter behavior, system exclusion, empty exports, and preserved GUID/table/rights/description values.
- [x] 3.3 Reject existing destinations unless `--overwrite` is provided and support file-write-free export dry-run; verify destination preservation and that dry-run never creates/replaces a file.
- [x] 3.4 Document export selection, overwrite, and dry-run in README; verify command examples match settings and fixtures.

## 4. Import planning and preflight

- [x] 4.1 Plan GUID-only creates/updates/unchanged entries for the whole file; verify preserved creation GUIDs, rename/update behavior, idempotence, and default preservation of unlisted templates.
- [x] 4.2 Reject GUID table reassignment, table/name collisions under different GUIDs, and writes to system templates; verify each conflict prevents all writes, including when a later entry conflicts.
- [x] 4.3 Validate the complete plan before writes and expose import dry-run using the same checks; verify planned counts/actions, invalid-plan failure, and zero Dataverse/file writes.
- [x] 4.4 Accept empty lists only as non-cleanup no-ops and reject missing/invalid files; verify no authentication is required for locally rejectable input and document desired-state description semantics and identity conflicts.

## 5. Safe execution and cleanup

- [x] 5.1 Require explicit cleanup plus a non-empty list of logical tables, independent of file/export selection; verify missing, invalid, and global scope rejection before any writes, including empty input with cleanup.
- [x] 5.2 Delete only unlisted non-system templates inside the explicit scope after successful creates/updates; replace the legacy deletion test and verify out-of-scope/system protection and importing entries outside cleanup tables.
- [x] 5.3 Fail fast on the first mutation error and honor cancellation without starting more mutations; verify completed/failed result reporting, no cleanup after create/update failure, and no remaining deletes after delete failure.
- [x] 5.4 Document cleanup examples, preflight/dry-run behavior, failure ordering, and lack of rollback in README and `docs/migrations/2.x-to-3.x.md`; verify examples never imply inferred/global cleanup or atomic execution.

## 6. CLI replacement and legacy removal

- [x] 6.1 Register `teamtemplates import/export`, DI, help, and completion with the resource settings; update CLI tree/settings tests for filters, cleanup scope, dry-run, overwrite, and defaults, and verify old paths fail without writes.
- [x] 6.2 Remove only obsolete teamtemplate commands/models/tests from legacy modules after checking consumers; retain shared/generated types still needed elsewhere and verify unrelated import/export tests.
- [x] 6.3 Document old/new command paths and legacy-list-to-envelope migration in `docs/migrations/2.x-to-3.x.md`; verify examples preserve GUIDs, supply required rights, and omit `issystem`, and keep README tables/completion examples current.

## 7. Integration verification

- [x] 7.1 Update main specs to implemented behavior; run focused resource/CLI and affected legacy tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify all pass and shared host/solution wiring preserves other resource registrations.

## Workflow follow-up

- Submit the implementation PR from `feat/teamtemplates-resource` into `marcel/planing2026` and align shared integration points with the parallel documenttemplates and duplicaterules work.
- Keep this change active after that integration PR; archive separately only after verifying the implementation has merged into `beta` or `main`.
