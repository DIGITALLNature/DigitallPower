# Code Generation Output Delta

## MODIFIED Requirements

### Requirement: Each target has one generation mode
Retained `codegeneration`/`cg` generation MUST select .NET or TypeScript through the typed configuration without a Full/Light mode selector. TypeScript Full runtime models and attribute-level allowlists MUST NOT be supported.

#### Scenario: Generate TypeScript without a mode selector
- **WHEN** a supported TypeScript configuration is executed
- **THEN** the existing typed-config TypeScript output is generated without selecting Full or Light
- **AND** no legacy Full runtime entity or form-context wrapper model is emitted

#### Scenario: Legacy output controls are supplied
- **WHEN** a configuration supplies `TypescriptGeneratorVersion`, `EntityFilters`, `EntityRefFilters`, or `EntityFormFilters`
- **THEN** generation fails with an actionable unsupported-setting error instead of silently ignoring the setting
- **AND** attribute-level filters are identified as removed without replacement

### Requirement: Supported .NET output is preserved
For retained `codegeneration`/`cg` commands, the .NET target MUST preserve existing typed-config generation behavior for Modern and Framework output, including configured entities, context, options, logical names, relationships, request wrappers, message constants, and optional metadata artifacts.

#### Scenario: Generate either supported .NET target
- **WHEN** a supported .NET configuration selects Modern or Framework output
- **THEN** its artifacts retain the existing target-specific syntax, names, paths, and configured inclusion behavior

### Requirement: Supported TypeScript output is preserved
For retained `codegeneration`/`cg` commands, the TypeScript target MUST preserve existing typed-config behavior for entity declarations, form typings, option sets, SDK message names, parameterized request wrappers, and optional form test helpers. Existing localization, naming, deterministic rendering, and template diagnostics MUST remain intact.

#### Scenario: Generate scoped TypeScript artifacts
- **WHEN** a TypeScript configuration selects entities, forms, option sets, and requests
- **THEN** generated artifacts retain the existing typed-config names, paths, and contents for the same metadata and settings

#### Scenario: Optional TypeScript outputs are controlled
- **WHEN** form test helpers are enabled or parameterized request wrapper generation is disabled
- **THEN** those settings retain their existing effects without introducing a separate generation mode

## ADDED Requirements

### Requirement: Model output is resolved before replacement
`model generate` SHALL resolve selections, metadata, contract dependencies, identifiers, and paths before clearing output. Unsupported contracts or unresolved conflicts SHALL fail without output changes. After resolution, generation SHALL clear the owned destination and write directly, without staging, backup, rollback, or a requirement for Git tracking.

#### Scenario: Resolution fails
- **WHEN** a selected request has an unsupported mapping or a final output conflict
- **THEN** generation fails with the source and reason identified
- **AND** existing output remains untouched

#### Scenario: Writing fails after clearing
- **WHEN** rendering, I/O, or cancellation prevents completion after output replacement begins
- **THEN** the command reports failure rather than success
- **AND** partial output is permitted without rollback

### Requirement: Model output paths have explicit ownership and safeguards
The supplied destination SHALL be the generation root without wrapper folders. Generation SHALL report the resolved disposable destination and reject roots, the working directory or its lexical ancestors, destinations containing the recipe, and linked destinations. Cleanup SHALL NOT follow directory links/junctions. These checks SHALL NOT be presented as comprehensive filesystem safety.

#### Scenario: An unsafe destination is supplied
- **WHEN** the destination matches a prohibited path or is linked
- **THEN** generation fails before deleting any output

#### Scenario: A nested link exists
- **WHEN** an owned output tree contains a directory link or junction
- **THEN** cleanup does not traverse or delete its target contents

#### Scenario: Regenerate into a valid destination
- **WHEN** resolution succeeds for a valid disposable output directory
- **THEN** stale contents are cleared without an interactive prompt or ownership marker
- **AND** category output is written directly beneath it

### Requirement: Model .NET targets differ only in compatibility
`netframework` output SHALL compile with C# 7.3 and net462-compatible APIs for .NET Framework net462-net481. `dotnet` output SHALL target .NET 10/C# 14. Nullable references SHALL be emitted only for `dotnet`. Both SHALL preserve the same model semantics without role-based behavior or requiring a LangVersion override.

#### Scenario: Consume either compatibility target
- **WHEN** equivalent recipes select either .NET target
- **THEN** representative entities and requests compile against their stated baselines
- **AND** syntax/null annotations differ only as required for compatibility

### Requirement: Model .NET entities stay thin and SDK-compatible
Selected tables SHALL produce partial Entity-derived classes with non-virtual SDK-backed properties. Columns writable on create or update SHALL have setters; other columns SHALL be getter-only. Business-required metadata SHALL NOT imply required/non-null record values. Choice properties SHALL retain OptionSetValue/OptionSetValueCollection and Boolean properties SHALL remain Boolean.

#### Scenario: Read partial or read-only data
- **WHEN** an entity omits a business-required column or reads a read-only column populated through SDK attributes
- **THEN** access preserves normal SDK absent/null semantics without constructor validation
- **AND** no forced public setter or duplicate typed/raw choice property is generated

### Requirement: Model .NET identifiers use current component names
Entities SHALL expose EntityLogicalName and applicable PrimaryNameAttribute directly, with nested Columns, Relationships, AlternateKeys, and local Choices containers. Choice members SHALL be integer constants, not enums. Global choices SHALL have standalone classes without per-entity forwarding. Type-code constants, service contexts, converters, and query/repository abstractions SHALL NOT be generated.

#### Scenario: Use local and global choices
- **WHEN** a consumer accesses a local status or shared global choice
- **THEN** local constants are available through `Account.Choices.StatusCode` and global constants through the configured root's `Choices` namespace
- **AND** integer values can be passed to SDK OptionSetValue without enum casts

#### Scenario: Access component identifiers
- **WHEN** a consumer uses Columns, Relationships, or AlternateKeys
- **THEN** constants preserve column logical names, relationship schema names, or alternate-key identifiers respectively
- **AND** alternate-key constants do not implement alternate-key operations

### Requirement: Model .NET navigation does not expand selection
Lookups SHALL remain EntityReference values. Typed SDK related-entity accessors SHALL be generated only between selected tables for supported 1:N, N:1, and N:N relationships. Relationship constants SHALL be allowed for unselected targets. Navigation SHALL NOT load records, synchronize lookups, track custom collections, or perform implicit association operations.

#### Scenario: A relationship points outside selection
- **WHEN** a selected table references an unselected table
- **THEN** its lookup and relationship identifier remain usable without a generated target model or typed navigation

#### Scenario: Assign related data
- **WHEN** a consumer assigns a typed selected related record or collection
- **THEN** standard SDK related data is attached without a Dataverse retrieval or custom mutation-tracking behavior

### Requirement: Model .NET requests distinguish APIs from platform messages
Selected Custom APIs SHALL generate request/response contracts and message-name constants. Selected platform SDK messages SHALL generate constants only, without duplicate SDK request classes or a default message catalogue. All constants SHALL share one static MessageNames class; separately shipped common-library constants SHALL remain unchanged.

#### Scenario: Select a platform message and a Custom API
- **WHEN** a .NET recipe selects WhoAmI and a Custom API
- **THEN** both names appear in MessageNames but only the Custom API receives generated request/response classes

### Requirement: Model output categories and filenames are predictable
.NET SHALL use Entities, Choices, and Requests folders/subnamespaces beneath the configured namespace and a Metadata folder for XML. TypeScript SHALL use tables, forms, choices, and requests modules with direct imports and no initial barrels. C# type files SHALL follow generated type names; TypeScript table/request/global-choice files SHALL preserve technical names and casing.

#### Scenario: Inspect generated categories
- **WHEN** either target generates its selected artifacts
- **THEN** no Model/DotNet/TypeScript wrapper, standalone BPF category, or per-category namespace customization is introduced

#### Scenario: Locate local TypeScript choices
- **WHEN** a local column choice is required
- **THEN** its module is at `choices/<table-logical-name>/<column-logical-name>.ts`
- **AND** global choices are directly beneath `choices`

### Requirement: Model metadata snapshots preserve SDK XML
.NET metadata selection SHALL emit full EntityFilters.All table metadata as DataContractSerializer EntityMetadata XML, one `<table-logical-name>.xml` under Metadata, independently of entity-class selection. No wrapper, re-import, offline-generation contract, or byte-identical cross-SDK guarantee SHALL be introduced.

#### Scenario: Export snapshots without entities
- **WHEN** only metadata for a table is selected
- **THEN** its XML deserializes directly to SDK EntityMetadata with representative attributes, keys, and relationships
- **AND** no C# table class is required or generated

### Requirement: TypeScript table models describe partial Web API records
Each selected table SHALL export one partial wire-record interface and a same-named runtime identifier value. Exact API properties and nullability SHALL be preserved without separate create/update types or business-required enforcement. Metadata SHALL contain logicalName, metadata-derived entitySetName, columns, and navigationProperties. No runtime model, hydration, or query DSL SHALL be generated.

#### Scenario: Use a table as both type and value
- **WHEN** a consumer imports Account
- **THEN** it can type a partial record as Account and use Account.logicalName and Account.columns.name
- **AND** no separate accountMetadata export is required

#### Scenario: Map primitive values
- **WHEN** ordinary columns are emitted
- **THEN** text/GUID/date-time wire values use strings, Boolean uses boolean, ordinary numeric values use number, single choices use number, and multiple-choice wire values use comma-separated strings
- **AND** form date values use Date and form multi-choice values use number arrays instead

### Requirement: TypeScript lookup and expansion shapes remain distinct
Table interfaces SHALL expose actual Web API lookup-ID, bind, and navigation property names separately, including target-specific polymorphic names. Expanded records SHALL be typed only when related tables are selected. Lookup/bind support SHALL NOT require typed expansion, implicit table selection, Xrm.LookupValue wire values, URL builders, or an any fallback.

#### Scenario: A selected account references a contact
- **WHEN** both tables are selected
- **THEN** `_primarycontactid_value`, `primarycontactid@odata.bind`, and typed `primarycontactid` expansion are separate applicable properties

#### Scenario: The target table is unselected
- **WHEN** only the referencing table is selected
- **THEN** lookup-ID and bind properties remain available without a typed target expansion

### Requirement: TypeScript choices are numeric value and type pairs
Choice modules SHALL export PascalCase as-const value objects and same-named derived union types. Table/form single-choice properties SHALL retain standard numeric types rather than narrow to those unions. Form multi-choice values SHALL remain number arrays with platform nullability. Local choice imports SHALL NOT depend on a table module being selected.

#### Scenario: Use constants on a forms-only project
- **WHEN** only a form requiring a local choice is generated
- **THEN** its choice module is independently importable and its numeric constants work with standard Xrm attributes

### Requirement: TypeScript forms expose strict snapshot names
Selected Main and Quick Create modules SHALL export FormContext; Quick Views SHALL export QuickFormControl. Name-based attribute/control/tab/section access, including collections, SHALL reject names absent from the generated contract. Ordinary FormXML elements SHALL have non-null accessors; values SHALL retain platform nullability. Sections SHALL be scoped by tab.

#### Scenario: An element is removed
- **WHEN** a form element is removed and models are regenerated
- **THEN** references to its old name fail TypeScript compilation through both convenience accessors and collection APIs
- **AND** no inherited or generic string overload silently accepts the old name

#### Scenario: Values and presence differ
- **WHEN** an ordinary required text attribute exists in FormXML
- **THEN** its attribute accessor is non-null but its value can still be null

#### Scenario: A section belongs to another tab
- **WHEN** a consumer accesses that section through a different tab
- **THEN** compilation rejects the section name

### Requirement: TypeScript form typing preserves deliberate platform access
Generated forms SHALL preserve standard non-name overloads, iteration, filtering, numeric indexing, and specialized Xrm APIs. Consumers SHALL assert the expected form locally and be able to widen explicitly to standard Xrm types for dynamic names. Global getFormContext augmentation, generated event contexts, runtime factories, and runtime identity checks SHALL NOT be introduced.

#### Scenario: Use the dynamic escape hatch
- **WHEN** a consumer assigns the generated form to Xrm.FormContext
- **THEN** standard dynamic string access is available through that explicitly widened value
- **AND** the generated value retains strict access in its original type

#### Scenario: A Quick View dependency is not selected
- **WHEN** a selected form embeds an unselected Quick View
- **THEN** that control uses the standard Xrm QuickFormControl type without expanding selection or failing generation
- **AND** the standard loading lifecycle is preserved

### Requirement: BPF form enrichment is conditional
Selected BPFs SHALL contribute nullable BPF-only attributes and all BPF controls to participating selected Main forms. An attribute also in FormXML SHALL retain its non-null ordinary accessor. No standalone BPF modules, process/stage constants, generated controllers, or additional form selection SHALL be introduced.

#### Scenario: Active process varies at runtime
- **WHEN** selected BPFs contribute controls to a Main form
- **THEN** generated control accessors remain nullable regardless of which process is active

#### Scenario: A BPF field also appears on the form
- **WHEN** its attribute is declared in ordinary FormXML
- **THEN** the attribute accessor remains non-null while its BPF control accessor remains nullable

### Requirement: Model generation excludes testing infrastructure
The new generator SHALL NOT emit test helpers, fixtures, mock adapters, testing dependencies, or Playwright scaffolding. Generated form types SHALL remain usable as erased imports and local assertions in consumer-owned tests. Legacy helper generation SHALL remain unchanged.

#### Scenario: Generate selected forms
- **WHEN** a model recipe selects forms
- **THEN** it produces no xrm-mock integration, test-builder runtime, or testing configuration

### Requirement: TypeScript requests use standard Xrm execution
Selected Custom APIs and exposed platform Web API actions/functions SHALL produce modules exporting Request and applicable Parameters/Response types. Request SHALL implement getMetadata for standard Xrm.WebApi.online.execute. Platform contracts SHALL follow environment CSDL. Empty interfaces, separate message constants, factories, executors, and transport/authentication wrappers SHALL NOT be generated.

#### Scenario: Execute a selected platform function
- **WHEN** RetrieveEnvironmentVariableValue is selected
- **THEN** its Request accepts DefinitionSchemaName and supplies the platform function metadata
- **AND** consumers execute it with standard Xrm APIs and parse/assert its response body themselves

#### Scenario: Preserve omitted inputs
- **WHEN** a request has optional inputs and the consumer omits them
- **THEN** construction does not add defaults, coerce values, or replace omission with null
- **AND** required inputs remain required and explicit null is allowed only by the contract

#### Scenario: No parameters or body exist
- **WHEN** a selected operation has no inputs or response body
- **THEN** no empty Parameters/Response interface is emitted
- **AND** a parameterless request supports `new Request()`

### Requirement: Request Entity types reflect declared contracts
Bound record targets SHALL use table-constrained entityType/id references. Table-constrained Entity payloads SHALL use strongly typed partial record data with applicable platform annotations. Unrestricted Entity payloads SHALL expose their openness through unknown-valued properties, not any or invented business fields. EntityCollection and responses SHALL follow the corresponding declared structure.

#### Scenario: A request needs an unselected table contract
- **WHEN** a table-constrained Entity parameter references a table not selected under tables
- **THEN** the required supporting payload declaration is generated without selecting a standalone table model
- **AND** a compatible selected table contract is reused when available

#### Scenario: The API declares an open Entity parameter
- **WHEN** no business property schema is available
- **THEN** generated typing permits explicit open data with the applicable OData annotations and unknown-valued properties
- **AND** consumers can supply their own narrower business contract

#### Scenario: A complex contract is required
- **WHEN** a request references a complex or enum dependency such as PrincipalAccess
- **THEN** supporting definitions are included and shared without implicitly selecting tables, forms, or other requests

### Requirement: Model request support fails explicitly at its boundary
Both targets SHALL reject explicitly selected classic Actions without parsing workflow XAML. TypeScript SHALL reject SDK messages lacking exposed Web API contracts and unsupported mappings before output changes. Specialized primitive/entity/collection/complex/enum mappings SHALL match verified platform representations rather than guessed any types or implicit runtime conversion.

#### Scenario: Select an unsupported operation
- **WHEN** an exact name identifies a classic Action, an unexposed platform message, or an unsupported TypeScript contract
- **THEN** generation fails before clearing with the operation and unsupported reason identified

### Requirement: Model localization is deterministic
Label generation SHALL use configured language or organization base language, never connection-user preference. Missing usable choice labels SHALL fall back silently to base language and fail if neither is usable. Form generation SHALL fail before output changes when session UI language differs from effective generation language, without changing user settings.

#### Scenario: Only base-language choice text is usable
- **WHEN** the requested-language choice label is missing
- **THEN** the base label names the constant without a warning or omitted numeric value

#### Scenario: Form languages differ
- **WHEN** forms are requested and the session UI language differs from the effective recipe language
- **THEN** generation fails before clearing
- **AND** a recipe without forms does not require this form-language check

### Requirement: Model symbols distinguish generated names from wire names
Schema-derived symbols SHALL preserve publisher prefixes and internal capitalization while splitting underscore segments and uppercasing each segment's initial character. Metadata keys SHALL lowercase the initial character of that result. Form modules SHALL use display names scoped by table/type. Native keyword/property escaping SHALL preserve exact wire keys and mapping values.

#### Scenario: Normalize schema names without a word dictionary
- **WHEN** contoso_order_number, contoso_order_ID, and contoso_ordernumber are normalized
- **THEN** generated PascalCase names are ContosoOrderNumber, ContosoOrderID, and ContosoOrdernumber respectively
- **AND** original logical/schema names remain unchanged in mappings and values

#### Scenario: Escape a language-sensitive name
- **WHEN** a C# identifier is a keyword or a TypeScript wire property requires quoting
- **THEN** native escaping preserves the intended name without renaming the payload

### Requirement: Model collisions resolve deterministically and preserve fixed APIs
Normalized collisions SHALL suffix every metadata-derived group member with `_` plus eight lowercase SHA-256 hex characters over its exact UTF-8 technical name. Fixed/generated/inherited APIs SHALL keep their names while conflicting metadata-derived symbols receive suffixes. Diagnostics SHALL map sources to symbols. Final symbol/path conflicts, including case-insensitive paths, SHALL fail before clearing.

#### Scenario: Metadata order changes
- **WHEN** colliding items are retrieved in a different order
- **THEN** each receives the same suffix rather than encounter-order underscores

#### Scenario: A column conflicts with a fixed member
- **WHEN** its generated property name conflicts with Columns, Choices, or an inherited API in that scope
- **THEN** the column-derived identifier receives a suffix and the fixed API remains intact
- **AND** its platform mapping retains the original name

#### Scenario: A suffix still collides
- **WHEN** final generated symbols or paths remain conflicting
- **THEN** generation fails with conflicting sources identified rather than overwriting, silently lengthening hashes, or assigning a winner

### Requirement: Choice and form collision suffixes retain source identity
Colliding choice members SHALL use `_` plus their numeric values rather than hashes, without a special negative-value naming convention. Colliding form modules SHALL use `_` plus their full form GUID without hyphens. Every collision-group member SHALL be suffixed and unique names SHALL remain unsuffixed.

#### Scenario: Choice labels normalize identically
- **WHEN** values 1 and 2 share a normalized label SomeLabel
- **THEN** their symbols are SomeLabel_1 and SomeLabel_2

#### Scenario: Duplicate form display names appear
- **WHEN** two forms normalize to the same name within a table/type scope
- **THEN** both filenames use full unhyphenated form-ID suffixes
- **AND** the first collision can rename the formerly unsuffixed module

### Requirement: Generated output is compiler-checked rather than universally lint-shaped
TypeScript output SHALL compile under strict checking against supported standard Xrm typings, including representative table/form/request consumers. Documentation SHALL explain excluding owned generated output from style linting. The generator SHALL NOT emit blanket eslint-disable comments or promise compatibility with arbitrary consumer lint rules.

#### Scenario: Consume generated modules
- **WHEN** realistic consumers import generated values and types with strict compilation and library checking enabled
- **THEN** generated forms and requests compile alongside tables and choices
- **AND** runtime values exist rather than appearing only as declarations
