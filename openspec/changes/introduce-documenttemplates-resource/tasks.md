# Tasks

Scope is approved; implementation is blocked on section 1. Do not silently preserve legacy status inversion, inline directives, or destructive replacement behavior.

## 1. Contract approval gate

- [ ] 1.1 Resolve portable identity, manifest/schema layout, file paths, export status selection, collisions, and empty input; verify approved examples and acceptance scenarios are recorded.
- [ ] 1.2 Resolve status representation, replacement intent/ID preservation/recovery, cleanup action/scope, and failure guarantees; verify design explicitly covers deletion-success/creation-failure.
- [ ] 1.3 Define supported Word parts/references and Excel adaptation, file validation, and target metadata errors; verify representative fixture cases are listed.
- [ ] 1.4 Refine specs and downstream tasks to the approved contract; verify no blocking gate remains and `pnpm exec openspec validate introduce-documenttemplates-resource --strict` passes.

## 2. Resource and document contract

- [ ] 2.1 Add documenttemplate source/test projects, move owned models and OpenXML dependency, and synchronize affected lock files; verify focused build and locked restore after dependency changes.
- [ ] 2.2 Implement settings/schemas and manifest/file resolution with useful existing fixtures; verify parsing, schema, missing/invalid file, and collision tests.
- [ ] 2.3 Implement approved export selection and explicit status representation; verify Word/Excel manifest-content-status round trips.
- [ ] 2.4 Document the file and status contract in README; verify sample manifests validate and no legacy inverted boolean is presented as the new contract.

## 3. Import lifecycle and content

- [ ] 3.1 Implement target metadata adaptation for the approved document parts; verify Word/Excel fixtures remain valid with different target metadata.
- [ ] 3.2 Implement import matching and approved explicit replacement semantics; verify unchanged, status-only, content replacement, and partial replacement failure cases.
- [ ] 3.3 Preserve unlisted templates by default and implement approved scoped cleanup; verify missing/invalid scope and out-of-scope protection.
- [ ] 3.4 Document replacement risks, cleanup, and approved execution guarantees; verify docs agree with failure tests.

## 4. CLI replacement and integration

- [ ] 4.1 Register `documenttemplates import/export`, wire DI, and remove old paths; verify CLI smoke/settings tests, completion metadata, and old-path rejection.
- [ ] 4.2 Remove proven obsolete legacy document code/dependencies and update README plus `docs/migrations/2.x-to-3.x.md`; verify all old-to-new command, format, status, and replacement changes are documented.
- [ ] 4.3 Run focused resource/CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and reconcile implemented main specs before archive.
