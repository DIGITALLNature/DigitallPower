---
type: guide
title: DigitallPower code wiki quickstart
description: Navigate the dgtp CLI architecture, Dataverse workflows, contracts, and focused validation guidance.
tags: [overview, navigation, dgtp]
---
# DigitallPower code wiki quickstart

DigitallPower is a .NET 10 global CLI (`dgtp`) for Dataverse/Power Platform development, deployment, configuration transfer, analysis, and maintenance. Start with [Runtime and command surface](architecture/overview.md) to understand how public CLI commands are composed, then follow the workflow that owns the requested behavior.

## Wiki map

* **Runtime and shared boundaries:** [runtime and command surface](architecture/overview.md), [Dataverse connection and identity management](architecture/dataverse-access.md), and [Dataverse models and transfer contracts](architecture/dataverse-contracts.md).
* **CLI support:** [completion, telemetry, and interceptors](cli/completion-and-observability.md).
* **Dataverse operations:** [configuration transfer](workflows/configuration-transfer.md), [solution analysis](workflows/solution-analysis.md), and [maintenance operations](workflows/maintenance.md).
* **Generation and deployment:** [C# and TypeScript code generation](workflows/code-generation.md) and [plugin/web-resource push](workflows/push.md).
* **Engineering workflow:** [build, test, package, and release](reference/build-and-test.md).

## Task routing

| Intent | Read first | Primary sources/symbols | Focused validation |
|---|---|---|---|
| Add/change a CLI command | [Runtime and command surface](architecture/overview.md) | `src/dgt.power/Program.cs`, `PowerLogic<TConfig>` | `tests/dgt.power.cli.tests` plus module suite |
| Change authentication or connection selection | [Dataverse connection](architecture/dataverse-access.md) | `XrmConnection`, `ProfileManager`, connection commands | `tests/dgt.power.connection.tests` |
| Add an export/import artifact or evolve its JSON | [Configuration transfer](workflows/configuration-transfer.md) | `BaseExport`, `BaseImport`, `DTO/*` | export/import test projects |
| Change a solution report | [Solution analysis](workflows/solution-analysis.md) | `BaseAnalyze`, `*Analyze` | analyzer tests |
| Change a live maintenance operation | [Maintenance operations](workflows/maintenance.md) | registered maintenance command and schema | maintenance tests |
| Change generated C# or TypeScript | [Code generation](workflows/code-generation.md) | config factory, metadata service, generator/templates | codegeneration tests; Jest when TypeScript output changes |
| Change deployment behavior | [Push deployment](workflows/push.md) | `PushCommand`, `AssemblyProcessor`, `WebresourcesProcessor` | push tests |

## Safe operational entry

Before any live Dataverse command, run `dgtp connection status`. Exit 0 means the current connection can proceed; exit 2 means user login is required. Use `dgtp connection refresh` only when interactive authentication is appropriate. For agent runs, use `--non-interactive` to prevent browser fallback. Maintenance and push commands can mutate or delete environment state; inspect their pages for command-specific destructive options.

## Navigation principles

Each workflow page names its command registrations, owning implementation symbols, contracts/configuration, tests, and narrow validation. The canonical shared data home is [Dataverse models and transfer contracts](architecture/dataverse-contracts.md); do not duplicate DTO or early-bound model guidance across feature pages. The repository has no source-evidence-blocked areas in this wiki’s scope, so there is no backlog.
