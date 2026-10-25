# `solution` — Solution-scoped operations

The `solution` branch hosts commands that act on Dataverse solutions: `version` and `lint` each act on a single solution, `copy-components` copies components from one or more source solutions into a target, and `codegeneration` generates early-bound code.

## `version` — Increment a solution version

| Command | Description |
|---------|-------------|
| `solution version <Solution> [--major\|--minor\|--build\|--revision]` | Increment a solution version (default: `--revision`) |

```bash
dgtp solution version sample_solution --minor
```

## `lint` — Run solution quality gates

| Command | Description |
|---------|-------------|
| `solution lint <Solution> -c ./lint.config.json` | Run the enabled lint rules against the given solution |

`lint` options:

| Option | Description |
|--------|-------------|
| `--rules <id1,id2>` | Restrict this run to a subset of rule ids (intersected with the enabled rules from config) |
| `--fail-on <None\|Info\|Warning\|Error>` | Minimum severity that fails the command (exit code 1). Default `Error`. `None` disables the gate - the command always exits 0, useful for report-only runs |
| `--report <path>` | Write all findings as JSON (each entry includes a `Baselined` flag) |
| `--sarif-output <path>` | Write all findings as a SARIF 2.1.0 log (suppressed results are marked via SARIF `suppressions`) |
| `--baseline <path>` | Path to a SARIF baseline file. Findings whose `RuleId + Solution + ComponentType + ComponentLogicalName/Id` match a baseline entry are excluded from the `--fail-on` gate (they still show up in the console/report/SARIF output, flagged as baselined) |
| `--update-baseline` | Overwrite `--baseline` with the findings from this run instead of gating on them. Requires `--baseline`. Always exits 0 |

Example configuration:

```json
{
  "version": 1,
  "rules": {
    "naming.unmanaged-field-logicalname": {
      "enabled": true,
      "severity": "Error",
      "options": {
        "publisherPrefixes": ["dgt_"]
      }
    },
    "completeness.table-root-component-behavior": {
      "enabled": true,
      "severity": "Error"
    },
    "webresource.jscript-sourcemap": {
      "enabled": true,
      "severity": "Warning"
    }
  }
}
```

Built-in rules:

- **`naming.unmanaged-field-logicalname`** validates unmanaged custom field logical names against the [DIGITALL Nature naming convention](https://digitallnature.github.io/customizing/naming-conventions/): `prfx_fieldname[_type-suffix]`, where the suffix is derived from the attribute's Dataverse type (e.g. `_id` for Lookup, `_set` for Choice, `_cur` for Currency, `_dt`/`_rf`/`_cf`/`_fx` for rollup/calculated/formula modifiers, etc. - see the linked page for the full table). `publisherPrefixes` accepts one or more allowed prefixes (trailing underscore optional) and defaults to `["dgt_"]`. Fields whose logical name contains no underscore at all (e.g. Dataverse-provisioned defaults like `name`, `createdon`, or an auto-created `statecode` on a new custom table) are never flagged, since those are outside of what an unmanaged customization can control.
- **`completeness.table-root-component-behavior`** validates each table's `RootComponentBehavior` against whether the table itself is managed (e.g. ISV-owned), not whether the linted solution is managed - this linter only ever targets unmanaged solutions. An **unmanaged** table must always be added completely (`IncludeSubcomponents` / "Include Entity Metadata and All Assets"); a **managed** table must never be added completely - only its actual delta may be listed explicitly (`DoNotIncludeSubcomponents`) or referenced as a shell (`IncludeAsShellOnly`). No options.
- **`webresource.jscript-sourcemap`** (default severity: `Warning`) flags JScript web resources (`webresourcetype = Script (JScript)`) whose content still contains a source map reference (e.g. `//# sourceMappingURL=...`) - a sign the file was added to the solution unminified (a development build artifact rather than production output). No options.

**Baseline workflow** (accepting existing findings so only *new* violations fail the pipeline):

```bash
# One-time: snapshot the current findings as the accepted baseline
dgtp solution lint sample_solution -c lint.config.json --baseline lint-baseline.sarif.json --update-baseline

# CI: only NEW findings (not in the baseline) fail the build
dgtp solution lint sample_solution -c lint.config.json --baseline lint-baseline.sarif.json --fail-on Error
```

## `copy-components` — Copy solution components between solutions

Copies the `solutioncomponent` rows of one or more source solutions into an unmanaged target solution, using the same `AddSolutionComponentRequest` Dataverse SDK message a maker's "Add existing" action uses under the hood.

| Option | Description |
|--------|-------------|
| `-s, --source <Sol1,Sol2>` | Comma-separated unique names of the solutions to copy components from (required) |
| `--dry-run` | Print the planned changes without adding any component to the target solution |
| `--raw` | Disable best-practice normalization: for tables, preserve only complete vs. non-complete behavior (shell-only sources are treated as non-complete) and skip the managed-active-layer filter |
| `--apps <skip\|strip\|allow>` | How to handle model-driven apps (default `skip`), see below |

Dataverse rejects `DoNotIncludeSubcomponents` on anything but tables, so a model-driven app cannot be added without Dataverse's own expansion. `--apps` controls the trade-off:

| Mode | Behavior |
|------|----------|
| `skip` (default) | Apps are not copied, and neither are app-bound components (app settings, app module components and app elements), which are meaningless without their app. Skipped rows are shown in the plan |
| `strip` | Apps and their app-bound components are copied, then every subcomponent Dataverse added on its own (not part of the plan, not already in the target) is removed again and listed in the output. Verify the app afterwards (e.g. `ValidateApp`) - removing a table or view the app references leaves it with missing dependencies |
| `allow` | Apps and their app-bound components are copied and Dataverse adds whatever it considers part of the app |

By default (best-practice mode, no `--raw`), the command avoids two common causes of solution bloat:

- **Managed tables are never copied completely.** An unmanaged (first-party) table is always added with `RootComponentBehavior = IncludeSubcomponents` (complete); a managed (e.g. ISV-owned) table is added as a skeleton (`DoNotIncludeSubcomponents`) - only its own delta, not the whole table.
- **A managed component (attribute, form, view, workflow, ...) is only copied if it has its own active customization layer** (its top `msdyn_componentlayer` row is the synthetic `Active` layer). A managed component with no active layer is redundant - the managed baseline the target environment already has installed provides it - and is skipped.
- **`AddRequiredComponents` is always `false`.** This keeps Dataverse from adding the "required" dependencies of any component - only the components explicitly present in the source solution(s) are added (model-driven apps are the exception, see `--apps`).

```bash
# Preview what would be copied, without changing the target solution
dgtp solution copy-components target_solution --source dev_solution --dry-run

# Copy with best-practice filtering (default)
dgtp solution copy-components target_solution --source dev_solution_a,dev_solution_b

# Mirror every source component as-is (no managed/active-layer filtering)
dgtp solution copy-components target_solution --source dev_solution --raw

# Also copy model-driven apps, removing the subcomponents Dataverse adds on its own
dgtp solution copy-components target_solution --source dev_solution --apps strip
```

## `codegeneration` (`cg`) — Early-bound code generation

Generates `.cs`, `.ts` and `metadata.xml` model files for Dataverse based on a JSON configuration.

```bash
dgtp solution codegeneration ./generated -c ./genconfig.json
# alias
dgtp solution cg ./generated -c ./genconfig.json
```

| Option | Description |
|--------|-------------|
| `-f`, `--folder` | Alternate name for the model folder (default: `Model`) |
| `-c`, `--config` | Full path to the config file (default: `config.json`) |

Supported V2 JSON schemas are available under [`schemas/codegeneration/v2/`](../../../schemas/codegeneration/v2).
See the [2.x-to-3.x migration guide](../../guides/migrations/2.x-to-3.x.md#code-generation) when upgrading.

### Supported outputs

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

### Configuration

Every config must declare `"version": 2` and a case-sensitive `"type"` value of `"dotnet"` or
`"typescript"`. Put `$schema` in the config to enable editor validation and completion. The
supported schemas list all settings and defaults:

- [.NET config schema](../../../schemas/codegeneration/v2/dotnet.schema.json)
- [TypeScript config schema](../../../schemas/codegeneration/v2/typescript.schema.json)

Entity scope can combine explicit logical names (`entities.names`), all entities from listed
solutions (`entities.fromSolutions`), and a wildcard logical-name mask (`entities.mask`). These
are additive: matching any of the configured selectors includes the entity. Requests and global
option sets are independently selected with `requests` and `optionSets`.

### .NET generation

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

### TypeScript generation

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

### Run generation

Save the .NET and TypeScript examples as separate config files, then run the command once per
target. Both invocations can write to the same model directory because the outputs use separate
subdirectories:

```bash
dgtp solution codegeneration ./generated -c ./genconfig.dotnet.json
dgtp solution codegeneration ./generated -c ./genconfig.typescript.json
```

Use `--folder` (or `-f`) to change the `Model` folder name, and `--config` (or `-c`) to select the
config file. Without `--config`, the command reads `config.json`.

### TypeScript template settings

| Environment variable | Purpose | Default |
|---|---|---|
| `DGTP_TSL_STRICT_MODE` | Enables fail-fast errors for undefined Liquid template values (`1`, `true`, or `yes`) | Enabled automatically in CI environments |
| `DGTP_TSL_MAX_STEPS` | Sets the positive maximum number of Fluid template execution steps | `20000` |

See the [2.x-to-3.x migration guide](../../guides/migrations/2.x-to-3.x.md#code-generation) for changes
when upgrading existing generation configs or code that consumes generated output.
