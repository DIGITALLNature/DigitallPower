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

## Consequences

- Legacy V1 configs still work via automatic mapping (with deprecation warning).
- TypeScript workers still use legacy `CodeGenerationConfig` internally (deferred to TS refinement phase).
- `MetadataWorker` is kept but uses `DotNetCodeGenerationConfig` — will be removed once fully absorbed.

