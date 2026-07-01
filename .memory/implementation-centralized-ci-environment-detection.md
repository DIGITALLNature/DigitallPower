# Centralized CI Environment Detection

## Context

CI agent detection existed in multiple places with diverging logic:

- `TelemetryConfig.IsCi` checked a full CI variable set.
- `TypescriptGeneratorWorkerLight` checked only `CI` for strict-mode fallback.

This created inconsistency risk when introducing CI-dependent behavior in additional modules.

## Decision

Introduced a shared runtime helper in `dgt.power.common`:

- `ExecutionEnvironment.CiEnvironmentVariables`
- `ExecutionEnvironment.IsCiAgent`
- `ExecutionEnvironment.IsTruthy(string?)`

All CI detection now routes through `ExecutionEnvironment.IsCiAgent`.

## Applied Refactor

1. Added `src/dgt.power.common/ExecutionEnvironment.cs`.
2. Replaced CI checks in:
   - `src/dgt.power/Telemetry/TelemetryConfig.cs`
   - `src/modules/dgt.power.codegeneration/Generators/Worker/TypescriptGeneratorWorkerLight.cs`
3. Updated tests to rely on the centralized variable list:
   - `tests/dgt.power.telemetry.tests/TelemetryConfigTests.cs`
   - `tests/dgt.power.telemetry.tests/ExecutionEnvironmentTests.cs` (new)

## Notes

- `TypescriptGeneratorWorkerLight` still honors `DGT_POWER_TSL_STRICT_MODE` as explicit override.
- The CI fallback path now uses the same definition as telemetry.
- Naming intentionally avoids collision with `System.Runtime.InteropServices.RuntimeEnvironment`.
