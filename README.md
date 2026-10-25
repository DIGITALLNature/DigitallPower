# DigitallPower CLI

[![License](https://img.shields.io/github/license/DIGITALLNature/DigitallPower.svg)](LICENSE.md)
[![GitHub tag (latest SemVer)](https://img.shields.io/github/tag/DIGITALLNature/DigitallPower.svg)](https://github.com/DIGITALLNature/DigitallPower/releases)
[![NuGet](https://img.shields.io/nuget/v/dgt.power)](https://www.nuget.org/packages/dgt.power)

> **Upgrading between major versions?** Review the
> [migration guides](docs/guides/migrations/) for user-affecting breaking changes before upgrading.

## Introduction

**DIGITALLPOWER** — the .NET tool for the Microsoft Power Platform from DIGITALL. A swiss army knife for all ALM tasks where the Power Platform CLI (`pac`) still has weaknesses.

DigitallPower (`dgtp`) is a cross-platform global .NET tool that helps developers, makers and operators to **develop, deploy and maintain** Microsoft Dataverse / Power Platform solutions. It complements the official tooling and focuses on the day-to-day pain points DIGITALL encounters in real-world enterprise projects — with the goal of universally helping the wider community.

## ✨ Features at a Glance

| Area | What it does |
|------|--------------|
| **Connections** | Manage typed Dataverse connections (interactive, device-code, client-secret, certificate, Azure DevOps federation) and check authentication status |
| **Export** | Extract configuration data (team templates, queues, SLAs, calendars, routing rules, document/Outlook templates, user roles, bulk delete jobs) from an environment |
| **Import** | Import the previously exported artifacts into another environment — ideal for ALM pipelines |
| **Analyze** | Inspect solutions for redundant components, active-layer issues, top-layer problems and obsolete patches |
| **Solution** | Run configuration-driven Dataverse quality gates (`solution lint`) such as unmanaged field naming and table completeness checks against a single solution, increment solution versions (`solution version`), and copy solution components between solutions (`solution copy-components`) |
| **Maintenance** | Bulk-delete records, manage auto-number formats, protect calculated fields, increment solution versions, update workflow states, filter PowerFx plugin steps, and more |
| **Code Generation** | Generate strongly-typed C# (early-bound), TypeScript and metadata files for Dataverse entities |
| **Plugin** | Deploy plugin assemblies and packages with `plugin push`; manage plugin steps (configuration, enable/disable, bulk state) |
| **Webresources** | Push webresources from a directory or a single file, with publisher-prefix naming and solution membership |

## 🚀 Installation

DigitallPower is published as a [.NET global tool](https://www.nuget.org/packages/dgt.power):

```bash
dotnet tool install -g dgt.power
```

After installation the command `dgtp` is available globally. See [Installation](docs/getting-started/installation.md) for update/uninstall instructions and requirements.

## ⚡ Quick Start

```bash
# 1. Create and select a connection
dgtp connection create dev --url https://contoso-dev.crm4.dynamics.com --tenant contoso.onmicrosoft.com
dgtp connection select dev

# 2. Verify the connection by listing connections
dgtp connection list

# 3. Export configuration data from the environment
dgtp export queues --filedir ./out/queues

# 4. Generate early-bound C# classes
dgtp codegeneration ./generated -c ./genconfig.json
```

Run `dgtp --help` or `dgtp <command> --help` to discover all options. See [Quick Start](docs/getting-started/quick-start.md) for next steps.

## 📚 Documentation

| Section | Description |
|---------|-------------|
| [Getting Started](docs/getting-started/) | Install dgtp and verify your setup |
| [Guides](docs/guides/) | CI/CD integration, tab completion, plugin deployment, migration guides |
| [Command Reference](docs/reference/commands/) | All commands organized by branch |
| [Configuration](docs/reference/configuration.md) | Environment variables, connection settings, JSON schemas |
| [Concepts](docs/concepts/) | Architecture, telemetry & privacy, connection model |
| [Contributor Docs](docs/development/) | Build & test, repository layout, spec-driven development |
| [Full docs index](docs/README.md) | Complete documentation navigation |

> **DIGITALL guidelines:** The [DIGITALL BizApps Guidelines](https://digitallnature.github.io/) site provides project-level guidance for working with dgtp in DIGITALL projects, including ALM workflows and coding standards.

## 📡 Telemetry

DigitallPower collects anonymous usage telemetry to help improve the tool. Telemetry is **opt-out** — it is enabled by default but can be easily disabled. Personally identifiable information is not intentionally collected; error data is anonymized on a best-effort basis (see limitations below).

```bash
dgtp telemetry disable   # Opt out permanently
dgtp telemetry status    # Check current state
```

See [Telemetry and Privacy](docs/concepts/telemetry-and-privacy.md) for what data is collected, how it's anonymized, and all opt-out options.

## 🤝 Community and Contributions

DigitallPower CLI is a **community-driven open source project** backed by DIGITALL. We highly appreciate any contributions — bug fixes, new features, documentation improvements, or spreading the word.

- **Questions?** [GitHub Discussions](https://github.com/DIGITALLNature/DigitallPower/discussions)
- **Found a bug?** [File an issue](https://github.com/DIGITALLNature/DigitallPower/issues)
- **Want to contribute?** See [CONTRIBUTING.md](CONTRIBUTING.md)

## 📘 License

DigitallPower CLI is released under the terms of the [MS-PL License](LICENSE.md).
