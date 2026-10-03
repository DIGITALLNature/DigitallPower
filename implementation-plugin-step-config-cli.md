# Design: `dgtp plugin step config set`

## Status

Design settled, ready for implementation. Not yet implemented.

## Context

`Configuration` was removed from `PluginRegistrationAttribute` (see
`.memory/decision-remove-plugin-configuration.md` if present, otherwise PR #7 /
branch `feat/remove-plugin-configuration`) because a compile-time attribute value
is not real configuration — it can't be changed without a rebuild/redeploy, and
complex values (e.g. JSON) are impractical to express as attribute arguments.

This command is the runtime replacement: it lets you set a plugin step's
unsecure/secure configuration directly against Dataverse, independent of the
plugin assembly's source code.

## Goal

Add a new command to the existing `dgtp` CLI (Spectre.Cli-based, resource-oriented,
alongside the existing `dgtp plugin push`) that sets the unsecure and/or secure
configuration on an `SdkMessageProcessingStep`.

## Command shape

```
dgtp plugin step config set
```

Resource path: `plugin` → `step` → `config`, verb `set` last. Reuses the
existing `dgtp` environment/connection targeting mechanism (no new flags needed
for that).

### Spectre.Cli registration sketch

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
| `--stage` | `string`/enum | ✅ | Execution stage (matches `PluginExecutionStage`). |
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

## Semantics

- **Omitted flag** (`--unsecure`/`--secure` not passed at all) → existing value on the step is left untouched.
- **Explicit empty string** (`--unsecure ""`) → explicitly clears the config. Implementation must check `is not null`, not `string.IsNullOrEmpty`, before deciding whether to send the field.
- **File reads** — UTF-8 standard (`File.ReadAllText(path)` default overload), no configurable encoding.
- **No dry-run** — the operation is a single deterministic field update on one resolved step; not needed.
- **No dedicated JSON output** — reuse whatever text/exit-code conventions the rest of `dgtp` already follows.

## Resolution failure handling

When resolving via the composite key (path B):

| Case | Behavior |
|---|---|
| Exactly one match | Proceed with the update. |
| Zero matches | Exit non-zero. Message: `No step found matching type=<X>, message=<Y>, stage=<Z>, entity=<W>. Verify the step exists, or use --step-id.` |
| Multiple matches | Exit non-zero. Print the candidate list (see below), with a hint to refine using `--execution-order`/`--secondary-entity` or to use `--step-id`. |

Candidate list entries should include: `StepId`, `PluginTypeName`, message, stage,
entity, `ExecutionOrder`, and current config presence (e.g. "unsecure: set/empty",
"secure: set/empty") — enough for the user to pick the right `--step-id` without
a follow-up query.

Progressive narrowing: if the user supplied `--secondary-entity` and/or
`--execution-order`, use them to narrow candidates before deciding the result is
ambiguous.

## Validation rules

- `--step-id` is mutually exclusive with the composite-key options (A vs. B).
- `--unsecure` and `--unsecure-file` are mutually exclusive (same for `--secure`/`--secure-file`).
- At least one of `--unsecure`/`--unsecure-file`/`--secure`/`--secure-file` must be provided.
- Validate in `Validate()` (pre-execution), not deep inside `ExecuteAsync`.

## Underlying operation

Update the `sdkmessageprocessingstep` record's `configuration` (unsecure) field
and the related `sdkmessageprocessingstepsecureconfig` record's `secureconfig`
field via `IOrganizationService`/Dataverse Web API — mirrors what the classic
Plugin Registration Tool does; there is no `pac` CLI command for this.

## Out of scope for this iteration

- `dgtp plugin step config get` / `unset` — future commands, same `config` branch.
- Dry-run mode.
- JSON/structured output mode.
- Built-in validation of which entity fields a given SDK message requires.

## Open items

None — see `.memory/summary.md` "Active tasks" for current status once
implementation starts.
