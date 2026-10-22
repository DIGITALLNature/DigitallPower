# Tasks

Existing-rule-only scope, GUID-only identity, and independently optional publication/owner intent are approved. Implementation remains blocked on section 1. Finalize `introduce-portable-owner-resolution` first for ownership; do not implement rule definitions, conditions, creation, deletion, or another principal resolver.

## 1. Contract approval gate

- [ ] 1.1 Finalize the shared owner contract, then approve duplicate-rule-specific owner eligibility, init selection/capture, settings/schema layout, and empty-set semantics; verify examples use GUID-only rule identity and shared logical-owner mappings.
- [ ] 1.2 Establish SDK-backed asynchronous publish completion, five-rule-per-base-table capacity handling, unpublish/publish ordering, ownership sequencing, cancellation, timeout, and retry rules; verify evidence and approved behavior, without treating immediate fake transitions as platform proof.
- [ ] 1.3 Refine schemas/settings and downstream tasks in the plan; verify no blocking gate remains and `pnpm exec openspec validate introduce-duplicaterules-resource --strict` passes.

## 2. Resource contract and capture

- [ ] 2.1 Add resource and test projects, references, and module-owned models; verify a focused build succeeds without a process-module dependency.
- [ ] 2.2 Implement approved settings, schemas, complete retrieval, and init capture; verify parsing, multi-page selection, GUID-only identity, same-name/different-ID rejection without fallback, and approved owner capture.
- [ ] 2.3 Document commands, configuration, and existing-rule-only scope in README; verify examples match schema fixtures.

## 3. Apply and reporting

- [ ] 3.1 Integrate the shared owner resolver and implement full preflight and no-write dry-run; verify missing rule GUIDs, unresolved owner keys, and resource-unsupported assignments prevent all batch writes.
- [ ] 3.2 Implement publication and ownership changes with approved completion handling; verify publish/unpublish, state-only and owner-only intent, omitted properties, idempotence, and no definition mutations.
- [ ] 3.3 Implement independent-rule continuation and failure aggregation; verify partial-effect reporting, cancellation, and failure exit behavior.
- [ ] 3.4 Document operational limits and partial-failure semantics; verify docs agree with the approved contract.

## 4. Host integration

- [ ] 4.1 Register the resource and DI and prune only proven orphaned historical DTOs; verify CLI smoke/settings tests and all consumers of retained shared types.
- [ ] 4.2 Run focused resource/CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and reconcile the implemented main spec before archive.
