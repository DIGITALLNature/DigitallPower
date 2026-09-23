# Plugin push plan output adaptation

The webresource V2 branch separates planning from presentation and execution:

- `WebResourcePushPlanner` returns a `WebResourcePushPlan`.
- `WebResourcePlanRenderer` renders that plan as a Spectre `Tree`.
- `WebResourcePushExecutor` renders once, returns immediately for `--dry-run`, and otherwise
  starts a separate execution phase using `WebResourceExecutionReporter`.

The plugin module already has the equivalent decision data, but it is hierarchical:
`AssemblyChange`/`PackageChange` feed the deployment plan, which contains type, step, image,
Custom API, and outdated-assembly operations.

The implementation keeps `PluginStateComparer` as the pure matching helper and uses
`PluginDeploymentPlanner` as the aggregate source of truth. The renderer builds a tree in this order:
target (assembly or package) → assembly → plugin type → step → image, with custom API
link/unlink and obsolete type/step/image deletion as action-labelled leaves. It should render
before writes, and the executor should return after rendering when `DryRun` is set. Package
targets need one root per package and assembly; standalone assemblies can use the assembly root.
`PluginPushCommand` renders and then executes one plan per target.

Plugin types are direct children of their assembly; there is no separate "plugin types plan"
grouping node. No-op plugin type and step actions use the shared `Unchanged` terminology.
Solution membership is deliberately omitted from the tree, matching the webresource
experience; it remains an execution concern.

Important behavior differences from webresources:

- Plugin plans require repository lookups at every hierarchy level, so planning must be completed
  before rendering; do not use placeholder IDs to avoid those reads.
- New assemblies, types, and steps are planned against empty remote snapshots. Their
  Dataverse-generated IDs are passed down during execution rather than represented by
  `Guid.Empty` during planning.
- Aggregate dry-run counts are intentionally removed; the tree is the authoritative representation
  of all planned changes.
- Managed-identity linking, Custom API resolution, and outdated-assembly
  migration are actions that should be represented explicitly in the plan if they can occur;
  solution membership is intentionally not rendered.
- Execution no longer emits the legacy action list from `PluginTypeDeploymentExecutor`; the plan tree is
  the action report for both dry-run and normal execution.

Planning now validates step message resolution and Custom API existence before rendering or
execution. Tests should assert the plan shape/action labels independently of terminal rendering,
then add renderer tests using a test `IAnsiConsole` for package/assembly nesting,
create/update/keep, purges, custom API changes, and dry-run-only actions. Dry-run tests should
assert zero Dataverse writes and the absence of aggregate count output.

Custom API handler types are a special case: although they carry a registration attribute, their
legacy `sdkmessageprocessingstep` implementation record is not a normal declarative step. Planning
and reconciliation must skip step/image matching for types with a Custom API message name, so that
the implementation record is preserved while the Custom API link is reconciled.
