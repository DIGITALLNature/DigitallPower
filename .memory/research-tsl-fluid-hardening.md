# TSL/Fluid.Core Hardening Assessment

## Scope

Review of the TypeScript Light (`tsl`) code-generation path and Fluid.Core integration:

- `TypescriptGeneratorWorkerLight`
- `CustomLiquidFilters`
- `LiquidTemplateEngine`
- `Templates/tsl/*.liquid`
- `TypescriptWorker` test coverage

## Current risk profile

1. **High: default production path is under-tested**
   - `CodeGenerationConfig.TypescriptGeneratorVersion` defaults to `Light`.
   - `tests/dgt.power.codegeneration.tests/TypescriptWorkerTests.cs` only tests `Full`.
   - `TypescriptWorkerLightTests.cs` exists but is fully commented out.

2. **High: shared mutable global state inside filters**
   - `CustomLiquidFilters` uses static mutable fields:
     - `s_usedTokens` (`Dictionary<int, Dictionary<string, List<string>>>`)
     - `s_console`
   - Risks:
     - race conditions under concurrent renders
     - memory growth (context hash entries never cleaned)
     - non-deterministic output if hash collisions happen

3. **High: latent template typing defect not covered by compilation tests**
   - `Templates/tsl/EntityForm.liquid` has `get(name: {{ TabName }}{{ SectionName }}ControlName): T | null;` where `T` is not declared for that overload.
   - This is exactly the kind of defect strict rendering/snapshot/type-check gates should catch automatically.

4. **Medium: parser/resource failures are not surfaced with useful diagnostics**
   - `LiquidTemplateEngine.LoadTemplate` uses `GetManifestResourceStream(... )!` and `Parser.Parse(...)` directly.
   - Missing resources or malformed templates fail late and without template-specific remediation guidance.

5. **Medium: Fluid strictness features are not used**
   - `StrictVariables` and `StrictFilters` are not enabled.
   - Missing variables/filters can silently degrade generated output quality.

## Fluid.Core best-practice alignment (community guidance)

From Fluid README guidance:

- Share `FluidParser` and cache `IFluidTemplate` (already aligned via static parser + cache).
- Reuse `TemplateOptions` and create new `TemplateContext` per render (partially aligned; context is recreated, options are per worker instance).
- Use strict modes (`StrictVariables`, `StrictFilters`) for fail-fast authoring.
- Configure execution limits (`MaxSteps`) to prevent runaway template execution.
- Use allow-listed member access via `MemberAccessStrategy.Register<T>()` (already used and should remain explicit).

## Hardening actions (priority order)

### P0 (must do before calling TSL enterprise-ready)

1. **Re-activate and modernize Light test suite**
   - Un-comment and migrate `TypescriptWorkerLightTests.cs` to current TUnit style.
   - Keep deterministic “golden file” comparison for generated artifacts.

2. **Add a template compilation gate**
   - Parse all embedded `Templates/tsl/*.liquid` with `TryParse`.
   - Fail with template name + parser error details.

3. **Add generated TypeScript compile gate**
   - Generate light output for canonical scenarios and run `tsc --noEmit`.
   - This catches template typing defects and declaration regressions immediately.

4. **Remove static mutable filter state**
   - Replace `s_usedTokens`/`s_console` pattern with per-render scoped state (context-bound or explicit state passed via model).

### P1 (stability/resilience hardening)

1. **Enable strict Fluid behavior in tests/CI**
   - `StrictVariables = true`, `StrictFilters = true` in validation mode.
   - Keep a controlled runtime mode if backward compatibility demands it.

2. **Add render-failure diagnostics wrapper**
   - Wrap render calls to include template name, entity/form context, and filter path in exception messages.

3. **Set execution guardrails**
   - Configure `TemplateOptions.MaxSteps` to a sane upper bound for generation workloads.

4. **Make options/profile registration immutable and startup-driven**
   - Build one dedicated `TemplateOptions` profile for tsl (filter + member registrations) and treat it as read-only during rendering.

### P2 (enterprise maintainability)

1. **Property-based fuzz tests for naming/sanitization**
   - Randomized schema/control/section names, special chars, localization edge cases.

2. **Concurrency stress tests**
   - Parallel rendering with shared templates/options and isolated contexts.
   - Assert deterministic output.

3. **Template contract tests**
   - For each view model, assert required properties used in templates are present and non-null under expected scenarios.

## Testing target model

Minimum target for “full coverage” in this component:

- **Unit tests**
  - every custom filter branch and error branch
  - `LiquidTemplateEngine` parse/cache/error behavior
  - `FormParser` malformed XML and missing node behavior
- **Golden/snapshot tests**
  - all tsl templates rendered with stable fixtures
- **Integration tests**
  - generated declarations pass TypeScript compiler
- **Stress tests**
  - parallel runs deterministic and leak-free

## Progress update

- **P0.1 completed**: `TypescriptWorkerLightTests` was reactivated in TUnit style and now validates TSL outputs with dedicated scenario resources under `Resources/TypescriptWorkerTsl/TestScenarios`.
- The Light entity scenario asserts the real output topology (`TypeScript/Entities/<EntityName>/{Entity,Forms}`) instead of assuming all `.d.ts` files are emitted in the root `TypeScript` folder.
- **P0.2 completed**: `LiquidTemplateEngine.LoadTemplate` now validates embedded templates via `FluidParser.TryParse(...)` and throws fail-fast diagnostics including full template resource name + parse error details.
- **P0.3 completed**: `TslTypeScriptCompileGateTests` generates a dedicated Light scenario and runs `tsc --noEmit` against generated declaration files; CI build/check workflows were extended with Node/pnpm dependency installation so the gate is enforced in automation.
- `SdkMessages.liquid` was corrected from `Assign = Assign` to quoted string enum values (`Assign = "Assign"`), and the TSL sdk message snapshot baseline was updated accordingly.
- **P0.4 completed**: `CustomLiquidFilters` no longer uses process-wide static mutable state for `unique` tokens or console output. State is now stored in `TemplateContext.AmbientValues` (per render context), and `TypescriptGeneratorWorkerLight` configures each render context explicitly.
- Added focused regression tests (`CustomLiquidFiltersTests`) to assert `unique` behavior is scoped per render context and does not leak across sequential renders.
- **P1.1 completed (Fluid 2.31-compatible)**: strict validation mode is now active for CI/tests (`CI=true`) and can be forced locally with `DGT_POWER_TSL_STRICT_MODE=true`; `TypescriptGeneratorWorkerLight` enables fail-fast undefined handling via `TemplateOptions.Undefined`.
- Added integration coverage (`TslStrictFluidModeTests`) to verify the strict-mode code path on a real TSL generation scenario.
