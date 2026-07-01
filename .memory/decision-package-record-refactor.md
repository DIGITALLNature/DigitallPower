# Decision: Convert Package to record class (Option A)

## Context

`Package` in `dgt.power.push/Model/Package.cs` is a domain model representing a NuGet plugin
package. It had mutable `set` properties (`Name`, `Version`, `Content`) included in `GetHashCode()`,
which is a structural hazard — hash instability when instances are stored in collections.

## Options Evaluated

### Option A — `record class` + `with` expression ✅ CHOSEN
Convert to `record class` with `init`-only properties. Post-construction mutations replaced with
`with` expressions (non-destructive copy).

### Option B — Factory method + `init` on identity fields
Keep `class`, mark `Name`/`Version` as `init`, add `WithUpdatedContent(...)` method. Pragmatic but
introduces a non-standard pattern.

### Option C — Separate `PackageConfig` DTO + `Package` wrapper
Full role separation. Over-engineered for this scope; breaks all consumers.

## Decision: Option A

**Rationale:**
- `Package` is **never deserialized** from JSON/DataContractSerializer — it is always constructed
  in code (`BuildPackageFromFile`, `BuildPackageFromCrm`, `CreatePluginPackage`, `UpdatePluginPackage`).
  So no serializer compatibility risk with `init` properties.
- `record class` is the idiomatic C# solution for immutable-after-construction value objects.
- `with` expressions make mutations explicit and auditable.
- Equality overrides (`Equals(Package?)`, `GetHashCode()`) remain explicit to scope equality to
  `Name + Version + Content` — excluding runtime-state fields (`Id`, `State`, `Solutions`).
- `IEquatable<Package>` declaration dropped — `record class` auto-implements it.

## Key Implementation Detail

`BuildPackageFromCrm` had an empty-constructor + post-mutation pattern. Refactored to a
conditional object initializer — the `AssemblyState.Create` branch now correctly uses the input
`name`/`version` parameters (previously left as `null!`). This was a subtle improvement beyond
the stated goal.

## Files Changed

- `src/modules/dgt.power.push/Model/Package.cs` — record class
- `src/modules/dgt.power.push/Logic/AssemblyModelBuilder.cs` — conditional init in BuildPackageFromCrm

