# Plugin deployment change surfaces

These observations describe coupling in the implementation, not an approved redesign.
Reassess them when the referenced responsibilities change.

## Deployment decisions have multiple consumers

The deployment plan is shared by rendering and execution, but it is a nested reconciliation
model rather than a fully resolved sequence of writes. The executor, renderer, and
`PluginDeploymentPlanExtensions.HasChanges` each interpret its state independently.
Adding a write requires checking all three consumers, including confirmation gating.

- `src/modules/dgt.power.plugin/Planning/Deployment/PluginDeploymentPlanExtensions.cs:14-90`
- `src/modules/dgt.power.plugin/Output/PluginPlanRenderer.cs:14-36,164-224`
- `src/modules/dgt.power.plugin/Execution/PluginTypeDeploymentExecutor.cs:158-223`
- `src/modules/dgt.power.plugin/Commands/PluginPushCommand.cs:157`

Normal step planning and upgrade planning independently reconstruct image comparisons,
unchanged-image lists, and solution links. Upgrade planning patches an already-built type
plan with migrated remote step identities.

- `src/modules/dgt.power.plugin/Planning/PluginDeploymentPlanner.cs:202-209,326-376`

## Ordering is a cross-resource contract

Standalone execution creates types, writes providers, applies registrations, and then
deletes obsolete types. Package execution applies registrations per assembly, writes
providers using the combined package type map, and only then performs cleanup.
Do not collapse these paths without preserving cross-assembly provider dependencies.

`OutdatedAssemblyMigrator` performs cleanup; step reassignment actually occurs in
`PluginTypeDeploymentExecutor.ApplyStepAsync`. Its name does not describe the whole
migration lifecycle.

- `src/modules/dgt.power.plugin/Execution/PluginPushExecutor.cs:28-41,93-130`
- `src/modules/dgt.power.plugin/Execution/PluginTypeDeploymentExecutor.cs:158-176`
- `src/modules/dgt.power.plugin/Execution/OutdatedAssemblyMigrator.cs:30-58`
- `tests/dgt.power.plugin.tests/Execution/DataProviderDeploymentTests.cs:102-158,207-284`

## Failed standalone upgrades can block subsequent pushes

Replacement assembly creation precedes registration/provider writes and old-assembly
cleanup. A failure between creation and cleanup can retain both standalone assemblies.
The standalone planner rejects multiple same-name standalone assemblies before planning,
so retaining the old assembly protects dependencies but does not by itself provide
automatic retry/recovery.

This is a consequence of the current control flow, not a live-environment reproduction.

- `src/modules/dgt.power.plugin/Execution/PluginPushExecutor.cs:28-41,68-71`
- `src/modules/dgt.power.plugin/Planning/PluginDeploymentPlanner.cs:27-35`
- `tests/dgt.power.plugin.tests/Execution/DataProviderDeploymentTests.cs:132-144`

## Orchestration tests use a separate implementation

`PluginPushExecutorTests.DeploymentTestHarness` independently implements plan/render/
dry-run/execute behavior instead of invoking the production command orchestration.
Those tests cover deployment components together, but cannot establish that production
confirmation, target handling, and dry-run wiring behave identically.

- `tests/dgt.power.plugin.tests/Execution/PluginPushExecutorTests.cs:25-68`
- `tests/dgt.power.plugin.tests/Commands/PluginPushCommandTests.cs:10-19`
- `src/modules/dgt.power.plugin/Commands/PluginPushCommand.cs:146-176`

## Declaration ownership differs by resource

Missing local steps/types become deletion candidates, and removing a Custom API declaration
unlinks existing APIs. Provider declarations instead preserve undeclared operations and
omitted optional metadata. Removing a managed-identity declaration does not schedule unlinking.
Any redesign must explicitly choose these ownership semantics rather than assuming every
resource is reconciled authoritatively.

- `src/modules/dgt.power.plugin/Planning/PluginRegistrationComparer.cs:42-43,86-87`
- `src/modules/dgt.power.plugin/Planning/PluginDeploymentPlanner.cs:230-248,124-129`
- `src/modules/dgt.power.plugin/Planning/DataProviderDeploymentPlanner.cs:136-141`
- `src/modules/dgt.power.plugin/Repositories/EntityDataProviderRepository.cs:216-231`

Package identity selection takes the first attributed assembly. Identity lookup matches
application ID alone; an existing record's tenant is not checked against a supplied tenant.
Conflicting declarations are therefore not treated as explicit deployment conflicts.

- `src/modules/dgt.power.plugin/Planning/PluginDeploymentPlanner.cs:76-82`
- `src/modules/dgt.power.plugin/Repositories/ManagedIdentityRepository.cs:14-40`
