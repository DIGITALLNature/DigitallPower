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
- [Configuration](#%EF%B8%8F-configuration)
- [Command Reference](#-command-reference)
  - [profile](#profile--authentication--environments)
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
| **Profiles** | Manage multiple Dataverse environment connections (interactive, MSAL, client-secret) |
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
# 1. Create and select a connection profile
dgtp profile create dev https://contoso-dev.crm4.dynamics.com --msal
dgtp profile select dev

# 2. Verify the connection by listing profiles
dgtp profile list

# 3. Export configuration data from the environment
dgtp export queues --filedir ./out/queues

# 4. Generate early-bound C# classes
dgtp codegeneration ./generated -c ./genconfig.json
```

Run `dgtp --help` or `dgtp <command> --help` to discover all options.

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

Profile data (credentials, selected profile) is stored in the user's [Isolated Storage](https://learn.microsoft.com/dotnet/standard/io/isolated-storage), not in the repository.

JSON schemas for the various configuration files used by the modules live under [`schemas/`](schemas) and can be referenced from your own config files via the `$schema` property for autocomplete in modern editors.

## 📚 Command Reference

The CLI is organized into branches. The general invocation pattern is:

```
dgtp <branch> <command> [arguments] [options]
```

### `profile` — Authentication & environments

| Command | Description |
|---------|-------------|
| `profile list` | List configured profiles |
| `profile create <name> <url> [--msal\|--clientsecret …]` | Create a new connection profile |
| `profile select <name>` | Set the active profile |
| `profile delete <name>` | Delete a profile |
| `profile purge` | Remove all profiles |

Example:

```bash
dgtp profile create prod https://contoso.crm4.dynamics.com --msal
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

The JSON schema for the generation configuration is available at [`schemas/codegeneration`](schemas/codegeneration).

### `push` — Deploy artifacts

Pushes a plugin assembly or web resource into a target solution.

```bash
dgtp push ./bin/Release/MyPlugin.dll --solution mysolution
```

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
│  profile   │ │  export  │ │  import  │ │  analyze  │  │ maintenance  │
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
                  │   profile management,    │
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

- **Module isolation.** Every feature area (`analyzer`, `codegeneration`, `export`, `import`, `maintenance`, `profile`, `push`) is an independent project under `src/modules/`. Modules expose `Spectre.Console.Cli`-style command classes that are registered by the host.
- **Shared kernel.** `dgt.power.common` provides the cross-cutting infrastructure: the `IXrmConnection`, profile management, file I/O helpers, base commands, tracing and exception types.
- **DI everywhere.** Long-lived services (HTTP/NuGet clients, profile manager, caches, JSON options) are singletons; per-command services (metadata, config resolver, generators, file service) are scoped; the `IOrganizationService` is lazily resolved from the active profile via `IXrmConnection.Connect()`.
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
│       ├── dgt.power.export/          # `export` commands
│       ├── dgt.power.import/          # `import` commands
│       ├── dgt.power.maintenance/     # `maintenance` commands
│       ├── dgt.power.profile/         # `profile` commands
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
