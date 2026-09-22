# Implementation Plan: Webresource Module

## Current starting point

The legacy `dgt.power.push` module autodetects a file versus directory target. Its directory path
is handled by `WebresourcesProcessor`, which currently combines configuration loading, solution
lookup, recursive file discovery, webresource type inference, Dataverse state lookup, create/update
operations, solution membership, publishing, obsolete-resource deletion, and console reporting.
The relevant models are `Webresources`, `WebresourceConfig`, `WebresourcesPattern`,
`SolutionWebresourceInfo`, and `WebresourceState`.

The target architecture is the one implemented by `feat/plugin-push-v2`: a resource-specific
command module with Local, Planning, Repositories, and Execution layers. The webresource module
should be created independently; the legacy module remains the compatibility path until both
plugin and webresource commands are available.

## Planned structure

```text
src/modules/dgt.power.webresource/
  Base/WebresourceSettings.cs
  Commands/WebresourcePushCommand.cs
  Commands/WebresourcePushSettings.cs
  Local/
    LocalWebresource.cs
    WebresourceConfigReader.cs
    WebresourceDiscovery.cs
    WebresourceTypeResolver.cs
  Planning/
    WebresourceAction.cs
    WebresourcePlan.cs
    WebresourcePushPlanner.cs
  Remote/
    RemoteWebresource.cs
    RemoteSolutionWebresource.cs
  Repositories/
    IWebresourceRepository.cs
    ISolutionRepository.cs
    WebresourceRepository.cs
    SolutionRepository.cs
  Execution/
    WebresourcePushExecutor.cs
    WebresourcePushOptions.cs
```

Names may be adjusted to match the final plugin branch conventions, but the layer boundaries
should remain explicit.

All C# type names use `WebResource` casing (`WebResourcePushCommand`,
`LocalWebResource`, `IWebResourceRepository`, etc.). The CLI command and namespace remain
lowercase `webresource`, matching command conventions and the module namespace.

## Delivery phases

1. **Scaffold and CLI contract**
   - Add the source and test projects, solution entries, host project reference, and DI
     registrations.
   - Register `webresource` → `push` in `CommandTree`; use singular resource naming.
   - Define a file-or-directory target and resource-only options: `--solution`, `--mapping-file`,
     `--publisher-prefix`, `--name`, `--publish`, `--delete-obsolete`, and `--dry-run`.
   - For a directory, resolve names relative to the directory target and apply config mappings.
     Require an explicit `--publisher-prefix`; do not derive naming from the selected solution.
     For a single file, require an explicit `--name`; do not guess a project root from the file
     system. Reject `--delete-obsolete` for a single-file target because the desired resource set
     is incomplete.
   - Add command-tree and settings-parsing tests before implementation.

2. **Port and harden the Local layer**
   - Read the existing mapping configuration without coupling the reader to Dataverse or
     Spectre output.
   - Recursively discover supported extensions and resolve Dataverse webresource types.
   - Normalize relative paths to `/`, apply explicit mappings first, and otherwise apply the
     solution publisher prefix.
   - Validate missing directories, unsupported/ambiguous mappings, duplicate logical names, and
     invalid resource names explicitly.
   - Represent content and its hash in an immutable local model. Keep file I/O and console
     formatting at the command boundary where possible.

3. **Introduce pure planning**
   - Match local resources by logical name and type against remote snapshots.
   - Produce `Create`, `Update`, and `Keep` actions based on content equality.
   - Separately plan solution membership additions and obsolete unmanaged-resource deletions.
   - Make the solution-prefix and `--delete-obsolete` preconditions explicit in settings validation.
   - Unit-test mapping, type inference, path/prefix behavior, duplicate handling, state transitions,
     membership guards, and obsolete detection without a Dataverse connection.

4. **Build async repositories**
   - Use `IOrganizationServiceAsync2` and repository interfaces; do not carry `DataContext` into
     the command/executor.
   - Batch or otherwise minimize remote lookups instead of the legacy one-query-per-file
     `EnrichState` pattern.
   - Fetch the target solution, publisher prefix, unmanaged webresources, and solution membership
     in a form reusable by both reconciliation and obsolete detection.
   - Keep repository DTOs separate from local files and plans.
   - Surface Dataverse failures; do not swallow `AddSolutionComponent` errors as successful pushes.

5. **Implement execution and dry-run**
   - Apply the plan in a deterministic order: create/update, solution membership, publish changed
     resources, then delete obsolete resources.
   - Add a resource to the solution only when it is not already a member, preserving the existing
     lazy-add behavior documented in `guide-webresource-solution-lazy-add.md`.
   - Make `--publish` apply to both created and updated resources. This intentionally improves on
     the legacy behavior, which publishes updates only.
   - Ensure dry-run performs discovery, remote reads, planning, and complete reporting but no
     create/update/delete/execute calls.
   - Return a failure result when an operation fails and include actionable resource context.

6. **Wire, document, and migrate**
   - Add the new project to `DigitallPower.slnx`, `dgt.power.csproj`, DI, and command-tree tests.
   - Add webresource command help/examples and update README command reference, completion
     examples, repository layout, and migration guidance.
   - Document the JSON mapping-file format with a complete example, explain directory-relative
     mapping keys and Dataverse logical-name values, and link the `schemas/webresource/schema.json`
     file. Do not add new-module usage documentation until the command is wired.
   - Add focused module tests plus command smoke tests; run the webresource, CLI, and solution
     builds.
   - Only after parity is verified, add a proper deprecation notice/hint to legacy `dgtp push`.
     Keep the legacy command available as a compatibility adapter during the transition, then
     remove the duplicated webresource implementation in a separate cleanup change.

## Settled behavior decisions

- `--publisher-prefix` is explicit and required for directory targets. It is the authoritative
  naming prefix and is independent of `--solution`; the solution option controls membership and
  obsolete-resource scope, not resource naming.
- The new command calls the mapping input `--mapping-file`; the legacy `--config` name remains
  confined to the compatibility command.
- JSON remains the mapping-file format. JSON Schema support is standardized and broadly available
  across editors, while YAML schema/intellisense support depends more heavily on the editor and
  language service. The mapping file is small enough that YAML's readability advantage does not
  justify another parser and less uniform tooling.
- Dataverse repository queries use generated early-bound logical-name constants and materialize
  results as generated `WebResource`/`Solution` classes; generic `Entity.GetAttributeValue` access
  is not used in the new module.
- Local, synchronous option relationships are validated in
  `WebResourcePushSettings.Validate()` through Spectre `ValidationResult`; command-level
  validation remains reserved for checks that require command/connection context.
- Dry-run shows its banner before local discovery, performs reads and planning, performs no
  Dataverse writes, and reports every planned action using the same labels as a real run
  (`Create`, `Update`, `Add`, `Publish`, `Delete`). The banner is the mode indicator; action
  messages do not duplicate it with `Would ...` prefixes.
- Expected webresource domain failures use focused `AbstractPowerException` types
  (`ManagedWebResourceException`, `WebResourceSolutionNotFoundException`, and
  `WebResourceMappingException`); CLI option validation remains `ValidationResult`.
- `--publish` publishes both created and updated resources.
- The target accepts either a single file or a directory. Directory scanning is recursive.
- Directory-relative paths and mapping-file keys are used only when the target is a directory. A
  single-file target must provide `--name`; its parent directory is not treated as an implicit
  project root.
- `--delete-obsolete` is available only for directory targets.

## Remaining behavior decisions

- Whether an unknown extension is ignored with a warning or treated as an error.
- Whether explicit mappings may target arbitrary Dataverse names or must satisfy the publisher
  prefix convention.
- Whether duplicate logical names are always fatal.
- Whether obsolete deletion is limited to unmanaged webresources already in the selected solution
  (the current behavior and safest default).
