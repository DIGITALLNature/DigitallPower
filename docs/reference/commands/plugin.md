# `plugin` — Manage plugin assemblies/packages

Commands for deploying Dataverse plugin assemblies and packages.

## `push` — Deploy plugin assemblies/packages

Registers a single plugin assembly (`.dll`), a plugin package (`.nupkg`), every `.dll`/`.nupkg`
found directly in a directory, or every file matched by a glob pattern (for example
`bin/release/**/*.nupkg`, quote it to prevent shell expansion). DLLs without plugins, such as
dependencies, are ignored. Targets are processed one after another; a failing target does not stop
the others but fails the command.

The whole batch is validated before anything is written: unreadable artifacts, a missing
`--publisher-prefix` for packages, and two targets declaring the same assembly, package, data
provider or Custom API reject the batch. Narrow the target to resolve a conflict. A path or glob
that matches no files, or matched files that contain no plugins, fails the command.

### Usage

```bash
dgtp plugin push ./bin/Release/MyPlugin.dll --solution mysolution
dgtp plugin push ./bin/Release/MyPlugin.1.0.0.nupkg --publisher-prefix contoso --solution mysolution
dgtp plugin push ./bin/Release --publisher-prefix contoso --solution mysolution
dgtp plugin push "./src/**/bin/Release/*.nupkg" --publisher-prefix contoso
```

### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `--solution` | No | Ensures package, standalone assembly, and declared plugin step membership in the given solution |
| `--publisher-prefix` | For `.nupkg` targets | Publisher customization prefix for plugin packages |
| `--dry-run` | No | Previews the deployment and solution membership plan without writing to Dataverse |
| `--confirm` | No | Prompts before executing each rendered target plan; ignored by dry-run, non-interactive, and CI execution |

Existing plugin package records must have a version in Dataverse; deployment fails explicitly
if the version is missing. A package without an attached file is treated as requiring a content upload.

### Supported registration attributes

`plugin push` requires registration attributes from `Digitall.Plugins.Registration` 3.0.0 or
later. Other registration namespaces are not recognized.

| Attribute | Behavior |
|-----------|----------|
| `PluginRegistrationAttribute` | Registers plugin steps, including message, stage, mode, entity filters, and images |
| `CustomApiRegistrationAttribute` | Links a plugin type to the declared Custom API |
| `CustomDataProviderRegistrationAttribute` | Creates/updates a custom data provider record and links its operation handlers (experimental support); the configuration table must already exist |

Workflow activity registration (`WorkflowRegistrationAttribute`) is not supported by `plugin push`.
Every concrete `IPlugin` type must carry one of the supported registration attributes; assemblies
with manually maintained plugin registrations are not supported.

### Custom data providers

**Experimental support:** Custom data-provider deployment is offered as experimental support.
Validate it in a non-production environment before production rollout.

Requires `Digitall.Plugins.Registration` 3.0.0 or later and its three-argument constructor:
data-source configuration-table schema name, operation, and provider name on every declaration.
Rebuild assemblies using the old two-argument constructor before deployment. See the
[registration library](https://github.com/DIGITALLNature/DigitallRegistrationPower) for attribute
usage and examples.

The data-source configuration table is a prerequisite: `plugin push` does not create, update, or
delete it. Provision the table in each environment first. A missing table, or one that is not an
organization-owned table backed by the `JsonConverter` data provider, is rejected before any
deployment writes. The target environment must contain exactly one data provider named
`JsonConverter`; its ID is resolved automatically.

`plugin push` creates or updates the provider record and assigns the Retrieve, RetrieveMultiple,
Create, Update, and Delete handlers. A declaration owns all handler slots of its provider:
undeclared handlers are cleared, even when they reference types from other assemblies. Omitted
optional metadata such as the description is preserved. If a provider is no longer declared, only
handlers referring to plugin types owned by the target are cleared; the provider record and its
table are never deleted. `--dry-run` previews the deployment.

With `--solution`, only missing provider records are added to the solution. The configuration
table, its schema, and its data are not managed by `plugin push` and are not added; transport the
table to downstream environments through your own solution, as with Custom API records.

Data-source records, custom configuration columns, business virtual tables, mappings, and
bulk-operation handlers are outside scope. Existing ordinary tables are not converted into
data-source tables.

### Step configuration

`plugin push` does not read, write, or reconcile unsecure or secure plugin-step configuration.
Existing configuration is preserved when a matching step is updated. New steps have no
configuration until it is provisioned by the target environment's deployment pipeline. Keep
configuration values in CI/CD secret or environment-variable providers, not registration
attributes or source-controlled configuration files.

### Managed identity

Managed identity provisioning and binding are outside `plugin push`. Configure the Dataverse
managed identity record and assembly/package binding separately in each environment, together
with that environment's Entra app registration or user-assigned managed identity, federated
credential, and resource permissions.

The same signed artifact can be promoted across stages; the federated credential's subject
includes the Dataverse environment ID. Identity client/tenant IDs do not belong in registration
attributes. See [Microsoft's managed identity setup guide](https://learn.microsoft.com/en-us/power-platform/admin/set-up-managed-identity).

Push leaves identity bindings on existing assembly/package records untouched. A newly created
replacement assembly does not inherit the superseded assembly's binding; provision its identity
binding separately. Push never creates or deletes shared managed identity records.

### Planning and execution

Before any writes, the command renders a deployment tree for plugin types, steps, images, Custom
API links, and assembly upgrades. It then shows completed operations, or reports that no changes
are required.
Data providers appear in a separate tree after the package or assembly tree; handler entries
reference plugin types rather than nesting the provider beneath them.
`--dry-run` stops after rendering the plan. Missing declared Custom APIs and unresolved step
messages fail before Dataverse changes occur.
Custom API unique names must resolve to exactly one record; ambiguous matches are rejected.
`--confirm` prompts after each target plan in interactive sessions; declining leaves that target
unchanged. Non-interactive and CI execution suppress the prompt.
For assembly upgrades, the tree represents the effective replacement-assembly state, including
steps and images migrated from the superseded assembly. A separate message then states whether
the outdated assembly will be deleted.

CLI-host errors and per-target `plugin push` failures include contextual exception messages and,
for Dataverse service faults, hexadecimal error codes and inner-fault messages. This output is
available in release builds without a diagnostic flag. Stack traces, fault trace text, and arbitrary
fault-detail values are not printed. Service messages may still contain environment-specific
information; review output before sharing it.

### Solution membership

When `--solution` is set, missing package, standalone assembly, and declared step memberships are
listed below the deployment tree, including in dry-run output. Plugin types, images, Custom APIs,
and managed identities are not added implicitly. When every managed component is already present,
the same section confirms that no membership additions are needed. An unknown solution fails
planning with `MissingSolutionException` before any Dataverse writes occur.
Package membership requires an unambiguous solution component definition for `pluginpackage`
in the target environment; missing or duplicate definitions fail planning rather than skipping
the requested membership.

### Updates and upgrades

Plugin packages are named using the explicit `<publisher-prefix>_<package-name>` value; DLL-only targets do not
require `--publisher-prefix`. Existing packages and same-version standalone assemblies are updated
only when their content differs. Package version differences alone do not cause an update. A
standalone assembly is updated in place when its major/minor version matches the single existing
same-name standalone assembly; build/revision changes in either direction are in-place updates.

When a local assembly's major or minor version differs, the command creates a
replacement, migrates matching declared plugin steps to preserve their environment configuration,
then removes the superseded assembly and registrations. More than one same-name standalone
assembly is unsupported: `plugin push` fails before writing and requires manual cleanup. Use
`--dry-run` to preview the supported update or replacement outcome.

### Removals and solution import

`plugin push` only changes the environment it targets. A cleared Custom API handler link is not
propagated by managed solution import (a Dataverse platform limitation), and the linked plugin
type or assembly cannot be deleted while the link exists. Workaround: unlink the handler manually
in each downstream environment before the release that removes the type or assembly. This
creates no unmanaged layer, and the next import then removes the type or assembly.

For plugin packages the same applies to steps: package assemblies are updated in place, even
when the assembly version changes. A managed import that removes a plugin type fails with "Unable
to delete plugintype due to 1 step(s) registered on it" while the type still has a step
downstream, or while a Custom API is still linked to it (Dataverse reports both as a registered
step). Delete the step or unlink the handler manually in each downstream environment before
deploying the release that removes the type.

## `step config set` — Set plugin step configuration

Sets the unsecure and/or secure configuration for an existing plugin step. Steps can be identified
either by their SDK message processing step ID or by a composite key (plugin type, message, stage, entity).
Configuration values can be provided inline or read from UTF-8 encoded files.

### Usage

```bash
dgtp plugin step config set --step-id 00000000-0000-0000-0000-000000000000 --unsecure "{\"key\":\"value\"}"
dgtp plugin step config set --plugin-type MyNamespace.MyPlugin --message Create --stage PreOperation --entity account --unsecure "{\"key\":\"value\"}"
dgtp plugin step config set --step-id 00000000-0000-0000-0000-000000000000 --secure "secret-value" --unsecure "{\"key\":\"value\"}"
dgtp plugin step config set --step-id 00000000-0000-0000-0000-000000000000 --unsecure-file ./config.json --secure-file ./secrets.json
```

### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `--step-id` | No (mutually exclusive with composite key) | SDK message processing step ID |
| `--plugin-type` | No (part of composite key) | Fully qualified plugin type name |
| `--message` | No (part of composite key) | SDK message name (Create, Update, Delete, etc.) |
| `--stage` | No (part of composite key) | Stage name or numeric value: PreValidation/10, PreOperation/20, MainOperation/30, PostOperation/40. Other numeric values are rejected. |
| `--entity` | No (part of composite key) | Primary entity logical name |
| `--secondary-entity` | No (part of composite key) | Secondary entity logical name (for Associate, Disassociate, etc.) |
| `--execution-order` | No (part of composite key) | Rank/execution order of the step |
| `--unsecure` | No (at least one config required) | Inline unsecure configuration value. Use empty string to clear |
| `--unsecure-file` | No (mutually exclusive with --unsecure) | Path to UTF-8 file containing unsecure configuration |
| `--secure` | No (at least one config required) | Inline secure configuration value. Use empty string to clear |
| `--secure-file` | No (mutually exclusive with --secure) | Path to UTF-8 file containing secure configuration |

### Notes

- Either `--step-id` or all required composite key options (`--plugin-type`, `--message`, `--stage`, `--entity`) must be provided, but not both. `--secondary-entity` and `--execution-order` are optional composite-key filters and cannot be used with `--step-id`.
- `--stage` accepts the listed names (case-insensitive) or numeric Dataverse stage values.
- `MainOperation` is supported for custom APIs and virtual table data providers. Internal-only stages 80 and 90 are not supported.
- At least one of `--unsecure`, `--unsecure-file`, `--secure`, or `--secure-file` must be provided.
- When using file-based configuration, the file must exist and be UTF-8 encoded.
- Composite key matching requires exactly one step to match; multiple matches or no matches will fail. Multiple matches list step display names and IDs so you can retry with `--step-id`.
- Secure configuration is stored in the `SdkMessageProcessingStepSecureConfig` entity.

## `step enable` — Enable a plugin step

Enables an existing plugin step. Steps can be identified by step ID, composite key, or solution name (applies to ALL steps in the solution).

### Usage

```bash
dgtp plugin step enable --step-id 00000000-0000-0000-0000-000000000000
dgtp plugin step enable --plugin-type MyNamespace.MyPlugin --message Create --stage PreOperation --entity account
dgtp plugin step enable --solution mysolution
```

### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `--step-id` | No (mutually exclusive with composite key and --solution) | SDK message processing step ID |
| `--plugin-type` | No (part of composite key, mutually exclusive with --step-id and --solution) | Fully qualified plugin type name |
| `--message` | No (part of composite key, mutually exclusive with --step-id and --solution) | SDK message name (Create, Update, Delete, etc.) |
| `--stage` | No (part of composite key, mutually exclusive with --step-id and --solution) | Stage name or numeric value: PreValidation/10, PreOperation/20, MainOperation/30, PostOperation/40 |
| `--entity` | No (part of composite key, mutually exclusive with --step-id and --solution) | Primary entity logical name |
| `--secondary-entity` | No (part of composite key, mutually exclusive with --step-id and --solution) | Secondary entity logical name (for Associate, Disassociate, etc.) |
| `--execution-order` | No (part of composite key, mutually exclusive with --step-id and --solution) | Rank/execution order of the step |
| `-s, --solution` | No (mutually exclusive with --step-id and composite key) | Unique name of the solution; applies to ALL steps in that solution |
| `--reason` | No | Reason for the state change; replaces the step's description when the state actually changes |
| `--dry-run` | No | Show which steps would change without modifying anything |

### Notes

- Steps that are already in the desired state are left untouched (no write, reason not stored) and reported as unchanged.
- The solution name must match exactly. Solutions without plugin steps report that no steps were found.
- A failing step does not stop the remaining steps in `--solution` mode; the command exits with an error if any step failed.
- Exactly one of the following must be provided: `--step-id`, all required composite key options (`--plugin-type`, `--message`, `--stage`, `--entity`), or `--solution`.
- `--secondary-entity` and `--execution-order` are optional composite-key filters and cannot be used with `--step-id` or `--solution`.
- Composite key matching requires exactly one step to match; multiple matches or no matches will fail. Multiple matches list step display names and IDs so you can retry with `--step-id`.

## `step disable` — Disable a plugin step

Disables an existing plugin step. Uses the same identification options as [`enable`](#step-enable--enable-a-plugin-step).

### Usage

```bash
dgtp plugin step disable --step-id 00000000-0000-0000-0000-000000000000
dgtp plugin step disable --plugin-type MyNamespace.MyPlugin --message Create --stage PreOperation --entity account
dgtp plugin step disable --solution mysolution
```

### Options

Same as [`enable`](#step-enable--enable-a-plugin-step).

## `step state set` — Bulk enable/disable plugin steps from config file

Applies enable/disable state to multiple plugin steps defined in a JSON configuration file. Steps can be identified by ID or by composite key (plugin type, message, stage, entity).

### Usage

```bash
dgtp plugin step state set -c step-states.json
```

### Config file format

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
      "enabled": true
    }
  ]
}
```

### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `-c, --config` | Yes | Path to the JSON config file |
| `--dry-run` | No | Show which steps would change without modifying anything |

### Notes

- The config file can reference the [JSON schema](../../../schemas/plugin/state/schema.json) for editor validation.
- Each step must set `enabled` and have either `stepId` or a complete composite key (`pluginType`, `message`, `stage`, `entity`); mixing both is rejected.
- Invalid or unresolvable entries are reported and do not stop the remaining entries; the command exits with an error if any entry failed.
- Steps already in the desired state are left untouched and reported as unchanged.
- `stage` accepts numeric values: 10 (PreValidation), 20 (PreOperation), 30 (MainOperation), 40 (PostOperation).
- Composite key matching requires exactly one step to match; multiple matches or no matches will fail.
- If a `reason` is provided in the config, it replaces the step's description when the state changes.
- The command reports a summary of enabled, disabled, and failed steps.
