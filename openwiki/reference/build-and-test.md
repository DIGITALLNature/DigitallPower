---
type: reference
title: Build, test, package, and release workflow
description: Repository build prerequisites, shared test architecture, focused validation commands, and NuGet release automation.
tags: [build, testing, release]
---
# Build, test, package, and release workflow

The solution is `DigitallPower.slnx`: executable `src/dgt.power`, shared `src/dgt.power.common`, eight module projects, and module/CLI/shared test projects. `global.json` selects .NET SDK 10 with latest-major roll-forward; the executable targets `net10.0`, packs as a global tool, and exposes `dgtp`. `Directory.Build.props` owns package metadata and current version. Package release is configured in `package.json` through semantic-release for `main` and prerelease `beta`, updating `Directory.Build.props`, changelog, NuGet package, and GitHub release.

## Standard validation

```bash
dotnet restore --locked-mode
dotnet build --no-restore --configuration Release
dotnet test --no-build --configuration Release -- --output Detailed
pnpm install --frozen-lockfile
pnpm test:tsl-jest
```

The GitHub build workflow uses this order (Node 22 and .NET 10): locked restore, frozen pnpm install, release build, all .NET tests, then a separate install/test step in `tests/dgt.power.codegeneration.tests/Fixtures/TslJest`. The Jest stage validates generated TypeScript consumer fixtures; it is required when templates, TypeScript generators, or generated helpers change.

## Shared test architecture

`tests/dgt.power.tests/CommandTestContextBuilder<TCommand,TCommandSettings>` is the dominant module-test seam. It creates `FakeOrganizationServiceAsync`, registers a base `RetrieveCurrentOrganizationRequest` fake, overlays project/request fakes with later custom fakes winning, then registers defaults. Builders can inject metadata, relationships, data, custom service setup, console, DI services, and `IConfiguration`; absent metadata for supplied data is auto-created. The harness supplies a short fake `pollrate`, so polling tests do not wait production intervals.

This explains test style: module suites test command classes against a controllable Dataverse simulation rather than a live environment. For example, queue-import tests inject `AssignRequest` failure and assert that a queue can exist while ownership assignment reports failure. Code-generation determinism tests compare generated artifacts across serial and parallel runs. Add test behavior through these seams rather than adding real credentials or network calls.

## Focused ownership map

| Change area | Focused project |
|---|---|
| CLI tree, completion, telemetry | `tests/dgt.power.cli.tests` |
| connection/profile behavior | `tests/dgt.power.connection.tests`, `tests/dgt.power.profile.tests` |
| analyzer | `tests/dgt.power.analyzer.tests` |
| export/import artifacts | `tests/dgt.power.export.tests`, `tests/dgt.power.import.tests` |
| maintenance | `tests/dgt.power.maintenance.tests` |
| generation | `tests/dgt.power.codegeneration.tests` plus `pnpm test:tsl-jest` when applicable |
| push | `tests/dgt.power.push.tests` |

Run a focused suite with `dotnet test --project <project path>`. Broaden to the full build when shared common contracts, registration, generators, or packaging change.
