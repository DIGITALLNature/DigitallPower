# Guide: dgt.power.linter rule implementation pitfalls (found in Phase 1 review)

Lessons from reviewing the first `ILintRule` implementation (`UnmanagedFieldNamingRule`) and its
tests. Apply these whenever adding/reviewing a lint rule that resolves attribute/entity metadata.

## `RetrieveAllEntitiesRequest.EntityFilters.Entity` does NOT include `Attributes`

`EntityFilters` is a flags enum (`Entity = 1, Attributes = 2, Privileges = 4, Relationships = 8`).
Requesting `EntityFilters.Entity` only returns entity-level metadata — every `EntityMetadata.Attributes`
in the response is `null`/empty. Any code that builds an attribute lookup cache via
`entities.SelectMany(e => e.Attributes ?? [])` after such a request will always produce an **empty**
cache, silently. If you need attribute metadata, request
`EntityFilters.Entity | EntityFilters.Attributes` (or `EntityFilters.All`).

## Don't paper over a broken cache with a "fallback" live call

A cache-miss fallback that issues a *different* SDK request to "compensate" is only valid if that
request can actually succeed for the failure case. A `RetrieveAttributeRequest` fallback that reuses
the attribute's `MetadataId` (a GUID) as `LogicalName`, with a placeholder `EntityLogicalName`, can
never resolve — it always throws. Wrapping it in a broad `catch (Exception) { return null; }`
converts a load-bearing bug into silent, permanent no-op behavior. If a lookup should never
legitimately miss (e.g. metadata was just fetched for the whole org), don't add a "fallback" at all —
fix the cache-building step and let a genuine miss surface as an error or an explicit skip with a
visible diagnostic, not a swallowed exception.

## Lint rule unit tests must assert on findings, not just the command's exit code

`CommandTestContext.Execute(...)` returning `true`/`false` only tells you the command didn't fail
the configured severity gate — it does **not** tell you the rule under test actually inspected
anything. A rule with a completely broken metadata cache (see above) produces zero findings in every
test, and "no findings → exit code true" makes every test pass regardless of whether the rule works.
Tests for a rule must:
- Seed metadata via `.WithMetaData([...])` with fully populated `EntityMetadata.Attributes` (not just
  register loose `AttributeMetadata` instances that are never actually returned by the fake
  `RetrieveAllEntitiesRequest` executor).
- Assert on the actual finding collection (count, `RuleId`, `ComponentLogicalName`, `Severity`) for
  both the "should flag" and "should not flag" cases — not only on the command's overall success flag.

## Solution scoping must be keyed by unique-name lookup, not `EntityReference.Name`

A `SolutionComponent.SolutionId` is an `EntityReference` and its `Name` field is often empty in fake or
query-built Dataverse contexts unless the record is explicitly resolved. The linter must therefore scope
components by matching the `SolutionComponent.SolutionId.Id` against the set of `Solution.Id` values whose
`UniqueName` is in the selected solution list. Checking `component.SolutionId.Name` against the user-supplied
solution unique names silently drops all components and produces a false "no findings" result.

## Where things stand

`schemas/linter/schema.json` gives each rule its own closed `$defs/rules/<ruleId>` schema - register
a new rule's id under `properties.rules` and add a matching `$defs/rules/<id>` entry, never widen a
shared/global options shape. This is what makes "adding a rule can't regress an existing rule's
config" actually true.

`UnmanagedFieldNamingRule`'s options are just `publisherPrefixes` (default `["dgt_"]`). The earlier
`fieldTypeSuffixes` config option (a map keyed by raw .NET `AttributeMetadata` subclass names) was
removed - the type→suffix mapping is the published naming convention, not something orgs should
reconfigure, and leaking SDK class names into public config was a bad abstraction to begin with.

## `OrganizationServiceContext` LINQ provider does not support `Contains()` pushdown

`context.CreateQuery<T>().Where(x => someLocalHashSet.Contains(x.SomeField))` throws
`NotSupportedException: Invalid 'where' condition. An entity member is invoking an invalid property
or method.` at runtime (it compiles fine - this only surfaces when the query actually executes).
This SDK's LINQ-to-QueryExpression translator does not support translating a captured collection's
`.Contains()` call. Use a raw `QueryExpression` with `ConditionOperator.In` instead (see
`LintContext.BuildSolutionComponentEntries` and `BaseAnalyze.GetSolutionComponents` for the
established pattern) whenever you need to filter by "is one of these N values".

## Formula/Calculated/Rollup are all detectable via `AttributeMetadata.SourceType`

`SourceType` (`int?`) is documented at https://learn.microsoft.com/dotnet/api/microsoft.xrm.sdk.metadata.attributemetadata.sourcetype:
0 = Simple, 1 = Calculated, 2 = Rollup, 3 = Formula, 4 = Prompt. No separate `FormulaDefinition`
property is needed - the earlier assumption that Power Fx formula columns (`_fx`) weren't
detectable in this SDK was wrong; add named `const int` fields (mirroring `Calculated`/`Rollup`)
rather than magic numbers when switching on it.

## `[GeneratedRegex]` must decorate a partial *method*, not a partial property

`[GeneratedRegex(pattern)] private static partial Regex Foo { get; }` does not compile. Use
`[GeneratedRegex(pattern)] private static partial Regex Foo();` and call it as `Foo()`.

## Fields with no underscore in their logical name are out of our control

Dataverse (and first-party apps like Marketing) sometimes provisions default columns directly as
unmanaged/customizable attributes with zero publisher prefix (e.g. `name`, `createdon`, or an
auto-created `statecode`/`statuscode` on a brand-new custom table). Since every field we author
ourselves always has at least a `new_`-style prefix, a logical name with **no underscore at all**
is a reliable signal that we didn't create it - skip it rather than flagging a false positive.
