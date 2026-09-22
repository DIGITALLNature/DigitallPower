# Plugin push plan output adaptation

The webresource V2 branch separates planning from presentation and execution:

- `WebResourcePushPlanner` returns a `WebResourcePushPlan`.
- `WebResourcePlanRenderer` renders that plan as a Spectre `Tree`.
- `WebResourcePushExecutor` renders once, returns immediately for `--dry-run`, and otherwise
  starts a separate execution phase using `WebResourceExecutionReporter`.

The plugin module already has the equivalent decision data, but it is hierarchical:
`AssemblyPlan`/`PackagePlan` contain plugin type reconciliation, which contains step and image
reconciliation, plus custom API links and outdated-assembly migration. `PluginTypeReconciler`
currently plans and applies in one traversal and writes action lines directly to `IAnsiConsole`.

The implementation keeps `PluginPushPlanner` as the source of truth and uses a plugin-specific
output model/renderer. The renderer builds a tree in this order:
target (assembly or package) → assembly → plugin type → step → image, with custom API
link/unlink and obsolete type/step/image deletion as action-labelled leaves. It should render
before writes, and the executor should return after rendering when `DryRun` is set. Package
targets need one root per package and assembly; standalone assemblies can use the assembly root.
The existing plugin target loop means a renderer should be invoked per target or accept a list of
target plans if a single combined tree is preferred.

Plugin types are direct children of their assembly; there is no separate "plugin types plan"
grouping node. Existing plugin types are shown as `Keep` rather than the internal `Reconcile`
term. Solution membership is deliberately omitted from the tree, matching the webresource
experience; it remains an execution concern.

Important behavior differences from webresources:

- Plugin plans require repository lookups at every hierarchy level, so planning must be completed
  before rendering; do not use placeholder IDs to avoid those reads.
- New plugin types currently use `Guid.Empty` in dry-run to allow downstream planning. This is
  safe only if repositories treat that ID as an empty remote scope; a renderer should consume
  the returned plans rather than infer actions from IDs.
- Aggregate dry-run counts are intentionally removed; the tree is the authoritative representation
  of all planned changes.
- Managed-identity linking, Custom API resolution, and outdated-assembly
  migration are actions that should be represented explicitly in the plan if they can occur;
  solution membership is intentionally not rendered.
- Execution output can reuse the webresource pattern (`Status` spinner followed by completion
  lines), but plugin execution should move console writes out of `PluginTypeReconciler` so the
  same action is not printed once during planning and again during execution.

Planning now validates step message resolution and Custom API existence before rendering or
execution. Tests should assert the plan shape/action labels independently of terminal rendering,
then add renderer tests using a test `IAnsiConsole` for package/assembly nesting,
create/update/keep, purges, custom API changes, and dry-run-only actions. Dry-run tests should
assert zero Dataverse writes and the absence of aggregate count output.
