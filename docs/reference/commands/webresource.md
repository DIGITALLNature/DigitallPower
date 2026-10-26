# `webresource` — Manage webresources

Commands for deploying Dataverse webresources.

## `push` — Deploy webresources

Pushes webresources from a directory or a single file. Directory targets are scanned recursively
and require an explicit publisher prefix. A solution is used for membership and obsolete-resource
scope; it does not determine the publisher prefix. Created and updated webresources are each
published in a separate request after resource writes and solution-membership additions complete.

### Usage

```bash
# Push all supported files below ./webresources
dgtp webresource push ./webresources \
  --publisher-prefix contoso \
  --solution ContosoCore

# Push one file with an explicit Dataverse logical name
dgtp webresource push ./webresources/app/main.js \
  --name contoso_/app/main.js \
  --solution ContosoCore
```

### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `--solution` | No | Ensures resource membership in the solution and scopes `--delete-obsolete` |
| `--publisher-prefix` | Directory targets | Prefix used to derive unmapped Dataverse logical names |
| `--mapping-file` | No | JSON file mapping directory-relative paths to logical names |
| `--name` | Single-file targets | Explicit Dataverse logical name |
| `--delete-obsolete` | No | Deletes unmanaged resources in the selected solution that are absent from a directory target |
| `--dry-run` | No | Renders the complete plan without Dataverse writes |
| `--confirm` | No | Prompts before applying a plan with changes; suppressed by `--non-interactive` and CI |

### Mapping file

Use `--mapping-file` when the local build layout does not match the desired Dataverse names:

```bash
dgtp webresource push ./webresources \
  --publisher-prefix contoso \
  --mapping-file ./webresource-mappings.json \
  --solution ContosoCore
```

The mapping file is JSON. Its keys are paths relative to the directory target, and its values are
complete Dataverse webresource logical names:

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/webresource/schema.json",
  "mappings": {
    "app/main.js": "contoso_/scripts/main.js",
    "pages/index.html": "contoso_/pages/home.html"
  }
}
```

Mappings override the default `publisherPrefix_/relative/path` naming convention for the paths they
specify; other supported files continue to use the default naming convention. This allows selective
remapping, such as migrating only legacy webresources. The schema is available at
[`schemas/webresource/schema.json`](../../../schemas/webresource/schema.json). The file must contain a
non-null `mappings` object; malformed mapping files stop deployment with an error. Every mapping
key must match a supported file path relative to the target directory. Unmatched entries (including
mappings for files that are absent or unsupported) stop deployment rather than being silently
ignored.

### Planning and execution

The command first renders the local deployment hierarchy and the resulting Dataverse name for
each resource:

```text
./webresources
├── app
│   └── main.js → contoso_/scripts/main.js ~ Update
└── pages
    └── index.html → contoso_/pages/home.html

Solution membership: ContosoCore
WebResource contoso_/scripts/main.js + Add

Obsolete webresources
WebResource contoso_/obsolete.js - Delete

Destructive operations: 1
```

When `--confirm` is specified, the command asks before executing a plan that creates, updates,
adds resources to a solution, or deletes obsolete resources. Declining leaves Dataverse unchanged.
The prompt is skipped in CI and when `--non-interactive` is set; `--dry-run` never prompts.

The tree shows local directory structure; mapping files may intentionally produce a different
Dataverse naming structure. For a normal run, the tree is followed by an execution phase with
checkmarks for completed operations. With `--dry-run`, the tree, missing solution memberships,
and any obsolete resources to delete are rendered without applying changes. Obsolete deletion
requires a directory target and `--solution`. An empty directory is a no-op, including when
`--delete-obsolete` is specified; it cannot be used to delete every webresource from a solution.
