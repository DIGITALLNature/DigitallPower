# Design

## Context

Code generation is a single `codegeneration` (`cg`) command that takes a mandatory config file (`-c`), a target directory, and a `--folder` name. The config is a V2 polymorphic document discriminated by `type` (`dotnet` | `typescript`) with a shared base class (`namespace`, `language`, `entities`, `requests`, `optionSets`). Entity selection resolves names, solutions, and a wildcard mask additively and writes the result back into the config object. TypeScript clears its whole subtree on each run; .NET deletes known generated files. Forms are generated for every selected entity and narrowed by a filter list of identifiers derived from generated filenames.

See proposal.md - Why for motivation. This document is a design checkpoint, not an implementation-ready design. Confirmed decisions are separated from outstanding decisions below; illustrative configuration examples discussed during exploration do not establish property names or schemas.

## Goals / Non-Goals

**Goals:**
- A config file that is the shared, authoritative recipe for what is generated and how.
- A resource-oriented CLI rooted at `model`.
- Targets that evolve independently, without a shared option shape.
- Predictable generation with reference validation before output changes.

**Non-Goals:**
- Config-free generation or a parallel CLI contract for generation settings. Individual property placement remains reviewable.
- Multiple targets in one config.
- Requiring the new generation contract to preserve the V2 format. The legacy implementation remains available during migration.

## Decisions

### Resource root is `model`
`model generate` is the shared command for both targets. The validated config identifies the generator; there are no target subcommands or duplicate CLI target selector. Require explicit config and output paths, with no conventional config filename or destination defaults. Exact flag names and aliases remain to be finalized.

*Alternatives:* `generate models` (keeps every resource a Dataverse object, but breaks resource-first grammar); `types`, `sdk`, `codegen`, `metadata` (rejected as too narrow, too broad, mechanism-named, or naming the input rather than the output).

### New project with parallel legacy support
The new generation contract is not constrained to preserve existing commands, V2 configuration, or output compatibility. This does not require changing every setting or output, nor does it require immediately removing the old implementation. User-affecting breaking changes are documented in the migration guide.

Implement the new generator in `src/modules/dgt.power.modelgeneration`, with tests in `tests/dgt.power.modelgeneration.tests`. Retain `dgt.power.codegeneration` and its `codegeneration` / `cg` commands during a transition period. Do not redirect old commands to incompatible new behavior.

Reuse the existing `DeprecationInterceptor` and `DeprecatedCommandAttribute` mechanism to direct legacy users to `model generate`; no new interceptor is needed. Transition duration, legacy maintenance scope, and removal criteria remain open.

Design the new module from scratch without constraining its contracts or architecture to the legacy implementation. Sharing logic or classes is allowed where it genuinely makes sense and does not compromise the new module. Neither sharing nor a common generation layer is required.

### Config is the contract; the CLI owns the destination
The config defines what is generated and how. The CLI supplies connection, config path, output location, and execution controls. Individual settings may later move to CLI parameters, but no override mechanism or config-free mode is introduced.

### One target per config; independent shapes
Each config describes one target. .NET and TypeScript contracts can evolve independently in structure, options, defaults, validation, and schema versions. Shared meaning should use consistent terminology, but shared shape is not required. The current unused TypeScript `namespace` property illustrates why target contracts should not be forced together. A minimal shared envelope and internal implementation reuse remain design choices, not requirements for a shared configuration base class.

### Explicit, independent artifact sections
Artifact categories are opt-in: a section requests that artifact and omission means it is not generated. Categories can be selected independently (for example, TypeScript entities without forms). Required supporting declarations are dependencies of a requested artifact, not separate options. Defaults keep sections concise; there are no switches for every member.

### Entity selection: include/exclude
Include/exclude selection is the agreed direction. The proposed algebra is the union of includes minus the union of excludes, with exclusion winning and no order dependence. Exact selector syntax, wildcard behavior, and diagnostics still need finalization.

Selecting a table always generates its full model definition, regardless of whether a solution includes the whole table or only selected components. Component-level solution scoping applies to components such as forms, not to partial entity models.

### Shared default scope, replaceable per section
An optional shared scope is the default for artifact sections. A section may declare its own selection, which replaces the default entirely. There is no narrowing or merging, so artifact independence is preserved.

### Resolve before writing
All explicit references (tables, solutions, operations, choices, and so on) are resolved and checked before any output is produced or any directory is cleared. Unknown explicit references fail rather than silently disappearing. Wildcard no-match behavior and implementation details of resolution remain open.

Reject unknown/additional config properties at every level, both in JSON schemas and runtime loading. Support the `$schema` annotation explicitly; its presence provides editor assistance but is not itself runtime validation.

### Owned output directory
The CLI supplies the destination. The tool owns that output directory and clears it on regeneration; no manifest, ownership marker, or mandatory overwrite guard is planned. Recipes must not share a destination if their outputs need to coexist. Help and migration guidance must explain directory ownership.

The supplied output directory is the generation root. Do not add `Model`, `DotNet`, or `TypeScript` wrapper directories; remove the separate `--folder` concept. Internal artifact subfolders remain a layout decision. Protection against catastrophic paths remains to be designed. A possible `--confirm` parameter was raised for later consideration, not agreed as part of this checkpoint.

After config validation and reference resolution, clear the output and render/write directly. Do not introduce staging, backup, or rollback. Rendering errors, I/O failures, or cancellation after clearing may leave incomplete output; report failure explicitly. Recovery is to fix the cause and regenerate. This risk is accepted for reproducible, tool-owned artifacts; Git tracking is not assumed.

### Form selection is broad within table or solution scope
Generate all supported forms belonging to selected tables, or included in selected solutions. A solution selector scopes form components, rather than expanding to every form of each referenced table. Combined with table selection, the table selector intentionally requests all supported forms for those tables.

Do not carry forward individual form-name filters, filename-derived identifiers, or individual form-ID lists. Extra declarations within the selected scope are an accepted trade-off for a straightforward shared recipe. Form selection remains independent of requesting entity-model output. The existing active Main, Quick Create, and Quick View set is the baseline to review, not a settled future support matrix.

Selection and generated naming are separate. Removing name-based selection does not solve localized or colliding generated identifiers.

### TypeScript output uses exported modules
Target bundler-based TypeScript projects with exported modules and explicit imports, without a parallel ambient-output mode. Type-only imports have no runtime output; required runtime values must have real implementations rather than exist only in declaration files.

Existing `XrmForm`, `XrmTable`, and related namespace names are not carried forward by default. Export names, grouping, and module paths remain open. Dataverse's standard `Xrm` API remains the platform API; generated modules do not replace it.

### .NET operation and message selection is explicit
Use one selection for custom operations and platform SDK messages, without an automatically appended built-in list:

| Selected item | Generated output |
|---|---|
| Custom API | Typed request/response classes and message-name constant |
| Classic Action explicitly selected by name | Unsupported-operation error before output changes; no contracts or constants |
| Platform SDK message | Message-name constant only |
| Unknown explicit name | Validation error before output changes |

Reuse SDK request/response types. Do not generate platform wrappers, including for platform messages lacking an SDK class. A platform message's existence alone does not establish a reliable parameter contract. Explicitly listing desired platform messages is the accepted trade-off; no generated catalogue or plugin-registration eligibility boundary is required.

Typed custom-operation generation supports Custom APIs only. Classify explicitly selected classic Actions using identity metadata as needed, but do not retrieve or parse workflow XAML. Solution-based Custom API discovery excludes classic Actions by definition, without warnings or failure merely because the solution contains them. The new generator's removal does not change legacy Action generation.

Reference: [#181 - Hardcoded default SDK message list](https://github.com/DIGITALLNature/DigitallPower/issues/181). The issue covers both generator defaults and public constants shipped in `dgt.power.common`. Explicit selection is the decision for the new generator, not authorization to remove those library constants or change a retained legacy generator. Reconcile the issue's remaining scope and compatibility concerns when defining migration.

### TypeScript Custom APIs provide directly executable requests
For each selected Custom API, generate:

- A parameter interface with required and optional inputs.
- A request class accepting that object, exposing request properties, and implementing `getMetadata()` for direct use with `Xrm.WebApi.online.execute`.
- A response-body interface where applicable. The caller parses and casts the response; no inference or execution wrapper is promised.

Parameterless APIs support construction without arguments. Do not generate separate TypeScript message constants, request factories, executors, transport/authentication abstractions, or a replacement Xrm type hierarchy.

Generated artifacts must work with standard Xrm typings. Reuse those types where suitable; narrowly scoped support types (for example for entity references) may be needed. Their shape and precise parameter mapping remain to be designed.

### Required choices are automatic dependencies
Include choices required by requested artifacts without a duplicate selection list. An independent choice section selects additional global choices or supports choices-only generation. Representation and deduplication remain open; agreed language fallback and member-collision rules are described below.

### .NET choice definitions favor enums; accessor types remain open
Prefer enums over integer-constant containers for generated .NET choice definitions. Do not infer from that preference that entity properties must expose enums. Evaluate SDK query translation, serialization, null and unknown numeric values, and multi-select collection behavior before deciding property types. No dual raw/typed property surface or configuration switch is agreed.

### Generation language and choice naming
Default to the organization's base language, with an explicit config override. Never select labels based on the connecting user's language. If the selected language has no usable choice label, use the base-language label without a warning. If neither provides a usable label, fail before output changes; never omit the numeric value.

Disambiguate choice labels that normalize to the same member name deterministically using their numeric values, for every member of the colliding group. Do not depend on metadata encounter order or require users to rename managed/platform choices. Exact suffix formatting remains open. Label/translation changes, or a newly introduced collision, may rename generated members; this trade-off is accepted.

### Required logical .NET consumer profile
Require a config target expressing consumer intent rather than framework or language version. Exact names and config shape remain open; `plugin` and `client` are working names only.

| Consumer intent | Generated-code compatibility baseline |
|---|---|
| Dataverse plugin | .NET Framework net462-net481, default C# 7.3; framework API compatibility with net462 |
| External client, for example ServiceClient | .NET 10, default C# 14 |

The profile controls available syntax and framework APIs only, not model behavior or special plugin/client logic. Do not require a `LangVersion` override. Nullable value types work in both profiles; nullable-reference annotations are emitted only for the modern profile.

### Thin SDK-compatible .NET entities
Preserve partial-record semantics: a column can be absent or null regardless of business-required metadata. Do not infer C# required members or constructor validation from that metadata. Keep SDK-compatible ID behavior.

Follow metadata read/write capabilities. Read-only columns have getter-only properties; remove the force-setter (`editableReadOnly`) option. A setter is allowed where metadata permits create or update, without attempting to infer the intended operation at runtime. Preserve testability by allowing tests to populate underlying SDK attributes and read them through generated accessors.

Generate ordinary non-virtual properties and remove the virtual-property option. Classes remain partial. Include useful table/column logical names, relationship schema names, and alternate-key identifiers alongside entities without individual inclusion switches. These identifiers do not themselves implement alternate-key operations.

Do not generate entity type-code constants: custom-table numeric codes are environment-specific. They may remain in an exported metadata snapshot.

### Typed SDK relationships without model expansion
Lookups remain standard `EntityReference` values, including references to unselected tables. Generate thin typed navigation properties for 1:N, N:1, and N:N relationships only where the related table is also selected. Relationship-name constants can describe unselected tables without generating their models.

Use SDK related-entity accessors for typed single-record and collection access. These properties read or attach related data, not retrieve it. No lazy loading, lookup synchronization, custom tracked collections, or implicit association operations. Collection assignment is typed; it does not promise in-place mutation through a custom collection.

### No generated service context
Drop generated `OrganizationServiceContext`, typed query properties, and context-specific `NoLock` behavior. Do not add a repository/query abstraction. Consumers can still use the standard SDK context themselves; generated convenience should not encourage reliance on its limited LINQ provider.

### Metadata snapshots are an independent artifact
Retain XML metadata export as an opt-in artifact independent of C# entity output, using the same include/exclude semantics. Its purpose is an exported table-metadata snapshot usable to populate metadata in the consuming test library, including relationships.

Keep it within the model-generation module for this scope. Do not introduce metadata re-import, offline generation, caching, or a separate metadata module. Serialization details and compatibility with the consuming test library must be resolved before implementation.

### Required .NET namespace and schema-derived names
Require an explicit root namespace in the .NET config, without vendor defaults or inference from solution names. Artifact subnamespace organization remains open.

Derive entity class and column property names from schema names, not localized display labels. Preserve publisher prefixes. Apply a simple, deterministic best-effort PascalCase conversion: split underscore-separated segments, discard empty segments, uppercase each segment's first character invariantly, and preserve the remaining capitalization. Do not guess missing word boundaries or normalize acronyms through a dictionary; no Humanizer dependency is needed for this rule.

| Schema name | Generated identifier |
|---|---|
| `contoso_order` | `ContosoOrder` |
| `contoso_order_number` | `ContosoOrderNumber` |
| `contoso_OrderNumber` | `ContosoOrderNumber` |
| `contoso_orderNumber` | `ContosoOrderNumber` |
| `contoso_order_ID` | `ContosoOrderID` |
| `contoso_order_id` | `ContosoOrderId` |
| `contoso_ordernumber` | `ContosoOrdernumber` |

SDK mapping attributes and constants retain original technical names. Valid-identifier handling, keyword escaping, and normalized collisions between classes/properties/files remain separate design work; do not resolve collisions by metadata encounter order. The enum-label collision rule does not automatically apply to entity names.

## Risks / Trade-offs

- [Clearing a mis-specified destination deletes user files] -> Document directory ownership and resolve references before clearing; path safeguards and optional confirmation still need discussion.
- [Independent target shapes duplicate concepts] -> Share terminology where meaning is shared; share logic internally rather than the config shape.
- [New contracts require consumer migration] -> Retain deprecated legacy commands during transition and provide before/after migration guidance.
- [Parallel implementations increase maintenance cost] -> Define legacy maintenance scope and removal criteria; do not silently route old commands to incompatible new behavior.
- [Broad form scope increases generation work and editor/file churn] -> Solution-based component selection keeps project scope manageable; avoid fragile per-form filters.
- [Module output breaks consumers of generated globals] -> Document import migration, request construction, runtime values, and test-helper usage; validate against representative bundled consumers.
- [Localization and duplicate form names destabilize generated APIs] -> Resolve naming separately before finalizing the output contract.
- [Failure after clearing leaves incomplete output] -> Accept this risk, fail explicitly, and regenerate after fixing the cause; no staging or rollback.
- [Label changes affect enum API names] -> Deterministic language fallback and collision handling; accept renaming when labels change.

## Outstanding Design Decisions

These are unresolved and can affect specs and tasks. They are blockers for completing the design, not details delegated to implementation:

- Define legacy maintenance scope, transition duration, and eventual removal criteria for parallel deprecated commands.
- Reconcile [#181](https://github.com/DIGITALLNature/DigitallPower/issues/181), including compatibility of separately shipped `dgt.power.common` message constants; their removal is not decided by the new generator's explicit-selection contract.
- TypeScript generated names, module organization, entity APIs, and stable form identifiers.
- .NET artifact/subnamespace layout, identifier validity/escaping, and collisions beyond enum members; remaining entity API details.
- Final .NET enum representation and SDK property types, TypeScript choice representation, dependency deduplication, and additional-choice selector details.
- Exact logical .NET profile names/config shape and compatibility verification.
- Metadata snapshot serialization compatibility with the consuming test library.
- Custom API binding and parameter mappings, optional/null handling, and any required support types.
- Final include/exclude selectors, wildcard diagnostics, and shared-default interpretation across artifact kinds.
- Supported form types and activation state; dependencies such as referenced Quick View forms.
- Exact CLI flags/aliases, internal artifact folders, output path safeguards, and whether optional confirmation adds value.
- Config envelope, version/schema numbering, and legacy-config diagnostics.
- Internal API and implementation boundaries within `dgt.power.modelgeneration`, including worthwhile unconstrained reuse.
