# Implementation: `dgtp plugin step config set`

## Status

**Implemented** - Command is fully implemented and tested with unit tests.

## Context

`Configuration` was removed from `PluginRegistrationAttribute` (see
`.memory/decision-remove-plugin-configuration.md` if present, otherwise PR #7 /
branch `feat/remove-plugin-configuration`) because a compile-time attribute value
is not real configuration — it can't be changed without a rebuild/redeploy, and
complex values (e.g. JSON) are impractical to express as attribute arguments.

This command is the runtime replacement: it lets you set a plugin step's
unsecure/secure configuration directly against Dataverse, independent of the
plugin assembly's source code.

## Command shape

```
dgtp plugin step config set
```

Resource path: `plugin` → `step` → `config`, verb `set` last. Reuses the
existing `dgtp` environment/connection targeting mechanism (no new flags needed
for that).

### Spectre.Cli registration

```csharp
config.AddBranch("plugin", plugin =>
{
    plugin.AddCommand<PluginPushCommand>("push");

    plugin.AddBranch("step", step =>
    {
        step.AddBranch("config", cfg =>
        {
            cfg.AddCommand<PluginStepConfigSetCommand>("set");
            // future: cfg.AddCommand<PluginStepConfigGetCommand>("get");
        });
    });
});
```

## Options

### Step resolution (mutually exclusive groups)

**A. Direct — authoritative**

| Option | Type | Description |
|---|---|---|
| `--step-id` | `Guid` | `SdkMessageProcessingStep` id. If provided, the composite-key options below are ignored. |

**B. Composite key — convenience resolver**

| Option | Type | Required | Description |
|---|---|---|---|
| `--plugin-type` | `string` | ✅ | Fully qualified type name (namespace + class) of the plugin. |
| `--message` | `string` | ✅ | SDK message name (e.g. `Update`). |
| `--stage` | `PluginStepStage` enum | ✅ | Execution stage by name or numeric value: PreValidation/10, PreOperation/20, MainOperation/30, PostOperation/40. Other numeric values are rejected. |
| `--entity` | `string` | ✅ | Primary entity logical name. |
| `--secondary-entity` | `string` | optional | Secondary entity logical name (disambiguator, e.g. for `Associate`). |
| `--execution-order` | `int` | optional | Execution order / rank (disambiguator). |

Resolution is a dumb lookup — no per-message validation of which entity fields
are required; wrong/irrelevant values simply yield zero matches. Any existing
step is a valid target, regardless of whether it was registered via a
`dgtp plugin push` from this codebase.

### Configuration values

| Option | Type | Description |
|---|---|---|
| `--unsecure` | `string` | Inline unsecure configuration value. |
| `--unsecure-file` | `path` | Path to a UTF-8 file containing the unsecure configuration value. Mutually exclusive with `--unsecure`. |
| `--secure` | `string` | Inline secure configuration value. |
| `--secure-file` | `path` | Path to a UTF-8 file containing the secure configuration value. Mutually exclusive with `--secure`. |

Both unsecure and secure can be set in the **same** invocation:

```
dgtp plugin step config set --step-id <guid> --unsecure "<value>" --secure "<value>"
```

## Implementation Notes

### Semantics

- **Omitted flag** (`--unsecure`/`--secure` not passed at all) → existing value on the step is left untouched.
  - *Implementation:* Validation requires at least one of unsecure/secure config options, so both cannot be omitted. If only one is provided, only that value is updated.
- **Explicit empty string** (`--unsecure ""`) → explicitly clears the config. Implementation checks `is not null`, not `string.IsNullOrEmpty`, before deciding whether to send the field.
- **File reads** — UTF-8 standard (`File.ReadAllText(path)` default overload), no configurable encoding.
- **No dry-run** — the operation is a single deterministic field update on one resolved step; not needed.
- **Success messages** include checkmark emoji (✓) for visual confirmation.

### Resolution failure handling

When resolving via the composite key (path B):

| Case | Behavior |
|---|---|
| Exactly one match | Proceed with the update. |
| Zero matches | Exit non-zero. Message: `No step found matching {criteria}. Verify the step exists, or use --step-id.` |
| Multiple matches | Exit non-zero. Print the candidate list with StepId, PluginType, Message, Stage, Entity, ExecutionOrder. |

*Note:* The implementation does not currently show config presence (unsecure: set/empty, secure: set/empty) in the candidate list as originally designed. This could be added in a future iteration.

Progressive narrowing: if the user supplied `--secondary-entity` and/or
`--execution-order`, use them to narrow candidates before deciding the result is
ambiguous.

## Validation rules

- `--step-id` is mutually exclusive with the composite-key options (A vs. B).
- `--unsecure` and `--unsecure-file` are mutually exclusive (same for `--secure`/`--secure-file`).
- At least one of `--unsecure`/`--unsecure-file`/`--secure`/`--secure-file` must be provided.
- File existence is validated in `Validate()` (pre-execution).
- Non-existent step-id will fail at execution time with a Dataverse error.

## Underlying operation

Update the `sdkmessageprocessingstep` record's `configuration` (unsecure) field
and the related `sdkmessageprocessingstepsecureconfig` record's `secureconfig`
field via `IOrganizationService`/Dataverse Web API — mirrors what the classic
Plugin Registration Tool does; there is no `pac` CLI command for this.

### Repository methods

- `SdkMessageProcessingStepRepository.UpdateConfigurationAsync(stepId, configuration)` - Updates unsecure config
- `SdkMessageProcessingStepSecureConfigRepository.UpsertAsync(stepId, secureConfig)` - Creates/updates secure config

## Files

- Command: `src/modules/dgt.power.plugin/Commands/PluginStepConfigSetCommand.cs`
- Settings: `src/modules/dgt.power.plugin/Commands/PluginStepConfigSetSettings.cs`
- Tests: `tests/dgt.power.plugin.tests/Commands/PluginStepConfigSetCommandTests.cs` (17 tests)
- Documentation: README.md (plugin section)

## Out of scope for this iteration

- `dgtp plugin step config get` / `unset` — future commands, same `config` branch.
- Dry-run mode.
- JSON/structured output mode.
- Built-in validation of which entity fields a given SDK message requires.

## Differences from original design

1. **Config presence in multiple matches output:** The implementation prints basic step info (StepId, PluginTypeName, Message, Stage, Entity, ExecutionOrder) but does not include configuration presence indicators as originally specified in the design.

2. **Stage option type:** `--stage` uses the `PluginStepStage` enum so Spectre.Console.Cli parses names and numeric enum values; validation rejects undefined values and the enum's `None` sentinel.

   Stages 80 and 90 are internal-only and are excluded. `MainOperation` remains supported for custom APIs and virtual table data providers, as documented in the [Dataverse event framework](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/event-framework#event-execution-pipeline). The [step table reference](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/reference/entities/sdkmessageprocessingstep#stage) includes internal stage values; their presence in metadata does not make them supported extension points.

3. **Validation enforces at least one config:** The design suggested that omitting both unsecure and secure flags would leave values untouched, but the implementation enforces that at least one must be provided (which aligns with the practical use case).
