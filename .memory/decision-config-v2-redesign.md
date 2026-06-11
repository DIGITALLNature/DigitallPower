# Decision: V2 Code Generation Config Redesign

## Context

The original `CodeGenerationConfig` was a flat class with 30+ properties mixing DotNet-only, TypeScript-only, and shared concerns. It used opt-out `Suppress*` booleans (11 of them), a double-negative `NonDebuggerNonUserCode`, and forced users to carry irrelevant properties for their output target.

In practice, DotNet and TypeScript configs are always separate — different entities, different solutions, different use cases (plugins vs. web resources).

## Decision

Replace the monolithic `CodeGenerationConfig` with a typed hierarchy using JSON polymorphism.

### Config hierarchy

```
CodeGenerationConfigBase (abstract, shared properties)
├── DotNetCodeGenerationConfig
└── TypeScriptCodeGenerationConfig
```

### Key design choices

1. **`"type"` discriminator** in JSON (`"dotnet"` / `"typescript"`) drives deserialization via `System.Text.Json` polymorphism.
2. **Opt-in `Include` sections** replace 11 `Suppress*` booleans. Defaults are `true` — users only set `false` for things they don't want.
3. **`DotNetTarget` enum** (`Modern` / `Framework`) replaces `SuppressNullableSupport`. Controls nullable annotations and using directives.
4. **`Requests` array** unifies `Actions`, `CustomAPIs`, and `AdditionalSdkMessages`. The generator auto-detects each entry's type (custom API, classic action, built-in SDK message).
5. **`DebuggerNonUserCode`** removed entirely — no value for slim generated classes.
6. **Metadata generation** folded into `DotNetInclude.Metadata` (was a separate `MetadataWorker`).
7. **`Namespace`** moved to `DotNetCodeGenerationConfig` only — TypeScript doesn't use it.
8. **V1 backward compatibility** via `CodeGenerationConfigFactory` — detects absence of `"type"` field, deserializes as legacy `CodeGenerationConfig`, maps to V2 with deprecation warning.

### What was removed

| Removed | Replaced by |
|---------|------------|
| `SuppressDotNet/TypeScript/MetaData` | Config type + `DotNetInclude.Metadata` |
| 8× `Suppress*` section booleans | `Include` sub-objects |
| `NonDebuggerNonUserCode` | Deleted |
| `SuppressNullableSupport` | `DotNetTarget` enum |
| `UseBaseLanguage` | `Language` (0 or LCID) |
| `Virtual` | `VirtualProperties` |
| `Actions` + `CustomAPIs` + `AdditionalSdkMessages` | `Requests` |
| `SdkMessageFilters` | Deleted (opt-in makes filters unnecessary) |

## Alternatives considered

- **Single config with `output` array** — rejected because DotNet and TypeScript always use different entity sets.
- **Single config with `output` discriminated object** — rejected for same reason.
- **Keep flat config, just rename** — rejected because it doesn't solve the irrelevant-properties problem.

### TypeScript V2 config refinements

After the initial V2 redesign (which focused on DotNet), the TypeScript config was refined:

1. **Full generator mode removed from V2** — `TypeScriptCodeGenerationConfig` only targets the Light generator. Full mode is V1-only and deprecated.
2. **Language property made nullable (`int?`)** — `null` means "use organization base language" (deterministic across users). `0` means user language. Any other LCID selects that specific language.

## Generator architecture

### Symmetrical public API

The command layer injects two generator interfaces:

```
CodeGenerationCommand
├── IDotNetGenerator       → Generate(verb, DotNetCodeGenerationConfig)
└── ITypeScriptGenerator   → Generate(verb, TypeScriptCodeGenerationConfig)   [V2]
                           → Generate(verb, CodeGenerationConfig)             [V1, obsolete]
```

Both interfaces expose only `Generate()` — all generation steps are private implementation details.

### TypeScript strategy pattern

`TypeScriptGenerator` (implements `ITypeScriptGenerator`) is a pure router that delegates to an `ITypescriptGenerationStrategy`:

```
ITypeScriptGenerator (public)
└── TypeScriptGenerator (router)
    ├── V2: always creates TypescriptLightGenerationStrategy
    └── V1: switch on TypescriptGeneratorVersion
        ├── Full → TypescriptFullGenerationStrategy
        └── Light → TypescriptLightGenerationStrategy

ITypescriptGenerationStrategy (internal)
├── TypescriptFullGenerationStrategy   — V1 Full mode (deprecated)
└── TypescriptLightGenerationStrategy  — V1 Light + V2 (has both Generate overloads)
    └── extends TypescriptGenerationStrategyBase (shared: PrepareDirectory, CreateFile, CopyTemplateFileContent)
```

### Folder structure

```
Generators/
├── Contracts/
│   ├── IDotNetGenerator.cs               — public, single Generate()
│   ├── IMetadataGenerator.cs             — public
│   ├── ITypeScriptGenerator.cs           — public, V2 + V1 (obsolete) Generate()
│   └── ITypescriptGenerationStrategy.cs  — internal, V1 Generate() only
├── DotNetGenerator.cs
├── MetadataGenerator.cs
├── TypeScriptGenerator.cs                — pure router, no generation logic
└── Strategy/
    ├── TypescriptGenerationStrategyBase.cs  — abstract (CreateFile, PrepareDirectory)
    ├── TypescriptFullGenerationStrategy.cs  — V1 Full mode
    └── TypescriptLightGenerationStrategy.cs — V1 Light + V2
```

## Worker layer elimination

The `Logic/` worker classes (`DotNetWorker`, `TypescriptWorker`, `MetadataWorker`) were removed entirely. They served as unnecessary intermediaries that re-read config from disk even though the `CodeGenerationCommand` had already parsed it.

### Before
```
CodeGenerationCommand → reads config → Worker → re-reads config → Generator
```

### After
```
CodeGenerationCommand → reads config once → Generator.Generate(verb, config)
```

### Changes
1. **Generator interfaces gained `Generate()` orchestration methods** — single entry points that call PrepareDirectory + all generation steps internally.
2. **`CodeGenerationCommand`** injects generators directly (`IDotNetGenerator`, `ITypeScriptGenerator`) — only two dependencies.
3. **Strategy pattern for TypeScript** — `TypeScriptGenerator` creates the appropriate `ITypescriptGenerationStrategy` (Full or Light) based on config, then delegates. No branching logic leaks outside.
4. **Deleted**: `DotNetWorker.cs`, `TypescriptWorker.cs`, `MetadataWorker.cs` from `Logic/`.
5. **Tests migrated** from `WorkerTestsBase<TWorker>` to a new `CodeGenerationTestsBase` with `CodeGenerationContextBuilder`/`CodeGenerationContext` that resolves generators from DI.

## Consequences

- Legacy V1 configs still work via automatic mapping (with deprecation warning).
- TypeScript Light generator fully supports V2 `TypeScriptCodeGenerationConfig` via `ITypeScriptGenerator.Generate(verb, tsConfig)`.
- TypeScript Full generator is V1-only; no V2 config path exists for it (intentional — Full mode is deprecated).
- All generation step methods are private — interfaces expose only `Generate()`.
- File names match type names (enforced convention in AGENTS.md).
