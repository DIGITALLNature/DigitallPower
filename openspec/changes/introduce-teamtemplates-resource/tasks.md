# Tasks

Scope is approved; implementation is blocked on section 1. Explicit cleanup is agreed, but its selector and guarantees are not yet approved.

## 1. Contract approval gate

- [ ] 1.1 Resolve portable identity, file/schema format, selection, overwrite, empty input, and writable metadata with the user; verify approved examples are added to design/specs.
- [ ] 1.2 Resolve cleanup scope, system-template protection, and template-specific preflight/dry-run/failure guarantees; verify explicit acceptance scenarios and no unresolved destructive defaults.
- [ ] 1.3 Refine implementation tasks to the approved options and file contract; verify `pnpm exec openspec validate introduce-teamtemplates-resource --strict` passes.

## 2. Resource-owned transfer

- [ ] 2.1 Add teamtemplate source/test projects and move both transfer directions and their owned models without legacy module dependencies; verify focused builds and retained transfer tests.
- [ ] 2.2 Implement approved settings/schemas and table metadata resolution; verify parsing, schema fixtures, cross-environment object type codes, and access-team validation tests.
- [ ] 2.3 Implement export/import matching and unchanged-item handling; verify round-trip, ambiguity, and idempotence tests.
- [ ] 2.4 Document the resource file contract and examples in README; verify examples match executable fixtures.

## 3. Safe synchronization

- [ ] 3.1 Replace default deletion with preservation of unlisted templates; verify the former deletion test is replaced by default-preservation coverage.
- [ ] 3.2 Implement approved explicit scoped cleanup and failure guarantees; verify missing/invalid scope, out-of-scope protection, and applicable system-template cases.
- [ ] 3.3 Document cleanup semantics and the breaking default change in README and `docs/migrations/2.x-to-3.x.md`; verify examples cannot imply implicit global deletion.

## 4. CLI replacement and integration

- [ ] 4.1 Register `teamtemplates import/export`, wire DI, and remove the two old paths; verify CLI smoke/settings tests, completion metadata, and old-path rejection.
- [ ] 4.2 Remove obsolete teamtemplate code from legacy modules after consumer checks; verify unrelated import/export tests remain intact and migration documentation covers path/format changes.
- [ ] 4.3 Run focused resource/CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and update main specs to implemented behavior before archive.
