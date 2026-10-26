# `duplicaterules` — Duplicate rule publication and ownership

Captures and applies publication state and portable ownership of existing duplicate-detection
rules by rule ID. Rule definitions and conditions are not part of this format — only publication
state (`published`) and ownership are transported.

| Command | Description |
|---------|-------------|
| `duplicaterules init <File>` | Capture publication state or ownership of existing duplicate rules |
| `duplicaterules apply <File>` | Apply configured publication state and ownership by duplicate rule ID |

All commands accept the [global options](../configuration.md#global-connectionauthentication-environment-variables):
`--connection`, `--connection-string`, `--no-telemetry`, `--non-interactive`.

## `init` — Capture publication state or ownership

```bash
dgtp duplicaterules init ./duplicaterules.json --entity contact
```

| Option | Description |
|--------|-------------|
| `--entity <BaseTable>` | Exact logical name of the rule base table to restrict capture to |
| `--rule-id <Guid>` | Duplicate rule ID to capture; repeat to select several rules |
| `--capture <state\|owner\|both>` | Capture scope (default: `state`) |
| `--owner-mapping <File>` | Source environment [owner mapping](../owner-mappings.md) required when capturing ownership |
| `--unmapped-owners <fail\|skip>` | Handling of owners without a source mapping key (default: `fail`) |
| `--overwrite` | Replace an existing output file after capture and validation complete |

When neither `--entity` nor `--rule-id` is given, all duplicate rules in the environment are
captured. Use `--entity` to restrict to one base table, or `--rule-id` (repeatable) to select
specific rules.

```bash
# Capture publication state for all rules on the contact table
dgtp duplicaterules init ./duplicaterules.json --entity contact

# Capture state and ownership for specific rules
dgtp duplicaterules init ./duplicaterules.json --rule-id 11111111-1111-1111-1111-111111111111 --capture both --owner-mapping ./owners.source.json
```

## `apply` — Apply publication state and ownership

```bash
dgtp duplicaterules apply ./duplicaterules.json
```

| Option | Description |
|--------|-------------|
| `--owner-mapping <File>` | Target environment [owner mapping](../owner-mappings.md); required for owner changes |
| `--dry-run` | Resolve and report planned changes without writing to Dataverse |
| `--no-wait` | Report accepted publish submissions without waiting for publication to complete |
| `--timeout <Seconds>` | Shared time budget in seconds for observing submitted publishes (default: `300`) |

Apply submits publish requests for rules that should be published, and unpublish requests for
rules that should be unpublished. By default, the command waits for each publish to complete
within the `--timeout` budget. With `--no-wait`, it reports accepted submissions immediately.

```bash
# Preview planned changes
dgtp duplicaterules apply ./duplicaterules.json --dry-run

# Apply with target owner mapping and extended timeout
dgtp duplicaterules apply ./duplicaterules.json --owner-mapping ./owners.target.json --timeout 600

# Submit publishes without waiting
dgtp duplicaterules apply ./duplicaterules.json --no-wait
```

## Configuration file format

The duplicate-rule configuration is a V1 JSON document with a `rules` array. Each entry carries
the stable `ruleId` (duplicate rule GUID), an optional informational `name`, and either
`published` (boolean) or `owner` (logical key) — or both.
See the [JSON schema](../../../schemas/duplicaterules/v1/schema.json) for the full validation rules.

```json
{
  "$schema": "https://digitallpower.dev/schemas/duplicaterules/v1/schema.json",
  "schemaVersion": 1,
  "rules": [
    {
      "ruleId": "44444444-4444-4444-4444-444444444444",
      "name": "Contact Duplicate Detection",
      "published": true,
      "owner": "salesOwner"
    }
  ]
}
```

## ALM workflow

1. **Capture** from the source environment: `dgtp duplicaterules init ./duplicaterules.json --entity contact --capture both --owner-mapping ./owners.source.json`
2. Commit both `duplicaterules.json` and `owners.source.json` to source control
3. Create a target owner mapping (`owners.target.json`) for the target environment
4. **Apply** in the target environment: `dgtp duplicaterules apply ./duplicaterules.json --owner-mapping ./owners.target.json`

Duplicate rules support owning teams (unlike process commands), so the owner mapping can use
`team` selectors for duplicate-rule ownership.
