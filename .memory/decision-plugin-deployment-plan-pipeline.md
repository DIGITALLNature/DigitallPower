# Plugin deployment plan pipeline

## Decision

`plugin push` uses one typed deployment plan for both visualization and execution:

1. `PluginDeploymentPlanner` loads the remote snapshot, validates SDK messages and Custom APIs,
   and creates an `AssemblyDeploymentPlan` or `PackageDeploymentPlan`.
2. `PluginPlanRenderer` renders the plan's hierarchy.
3. `PluginPushExecutor` applies the same plan without repeating reconciliation decisions.

`PluginDeploymentPipeline` owns this sequence and stops after rendering for dry runs.

## Plan contents

The aggregate plan retains the concrete assembly/package action, nested plugin type and step
reconciliation plans, image changes, Custom API link changes, solution and managed-identity
operations, and complete outdated-assembly migration data. Newly created record IDs are passed
down during execution rather than represented by `Guid.Empty` during planning.

For upgrades, plugin types are planned against an empty replacement assembly while every existing
same-named assembly is captured as an outdated assembly. This ensures dry-run output includes both
replacement creation and the migrations/deletions that execution will perform.

## Package identity resolution

Dataverse can create package-owned assembly records as a side effect of uploading a plugin
package. After the package write, execution may look up such an assembly by name to obtain its
server-generated ID. This is identity resolution only: it does not rerun matching or alter the
planned actions. If no package-owned record exists, the preplanned standalone assembly operation
is applied.

## Testing boundary

- Planner tests verify the complete planned hierarchy and resolved operation data.
- Renderer tests verify labels and action presentation from an existing plan.
- Executor tests supply an existing plan and verify writes; execution must not invoke planner
  decision logic.
