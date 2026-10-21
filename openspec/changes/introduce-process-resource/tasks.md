# Tasks

Scope is approved; implementation is blocked on section 1. Answering those gates must refine this plan before code changes. Later tasks describe agreed work, not permission to invent unresolved contract details.

## 1. Contract approval gate

- [ ] 1.1 Resolve process identity, category coverage, and selection semantics with the user; verify design records approved keys, filters, indirect business-rule handling, and the AIFlow disposition.
- [ ] 1.2 Resolve owner/mapping formats, lookup keys, impersonation boundaries, and capture/output options; verify reviewed JSON examples and matching acceptance scenarios are added to the plan.
- [ ] 1.3 Resolve category-specific sequencing, dependency handling, retry bounds, and recovery/reporting; verify design and scenarios cover failures after intermediate transitions.
- [ ] 1.4 Reconcile specs and tasks with the approved contract; verify `pnpm exec openspec validate introduce-process-resource --strict` passes and no blocking design gate remains.

## 2. Resource ownership and contract

- [ ] 2.1 Add the process source/test projects and host references with no dependency on maintenance internals; verify project-reference build succeeds.
- [ ] 2.2 Implement approved settings, configuration, owner mappings, and schemas with malformed/ambiguous input tests; verify fixtures validate and parsing tests pass.
- [ ] 2.3 Document the approved file/mapping contract and owner kinds in README and the migration guide; verify examples match the schemas and settings.

## 3. Capture and selection

- [ ] 3.1 Implement category-aware selection and complete paged retrieval, including approved indirect business rules; verify filter, duplicate, multi-page, and cancellation tests.
- [ ] 3.2 Implement selectable state/ownership capture without inferred defaults or silent key collisions; verify each capture mode and approved file-overwrite cases.
- [ ] 3.3 Document capture scope and portable examples; verify examples reproduce the fixture structure.

## 4. Apply and failure semantics

- [ ] 4.1 Implement full reference/identity preflight and dry-run using the same resolved intent; verify missing, ambiguous, and unsupported references produce zero writes.
- [ ] 4.2 Implement explicit state/owner application and impersonation with category rules; verify omitted intent, idempotence, regular/application-user mapping, caller restoration, and owner-only state preservation.
- [ ] 4.3 Implement approved dependency/retry handling and independent-object continuation; verify final failures, cancellation, and unrecovered transitions are reported without false success.
- [ ] 4.4 Document dry-run limits and partial effects; verify examples and exit behavior agree with the execution spec.

## 5. CLI replacement and integration

- [ ] 5.1 Register new commands and DI, remove the two replaced maintenance commands and obsolete models/schemas after consumer checks; verify command-tree smoke, settings parsing, and old-path rejection tests.
- [ ] 5.2 Update README paths/completion examples and `docs/migrations/2.x-to-3.x.md`; verify old-to-new paths and intentional format/default changes are covered without aliases.
- [ ] 5.3 Run focused process and CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and update main specs through the repository workflow before archive.
