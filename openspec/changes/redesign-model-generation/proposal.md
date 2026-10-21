# Proposal

## Why

The current `codegeneration` command exposes a generation mechanism rather than a resource-oriented workflow. Redesigning it around `model` provides an opportunity to reconsider the CLI, configuration contract, and module API together instead of carrying existing boundaries into a new command name.

## What Changes

- Introduce one `model generate` command for both .NET and TypeScript, selected through the validated config rather than target subcommands.
- Implement it in a new `dgt.power.modelgeneration` project with matching tests. Retain existing `codegeneration` / `cg` behavior during transition and add a deprecation hint through the existing interceptor mechanism.
- Make the config file the shared, authoritative contract for what is generated and how. One config describes one target (.NET or TypeScript), and the two target shapes evolve independently. No config-free generation or generation-setting CLI overrides are planned; individual settings may still move to CLI parameters.
- Require explicit config and output paths. The supplied output directory is the owned generation root, without automatic `Model`/`DotNet`/`TypeScript` wrappers or a separate `--folder` option.
- Reject additional config properties in schemas and at runtime. Validate and resolve references before clearing output, then render/write directly; incomplete output after subsequent failure is an accepted risk, with no staging or rollback.
- Select artifacts explicitly and independently through opt-in config sections.
- Replace additive entity selection with include/exclude selectors, an optional shared default scope, and validation of all references before any output.
- Select forms by table or solution membership, generating all supported forms in that scope instead of maintaining individual filename-derived filters. Generated naming is a separate outstanding decision.
- **BREAKING for migrating consumers**: Generate TypeScript exported modules for bundler-based consumers instead of ambient generated namespaces. Names and module organization remain open.
- Generate TypeScript Custom API parameter interfaces, request classes implementing `getMetadata()`, and response-body interfaces for use with standard Xrm typings and `Xrm.WebApi.online.execute`, without a generated execution layer.
- **BREAKING for migrating consumers**: The new generator does not append a default .NET SDK message list. A single explicit selection generates custom-operation contracts and names, or platform-message names only; SDK platform wrappers are not regenerated. Track the existing default-message discussion in [#181](https://github.com/DIGITALLNature/DigitallPower/issues/181).
- Include choices required by selected artifacts automatically, with independent selection of additional global choices.
- **BREAKING for migrating consumers**: Drop classic Action request/response and name generation in the new implementation. Explicit Action names fail before output changes; solution-based Custom API discovery excludes them. Do not parse workflow XAML.
- Require a logical .NET consumer profile for plugins (Framework/C# 7.3) or external clients (.NET 10/C# 14), controlling syntax/API compatibility only. Profile names remain open.
- Require the .NET root namespace. Derive entity/property names from schema names using deterministic best-effort PascalCase, retaining publisher prefixes and existing internal capitalization.
- Keep .NET entities thin and SDK-compatible: metadata-aligned setters, non-virtual properties, useful identifiers included, and typed standard-SDK navigation between selected tables without custom collections or implicit loading. Remove forced-setter and virtual-property switches.
- **BREAKING for migrating consumers**: Do not generate service contexts or numeric entity type-code constants in the new implementation.
- Retain metadata XML snapshots as an independent opt-in artifact for consuming test libraries, with the same selection semantics and no re-import functionality.
- Favor .NET enum definitions, while leaving entity choice-property types open pending compatibility evaluation.
- Default label generation to organization base language, allow an override, silently fall back to base labels, and fail if neither label exists. Resolve colliding choice member names using numeric values.
- Design the new module independently of legacy contracts. Reuse logic/classes only where it genuinely helps without compromising the new module. Transition duration and removal criteria remain undecided.
- Document user-affecting breaking changes and migration steps.

This is a planning checkpoint, not an implementation-ready design. Remaining generated APIs/naming, configuration shapes, transition criteria, and internal architecture must be resolved before completing acceptance scenarios and implementation tasks. Differences listed above apply to the new generator; retaining legacy commands gives consumers a migration path.

## Capabilities

### New Capabilities

None identified at this stage. Reuse the existing code-generation capability boundaries unless further discovery establishes a distinct behavior.

### Modified Capabilities

- `cli-command-surface`: Introduce `model generate` and deprecate retained `codegeneration` / `cg`, with matching help, completion, and parsing coverage.
- `codegeneration-config`: Define the new independent recipe contracts, explicit paths, required .NET profile/namespace, strict properties, and selection semantics while preserving legacy configuration behavior.
- `codegeneration-output`: Describe the new generator's output alongside retained legacy behavior: exported TypeScript modules/requests, revised forms/messages/choice dependencies, thin .NET entities, and independently selected metadata snapshots.

Existing main specs remain the current behavior until implementation and spec synchronization. Final delta specs must distinguish new and legacy generator behavior rather than overwrite legacy requirements as though its commands had been removed.

## Impact

- CLI registration in `src/dgt.power/CommandTree.cs` and structural/settings-parsing tests in `tests/dgt.power.cli.tests`.
- New generation implementation in `src/modules/dgt.power.modelgeneration`, with tests in `tests/dgt.power.modelgeneration.tests` and host/solution project wiring.
- Legacy `src/modules/dgt.power.codegeneration` settings annotated for the existing `DeprecationInterceptor`; existing generation behavior retained.
- Matching configuration schemas if their contracts change.
- Generated-code consumer migration, TypeScript templates/support files, and compilation/runtime coverage against standard Xrm typings.
- README usage examples, shell-completion examples, and `docs/migrations/2.x-to-3.x.md` for applicable user-facing breaking changes.
- No dependency additions or legacy project renames are decided. No linguistic naming dependency is needed for the agreed schema-name conversion.

## References

- [#181 - Hardcoded default SDK message list](https://github.com/DIGITALLNature/DigitallPower/issues/181): generation defaults and compatibility implications, including the separately shipped public constants in `dgt.power.common`. This redesign does not implicitly remove those library constants or resolve the issue's full scope.
