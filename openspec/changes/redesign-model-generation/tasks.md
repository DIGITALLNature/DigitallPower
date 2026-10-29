# Tasks

All work below belongs to the new generator unless explicitly labeled legacy integration. Keep retained `codegeneration`/`cg` output and legacy schemas unchanged. No separate feasibility PoC or reuse of legacy types/templates is required. Each group includes its own tests and directly related documentation; the last group covers cross-feature integration only.

## 1. Module and recipe foundation

- [ ] 1.1 Add `src\modules\dgt.power.modelgeneration` and `tests\dgt.power.modelgeneration.tests` using repository project, licensing, signing, and TUnit conventions; wire solution/host references and verify a focused build includes both projects.
- [ ] 1.2 Implement target-specific version-1 recipe models and loading with required version, .NET target/namespace, optional language/$schema, independent artifact sections, and typed options; the loader accepts the target type from the required `--language` CLI option so recipes need no `type` discriminator; verify loader tests accept both reviewed recipes and property ordering.
- [ ] 1.3 Implement strict recipe diagnostics for unknown/nested/target-inapplicable settings, malformed or unsupported versions, invalid selection shapes, legacy recipes (including V2 configs with a `type` discriminator), recipe/language mismatches (e.g. TypeScript-only properties with `--language dotnet`), and ambiguous duplicate assignments; verify failures occur before connection acquisition or output mutation.
- [ ] 1.4 Add `schemas\modelgeneration\v1\dotnet.schema.json` and `typescript.schema.json` matching runtime properties, required fields (version only; no `type`), defaults, and additional-property rejection; verify positive/negative fixture parity without changing published legacy schemas.
- [ ] 1.5 Document both recipe envelopes, target baselines, independent sections, and schema URLs in README and the applicable migration guide; verify examples against the schemas and loader. Synchronize dependency lock files if project/dependency changes affect the locked graph.
- [ ] 1.6 Add a compatible Scriban dependency to the new module without changing legacy Fluid integration; verify signing/publishing compatibility, the exact package's license/required notices, and locked restore, synchronize affected lock files, and reflect the dependency in the README architecture/dependency description. Identify its notice requirements for the separate release/packaging work without implementing that broader change here.

## 2. Selection and metadata resolution

- [ ] 2.1 Implement exact table/solution resolution and union-minus-exclusion semantics for tables and snapshots, without recipe mutation or implicit related-table selection; verify duplicate/order independence, unknown inclusions/exclusions, valid no-effect exclusions, and partial solution components with metadata-backed tests.
- [ ] 2.2 Implement independent active Main/Quick Create/Quick View selection by table or solution form component and table-wide exclusions; verify solution membership does not select all forms of a referenced table and unsupported/inactive forms are not emitted.
- [ ] 2.3 Resolve exact request names by category and solution Custom API membership, distinguishing platform SDK messages, exposed Web API operations, and classic Actions without workflow XAML parsing; verify explicit Actions fail while solution discovery ignores them without warnings.
- [ ] 2.4 Resolve choice dependencies, explicit additional globals, and selected workflow unique names for BPF form options; verify shared definitions deduplicate and dependencies do not select standalone tables/forms/requests.
- [ ] 2.5 Resolve organization/configured label language and perform the form session-language gate; verify silent base-label fallback, missing usable labels, mismatch failure, no user-setting modification, and no form-language check for recipes without forms.
- [ ] 2.6 Implement actionable reference diagnostics and section-empty/all-empty handling; verify existing solutions with no relevant components warn or fail according to the total result and all failures leave sentinel output untouched.
- [ ] 2.7 Ensure solution/form/request/metadata enumeration retrieves complete required result sets and propagates cancellation/errors; verify multi-page fixtures and cancellation rather than success-shaped defaults.
- [ ] 2.8 Document selection examples and empty-result/reference diagnostics in README and migration guidance; verify examples cover forms-only, choices-only, and snapshots-only generation without implied scope inheritance.

## 3. Identifier and path planning

- [ ] 3.1 Implement schema-derived PascalCase/camelCase conversion, exact wire-name preservation, native C# keyword escaping, and TypeScript property quoting/identifier validation; verify the design examples and language-sensitive names with unit and generated-code compilation tests.
- [ ] 3.2 Implement scope-aware reserved/generated/inherited/enclosing-name handling and `_` plus eight lowercase SHA-256 hex suffixes over exact UTF-8 technical names; verify every collision-group member is suffixed, fixed APIs stay intact, retrieval-order permutations are stable, and diagnostics map each source.
- [ ] 3.3 Implement numeric choice-label suffixes and full unhyphenated GUID form suffixes, scoped by table/form type; verify duplicate labels/forms, unique unsuffixed names, and the first-collision rename without a special negative-value convention.
- [ ] 3.4 Plan all category filenames/import paths and validate final identifier/path uniqueness, including case-insensitive path conflicts and conflicts remaining after suffixes; verify failure identifies sources before clearing and never extends hashes or overwrites a winner.
- [ ] 3.5 Document generated paths, normalization, collision renames, and diagnostic mappings in README/migration examples; verify the examples match naming test expectations.

## 4. Resolved plans and owned output

- [ ] 4.1 Introduce the simplest target-specific resolved plans needed by renderers, containing selected contracts, dependencies, names, and paths; verify plan construction does not mutate the recipe and rendering can complete without Dataverse calls.
- [ ] 4.2 Implement normalized output-path checks for filesystem roots, cwd/lexical ancestors, recipe containment, and linked destinations; verify rejection leaves existing output untouched, including supported directory-link/junction cases.
- [ ] 4.3 Implement owned-directory cleanup without traversing directory links/junctions, followed by direct writes with explicit error/cancellation reporting; verify external target sentinels survive, stale files are cleared, and post-clear failure is reported without staging/rollback.
- [ ] 4.4 Document disposable-directory ownership, one recipe per destination, safeguards and their limits, and regeneration after partial failure; verify help/README examples introduce no wrapper folder, confirmation prompt, manifest, or Git-root requirement.
- [ ] 4.5 Implement embedded native Scriban template parsing/reuse and strict missing-variable/member diagnostics over typed resolved rendering models; verify malformed templates fail before clearing, model mismatches fail explicitly with template context, and renderers require neither metadata access nor recipe mutation.
- [ ] 4.6 Establish layout-only template and whitespace/auto-indentation conventions for both source targets; verify representative loops, optional setters, blank lines, nested blocks, and fragments with render tests plus the per-target compilation gates, and check representative rendering cost without assuming published Liquid-mode results apply to native Scriban. Keep metadata XML on SDK serialization and add no template-override API or generic rendering framework.

## 5. .NET entities, choices, and snapshots

- [ ] 5.1 Render partial SDK Entity classes with metadata-aligned setters, non-virtual properties, nullable partial-record semantics, and native SDK choice/lookup types; verify absent/null/read-only attributes and unknown numeric choices with SDK-backed tests.
- [ ] 5.2 Render direct EntityLogicalName/PrimaryNameAttribute plus nested Columns/Relationships/AlternateKeys/Choices and standalone global Choices classes; verify local/global integer constants and mapped source names compile without enums, forwarding, or inherited-member hiding.
- [ ] 5.3 Render SDK related-data navigation only between selected tables for 1:N, N:1, and N:N; verify unselected-target lookups/constants remain available, collection assignment uses SDK semantics, and navigation performs no retrieval or implicit synchronization.
- [ ] 5.4 Add compilation gates for representative `netframework` and `dotnet` outputs against the exact C# and API baselines; verify entities/constants compile without LangVersion overrides, generated service contexts, type codes, forced setters, or duplicate raw/typed choice accessors.
- [ ] 5.5 Emit full EntityMetadata data-contract XML snapshots independently of C# classes; verify direct deserialization and representative attributes/keys/relationships, logical-name filenames, and snapshots-only recipes.
- [ ] 5.6 Document .NET consumer examples, category namespaces, removed legacy conveniences, SDK null/value behavior, and XML consumption in README/migration guidance; verify examples against generated code and round-trip fixtures.

## 6. .NET Custom API requests and message names

- [ ] 6.1 Render SDK-compatible Custom API request/response contracts with parameter/response mappings for both compatibility targets; verify generated consumers compile and SDK request/response parameter storage behaves correctly.
- [ ] 6.2 Render one static MessageNames in Requests for explicitly selected Custom APIs/platform messages only; verify WhoAmI adds a constant but no request wrapper, no default catalogue appears, and separately shipped common constants remain untouched.
- [ ] 6.3 Document `requests` selection, Custom API versus platform-message output, and migration from default message catalogues; verify examples compile and do not imply classic Action support.

## 7. TypeScript tables and choices

- [ ] 7.1 Render importable table modules with one wire interface and same-named runtime metadata const; verify partial projections, exact property names, API nullability, metadata-derived entity-set names, and real emitted runtime values with strict compilation/runtime checks.
- [ ] 7.2 Implement ordinary scalar/choice/date/multi-choice mappings and independent lookup-ID/bind/selected-target expansion contracts using actual polymorphic navigation metadata; verify representative Web API payloads and no implicit target model, converters, bind builders, or any fallbacks.
- [ ] 7.3 Render global/local as-const choice objects and derived union types while leaving record/form values SDK-aligned numeric types; verify shared choice deduplication, direct imports, and forms-only local choice consumption.
- [ ] 7.4 Document table metadata versus wire keys, lookup/bind/expansion usage, choice imports, and generated-directory lint exclusions; verify examples compile strictly with library checking enabled and no blanket eslint-disable output.

## 8. TypeScript strict forms and BPF enrichment

- [ ] 8.1 Render per-form FormContext/QuickFormControl modules with non-null ordinary FormXML accessors and platform-nullable values, preserving standard specialized APIs; verify Main/Quick Create/Quick View consumers compile against standard Xrm typings.
- [ ] 8.2 Make name-based attributes/controls/tabs/sections and collection APIs strict without generic or inherited string loopholes; verify removed/renamed elements and wrong-tab sections cause expected compilation errors while numeric/all-items/filter/iteration access still works.
- [ ] 8.3 Preserve explicit widening to standard Xrm types and local event-context assertions without global augmentation or generated event types; verify dynamic access works only through the deliberate escape hatch and ordinary consumers remain assignable.
- [ ] 8.4 Type selected embedded Quick Views and retain standard QuickFormControl for unselected references; verify no selection expansion/failure and preservation of loading APIs.
- [ ] 8.5 Enrich only participating selected Main forms from included BPFs, keeping BPF-only attributes and all process controls nullable while ordinary attributes stay non-null; verify multiple processes, unselected participants, and no Quick Create/Quick View enrichment or standalone BPF artifacts.
- [ ] 8.6 Verify generated output contains no test helpers, mock adapters, fixtures, testing dependencies, or Playwright scaffolding; document snapshot regeneration, erased imports/local assertions, and consumer-owned testing in README/migration guidance with strict-compilation examples.

## 9. TypeScript executable request contracts

- [ ] 9.1 Retrieve and parse connected-environment CSDL for selected platform contracts and dependencies without exposing generated transport infrastructure; verify fixture-based binding/action/function/type resolution and actionable failures for unexposed SDK messages.
- [ ] 9.2 Render Request/getMetadata and applicable Parameters/body Response exports, including parameterless requests; verify strict Xrm execute compatibility, required/optional inputs, omission versus null, runtime metadata, and caller-owned response parsing.
- [ ] 9.3 Implement bound record references, table-constrained Entity payload dependencies, unrestricted unknown-valued Entity payloads with platform annotations, and EntityCollection equivalents; verify compatible selected-table reuse and no implicit standalone artifact selection.
- [ ] 9.4 Resolve/deduplicate complex and enum supporting contracts such as PrincipalAccess and verify actual serialized shapes; cover primitive/collection responses and dependency cycles with supported examples or explicit pre-clear rejection.
- [ ] 9.5 Exercise specialized Int64 precision, binary/file/image, collection binding, overload resolution, nullable inputs, and non-object responses against authoritative samples/metadata and standard-client behavior; add faithful mappings and tests where verified, otherwise deliver named unsupported diagnostics and document limits before release rather than guessing any/bigint/conversion shapes.
- [ ] 9.6 Add representative RetrieveEnvironmentVariableValue and bound/unbound Custom API consumers with serialization tests and reproducible live-environment execution cases; verify standard Xrm execution without executors/factories/message-name exports and record unavailable live validation explicitly.
- [ ] 9.7 Document generated request usage, open versus closed Entity contracts, supporting dependencies, verified support boundaries, and migration from legacy wrappers in README/migration guidance; verify examples compile and match request metadata fixtures.

## 10. CLI and legacy integration

- [ ] 10.1 Register `model generate` through CommandTree and host DI with required named options (`--language`, `--config/-c`, `--output/-o`) and lazy IDataverseConnection acquisition after local validation; verify command tests for both targets, cancellation, missing/invalid `--language`, recipe/language mismatch before authentication, and injected console diagnostics.
- [ ] 10.2 Add CommandTreeTests coverage for model/model generate and SettingsParsingTests coverage for the required `--language` option, long/short config/output options, existing connection options, missing options, and rejected positional/removed settings; verify runtime help and completion metadata share the registration.
- [ ] 10.3 Mark legacy settings through the existing DeprecatedCommandAttribute/DeprecationInterceptor mechanism; verify codegeneration/cg still run legacy recipes with hints and unchanged output, including legacy helpers and V2 schemas.
- [ ] 10.4 Update README command navigation, examples, completion table, architecture/project layout, and migration guide as the CLI lands; verify documented commands/settings parse and all retained/new schema links resolve locally.

## 11. Cross-feature acceptance

- [ ] 11.1 Run complete representative .NET and TypeScript recipes through model generate with fixture-backed Dataverse responses, regenerate twice, and vary metadata ordering; verify deterministic output, direct category layouts, all-artifact and single-artifact recipes, and pre-clear failure sentinels.
- [ ] 11.2 Run the focused model-generation, CLI, and retained codegeneration suites plus compiler gates using existing tooling; verify all pass, both generated .NET baselines compile, and strict TypeScript consumers cover tables/forms/requests rather than only constants.
- [ ] 11.3 Verify `dotnet build`, applicable repository formatting/analyzer checks, and locked restore after any graph changes; fix new findings in code, without editing CHANGELOG or the Qodana baseline.
- [ ] 11.4 Reconcile implemented behavior and documented support limits with these delta specs, run `pnpm exec openspec validate --all`, and refresh generated instructions with `pnpm run openspec:update`; verify no remaining unchecked acceptance work or unrecorded contract changes before review.

## Workflow follow-up

- Address dependency-wide license inventory, bundled notice files, checks of actual distributed artifacts, and the MS-PL/MS-RL declaration mismatch in a separate release/packaging change; confirm the selected Scriban dependency's notice obligations are covered before release.
- Review the completed implementation and explicitly reported live-environment validation limits before release.
- Synchronize current specs with implemented behavior and archive through the OpenSpec archive workflow after acceptance; preserve retained-generator requirements.
- Consider legacy removal only in a future major release after migration experience, without promising a removal date in this change.
