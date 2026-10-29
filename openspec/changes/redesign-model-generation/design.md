# Design

## Context

Code generation is a single `codegeneration` (`cg`) command that takes a mandatory config file (`-c`), a target directory, and a `--folder` name. The config is a V2 polymorphic document discriminated by `type` (`dotnet` | `typescript`) with a shared base class (`namespace`, `language`, `entities`, `requests`, `optionSets`). Entity selection resolves names, solutions, and a wildcard mask additively and writes the result back into the config object. TypeScript clears its whole subtree on each run; .NET deletes known generated files. Forms are generated for every selected entity and narrowed by a filter list of identifiers derived from generated filenames.

See proposal.md - Why for motivation. The user-facing design is agreed and ready to translate into delta specs and implementation tasks. Reviewed recipes below establish property names and selection structure; implementation verification items identify contracts to exercise against real generated output, not a requirement for another design or PoC phase.

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
`model generate` is the shared command for both targets. The target is selected exclusively through the required `--language` option (`dotnet` or `typescript`); there are no target subcommands or duplicate CLI target selector. Require named `--language`, `--config` / `-c`, and `--output` / `-o` options, with no positional arguments or conventional config filename/destination defaults. Existing connection options remain available.

*Alternatives:* `generate models` (keeps every resource a Dataverse object, but breaks resource-first grammar); `types`, `sdk`, `codegen`, `metadata` (rejected as too narrow, too broad, mechanism-named, or naming the input rather than the output).

### New project with parallel legacy support
The new generation contract is not constrained to preserve existing commands, V2 configuration, or output compatibility. This does not require changing every setting or output, nor does it require immediately removing the old implementation. User-affecting breaking changes are documented in the migration guide.

Implement the new generator in `src/modules/dgt.power.modelgeneration`, with tests in `tests/dgt.power.modelgeneration.tests`. Retain `dgt.power.codegeneration` and its `codegeneration` / `cg` commands during a transition period. Do not redirect old commands to incompatible new behavior.

Reuse the existing `DeprecationInterceptor` and `DeprecatedCommandAttribute` mechanism to direct legacy users to `model generate`; no new interceptor is needed. New features belong only in the new generator. Maintain the legacy generator for significant bugs and compatibility/security issues without redesigning its contracts. Consider removal in a future major release after the replacement is released, migration guidance is available, and migration experience has been evaluated; no release/date is promised.

Design the new module from scratch without constraining its contracts or architecture to the legacy implementation. Sharing logic or classes is allowed where it genuinely makes sense and does not compromise the new module. Neither sharing nor a common generation layer is required.

### Config is the contract; the CLI owns target and destination
The config defines what is generated and how. The CLI supplies the target selection (`--language`), connection, config path, output location, and execution controls. Individual settings may later move to CLI parameters, but no override mechanism or config-free mode is introduced.

### Target-specific recipes; independent shapes
Each recipe is specific to one target, selected by the `--language` CLI option. .NET and TypeScript contracts can evolve independently in structure, options, defaults, validation, and schema versions. Shared meaning should use consistent terminology, but shared shape is not required. The current unused TypeScript `namespace` property illustrates why target contracts should not be forced together. There is no shared envelope or polymorphic discriminator; the CLI determines which recipe type to load, so recipes need no `type` property and cannot be mismatched with the wrong generator.

### Explicit, independent artifact sections
Artifact categories are opt-in: a section requests that artifact and omission means it is not generated. Categories can be selected independently (for example, TypeScript entities without forms). Required supporting declarations are dependencies of a requested artifact, not separate options. Defaults keep sections concise; there are no switches for every member.

Each present section contains its own explicit selection. Do not provide a shared/global scope, empty-object opt-in, or Boolean shorthand. Repeated selection is accepted so a section can be understood without resolving inheritance or replacement rules. This supersedes the earlier shared-default-scope proposal.

Use flat artifact sections: both targets have `tables`, `requests`, and `choices`; .NET adds `metadata`, and TypeScript adds `forms`. Use developer-facing artifact names rather than force a technically exhaustive taxonomy: requests include their tightly coupled responses, and .NET also includes supporting message-name constants.

Each artifact section separates `include`, supported `exclude`, and optional `options`. Include objects use `tables`/`solutions` or `names`/`solutions`; exclude objects use the supported exact-name lists. Additional global choices use a simple `include` name array. `options` contains only explicitly supported, typed generation settings, not an arbitrary extension bag. There is no universal selection wrapper. BPF enrichment belongs under `forms.options.businessProcessFlows`, not a separate artifact section.

### New recipe schema family
Use a required numeric `version` field, starting the new independent recipe family at version 1. The target is selected by the required `--language` CLI option, not by any recipe property; recipes do not contain a `type` field. Each target's versions evolve independently. Unknown versions fail explicitly, without silently treating them as the latest format.

Plan schemas at `schemas/modelgeneration/v1/dotnet.schema.json` and `schemas/modelgeneration/v1/typescript.schema.json`. Keep published legacy `schemas/codegeneration/v2/` URLs and legacy loading behavior unchanged; do not introduce codegeneration V3 merely to continue its numbering. Optional `$schema` provides editor assistance; it is not used for runtime target or version inference. Do not silently convert legacy configs in `model generate`.

Require `target` for .NET compatibility selection; do not use `profile`. TypeScript has no `target` setting initially and rejects it as unsupported. Keep the optional root property `language` for localized metadata labels. TypeScript has no generated namespace setting.

### Reviewed recipe shapes
The following representative recipes capture the reviewed property names. Each omitted artifact section disables that output; choices needed by requested artifacts remain automatic dependencies.

```json
{
  "version": 1,
  "tables": {
    "include": { "solutions": ["ContosoCore"] },
    "exclude": { "tables": ["contoso_internalsetting"] }
  },
  "forms": {
    "include": { "tables": ["account", "contact"] },
    "options": {
      "businessProcessFlows": {
        "include": ["contoso_salesprocess"]
      }
    }
  },
  "requests": {
    "include": {
      "solutions": ["ContosoCore"],
      "names": ["RetrieveEnvironmentVariableValue"]
    }
  },
  "choices": { "include": ["contoso_customersegment"] }
}
```

```json
{
  "version": 1,
  "target": "netframework",
  "namespace": "Contoso.Dataverse",
  "tables": {
    "include": { "solutions": ["ContosoCore"] },
    "exclude": { "tables": ["contoso_internalsetting"] }
  },
  "requests": {
    "include": {
      "solutions": ["ContosoCore"],
      "names": ["WhoAmI"]
    }
  },
  "choices": { "include": ["contoso_customersegment"] },
  "metadata": {
    "include": { "tables": ["account", "contact"] }
  }
}
```

Run the TypeScript recipe with `dgtp model generate --language typescript --config <file> --output <owned-directory>`. Run the .NET recipe with `dgtp model generate --language dotnet --config <file> --output <owned-directory>`. The .NET `WhoAmI` selection produces only a message-name constant, not a duplicate SDK request/response.

### Minimal, artifact-specific selection
Selection is the union of includes minus the union of excludes, with exclusion winning, duplicate elimination, and no order dependence. Missing includes never imply select-all. Do not introduce a universal selector language, wildcards, publisher-prefix selection, or arbitrary predicates. Those mechanisms make the generated set harder to predict; explicit names and maintained solution membership cover the initial use cases.

| Artifact | Include sources | Exclude sources |
|---|---|---|
| Table models, either target | Table logical names; solution unique names | Table logical names |
| TypeScript forms | Table logical names; solution unique names, selecting form components | Table logical names, removing all selected forms for those tables |
| .NET XML snapshots | Same as table models | Table logical names |
| .NET requests | Exact Custom API or platform-message names; solution Custom API components | Exact operation/message names |
| TypeScript requests | Exact Custom API or platform Web API action/function names; solution Custom API components | Exact operation names |
| Additional global choices | Exact global choice technical names | None |

Solution selection does not discover platform messages/operations or classic Actions. There are no solution-based exclusions. Selecting a solution does not transitively select tables merely referenced by its forms, relationships, or operations.

Selecting a table always generates its full model definition, regardless of whether a solution includes the whole table or only selected components. Component-level solution scoping applies to components such as forms, not to partial entity models.

BPF enrichment accepts exact workflow unique names in `forms.options.businessProcessFlows.include`, with no solution/exclusion selectors or implicit selection expansion.

### Resolve before writing
All explicit references (tables, solutions, operations, choices, and so on) are resolved and checked before any output is produced or any directory is cleared. Unknown explicit references fail rather than silently disappearing, including in exclusions. Excluding an existing item outside the included set is valid. An existing solution with no relevant components is not an unknown reference.

Warn when a requested artifact section resolves to nothing while other sections produce output. Fail before clearing if the entire recipe resolves to no artifacts: this is an invalid/no-op generation request, consistent with plugin push rejecting an entirely empty target, not an output-backup mechanism.

Reject unknown/additional config properties at every level, both in JSON schemas and runtime loading. Support the `$schema` annotation explicitly; its presence provides editor assistance but is not itself runtime validation.

### Resolution-first generation pipeline
Use the following separation:

```text
Load and validate recipe
  --> Resolve selection, metadata, dependencies, names, and paths
  --> Target-specific resolved generation plan
  --> Clear owned output
  --> Render and write
```

Resolve unsupported contracts and naming/path conflicts before clearing. Rendering consumes the resolved plan without Dataverse calls or recipe mutation. This moves predictable contract failures ahead of destructive output changes without promising that rendering or I/O cannot fail. Do not mandate a universal cross-target plan or common generation framework; each target can use the simplest suitable representation and share logic where useful.

### Scriban renders resolved models through layout-only templates
Use Scriban in the new module for embedded .NET and TypeScript source templates. Write new templates in native Scriban syntax rather than adopting Liquid compatibility mode or requiring reuse of legacy templates. Retain Fluid and existing templates unchanged in the legacy module.

C# owns selection, localization, type/nullability mapping, identifier and literal escaping, collisions, imports, and output paths. Typed rendering models expose those resolved decisions. Templates stay deliberately simple: output layout, member iteration, and small structural conditions such as whether a setter exists. Do not expose raw Dataverse metadata or move semantic decisions into template functions/filters. Scriban's richer scripting language is not a reason to introduce a second business-logic layer.

Choose Scriban for developer-oriented source-template ergonomics, particularly non-greedy whitespace trimming and configurable auto-indentation. Establish explicit whitespace/indentation conventions and verify blank lines, nested blocks, and inserted fragments through rendering tests. No measured performance advantage over Fluid is assumed; parse templates once and reuse parsed representations across generated artifacts.

Fluid's published comparison uses Scriban's Liquid compatibility mode, not the native templates planned here; it does not establish the relative cost of this generator's workload. Check representative rendering cost during implementation. The resolved-model boundary keeps a later renderer/template replacement contained without a pluggable-engine abstraction.

Parse and check embedded templates before clearing output. Configure strict missing-variable/member handling so rendering-model mismatches fail explicitly rather than emit missing text; include template identity and source location in diagnostics where available. Rendering failures after clearing retain the agreed direct-write failure semantics. Cover representative output and negative diagnostics alongside generated-code compilation, not snapshots alone.

XML metadata snapshots continue to use SDK data-contract serialization, not Scriban. Do not introduce consumer template overrides, a new configuration surface, or a generic rendering framework. Select a compatible Scriban package during implementation, verify compatibility with repository signing/publishing constraints, and synchronize the affected dependency locks.

### Owned output directory
The CLI supplies the destination. The tool owns that output directory and clears it on regeneration; no manifest, ownership marker, or mandatory overwrite guard is planned. Recipes must not share a destination if their outputs need to coexist. Help and migration guidance must explain directory ownership.

The supplied output directory is the generation root. Do not add `Model`, `DotNet`, or `TypeScript` wrapper directories; remove the separate `--folder` concept. Category folders are described below. Do not add a `--confirm` flag or interactive overwrite prompt.

Before clearing, reject a filesystem root, the current working directory or a lexical ancestor, and any destination containing the input recipe. Reject linked output destinations; never follow directory symbolic links or junctions when clearing the output tree. Use normalized paths and platform-appropriate comparison. These are minimal best-effort safeguards, not a cross-platform guarantee against mounts, aliases, or filesystem races. Do not add Git-root detection or a general output-safety framework. Verify link handling with focused filesystem tests. Clearly report the resolved output being cleared and document that it must be disposable.

After the resolved generation plan is complete, clear the output and render/write directly. Do not introduce staging, backup, or rollback. Rendering errors, I/O failures, or cancellation after clearing may leave incomplete output; report failure explicitly. Recovery is to fix the cause and regenerate. This risk is accepted for reproducible, tool-owned artifacts; Git tracking is not assumed.

### Form selection is broad within table or solution scope
Generate all supported forms belonging to selected tables, or included in selected solutions. A solution selector scopes form components, rather than expanding to every form of each referenced table. Combined with table selection, the table selector intentionally requests all supported forms for those tables.

Do not carry forward individual form-name filters, filename-derived identifiers, individual form-ID lists, or configurable form-type filters initially. Extra declarations within the selected scope are an accepted trade-off for a straightforward shared recipe. Form selection remains independent of requesting entity-model output.

Support active Main, Quick Create, and Quick View forms. Main and Quick Create modules export `FormContext` compatible with `Xrm.FormContext`; Quick View modules export `QuickFormControl` compatible with `Xrm.Controls.QuickFormControl`, not a fictional full form context. Replace the name-based accessor surface as necessary for strict generated names rather than inheriting permissive fallback signatures.

Selection and generated naming are separate. Removing name-based selection does not solve localized or colliding generated identifiers.

### TypeScript output uses exported modules
Target bundler-based TypeScript projects with exported modules and explicit imports, without a parallel ambient-output mode. Type-only imports have no runtime output; required runtime values must have real implementations rather than exist only in declaration files.

Do not carry forward the generated `XrmForm`, `XrmTable`, and related ambient namespace hierarchy. Dataverse's standard `Xrm` API remains the platform API; generated modules do not replace it.

Use category folders and one module per table, form, choice definition, or request contract: `tables`, `forms`, `choices`, and `requests`. `requests` covers Custom APIs and platform actions/functions with their response contracts. Organize forms by table and form type. Use direct imports initially, without generated barrel files; barrels remain a possible later convenience. No standalone BPF folder or test-support output is generated.

Form modules use simple exports such as `FormContext` or `QuickFormControl`. Consumers choose descriptive import aliases. Supporting form declaration details still require technical verification.

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

Include a grouped runtime metadata object alongside the interface, containing `logicalName`, `entitySetName`, `columns`, and `navigationProperties`. Read entity-set names from metadata, not pluralization. Export the interface and value under the same PascalCase name in TypeScript's separate type/value spaces; do not generate an additional `accountMetadata` export. This is plain identifier data, not a runtime model class, query DSL, or ambient namespace.

```typescript
export interface Account {
  accountid?: string;
  name?: string | null;
  accountcategorycode?: number | null;
}

export const Account = {
  logicalName: "account",
  entitySetName: "accounts",
  columns: {
    accountId: "accountid",
    name: "name",
    accountCategoryCode: "accountcategorycode"
  },
  navigationProperties: {}
} as const;
```

The empty navigation group in this abbreviated example does not suppress real selected relationships. Consumers use `Account` as a record type and `Account.logicalName` / `Account.columns.name` as runtime values, with standard `Xrm.WebApi` calls and explicit response assertions.

### TypeScript naming distinguishes generated symbols from wire names
Use PascalCase for generated types/classes, shared-name table values, and choice value objects; use camelCase for grouped metadata property keys. Derive table types and column identifier keys from schema names using the agreed best-effort conversion below, lowercasing the initial character for camelCase. Preserve publisher prefixes and existing internal capitalization.

Keep record properties and operation input/output properties exactly as required by the platform. For example, `columns.contosoOrderNumber` can contain `"contoso_ordernumber"`, but the record interface itself exposes `contoso_ordernumber`. Renaming wire properties would require converters and is outside this design.

### Form names are readable snapshots with explicit language checks
Derive form-module names from display names, scoped by table and form type. Do not rely on rarely populated form unique names. Accept display-name and localization changes affecting imports rather than use GUID-only names.

For every member of a colliding normalized form-name group, append `_` followed by the full form GUID without hyphens, for example `Account_0123456789abcdef0123456789abcdef`; unique names remain unsuffixed. Do not choose a winner by retrieval order. Adding the first collision can rename the previously unsuffixed module.

When forms are requested, fail before output changes if the connecting user's UI language differs from the effective generation language (configured override or organization base language). Do not modify user settings. Recipes without forms need no form-language check.

### Form accessors retain useful snapshot guarantees
Ordinary attributes and controls declared in FormXML have precise non-null accessors. Runtime-conditional elements, such as BPF-only attributes and controls, remain nullable. Attribute-value nullability is separate from attribute-object presence; business-required metadata does not imply a non-null value.

Restrict name-based access to generated names for attributes, controls, tabs, and sections, including their collection APIs. Removing or renaming an element and regenerating must make references to its old name fail compilation, not merely lose precise typing through a standard string fallback. Preserve other standard Xrm methods, non-name overloads, and specialized event APIs without retaining inherited signatures that defeat strict name checking. Verify the actual type composition; adding strict overloads beside permissive ones is insufficient.

For dynamic access, consumers deliberately widen to the standard platform type:

```typescript
const platformForm: Xrm.FormContext = form;
platformForm.getAttribute(dynamicName);
```

This is a snapshot-based compile-time contract, comparable to .NET early-bound properties: regular regeneration and compilation catches stale references earlier. It is not runtime validation. Known conditional elements still have nullable accessors, independently of strict name selection. Test removed-name failures as well as ordinary API compatibility.

Preserve ordinary collection behavior such as all-items access, numeric indexing, iteration, and filtering. Section names are scoped to their containing tab. The existing generator demonstrates that form-specific typing is achievable, but does not constrain the new type design or require reuse. No separate PoC is required: compilation tests during implementation must cover strict names, collection access, and explicit widening.

Use local assertions such as `context.getFormContext() as AccountSalesFormContext`, with an imported alias for the generated `FormContext`. Do not generate event-context types, globally augment `getFormContext<T>()`, or add a runtime context factory. Assertions express the expected form; they do not validate its identity.

Embedded Quick Views use their generated type only when those forms are selected; otherwise retain `Xrm.Controls.QuickFormControl`. Do not automatically select referenced forms/tables or fail merely because the referenced Quick View was not selected. Preserve the platform's loading lifecycle.

### BPFs enrich forms rather than form an independent artifact
Select workflow unique names through `forms.options.businessProcessFlows.include`. Generate no dedicated BPF modules, process/stage constants, process controller, execution helper, or other runtime abstraction.

Selected BPFs also enrich selected Main-form typings for their corresponding participating tables. Combine known attributes/controls across selected processes; BPF-only accessors remain nullable because the active process/stage varies. If an attribute also exists in FormXML, keep its ordinary non-null accessor; its BPF control remains nullable. Do not enrich Quick Create or Quick View forms.

Selecting a BPF does not select additional tables/forms. With no BPF selection, no BPF enrichment occurs. Consumers use standard client APIs to determine the active process and runtime availability.

### No generated test-support framework
Generate no test helpers, form fixtures, mock adapters, or testing dependencies. Do not introduce a new dependency on xrm-mock or replace it with generated Playwright scaffolding. Legacy helpers remain available only through the unchanged legacy generator.

Complex client logic can be unit-tested as consumer-owned service/library code. Real form integration can be tested with browser tooling such as Playwright. Generated form typings remain usable through erased type imports and local assertions in browser callbacks; runtime imports are not automatically transferred into a browser execution context. Testing infrastructure is outside this generator's output contract.

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

### TypeScript requests are directly executable
Support explicitly selected Custom APIs and platform Web API actions/functions, not just SDK message-name existence. Solution selection discovers Custom APIs only. Resolve platform operation contracts from the connected environment's CSDL `$metadata`: binding, parameters, return types, and required supporting definitions. An SDK message without an exposed Web API operation cannot produce a TypeScript request. For example, `RetrieveEnvironmentVariableValue` can produce a typed function request and response.

For each selected operation, generate:

- A `Parameters` interface with required and optional inputs, when inputs exist.
- A request class accepting that object, exposing request properties, and implementing `getMetadata()` for direct use with `Xrm.WebApi.online.execute`.
- A `Response` body interface where applicable. The caller parses and casts the response; no inference or execution wrapper is promised.

Each operation module exports `Request` and the applicable `Parameters`/`Response` types. Omit empty parameter/response interfaces. Parameterless operations support `new Request()`. Preserve omission of optional inputs; do not add defaults, coerce values, or replace omitted inputs with `null`. Optionality does not automatically permit explicit null.

For entity-bound Custom APIs, accept `entity: { entityType, id }` in the parameter object, constraining `entityType` to the bound table logical name. This is the platform request reference shape, not `Xrm.LookupValue`. The request supplies binding metadata. Collection-bound operations do not take an individual record target; exact collection-binding handling remains open.

Do not generate separate TypeScript message constants, request factories, executors, transport/authentication abstractions, or a replacement Xrm type hierarchy. The request already carries the operation name in `getMetadata()`; unlike .NET plugin code, no independent message-name consumer need has been established.

Generated artifacts must work with standard Xrm typings. Reuse those types where suitable. Automatically include supporting operation contract types, such as the complex `PrincipalAccess` parameter type, and reuse shared definitions across selected operations. These supporting types do not select full table models, forms, or other operations. Fail before clearing for unsupported contracts rather than guess types or use `any`.

### Entity-valued request contracts reflect the declared schema
Distinguish a bound record target or entity reference from an Entity payload containing record data. For an Entity parameter constrained to a known table, generate a strongly typed partial Web API record payload with the applicable type annotation and known column types. Include the needed supporting declaration without requiring standalone table selection; reuse a selected table contract where compatible. Do not assume every Entity parameter can be reduced to `{ entityType, id }`.

An unrestricted Entity parameter can represent different concrete tables or genuinely dynamic data, including `Microsoft.Dynamics.CRM.expando`. Represent that openness explicitly with unknown-valued properties and the applicable platform annotations, not `any` or invented business properties. Consumers can supply a narrower business type where the API's metadata does not declare one. EntityCollection follows the corresponding element contract; response types follow the same declared-schema principle.

Reference: [Use open types with custom APIs](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/use-open-types). Metadata cannot recover an undeclared business schema. Validate actual serialization and standard-Xrm execution against representative requests in the first implementation, including open-type annotations and platform limitations. Refine specialized mappings from that evidence rather than requiring every edge case to be resolved on paper; unsupported mappings still fail before output changes.

### Required choices are automatic dependencies
Include choices required by requested artifacts without a duplicate selection list, deduplicating shared definitions. An independent choice section selects additional global choices by exact technical name only, including choices-only generation. No solution selector or exclusions are needed. Local column choices are dependencies of their owning artifacts, not independently selected items. Required choice dependencies cannot be excluded.

TypeScript generates exported `as const` value objects and derived union types. Table single-choice properties and form choice attributes retain standard numeric types initially rather than narrowing to the generated union. Form single-choice values use `number | null`; multi-choice values use `number[] | null`, distinct from Web API wire representations. Stronger choice typing may be revisited after evaluating real generated-code usage; changing that exposed typing is a deliberate contract change, not an internal refactor.

TypeScript global choice modules live directly under `choices`, named from their technical names; local modules live under `choices/<table-logical-name>/<column-logical-name>.ts`. Export a PascalCase enum-like value object and same-named derived union type. Local modules support forms-only generation without selecting a table model, rather than nesting values under a table export. Align ownership/dependency semantics with .NET, not its physical nesting.

### .NET choices retain SDK types and integer constants
Keep `OptionSetValue` for single-choice/state/status entity properties and `OptionSetValueCollection` for multi-select properties, with the target-appropriate null annotations. Preserve SDK storage, unknown numeric values, query/serialization behavior, and collection semantics. No enum-valued accessor conversion, duplicate raw/typed properties, or configuration switch is introduced.

Generate grouped integer constants rather than enums, avoiding casts when constructing `OptionSetValue`, comparing `.Value`, or composing SDK queries. This supersedes the earlier enum preference. Boolean columns remain Boolean properties; exact named-Boolean constant output still requires finalization.

Local constants live in nested entity containers such as `Account.Choices.AccountCategoryCode.PreferredCustomer` and `Account.Choices.StatusCode.Active`. Global choices have one standalone static constants class in the `Choices` namespace, named from the global technical name; do not duplicate/forward them into each entity or introduce table/Global subnamespaces. This avoids ambiguous imported `StatusCode` types and preserves entity-local discovery.

Follow current Dataverse terminology for designed names (`Columns`, `Choices`) while preserving historical SDK types/members such as `OptionSetValue` and `EntityLogicalName`. Cross-target terminology consistency is useful, not a requirement to force identical APIs.

### Generation language and choice naming
Default to the organization's base language, with an explicit config override. Never select labels based on the connecting user's language. If the selected language has no usable choice label, use the base-language label without a warning. If neither provides a usable label, fail before output changes; never omit the numeric value.

Disambiguate choice labels that normalize to the same member name using `_` followed by the numeric value, such as `SomeLabel_1` and `SomeLabel_2`, for every member of the colliding group. No special negative-value suffix convention is introduced. Do not depend on metadata encounter order or require users to rename managed/platform choices. Label/translation changes, or a newly introduced collision, may rename generated members; this trade-off is accepted.

### Required .NET compatibility target
Require `target` in the .NET recipe. Use platform-family names rather than plugin/client roles: external clients can also target .NET Framework.

| Target | Generated-code compatibility baseline |
|---|---|
| `netframework` | .NET Framework net462-net481, default C# 7.3; framework API compatibility with net462 |
| `dotnet` | .NET 10, default C# 14 |

Document these explicit baselines; `dotnet` does not promise every historical .NET version. The target controls available syntax and framework APIs only, not model behavior or special plugin/client logic. Do not require a `LangVersion` override. Nullable value types work in both targets; nullable-reference annotations are emitted only for `dotnet`. The generator itself still runs on .NET 10 for either target.

### Thin SDK-compatible .NET entities
Preserve partial-record semantics: a column can be absent or null regardless of business-required metadata. Do not infer C# required members or constructor validation from that metadata. Keep SDK-compatible ID behavior.

Follow metadata read/write capabilities. Read-only columns have getter-only properties; remove the force-setter (`editableReadOnly`) option. A setter is allowed where metadata permits create or update, without attempting to infer the intended operation at runtime. Preserve testability by allowing tests to populate underlying SDK attributes and read them through generated accessors.

Generate ordinary non-virtual properties and remove the virtual-property option. Classes remain partial. Include useful table/column logical names, relationship schema names, and alternate-key identifiers alongside entities without individual inclusion switches. These identifiers do not themselves implement alternate-key operations.

Keep `EntityLogicalName` and applicable `PrimaryNameAttribute` directly on the entity. Use nested static `Columns`, `Relationships`, `AlternateKeys`, and local `Choices` containers. `Columns` identifies the component rather than its string representation and avoids hiding the SDK's inherited `Entity.Attributes` collection. Prefer `Relationships` over legacy `Relations`. Do not introduce a separate `AccountMetadata` constants API; separate partial-class files remain possible without changing consumer syntax.

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
| Global choice constants | `Choices` |
| Custom API requests/responses and message-name constants | `Requests` |
| XML snapshots | `Metadata` folder only; no C# namespace |

Use current platform terminology and developer-facing artifact names, not SDK history as a naming constraint. Keep requests, responses, and constants together in `Requests`. The category name follows the primary artifact; matching responses and supporting constants do not require a separate taxonomy. No per-category namespace settings initially.

Use one static `MessageNames` class in `Requests` for constants of all selected platform messages and Custom APIs. The member group still names actual SDK messages, useful independently for plugin execution-context comparisons. Do not retain the misleading platform-only implication of `SdkMessageNames`, split custom/platform containers, or append built-in messages automatically.

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

SDK mapping attributes and constant values retain original technical names.

### Identifier validity and keyword escaping
Preserve valid platform names and use language-native escaping where available. Escape C# keywords with `@` where necessary. Quote TypeScript wire-property names that cannot be written as bare property names, preserving exact payload keys rather than normalizing them. Generated type/declaration identifiers that cannot be expressed directly require normalization and the collision policy below. Verify emitted identifiers against the selected language baseline before writing.

### Deterministic normalized-name collision handling
For both targets, keep readable normalized identifiers when unique. When generated identifiers collide in their actual scope, append `_` followed by the first eight lowercase hexadecimal characters of SHA-256 over each item's exact original technical name encoded as UTF-8, such as `ContosoOrder_a1b2c3d4`. Suffix every metadata-derived member of the colliding group. Do not depend on metadata encounter order, use environment-dependent metadata GUIDs for schema-derived names, or require users to rename managed/platform schema.

Emit diagnostics showing the source-to-symbol mapping. Compared with sorted underscores, hash suffixes avoid reassignment when the collision group grows; compared with appending the original name, they avoid long repetitive symbols. Opaque suffixes are an accepted rare-case trade-off; consumers can inspect the symbol's metadata/value. Introducing the first collision can rename the previously unsuffixed symbol.

Reserve fixed generated and inherited API names in their actual scope. If a metadata-derived identifier conflicts with one, suffix the metadata-derived identifier using the same rule and leave the fixed API intact. Consider language restrictions and enclosing declaration names as part of identifier resolution. Preserve original Dataverse names in mappings and values. Apply this policy across both targets, not as a .NET-only exception; separate TypeScript wire-record and metadata-object scopes do not inherently conflict with each other.

Retain the specific form-ID and choice numeric-value suffix rules above. Check final symbol and file/path uniqueness before clearing output, including case-insensitive path collisions. If any conflict remains, report the conflicting sources and fail rather than overwrite output or choose a winner by encounter order. The eight-character hash is not itself a uniqueness guarantee; do not silently extend or reassign suffixes.

### Filenames and consumer linting
Use generated type names for C# type files, `MessageNames.cs` for message constants, and table logical names for XML snapshots. TypeScript table, request, and global-choice files use their original technical names, preserving casing and underscores. Forms retain the agreed normalized display-name filenames and ID collision suffixes. Local TypeScript choice paths are described above.

Document excluding the owned generated directory from consumer style linting, including ESLint. Do not add blanket generated `eslint-disable` comments, filename-style switches, or promise compatibility with every custom lint rule. Generated output must still pass strict TypeScript compilation against supported Xrm typings; lint exclusion is not a substitute.

## Risks / Trade-offs

- [Clearing a mis-specified destination deletes user files] -> Document disposable-directory ownership, resolve references before clearing, and apply minimal path checks without claiming comprehensive safety; no confirmation prompt/flag.
- [Independent target shapes duplicate concepts] -> Share terminology where meaning is shared; share logic internally rather than the config shape.
- [New contracts require consumer migration] -> Retain deprecated legacy commands during transition and provide before/after migration guidance.
- [Parallel implementations increase maintenance cost] -> Keep legacy maintenance-only and evaluate removal in a future major release after migration experience; do not silently redirect commands.
- [Broad form scope increases generation work and editor/file churn] -> Solution-based component selection keeps project scope manageable; avoid fragile per-form filters.
- [Module output breaks consumers of generated globals] -> Document import migration, request construction, runtime values, and removal of test helpers; validate against representative bundled consumers.
- [Localization and duplicate form names destabilize generated APIs] -> Check the session language, accept display-name snapshot changes, and suffix colliding forms with stable IDs.
- [Failure after clearing leaves incomplete output] -> Accept this risk, fail explicitly, and regenerate after fixing the cause; no staging or rollback.
- [Label changes affect choice constant names] -> Deterministic language fallback and collision handling; accept renaming when labels change.
- [Form snapshots diverge from runtime configuration] -> Preserve useful non-null FormXML accessors but nullable conditional elements; regenerate and compile to catch removed names, without promising runtime validation.
- [Platform operation contracts introduce complex dependencies] -> Resolve CSDL contract dependencies without implicit artifact selection; fail explicitly for unsupported mappings and validate representative standard-Xrm consumers.
- [Dependency notices may not accompany distributed binaries] -> Verify the selected Scriban package's license and required notices. Dependency-wide license inventory, bundled notices, and artifact checks belong to a separate release/packaging change, including resolution of the existing Licence.md MS-PL versus package-metadata MS-RL mismatch. Track this as a release obligation, not as unique to Scriban; MIT dependencies also require notices.

## Implementation Verification and Refinement

No further high-level recipe or consumer-API decisions block delta specs and task creation. Exercise the following in the first implementation and refine from actual generated-code usage; they do not require separate PoCs or reuse of legacy internals:

- Compile TypeScript form consumers with strict-name access and collection APIs, removed-name failures, preserved standard methods, and explicit widening without permissive fallback loopholes.
- Compile request consumers against standard Xrm typings and verify serialization/execution for bound/unbound requests, entity references, table-constrained/open Entity payloads, collections, complex/enum dependencies, optional versus nullable inputs, and response bodies.
- Verify specialized numeric/binary/file/image mappings and platform binding/overload limits. Make unsupported contracts explicit before clearing rather than invent fallback shapes. Supporting declaration layout can follow the simplest suitable implementation; barrels remain deferred.
- Cover identifier escaping, reserved/inherited-member conflicts, every-member collision suffixes, metadata-order independence, and final symbol/path conflicts in both targets.
- Compile both .NET compatibility targets and round-trip metadata snapshots. Exercise remaining entity/message details, including whether named Boolean constants add value without changing Boolean property types.
- Test linked destinations and non-traversal of directory links/junctions, alongside existing output-path checks and actionable legacy/invalid-recipe diagnostics.
- Choose concrete internal API boundaries for the target-specific resolution-first pipeline, sharing only where useful without a mandatory universal plan.
