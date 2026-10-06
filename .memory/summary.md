# Project Summary

## Overview

**DigitallPower** — CLI tooling for Dataverse/Power Platform operations (export, import, plugin and webresource deployment, maintenance, code generation, analysis, connection management).

## Architecture

- **Framework:** .NET 10, C# latest, nullable enabled, implicit usings
- **CLI Host:** `dgt.power` (entry point, Spectre.Console command tree)
- **Common Layer:** `dgt.power.common` (shared abstractions, extensions, fixtures, `ExecutionEnvironment`)
- **Modules:** `dgt.power.{analyzer, codegeneration, connection, export, import, maintenance, plugin, solution,
  webresource}`. Plugin and webresource deployment use independent resource-specific modules;
  the combined `push` module was removed in 3.x. Major-version migration guides are in
  [`docs/migrations/`](../docs/migrations/).
- **Models:** `dgt.power.dataverse` (generated Dataverse entity wrappers), `dgt.power.dto` (cross-module DTOs)
- **Tests:** TUnit framework, one test project per module in `tests/`
- **Static Analysis:** `Microsoft.Extensions.StaticAnalysis` + Qodana (`jetbrains/qodana-cdnet:2026.1-eap`)
- **Baseline:** `baseline.sarif.json` (693 accepted findings — style, code health, known false positives)

## Module Overview

```
src/
├── dgt.power/                  CLI host, telemetry, connection commands
├── dgt.power.common/           IConnector, IDataverseConnection, PowerLogic<T>, ExecutionEnvironment
├── dgt.power.dataverse/        Generated entity classes (DataContext, Solution, Workflow, etc.)
├── dgt.power.dto/              Shared DTOs for config/export/import shapes
└── modules/
    ├── dgt.power.analyzer/     Solution layer analysis (redundant patches, active layers)
    ├── dgt.power.codegeneration/  .NET + TypeScript code generation (Liquid/TSL templates)
    ├── dgt.power.connection/   Connection management + auth lifecycle (canonical)
    ├── dgt.power.export/       Entity data export (calendar, templates, bulk deletes, etc.)
    ├── dgt.power.import/       Entity data import with conflict resolution
    ├── dgt.power.maintenance/  Workflow state management, SDK step control, carrier info
    ├── dgt.power.plugin/       Resource-oriented `plugin push` + `plugin step config set`
    ├── dgt.power.solution/     `dgtp solution version|lint|copy-components` - single/multi-solution operations: version increment (formerly `maintenance solution-version`), configuration-driven Dataverse quality gates (formerly dgt.power.linter), and copying solution components between solutions with managed/active-layer-aware filtering, per-component execution progress, and `--apps skip|strip|allow` model-driven app handling
    └── dgt.power.webresource/  Dedicated webresource deployment
```

## Key Conventions

### Async Pattern
- All `IConnector`/`IDataverseConnection` methods are async (return `Task`)
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

### Plugin repository analyzer caveat
- Interface-typed locals constructed directly trigger CA1859, while concrete-typed locals can
  trigger Qodana's "only implementations are used" inspection. Configuration contracts use
  narrow `UnusedMemberInSuper.Global` suppressions pending constructor injection. See
  [plugin step configuration notes](implementation-plugin-step-config-set.md).
- Secure configuration creation/linking is transactional; the default test transaction fake
  does not emulate rollback, so atomic request construction is verified separately.
- Composite step resolution uses plugin `TypeName` and explicit linked-entity predicates;
  entity criteria filter after the left join, excluding unmatched steps.
- Package DLL selection uses `net48` for local metadata inspection, independently of the CLI
  runtime, and leaves package-specific deployment target validation to Dataverse; see
  [package framework selection](implementation-plugin-push-outdated-assembly-migration.md#package-framework-selection).

- Step-image updates include the parent step reference alongside attributes, preserving
  the image ID and leaving other registration fields untouched. This mitigates a platform-internal update fault;
  live confirmation is still required. See [image update payload notes](research-plugin-step-image-update-payload.md).

### CLI command tree registration
- `src/dgt.power/CommandTree.cs` (`CommandTree.Register`) is the single command-tree registration
  source used by both `Program.Configure` and `tests/dgt.power.cli.tests`. Register command changes
  there, and cover their paths/settings in the CLI tests.
- CI environment tests in `ExecutionEnvironmentTests` and `TelemetryConfigTests` share the
  `CiEnvironmentVariables` exclusion key to protect process-wide variable mutations while
  unrelated tests remain parallel. See `guide-cli-ci-environment-test-isolation.md`.

### Plugin custom data provider registrations
- `plugin push` reads the required `(dataSourceSchemaName, eventRegistration, providerName)`
  constructor arguments on every custom data-provider declaration and groups by data-source schema name.
  Required-value validation rejects old constructors, including in already-built assemblies, with
  a concise required-values error, without a constructor-count guard or an `Unspecified` event sentinel. It
  provisions/validates the specialized configuration table, and assigns handler GUIDs on
  `EntityDataProvider` instead of SDK steps. Package handlers are registered before provider writes;
  upgrades preserve references before cleanup. Provider/table schema solution membership is managed.
  Instance records and business virtual tables are outside deployment scope. See
  [custom data provider push behavior](research-plugin-custom-data-provider-push.md).
  New configuration tables use `<publisher-prefix>_name` for the primary-name column and its
  external mapping; existing primary-name columns are not renamed.
- First-time configuration-table provisioning needs live-Dataverse validation; request-payload
  tests do not establish platform acceptance. Ordinary tables cannot be converted into data-source
  tables. Provider reconciliation supports exactly the five PRT operations via a shared enum and
  explicit field mapping backed by the generated `EntityDataProvider.LogicalNames` constants,
  not suffix-based handler discovery. The provider repository uses the early-bound model.
  New providers leave undeclared handlers unset rather than assigning a hard-coded fallback plugin.
  Configuration-table backing provider IDs are resolved by the exact `JsonConverter` name;
  missing or ambiguous matches fail without GUID fallback.
  Public metadata exposes no dedicated data-source-table flag; the backing-provider comparison
  checks compatibility rather than an explicit table classification.
  The repository owns compatibility validation and private backing-provider resolution; the
  planner invokes `ValidateDataSourceAsync` before writes, including for new tables.
  Incompatible existing tables raise the domain-specific `InvalidDataSourceException`.
- Data-provider deployment is documented as experimental until provisioning and platform-owned
  step cleanup are validated live. Execution uses explicit phases sequenced by `PluginPushExecutor`;
  provider failure prevents obsolete type/assembly cleanup.
- Migration-only provider handlers resolve replacement plugin types by CLR type name. Planning
  rejects duplicate replacement names before writes; see
  [migration handler resolution](research-plugin-migration-handler-ambiguity.md).
- CLI-host and per-target plugin failures preserve contextual messages and Dataverse fault codes/
  inner-fault messages in all build configurations, without dumping trace text or arbitrary fault data.

### Generated model extensions
- Vanilla `RoutingRuleItem` generation omits Customer Service's `msdyn_routeto` and the virtual
  lookup discriminator `assignobjectidtype`. Keep the hand-written
  `Extensions/Dataverse/RoutingRuleItem.MsdynRouteto.cs` partial: routing import/export still uses it.
  Its setters follow generated `SetAttributeValue` behavior without property-notification hooks.

### Dependency ownership
- Direct package references declare API usage; repeated references do not duplicate
  runtime packages. Common security overrides flow through project references,
  but private build-tool packages do not.
- Module test dependencies can upgrade runtime packages relative to the CLI.
  Check resolved graphs, not just manifest versions, when consolidating references.
  Dependency policy and alignment should be reviewed as a dedicated solution-wide
  change rather than piecemeal feature cleanup.
  See [dependency ownership notes](research-dependency-ownership.md).

## Key Decisions

| Decision | File | Summary |
|----------|------|---------|
| V2 code generation config | `decision-config-v2-redesign.md` | Typed config hierarchy; worker layer eliminated; strategy pattern for TS Full/Light; symmetrical IDotNetGenerator/ITypeScriptGenerator API |
| Async suffix enforcement | `decision-async-suffix-s4261.md` | S4261 enforced in src, exempted in tests |
| Remove --insecure/--security-protocol | `decision-remove-insecure-protocol.md` | SYSLIB0014; ServicePointManager is no-op on .NET 8+ |
| Package as record class | `decision-package-record-refactor.md` | init-only props, equality scoped to Name+Version+Content |
| Post-TSL architecture priorities | `decision-post-tsl-architecture-wave.md` | VSTHRD200/002, S1067/S3358, debt-baseline for S1135/S125 |
| Remove sync Invoke from PowerLogic | `decision-remove-sync-invoke.md` | InvokeAsync is now the single abstract entry point; Task.FromResult interim pattern |
| Version-stable connection and state storage | `implementation-typed-connection-storage.md` | Typed `connections.json`, OS-protected secrets/token cache, stable `DGTP_HOME`, certificate key/password boundaries, token-check semantics, account-scoped cleanup, and no migration from 2.x |
| Keep certificate authentication | `decision-keep-certificate-auth.md` | Retain certificate auth as an available option; document that automated coverage is not end-to-end auth validation |
| Persistent MSAL cache account removal | `research-persistent-msal-token-cache-removal.md` | Match Azure.Identity's actual `.nocae` cache name, platform storage settings, protected-first/fallback behavior, and remove accounts individually |
| Non-interactive auth for coding agents | `decision-non-interactive-auth-for-agents.md` | `--non-interactive`/`DGTP_NON_INTERACTIVE`, exit code 2, `dgtp connection status` + `dgtp connection refresh` |
| Error telemetry anonymization | `decision-error-telemetry-anonymization.md` | Automated crash reporting recorded as OTel exception events; GUID/home-path/org-URL redaction and single-owner provider lifecycle |
| Runtime error diagnostics | `decision-runtime-error-diagnostics.md` | CLI-host/plugin failures show contextual messages and Dataverse fault codes in all builds; stack traces and arbitrary fault payloads are omitted |
| Generic command deprecation | `decision-generic-command-deprecation.md` | `[DeprecatedCommand]` attribute on `CommandSettings` + single `DeprecationInterceptor`, replacing fragile argv-position detection |
| Persist-after-verify for connection commands | `guide-persist-after-verify-connection-commands.md` | `CreateConnectionCommand` stages secrets, verifies the candidate connection, then persists metadata with rollback on failure |
| Major-version migration guides | `decision-major-version-migration-guides.md` | Document user-affecting breaks in `docs/migrations/<from>-to-<to>.md`; the 2.x-to-3.x guide covers profile removal, typed connection recreation, configuration replacements, and deployment changes |
| Resource-oriented CLI redesign (`plugin push`) | `decision-resource-oriented-cli-redesign.md` | Independent `dgtp plugin push` and `dgtp webresource push` modules replaced the combined 2.x `push`; module-local repos/executors are constructed via `new`, not registered in global DI |
| `dgt.power.plugin` namespace layout | `implementation-plugin-push-outdated-assembly-migration.md` | `Local` and `Remote` state / `Planning.Comparison` state models / `Planning.Deployment` executable plan models / `Repositories` / `Execution` / `Output` / `Commands` / `Base` |
| Plugin deployment plan pipeline | `decision-plugin-deployment-plan-pipeline.md` | `PluginDeploymentPlanner` creates one typed, validated plan; `PluginPlanRenderer` visualizes it; `PluginPushExecutor` applies it without repeating reconciliation decisions |
| Plugin plan output and progress | `research-plugin-plan-output-adaptation.md` | Solution membership is rendered below the plan; execution reports each completed operation, including partial progress |
| Plugin package content idempotence | `research-plugin-package-content-idempotence.md` | Existing plugin packages compare SHA-256 hashes of Dataverse `package` file-column bytes and local package bytes; version differences alone are unchanged |
| Plugin registration v3 cutoff | `decision-plugin-registration-v3-cutoff.md` | `plugin push` requires `Digitall.Plugins.Registration` 3.0.0+ for the three-argument provider contract; historical namespaces require dgtp 2.x or migration |
| Webresource publish strategy | `research-webresource-push-v2-review.md` | Publish each changed resource separately after all individual creates/updates and membership changes; defer publish batching until deployment writes can be batched coherently |
| TSL Jest test harness | `decision-tsl-jest-test-harness.md` | Generated fixtures from .NET + dedicated Jest project invoked by `pnpm test` in CI (Option A) |
| Azure DevOps Workload Identity Federation connections | `decision-azure-devops-workload-identity-federation.md` | Azure Pipelines WIF via `Azure.Identity.AzurePipelinesCredential`; current code persists a typed connection definition |
| Resource-oriented CLI restructuring | `decision-resource-oriented-cli-restructuring.md` | `dgtp <resource> <verb> <target>` shape (mirrors colleague's `dgt.power.plugin`); `dgt.power.solution` is the current module name for the ported linter surface; `analyze`/`maintenance` remain out of scope for this Phase 1; single-solution positional arg replaces `--solutions` list; Sarif.Sdk replaces hand-rolled SARIF POCOs |
| ILintRule default severity contract | `decision-ilint-rule-default-severity-contract.md` | Retain `ILintRule.DefaultSeverity` as public metadata exposed through `LintRuleCatalog.All`; use a narrow Qodana suppression rather than breaking the interface |

## TSL Template Engine (codegeneration)

The TypeScript/Liquid (TSL) template engine has enterprise-grade hardening:
- **Thread safety:** Liquid filter state isolated per render (`TslRenderDiagnostics`)
- **Centralized options:** `TslTemplateOptionsFactory` provides canonical `TemplateOptions` profile
- **Compile gates:** TSL templates validated at build-time via TypeScript 6 compiler
- **CI mode:** `ExecutionEnvironment.IsCi` controls strict validation fallback
- **Env controls:** `DGTP_TSL_STRICT_MODE`, `DGTP_TSL_MAX_STEPS`

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

- Reflection-bound test fixtures intentionally preserve their external namespace and public
  accessors; narrowly suppress the corresponding unused-member/namespace inspections. See
  [Qodana test-fixture guidance](guide-qodana-telemetry-and-doc-analyzer-fixes.md).

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



- **`--insecure` / `--security-protocol`** removed as breaking changes; legacy profile storage is not imported by typed connections.
- **`FormXmlControlData.ControlId`** uses `{ get; set; }` in `GetHashCode()` — suppressed. Candidate for `record class`.
- **Schema URLs in README point to the `beta` branch** — must be updated to `main` before merging to main. Search README for `raw.githubusercontent.com/.*/beta/` and replace with `.*/main/`.
- **TSL `Light` runtime guardrails** depend on env-driven validation; invalid max-step overrides fail fast.
- **CA1716** (`dgt.power.export` namespace conflicts with `export` keyword) — accepted; renaming would be a massive breaking change.
- **Webresource push plan contract:** the command should render a complete, case-insensitive plan
  before execution. Solution membership and obsolete deletion are plan operations, each changed
  resource is published separately after individual writes, and an unchanged managed resource is a
  valid no-op. An empty directory exits as a no-op even with `--delete-obsolete`; it cannot be used
  to delete every webresource from a solution. Mapping files may selectively rename supported files;
  other files use publisher-prefix-derived names. The file must contain a non-null `mappings`
  object and every mapping key must match a supported file in the target; invalid or unmatched
  mappings fail before deployment. Public plan records use one correspondingly named file each.
  Logical names are normalized to `/` before deriving webresource display names, regardless of
  whether an explicit or mapped name uses Windows separators.
  Command-level dry-run coverage plans create, update, membership, publish, and obsolete-delete
  operations, then verifies no writes occur. Execution uses one overall spinner and reports each
  successful operation through a progress callback, matching plugin push; see
  [`research-webresource-push-v2-review.md`](research-webresource-push-v2-review.md).
- **Console encoding:** the host sets UTF-8 output only after the dotnet-suggest early-exit gate,
  preserving its stdout-only completion protocol while enabling Spectre.Console plan emojis.
- **`SolutionComponent.ComponentType`/`RootComponentBehavior` are `OptionSetValue?` (a nullable-annotated reference type), not `int?`.** `.HasValue` does not compile on them, and `component.ComponentType is not { } type` binds `type` as `OptionSetValue`, not the underlying int — always chain through `.Value` first (`component.ComponentType?.Value is not { } type`). See `implementation-solution-copy-components.md` for the full write-up; this applies to any future code touching `solutioncomponent` rows.
- **`solution copy-components` raw mode preserves only complete vs. non-complete table behavior** (`IncludeAsShellOnly` maps to non-complete); managed-state and active-layer `IN` queries batch 500 IDs and page each batch before classification. See `implementation-solution-copy-components.md`.
- **`AddSolutionComponentRequest.DoNotIncludeSubcomponents = true` is only accepted for Entity roots (type 1)**; model-driven apps (type 80) cannot be added without Dataverse's expansion. `solution copy-components --apps skip|strip|allow` (default `skip`) controls this (app-bound AppSetting/AppModuleComponent rows follow the app, matched by `solutioncomponentdefinition` name - componenttypes >10000 differ per environment); `strip` removes platform-added subcomponents afterwards and is unverified against a real environment. See `implementation-solution-copy-components.md`.
- Connections, telemetry identity and version-check state now use stable per-user storage rather
  than assembly-scoped isolated storage. The redesign intentionally does not import data from 2.x;
  users recreate named connections. See `implementation-typed-connection-storage.md` and
  `research-isolated-storage-major-version-scoping.md`.

## Memory Files Index

| File | Type | Content |
|------|------|---------|
| `research-dependency-ownership.md` | research | Direct versus transitive dependencies, private build tools, test/runtime version differences, and cleanup boundaries |
| `decision-config-v2-redesign.md` | decision | V2 CodeGenerationConfig: typed hierarchy, Requests unification, strategy pattern, generator architecture |
| `decision-async-suffix-s4261.md` | decision | Async suffix convention; test exemption rationale |
| `decision-remove-insecure-protocol.md` | decision | Why CLI options removed; backward-compat handling |
| `decision-package-record-refactor.md` | decision | Historical dgtp 2.x push-module package record design; equality semantics |
| `decision-post-tsl-architecture-wave.md` | decision | Priority order for remaining quality findings |
| `implementation-typed-connection-storage.md` | implementation | Typed connection definitions, stable home/state files, storage boundaries and design rationale, global connection override variables, no legacy migration, verification limitations |
| `decision-keep-certificate-auth.md` | decision | Keep certificate-based authentication available; preserve the end-to-end validation caveat |
| `guide-cli-ci-environment-test-isolation.md` | guide | Shared TUnit exclusion key for process-wide CI environment variables and race diagnosis |
| `decision-non-interactive-auth-for-agents.md` | decision | Non-interactive auth: exit code 2, `DGTP_NON_INTERACTIVE`, `dgtp connection status` and `refresh` |
| `guide-static-analysis-cleanup.md` | guide | Systematic approach for CA/Sonar cleanup |
| `guide-sonar-rules-applied.md` | guide | Fix patterns for S3902, S3971, S2930, S3900, S4261 |
| `guide-code-quality-patterns.md` | guide | Anti-patterns with canonical fixes (DI downcasts, GetHashCode, covariant arrays) |
| `guide-connection-command-test-pattern.md` | guide | Connection command tests: temporary `DgtpHome`, file-backed `ConnectionStore`, fake secret store, and fake auth connections |
| `guide-qodana-telemetry-and-doc-analyzer-fixes.md` | guide | Patterns for analyzer-safe telemetry provider disposal, XML docs for inaccessible types, regex naming cleanup, and test-hygiene warnings |
| `guide-review-feedback-triage.md` | guide | Assess automated review comments against current HEAD and trace plugin deployment claims through parsing, planning, execution, and tests |
| `implementation-centralized-ci-environment-detection.md` | implementation | ExecutionEnvironment in common; reused by telemetry + codegen |
| `implementation-tsl-p1-p2-completion.md` | implementation | TSL hardening: diagnostics, options factory, compile gates, test suites |
| `implementation-registration-attributes.md` | implementation | Historical dgtp 2.x combined-push registration behavior; current v3 requirements are in the migration guide |
| `implementation-assembly-version-upgrade-migration.md` | implementation | Historical dgtp 2.x assembly migration behavior and flags; see the migration guide for v3 behavior |
| `implementation-plugin-push-outdated-assembly-migration.md` | implementation | New `dgt.power.plugin` module: `plugin push` pipeline (Local/Planning/Dataverse/Execution), unconditional outdated-assembly migration+purge (no flag), code-activity rejection |
| `implementation-plugin-push-fail-closed-validation.md` | implementation | Plugin push aborts incomplete metadata reads, validates requested solutions and package component definitions before writes, and rejects all Create pre-images |
| `implementation-plugin-step-config-set.md` | implementation | `dgtp plugin step config set`: enum-validated stages 10/20/30/40, file/inline config, composite resolution with concise name/ID ambiguity diagnostics |
| `research-plugin-plan-output-adaptation.md` | research | Adaptation of webresource V2 plan-tree and execution reporting for hierarchical plugin pushes |
| `decision-plugin-deployment-plan-pipeline.md` | decision | Single typed plan shared by plugin push rendering and execution, including upgrade and package ID-resolution semantics |
| `decision-plugin-upgrade-retention-policy.md` | decision | Strict declarative standalone replacement policy and major/minor version-train behavior |
| `decision-plugin-secure-config-provisioning.md` | decision | Secure step configuration is CI-provisioned by a future separate post-deployment command, never source-controlled |
| `decision-plugin-confirmation-precedence.md` | decision | Optional per-target plugin push confirmation is overridden by dry-run and non-interactive/CI execution modes |
| `research-plugin-custom-data-provider-push.md` | research | Provider identity, grouped metadata, handler lifecycle, specialized table provisioning, solution membership, and live-validation caveats |
| `research-plugin-package-content-idempotence.md` | research | Package deployment is idempotent by package-file SHA-256 hash, not immutable package version |
| `research-plugin-assembly-version-trains.md` | research | Dataverse updates build/revision changes in-place in either direction; major/minor changes require a new assembly |
| `research-plugin-step-secure-configuration.md` | research | Both step configuration fields are environment-provisioned state; cross-train replacement preserves matching step associations by reassignment |
| `research-metadata-load-context-resolver.md` | research | Metadata resolver deduplicates DLL filenames and prioritizes runtime, target, then module paths |
| `research-sdk-message-filter-secondary-entity.md` | research | Entity-scoped SDK message filters require a null secondary-entity predicate for `none`/empty declarations |
| `decision-plugin-registration-v3-cutoff.md` | decision | V3 resource-oriented plugin command requires `Digitall.Plugins.Registration` 3.0.0+ and drops historical registration namespaces |
| `research-qodana-plugin-push-findings.md` | research | Qodana cleanup patterns for plugin push exceptions, ownership-transfer test helpers, and namespace imports |
| `implementation-codegeneration-metadata-service-legacy-split.md` | implementation | Codegeneration metadata service split: keep shared/V2 code in main file and move V1 overloads into a legacy partial |
| `guide-webresource-solution-lazy-add.md` | guide | `webresource push`: add resources to solutions only when not already a member; reuse one pre-fetch for upsert and obsolete checks |
| `implementation-webresource-module-plan.md` | implementation | Historical extraction plan for `webresource push`, including behavior carried forward from dgtp 2.x |
| `research-servicepointmanager-dotnet8.md` | research | ServicePointManager no-op; Dataverse.Client has no HttpClient hook |
| `research-isolated-storage-major-version-scoping.md` | research | Historical assembly-major isolated-storage scoping and the stable app-data replacement now used by connections/state |
| `research-tsl-fluid-hardening.md` | research | Fluid.Core stability assessment and hardening strategy |
| `research-v2-typescript-config-design-gaps.md` | research | Current V2 TS caveats after redesign: no `TypingPath`, no per-entity filters, string-based `forms.filter` |
| `research-form-language-localization.md` | research | Metadata `Label` LCID resolution vs. OOB record data (`systemform.name`) being session-UI-language dependent, not per-request; `FormViewModel.LanguageCode` gap fix; known limitation + warning for form name/config.Forms matching |
| `implementation-v2-codegeneration-config-shape.md` | implementation | Final nested V2 config shape: shared `entities` scope, `optionSets`, and target-specific `output` blocks |
| `implementation-v2-schema-allows-dollar-schema.md` | implementation | V2 schemas now allow top-level `$schema` for editor compatibility under `additionalProperties: false` |
| `implementation-linter-module-phase-1.md` | implementation | Phase 1 `dgt.power.linter` scaffold: `ILintRule`/`LintRuleCatalog`/`LintContext`/`LintRunCommand`, first rule `naming.unmanaged-field-logicalname` |
| `guide-linter-rule-implementation-pitfalls.md` | guide | `EntityFilters.Entity` excludes `Attributes` (always-empty cache trap); never paper over a broken cache with a doomed-to-fail fallback call; lint rule tests must assert on findings, not just the command's exit code |
| `decision-error-telemetry-anonymization.md` | decision | Crash reporting via OTel exception events; anonymization scope (GUIDs, home-dir paths, org/tenant URLs) and known limitations |
| `decision-generic-command-deprecation.md` | decision | `[DeprecatedCommand]` attribute + `DeprecationInterceptor`: how to deprecate any command/branch, and why argv-position detection was replaced |
| `guide-persist-after-verify-connection-commands.md` | guide | `CreateConnectionCommand`: stage secrets, verify before persistence, and roll back metadata/secrets on failure |
| `implementation-175-ts-mock-form-improvements.md` | implementation | Issue #175 plan: factory function, relaxed server mock types, no-$select fix, type re-exports, SubGrid helper, languageId option |
| `decision-azure-devops-workload-identity-federation.md` | decision | WIF/OIDC connections via `AzurePipelinesCredential`; CLI surface, architecture, why not `pac`/hand-rolled OIDC, CI REST-lookup pattern, self-constructed `SYSTEM_OIDCREQUESTURI` (no task dependency, verified live), Managed Identity out-of-scope split |
| `implementation-linter-phase-2-fail-gate-baseline.md` | implementation | Phase 2 linter: `--fail-on`/`--baseline`/`--update-baseline`/`--sarif-output`, `LintFinding.BaselineKey`, `Reporting/SarifWriter` |
| `implementation-linter-entity-component-membership.md` | implementation | `EntityComponentMembership`/`EntityComponentMembershipResolver`: resolves attributes for entities added with `RootComponentBehavior.IncludeSubcomponents` (no per-attribute solutioncomponent rows exist for those); generic `ExplicitSubcomponentsByType` for future component types; table-level (not solution-level) `IsManaged` drives `completeness.table-root-component-behavior` (implemented) |
| `implementation-solution-copy-components.md` | implementation | `dgtp solution copy-components`: CLI shape, required dependencies vs. subcomponents (Entity-only `DoNotIncludeSubcomponents`; `--apps` modes), unified managed/active-layer inclusion rule across all componenttypes, generic managed-state resolution via `solutioncomponentdefinition.primaryentityname`, and the `OptionSetValue?` vs `int?` gotcha on `SolutionComponent.ComponentType`/`RootComponentBehavior` |
| `decision-ilint-rule-default-severity-contract.md` | decision | Keep `ILintRule.DefaultSeverity` as public metadata despite the implementation-only Qodana usage finding; narrowly suppress the inspection instead of removing the interface member |
| `decision-resource-oriented-cli-restructuring.md` | decision | Planned multi-phase CLI restructuring: `dgt.power.linter` → `dgt.power.solution`, `dgtp solution lint`/`dgtp solution version`, maintenance/analyze command-to-resource mapping tables, hard-cut deprecation policy, Sarif.Sdk adoption |
| `research-webresource-push-v2-review.md` | research | Design and compatibility review of resource-oriented webresource deployment, including deferred publish batching and plugin-aligned execution progress |
