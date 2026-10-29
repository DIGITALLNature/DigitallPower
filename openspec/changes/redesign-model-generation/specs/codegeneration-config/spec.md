# Code Generation Configuration Delta

## MODIFIED Requirements

### Requirement: V2 configurations select one output target
For retained `codegeneration`/`cg` commands, a V2 configuration MUST set `version` to `2` and MUST select exactly one supported target through the case-sensitive discriminator property `type`, with the case-sensitive value `dotnet` or `typescript`. The discriminator MUST be accepted regardless of its position in the root object. Shared entity, request, and option-set scope MUST be resolved independently from target-specific output settings.

#### Scenario: A supported V2 configuration is loaded
- **WHEN** a V2 configuration declares `type` as `dotnet` or `typescript`
- **THEN** it is deserialized into the matching typed configuration model

#### Scenario: V2 target is missing or unsupported
- **WHEN** a V2 configuration has no string `type` or declares another value
- **THEN** configuration resolution fails with an actionable error

#### Scenario: The target discriminator follows other settings
- **WHEN** a supported V2 configuration places `type` after `$schema`, `version`, or ordinary output settings
- **THEN** it is deserialized into the matching typed configuration model without reordering the file

#### Scenario: The target discriminator has incorrect casing
- **WHEN** a configuration supplies only `Type` or `TYPE` instead of `type`, or uses a target value such as `DotNet` or `TypeScript`
- **THEN** configuration resolution fails instead of normalizing the discriminator

### Requirement: Unsupported configuration versions fail explicitly
For retained `codegeneration`/`cg` commands, configuration loading MUST require an explicit integer `version` of `2`. Missing, malformed, legacy, and unsupported versions MUST fail before authentication, metadata retrieval, or output changes. Errors MUST identify the supported format; legacy and missing-version errors MUST direct users to the 2.x-to-3.x migration guide.

#### Scenario: An unknown version is loaded
- **WHEN** a configuration declares an unsupported version
- **THEN** resolution fails and identifies version 2 as the supported format

#### Scenario: A V1 configuration is loaded
- **WHEN** a configuration declares `version` equal to `1`
- **THEN** resolution fails with migration guidance instead of adapting or executing the configuration
- **AND** no Dataverse authentication or output changes occur

#### Scenario: A version is omitted
- **WHEN** a configuration omits `version`, even if it has a valid `type`
- **THEN** resolution fails with migration guidance rather than inferring a format

#### Scenario: A version has an invalid representation
- **WHEN** `version` is null, a string, a boolean, an object, an array, or a non-integer number
- **THEN** resolution fails with an actionable version-validation error

### Requirement: Ambiguous configuration assignments fail explicitly
For retained `codegeneration`/`cg` commands, configuration loading MUST reject duplicate assignments to the target discriminator or bound configuration properties before authentication, metadata retrieval, or output changes. Ordinary configuration properties MUST retain case-insensitive binding, so casing variants MUST NOT bypass duplicate-assignment rejection.

#### Scenario: A discriminator is repeated
- **WHEN** a configuration repeats `type`, whether the values agree or conflict
- **THEN** configuration resolution fails rather than selecting the first or last target

#### Scenario: A configuration property is repeated
- **WHEN** a configuration repeats a bound property such as `version`, including a case variant such as `VERSION`
- **THEN** configuration resolution fails rather than validating one assignment and using another

### Requirement: Schemas require the runtime configuration contract
Legacy V2 target schemas MUST require explicit `version` and `type` fields and describe their supported settings and defaults. `$schema` annotations MUST remain usable. Retired codegeneration V1 schemas MUST NOT be presented as supported by dgtp 3. The independent modelgeneration V1 schema family MUST NOT change legacy schema URLs or command behavior.

#### Scenario: A config lacks a required discriminator
- **WHEN** a legacy V2 config omits `version` or `type`
- **THEN** both legacy runtime loading and validation against the supported legacy target schema reject it

#### Scenario: A documented config is validated
- **WHEN** a current README legacy-generation example is loaded and validated against its declared schema
- **THEN** both accept its supported version, target, and settings

## ADDED Requirements

### Requirement: Model recipes have an independent versioned envelope
`model generate` SHALL require integer `version: 1` in the recipe. The target SHALL be selected exclusively through the required `--language` CLI option (`dotnet` or `typescript`), not through the recipe. Recipes SHALL NOT contain a `type` property. `$schema` SHALL be optional editor metadata. Missing, invalid, unsupported, or legacy recipe formats SHALL fail before authentication or output changes, without inferred versions or automatic conversion.

#### Scenario: Recipe is loaded for the selected target
- **WHEN** a model recipe supplies version 1 and `--language` selects the target, regardless of property order
- **THEN** the recipe is deserialized into the matching target-specific recipe model

#### Scenario: Reject incompatible recipes
- **WHEN** a recipe omits its version, uses an unsupported version, or supplies a legacy V2 configuration
- **THEN** loading fails with supported-format and applicable migration guidance before authentication or output changes

#### Scenario: Recipe contains a type property
- **WHEN** a recipe contains a `type` property
- **THEN** loading fails with an actionable error indicating that target selection is done via `--language`, not the recipe

### Requirement: Recipe loading validates target compatibility
The recipe SHALL be deserialized into the target-specific model selected by `--language`. Settings that are incompatible with the selected target SHALL fail before authentication or output changes. A .NET recipe contains `namespace`, `target`, and optionally `metadata`; a TypeScript recipe contains `forms` and rejects `namespace`, `target`, and `metadata`.

#### Scenario: TypeScript recipe loaded with --language dotnet
- **WHEN** a recipe containing `forms` or other TypeScript-only settings is loaded with `--language dotnet`
- **THEN** loading fails with an actionable error identifying the incompatible setting
- **AND** no authentication or output changes occur

#### Scenario: .NET recipe loaded with --language typescript
- **WHEN** a recipe containing `namespace`, `target`, or `metadata` is loaded with `--language typescript`
- **THEN** loading fails with an actionable error identifying the incompatible setting
- **AND** no authentication or output changes occur

#### Scenario: Matching recipe and language succeed
- **WHEN** a recipe's settings are compatible with the selected `--language`
- **THEN** loading succeeds and produces the matching target recipe model

### Requirement: Model recipe schemas and loading reject unsupported settings
Model recipe schemas SHALL live under `schemas/modelgeneration/v1/` with separate .NET and TypeScript schemas. Schemas and runtime loading SHALL reject additional properties at every level and require the same target settings. Unknown settings SHALL NOT be silently ignored.

#### Scenario: A nested option is misspelled
- **WHEN** a recipe contains an unknown property inside an artifact's options or selection
- **THEN** schema validation and runtime loading reject it with an actionable error
- **AND** runtime rejection occurs before authentication or output changes

#### Scenario: Current examples validate
- **WHEN** either target's documented model recipe is validated and loaded with the matching `--language`
- **THEN** its optional `$schema` and supported settings are accepted by both

### Requirement: Model compatibility settings are target-specific
.NET recipes SHALL require `namespace` and `target` of `netframework` or `dotnet`. TypeScript recipes SHALL NOT accept `namespace` or `target`. Both targets SHALL support optional `language` for localized metadata labels, defaulting to the organization base language. Output location SHALL belong exclusively to the CLI.

#### Scenario: Select .NET compatibility
- **WHEN** a .NET recipe supplies its namespace and supported target and `--language dotnet` is used
- **THEN** the target controls generated syntax/API compatibility, not plugin/client model behavior

#### Scenario: Unsupported target settings are supplied
- **WHEN** .NET omits its namespace/target or TypeScript supplies either setting
- **THEN** recipe validation fails before authentication or output changes

### Requirement: Model artifact selection is explicit and independent
Both targets SHALL support `tables`, `requests`, and `choices`; .NET SHALL additionally support `metadata`, and TypeScript `forms`. Omission SHALL disable an artifact, except required supporting dependencies. Present sections SHALL require explicit includes, with supported exclusions and typed options. Shared scope, Boolean/empty opt-ins, wildcards, and publisher-prefix selection SHALL NOT be supported.

#### Scenario: Generate forms without table models
- **WHEN** only forms are selected
- **THEN** form contracts and their required dependencies are generated without standalone table models

#### Scenario: Artifact options are supplied
- **WHEN** a supported option appears under an artifact's `options`
- **THEN** it influences that artifact without changing other sections' selection
- **AND** arbitrary extension properties are rejected

### Requirement: Table and snapshot selection combines exact names and solutions
Table models and .NET snapshots SHALL include by `include.tables` logical names and `include.solutions` unique names and exclude by `exclude.tables`. Includes SHALL be unioned and deduplicated before exclusions, independently of order. A selected table SHALL use its full model metadata, not a solution-component column subset.

#### Scenario: Include and exclude overlap
- **WHEN** a table is included directly and through a solution but also excluded
- **THEN** no artifact for that table is produced in that section
- **AND** another section's selection is unaffected

#### Scenario: A solution contains a partial table component
- **WHEN** table selection discovers that table through solution membership
- **THEN** the generated model covers the table definition rather than only the included columns

### Requirement: Form selection follows table or form-component scope
Forms SHALL include all supported active Main, Quick Create, and Quick View forms for `include.tables`, or only solution form components for `include.solutions`. `exclude.tables` SHALL remove all selected forms for those tables. Individual form-name/ID/type selectors and automatic related-form/table selection SHALL NOT be supported.

#### Scenario: Select solution form components
- **WHEN** a solution contains one of several forms for a table
- **THEN** solution-based selection includes that form, not every form of the referenced table

#### Scenario: Exclude a table's forms
- **WHEN** a table is in `forms.exclude.tables`
- **THEN** its selected forms are excluded regardless of include source

### Requirement: Request and additional choice selection stays minimal
Requests SHALL include exact names through `include.names` and solution Custom APIs through `include.solutions`, subtracting `exclude.names`. Additional global choices SHALL use an exact technical-name `include` array without solution/exclusion selectors. Required choices SHALL be automatic deduplicated dependencies and SHALL NOT be excluded.

#### Scenario: Select a solution containing other operation categories
- **WHEN** request selection names a solution containing Custom APIs and classic Actions
- **THEN** only its Custom APIs are discovered, without warnings merely because Actions exist
- **AND** platform operations are not automatically discovered

#### Scenario: Choices are shared dependencies
- **WHEN** multiple requested artifacts require the same global choice
- **THEN** its definition is emitted once without a duplicate explicit choice selection

### Requirement: Business process flows are form enrichment options
`forms.options.businessProcessFlows.include` SHALL accept exact workflow unique names. Selected processes SHALL enrich only selected Main forms for their participating tables. The option SHALL NOT select additional tables/forms or request standalone process artifacts.

#### Scenario: A process spans selected and unselected tables
- **WHEN** the process is included for form enrichment
- **THEN** only already-selected participating Main forms are enriched
- **AND** Quick Create and Quick View forms are not enriched

### Requirement: Selection references and empty results are checked before clearing
All explicit references, including exclusions and BPFs, SHALL resolve before output changes. Unknown references SHALL fail; existing exclusions outside the included set SHALL be valid. Empty requested sections SHALL warn when others produce artifacts; an entirely empty result SHALL fail before clearing.

#### Scenario: An exclusion names an unknown table
- **WHEN** that name cannot be resolved
- **THEN** generation fails and existing output remains untouched

#### Scenario: An exclusion is valid but has no effect
- **WHEN** an existing table is excluded without being included
- **THEN** the exclusion is accepted

#### Scenario: A solution has no relevant components
- **WHEN** an existing selected solution yields no matching artifacts
- **THEN** it is not reported as an unknown solution
- **AND** generation warns or fails according to whether other sections produce output
