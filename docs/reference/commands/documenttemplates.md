# `documenttemplates` — Document template transfer

Imports and exports active Word and Excel document templates as a portable, versioned package.
Templates are exported as files alongside a manifest that records their metadata and activation state.

| Command | Description |
|---------|-------------|
| `documenttemplates import <Manifest>` | Import a portable document template manifest and activate listed templates |
| `documenttemplates export <Directory>` | Export active document templates as a portable package |

All commands accept the [global options](../configuration.md#global-connectionauthentication-environment-variables):
`--connection`, `--connection-string`, `--no-telemetry`, `--non-interactive`.

## `import` — Import document templates

```bash
dgtp documenttemplates import ./templates/documenttemplates.json
```

| Option | Description |
|--------|-------------|
| `--dry-run` | Prepare all templates against the target without writing |

The manifest references template files by relative path. All referenced files must exist under
the manifest's directory. Import validates the full package (manifest, files, target metadata)
before writing anything.

```bash
# Preview the import plan
dgtp documenttemplates import ./templates/documenttemplates.json --dry-run
```

## `export` — Export document templates

```bash
dgtp documenttemplates export ./templates
```

| Option | Description |
|--------|-------------|
| `--entity <LogicalName>` | Narrow the active selection to one table |
| `--type <word\|excel>` | Narrow the active selection to Word or Excel |
| `--overwrite` | Replace generated destinations only; preserve unrelated files |

Export writes a `documenttemplates.json` manifest and the template files into the target directory.
Only active templates are exported; inactive templates are skipped.

```bash
# Export all active Word and Excel templates
dgtp documenttemplates export ./templates --overwrite

# Export only Word templates for the account table
dgtp documenttemplates export ./templates --entity account --type word
```

## Configuration file format

The manifest is a strict V1 JSON document with a `templates` array. Each entry records the
template's stable `sourceId` (GUID), `name`, `documentType` (`word` or `excel`), `entity` logical
name, relative `file` path, `languageCode`, and optional `description`.
See the [JSON schema](../../../schemas/documenttemplates/v1/schema.json) for the full validation rules.

```json
{
  "$schema": "https://digitallpower.dev/schemas/documenttemplates/v1/schema.json",
  "schemaVersion": 1,
  "templates": [
    {
      "sourceId": "22222222-2222-2222-2222-222222222222",
      "name": "Quote Template",
      "documentType": "word",
      "entity": "quote",
      "file": "quote-template.docx",
      "languageCode": 1033,
      "description": "Standard quote document template"
    }
  ]
}
```

## ALM workflow

1. **Export** from the source environment: `dgtp documenttemplates export ./templates`
2. Commit the directory (manifest + template files) to source control
3. **Import** into the target environment: `dgtp documenttemplates import ./templates/documenttemplates.json`

The stable `sourceId` ensures templates are matched across environments. Import activates the
listed templates after uploading their file content.
