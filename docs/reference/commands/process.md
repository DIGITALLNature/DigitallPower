# `process` — Process state and ownership

Captures and applies explicit process state (active/inactive) and portable ownership by workflow ID.
Supports dry-run previews, preflight validation, and optional run-level impersonation.

| Command | Description |
|---------|-------------|
| `process init <File>` | Capture process state or ownership from selected solutions |
| `process apply <File>` | Apply configured process state and ownership by workflow ID |

All commands accept the [global options](../configuration.md#global-connectionauthentication-environment-variables):
`--connection`, `--connection-string`, `--no-telemetry`, `--non-interactive`.

## `init` — Capture process state or ownership

```bash
dgtp process init ./processes.json --solution ContosoCore
```

| Option | Description |
|--------|-------------|
| `--solution <UniqueName>` | Exact unique name of a solution to include; repeat to combine solution scopes (required) |
| `--capture <state\|owner\|both>` | Capture scope (default: `state`) |
| `--owner-mapping <File>` | Source environment [owner mapping](../owner-mappings.md) required when capturing ownership |
| `--unmapped-owners <fail\|skip>` | Handling of owners without a source mapping key (default: `fail`) |
| `--overwrite` | Replace an existing output file after capture and validation complete |

Capture resolves process definitions from the specified solutions and writes a configuration file
with explicit state and/or ownership intent. When capturing ownership, each process owner is
reverse-matched to a logical key in the source owner mapping.

```bash
# Capture only process state
dgtp process init ./processes.json --solution ContosoCore

# Capture both state and ownership
dgtp process init ./processes.json --solution ContosoCore --capture both --owner-mapping ./owners.source.json

# Combine multiple solution scopes
dgtp process init ./processes.json --solution ContosoCore --solution ContosoSales --capture state
```

## `apply` — Apply process state and ownership

```bash
dgtp process apply ./processes.json
```

| Option | Description |
|--------|-------------|
| `--owner-mapping <File>` | Target environment [owner mapping](../owner-mappings.md); required for owner changes and `--activate-as` |
| `--activate-as <OwnerKey>` | Run every state transition as the mapped Dataverse user |
| `--activate-as-owner` | Run each process state transition as its configured target owner |
| `--dry-run` | Resolve and report transitions without writing to Dataverse |

`--activate-as` and `--activate-as-owner` are mutually exclusive. Apply validates the entire
configuration before writing, then executes transitions in order. With `--dry-run`, the command
reports the planned transitions without touching Dataverse.

```bash
# Preview planned transitions
dgtp process apply ./processes.json --dry-run

# Apply with target owner mapping
dgtp process apply ./processes.json --owner-mapping ./owners.target.json

# Apply with run-level impersonation
dgtp process apply ./processes.json --owner-mapping ./owners.target.json --activate-as integrationUser
```

## Configuration file format

The process configuration is a V1 JSON document with a `processes` array. Each entry carries the
stable `processId` (workflow GUID), an optional informational `name`, and either `active`
(boolean) or `owner` (logical key) — or both.
See the [JSON schema](../../../schemas/process/v1/schema.json) for the full validation rules.

```json
{
  "$schema": "https://digitallpower.dev/schemas/process/v1/schema.json",
  "schemaVersion": 1,
  "processes": [
    {
      "processId": "33333333-3333-3333-3333-333333333333",
      "name": "Account Creation Workflow",
      "active": true,
      "owner": "salesOwner"
    }
  ]
}
```

## ALM workflow

1. **Capture** from the source environment: `dgtp process init ./processes.json --solution MySolution --capture both --owner-mapping ./owners.source.json`
2. Commit both `processes.json` and `owners.source.json` to source control
3. Create a target owner mapping (`owners.target.json`) for the target environment
4. **Apply** in the target environment: `dgtp process apply ./processes.json --owner-mapping ./owners.target.json`

Owner mappings are environment-specific: the same logical key (e.g. `salesOwner`) resolves to
different principals in the source and target environments.
