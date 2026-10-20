# Proposal

## Why

The current `codegeneration` command exposes a generation mechanism rather than a resource-oriented workflow. Redesigning it around `model` provides an opportunity to reconsider the CLI, configuration contract, and module API together instead of carrying existing boundaries into a new command name.

## What Changes

- **BREAKING**: Replace the `codegeneration` / `cg` entry point with the confirmed singular resource root `model`.
- Design generation for both .NET and TypeScript under that resource. Whether targets need separate subcommands remains open until their CLI inputs and workflows are defined.
- Make the config file the shared, authoritative contract for what is generated and how. One config describes one target (.NET or TypeScript), and the two target shapes evolve independently. No config-free generation or generation-setting CLI overrides are planned; individual settings may still move to CLI parameters.
- The CLI owns where output is written. The output directory is owned by the tool and cleared on every run.
- Select artifacts explicitly and independently through opt-in config sections.
- Replace additive entity selection with include/exclude selectors, an optional shared default scope, and validation of all references before any output.
- Select forms by table or solution membership, generating all supported forms in that scope instead of maintaining individual filename-derived filters. Generated naming is a separate outstanding decision.
- **BREAKING**: Generate TypeScript exported modules for bundler-based consumers instead of ambient generated namespaces. Names and module organization remain open.
- Generate TypeScript Custom API parameter interfaces, request classes implementing `getMetadata()`, and response-body interfaces for use with standard Xrm typings and `Xrm.WebApi.online.execute`, without a generated execution layer.
- **BREAKING**: Remove the automatically appended .NET SDK message list. A single explicit selection generates custom-operation contracts and names, or platform-message names only; SDK platform wrappers are not regenerated.
- Include choices required by selected artifacts automatically, with independent selection of additional global choices.
- Review the module's internal API and boundaries after agreeing on the user-facing workflow. The CLI resource name does not require a matching project or assembly name; module naming remains open.
- Use a clean replacement rather than requiring compatibility with the existing commands or V2 configuration contract. This permits deliberate breaking changes; it does not require changing every existing setting or generated artifact.
- Document user-affecting breaking changes and migration steps.

This is an initial planning proposal, not an implementation-ready design. Target command layout, configuration requirements, generated-output compatibility, module naming, and internal architecture must be resolved before completing acceptance scenarios and implementation tasks.

## Capabilities

### New Capabilities

None identified at this stage. Reuse the existing code-generation capability boundaries unless further discovery establishes a distinct behavior.

### Modified Capabilities

- `cli-command-surface`: Replace the generation entry point with `model` and define the agreed target command layout, with matching help, completion, and parsing coverage.
- `codegeneration-config`: Define the redesigned configuration and CLI-input contract, including target selection and any replacement of the existing V2 format.
- `codegeneration-output`: Replace ambient TypeScript output with exported modules and directly usable Custom API requests; revise form selection, .NET message generation, and choice dependency output.

Classic Action request/response generation is under review; its removal is not a confirmed part of this proposal. Existing main specs remain the current behavior until implementation and spec synchronization.

## Impact

- CLI registration in `src/dgt.power/CommandTree.cs` and structural/settings-parsing tests in `tests/dgt.power.cli.tests`.
- Generation commands, settings, configuration loading, and potentially generator/service interfaces in `src/modules/dgt.power.codegeneration`, with corresponding module tests.
- Matching configuration schemas if their contracts change.
- Generated-code consumer migration, TypeScript templates/support files, and compilation/runtime coverage against standard Xrm typings.
- README usage examples, shell-completion examples, and `docs/migrations/2.x-to-3.x.md` for applicable user-facing breaking changes.
- No dependency changes or project renames are decided.
