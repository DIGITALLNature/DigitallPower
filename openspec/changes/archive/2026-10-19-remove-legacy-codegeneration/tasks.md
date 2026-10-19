# Tasks

## 1. Establish the retained contract

- [x] 1.1 Review remaining typed-config fields and their actual consumers, particularly shared/hidden namespace properties, for compatibility-only behavior; record concrete keep/remove decisions in design.md. Verify each proposed removal against source, schemas, and fixtures. Retain V2 unless an incompatible cleanup is agreed; if needed, revise this change's contract and schema-version plan to V3 before implementing it.
- [x] 1.2 Add or adapt typed-config regression fixtures for both .NET targets and TypeScript entities/forms, options, messages, parameterized requests, and optional helpers before consolidation; verify the current surviving pipelines pass these cases and retain expected artifact names and contents.

## 2. Remove legacy execution and consolidate generators

- [x] 2.1 Move the surviving typed-config TypeScript pipeline into TypeScriptGenerator and remove Full/Light dispatch, strategy types, and the mode enum; migrate relevant tests and verify one implementation per language produces the baseline artifacts without strategy selection.
- [x] 2.2 Remove the V1 model, mappings, config-result union branches, command routing, and legacy generator contracts; remove CodeGenerationConfigFactory and replace manual target selection with JsonPolymorphic/JsonDerivedType registrations on CodeGenerationConfigBase and standard base-type deserialization. Keep only a thin loading/validation boundary, with no custom polymorphic converter or discriminator normalization. Update test contexts and dependent call sites; verify command and generator tests compile and pass.
- [x] 2.3 Require explicit supported integer version and target before connection acquisition; preserve migration errors for absent/V1 versions and rejection tests for unknown, null, non-integer, and incorrectly typed versions. Require standard lowercase type discriminator names and values. Verify command-level failures do not acquire a connection or change existing output files.
- [x] 2.4 Remove attribute-filter support and reject retired filter/mode settings even in typed configs; verify casing variants fail actionably and normal `$schema` annotations remain accepted. Remove filter-only tests while retaining shared behavior coverage.
- [x] 2.5 Update supported target schemas to require version and type, synchronize any model changes, and remove Full/Light descriptions. Remove local V1 schemas if no retained consumer needs them; if retained solely for historical links, clearly identify them as historical. Verify supported examples validate and missing-version examples fail.
- [x] 2.6 Add the removal and config migration section to docs/migrations/2.x-to-3.x.md with before/after configs, separate target invocations, field/suppression/default mappings, no filter replacement, and Full-output consumer changes. Include removal of the public config factory API and the standard lowercase type discriminator contract. Remove obsolete V1 usage guidance from the README and verify examples match the retained contract.
- [x] 2.7 Enable AllowOutOfOrderMetadataProperties and disable AllowDuplicateProperties for native config deserialization. Test both targets with type first, after $schema/version, and after ordinary settings; reject duplicate type/version assignments, case variants of bound ordinary properties, and missing/invalid/incorrectly cased discriminators. Preserve $schema acceptance, case-insensitive ordinary-property binding, unrelated parser allowances, and targeted retired-setting errors.

## 3. Remove obsolete supporting code and finish user documentation

- [x] 3.1 Remove exclusively legacy metadata overloads, template models, Templates/ts resources, constants, and unreferenced fixtures; retain helpers used by typed-config forms/BPF metadata. Verify surviving call sites, embedded-resource loading tests, and template contract/diagnostics tests pass.
- [x] 3.2 Remove dependencies only where exclusive obsolete usage is established, synchronizing affected lock files when necessary; verify the relevant locked restore succeeds if dependencies changed and the module builds without removed references.
- [x] 3.3 Migrate remaining useful coverage from legacy entry points and retire Full-only scenarios; verify localization, naming, determinism, render diagnostics, existing TypeScript compilation gates, and helper behavior on the retained generator.
- [x] 3.4 Complete the concise README code-generation overhaul with supported outputs, prerequisites, one minimal config per target, invocations, and schema/migration links; verify its table of contents, examples, and links. Keep complete upgrade details in the existing major-version guide, not a second reference table in the README.
- [x] 3.5 Document existing generated-output cleanup boundaries and backup guidance in the migration guide, without adding broader deletion behavior or runtime-footprint claims; verify against the retained directory-preparation implementation and identify any obsolete Full artifacts outside that boundary.

## 4. Integration and specification alignment

- [x] 4.1 Run the complete code-generation module tests and existing relevant TypeScript compile checks after native polymorphic loading is implemented, then dotnet build; verify no new failures or build errors and review generated-output diffs for unintended changes.
- [x] 4.2 Verify no production V1 execution, manual config-target factory, Full/Light selector, attribute-filter support, obsolete strategy, or legacy embedded template remains; check retained historical references occur only where needed for migration, rejection tests, or history. Confirm CLI paths/options and the internal module name are unchanged.
- [x] 4.3 Sync the implemented delta specs, including polymorphic-loading behavior, to main specs and run pnpm exec openspec validate --all; verify current specs describe the implemented contract and the migration guide gives a direct 2.x-to-3.x path without an intermediate upgrade.

## Workflow follow-up

- Archive this change only after implementation and review are complete.
- If the separate resource-oriented design also ships in dgtp 3, reconcile its migration instructions with the same 2.x-to-3.x guide before release.
