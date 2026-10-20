# Design

## Context

See proposal.md for motivation. Current `CodeGenerationCommand` routes between V1 and typed configurations. V1 .NET already maps into `DotNetGenerator`, whereas `TypeScriptGenerator` delegates to Full or Light strategies. The Light strategy contains separate V1 and typed-config entry points.

`MetadataService.Legacy.cs`, configuration filter types, `TsLiquidTemplateModelFactory`, and `Templates/ts` support legacy generation; `Templates/tsl` supports the surviving TypeScript pipeline. The project embeds both template sets. Tests include V1 mapping and generator fixtures as well as template, determinism, diagnostics, and TypeScript compilation coverage.

The factory currently defaults a missing version to 1. V2 schemas require only `type`, despite runtime routing needing explicit `version: 2`. The shared config exposes an unused TypeScript namespace and .NET hides the base namespace property.

## Goals / Non-Goals

**Goals:** Remove obsolete branches completely, retain useful coverage on the surviving entry points, and make format rejection happen before connection acquisition or filesystem mutation.

**Non-Goals:** No resource repositories, async metadata conversion, new artifact abstraction, module split, command-tree redesign, blanket output renaming, or automatic config converter. Existing .NET Framework output is useful target support, not the legacy generation being removed.

## Decisions

### 1. Consolidate the existing pipeline rather than replace it

Move the typed-config Light implementation into the existing `TypeScriptGenerator`. Remove its V1 methods, `TypescriptFullGenerationStrategy`, `ITypescriptGenerationStrategy`, and the mode enum. Fold strategy-base functionality into the concrete generator where it has no other surviving consumer. Retain small rendering/file helpers only where justified by actual reuse.

Keep `DotNetGenerator` and language-specific DI contracts; one implementation per language does not require eliminating useful interfaces. Rename surviving symbols/files that describe removed choices. Do not rename `Templates/tsl`, resource identifiers, environment variables, or established public paths solely to erase a historical abbreviation; those can remain stable without offering a Light mode.

Alternative rejected: retaining a renamed strategy layer around one implementation. It preserves indirection without a supported choice. Rewriting the generators is also rejected because it adds unrelated output risk.

### 2. Remove compatibility-only code by surviving consumers

Delete `CodeGenerationConfig`, mappings, V1 result routing, attribute-filter types, legacy generator overloads, and metadata methods with no surviving consumers. Simplify the config-resolution return type when the V1/V2 union no longer serves a purpose.

Trace legacy template models, constants, embedded resources, fixtures, and package references before deletion. Preserve shared BPF/form metadata handling, localization, request discovery, and rendering code when the retained pipeline still uses them; Full-only output removal does not imply all BPF-related metadata is obsolete. A dependency is removable only when no surviving usage requires it.

Alternative rejected: deleting entire helpers or packages based only on legacy-looking names.

### 3. Keep format V2 unless a concrete contract cleanup justifies V3

The normative specs currently retain V2. Require explicit integer version and type; reject missing/V1 versions with a guide reference before authenticating. Reject the removed mode selector and attribute-filter properties even on an otherwise valid typed config, accounting for the loader's existing case-insensitive property handling. Continue accepting `$schema`.

The TypeScript `namespace` property is accepted but currently unused; retain it in V2 because that removal alone does not justify a format-version bump. If a later set of justified breaking config changes leads to format V3, remove this field as part of that versioned contract review. Keep the actively used .NET namespace. Preserve other useful output controls and do not reserve config changes for a hypothetical future resource-oriented design. Existing parser allowances outside the required validation changes remain unchanged.

If the review identifies a necessary removal, rename, or semantic change to an accepted V2 setting, record its exact old/new behavior and obtain agreement on that cleanup before implementation. Update this change's config delta, design, migration tasks, and schema paths to format V3 first. Do not silently redefine V2, keep a V2 adapter, or support both formats merely for compatibility. The dgtp major version alone never determines the config format number.

Alternative rejected: introducing format V3 now without an identified contract change.

#### Use native JSON polymorphism for target selection

Remove `CodeGenerationConfigFactory` and its manual target switch. Annotate `CodeGenerationConfigBase` with `JsonPolymorphic(TypeDiscriminatorPropertyName = "type")` and `JsonDerivedType` registrations for `DotNetCodeGenerationConfig` (`dotnet`) and `TypeScriptCodeGenerationConfig` (`typescript`). Deserialize the base type using standard `System.Text.Json` polymorphism, as already used by the connection models.

Enable `AllowOutOfOrderMetadataProperties` so `$schema`, `version`, and ordinary settings may precede `type`. The project targets .NET 10, which supports this option; no config reordering is required. Set `AllowDuplicateProperties` to false and retain case-insensitive ordinary-property binding so conflicting assignments, including `version`/`VERSION`, fail instead of validating one value and using another.

Use the standard case-sensitive discriminator name and values: lowercase `type`, with `dotnet` or `typescript`. Do not normalize `Type` or `TYPE` or introduce a custom converter. This drops the incidental discriminator-name casing allowance added by the branch's replacement factory; the pre-removal factory and published V2 schemas already require lowercase `type`, so this does not require format V3.

Retain only a thin loading/validation boundary for explicit version validation, missing/V1 migration guidance, and case-insensitive rejection of retired settings before authentication or output mutation. Native polymorphism owns concrete-type selection and missing/invalid target rejection. Preserve `$schema` acceptance and unrelated parser behavior; do not globally reject unknown properties as a shortcut for retired-setting validation.

Alternative rejected: retaining manual target dispatch, adding discriminator normalization, or introducing a custom polymorphic converter around functionality provided by the serializer.

### 4. Preserve outputs and migrate coverage, not obsolete executions

Capture representative typed-config output before consolidation. Migrate test helpers and still-relevant V1 fixtures to typed configs without regenerating expectations indiscriminately. Delete tests only when their behavior is intentionally removed; adapt tests for shared functionality rather than dropping them with the old entry point.

Cover Modern and Framework .NET output, TS declarations/forms, helpers, options, requests, localization, rendering diagnostics, determinism, and the existing TypeScript compile gates. Add negative config tests and a command-level check that invalid configs neither acquire a connection nor modify output.

For native config deserialization, cover both target types with `type` first, after `$schema`/`version`, and after ordinary output settings; reject missing, unsupported, non-string, and incorrectly cased discriminators. Cover duplicate discriminator and version assignments, including case variants for ordinary properties, while retaining the existing version and retired-setting diagnostics.

### 5. Document the final release contract once

Replace the README's lengthy code-generation reference with supported outputs, prerequisites, one minimal example per target, invocations, and links to schemas and migration guidance. Remove the V1 tutorial and Full/Light choice; make no general runtime-footprint claim.

Extend `docs/migrations/2.x-to-3.x.md` with field mappings, inversion of relevant suppression flags, changed defaults, separate invocations, Full output consumer changes, and filter removal without replacement. Adding `version` and `type` alone is not a valid V1 migration. Include any removed public module APIs when they affect consumers.

The surviving TypeScript `PrepareDirectory` already clears its generated TypeScript subtree. Preserve that behavior, document its boundary, and advise users to back up custom edits and remove obsolete generated files outside that subtree if applicable. Do not introduce broader deletion behavior.

Write the guide for direct 2.x-to-final-3.x migration. If a later resource-oriented change also lands before release, it must reconcile this section rather than add an intermediate migration hop.

## Risks / Trade-offs

- [Shared behavior is accidentally deleted with legacy helpers] -> Trace surviving call sites and migrate relevant tests before removing helpers.
- [V1-based tests conceal a change in typed output] -> Establish typed-config baselines before consolidation and inspect fixture diffs.
- [Legacy settings silently appear effective] -> Explicit rejection tests for removed controls, including casing variants and `$schema` acceptance.
- [Config cleanup expands into redesign] -> Preserve V2 by default and gate any incompatible cleanup on an explicit contract decision.
- [Full consumers need source changes] -> Document lost runtime classes/wrappers and supported outputs; do not promise a lossless config conversion.

## Migration Plan

Land cleanup before the separate resource-oriented redesign. Update schemas and directly affected fixtures with the implementation, then update the README and existing major-version guide. Sync the completed delta specs into main specs, validate all specs, and archive only after implementation is complete.

Users retain their original configs and generated code until migration is verified. Rollback means restoring those files and using their previous dgtp 2.x installation; no hidden legacy mode remains in dgtp 3.
