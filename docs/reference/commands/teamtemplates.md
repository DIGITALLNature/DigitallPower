# `teamtemplates` — Team template transfer

Imports and exports Dataverse team templates as a versioned, portable file. Team templates define
default access rights that are applied when a team is created for a record.

| Command | Description |
|---------|-------------|
| `teamtemplates import <File>` | Import team templates from a versioned file |
| `teamtemplates export <File>` | Export team templates to a versioned file |

All commands accept the [global options](../configuration.md#global-connectionauthentication-environment-variables):
`--connection`, `--connection-string`, `--no-telemetry`, `--non-interactive`.

## `import` — Import team templates

```bash
dgtp teamtemplates import ./teamtemplates.json
```

| Option | Description |
|--------|-------------|
| `--dry-run` | Validate and report the complete plan without writing |
| `--cleanup` | Remove unlisted non-system templates only in explicit cleanup tables, after successful imports |
| `--cleanup-table <LogicalName>` | Exact logical table name eligible for cleanup; repeat for multiple tables. Requires `--cleanup` |

Cleanup is opt-in and scoped: you must pass `--cleanup` together with one or more `--cleanup-table`
values. Only non-system team templates in those tables that are not listed in the import file are
removed. Global cleanup (all tables) is not supported.

```bash
# Preview the import plan
dgtp teamtemplates import ./teamtemplates.json --dry-run

# Import and clean up unlisted templates in the account table
dgtp teamtemplates import ./teamtemplates.json --cleanup --cleanup-table account
```

## `export` — Export team templates

```bash
dgtp teamtemplates export ./teamtemplates.json
```

| Option | Description |
|--------|-------------|
| `--dry-run` | Validate and report export without creating or replacing files |
| `--overwrite` | Allow replacing an existing output file |
| `--table <LogicalName>` | Export only this logical table; repeat for multiple tables. Default: all non-system templates |

```bash
# Export all non-system team templates
dgtp teamtemplates export ./teamtemplates.json --overwrite

# Export only templates for the account and contact tables
dgtp teamtemplates export ./teamtemplates.json --table account --table contact
```

## Configuration file format

The teamtemplates file is a V1 JSON document with a `templates` array. Each template entry carries
its stable `teamtemplateid` (GUID), the `entity` logical name, the template `name`, and the
`defaultaccessrightsmask` (a combination of Read, Write, Append, AppendTo, Delete, Share, Assign
rights). See the [JSON schema](../../../schemas/teamtemplates/v1/schema.json) for the full
validation rules.

```json
{
  "$schema": "https://digitallpower.dev/schemas/teamtemplates/v1/schema.json",
  "schemaVersion": 1,
  "templates": [
    {
      "teamtemplateid": "11111111-1111-1111-1111-111111111111",
      "entity": "account",
      "teamtemplatename": "Sales Team",
      "defaultaccessrightsmask": 262151
    }
  ]
}
```

## ALM workflow

1. **Export** from the source environment: `dgtp teamtemplates export ./teamtemplates.json`
2. Commit the file to source control
3. **Import** into the target environment: `dgtp teamtemplates import ./teamtemplates.json`

The stable `teamtemplateid` ensures templates are matched across environments. Import is
transactional: if a mutation fails, completed changes are not rolled back. The command reports
partial results, and cleanup starts only after all creates and updates succeed.
