# `codegeneration` (`cg`) — Early-bound code generation

Generates `.cs`, `.ts` and `metadata.xml` model files for Dataverse based on a JSON configuration.

```bash
dgtp codegeneration ./generated -c ./genconfig.json
# alias
dgtp cg ./generated -c ./genconfig.json
```

| Option | Description |
|--------|-------------|
| `-f`, `--folder` | Alternate name for the model folder (default: `Model`) |
| `-c`, `--config` | Full path to the config file (default: `config.json`) |

Supported V2 JSON schemas are available under [`schemas/codegeneration/v2/`](../../../schemas/codegeneration/v2).
See the [2.x-to-3.x migration guide](../../guides/migrations/2.x-to-3.x.md#code-generation) when upgrading.

## Supported outputs

Code generation reads Dataverse metadata and requires an authenticated Dataverse connection. Each
config selects one target; create separate config files and invoke the command once for every target
you want to generate:

- **.NET** — C# early-bound classes for Modern or Framework targets, request wrappers and message
  constants, and global option-set constants. XML metadata files are optional.
- **TypeScript** — ambient entity and form typings, option-set and SDK message declarations,
  parameterized Custom API wrappers, and optional XrmMock form test helpers.

Generated files are written below `<target-directory>/<folder>` (the default folder is `Model`):
`.NET` files go in `DotNet`, optional XML metadata in `MetaData`, and TypeScript output in
`TypeScript`. Generation replaces generated files in the selected output area; keep custom files
and edits elsewhere or back them up before regenerating. TypeScript generation clears its entire
`TypeScript` subtree.

## Configuration

Every config must declare `"version": 2` and a case-sensitive `"type"` value of `"dotnet"` or
`"typescript"`. Put `$schema` in the config to enable editor validation and completion. The
supported schemas list all settings and defaults:

- [.NET config schema](../../../schemas/codegeneration/v2/dotnet.schema.json)
- [TypeScript config schema](../../../schemas/codegeneration/v2/typescript.schema.json)

Entity scope can combine explicit logical names (`entities.names`), all entities from listed
solutions (`entities.fromSolutions`), and a wildcard logical-name mask (`entities.mask`). These
are additive: matching any of the configured selectors includes the entity. Requests and global
option sets are independently selected with `requests` and `optionSets`.

## .NET generation

The default target is `Modern`, intended for .NET 8 and later. Select `Framework` for .NET Framework
4.6.2 projects. The `output.include` settings control context, option-set constants, logical-name
constants, relationship constants, navigation properties, entity type codes, alternate keys, and
optional metadata XML. These sections are enabled by default except metadata. `output.virtual`
makes entity properties virtual, while `output.editableReadOnly` emits setters for read-only
attributes.

This example generates entities from a solution and writes metadata XML as well:

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/codegeneration/v2/dotnet.schema.json",
  "version": 2,
  "type": "dotnet",
  "namespace": "Contoso.Dataverse.Model",
  "entities": { "fromSolutions": ["ContosoCore"] },
  "requests": ["contoso_ApproveOrder"],
  "optionSets": ["contoso_orderstatus"],
  "output": {
    "target": "Modern",
    "include": { "metadata": true }
  }
}
```

`requests` generates SDK message name constants, and messages with parameter metadata also produce
typed request/response wrappers. `optionSets` selects global option sets to emit as constants.

## TypeScript generation

TypeScript generation emits entity declarations for scoped entities and form typings for their
forms. By default, all forms for each scoped entity are generated. Set `output.forms.filter` to an
allowlist of form identifiers to narrow this, or set `output.forms.fromSolutions` to include only
forms belonging to the configured entity solutions. Set `output.forms.testHelpers` to generate
XrmMock helpers alongside form typings; this is disabled by default.

Form identifiers include the entity, environment-specific form name, and form type. Generate without
a filter first, then use the generated form declaration's filename without `.d.ts` as its identifier
(for example, `account.account_main.form` for a main form named `Account`).

Requests generate SDK message name declarations. Parameterized Custom APIs also generate typed
wrappers by default; set `output.customApis` to `false` to omit those wrappers. Option-set
declarations are controlled by `optionSets`.

The optional `language` is a Dataverse LCID used to localize generated labels. When omitted or
`null`, the organization base language is used. Form names are resolved using the connecting user's
Dataverse UI language; if that differs from the configured label language, `dgtp` warns because
form names and form filters may not match. The TypeScript `namespace` property is accepted but is
currently unused.

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/codegeneration/v2/typescript.schema.json",
  "version": 2,
  "type": "typescript",
  "entities": { "names": ["account"], "fromSolutions": ["ContosoCore"] },
  "requests": ["contoso_ApproveOrder"],
  "optionSets": ["contoso_orderstatus"],
  "output": {
    "forms": {
      "testHelpers": true
    }
  }
}
```

## Run generation

Save the .NET and TypeScript examples as separate config files, then run the command once per
target. Both invocations can write to the same model directory because the outputs use separate
subdirectories:

```bash
dgtp codegeneration ./generated -c ./genconfig.dotnet.json
dgtp codegeneration ./generated -c ./genconfig.typescript.json
```

Use `--folder` (or `-f`) to change the `Model` folder name, and `--config` (or `-c`) to select the
config file. Without `--config`, the command reads `config.json`.

## TypeScript template settings

| Environment variable | Purpose | Default |
|---|---|---|
| `DGTP_TSL_STRICT_MODE` | Enables fail-fast errors for undefined Liquid template values (`1`, `true`, or `yes`) | Enabled automatically in CI environments |
| `DGTP_TSL_MAX_STEPS` | Sets the positive maximum number of Fluid template execution steps | `20000` |

See the [2.x-to-3.x migration guide](../../guides/migrations/2.x-to-3.x.md#code-generation) for changes
when upgrading existing generation configs or code that consumes generated output.
