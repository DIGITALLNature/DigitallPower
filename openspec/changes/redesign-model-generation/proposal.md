# Proposal

## Why

The current `codegeneration` command exposes a generation mechanism rather than a resource-oriented workflow. Redesigning it around `model` provides an opportunity to reconsider the CLI, configuration contract, and module API together instead of carrying existing boundaries into a new command name.

## What Changes

- Introduce one `model generate` command for both .NET and TypeScript, selected through the validated config rather than target subcommands.
- Implement it in a new `dgt.power.modelgeneration` project with matching tests. Retain existing `codegeneration` / `cg` behavior during transition and add a deprecation hint through the existing interceptor mechanism.
- Make the config file the shared, authoritative contract for what is generated and how. One config describes one target (.NET or TypeScript), and the two target shapes evolve independently. No config-free generation or generation-setting CLI overrides are planned; individual settings may still move to CLI parameters.
- Require named `--config/-c` and `--output/-o` options, with no positional arguments. The supplied output directory is the owned generation root, without automatic `Model`/`DotNet`/`TypeScript` wrappers or a separate `--folder` option. Apply minimal root/working-directory/recipe-containment checks, reject linked destinations, and never follow directory links/junctions during cleanup, without a confirmation flag/prompt or comprehensive safety guarantee.
- Reject additional config properties in schemas and at runtime. Resolve selection, metadata, dependencies, names, and paths into a target-specific plan before clearing output. Rendering performs no Dataverse calls or recipe mutation. Render/write directly; incomplete output after subsequent failure is an accepted risk, with no staging or rollback.
- Select artifacts explicitly and independently through sections containing their own selections, without shared scope, empty-object opt-in, or Boolean shorthand. Both targets use `tables`, `requests`, and `choices`; TypeScript adds `forms`, and .NET adds `metadata`. Use `include`, supported `exclude`, and optional typed `options` within artifact sections. Use a new `modelgeneration/v1` schema family with required `type`/`version` fields, independently evolving target versions, and optional `$schema`; preserve legacy schema URLs.
- Use minimal artifact-specific selection, without wildcards or publisher-prefix selectors. Tables, forms, and snapshots include by table names/solution membership and exclude by table names; requests include by exact names/solution Custom API membership and exclude by exact names. Additional global choices use explicit names only.
- Combine includes by union, then subtract exclusions, independent of order. Accept selection repetition for transparency. Fail on unknown references or an entirely empty generation; warn for individual empty sections.
- Select active Main, Quick Create, and Quick View forms by table or solution form-component membership, without individual form-name/ID/type filters. Use display-name-derived module names, stable ID suffixes for collisions, and a pre-write session/generation-language check.
- **BREAKING for migrating consumers**: Generate TypeScript exported modules for bundler-based consumers instead of ambient generated namespaces. Use `tables`, `forms`, `choices`, and `requests` folders, one module per item, and direct imports without initial barrel files. Preserve technical filenames except display-name-based forms; document excluding generated output from style linting while retaining strict compilation.
- Generate one TypeScript Web API record interface per table plus a same-named runtime identifier value, such as `Account` in both type/value spaces. Preserve wire names/representations, including separate lookup-ID/bind properties and string multi-select values; type expanded navigation only between selected tables.
- Keep form typings platform-aligned: full contexts for Main/Quick Create, control types for Quick Views, non-null ordinary FormXML accessors, and nullable runtime-conditional elements. Restrict name-based attribute/control/tab/section access and collection APIs to generated names so removed elements are caught after regeneration and compilation. Dynamic access requires an explicit opt-out to standard Xrm types. Use local `as` assertions, without global Xrm augmentation or generated event-context types. Unselected referenced Quick Views retain standard Xrm types.
- Select BPFs by workflow unique name under `forms.options.businessProcessFlows.include`, solely for nullable enrichment of selected participating Main forms. Generate no standalone BPF artifacts, process/stage constants, runtime abstractions, or implicit form/table selection.
- Generate no test helpers, fixtures, mock adapters, testing dependencies, or replacement Playwright scaffolding. Consumers can unit-test their own libraries and use real-app browser testing independently; legacy helpers remain unchanged.
- Generate TypeScript requests for Custom APIs and explicitly selected platform Web API actions/functions, using environment CSDL contracts where needed. Export `Parameters`, executable `Request`, and body-only `Response` where applicable; omit empty interfaces and preserve optional-input omission. Include necessary supporting contract types, not unrelated artifacts, with no generated execution layer.
- Strongly type table-constrained Entity payloads using request dependencies without implicitly selecting standalone table models. Represent genuinely unrestricted Entity data explicitly with unknown-valued properties rather than `any` or invented business schemas. Verify specialized mappings with the first implementation and fail before output changes for unsupported contracts.
- **BREAKING for migrating consumers**: The new generator does not append a default .NET SDK message list. A single explicit selection generates custom-operation contracts and names, or platform-message names only; SDK platform wrappers are not regenerated. Track the existing default-message discussion in [#181](https://github.com/DIGITALLNature/DigitallPower/issues/181).
- Include choices required by selected artifacts automatically and deduplicate them, with independent explicit-name selection of additional global choices. TypeScript uses PascalCase `as const` objects with same-named derived unions, local modules grouped by table, and standard numeric form/single-choice record values initially.
- **BREAKING for migrating consumers**: Drop classic Action request/response and name generation in the new implementation. Explicit Action names fail before output changes; solution-based Custom API discovery excludes them. Do not parse workflow XAML.
- Require .NET `target` of `netframework` (net462-compatible APIs/C# 7.3) or `dotnet` (.NET 10/C# 14), controlling syntax/API compatibility only, not consumer role. Framework-based external clients are supported by the former; TypeScript has no `target` setting initially.
- Require the .NET root namespace. Derive entity/property names from schema names using deterministic best-effort PascalCase, retaining publisher prefixes and existing internal capitalization.
- Use .NET `Entities`, global `Choices`, and `Requests` subnamespaces/folders, plus `Metadata` for XML snapshots. Keep local choice constants nested under the entity's `Choices`, alongside `Columns`, `Relationships`, and `AlternateKeys` groups. Follow current Dataverse terminology for names we design while retaining SDK-defined types/members. Keep requests/responses and a single static `MessageNames` constants class together in `Requests`; TypeScript has no separate message-name constants export.
- Keep .NET entities thin and SDK-compatible: metadata-aligned setters, non-virtual properties, useful identifiers included, and typed standard-SDK navigation between selected tables without custom collections or implicit loading. Remove forced-setter and virtual-property switches.
- **BREAKING for migrating consumers**: Do not generate service contexts or numeric entity type-code constants in the new implementation.
- Retain metadata XML snapshots as an independent opt-in artifact for consuming test libraries, preserving SDK `EntityMetadata` data-contract XML, one logical-name file per table, without wrappers or re-import functionality.
- Keep .NET `OptionSetValue` / `OptionSetValueCollection` entity properties and generate integer constants rather than enums, avoiding conversion accessors and casts.
- Default label generation to organization base language, allow an override, silently fall back to base labels, and fail if neither label exists. Resolve colliding choice member names using numeric values.
- Use language-native keyword/property escaping while preserving wire names. Resolve normalized-name collisions with `_` plus eight lowercase SHA-256 hex characters derived from the exact UTF-8 technical name; preserve fixed/inherited APIs and suffix conflicting metadata-derived symbols. Choice collisions use numeric suffixes and form collisions use full unhyphenated GUID suffixes. Suffix every metadata-derived collision-group member, emit diagnostic mappings, and fail before clearing for remaining symbol/path conflicts, including case-insensitive path collisions.
- Design the new module independently of legacy contracts. Reuse logic/classes only where it genuinely helps without compromising the new module. Keep legacy maintenance-only, with new features in the replacement; consider removal in a future major release after migration experience, without a fixed date. Separately shipped message constants remain unchanged by this redesign.
- Document user-affecting breaking changes and migration steps.

The user-facing recipe, representative consumer surfaces, and naming policies have been reviewed and are ready to translate into delta specs and implementation tasks. Strict-form compatibility, specialized request mappings, and filesystem behavior require implementation tests and refinement against real generated output, not separate PoCs or a requirement to reuse legacy internals. Unsupported contracts must still fail explicitly before output changes. Differences listed above apply to the new generator; retaining legacy commands gives consumers a migration path.

## Capabilities

### New Capabilities

None identified at this stage. Reuse the existing code-generation capability boundaries unless further discovery establishes a distinct behavior.

### Modified Capabilities

- `cli-command-surface`: Introduce `model generate` and deprecate retained `codegeneration` / `cg`, with matching help, completion, and parsing coverage.
- `codegeneration-config`: Define the new independent recipe contracts, explicit paths, required .NET target/namespace, strict properties, artifact options, and selection semantics while preserving legacy configuration behavior.
- `codegeneration-output`: Describe the new generator's output alongside retained legacy behavior: TypeScript record/form modules, Custom API/platform Web API requests and supporting contracts, strict form access and BPF enrichment without standalone BPF artifacts or test helpers, choice dependencies, thin .NET entities/requests/message names, deterministic naming, and independent metadata snapshots.

Existing main specs remain the current behavior until implementation and spec synchronization. Final delta specs must distinguish new and legacy generator behavior rather than overwrite legacy requirements as though its commands had been removed.

## Impact

- CLI registration in `src/dgt.power/CommandTree.cs` and structural/settings-parsing tests in `tests/dgt.power.cli.tests`.
- New generation implementation in `src/modules/dgt.power.modelgeneration`, with tests in `tests/dgt.power.modelgeneration.tests` and host/solution project wiring.
- Legacy `src/modules/dgt.power.codegeneration` settings annotated for the existing `DeprecationInterceptor`; existing generation behavior retained.
- New target-specific schemas under `schemas/modelgeneration/v1/`, preserving published `schemas/codegeneration/v2/` contracts.
- Generated-code consumer migration, TypeScript templates/support files, and compilation/runtime coverage against standard Xrm typings.
- Web API CSDL retrieval/parsing and operation-contract dependency resolution; no transport/execution abstraction in generated output.
- README usage examples, shell-completion examples, and `docs/migrations/2.x-to-3.x.md` for applicable user-facing breaking changes.
- No dependency additions or legacy project renames are decided. No linguistic naming dependency is needed for the agreed schema-name conversion.

## References

- [#181 - Hardcoded default SDK message list](https://github.com/DIGITALLNature/DigitallPower/issues/181): generation defaults and compatibility implications, including the separately shipped public constants in `dgt.power.common`. This redesign does not implicitly remove those library constants or resolve the issue's full scope.
