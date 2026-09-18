# Implementation: `dgt.power.plugin` Module — `plugin push` Pipeline & Outdated Assembly Migration

## Architecture

`dgt.power.plugin` is a from-scratch module (see `decision-resource-oriented-cli-redesign.md` for why)
implementing `dgtp plugin push`, structured in four layers:

- **`Local/`** — pure parsing of on-disk artifacts: `AssemblyReflectionReader` (reflects a `.dll` for
  plugin types/steps/images declared via `Digitall.Plugins.Registration` attributes),
  `PluginPackageReader` (reads `.nupkg` content). Produces `LocalAssembly`, `LocalPluginType`,
  `LocalPluginStep`, `LocalPluginStepImage` records - no Dataverse access.
- **`Planning/`** — pure decision logic, no Dataverse access, fully unit-testable. `PluginPushPlanner`
  compares Local vs. Remote (Dataverse-shaped) records and returns plan records (not tuples - see
  `PluginTypeReconciliationPlan`, `PluginStepReconciliationPlan`, `PluginStepImageReconciliationPlan`,
  `OutdatedTypeMigration`).
- **`Dataverse/`** — thin repositories (`IPluginAssemblyRepository`, `IPluginTypeRepository`,
  `ISdkMessageProcessingStepRepository`, `ISdkMessageProcessingStepImageRepository`,
  `ICustomApiRepository`, `ISdkMessageRepository`), one per entity, CRUD only - no decision logic.
- **`Execution/`** — orchestrators that call Planning then apply the plan via Dataverse repos:
  `PluginPushExecutor` (top-level per-assembly orchestration), `PluginTypeReconciler` (types/steps/
  images/custom-api reconciliation for the current assembly), `OutdatedAssemblyMigrator` (see below).

`PluginPushCommand` constructs every repo/executor/migrator via `new` (module-local DI convention - see
`decision-resource-oriented-cli-redesign.md`), casting `Connection` to `(IOrganizationServiceAsync2)`
once. Per-target processing catches generic exceptions but excludes `AbstractPowerException` subtypes
from the catch filter so they propagate to `Program.cs`'s global exception handler for correct exit
codes (e.g. `WorkflowActivityNotSupportedException` → `NotSupported`).

## Code Activities Are Rejected, Not Supported

Registering workflow activities (`CodeActivity`) only works on Windows due to Workflow Foundation DLL
dependencies, and code activities are effectively deprecated in Dataverse in favor of Custom APIs.
`plugin push` fails fast with `WorkflowActivityNotSupportedException` (exit code `NotSupported`) the
moment a `CodeActivity` is detected in the assembly, instead of silently skipping or attempting
registration. The legacy `push` command still supports them as-is.

## Mixed Plugin + Code Activity Assemblies Are Rejected

Mixing plugin types and code activities in the same assembly is not supported by Dataverse. This check
is preserved: `AssemblyReflectionReader`/the plugin type reader detects code activities anywhere in the
assembly and raises `WorkflowActivityNotSupportedException` before any plugin types are processed, even
if the assembly also contains valid plugin types.

## Outdated Assembly Migration (`--purge-outdated`)

Replaces the legacy `push` module's `--delete-on-upgrade`/`--no-migrate-custom-apis` flags (see
`implementation-assembly-version-upgrade-migration.md` for the old semantics) with a single flag and
simplified, unconditional Custom API handling - both explicitly requested by the user:

- **`--purge-outdated`** replaces `--delete-on-upgrade` (same purge/delete semantics: migrate steps,
  then delete the outdated assembly + its plugin types + any steps left without a replacement type).
- **The Custom API opt-out flag (`--no-migrate-custom-apis`) was removed entirely.** Custom API links
  are now migrated **unconditionally** on every `Upgrade` (major/minor version change), independent of
  `--purge-outdated` - the user's reasoning: "I cant think of any use case for" keeping old Custom API
  links pointed at a superseded assembly.
- Type matching (old outdated type → new replacement type) is done by `TypeName`, purely in the
  Planning layer (`PluginPushPlanner.PlanOutdatedTypeMigration`), comparing the old `RemotePluginType`
  against the newly-declared `LocalPluginType` list - **not** against the newly-created remote types.
  This keeps the plan/report step correct even in `--dry-run`, before any Dataverse write happens.
- The **apply** step (`OutdatedAssemblyMigrator.MigrateAsync`) resolves the actual new-type GUIDs lazily
  (`newTypeIdsByName ??= ...`), only once, only when non-dry-run and only when at least one migration
  needs applying.
- `IPluginAssemblyRepository.ListOutdatedAsync(name, excludeId)` returns **all** previously-superseded
  assemblies with the same name (not just the immediately prior version) ordered by version descending -
  matching the old `BuildOutdatedAssemblyContentFromCrm` behavior.
- `ISdkMessageProcessingStepRepository.ReassignPluginTypeAsync(stepId, newPluginTypeId)` updates only
  the `EventHandler` field, preserving step id/history/filters/images.
- Orphaned-step cascade deletion (when a type has no replacement) reuses the existing
  `IPluginTypeRepository.GetDependentStepIdsAsync` + `DeleteAsync` pattern already used for orphaned-type
  purging in `PluginTypeReconciler` - no new repository method was needed for that path.
- `PluginPushExecutor.ProcessAssemblyAsync` calls `OutdatedAssemblyMigrator.MigrateAsync` only when
  `plan.Action == AssemblyAction.Upgrade`.

## Testing Notes / Fake Service Gotchas

- `ISdkMessageProcessingStepRepository.ListByPluginTypeAsync` **inner-joins** `sdkmessage` (to resolve
  the message name) - any fake `SdkMessageProcessingStep` used in a test that goes through this method
  must have `SdkMessageId` set (via `SeedMessage(service, "Create")` or similar) and non-null
  `Mode`/`Stage`, or the fake join silently excludes the row (returns empty) / `ToRemoteStep` throws
  `NullReferenceException`.
- `RetrieveDependenciesForDeleteExecutor` (test fake for `GetDependentStepIdsAsync`) does **not** join
  `sdkmessage` - it only filters `sdkmessageprocessingstep.eventhandler == ObjectId` - so it works with
  minimal step fixtures that skip `SdkMessageId`.
- The `dgt.power.plugin.tests` project occasionally throws a `FaultException` about `customapi` missing
  from the fake `MetadataCache` when run via `dotnet test` with parallel execution; this is pre-existing
  test-harness flakiness (`Digitall.Dataverse.Testing` internals), not a regression - rerun to confirm.
- On this environment, `dotnet test` (and even `rtk`-wrapped test runs) can spuriously report
  "Zero tests ran" with no build errors, or fail to start the exe with "Access denied" due to a
  lingering file lock. Building explicitly (`dotnet build <project>`) then running the produced
  `.exe` directly from its `bin/Debug/net10.0` folder (retrying once after a short pause on access-denied)
  reliably surfaces the real pass/fail result.

## Status

Implemented, tested (131/131 `dgt.power.plugin.tests`, 173/173 `dgt.power.cli.tests`), and documented in
README's `plugin push` section. Remaining follow-up: delete legacy `Logic/`/`Model/` files and the
`UiPath.Workflow` package reference from `dgt.power.push` (tracked as `plugin-cleanup-old` in the
session's todo list, not yet started as of this writing).
