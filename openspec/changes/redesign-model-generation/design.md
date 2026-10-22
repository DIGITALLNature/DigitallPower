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
`model generate` is the shared command for both targets. The validated config identifies the generator; there are no target subcommands or duplicate CLI target selector. Require named `--config` / `-c` and `--output` / `-o` options, with no positional arguments or conventional config filename/destination defaults. Existing connection options remain available.

*Alternatives:* `generate models` (keeps every resource a Dataverse object, but breaks resource-first grammar); `types`, `sdk`, `codegen`, `metadata` (rejected as too narrow, too broad, mechanism-named, or naming the input rather than the output).

### New project with parallel legacy support
The new generation contract is not constrained to preserve existing commands, V2 configuration, or output compatibility. This does not require changing every setting or output, nor does it require immediately removing the old implementation. User-affecting breaking changes are documented in the migration guide.

Implement the new generator in `src/modules/dgt.power.modelgeneration`, with tests in `tests/dgt.power.modelgeneration.tests`. Retain `dgt.power.codegeneration` and its `codegeneration` / `cg` commands during a transition period. Do not redirect old commands to incompatible new behavior.

Reuse the existing `DeprecationInterceptor` and `DeprecatedCommandAttribute` mechanism to direct legacy users to `model generate`; no new interceptor is needed. New features belong only in the new generator. Maintain the legacy generator for significant bugs and compatibility/security issues without redesigning its contracts. Consider removal in a future major release after the replacement is released, migration guidance is available, and migration experience has been evaluated; no release/date is promised.

Design the new module from scratch without constraining its contracts or architecture to the legacy implementation. Sharing logic or classes is allowed where it genuinely makes sense and does not compromise the new module. Neither sharing nor a common generation layer is required.

### Config is the contract; the CLI owns the destination
The config defines what is generated and how. The CLI supplies connection, config path, output location, and execution controls. Individual settings may later move to CLI parameters, but no override mechanism or config-free mode is introduced.

### One target per config; independent shapes
Each config describes one target. .NET and TypeScript contracts can evolve independently in structure, options, defaults, validation, and schema versions. Shared meaning should use consistent terminology, but shared shape is not required. The current unused TypeScript `namespace` property illustrates why target contracts should not be forced together. A minimal shared envelope and internal implementation reuse remain design choices, not requirements for a shared configuration base class.

### Explicit, independent artifact sections
Artifact categories are opt-in: a section requests that artifact and omission means it is not generated. Categories can be selected independently (for example, TypeScript entities without forms). Required supporting declarations are dependencies of a requested artifact, not separate options. Defaults keep sections concise; there are no switches for every member.

Each present section contains its own explicit selection. Do not provide a shared/global scope, empty-object opt-in, or Boolean shorthand. Repeated selection is accepted so a section can be understood without resolving inheritance or replacement rules. This supersedes the earlier shared-default-scope proposal.

Use flat artifact sections: .NET `entities`, `metadata`, `messages`, and `optionSets`; TypeScript `tables`, `forms`, `operations`, `choices`, and `bpfs`. The working selection shape uses `include` objects with `tables`/`solutions` or `names`/`solutions`, and `exclude` objects with the supported exact-name lists. Standalone global choices and BPFs use simple `include` name arrays. Complete recipe review and placement of optional test support remain open.

### New recipe schema family
Use required `type` and numeric `version` fields, starting the new independent recipe family at version 1. `type` selects `dotnet` or `typescript`; each target's versions evolve independently. Unknown versions fail explicitly, without silently treating them as the latest format.

Plan schemas at `schemas/modelgeneration/v1/dotnet.schema.json` and `schemas/modelgeneration/v1/typescript.schema.json`. Keep published legacy `schemas/codegeneration/v2/` URLs and legacy loading behavior unchanged; do not introduce codegeneration V3 merely to continue its numbering. Optional `$schema` provides editor assistance, not runtime target/version inference. Do not silently convert legacy configs in `model generate`.

### Minimal, artifact-specific selection
Selection is the union of includes minus the union of excludes, with exclusion winning, duplicate elimination, and no order dependence. Missing includes never imply select-all. Do not introduce a universal selector language, wildcards, publisher-prefix selection, or arbitrary predicates. Those mechanisms make the generated set harder to predict; explicit names and maintained solution membership cover the initial use cases.

| Artifact | Include sources | Exclude sources |
|---|---|---|
| Table models, either target | Table logical names; solution unique names | Table logical names |
| TypeScript forms | Table logical names; solution unique names, selecting form components | Table logical names, removing all selected forms for those tables |
| .NET XML snapshots | Same as table models | Table logical names |
| .NET operations/messages | Exact Custom API or platform-message names; solution Custom API components | Exact operation/message names |
| TypeScript operations | Exact Custom API or platform Web API action/function names; solution Custom API components | Exact operation names |
| Additional global choices | Exact global choice technical names | None |
| TypeScript BPFs | Exact workflow unique names | None |

Solution selection does not discover platform messages/operations or classic Actions. There are no solution-based exclusions. Selecting a solution does not transitively select tables merely referenced by its forms, relationships, or operations.

Selecting a table always generates its full model definition, regardless of whether a solution includes the whole table or only selected components. Component-level solution scoping applies to components such as forms, not to partial entity models.

### Resolve before writing
All explicit references (tables, solutions, operations, choices, and so on) are resolved and checked before any output is produced or any directory is cleared. Unknown explicit references fail rather than silently disappearing, including in exclusions. Excluding an existing item outside the included set is valid. An existing solution with no relevant components is not an unknown reference.

Warn when a requested artifact section resolves to nothing while other sections produce output. Fail before clearing if the entire recipe resolves to no artifacts: this is an invalid/no-op generation request, consistent with plugin push rejecting an entirely empty target, not an output-backup mechanism.

Reject unknown/additional config properties at every level, both in JSON schemas and runtime loading. Support the `$schema` annotation explicitly; its presence provides editor assistance but is not itself runtime validation.

### Owned output directory
The CLI supplies the destination. The tool owns that output directory and clears it on regeneration; no manifest, ownership marker, or mandatory overwrite guard is planned. Recipes must not share a destination if their outputs need to coexist. Help and migration guidance must explain directory ownership.

The supplied output directory is the generation root. Do not add `Model`, `DotNet`, or `TypeScript` wrapper directories; remove the separate `--folder` concept. Category folders are described below. Do not add a `--confirm` flag or interactive overwrite prompt.

Before clearing, reject a filesystem root, the current working directory or a lexical ancestor, and any destination containing the input recipe. Use normalized paths and platform-appropriate comparison. These are minimal best-effort safeguards, not a cross-platform guarantee against linked paths, mounts, aliases, or filesystem races. Do not add Git-root detection or a general output-safety framework. Exact deletion/link handling still requires implementation design. Clearly report the resolved output being cleared and document that it must be disposable.

After config validation and reference resolution, clear the output and render/write directly. Do not introduce staging, backup, or rollback. Rendering errors, I/O failures, or cancellation after clearing may leave incomplete output; report failure explicitly. Recovery is to fix the cause and regenerate. This risk is accepted for reproducible, tool-owned artifacts; Git tracking is not assumed.

### Form selection is broad within table or solution scope
Generate all supported forms belonging to selected tables, or included in selected solutions. A solution selector scopes form components, rather than expanding to every form of each referenced table. Combined with table selection, the table selector intentionally requests all supported forms for those tables.

Do not carry forward individual form-name filters, filename-derived identifiers, individual form-ID lists, or configurable form-type filters initially. Extra declarations within the selected scope are an accepted trade-off for a straightforward shared recipe. Form selection remains independent of requesting entity-model output.

Support active Main, Quick Create, and Quick View forms. Main and Quick Create modules export `FormContext` extending `Xrm.FormContext`; Quick View modules export `QuickFormControl` extending `Xrm.Controls.QuickFormControl`, not a fictional full form context.

Selection and generated naming are separate. Removing name-based selection does not solve localized or colliding generated identifiers.

### TypeScript output uses exported modules
Target bundler-based TypeScript projects with exported modules and explicit imports, without a parallel ambient-output mode. Type-only imports have no runtime output; required runtime values must have real implementations rather than exist only in declaration files.

Do not carry forward the generated `XrmForm`, `XrmTable`, and related ambient namespace hierarchy. Dataverse's standard `Xrm` API remains the platform API; generated modules do not replace it.

Use category folders and one module per table, form, choice definition, operation, or BPF: `tables`, `forms`, `choices`, `operations`, and `bpfs`. `operations` covers both Custom APIs and platform actions/functions. Organize forms by table and form type. Use direct imports initially, without generated barrel files; barrels remain a possible later convenience.

Form modules use simple exports such as `FormContext` or `QuickFormControl`. Consumers choose descriptive import aliases. Supporting form export details remain open.

### TypeScript tables describe Web API records
Generate one interface per selected table, combining read/write shapes initially. Do not generate separate read/create/update interfaces, a table-wide form context, hydration, or converters. Actual form contexts come only from selected forms.

Retain exact Web API property names and representations. Properties allow omission for partial projections; nullability follows the API rather than business-required metadata. Consumers remain responsible for sending properties appropriate to the operation.

| Value | Web API record non-null type | Form value non-null type |
|---|---|---|
| Text | `string` | `string` |
| Boolean | `boolean` | `boolean` |
| Ordinary integer, floating point, decimal, money | `number` | `number` where supported |
| GUID | `string` | Follow the applicable form API |
| Date/date-time | `string` | `Date` |
| Single choice/state/status | `number` | `number` |
| Multiple choices | Comma-separated `string` | `number[]` |

No parsing, formatting, or hydration helpers are introduced. Specialized 64-bit integer, binary/file/image, and operation-specific mappings are deferred for verification. Do not infer `bigint` from `Int64` metadata; typings must reflect the actual standard client's representation and document numeric precision limits where applicable.

Generate typed single/collection expanded navigation properties only where the related table is selected, using actual Web API navigation-property metadata. Do not expand table selection or introduce `any` for unselected targets. Lookup IDs and applicable bind properties remain usable independently of typed expansion.

Expose lookup IDs such as `_primarycontactid_value?: string | null`, bind properties such as `"primarycontactid@odata.bind"?: string | null`, and expanded records such as `primarycontactid?: Contact | null` separately. Use actual target-specific navigation names for polymorphic lookups. Do not invent a plain logical-name string lookup property, use `Xrm.LookupValue`, or generate bind-URL builders.

Include a grouped runtime metadata object alongside the interface, containing `logicalName`, `entitySetName`, `columns`, and `navigationProperties`. Read entity-set names from metadata, not pluralization. This is plain identifier data, not a query DSL. Whether to export a distinct camelCase metadata value (`contosoOrderMetadata`) or share the PascalCase interface name in the type/value spaces (`ContosoOrder`) is explicitly deferred; neither is required by TypeScript's runtime limitations.

### TypeScript naming distinguishes generated symbols from wire names
Use PascalCase for generated types/classes and camelCase for generated metadata constants and property keys. Derive table types and column identifier keys from schema names using the agreed best-effort conversion below, lowercasing the initial character for camelCase. Preserve publisher prefixes and existing internal capitalization.

Keep record properties and operation input/output properties exactly as required by the platform. For example, `columns.contosoOrderNumber` can contain `"contoso_ordernumber"`, but the record interface itself exposes `contoso_ordernumber`. Renaming wire properties would require converters and is outside this design.

### Form names are readable snapshots with explicit language checks
Derive form-module names from display names, scoped by table and form type. Do not rely on rarely populated form unique names. Accept display-name and localization changes affecting imports rather than use GUID-only names.

For every member of a colliding normalized form-name group, append a deterministic form-ID suffix; unique names remain unsuffixed. Do not choose a winner by retrieval order. Adding the first collision can rename the previously unsuffixed module. Exact suffix formatting remains open.

When forms are requested, fail before output changes if the connecting user's UI language differs from the effective generation language (configured override or organization base language). Do not modify user settings. Recipes without forms need no form-language check.

### Form accessors retain useful snapshot guarantees
Ordinary attributes and controls declared in FormXML have precise non-null accessors. Runtime-conditional elements, such as BPF-only attributes and controls, remain nullable. Attribute-value nullability is separate from attribute-object presence; business-required metadata does not imply a non-null value.

Preserve standard Xrm methods, overloads, and specialized event APIs. Standard string-based fallback access can mean that removing an element loses precise typing rather than necessarily causing a compile error. Do not promise strict rejection of every removed name.

Use local assertions such as `context.getFormContext() as AccountSalesFormContext`, with an imported alias for the generated `FormContext`. Do not generate event-context types, globally augment `getFormContext<T>()`, or add a runtime context factory. Assertions express the expected form; they do not validate its identity.

Embedded Quick Views use their generated type only when those forms are selected; otherwise retain `Xrm.Controls.QuickFormControl`. Do not automatically select referenced forms/tables or fail merely because the referenced Quick View was not selected. Preserve the platform's loading lifecycle.

### BPFs are separate typings with explicit form enrichment
Generate dedicated plain BPF typings for explicitly selected workflow unique names. No process controller, execution helper, or other runtime abstraction is generated. Exact BPF exports remain open.

Selected BPFs also enrich selected Main-form typings for their corresponding participating tables. Combine known attributes/controls across selected processes; BPF-only accessors remain nullable because the active process/stage varies. If an attribute also exists in FormXML, keep its ordinary non-null accessor; its BPF control remains nullable. Do not enrich Quick Create or Quick View forms.

Selecting a BPF does not select additional tables/forms. With no BPF selection, no BPF enrichment occurs. Consumers use standard client APIs to determine the active process and runtime availability.

### Form test support is opt-in and narrowly scoped
Retain optional test support for constructing form-shaped fixtures from generated form definitions using an established mock library. Ordinary form generation must not require testing dependencies.

Do not commit to carrying forward the legacy framework's Web API, navigation, server-data, and event-mocking layers. The initial purpose is avoiding hand-written form fixtures, not implementing another testing framework. Adapter/library choice and the exact helper API remain open.

### .NET operation and message selection is explicit
Use one selection for custom operations and platform SDK messages, without an automatically appended built-in list:

| Selected item | Generated output |
|---|---|
| Custom API | Typed request/response classes and message-name constant |
| Classic Action explicitly selected by name | Unsupported-operation error before output changes; no contracts or constants |
| Platform SDK message | Message-name constant only |
| Unknown explicit name | Validation error before output changes |

Reuse SDK request/response types. Do not generate platform wrappers, including for platform messages lacking an SDK class. A platform message's existence alone does not establish a reliable parameter contract. Explicitly listing desired platform messages is the accepted trade-off; no generated catalogue or plugin-registration eligibility boundary is required.

Typed .NET custom-operation generation supports Custom APIs only. Both targets reject explicitly selected classic Actions, even if Web API exposes them. Classify Actions using identity metadata as needed, but do not retrieve or parse workflow XAML. Solution-based Custom API discovery excludes classic Actions by definition, without warnings or failure merely because the solution contains them. The new generator's removal does not change legacy Action generation.

Reference: [#181 - Hardcoded default SDK message list](https://github.com/DIGITALLNature/DigitallPower/issues/181). The issue covers both generator defaults and public constants shipped in `dgt.power.common`. Explicit selection is the decision for the new generator, not authorization to remove those library constants or change a retained legacy generator. Reconcile the issue's remaining scope and compatibility concerns when defining migration.

### TypeScript operations provide directly executable requests
Support explicitly selected Custom APIs and platform Web API actions/functions, not just SDK message-name existence. Solution selection discovers Custom APIs only. Resolve platform operation contracts from the connected environment's CSDL `$metadata`: binding, parameters, return types, and required supporting definitions. An SDK message without an exposed Web API operation cannot produce a TypeScript request. For example, `RetrieveEnvironmentVariableValue` can produce a typed function request and response.

For each selected operation, generate:

- A `Parameters` interface with required and optional inputs, when inputs exist.
- A request class accepting that object, exposing request properties, and implementing `getMetadata()` for direct use with `Xrm.WebApi.online.execute`.
- A `Response` body interface where applicable. The caller parses and casts the response; no inference or execution wrapper is promised.

Each operation module exports `Request` and the applicable `Parameters`/`Response` types. Omit empty parameter/response interfaces. Parameterless operations support `new Request()`. Preserve omission of optional inputs; do not add defaults, coerce values, or replace omitted inputs with `null`. Optionality does not automatically permit explicit null.

For entity-bound Custom APIs, accept `entity: { entityType, id }` in the parameter object, constraining `entityType` to the bound table logical name. This is the platform request reference shape, not `Xrm.LookupValue`. The request supplies binding metadata. Collection-bound operations do not take an individual record target; exact collection-binding handling remains open.

Do not generate separate TypeScript message constants, request factories, executors, transport/authentication abstractions, or a replacement Xrm type hierarchy.

Generated artifacts must work with standard Xrm typings. Reuse those types where suitable. Automatically include supporting operation contract types, such as the complex `PrincipalAccess` parameter type, and reuse shared definitions across selected operations. These supporting types do not select full table models, forms, or other operations. Entity-valued properties require a faithful contract mapping, not automatic table generation. Fail before clearing for unsupported contracts rather than guess types or use `any`. Exact entity, primitive, enumeration, and complex-type mappings remain to be designed.

### Required choices are automatic dependencies
Include choices required by requested artifacts without a duplicate selection list, deduplicating shared definitions. An independent choice section selects additional global choices by exact technical name only, including choices-only generation. No solution selector or exclusions are needed. Local column choices are dependencies of their owning artifacts, not independently selected items. Required choice dependencies cannot be excluded.

TypeScript generates exported `as const` value objects and derived union types. Table single-choice properties and form choice attributes retain standard numeric types initially rather than narrowing to the generated union. Form single-choice values use `number | null`; multi-choice values use `number[] | null`, distinct from Web API wire representations. Stronger choice typing may be revisited after evaluating real generated-code usage; changing that exposed typing is a deliberate contract change, not an internal refactor.

TypeScript global choice modules live directly under `choices`, named from their technical names; local modules live under `choices/<table-logical-name>/<column-logical-name>.ts`. Export a PascalCase enum-like value object and same-named derived union type. This deliberate type/value pairing is separate from the deferred table-metadata naming decision. Local modules support forms-only generation without selecting a table model. Align ownership/dependency semantics with .NET, not its physical nesting.

### .NET option sets retain SDK types and integer constants
Keep `OptionSetValue` for single-choice/state/status entity properties and `OptionSetValueCollection` for multi-select properties, with the profile-appropriate null annotations. Preserve SDK storage, unknown numeric values, query/serialization behavior, and collection semantics. No enum-valued accessor conversion, duplicate raw/typed properties, or configuration switch is introduced.

Generate grouped integer constants rather than enums, avoiding casts when constructing `OptionSetValue`, comparing `.Value`, or composing SDK queries. This supersedes the earlier enum preference. Boolean columns remain Boolean properties; exact named-Boolean constant output still requires finalization.

Local constants live in nested entity containers such as `Account.Options.AccountCategoryCode.PreferredCustomer` and `Account.Options.StatusCode.Active`. Global option sets have one standalone static constants class in the `OptionSets` namespace, named from the global technical name; do not duplicate/forward them into each entity or introduce table/Global subnamespaces. This avoids ambiguous imported `StatusCode` types and preserves entity-local discovery.

### Generation language and choice naming
Default to the organization's base language, with an explicit config override. Never select labels based on the connecting user's language. If the selected language has no usable choice label, use the base-language label without a warning. If neither provides a usable label, fail before output changes; never omit the numeric value.

Disambiguate choice labels that normalize to the same member name deterministically using their numeric values, for every member of the colliding group. Do not depend on metadata encounter order or require users to rename managed/platform choices. Exact suffix formatting remains open. Label/translation changes, or a newly introduced collision, may rename generated members; this trade-off is accepted.

### Required .NET compatibility profile
Require `profile` in the .NET recipe. Use platform-family names rather than plugin/client roles: external clients can also target .NET Framework.

| Profile | Generated-code compatibility baseline |
|---|---|
| `netframework` | .NET Framework net462-net481, default C# 7.3; framework API compatibility with net462 |
| `dotnet` | .NET 10, default C# 14 |

Document these explicit baselines; `dotnet` does not promise every historical .NET version. The profile controls available syntax and framework APIs only, not model behavior or special plugin/client logic. Do not require a `LangVersion` override. Nullable value types work in both profiles; nullable-reference annotations are emitted only for `dotnet`. The generator itself still runs on .NET 10 for either profile.

### Thin SDK-compatible .NET entities
Preserve partial-record semantics: a column can be absent or null regardless of business-required metadata. Do not infer C# required members or constructor validation from that metadata. Keep SDK-compatible ID behavior.

Follow metadata read/write capabilities. Read-only columns have getter-only properties; remove the force-setter (`editableReadOnly`) option. A setter is allowed where metadata permits create or update, without attempting to infer the intended operation at runtime. Preserve testability by allowing tests to populate underlying SDK attributes and read them through generated accessors.

Generate ordinary non-virtual properties and remove the virtual-property option. Classes remain partial. Include useful table/column logical names, relationship schema names, and alternate-key identifiers alongside entities without individual inclusion switches. These identifiers do not themselves implement alternate-key operations.

Keep `EntityLogicalName` and applicable `PrimaryNameAttribute` directly on the entity. Use nested static `LogicalNames`, `Relationships`, `AlternateKeys`, and local `Options` containers. Prefer `Relationships` over legacy `Relations`. Do not introduce a separate `AccountMetadata` constants API; separate partial-class files remain possible without changing consumer syntax. The final overall constants shape should be reviewed together.

Do not generate entity type-code constants: custom-table numeric codes are environment-specific. They may remain in an exported metadata snapshot.

### Typed SDK relationships without model expansion
Lookups remain standard `EntityReference` values, including references to unselected tables. Generate thin typed navigation properties for 1:N, N:1, and N:N relationships only where the related table is also selected. Relationship-name constants can describe unselected tables without generating their models.

Use SDK related-entity accessors for typed single-record and collection access. These properties read or attach related data, not retrieve it. No lazy loading, lookup synchronization, custom tracked collections, or implicit association operations. Collection assignment is typed; it does not promise in-place mutation through a custom collection.

### No generated service context
Drop generated `OrganizationServiceContext`, typed query properties, and context-specific `NoLock` behavior. Do not add a repository/query abstraction. Consumers can still use the standard SDK context themselves; generated convenience should not encourage reliance on its limited LINQ provider.

### Metadata snapshots are an independent artifact
Retain XML metadata export as an opt-in artifact independent of C# entity output, using the same include/exclude semantics. Its purpose is an exported table-metadata snapshot usable to populate metadata in the consuming test library, including relationships.

Keep it within the model-generation module for this scope. Do not introduce metadata re-import, offline generation, caching, or a separate metadata module.

Preserve the existing payload format: full table metadata retrieved with `EntityFilters.All`, serialized using `DataContractSerializer(typeof(EntityMetadata))`, one `<table-logical-name>.xml` file under `Metadata`. No custom wrapper/envelope. Consumers deserialize directly to SDK `EntityMetadata`. Verify representative attributes, keys, and relationships; do not promise byte-identical XML or compatibility across arbitrary future SDK versions.

### Required .NET namespace and schema-derived names
Require an explicit root namespace in the .NET config, without vendor defaults or inference from solution names. Use fixed category subnamespaces and matching folders:

| Content | Folder / subnamespace beneath configured root |
|---|---|
| Entity classes and their identifiers | `Entities` |
| Global option-set constants | `OptionSets` |
| Custom API requests/responses and message-name constants | `Messages` |
| XML snapshots | `Metadata` folder only; no C# namespace |

`OptionSets` deliberately follows .NET SDK terminology; TypeScript can use `choices`. Keep requests, responses, and constants together rather than introducing a separate `Requests` namespace. No per-category namespace settings initially.

Use one static `MessageNames` class in `Messages` for constants of all selected platform messages and Custom APIs. Do not retain the misleading platform-only implication of `SdkMessageNames`, split custom/platform containers, or append built-in messages automatically.

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

SDK mapping attributes and constant values retain original technical names. Valid-identifier handling and keyword escaping remain separate design work.

### Deterministic normalized-name collision handling
For both targets, keep readable normalized identifiers when unique. When generated identifiers collide in their actual scope, append a short deterministic hash of each item's exact original technical name to every member of the colliding group. Do not depend on metadata encounter order, use environment-dependent metadata GUIDs for schema-derived names, or require users to rename managed/platform schema.

Emit diagnostics showing the source-to-symbol mapping. Compared with sorted underscores, hash suffixes avoid reassignment when the collision group grows; compared with appending the original name, they avoid long repetitive symbols. Opaque suffixes are an accepted rare-case trade-off; consumers can inspect the symbol's metadata/value. Introducing the first collision can rename the previously unsuffixed symbol.

Retain the specific form-ID and choice numeric-value suffix rules above. Final hash algorithm/length, escaping, generated-member conflicts, file/path collision handling, and final uniqueness checks still require specification; a short hash is not itself a uniqueness guarantee.

### Filenames and consumer linting
Use generated type names for C# type files, `MessageNames.cs` for message constants, and table logical names for XML snapshots. TypeScript table, operation, global-choice, and BPF files use their original technical names, preserving casing and underscores. Forms retain the agreed normalized display-name filenames and ID collision suffixes. Local TypeScript choice paths are described above.

Document excluding the owned generated directory from consumer style linting, including ESLint. Do not add blanket generated `eslint-disable` comments, filename-style switches, or promise compatibility with every custom lint rule. Generated output must still pass strict TypeScript compilation against supported Xrm typings; lint exclusion is not a substitute.

## Risks / Trade-offs

- [Clearing a mis-specified destination deletes user files] -> Document disposable-directory ownership, resolve references before clearing, and apply minimal path checks without claiming comprehensive safety; no confirmation prompt/flag.
- [Independent target shapes duplicate concepts] -> Share terminology where meaning is shared; share logic internally rather than the config shape.
- [New contracts require consumer migration] -> Retain deprecated legacy commands during transition and provide before/after migration guidance.
- [Parallel implementations increase maintenance cost] -> Keep legacy maintenance-only and evaluate removal in a future major release after migration experience; do not silently redirect commands.
- [Broad form scope increases generation work and editor/file churn] -> Solution-based component selection keeps project scope manageable; avoid fragile per-form filters.
- [Module output breaks consumers of generated globals] -> Document import migration, request construction, runtime values, and test-helper usage; validate against representative bundled consumers.
- [Localization and duplicate form names destabilize generated APIs] -> Check the session language, accept display-name snapshot changes, and suffix colliding forms with stable IDs.
- [Failure after clearing leaves incomplete output] -> Accept this risk, fail explicitly, and regenerate after fixing the cause; no staging or rollback.
- [Label changes affect choice constant names] -> Deterministic language fallback and collision handling; accept renaming when labels change.
- [Form snapshots diverge from runtime configuration] -> Preserve useful non-null FormXML accessors but nullable conditional elements; regenerate to recover precise typing, without promising runtime validation.
- [Platform operation contracts introduce complex dependencies] -> Resolve CSDL contract dependencies without implicit artifact selection; fail explicitly for unsupported mappings and validate representative standard-Xrm consumers.

## Outstanding Design Decisions

These are unresolved and can affect specs and tasks. They are blockers for completing the design, not details delegated to implementation:

- Final combined review of both complete recipes and representative generated files, including the overall constants shape and optional test-support configuration.
- TypeScript supporting form exports, exact dedicated BPF typings, and shared supporting-operation-type placement.
- TypeScript table interface/runtime metadata shared-name versus separate exports is explicitly postponed. Barrel files remain deferred.
- Identifier validity/keyword escaping, exact hash/ID/numeric suffix formats, generated-member conflicts, path collisions, and final uniqueness checks for both targets.
- Final named-Boolean constants and remaining entity/message member details; compile both compatibility profiles and round-trip metadata snapshots.
- Specialized numeric/binary/file/image and operation binding/overload/entity/collection/complex/enum mappings, nullable inputs, and non-object response bodies are postponed for technical verification. Validate standard execute compatibility rather than infer it from CSDL alone; do not invent fallback contracts.
- Form test-support adapter/library and helper API, without assuming the legacy mocking framework.
- Precise filesystem/link deletion handling behind the agreed minimal path checks, and actionable legacy/invalid-recipe diagnostics.
- Internal API and implementation boundaries within `dgt.power.modelgeneration`, including worthwhile unconstrained reuse.
