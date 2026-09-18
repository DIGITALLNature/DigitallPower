# Implementation: entity component membership (IncludeSubcomponents resolution)

Fixes a correctness gap found after Phase 2: entities added to a solution with
`rootcomponentbehavior = IncludeSubcomponents` (the "whole table" case, e.g. adding a table via the
maker portal without cherry-picking fields) never get individual `solutioncomponent` rows for their
attributes. Any rule that filtered raw `solutioncomponent` rows by `componenttype = Attribute` (as
`UnmanagedFieldNamingRule` used to) silently skipped every field on such a table.

## Model

- `SolutionComponent.Options.RootComponentBehavior` (generated, `src/dgt.power.common/DotNet/SolutionComponent.cs`)
  already has all three constants (`IncludeSubcomponents = 0`, `DoNotIncludeSubcomponents = 1`,
  `IncludeAsShellOnly = 2`) - use these directly, do not redeclare them. (A first pass of this work
  added a local duplicate because a grep for the wrong substrings missed `IncludeAsShellOnly`; fixed.)
- `Base/EntityComponentMembership.cs` - per (solution, entity) view: `RootComponentBehavior`,
  `IsTableManaged` (from `EntityMetadata.IsManaged` - **table-level**, not solution-level; the
  linter only ever lints unmanaged solutions, so solution-level `IsManaged` is not modeled),
  `ExplicitSubcomponentsByType` (generic - any child solutioncomponent row grouped by componenttype,
  works for future component types without changes), and `EffectiveAttributes` (today's concrete
  resolution: all attributes when `IncludeSubcomponents`, only explicit ones otherwise, none for
  `IncludeAsShellOnly`).
- `Base/EntityComponentMembershipResolver.cs` - pure static resolver, no Dataverse calls of its own;
  takes the data `LintContext` already has (`SolutionComponentEntries`, `SolutionUniqueNamesById`,
  `EntityMetadata`, `AttributeMetadataById`) and builds the membership dictionary keyed by
  `EntityComponentMembershipResolver.BuildKey(solutionUniqueName, entityLogicalName)`.

## `LintContext` changes

- `BuildSolutionComponentEntries`'s `ColumnSet` now also fetches `rootcomponentbehavior` and
  `rootsolutioncomponentid` (previously missing entirely).
- The solution-id → unique-name map built while resolving component scope is now kept and exposed
  as `SolutionUniqueNamesById` (previously discarded after use) - this incidentally also fixed the
  Phase-1/2 caveat about `SolutionUniqueName` being unreliable (`EntityReference.Name` is not
  populated in the fake test environment; rules should never read `component.SolutionId?.Name`).
- New `EntityMemberships` property, built eagerly in the constructor (consistent with the rest of
  `LintContext`'s eager-fetch style).

## Rule changes

`UnmanagedFieldNamingRule.EvaluateAsync` now iterates `context.EntityMemberships.Values` and their
`EffectiveAttributes` instead of raw `SolutionComponentEntries` filtered by componenttype. Findings
now carry `membership.SolutionUniqueName` (reliable) instead of `component.SolutionId?.Name`
(unreliable) and `attribute.MetadataId` instead of `component.ObjectId` (same value in practice, but
sourced from metadata directly since implicitly-included attributes have no solutioncomponent row
to take an `ObjectId` from).

## Design decisions for future rules (per user confirmation, 2026-09-18)

- **Table completeness / delta rules are keyed off the table's `IsManaged`, not the solution's.**
  The linter only ever lints unmanaged solutions. For a table that is itself unmanaged (a
  first-party custom table), its component in the solution must have
  `RootComponentBehavior = IncludeSubcomponents` (0) - anything else is a finding. For a table that
  is itself managed (e.g. ISV-owned), its component must be `DoNotIncludeSubcomponents` (1) or
  `IncludeAsShellOnly` (2) - `IncludeSubcomponents` (0) on a managed table is a finding (you'd be
  attempting to own/re-export a table you don't control). **Implemented** as
  `Rules/TableRootComponentBehaviorRule.cs` (id `completeness.table-root-component-behavior`) -
  no config options, just `enabled`/`severity`. Core check: a membership is valid iff
  `membership.IsTableManaged != (membership.RootComponentBehavior == IncludeSubcomponents)` (get
  this comparison direction right - it was inverted in the first draft and silently flagged the
  exact opposite of every combination; caught by a test asserting the full expected violation set,
  not just "some findings exist").
- `ExplicitSubcomponentsByType` is intentionally generic (any componenttype, not just Attribute) so
  future rules (forms, views, relationships, ...) reuse the same resolver without a redesign - only
  `EffectiveAttributes`-style "resolve implicit membership" helpers need to be added per type as
  those rules are built.

## Tests

- `EntityComponentMembershipResolverTests.cs` - resolver unit tests (pure, no fake org service
  needed): IncludeSubcomponents/DoNotIncludeSubcomponents/IncludeAsShellOnly resolution,
  `IsTableManaged` passthrough.
- `UnmanagedFieldNamingRuleTests.cs` - test data now includes a realistic Entity-type
  solutioncomponent row (previously only attribute rows existed, which is not a shape Dataverse
  actually produces). Added `EvaluateAsync_EntityAddedWithIncludeSubcomponents_...` and
  `EvaluateAsync_EntityIncludedAsShellOnly_...` regression tests for the exact bug this fixes.
- `TableRootComponentBehaviorRuleTests.cs` - one entity per (IsManaged x RootComponentBehavior)
  combination (6 total), asserts the exact set of flagged entities (not just "count > 0"), which is
  what caught the inverted condition above.

