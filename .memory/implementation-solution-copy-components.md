# Implementation: `dgtp solution copy-components`

Copies `solutioncomponent` rows from one or more source solutions into an unmanaged target
solution. Lives in `dgt.power.solution` alongside `version`/`lint`.

## CLI shape

```
dgtp solution copy-components <Target> --source <Sol1,Sol2> [--dry-run] [--raw]
```

- `<Target>` (positional) - the unmanaged solution being mutated (consistent with `solution
  version <Solution>`/`solution lint <Solution>`: the positional is always the solution the verb
  acts on).
- `--source`/`-s` (required, comma-separated) - one or more source solutions. Multiple sources are
  inherent to this feature (unlike `lint`'s dropped multi-solution mode).
- `--dry-run` - renders the plan table, never calls `AddSolutionComponentRequest`.
- `--raw` - disables best-practice normalization (mirrors the source's own `RootComponentBehavior`,
  skips the managed/active-layer filter). Best-practice mode is the **default** (opt-out design,
  confirmed by the user) - not opt-in.

## Why `AddRequiredComponents` is always `false` (never a flag)

Research finding (Microsoft Learn, `AddSolutionComponentRequest.AddRequiredComponents`): when
`true`, Dataverse transitively expands to every component it considers "required" - this is what
silently drags tables/forms/views/sitemaps into a solution when adding a model-driven app
(`AppModule`, componenttype 80, missing from the generated `SolutionComponent.Options.ComponentType`
enum - it's resolved generically via `solutioncomponentdefinition` instead, see below). None of
Microsoft's own SDK samples set it `true`. Conclusion: this alone satisfies "don't let the platform
auto-pull app dependencies" - **no app-specific code path exists**, the rule is applied uniformly to
every componenttype, unconditionally (not gated by `--best-practices`/`--raw`).

`AddSolutionComponentRequest.DoNotIncludeSubcomponents` is a plain bool - there is no way to
request `RootComponentBehavior.IncludeAsShellOnly` (2) through this message. Only 0
(`IncludeSubcomponents`)/1 (`DoNotIncludeSubcomponents`) are ever produced; this matches
`TableRootComponentBehaviorRule`, which also only distinguishes complete vs. not-complete.

## Unified inclusion rule (best-practice mode)

Broadened during design Q&A from "tables only" to **all componenttypes** (user's explicit choice):

- **Entity** rows are always included (anchor for children): `DoNotIncludeSubcomponents = table's
  own `EntityMetadata.IsManaged`` (unmanaged -> complete, managed -> skeleton).
- **Every other componenttype** (attribute, form, view, workflow, webresource, appmodule, ...):
  included if unmanaged; if managed, included only if its top `msdyn_componentlayer` row is the
  synthetic `"Active"` layer (mirrors `BaseAnalyze.GetSolutionLayers`/`GetTopNotActiveLayer` in
  `dgt.power.analyzer`, ported to `IOrganizationServiceAsync2`). No active layer = redundant vs. the
  managed baseline = skipped.
- Managed-state resolution is generic, not per-componenttype-hardcoded:
  - Entity/Attribute: metadata-driven (`EntityMetadata.IsManaged`/`AttributeMetadata.IsManaged`),
    reusing metadata already fetched for the entity-membership pass.
  - Everything else: resolved via the componenttype's own backing table (`solutioncomponentdefinition.primaryentityname`),
    querying that table's own `ismanaged` column. Componenttypes with no backing table (pure
    metadata, e.g. Relationship/OptionSet/EntityKey) or whose backing table lacks `ismanaged`
    **fail open** (treated as unmanaged, always included) - best-practice filtering can only apply
    where a managed state is actually observable. This is a known, accepted v1 scope limit.
  - `solutioncomponentdefinition` is queried unconditionally (single small query, no per-type
    filter) for both `name` (matches `msdyn_componentlayer.msdyn_solutioncomponentname`) and
    `primaryentityname` (backing table) - see `SolutionComponentDefinitionInfo`.

Multiple sources containing the same component (by componenttype+objectid) are deduped; on
conflicting `RootComponentBehavior` (relevant for `--raw` only) the most complete one wins.

## Files

```
src/modules/dgt.power.solution/
  Base/
    CopyComponentsSettings.cs
    SolutionComponentDefinitionInfo.cs
    ComponentCopyDecision.cs
    ComponentManagedStateResolver.cs      # managed-state per (componenttype, objectid)
    ComponentActiveLayerResolver.cs       # active-layer per (componenttype, objectid)
    CopyComponentsContext.cs              # fetch + orchestrate + decide
  CopyComponentsCommand.cs                # validate target/source, render plan, execute

tests/dgt.power.solution.tests/CopyComponentsCommandTests.cs
```

`CommandTree.cs`: registered under the existing `solution` branch as `copy-components`.

## Gotcha: `SolutionComponent.ComponentType`/`RootComponentBehavior` are `OptionSetValue?`, not `int?`

`OptionSetValue` is a *class* (reference type) - the `?` is a nullable-*reference* annotation, not
`System.Nullable<T>`. Consequences that cost a full failed build to discover:

- `component.ComponentType.HasValue` does **not** compile (`HasValue` is `Nullable<T>`-only).
  Use `component.ComponentType != null` or `component.ComponentType?.Value != null`.
- `component.ComponentType is not { } type` binds `type` as the `OptionSetValue` itself (the `{}`
  property pattern captures the matched value's own type), **not** its `.Value` int. Every later
  comparison against an `int` constant (`SolutionComponent.Options.ComponentType.Entity`, etc.)
  then fails with CS0019/CS1503.
- Correct pattern: `component.ComponentType?.Value is not { } type` (chain through `.Value` via `?.`
  *before* pattern-matching) - this is what `EntityComponentMembershipResolver` already did
  correctly; the bug was only introduced in the new copy-components code, now fixed to match.

`SolutionComponentDefinition.SolutionComponentType` (a different table) is a genuine `int?` -
`.HasValue` works fine there. Don't conflate the two when writing similar resolver code later.

## Test data gotcha: several early-bound entities are getter-only

`SolutionComponent`, `SolutionComponentDefinition`, and `Solution.IsManaged` expose only getters
(`GetAttributeValue` with no setter) in this repo's generated `dgt.power.dataverse` wrappers, even
though the base `Microsoft.Xrm.Sdk.Entity` always allows attribute-indexer writes regardless. Test
data must use `new SolutionComponent(id) { [SolutionComponent.LogicalNames.ComponentType] = new
OptionSetValue(...), ... }` (indexer), not the typed property setter, for those specific
getter-only properties. `Solution.UniqueName`/`Version` *do* have normal setters - it's a
per-property split in the generation config, not a whole-entity rule; check each property
individually rather than assuming.
