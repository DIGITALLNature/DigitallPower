# Guide: Sonar Rules Applied in chore-analysis

A reference of all Sonar/Roslyn analyzer warnings that were fixed, with the pattern used to fix each.

---

## S3902 — Do not call Assembly.GetExecutingAssembly inside a library

**Pattern:** `Assembly.GetExecutingAssembly()` performs a runtime stack walk and can return the wrong assembly under inlining/AOT.

**Fix:** Replace with `typeof(ContainingClass).Assembly` — resolved at compile time.

**Files fixed:**
- `src/dgt.power/Telemetry/DgtpActivitySource.cs`
- `src/dgt.power/VersionCheckInterceptor.cs`
- `src/modules/dgt.power.codegeneration/Templates/LiquidTemplateEngine.cs`
- `src/modules/dgt.power.push/PushCommand.cs`
- `src/modules/dgt.power.push/Logic/AssemblyModelBuilder.cs`

---

## S3971 — GC.SuppressFinalize should not be called on sealed types without a finalizer

**Pattern:** `GC.SuppressFinalize(this)` is only meaningful when a class has a finalizer (`~Destructor()`). On sealed classes without one, it's a dead call.

**Fix:** Remove the call entirely.

**Files fixed:**
- `src/modules/dgt.power.push/Logic/AssemblyModelBuilder.cs`
- `src/modules/dgt.power.push/Logic/AssemblyProcessor.cs`

**Note:** `CommandTestsBase` and `ProfileTestsBase` (non-sealed, no finalizer) also have `GC.SuppressFinalize` — these still trigger S3971 and may need cleanup.

---

## S2930 — IDisposable objects must be disposed before losing scope

**Fix:** Use `using var` declarations for all `IDisposable` locals.

**Files fixed:**
- `src/dgt.power/VersionCheckInterceptor.cs` — `SourceCacheContext`
- `src/modules/dgt.power.codegeneration/Generators/Worker/TypescriptGeneratorWorker.cs` — `StreamReader`
- `src/modules/dgt.power.codegeneration/Services/MetadataService.cs` — `StringReader`, `XmlReader`
- `src/modules/dgt.power.push/PushCommand.cs` — `MetadataLoadContext`
- `tests/dgt.power.codegeneration.tests/Base/CodeGenerationTestsBase.cs` — `XmlTextReader` ×2
- `tests/dgt.power.tests/ConsoleInjectionTests.cs` — `TestConsole`

---

## S3900 — Argument validation

**Pattern:** Public methods taking reference-type parameters should validate them with `ArgumentNullException.ThrowIfNull`.

**Files fixed:**
- `src/modules/dgt.power.codegeneration/Extensions/LabelExtensions.cs` — `label` parameter
- `src/modules/dgt.power.maintenance/Logic/EnsureSdkStepStatus.cs` — `args` parameter

---

## S4261 — Async methods should be suffixed with Async

See `decision-async-suffix-s4261.md` for full details.
