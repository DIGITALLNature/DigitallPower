# Project Summary

## Overview

**DigitallPower** — CLI tooling for Dataverse/Power Platform operations (export, import, push, maintenance, code generation, analysis, profiling).

## Architecture

- **Framework:** .NET 10, C# latest, nullable enabled, implicit usings
- **CLI Host:** `dgt.power` (entry point, Spectre.Console command tree)
- **Common Layer:** `dgt.power.common` (shared abstractions, extensions, fixtures, `ExecutionEnvironment`)
- **Modules:** `dgt.power.{analyzer, codegeneration, export, import, maintenance, profile, push}`
- **Models:** `dgt.power.dataverse` (generated Dataverse entity wrappers), `dgt.power.dto` (cross-module DTOs)
- **Tests:** TUnit framework, one test project per module in `tests/`
- **Static Analysis:** `Microsoft.Extensions.StaticAnalysis` + Qodana (`jetbrains/qodana-cdnet:2026.1-eap`)
- **Baseline:** `baseline.sarif.json` (693 accepted findings — style, code health, known false positives)

## Module Overview

```
src/
├── dgt.power/                  CLI host, telemetry, profile commands
├── dgt.power.common/           IConnector, IXrmConnection, PowerLogic<T>, ExecutionEnvironment
├── dgt.power.dataverse/        Generated entity classes (DataContext, Solution, Workflow, etc.)
├── dgt.power.dto/              Shared DTOs for config/export/import shapes
└── modules/
    ├── dgt.power.analyzer/     Solution layer analysis (redundant patches, active layers)
    ├── dgt.power.codegeneration/  .NET + TypeScript code generation (Liquid/TSL templates)
    ├── dgt.power.connection/   Connection management + auth lifecycle (canonical)
    ├── dgt.power.export/       Entity data export (calendar, templates, bulk deletes, etc.)
    ├── dgt.power.import/       Entity data import with conflict resolution
    ├── dgt.power.maintenance/  Workflow state management, SDK step control, carrier info
    ├── dgt.power.profile/      Deprecated alias for dgt.power.connection (kept for BC)
    └── dgt.power.push/         Plugin assembly + webresource deployment
```

## Key Conventions

### Async Pattern
- All `IConnector`/`IXrmConnection` methods are async (return `Task`)
- All async methods carry `Async` suffix (S4261) — enforced by analyzers
- Tests are exempt from `Async` suffix via `tests/.editorconfig`
- Async methods with parameter validation use the split pattern: public method validates, calls private `*CoreAsync` implementation
- `PowerLogic<T>` has a single abstract entry point `InvokeAsync` — sync `Invoke` was removed
- Sync `PowerLogic<T>` commands bridge through `Task.FromResult(InvokeCore(args))` until module logic is fully async

### Naming
- Static fields: `s_` prefix (e.g. `s_parser`, `s_options`, `s_separators`)
- Standard .NET PascalCase for public members
- `IsCrmUiWorkflow` not `IsCrmUIWorkflow` (acronym rule: only first letter caps for 2+ char acronyms)
- Enum members: PascalCase (e.g. `Up2Date` not `Up2date`)

### Collection Contracts
- Public APIs expose `IReadOnlyList<T>` or `IReadOnlyCollection<T>`, not `List<T>` or arrays
- Exception: codegeneration `HashSet<string>` selection/filter collections (perf-intentional)
- DTOs mutated during export use `#pragma` suppression for CA2227

### Error Handling
- `CA1031` suppressed project-wide in `AssemblyProcessor` (Dataverse exception wrapping)
- All custom exceptions have standard constructor overloads (CA1032)
- Public methods validate args with `ArgumentNullException.ThrowIfNull`

## Key Decisions

| Decision | File | Summary |
|----------|------|---------|
| V2 code generation config | `decision-config-v2-redesign.md` | Typed config hierarchy; worker layer eliminated; strategy pattern for TS Full/Light; symmetrical IDotNetGenerator/ITypeScriptGenerator API |
| Async suffix enforcement | `decision-async-suffix-s4261.md` | S4261 enforced in src, exempted in tests |
| Remove --insecure/--security-protocol | `decision-remove-insecure-protocol.md` | SYSLIB0014; ServicePointManager is no-op on .NET 8+ |
| Package as record class | `decision-package-record-refactor.md` | init-only props, equality scoped to Name+Version+Content |
| Post-TSL architecture priorities | `decision-post-tsl-architecture-wave.md` | VSTHRD200/002, S1067/S3358, debt-baseline for S1135/S125 |
| Remove sync Invoke from PowerLogic | `decision-remove-sync-invoke.md` | InvokeAsync is now the single abstract entry point; Task.FromResult interim pattern |
| Non-interactive auth for coding agents | `decision-non-interactive-auth-for-agents.md` | `--non-interactive`/`DGTP_NON_INTERACTIVE`, exit code 2, `dgtp connection status` + `dgtp connection refresh`; `profile` is deprecated alias |
| Error telemetry anonymization | `decision-error-telemetry-anonymization.md` | Automated crash reporting recorded as OTel exception events; GUID/home-path/org-URL redaction for privacy |
| Generic command deprecation | `decision-generic-command-deprecation.md` | `[DeprecatedCommand]` attribute on `CommandSettings` + single `DeprecationInterceptor`, replacing fragile argv-position detection |
| Persist-after-verify for connection commands | `guide-persist-after-verify-connection-commands.md` | `CreateConnectionCommand`/`CreateProfileCommand` now `Save()` only after a successful connectivity check, not before |

## TSL Template Engine (codegeneration)

The TypeScript/Liquid (TSL) template engine has enterprise-grade hardening:
- **Thread safety:** Liquid filter state isolated per render (`TslRenderDiagnostics`)
- **Centralized options:** `TslTemplateOptionsFactory` provides canonical `TemplateOptions` profile
- **Compile gates:** TSL templates validated at build-time via TypeScript 6 compiler
- **CI mode:** `ExecutionEnvironment.IsCi` controls strict validation fallback
- **Env controls:** `DGT_POWER_TSL_STRICT_MODE`, `DGT_POWER_TSL_MAX_STEPS`

### Codegeneration Config Resolution

- **`CodeGenerationConfigFactory`** is the single entry point for config loading
- Routes by `version`: missing/1 → V1 (deprecation warning), 2 → V2 (requires `type`), other → throw
- Returns `CodeGenerationConfigResult` (discriminated union: `V1(CodeGenerationConfig)` | `V2(CodeGenerationConfigBase)`)
- V2 shared root now uses `namespace`, `language`, `entities { names, fromSolutions, mask }`, `requests`, `optionSets`
- TypeScript-specific settings live under `output.forms` / `output.customApis`; .NET-specific settings live under `output.target` / `output.virtual` / `output.editableReadOnly` / `output.include`
- V1 configs preserved as-is (can generate both .NET + TypeScript in one run)
- Manual discriminator routing via `JsonDocument` (STJ polymorphic attributes removed — incompatible with `$schema` in config files)

### Codegeneration Generator Architecture

- **`CodeGenerationCommand`** injects `IDotNetGenerator` + `ITypeScriptGenerator` (two interfaces, symmetrical)
- Pattern matches on `CodeGenerationConfigResult` — no silent catch, no dual code paths
- **`MetadataService`** is split across `MetadataService.cs` (shared + V2 API) and `MetadataService.Legacy.cs` (V1 `CodeGenerationConfig` overloads) so legacy support does not re-trigger S104/S4136 in the main file
- **`TypeScriptGenerator`** is a pure router using the strategy pattern:
  - `ITypescriptGenerationStrategy` (internal) — single `Generate()` method
  - `TypescriptFullGenerationStrategy` — V1 Full mode (deprecated)
  - `TypescriptLightGenerationStrategy` — V1 Light + V2
  - `TypescriptGenerationStrategyBase` — shared file I/O (CreateFile, PrepareDirectory)
- All generation step methods are private; interfaces expose only `Generate()`
- Strategy classes live in `Generators/Strategy/`; contracts in `Generators/Contracts/`

## Qodana Baseline (693 findings)

| Category | Count | Disposition |
|----------|-------|-------------|
| Code health (UnusedMember, ClassNeverInstantiated, etc.) | ~200 | DI-registered services → false positives; rest is tech debt |
| Style (ArrangeObjectCreation, UseCollectionExpression, etc.) | ~65 | Cosmetic, no risk |
| Naming (InconsistentNaming, CheckNamespace) | ~45 | Generated code / CRM schema names that can't change |
| Nullability (CS8604, CS8602) | ~10 | Validated-but-not-tracked-across-methods; guarded at runtime |
| Other (S103 line length, S1135 TODOs, CA1716 keyword) | ~40 | Accepted trade-offs |
| Test code | ~330 | Test infrastructure, not production risk |

## Completion Subsystem (`src/dgt.power/Completion/` + `Commands/Complete/`)

`dgtp` supports dotnet-suggest shell tab completion. Two separate subsystems:

### 1. Suggest gate (`Completion/`)
- `SuggestDirective` — parses `[suggest:N]` CLI directive
- `DotnetSuggestHandler` — early exit in `Program.cs` before telemetry/NuGet/Dataverse; captures model and streams candidates to stdout
- `ModelCaptureHelpProvider` — `IHelpProvider` that captures `ICommandModel` from Spectre via `--help` invocation on a minimal `CommandApp`
- `CompletionEngine` — token-walking algorithm; returns command names / `--option` flags

### 2. Setup commands (`Commands/Complete/`)
- `CompleteSetupCommand` — registers dgtp with `dotnet-suggest register`; `--all` flag also runs shim install
- `CompleteInstallShellCommand` — writes dotnet-suggest shim into shell RC file with idempotency markers
- `ShellDetector` — auto-detects from `$SHELL` env var; maps to bash/zsh/fish/pwsh
- `ShellShimInstaller` — virtual methods for testability; idempotency via `# >>> dgtp tab completion start >>>` markers; creates parent dirs

### Key caveats
- `DotnetSuggestHandler` must run as the FIRST statement in `Program.cs`, before any I/O, telemetry or network calls
- `AnsiConsoleOutput(TextWriter)` constructor — no static `.Create()` method
- `IHelpProvider.Write(model, null)` receives `ICommandModel` (not `ICommandInfo`)
- `ICommand<T>.ExecuteAsync(context, settings, ct)` is an explicit interface impl — tests must cast via `(ICommand<T>)command`



- **`--insecure` / `--security-protocol`** removed as breaking changes. Existing profile JSON still deserializes (nullable + `JsonIgnoreCondition.WhenWritingDefault`).
- **`FormXmlControlData.ControlId`** uses `{ get; set; }` in `GetHashCode()` — suppressed. Candidate for `record class`.
- **`Assembly` model** (push module) has mutable-GetHashCode pattern. Not yet refactored.
- **Schema URLs in README point to the `beta` branch** — must be updated to `main` before merging to main. Search README for `raw.githubusercontent.com/.*/beta/` and replace with `.*/main/`.
- **TSL `Light` runtime guardrails** depend on env-driven validation; invalid max-step overrides fail fast.
- **CA1716** (`dgt.power.export` namespace conflicts with `export` keyword) — accepted; renaming would be a massive breaking change.

## Memory Files Index

| File | Type | Content |
|------|------|---------|
| `decision-config-v2-redesign.md` | decision | V2 CodeGenerationConfig: typed hierarchy, Requests unification, strategy pattern, generator architecture |
| `decision-async-suffix-s4261.md` | decision | Async suffix convention; test exemption rationale |
| `decision-remove-insecure-protocol.md` | decision | Why CLI options removed; backward-compat handling |
| `decision-package-record-refactor.md` | decision | Package record class design; equality semantics |
| `decision-post-tsl-architecture-wave.md` | decision | Priority order for remaining quality findings |
| `decision-non-interactive-auth-for-agents.md` | decision | Non-interactive auth: exit code 2, `DGTP_NON_INTERACTIVE`, `dgtp profile auth-check` |
| `guide-static-analysis-cleanup.md` | guide | Systematic approach for CA/Sonar cleanup |
| `guide-sonar-rules-applied.md` | guide | Fix patterns for S3902, S3971, S2930, S3900, S4261 |
| `guide-code-quality-patterns.md` | guide | Anti-patterns with canonical fixes (DI downcasts, GetHashCode, covariant arrays) |
| `implementation-centralized-ci-environment-detection.md` | implementation | ExecutionEnvironment in common; reused by telemetry + codegen |
| `implementation-tsl-p1-p2-completion.md` | implementation | TSL hardening: diagnostics, options factory, compile gates, test suites |
| `implementation-registration-attributes.md` | implementation | Push module: all evaluated registration attributes, behavior, and limitations |
| `implementation-assembly-version-upgrade-migration.md` | implementation | Push module: migrate Steps/CustomAPIs on assembly major/minor version upgrade |
| `implementation-codegeneration-metadata-service-legacy-split.md` | implementation | Codegeneration metadata service split: keep shared/V2 code in main file and move V1 overloads into a legacy partial |
| `guide-webresource-solution-lazy-add.md` | guide | Push module: only add webresource to solution when not already a member; single pre-fetch for both upsert + obsolete checks |
| `research-servicepointmanager-dotnet8.md` | research | ServicePointManager no-op; Dataverse.Client has no HttpClient hook |
| `research-tsl-fluid-hardening.md` | research | Fluid.Core stability assessment and hardening strategy |
| `research-v2-typescript-config-design-gaps.md` | research | Current V2 TS caveats after redesign: no `TypingPath`, no per-entity filters, string-based `forms.filter` |
| `implementation-v2-codegeneration-config-shape.md` | implementation | Final nested V2 config shape: shared `entities` scope, `optionSets`, and target-specific `output` blocks |
| `implementation-v2-schema-allows-dollar-schema.md` | implementation | V2 schemas now allow top-level `$schema` for editor compatibility under `additionalProperties: false` |
| `decision-error-telemetry-anonymization.md` | decision | Crash reporting via OTel exception events; anonymization scope (GUIDs, home-dir paths, org/tenant URLs) and known limitations |
| `decision-generic-command-deprecation.md` | decision | `[DeprecatedCommand]` attribute + `DeprecationInterceptor`: how to deprecate any command/branch, and why argv-position detection was replaced |
| `guide-persist-after-verify-connection-commands.md` | guide | `CreateConnectionCommand`/`CreateProfileCommand`: why `Save()` must run after connectivity check, not before; test pattern with Transient `IProfileManager` |
