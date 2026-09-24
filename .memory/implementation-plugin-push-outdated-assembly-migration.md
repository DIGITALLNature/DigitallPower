# Implementation: `dgt.power.plugin` Module — `plugin push` Pipeline & Outdated Assembly Migration

## Architecture

`dgt.power.plugin` is a from-scratch module (see `decision-resource-oriented-cli-redesign.md` for why)
implementing `dgtp plugin push`, structured in four layers:

- **`Local/`** — pure parsing of on-disk artifacts: `AssemblyReflectionReader` (reflects a `.dll` for
  plugin types/steps/images declared via the registration attributes - see below),
  `PluginPackageReader` (reads `.nupkg` content). Produces `LocalAssembly`, `LocalPluginType`,
  `LocalPluginStep`, `LocalPluginStepImage` records - no Dataverse access.
- **`Remote/`** — minimal, already-fetched Dataverse state records (`RemoteAssembly`, `RemotePackage`,
  `RemotePluginType`, `RemotePluginStep`, `RemotePluginStepImage`), mirroring `Local/` but for the
  target environment. Split out of `Planning/` (where these types originally lived, mixed in with pure
  decision logic) to keep the module's namespace layout self-explanatory: `Local` and `Remote` are both
  plain state/model namespaces, `Planning` is pure logic.
- **`Planning/`** — typed deployment plans and planning logic. `PluginRegistrationComparer` contains pure
  Local-vs.-Remote matching helpers, while `PluginDeploymentPlanner` loads the complete remote
  snapshot, validates references, and returns the immutable plan consumed by rendering and execution.
  Low-level comparison values use `Comparison`/`ComparisonSet`; `Plan` is reserved for the complete
  executable deployment graph. The files and namespaces are grouped under `Planning.Comparison` and
  `Planning.Deployment`.
- **`Repositories/`** — thin repositories (`IPluginAssemblyRepository`, `IPluginTypeRepository`,
  `ISdkMessageProcessingStepRepository`, `ISdkMessageProcessingStepImageRepository`,
  `ICustomApiRepository`, `ISdkMessageRepository`), one per entity, CRUD only - no decision logic.
  Renamed from `Dataverse/` because that name collided conceptually with the separate
  `dgt.power.dataverse` generated-entities project these repositories depend on (`using
  dgt.power.dataverse;`) - "Dataverse" described *what they talk to*, not *what they are*.
- **`Execution/`** — `PluginPushExecutor` applies top-level assembly/package operations,
  `PluginTypeDeploymentExecutor` applies preplanned type/step/image/Custom API operations, and
  `OutdatedAssemblyMigrator` applies preplanned upgrade migrations. `PluginPushCommand` owns
  plan/render/execute sequencing and terminal output.

`PluginPushCommand` constructs every repo/executor/migrator via `new` (module-local DI convention - see
`decision-resource-oriented-cli-redesign.md`), casting `Connection` to `(IOrganizationServiceAsync2)`
once. Per-target processing catches generic exceptions but excludes `AbstractPowerException` subtypes
from the catch filter so they propagate to `Program.cs`'s global exception handler.

## Declaratively-Registered vs. Plain `IPlugin` Types

`LocalPluginType.HasRegistrationAttribute` distinguishes a type carrying one of the registration
attributes (`PluginRegistration`/`CustomApiRegistration`/`CustomDataProviderRegistration`) from a plain
`IPlugin`-implementing type with none (named `IsPowerPlugin` until the term was dropped as unclear -
this mirrors the legacy `push` module's `AssemblyType.PowerPlugin` flag conceptually, but with clearer
naming; that legacy module was left untouched). Both kinds of types get a `PluginType` record
created/kept in Dataverse (a step can only reference a registered type, so even manually-managed types
need the type row) - but only types with a registration attribute have their steps/images/Custom-API
link parsed and reconciled. Types without one are intentionally left alone in
the deployment plan, so matching remote types and any steps configured for them manually through the
Plugin Registration Tool are never touched or purged. This supports a legitimate mixed scenario: some
plugin types are declaratively managed by `plugin push`, while others remain manually managed in the
same assembly.

Because this is easy to trigger by accident (forgetting to add a registration attribute silently
results in "type registered, no steps ever pushed, no error"), `AssemblyReflectionReader.BuildPluginType`
prints a console hint for every type without one, naming the type and asking whether the attribute was
forgotten. The assembly-level `LocalAssembly.Kind` flags (`LocalAssemblyKind.Plugin` /
`.DeclarativePlugin`) are informational only - the only place `Kind` is read is `PluginPushCommand.cs`'s
`Kind == Undefined` check (does the assembly contain any plugin types at all); the `Plugin`/`DeclarativePlugin`
distinction at the assembly level does not currently drive any different behavior.

## No Package Dependency on the Registration Attributes

`dgt.power.plugin` has **no `PackageReference` on the registration attributes package**
(`Digitall.Plugins.Registration`). `AssemblyReflectionReader` loads the
target `.dll` into a `System.Reflection.MetadataLoadContext` (reflection-only context), so the types it
sees are never assignable to (or comparable with) any locally referenced attribute type anyway -
detection is purely by attribute type `Name`/`Namespace` string matching via `CustomAttributeData`.
`Local/RegistrationAttributeNames.cs` recognizes only the supported
`Digitall.Plugins.Registration` namespace. This v3 boundary deliberately drops historical aliases;
users maintaining old registrations should use dgtp v2 or upgrade their registration package. The
legacy `dgt.power.push` module still has its own independent
`PackageReference` on the registration package because it actually instantiates
`WorkflowRegistrationAttribute` at runtime for code-activity support (a real type dependency); that
module was intentionally left untouched.

## Code Activities Are Rejected, Not Supported

Registering workflow activities (`CodeActivity`) only works on Windows due to Workflow Foundation DLL
dependencies, and code activities are effectively deprecated in Dataverse in favor of Custom APIs.
`plugin push` fails fast with `WorkflowActivityNotSupportedException` the
moment a `CodeActivity` is detected in the assembly, instead of silently skipping or attempting
registration. The legacy `push` command still supports them as-is.

## Mixed Plugin + Code Activity Assemblies Are Rejected

Mixing plugin types and code activities in the same assembly is not supported by Dataverse. This check
is preserved: `AssemblyReflectionReader`/the plugin type reader detects code activities anywhere in the
assembly and raises `WorkflowActivityNotSupportedException` before any plugin types are processed, even
if the assembly also contains valid plugin types.

## Outdated Assembly Migration (unconditional, no flag)

Replaces the legacy `push` module's `--delete-on-upgrade`/`--no-migrate-custom-apis` flags (see
`implementation-assembly-version-upgrade-migration.md` for the old semantics). This went through two
design iterations before landing on the final, flag-free shape:

1. First iteration introduced `--purge-outdated` (replacing `--delete-on-upgrade`) while keeping Custom
   API migration unconditional and gating only step migration + assembly/type deletion behind the flag.
2. **Final iteration (this one): the flag was removed entirely.** The user's reasoning: registration
   attributes are the declarative source of truth for the desired Dataverse state, and `plugin push`
   already unconditionally purges orphaned steps/types on the *current* assembly (no flag) - so gating
   the *outdated*-assembly cleanup behind a flag was an inconsistency, not a real safety feature. It also
   produced a worse "split-brain" state than either extreme: Custom API links pointing at the new type
   while steps still fired the old one. `--dry-run` remains the only safety valve (preview before any
   write); there is no other opt-out.

Current unconditional behavior on every `Upgrade` (major/minor version change):
- Custom API links **and** plugin steps are migrated to the same-named replacement type on the new
  assembly.
- The outdated assembly, its plugin types, and any steps left without a replacement type are then always
  deleted.

Mechanics (unchanged since the first iteration):
- Type matching (old outdated type → new replacement type) is done by `TypeName`, purely in the
  Planning layer (`PluginRegistrationComparer.CompareOutdatedTypes`), comparing the old `RemotePluginType`
  against the newly-declared `LocalPluginType` list - **not** against the newly-created remote types.
  This keeps the plan/report step correct even in `--dry-run`, before any Dataverse write happens.
- The apply step receives the replacement type IDs produced by `PluginTypeDeploymentExecutor`; it performs
  no discovery or reconciliation queries.
- `IPluginAssemblyRepository.ListOutdatedAsync(name, excludeId)` returns **all** previously-superseded
  assemblies with the same name (not just the immediately prior version) ordered by version descending -
  matching the old `BuildOutdatedAssemblyContentFromCrm` behavior.
- `ISdkMessageProcessingStepRepository.ReassignPluginTypeAsync(stepId, newPluginTypeId)` updates only
  the `EventHandler` field, preserving step id/history/filters/images.
- Orphaned-step cascade deletion (when a type has no replacement) reuses the existing
  `IPluginTypeRepository.GetDependentStepIdsAsync` + `DeleteAsync` pattern already used for orphaned-type
  purging in the typed deployment plan - no new repository method was needed for that path.
- `PluginPushExecutor` applies `OutdatedAssemblyDeployment` only after replacement types exist.

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
  "Zero tests ran" with no build errors, or fail to start the test `.exe` with "Access denied" due
  to Windows Defender. Build explicitly (`dotnet build <project>`) and run the produced test DLL
  through `dotnet exec <path-to-test.dll>` to reliably surface the real pass/fail result.

## Status

Implemented, tested (130/130 `dgt.power.plugin.tests`, 173/173 `dgt.power.cli.tests`), and documented in
README's `plugin push` section. The `--purge-outdated` flag was later removed entirely - outdated
assembly migration/purge is now unconditional (see the section above).

The scaffolding-era leftover `Logic/`/`Model/` files inside `dgt.power.plugin` (copies of
`AssemblyProcessor`/`AssemblyModelBuilder`/`AssemblyValidator` and the old `Model/*` types used only as
a porting reference while building the new Local/Planning/Dataverse/Execution layers) have been deleted,
along with the now-unneeded `UiPath.Workflow` package reference from `dgt.power.plugin.csproj`. The only
class from the old `Model` namespace still needed was `AssemblyException` (used by
`AssemblyReflectionReader.MapDataProviderEventToMessage` for an unresolvable data-provider event value) -
it was moved to the module root as `dgt.power.plugin.AssemblyException`, alongside the other top-level
exception types (`WorkflowActivityNotSupportedException`, `InvalidPluginStepException`,
`UnresolvedPluginStepMessageException`).

**Note:** the legacy `dgt.power.push` module (backing the still-supported `push` command) has its own,
separate copies of `Logic/AssemblyProcessor.cs`, `Logic/AssemblyModelBuilder.cs`, `Model/*`, etc. Those
are intentionally left in place - `push` is not yet deprecated - and are unrelated to the cleanup
described here.
