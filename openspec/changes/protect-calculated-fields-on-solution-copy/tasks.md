# Tasks

The user approved implementation on 2026-10-26 with the evidence and limitations recorded in design, explicitly accepting editable labels/translations. Section 1 records confirmed observations and accepted test limitations, not unperformed tests. No further live experiments are required; cleanup of the disposable setup remains authorized.

## 1. Platform evidence gates

- [x] 1.1 Record approved disposable tests confirming minimal updates for all three calculation types, read-back false, preserved formulas/unrelated settings, and zero-update reruns.
- [x] 1.2 Record export/import without publish, editable labels/translations, same-environment test limits, and unexecuted original-publisher servicing; obtain explicit user acceptance of those limits before implementation.
- [x] 1.3 Add accurate fixtures for observed complete-table metadata, SourceType 0 rollup companions, and IsBaseCurrency flags; preserve historic `bpf_duration` as a documented compatibility exception without speculative name heuristics.

## 2. Solution-owned preparation and scope

- [x] 2.1 Add solution-owned protection planning after the final copy/app selection without a legacy-maintenance dependency; verify all three source types, explicitly selected standalone attributes without parent membership, complete-table expansion, and deduplication across sources in focused TUnit tests.
- [x] 2.2 Implement bounded exclusions using evidence from 1.3; verify managed attributes even with active layers, simple/prompt fields, skeleton-only expansion, skipped app components, unrelated target membership, and automatic app/dependency additions never acquire protection; verify every in-scope technical exception has a reason.
- [x] 2.3 Implement full preflight classification before any copy/app-cleanup/protection mutation; verify known null source type is non-calculation, already false is unchanged even when immutable, unprotected immutable candidates fail, unknown required metadata and retrieval failures cause zero writes, and a later invalid candidate prevents all earlier effects.
- [x] 2.4 Render field identities, calculation types, changes/unchanged/exceptions, and shared-environment consequences; verify best-practice dry-run uses identical preparation and fails consistently with zero writes, while raw and raw dry-run make no protection-specific queries or updates.

## 3. Execution and failure guarantees

- [x] 3.1 Using approved platform evidence, implement minimal sequential protection updates and per-field read-back only after successful copy/app cleanup; verify update/read ordering for all three types, no formula/unrelated-property changes, no protection after copy/cleanup failure, and no protection updates on an unchanged rerun.
- [x] 3.2 Implement fail-fast and cancellation reporting across copy and protection phases; verify a second update fault, failed read-back, read-back mismatch, and cancellation stop later client mutations, fail the run, and distinguish copied, confirmed protected, failed, and unconfirmed effects without rollback claims or mutation retries.
- [x] 3.3 Do not publish: verify zero publish requests during actual runs and dry-run, consistent with successful no-publish export/import evidence.

## 4. Compatibility, documentation, and integration

- [x] 4.1 Update existing `CopyComponentsSettings` help descriptions for default protection, raw bypass, and dry-run; verify CLI settings parsing defaults/combinations and structural/help tests while preserving source selection, table normalization, active-layer filtering, and all app modes.
- [x] 4.2 Update README and `docs/migrations/2.x-to-3.x.md` with executable examples, all three types, bounded scope, exceptions, shared column-level managed-property effects, editable labels/translations and accepted platform-test limits, permission/runtime limits, partial effects, and raw mode's broader selection changes; verify claims against tests and recorded evidence.
- [x] 4.3 Run focused solution protection/copy and CLI tests, `dotnet build`, and `pnpm exec openspec validate protect-calculated-fields-on-solution-copy --strict`; verify no maintenance-module dependency and no regression in existing copy/app behavior, then run `pnpm exec openspec validate --all` and identify unrelated incomplete planning separately.

## Workflow follow-up

- Synchronize implemented requirements into main specs only after behavior and evidence agree; keep this change active while its implementation PR is open.
- Archive only as a separate follow-up after verifying this change's implementation was merged into exactly `beta` or `main`; a merge into `marcel/planing2026` is insufficient.
