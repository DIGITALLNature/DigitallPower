<h1 align="center"> DigitallPower CLI </h1> <br>

<br/>
<p align="center">
    <a href="LICENSE" target="_blank">
        <img src="https://img.shields.io/github/license/DIGITALLNature/DigitallPower.svg" alt="GitHub license">
    </a>
    <a href="https://github.com/DIGITALLNature/DigitallPower/releases" target="_blank">
        <img src="https://img.shields.io/github/tag/DIGITALLNature/DigitallPower.svg" alt="GitHub tag (latest SemVer)">
    </a>
    <a href="https://www.nuget.org/packages/dgt.power" target="_blank">
        <img src="https://img.shields.io/nuget/v/dgt.power" alt="Nuget">
    </a>
    <a href="https://github.com/DIGITALLNature/DigitallPower/graphs/contributors" target="_blank">
        <img src="https://img.shields.io/github/contributors-anon/DIGITALLNature/DigitallPower.svg" alt="GitHub contributors">
    </a>
    <a href="https://sonarcloud.io/project/overview?id=DIGITALLNature_DigitallPower" target="_blank">
        <img src="https://sonarcloud.io/api/project_badges/measure?project=DIGITALLNature_DigitallPower&metric=alert_status" alt="Quality Gate Status">
    </a>
</p>
<br/>

# Introduction

**DIGITALLPOWER** — the .NET tool for the Microsoft Power Platform from DIGITALL. A swiss army knife for all ALM tasks where the Power Platform CLI (`pac`) still has weaknesses.

DigitallPower (`dgtp`) is a cross-platform global .NET tool that helps developers, makers and operators to **develop, deploy and maintain** Microsoft Dataverse / Power Platform solutions. It complements the official tooling and focuses on the day-to-day pain points DIGITALL encounters in real-world enterprise projects — with the goal of universally helping the wider community.

## Table of Contents

- [Features at a Glance](#-features-at-a-glance)
- [Installation](#-installation)
- [Quick Start](#-quick-start)
- [Tab Completion](#-tab-completion)
- [Configuration](#%EF%B8%8F-configuration)
- [Command Reference](#-command-reference)
  - [connection](#connection--authentication--environments)
  - [export](#export--export-dataverse-artifacts)
  - [import](#import--import-dataverse-artifacts)
  - [analyze](#analyze--solution-analysis)
  - [maintenance](#maintenance--operational-tasks)
  - [codegeneration](#codegeneration-cg--early-bound-code-generation)
  - [push](#push--deploy-artifacts)
- [Solution Architecture](#-solution-architecture)
- [Repository Layout](#-repository-layout)
- [Build & Test](#%EF%B8%8F-build--test)
- [Requirements](#-requirements)
- [Community & Contributions](#%EF%B8%8F-community-and-contributions)
- [License](#-license)

## ✨ Features at a Glance

| Area | What it does |
|------|--------------|
| **Connections** | Manage multiple Dataverse environment connections (interactive, MSAL, client-secret) |
| **Export** | Extract configuration data (team templates, queues, SLAs, calendars, routing rules, document/Outlook templates, user roles, bulk delete jobs) from an environment |
| **Import** | Import the previously exported artifacts into another environment — ideal for ALM pipelines |
| **Analyze** | Inspect solutions for redundant components, active-layer issues, top-layer problems and obsolete patches |
| **Maintenance** | Bulk-delete records, manage auto-number formats, protect calculated fields, increment solution versions, update workflow states, filter PowerFx plugin steps, ensure SDK step status, and more |
| **Code Generation** | Generate strongly-typed C# (early-bound), TypeScript and metadata files for Dataverse entities |
| **Push** | Push web resources and plugin assemblies directly into a target solution |

## 🚀 Installation

DigitallPower is published as a [.NET global tool](https://www.nuget.org/packages/dgt.power):

```bash
dotnet tool install -g dgt.power
```

Update to the latest version:

```bash
dotnet tool update -g dgt.power
```

After installation the command `dgtp` is available globally.

## ⚡ Quick Start

```bash
# 1. Create and select a connection
dgtp connection create dev --url https://contoso-dev.crm4.dynamics.com
dgtp connection select dev

# 2. Verify the connection by listing connections
dgtp connection list

# 3. Export configuration data from the environment
dgtp export queues --filedir ./out/queues

# 4. Generate early-bound C# classes
dgtp codegeneration ./generated -c ./genconfig.json
```

Run `dgtp --help` or `dgtp <command> --help` to discover all options.

## 🔁 Tab Completion

`dgtp` supports shell tab completion via [dotnet-suggest](https://github.com/dotnet/command-line-api/blob/main/docs/dotnet-suggest.md).

### Quick setup (recommended)

Run both steps in one command:

```bash
# Install dotnet-suggest first if you haven't already
dotnet tool install -g dotnet-suggest

# Register dgtp AND install the shell shim
dgtp complete setup --all
```

Then reload your shell (`source ~/.zshrc` or open a new terminal).

### Manual setup

**1. Install the `dotnet-suggest` global tool:**

```bash
dotnet tool install -g dotnet-suggest
```

**2. Register `dgtp` with dotnet-suggest:**

```bash
dgtp complete setup
```

**3. Install the shell shim:**

```bash
dgtp complete install-shell
```

This auto-detects your current shell and writes the shim to your RC file.  
Use `--shell bash|zsh|pwsh|fish` to override the detected shell.  
Use `--dry-run` to preview what would be written without making changes.

The shim is written with idempotency markers — running the command again does nothing if already installed:

```
# >>> dgtp tab completion start >>>
...dotnet-suggest shim script...
# <<< dgtp tab completion end <<<
```

### `complete` command reference

| Command | Description |
|---------|-------------|
| `dgtp complete setup` | Registers dgtp with dotnet-suggest |
| `dgtp complete setup --all` | Registers AND installs shell shim |
| `dgtp complete setup --all --shell bash` | Same, with explicit shell override |
| `dgtp complete install-shell` | Installs shell shim (auto-detects shell) |
| `dgtp complete install-shell --shell zsh` | Installs shim for zsh explicitly |
| `dgtp complete install-shell --dry-run` | Preview without writing |

### What gets completed

| Input | Completions |
|-------|-------------|
| `dgtp <TAB>` | `export` `import` `maintenance` `analyze` `connection` `codegeneration` `push` `complete` |
| `dgtp export <TAB>` | `teamtemplates` `bulkdeletes` `queues` … |
| `dgtp export --<TAB>` | `--filedir` `--filename` `--inline` `--no-telemetry` |
| `dgtp connection <TAB>` | `list` `create` `delete` `select` `status` `refresh` |

> **Note:** Tab completion is static (command names and option flags only). It does not connect to Dataverse and requires no network access.

## ⚙️ Configuration

Configuration is layered (later sources override earlier ones):

1. Built-in defaults (e.g. `pollrate = 5000`)
2. `dgtp.json` in the current working directory (optional)
3. Environment variables prefixed with `dgtp:` (e.g. `dgtp:pollrate=10000`)
4. Command-line arguments

Example `dgtp.json`:

```json
{
  "pollrate": 10000
}
```

Connection data (credentials, selected connection) is stored in the user's [Isolated Storage](https://learn.microsoft.com/dotnet/standard/io/isolated-storage), not in the repository.

JSON schemas for the various configuration files used by the modules live under [`schemas/`](schemas) and can be referenced from your own config files via the `$schema` property for autocomplete in modern editors.

## 📚 Command Reference

The CLI is organized into branches. The general invocation pattern is:

```
dgtp <branch> <command> [arguments] [options]
```

### `connection` — Authentication & environments

| Command | Description |
|---------|-------------|
| `connection list` | List configured connections |
| `connection create <name> --url <url>` | Create a new MSAL connection |
| `connection create <name> --connection-string <string>` | Create a connection using a full Dataverse connection string (service principal, etc.) |
| `connection create ... --no-verify` | Skip the post-create connectivity check |
| `connection select <name>` | Set the active connection |
| `connection delete <name>` | Delete a specific connection |
| `connection delete --all` | Delete all connections |
| `connection status` | Check whether the current MSAL token is valid (exit 0 = valid, 2 = login required) |
| `connection refresh` | Force an interactive MSAL browser login and save the refreshed token |

> **Note:** The `profile` command is a deprecated alias for `connection` and will be removed in a future release.

Example:

```bash
dgtp connection create prod --url https://contoso.crm4.dynamics.com
```

Agent-friendly auth workflow:

```bash
dgtp connection status       # exit 0 = valid, exit 2 = login required
dgtp connection refresh      # re-authenticate interactively
dgtp connection status       # confirm valid before proceeding
```

### `export` — Export Dataverse artifacts

Exports configuration data from the currently selected environment into JSON files.

| Command | Description |
|---------|-------------|
| `export teamtemplates` | Team templates |
| `export bulkdeletes` | Bulk delete jobs |
| `export queues` | Queues |
| `export documenttemplates` | Document templates |
| `export calendars` | Calendars |
| `export slaconfigs` | SLAs |
| `export routingruleconfigs` | Routing rules |
| `export userroles` | User → security role assignments |
| `export outlooktemplates` | Outlook templates |

All export commands accept `--filedir <path>` to control the output directory.

```bash
dgtp export bulkdeletes --filedir ./out/bulkdeletes
```

### `import` — Import Dataverse artifacts

Counterpart to `export`. Reads the previously exported JSON files and applies them to the currently selected environment.

| Command | Description |
|---------|-------------|
| `import outlooktemplates` | Outlook templates |
| `import userroles` | User → security role assignments |
| `import queues` | Queues |
| `import teamtemplates` | Team templates |
| `import bulkdeletes` | Bulk delete jobs |
| `import documenttemplates` | Document templates |
| `import secureconfigs` | Secure configurations of plugin steps |
| `import calendar` | Calendars |
| `import slaconfigs` | SLAs |
| `import routingruleconfigs` | Routing rules |

```bash
dgtp import outlooktemplates --filedir ./out/outlooktemplates
```

### `analyze` — Solution analysis

Static analysis of one or many Dataverse solutions.

| Command | Description |
|---------|-------------|
| `analyze entityallassets` | Scan solutions for entities containing all assets |
| `analyze noactivelayer` | Find unmanaged solution components without an active layer |
| `analyze activelayer` | Find managed solution components that already received an active layer |
| `analyze toplayer` | Find managed solution components where the given solution is not the top layer |
| `analyze redundantcomponents` | Find components contained in multiple solutions |
| `analyze redundantpatches` | Find patches that are no longer needed because their components are no longer top-layer |

```bash
dgtp analyze noactivelayer --inline solution1,solution2
```

### `maintenance` — Operational tasks

Day-to-day administrative actions against a live environment.

| Command | Description |
|---------|-------------|
| `maintenance bulkdelete` | Run a bulk delete job for a given FetchXML and wait for completion |
| `maintenance autonumber` | Set auto-number formats for columns from a JSON config |
| `maintenance protectfields` | Prevent all calculated fields from receiving an active layer |
| `maintenance carrierinfo` | Export carrier solutions metadata to JSON |
| `maintenance solution-version <solution> [--major\|--minor\|--build\|--revision]` | Increment a solution version |
| `maintenance createworkflowstate` | Generate a workflow-state configuration file |
| `maintenance workflowstate` | Apply a workflow-state configuration |
| `maintenance removeredundantcomponents <target> <source> [--dryrun]` | Remove solution components that already exist in another solution |
| `maintenance filterfxplugins` | Add message filtering for PowerFx plugin steps |
| `maintenance ensuresdksteps` | Enable/disable SDK steps within a solution |

```bash
dgtp maintenance solution-version sample_solution --minor
dgtp maintenance bulkdelete --inline "<fetchxml>...</fetchxml>"
```

### `codegeneration` (`cg`) — Early-bound code generation

Generates `.cs`, `.ts` and `metadata.xml` model files for Dataverse based on a JSON configuration.

```bash
dgtp codegeneration ./generated -c ./genconfig.json
# alias
dgtp cg ./generated -c ./genconfig.json
```

JSON schemas for all config versions are available under [`schemas/codegeneration/`](schemas/codegeneration).

#### V2 config (recommended)

V2 configs use `"version": 2` and a `"type"` discriminator to produce one focused file per output target. The design separates two concerns:

- **Scope** — what to load from Dataverse (`entities`, `requests`, `optionSets`)
- **Output** — what artefacts to write (type-specific `output` object)

Only `"type"` is required; every other property has a sensible default and may be omitted.

> **Tip:** Run one command per config file — `dgtp cg ./generated -c ./genconfig.dotnet.json` and `dgtp cg ./generated -c ./genconfig.typescript.json`.

##### Shared root properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `version` | integer | `1` | Must be `2` to use the V2 engine |
| `type` | `"dotnet"` \| `"typescript"` | — | **Required.** Selects generator and schema |
| `namespace` | string \| null | `null` (TS) / `"Digitall.Dataverse.Model"` (.NET) | Root namespace for generated classes |
| `language` | integer \| null | `null` | LCID for label localization (e.g. `1033` for English). `null` or omitted = use the organization's base language |

##### Scope properties (shared by both types)

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `entities.names` | string[] | `[]` | Explicit list of entity logical names |
| `entities.fromSolutions` | string[] | `[]` | Include all entities from these solutions |
| `entities.mask` | string \| null | `null` | Publisher-prefix wildcard (e.g. `"contoso_*"`) |
| `requests` | string[] | `[]` | SDK message / custom action names; generates message constants |
| `optionSets` | string[] | `[]` | Global option set logical names |

The three entity inputs are combined as an **additive union** — an entity matches if it appears in `names`, belongs to any listed solution, or matches the `mask` pattern.

##### TypeScript output properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `output.forms` | object \| absent | *(all forms)* | Omit entirely to generate all forms for all scoped entities |
| `output.forms.filter` | string[] | `[]` | Restrict to specific forms by `"entityLogicalName.formName"`; empty = all forms |
| `output.forms.fromSolutions` | boolean | `false` | Only include forms that belong to the solutions listed in `entities.fromSolutions` |
| `output.forms.testHelpers` | boolean | `false` | Generate XrmMock test helper files alongside form helpers |
| `output.customApis` | boolean | `true` | Generate typed Custom API request/response wrappers for parameterised messages |

##### .NET output properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `output.target` | `"Modern"` \| `"Framework"` | `"Modern"` | `Modern` = net8.0+ (nullable, implicit usings); `Framework` = .NET Framework 4.6.2 (Dataverse plugins, no nullable) |
| `output.virtual` | boolean | `false` | Add the `virtual` keyword to generated entity properties, enabling mocking and subclass overrides |
| `output.editableReadOnly` | boolean | `false` | Treat read-only attributes as editable |
| `output.include.context` | boolean | `true` | Generate `DataContext` class |
| `output.include.options` | boolean | `true` | Generate `OptionSetValues` enum classes |
| `output.include.logicalNames` | boolean | `true` | Generate logical-name string constants |
| `output.include.relations` | boolean | `true` | Generate relationship metadata |
| `output.include.navigationProps` | boolean | `true` | Generate navigation properties |
| `output.include.entityTypeCode` | boolean | `true` | Generate entity type code constants |
| `output.include.alternateKeys` | boolean | `true` | Generate alternate-key members |
| `output.include.metadata` | boolean | `false` | Write `metadata.xml` sidecar files |

##### Minimal examples

Minimal .NET config (entities from a solution, all defaults):

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/beta/schemas/codegeneration/v2/dotnet.schema.json",
  "version": 2,
  "type": "dotnet",
  "entities": { "fromSolutions": ["ContosoCore"] }
}
```

Minimal TypeScript config (entities from a solution, all forms, no test helpers):

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/beta/schemas/codegeneration/v2/typescript.schema.json",
  "version": 2,
  "type": "typescript",
  "entities": { "fromSolutions": ["ContosoCore"] }
}
```

##### Full examples

**.NET** — `genconfig.dotnet.json`:

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/beta/schemas/codegeneration/v2/dotnet.schema.json",
  "version": 2,
  "type": "dotnet",
  "namespace": "Contoso.Dataverse.Model",
  "entities": {
    "names": ["account", "contact"],
    "fromSolutions": ["ContosoCore"],
    "mask": "contoso_*"
  },
  "requests": ["contoso_ApproveOrder"],
  "optionSets": ["contoso_status"],
  "output": {
    "target": "Modern",
    "virtual": false,
    "editableReadOnly": false,
    "include": {
      "context": true,
      "options": true,
      "logicalNames": true,
      "relations": true,
      "navigationProps": true,
      "entityTypeCode": true,
      "alternateKeys": true,
      "metadata": false
    }
  }
}
```

**TypeScript** — `genconfig.typescript.json`:

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/beta/schemas/codegeneration/v2/typescript.schema.json",
  "version": 2,
  "type": "typescript",
  "entities": {
    "names": ["account", "contact"],
    "fromSolutions": ["ContosoCore"],
    "mask": "contoso_*"
  },
  "requests": ["contoso_ApproveOrder"],
  "optionSets": ["contoso_status"],
  "output": {
    "forms": {
      "filter": ["account.Account Main Form"],
      "fromSolutions": false,
      "testHelpers": true
    },
    "customApis": true
  }
}
```

#### V1 config (legacy, deprecated)

V1 configs use a single file for both .NET and TypeScript output and are detected by `"version": 1` (or the absence of a `version` field). They continue to work unchanged — they are mapped internally to the V2 runtime shape before execution.

```json
{
  "version": 1,
  "Entities": ["account", "contact"],
  "Solutions": ["ContosoCore"],
  "Actions": ["contoso_ApproveOrder"],
  "GlobalOptionSets": ["contoso_status"],
  "NameSpace": "Contoso.Dataverse.Model",
  "TypescriptGeneratorVersion": "Light",
  "SuppressMetaData": true
}
```

##### V1 → V2 migration

| V1 property | V2 equivalent |
|-------------|--------------|
| `Entities` | `entities.names` |
| `Solutions` | `entities.fromSolutions` |
| `EntityMask` | `entities.mask` |
| `Actions` / `SdkMessages` | `requests` |
| `GlobalOptionSets` | `optionSets` |
| `NameSpace` | `namespace` |
| `XrmMockFormHelpers` | `output.forms.testHelpers` |
| `OnlyFormsFromSolutions` | `output.forms.fromSolutions` |
| `SuppressMetaData` | `output.include.metadata: false` |
| `Target` | `output.target` |

> **Note:** V1 attribute-level filters (`EntityFilters`, `EntityRefFilters`, `EntityFormFilters`) have no V2 equivalent — they were removed by design to keep the V2 schema focused.

#### TypeScript environment variables

| Environment variable | Purpose | Default |
|---|---|---|
| `DGT_POWER_TSL_STRICT_MODE` | Enables fail-fast handling for undefined Liquid values (`1` / `true` / `yes`) | Falls back to CI-agent detection |
| `DGT_POWER_TSL_MAX_STEPS` | Overrides Fluid template execution step limit with a positive integer | `20000` |

### `push` — Deploy artifacts

Pushes a plugin assembly or web resource into a target solution.

```bash
dgtp push ./bin/Release/MyPlugin.dll --solution mysolution
dgtp push ./bin/Release/MyPlugin.1.0.0.nupkg --solution mysolution
```

#### Supported Registration Attributes

When pushing a plugin assembly, `push` evaluates the following attributes from the `dgt.registration` package:

| Attribute | Purpose |
|-----------|---------|
| `PluginRegistrationAttribute` | Registers plugin steps (message, stage, mode, entity filters, images) |
| `CustomApiRegistrationAttribute` | Links a plugin type to a Custom API by message name |
| `CustomDataProviderRegistrationAttribute` | Generates data provider steps (Retrieve, RetrieveMultiple, Create, Update, Delete) for virtual entities |
| `WorkflowRegistrationAttribute` | Marks workflow activities with group/name metadata |
| `ManagedIdentityRegistrationAttribute` | Links the assembly to an Azure Managed Identity for secure authentication |

#### Managed Identity Support

When a plugin assembly is decorated with `ManagedIdentityRegistrationAttribute` (assembly-level), the push module automatically:
1. Looks up an existing `managedidentity` record by `ApplicationId` (ClientId)
2. Creates one if not found (with `CredentialSource=ManagedIdentity`, `SubjectScope=Global`)
3. Links the `PluginAssembly.ManagedIdentityId` to the managed identity record

This enables plugins to use Azure Managed Identity for secure service-to-service authentication without manual registration steps.

#### Assembly Version Upgrade

When a plugin assembly's **major or minor** version changes (e.g. `1.0.0.0` → `1.1.0.0`), the push command creates a **new** assembly record in Dataverse (since a different version constitutes a different assembly identity). The tool handles reference migration:

| Flag | Behavior |
|------|----------|
| *(default)* | New assembly is created; Custom API references are migrated to new types automatically |
| `--delete-on-upgrade` | Migrates **plugin steps** and Custom APIs to the new assembly, then deletes the old assembly |
| `--no-migrate-custom-apis` | Skips Custom API migration (ignored when `--delete-on-upgrade` is set, since deletion requires migration) |

> **Plugin Packages (.nupkg):** No special handling needed — the platform manages assembly GUIDs within a package. Content updates preserve all references automatically.

## 🏗 Solution Architecture

DigitallPower is built as a modular CLI. The host project (`dgt.power`) wires up dependency injection (`Microsoft.Extensions.DependencyInjection`), configuration (`Microsoft.Extensions.Configuration`) and the [Spectre.Console.Cli](https://spectreconsole.net/cli/) command framework, and then registers commands contributed by independent feature modules.

```
                ┌──────────────────────────────────────────┐
                │              dgt.power (dgtp)            │
                │  Program.cs · DI container · CLI host    │
                └────────────────┬─────────────────────────┘
                                 │ references
       ┌─────────────────────────┼─────────────────────────────┐
       │            │            │            │                │
┌──────┴─────┐ ┌────┴─────┐ ┌────┴─────┐ ┌────┴──────┐  ┌──────┴───────┐
│ connection │ │  export  │ │  import  │ │  analyze  │  │ maintenance  │
└────────────┘ └──────────┘ └──────────┘ └───────────┘  └──────────────┘
       │            │            │            │                │
       └──────┬─────┴────────────┴────────────┴────────────────┘
              │
       ┌──────┴──────────────┐   ┌──────────────────────────────┐
       │  codegeneration     │   │            push              │
       └─────────────────────┘   └──────────────────────────────┘
                  │                            │
                  └────────────┬───────────────┘
                               ▼
                  ┌──────────────────────────┐
                  │     dgt.power.common     │
                  │  (Xrm connection, file   │
                  │   access, tracer,        │
                  │   connection management, │
                  │   shared base commands)  │
                  └──────────────────────────┘
                               │
                               ▼
                ┌──────────────────────────────────┐
                │  Microsoft.PowerPlatform         │
                │  .Dataverse.Client / Xrm SDK     │
                └──────────────────────────────────┘
```

Key design principles:

- **Module isolation.** Every feature area (`analyzer`, `codegeneration`, `connection`, `export`, `import`, `maintenance`, `push`) is an independent project under `src/modules/`. Modules expose `Spectre.Console.Cli`-style command classes that are registered by the host.
- **Shared kernel.** `dgt.power.common` provides the cross-cutting infrastructure: the `IXrmConnection`, connection management, file I/O helpers, base commands, tracing and exception types (including standard .NET exception constructor overloads for integration-safe error handling), plus shared runtime environment helpers (`ExecutionEnvironment`) used by multiple modules.
- **DI everywhere.** Long-lived services (HTTP/NuGet clients, connection manager, caches, JSON options) are singletons; per-command services (metadata, config resolver, generators, file service) are scoped; the `IOrganizationService` is lazily resolved from the active connection via `IXrmConnection.ConnectAsync()`.
- **Configuration layering.** `dgtp.json` ⇒ `dgtp:*` environment variables ⇒ command-line arguments allow the same binary to be used locally and in CI/CD without code changes.
- **Update awareness.** A `VersionCheckInterceptor` queries NuGet on each run to warn the user when a newer version of `dgt.power` is available.

## 📁 Repository Layout

```
DigitallPower/
├── src/
│   ├── dgt.power/                # CLI host project (produces the `dgtp` tool)
│   ├── dgt.power.common/         # Shared infrastructure (connection, profiles, IO, tracer)
│   ├── models/                   # Shared DTOs / data contracts
│   └── modules/
│       ├── dgt.power.analyzer/        # `analyze` commands
│       ├── dgt.power.codegeneration/  # `codegeneration` / `cg` command
│       ├── dgt.power.connection/      # `connection` commands (auth management)
│       ├── dgt.power.export/          # `export` commands
│       ├── dgt.power.import/          # `import` commands
│       ├── dgt.power.maintenance/     # `maintenance` commands
│       ├── dgt.power.profile/         # `profile` commands (deprecated alias for `connection`)
│       └── dgt.power.push/            # `push` command
├── tests/                        # Unit and integration tests
├── samples/                      # Example inputs (configs, plugin samples)
├── schemas/                      # JSON schemas for configuration files
├── Directory.Build.props         # Common MSBuild properties
├── global.json                   # Pinned .NET SDK
└── DigitallPower.sln             # Solution file
```

## 🛠️ Build & Test

```bash
pnpm install                 # Install JS tooling dependencies (includes TypeScript compiler for TSL gates)
dotnet restore                # Restore dependencies (uses lock files)
dotnet build                  # Build the solution
dotnet test                   # Run all tests
dotnet test --filter "Name~Foo"   # Run a subset of tests
```

To produce a local NuGet package of the tool:

```bash
dotnet pack src/dgt.power/dgt.power.csproj -c Release
```

The resulting `.nupkg` is placed in the `packages/` folder and can be installed locally with:

```bash
dotnet tool install --global --add-source ./packages dgt.power --version <version>
```

## ✅ Requirements

- **.NET SDK 10.0** (the SDK version is pinned via [`global.json`](global.json))
- **Node.js 22 + pnpm** (needed for the TypeScript compile gate in `dgt.power.codegeneration.tests`)
- Network access to your Dataverse environment (`*.dynamics.com`) and to `api.nuget.org` (for the version check)
- An account with sufficient privileges on the target Dataverse environment

## 📡 Telemetry

DigitallPower collects anonymous usage telemetry to help improve the tool. Telemetry is **opt-out** — it is enabled by default but can be easily disabled.

### What is collected

| Data | Example | Purpose |
|------|---------|---------|
| Command name | `UserRoleImport` | Understand which modules are used |
| Success/failure | `true` | Track reliability |
| CI environment | `true` | Distinguish interactive vs automated usage |
| OS platform | `Unix` | Platform distribution |
| Tool version | `2.1.0` | Version adoption |
| Anonymous install ID | `a1b2c3d4-...` | Count unique installations |

**No personally identifiable information is collected.** No usernames, organization URLs, file contents, or environment-specific data is ever transmitted.

CI detection is centralized in `dgt.power.common.ExecutionEnvironment` and currently recognizes `TF_BUILD`, `BUILD_BUILDURI`, `GITHUB_ACTIONS`, `GITLAB_CI`, `JENKINS_URL`, and `CI`.

### How to disable telemetry

**Per invocation:**

```bash
dgtp export --no-telemetry
```

**Permanently (environment variable):**

```bash
export DGT_TELEMETRY_OPTOUT=1
```

Set `DGT_TELEMETRY_OPTOUT` to `1`, `true`, or `yes` to permanently disable telemetry.

**Override telemetry endpoint (advanced):**

```bash
export DGT_TELEMETRY_CONNECTION_STRING="InstrumentationKey=..."
```

Set `DGT_TELEMETRY_CONNECTION_STRING` to an Azure Monitor connection string to route telemetry to a custom endpoint. When not set, the build-time embedded connection string is used (or telemetry is disabled if none was embedded).

### Example query

To count how often each module was invoked, split by CI and non-CI usage:

```kusto
dependencies
| where timestamp > ago(30d)
| extend
    module = tostring(customDimensions["dgtp.command"]),
    is_ci = tolower(tostring(customDimensions["dgtp.is_ci"])) == "true"
| where isnotempty(module)
| summarize CI = countif(is_ci), NonCI = countif(not(is_ci)) by module
| order by CI + NonCI desc
```

### First-run notice

On first use, the CLI displays a one-time notice informing you about telemetry collection and how to opt out. This notice is shown only once per installation.

## ❤️ Community and Contributions

DigitallPower CLI is a **community-driven open source project** backed by DIGITALL. We are committed to a fully transparent development process and **highly appreciate any contributions**. Whether you are helping us fixing bugs, proposing new features, improving our documentation or spreading the word — **we would love to have you as part of the DigitallPower community**.

### 📫 Have a question? Want to chat? Ran into a problem?

We are happy to answer your questions via [GitHub Discussions](https://github.com/DIGITALLNature/DigitallPower/discussions).

### 🤝 Found a bug? Missing a specific feature?

Feel free to **file a new issue** with a descriptive title on the [DigitallPower](https://github.com/DIGITALLNature/DigitallPower/issues) repository. If you already found a solution to your problem, **we would love to review your pull request**. Have a look at our [contribution guidelines](https://github.com/DIGITALLNature/DigitallPower/blob/main/contributing.md) to find out about our coding standards and the conventional-commits workflow used in this repository.

## 📘 License

DigitallPower CLI is released under the terms of the [MS-RL License](Licence.md).
