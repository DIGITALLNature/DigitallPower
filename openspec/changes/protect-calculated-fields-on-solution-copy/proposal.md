# Proposal

## Why

Calculated, rollup, and Power Fx formula columns contain application logic that downstream customizations can alter. Protecting the columns intentionally included by `solution copy-components` makes this an explicit managed-deployment policy instead of preserving the environment-wide `maintenance protectfields` command.

## What Changes

- **BREAKING**: Best-practice mode sets `IsCustomizable.Value` to false for eligible unmanaged calculated, rollup, and Power Fx formula columns in the final copy plan. This changes shared environment metadata, not a target-solution-local copy.
- Include explicitly selected attributes and fields of completely copied tables; exclude unrelated target membership, app/dependency expansion, managed columns, prompt/simple columns, and technical companion fields.
- `--raw` bypasses protection-specific reads and writes; `--dry-run` prepares and reports the same protection plan without mutations.
- Prepare all metadata before any copy/protection write. Already protected columns require no update; unknown or unchangeable protection requirements fail preflight.
- Run protection only after all copy/app-cleanup work succeeds; update sequentially, read back each result, and stop on failure with explicit partial-effect reporting and no rollback promise.
- Use the approved disposable-platform evidence for all three types, minimal updates, technical exceptions, and managed export/import. The user accepted the same-environment roundtrip and remaining deployment-test limitations on 2026-10-26.
- Do not retain the standalone maintenance command or scan the whole environment.

## Capabilities

### New Capabilities

- `solution-copy-field-protection`: Plan-scoped protection of calculation-bearing columns during solution copying, including preview, validation, and partial-failure guarantees.

### Modified Capabilities

None at this stage.

## Impact

Affects the existing solution module's copy planning/execution, settings descriptions, resource and CLI tests, README, and `docs/migrations/2.x-to-3.x.md`. No new module, dependency, configuration file, or schema is planned. Protection means setting and verifying the column's `IsCustomizable.Value` managed property, not a formula-only lock or a guarantee that every column property is immutable. Labels/translations may remain editable. It is not field-level security and does not lock unmanaged development. `retire-legacy-maintenance` remains independent. The user approved implementation with the recorded evidence and limitations on 2026-10-26.
