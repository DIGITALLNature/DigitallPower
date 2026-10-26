# Tasks

The product contract is approved for planning. Platform evidence in section 1 blocks dependent implementation; no live writes, publish operations, or managed imports are authorized by this plan. If evidence contradicts the contract, stop for design review rather than silently changing scope or guarantees.

## 1. Platform evidence gates

- [ ] 1.1 Obtain separate approval for a bounded disposable test setup and verify minimal metadata updates for Calculated, Rollup, and Power Fx Formula columns; verify read-back false, unchanged formulas/unrelated settings, and already-protected behavior, then record concise supported payload conclusions in design.
- [ ] 1.2 Establish publication and managed-deployment behavior after 1.1; verify exported protection, imported whole-column restrictions, and original-publisher servicing in approved test environments, recording whether narrow publication is required; stop for design review if broader writes are necessary.
- [ ] 1.3 Establish reliable complete-table eligibility and technical-companion identification, including rollup companions, basis-currency fields, and historic `bpf_duration`; verify real metadata examples, document exception rationale, and add accurate fixtures without speculative name heuristics.

## 2. Solution-owned preparation and scope

- [ ] 2.1 Add solution-owned protection planning after the final copy/app selection without a legacy-maintenance dependency; verify all three source types, explicitly selected standalone attributes without parent membership, complete-table expansion, and deduplication across sources in focused TUnit tests.
- [ ] 2.2 Implement bounded exclusions using evidence from 1.3; verify managed attributes even with active layers, simple/prompt fields, skeleton-only expansion, skipped app components, unrelated target membership, and automatic app/dependency additions never acquire protection; verify every in-scope technical exception has a reason.
- [ ] 2.3 Implement full preflight classification before any copy/app-cleanup/protection mutation; verify known null source type is non-calculation, already false is unchanged even when immutable, unprotected immutable candidates fail, unknown required metadata and retrieval failures cause zero writes, and a later invalid candidate prevents all earlier effects.
- [ ] 2.4 Render field identities, calculation types, changes/unchanged/exceptions, and shared-environment consequences; verify best-practice dry-run uses identical preparation and fails consistently with zero writes, while raw and raw dry-run make no protection-specific queries or updates.

## 3. Execution and failure guarantees

- [ ] 3.1 After all section 1 gates pass, implement minimal sequential protection updates and per-field read-back only after successful copy/app cleanup; verify update/read ordering for all three types, no formula/unrelated-property changes, no protection after copy/cleanup failure, and no protection updates on an unchanged rerun.
- [ ] 3.2 Implement fail-fast and cancellation reporting across copy and protection phases; verify a second update fault, failed read-back, read-back mismatch, and cancellation stop later client mutations, fail the run, and distinguish copied, confirmed protected, failed, and unconfirmed effects without rollback claims or mutation retries.
- [ ] 3.3 Implement only publication behavior established by 1.2 and compatible with the approved contract; verify any required narrow operation and its failure reporting, no publication when unnecessary, and zero publish writes in dry-run; return broader requirements to design review.

## 4. Compatibility, documentation, and integration

- [ ] 4.1 Update existing `CopyComponentsSettings` help descriptions for default protection, raw bypass, and dry-run; verify CLI settings parsing defaults/combinations and structural/help tests while preserving source selection, table normalization, active-layer filtering, and all app modes.
- [ ] 4.2 Update README and `docs/migrations/2.x-to-3.x.md` with executable examples, all three types, bounded scope, exceptions, shared whole-column managed-deployment effects, permission/runtime limits, partial effects, and raw mode's broader selection changes; verify every example and claim against tests and platform evidence.
- [ ] 4.3 Run focused solution protection/copy and CLI tests, `dotnet build`, and `pnpm exec openspec validate protect-calculated-fields-on-solution-copy --strict`; verify no maintenance-module dependency and no regression in existing copy/app behavior, then run `pnpm exec openspec validate --all` and identify unrelated incomplete planning separately.

## Workflow follow-up

- Synchronize implemented requirements into main specs only after behavior and evidence agree; keep this change active while its implementation PR is open.
- Archive only as a separate follow-up after verifying this change's implementation was merged into exactly `beta` or `main`; a merge into `marcel/planing2026` is insufficient.
