# Tasks

Feature evaluation can start now. No pending feature deletion or destination is approved. Removal tasks depend on explicit decisions and completed retained-feature migrations.

## 1. Feature disposition gates

- [ ] 1.1 Review bulk-delete execution together with transfer functionality; verify both retirement designs record the same approved disposition, destination, and linked change.
- [ ] 1.2 Review auto-number formatting with the user; verify the matrix records retain/replace/remove, rationale, and any destination.
- [ ] 1.3 Review calculated-field protection with the user; verify the matrix records operational value, overlap, and approved disposition.
- [ ] 1.4 Review carrier-information export with the user; verify the matrix records supported use cases and approved disposition.
- [ ] 1.5 Review redundant-component removal and solution-operation overlap; verify approved retained behavior or removal and replacement are explicit.
- [ ] 1.6 Review PowerFx plugin filtering and plugin-resource fit; verify approved behavior and destination or removal are recorded.
- [ ] 1.7 Create separately reviewed proposals for retained migrations and refine this plan's deltas/tasks after all decisions; verify every matrix row is approved and every retained destination is linked.

## 2. Approved feature retirement

- [ ] 2.1 Verify `introduce-process-resource` and every approved retained-feature migration are implemented before deleting their old code; verify their replacement commands and documented behavior are present.
- [ ] 2.2 Implement only approved obsolete-feature removals, with focused rejection/regression tests per removal; verify no undecided capability is removed.
- [ ] 2.3 Document each approved removal/replacement and configuration consequence in README and `docs/migrations/2.x-to-3.x.md`; verify every matrix row has migration guidance or an explicit no-replacement statement.

## 3. Container removal and integration

- [ ] 3.1 Remove the maintenance branch, project references, DI, and proven orphaned schemas/models/helpers/tests; verify consumer searches show no required dependency was deleted.
- [ ] 3.2 Update command-tree smoke, completion, and old-branch rejection tests plus README project structure; verify all retained resource commands remain available.
- [ ] 3.3 Run affected resource/CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and merge the CLI delta without losing other changes' requirements before archive.
