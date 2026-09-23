# Plugin deployment plan pipeline

## Decision

`plugin push` uses one typed deployment plan for both visualization and execution. Low-level
local/remote state is represented as `Comparison` and `ComparisonSet` values; the term `Plan` is
reserved for the complete executable deployment graph.

The comparison helper is named `PluginRegistrationComparer`. Its collection results are named
`PluginTypeComparisonSet`, `PluginStepComparisonSet`, and `PluginStepImageComparisonSet`; individual
items are `*Comparison` records. A comparison captures remote state and whether a mutation is
required; no-change is represented by no required mutation, not by a fake change. These files live
in `Planning/Comparison/`. Executable-plan records live in `Planning/Deployment/`. Their namespaces
follow the directory structure: `dgt.power.plugin.Planning.Comparison` and
`dgt.power.plugin.Planning.Deployment`.

Assembly package ownership is comparison state (`AssemblyComparison.IsPackageOwned`), not a change
or executable action. The renderer therefore omits a status label for package-owned assemblies.

`LocalAssembly.ContentHash` and `LocalPackage.PackageHash` are immutable values populated when the
reader first loads DLL/package bytes. Remote hashes are populated by their repositories. Comparison
properties therefore compare hash strings and do not decode or hash payloads repeatedly.

1. `PluginDeploymentPlanner` loads the remote snapshot, validates SDK messages and Custom APIs,
   and creates an `AssemblyDeploymentPlan` or `PackageDeploymentPlan`.
2. `PluginPlanRenderer` renders the plan's hierarchy.
3. `PluginPushExecutor` applies the same plan without recalculating changes.

`PluginPushCommand` owns this sequence and stops after rendering for dry runs. No deployment
pipeline wrapper is used: the command is the terminal UI and orchestration boundary.

Terminal output has explicit phase labels: `Plan` precedes planning and tree rendering, while
`Execution` appears only immediately before a non-dry-run apply. The command does not use a
cross-phase spinner, so these labels remain the visible phase boundary. It scopes live spinners to
the work that precedes stable output: `Processing <file>...` ends before the plan tree is rendered,
and `Applying <file>...` ends before execution returns. This prevents Spectre's live display from
moving phase labels or the plan tree.

`PluginPushExecutor`, `PluginTypeDeploymentExecutor`, and `OutdatedAssemblyMigrator` accept an
optional typed `Action<PluginDeploymentProgress>` callback. They invoke it only after a successful
Dataverse write. `PluginPushCommand` renders completed-operation lines and reports `No changes
applied` when the callback received no events. It renders callback events immediately while the
`Applying deployment plan...` spinner is active so partial successes remain visible if a later
operation fails. Executors do not reference terminal output types.

## Plan contents

The aggregate plan retains the concrete assembly/package change, nested plugin type and step
change sets, image changes, Custom API link changes, solution and managed-identity
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
