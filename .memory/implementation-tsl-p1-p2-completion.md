# TSL P1/P2 Completion Wave

## Scope

Completed all remaining TSL hardening items from P1 and P2:

- P1.3 execution limits
- P1.4 centralized immutable template-options profile
- P2.1 property/fuzz coverage for naming/sanitizing
- P2.2 parallel and serial determinism checks
- P2.3 template contract tests (positive + strict negative)

## P1.3 — Execution Limits

- Introduced `TslTemplateOptionsFactory` and set explicit Fluid execution limit:
  - `DefaultTemplateMaxSteps = 20_000`
  - env override: `DGT_POWER_TSL_MAX_STEPS`
- Invalid override values fail fast with explicit `InvalidOperationException`.

## P1.4 — TemplateOptions Profile Consolidation

- Moved all TSL `TemplateOptions` setup out of `TypescriptGeneratorWorkerLight` into `TslTemplateOptionsFactory`.
- Factory now owns:
  - strict-mode handling (`DGT_POWER_TSL_STRICT_MODE` / CI fallback),
  - max-step configuration,
  - core and TSL filter registration,
  - value converters and member-access registration.
- `TypescriptGeneratorWorkerLight` now consumes only `TslTemplateOptionsFactory.Create()`.

## P2.1 — Fuzz/Property Tests

- Added `TslNamingFuzzTests`:
  - deterministic fuzz seeds,
  - sanitize/camelcase determinism checks,
  - edge-case inputs (unicode/diacritics/long names),
  - collision coverage for `unique` filter.

## P2.2 — Parallel Determinism

- Added `TslDeterminismTests`:
  - parallel run equivalence (hash-comparison per artifact),
  - repeated serial run equivalence,
  - deterministic fixture input with fixed IDs.

## P2.3 — Template Contracts

- Added `TslTemplateContractTests`:
  - positive contract rendering for all TSL templates:
    - `Entity`, `EntityForm`, `EntityFormTestHelper`, `OptionSets`, `SdkMessages`, `CustomApi`
  - strict-mode negative contract cases for missing required fields with fail-fast behavior.

## Operational Notes

- README now documents `DGT_POWER_TSL_STRICT_MODE` and `DGT_POWER_TSL_MAX_STEPS`.
- `todo.md` marks P1 and P2 items as completed with implemented artifacts.
