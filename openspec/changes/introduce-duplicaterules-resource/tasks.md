# Tasks

Scope is approved; implementation is blocked on section 1. Do not implement rule definitions, conditions, creation, or deletion.

## 1. Contract approval gate

- [ ] 1.1 Resolve rule identity, selection, portability, owner kinds, mapping needs, capture options, and empty-set semantics with the user; verify approved examples and scenarios are recorded.
- [ ] 1.2 Establish SDK-backed publish/unpublish completion, ownership sequencing, cancellation, timeout, and retry rules; verify design records the evidence and approved behavior.
- [ ] 1.3 Refine schemas/settings and downstream tasks in the plan; verify no blocking gate remains and `pnpm exec openspec validate introduce-duplicaterules-resource --strict` passes.

## 2. Resource contract and capture

- [ ] 2.1 Add resource and test projects, references, and module-owned models; verify a focused build succeeds without a process-module dependency.
- [ ] 2.2 Implement approved settings, schemas, complete retrieval, and init capture; verify parsing, multi-page selection, identity ambiguity, and capture tests.
- [ ] 2.3 Document commands, configuration, and existing-rule-only scope in README; verify examples match schema fixtures.

## 3. Apply and reporting

- [ ] 3.1 Implement full preflight and no-write dry-run; verify invalid rule/owner references and unsupported assignments prevent all batch writes.
- [ ] 3.2 Implement publication and ownership changes with approved completion handling; verify publish/unpublish, omitted properties, idempotence, and no definition mutations.
- [ ] 3.3 Implement independent-rule continuation and failure aggregation; verify partial-effect reporting, cancellation, and failure exit behavior.
- [ ] 3.4 Document operational limits and partial-failure semantics; verify docs agree with the approved contract.

## 4. Host integration

- [ ] 4.1 Register the resource and DI and prune only proven orphaned historical DTOs; verify CLI smoke/settings tests and all consumers of retained shared types.
- [ ] 4.2 Run focused resource/CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and reconcile the implemented main spec before archive.
