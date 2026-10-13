# Implementation: Plugin Step Enable/Disable and State Set Commands

## Overview

The `dgtp plugin step` subcommands provide comprehensive management of SDK message processing steps:
- `enable` / `disable`: Enable or disable individual plugin steps
- `state set`: Bulk enable/disable plugin steps from a JSON configuration file
- `config set`: Set unsecure/secure configuration (pre-existing)

## Design Decisions

### Three Mutually Exclusive Identification Modes

Both `enable` and `disable` commands support three identification modes (enforced via `Validate()`):

1. **Direct ID**: `--step-id <guid>` - Identifies a step by its SDK message processing step ID
2. **Composite Key**: `--plugin-type <type> --message <msg> --stage <stage> --entity <entity>` (+ optional `--secondary-entity`, `--execution-order`) - Identifies a step by its metadata
3. **Solution-scoped**: `--solution <name>` - Applies the operation to ALL steps in the specified solution

Rationale: Provides flexibility for different use cases:
- Single step: Use step ID or composite key
- All steps in a solution: Use `--solution` (replaces `maintenance ensuresdksteps`)

### Auto-Persist Reason

The `--reason` parameter, when provided, replaces the step's description in Dataverse (no `--persist-reason` flag). It is written only when the state actually changes.

### Idempotency

Steps already in the desired state are skipped (no write, reason not stored) and reported as unchanged (`○ Already enabled|disabled`). This avoids audit noise and keeps dry-run and summaries meaningful. This intentionally diverges from the removed `ensuresdksteps`, which always wrote.

### Dry-Run (no confirm)

`--dry-run` is available on `enable`, `disable` and `state set` (yellow `○ Would enable|disable`, DRY RUN banner). A `--confirm` prompt was rejected; dry-run gives the safety without breaking non-interactive use.

### Shared Handler and Failure Boundaries

`PluginStepStateHandler` holds resolve/apply/summary logic for all three commands; `PluginStepEnableDisableCommandBase` shares the enable/disable command. Per-step (and per-entry in `state set`) failures are reported and counted without aborting the rest; cancellation and fatal exceptions are never swallowed (exception filter). All user-controlled values are escaped via `MarkupLineInterpolated`. `PluginStepStateConfig.Validate()` requires `enabled` and exactly one identification mode. Solution lookup is exact-match via `SolutionComponentRepository` (unknown solution fails). Config schema: `schemas/plugin/state/schema.json`.

### Known Gap: Paging

`ListComponentIdsAsync` and `ListStatesAsync` do not page; tracked in `todo.md` to adopt the shared paging helper.

### Consistent Output Format

Per-step output follows the pattern: `<emoji> <Verb> <step name> - <reason>`

- Enabled steps: `[green]●[/] Enabled {stepName} - {reason}`
- Disabled steps: `[red]●[/] Disabled {stepName} - {reason}`

Bulk operations (`state set`) use the same per-step format, followed by a summary with checkmarks:
- `[green]✓[/] {count} steps enabled`
- `[green]✓[/] {count} steps disabled`
- `[red]✗[/] {count} steps failed`

Rationale: Consistent with existing commands (plugin push, webresource push).

### Solution-Scoped Operations

The `--solution` parameter queries all SDK steps in the specified solution and applies the enable/disable operation to each. This provides a direct replacement for the removed `maintenance ensuresdksteps` command.

**Migration**:
```bash
# Before
dgtp maintenance ensuresdksteps -s mysolution

# After  
dgtp plugin step enable --solution mysolution
dgtp plugin step disable --solution mysolution
```

### Config File Format for State Set

```json
{
  "steps": [
    {
      "stepId": "00000000-0000-0000-0000-000000000000",
      "enabled": false,
      "reason": "Temporarily disabled for deployment"
    },
    {
      "pluginType": "MyNamespace.MyPlugin",
      "message": "Create",
      "stage": 20,
      "entity": "account",
      "enabled": true,
      "reason": "Production ready"
    }
  ]
}
```

Each step in the config must have either `stepId` or a complete composite key (`pluginType`, `message`, `stage`, `entity`).

## Breaking Changes

### Removed: ensuresdksteps Command

The `maintenance ensuresdksteps` command was removed as it was superseded by `plugin step enable/disable --solution`.

**Migration Path**: Use `plugin step enable --solution <name>` or `plugin step disable --solution <name>`.

## Files

- `src/modules/dgt.power.plugin/Commands/PluginStepEnableDisableSettings.cs` - Settings with three identification modes
- `src/modules/dgt.power.plugin/Commands/PluginStepEnableDisableCommand.cs` - Command logic including solution-scoped handling
- `src/modules/dgt.power.plugin/Commands/PluginStepStateSetSettings.cs` - Config file-based settings
- `src/modules/dgt.power.plugin/Commands/PluginStepStateSetCommand.cs` - Bulk operations from config
- `src/modules/dgt.power.plugin/Repositories/SdkMessageProcessingStepRepository.cs` - Repository for step operations

## Related

- `implementation-plugin-step-config-set.md` - Configuration management for steps
- `decision-resource-oriented-cli-redesign.md` - Overall CLI design philosophy
