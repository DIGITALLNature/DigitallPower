# Guide: Code Quality Patterns — Lessons from chore-analysis Session

Findings from a thorough static analysis pass (Roslyn CA + SonarQube S-rules) in June 2026.

---

## Anti-Pattern: Interface-Downcast in DI

### Problem
```csharp
// Registers interface, immediately casts to concrete — DI theater
protected ProfileManager ProfileManager => (ProfileManager)_serviceProvider.GetRequiredService<IProfileManager>();
protected Identities GetIdentities() => (Identities)ProfileManager.LoadIdentities();
```
Breaks on any implementation swap with `InvalidCastException`.

### Root Cause
Interface does not expose all members that consumers need.

### Fix
Add missing members to the interface:
```csharp
public interface IIdentities {
    string Current { get; }
    string CurrentConnectionString { get; }
    // ...
}
```
Then resolve directly from the interface — no cast needed.

---

## Anti-Pattern: Mutable Properties in GetHashCode

### Problem
```csharp
public string Name { get; set; }  // mutable!
public override int GetHashCode() => HashCode.Combine(Name, Version); // unstable hash
```
If an instance is added to a `Dictionary`/`HashSet` and then mutated, it becomes unreachable.

### Fix Options (in priority order)
1. **`record class` with `init`** — idiomatic, compiler-enforced: `public string Name { get; init; }`
2. **`init` on identity fields only** — if some fields are legitimately mutable post-construction
3. **ReSharper suppress + comment** — only if mutation is bounded (before collection insertion) and refactor is disproportionate

### Note on record class + serializers
`DataContractSerializer` and `System.Text.Json` (in .NET 6+) support `init` properties via
reflection bypass. Before converting, verify whether the type is actually deserialized — if it is
only ever constructed in code, there is zero serializer risk.

---

## Anti-Pattern: Covariant Array Conversion

### Problem
```csharp
string[] solutions = config.Solutions;
service.AddCondition("field", ConditionOperator.In, solutions); // CS2010 covariant conversion
```
`string[]` implicitly cast to `object[]` — write access on the object[] could produce `ArrayTypeMismatchException`.

### Fix
```csharp
Array.ConvertAll(config.Solutions, static s => (object)s)
```

---

## Anti-Pattern: Stale XML Doc `<param>` tags

When a method signature changes (parameter added/renamed), stale `<param>` tags remain silently.
Compiler warning `CS1573`/`CS1572` fires but only if `<GenerateDocumentationFile>` is enabled.

### Fix
Always update the `<param>` block when changing method signatures. Missing params → add.
Extra params → remove. Parameter renamed → rename in doc.

---

## Anti-Pattern: Nullable strings in Spectre.Console

`Table.AddRow(...)` accepts `string[]` — passing `string?` causes potential `NullReferenceException`
in Spectre internals.

### Fix
Always null-coalesce optional Dataverse fields before passing to Spectre:
```csharp
table.AddRow(entity.Name ?? string.Empty, entity.Description ?? string.Empty)
```

---

## Anti-Pattern: ServiceProvider not disposed in test base classes

```csharp
public override void Dispose()
{
    _storage.Remove();
    _storage.Dispose();  // ← manual disposal of a singleton owned by ServiceProvider
    base.Dispose();
    GC.SuppressFinalize(this);  // ← pointless without a finalizer
}
```
The `ServiceProvider` owns its registered singletons — disposing them manually causes double-dispose.
`GC.SuppressFinalize(this)` without a finalizer is cargo-cult.

### Fix
```csharp
public override void Dispose()
{
    _serviceProvider.Dispose(); // disposes all owned singletons including _storage
    base.Dispose();
}
```

---

## Pattern: GetArrayValues — null element filtering

When using `x.Value as string` in a LINQ select, null results propagate to the array.
If the target property type is `string[]?` (non-nullable elements), use `OfType<string>()`:

```csharp
// Before (returns string?[]? — wrong type contract)
var result = valuesRaw?.Select(x => x.Value as string).ToArray();

// After (returns string[]? — filters out any non-string elements)
var result = valuesRaw?.Select(x => x.Value as string).OfType<string>().ToArray();
```
