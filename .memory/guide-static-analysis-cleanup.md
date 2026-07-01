# Guide: Static Analysis Cleanup Approach

How we systematically cleaned up Roslyn CA and SonarQube S-rule warnings in this codebase.

## Approach

1. Add `Microsoft.Extensions.StaticAnalysis` to every project (`src/` + `tests/`).
2. Work category by category — one logical group per commit.
3. **Suppress only where a fix would change observable behavior or require disproportionate refactoring** — always add a comment explaining why.
4. Breaking changes get `fix!` prefix.

## What Was Fixed

| Category | Action |
|----------|--------|
| `S3902` — `Assembly.GetExecutingAssembly()` | Replaced with `typeof(ContainingClass).Assembly` everywhere |
| CA1850 — deadlock warnings | Console app has no SynchronizationContext; real bug was `.Wait()+.Result` in `UpdateWorkflowState.cs` → fixed to `.GetAwaiter().GetResult()` |
| CA1000 — sealed class | Added `sealed` to `Assembly`, `PluginStep`, `PluginStepImage`, `PluginType` in push module |
| CA1868 — `.Any()` on native collections | Fixed 3 occurrences using `Array.Exists()` / `List.Exists()` |
| CA2227 — collection properties should be read-only | Changed 14 properties from `set` to `init`; 3 skipped (must stay `set` due to post-construction assignment) |
| CA2213 / CA1816 — IDisposable / GC.SuppressFinalize | Fixed `ProfileTestsBase.cs`: `_storage.Dispose()` + `GC.SuppressFinalize(this)` |
| CA1031 — catch general exception | Category A (wrap-rethrow) + C (rollback-inner) → `#pragma`; Category B (swallow-log) → `when (e is not OutOfMemoryException and not StackOverflowException)` |
| CA1002 — do not expose generic `List<T>` | Delete 6 wrapper classes (`SlaConfigs`, `Calendars`, `TeamTemplates`, `UserRoles`, `RoutingRuleConfigs`, `AutoNumberFormats`, `Carriers`); return types → `IReadOnlyList<T>`; Liquid ViewModel internal records → file-level `#pragma`; push models with `.Add()` callers → `#pragma`; `BulkDeletes.Deletes` and `Calendar.Rules` → `#pragma` (`.Add()` in export); `DuplicateRules.Rules`, `DuplicateRule.DuplicateRuleConditions`, `DocumentTemplates.Templates` → `IReadOnlyList<T>` (only iterated) |
| CA1819 — array properties should not be returned | Codegeneration config/filter properties now expose `IReadOnlyCollection<string>` or the existing `ICollection<string>` backed by `HashSet<string>` directly; callers use `.Count` instead of `.Length` to avoid per-access allocations |
| CA2227 — mutable collection properties | `CodeGenerationConfig.Entities` / `.Forms` suppressed with `#pragma` — assigned post-construction; see suppression policy below |
| CA1827 — use `TrueForAll` / `Exists` on `List<T>` | `AssemblyProcessor.cs`: 8 `.All()` → `.TrueForAll()` on `List<T>` push model properties; `QueueImport.cs`: 1 `.All()` → `.TrueForAll()` where collection is `List<dataverse.Queue>` |
| Where-before-OrderBy | `DotNetEntityViewModelBuilder.cs`: `.OrderBy().Where()` → `.Where().OrderBy()` (filter before sort) |
| CA1826 — use property/indexer instead of Linq | `AssemblyModelBuilder.cs`: `assemblies.First()` → `assemblies[0]` when list has already been guard-checked for empty |
| Identity key normalization | `Identities.Upsert/Remove/SetCurrent/Contains` now normalize keys to `ToUpperInvariant()` internally; callers no longer need to normalize — removed from all 3 profile commands (`CreateProfileCommand`, `DeleteProfileCommand`, `SelectProfileCommand`). Fixed pre-existing `ShouldDeleteProfile` test failure. |
| `S3971` / CA1816 — `GC.SuppressFinalize` without finalizer | Removed the call; only meaningful with a `~Destructor()` |
| `S2930` — IDisposable not disposed | Added `using var` declarations |
| `S3900` — missing null argument checks | Added `ArgumentNullException.ThrowIfNull` to public methods |
| `S4261` — async methods without `Async` suffix | Renamed all async methods in `src/`; tests exempted via `tests/.editorconfig` |
| CA1307 — StringComparison missing | Added explicit `StringComparison.Ordinal` / `OrdinalIgnoreCase` |
| CA1308 — ToLower → ToUpper for normalisation | Suppressed (ToLower used for display, not comparison) |
| CA2007 — ConfigureAwait missing | Suppressed (app-level code, not library) |
| SYSLIB0014 — ServicePointManager | Removed the dead CLI options; see `decision-remove-insecure-protocol.md` |

## Suppression Policy

- CA1308, CA2007: suppressed globally via `GlobalSuppressions.cs` — not applicable in app-level code
- `FormXmlControlData.ControlId` in `GetHashCode`: ReSharper suppression with comment — mutation occurs before collection insertion, bounds are clear
- S4261 in `tests/`: suppressed via `tests/.editorconfig` — test method names are the display name, `Async` suffix adds noise
- CA1002 on `[DataMember]` push models: `WorkflowTypes`, `PluginTypes`, `PluginSteps`, `PluginStepImages` are populated via `.Add()/.AddRange()` in `AssemblyModelBuilder` → `#pragma`; `Solutions` has no `.Add()` callers → changed to `IReadOnlyList<string>`
- CA1002 on `TabDetail.Sections`: populated via `.Add()` in `FormParser` → `#pragma`
- CA2227 exceptions: `Identities.IdentityStore` (replaced post-construction), `AttributeMetadataViewModel.TargetEntityNames/Options` (set in `ToViewModel()`), `CodeGenerationConfig.Entities/Forms` (set post-construction) → must keep `set`
- CA1819 in config/filter objects: prefer `IReadOnlyCollection<string>` backed by `HashSet<string>` over `string[]` getters that call `.ToArray()` or `.ToHashSet()` on every access

## Notable Patterns

### Interface downcast anti-pattern
Adding missing members to an interface eliminates the need to downcast `IFoo → Foo` in consumers. Do this before adding ReSharper suppressions.

### GetHashCode on mutable state
Two options: suppress with a comment explaining mutation bounds, or convert to `record class` with `init`-only properties. See `decision-package-record-refactor.md` for when each applies.

### Nullable narrowing in LINQ
`x as string` inside `.Select()` yields `string?`. If the collection property promises `string[]`, filter with `.OfType<string>()` to eliminate `null` elements and narrow the type.

### Co-variant array conversion
`string[]` cast to `object[]` in params is dangerous — writing back through `object[]` reference can throw `ArrayTypeMismatchException`. Fix: `Array.ConvertAll(arr, static s => (object)s)`.
