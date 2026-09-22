---
type: guide
title: DigitallPower code wiki quickstart
description: Route a dgtp change from the CLI entrypoint to its owning runtime boundary, workflow contract, and focused validation suite. Use source and focused tests as the authority before changing behavior.
tags: [overview, navigation, dgtp]
verified:
  - by: openwiki/0.5.2
    at: 2026-09-22T15:05:10.374Z
sources:
  - id: openwiki-source-3acf8b91e56390d5b0764c18
    resource: repo://DigitallPower.slnx
  - id: openwiki-source-5b54a58d1b51cd490b0e7162
    resource: repo://package.json
  - id: openwiki-source-8049106abeb1cc799cd2fb17
    resource: repo://src/dgt.power.common/BaseProgramSettings.cs
  - id: openwiki-source-6369c743fefdceaa63d7d586
    resource: repo://src/dgt.power.common/PowerLogic.cs
  - id: openwiki-source-d14c41ae7919e20b31110ef7
    resource: repo://src/dgt.power/CommandTree.cs
  - id: openwiki-source-b468d047455e930433612cb4
    resource: repo://src/dgt.power/Program.cs
  - id: openwiki-source-a99ee8fb3478a749e3224ea3
    resource: repo://src/modules/dgt.power.connection/Commands/ConnectionStatusCommand.cs
  - id: openwiki-source-e8dd2dede96d5964568b0146
    resource: repo://src/modules/dgt.power.import/Logic/QueueImport.cs
  - id: openwiki-source-87e0fe28c22b3697725b53f7
    resource: repo://tests/dgt.power.cli.tests/CommandTreeTests.cs
  - id: openwiki-source-15fd863320d369533304ec8c
    resource: repo://tests/dgt.power.cli.tests/SettingsParsingTests.cs
generated: { by: "openwiki/0.5.2", at: "2026-09-22T15:05:10.374Z" }
---
# DigitallPower code wiki quickstart

Use this page to scope a safe code change, not as a replacement for the implementation. `dgtp` is the .NET global-tool command produced by `src/dgt.power`; `Program.cs` composes the normal runtime and `CommandTree.cs` exposes the public command surface. Read the owning source and its focused tests before editing. The linked pages below explain the contracts and invariants that tests alone may not make obvious.

## Start at the runtime boundary

1. **Classify the change.** Is it host wiring or a setting, connection/authentication, a persisted DTO/configuration, a Dataverse operation, generated output, or deployment behavior?
2. **Trace from the command tree.** `CommandTree.Register` maps the public branch or command to its implementation type. `Program.cs` supplies configuration, DI, interceptors, error handling, and the shared Dataverse-service boundary.
3. **Find the state or contract owner.** Do not infer shared model behavior from one command: early-bound entities and persisted transfer DTOs have different ownership and compatibility obligations.
4. **Choose narrow validation first.** Command reachability/settings parsing and domain behavior are separate test seams. Broaden to the CI-equivalent workflow when changing shared contracts, host composition, dependencies, packaging, or generated output.

The normal path builds configuration from defaults, optional working-directory `dgtp.json`, `dgtp:` environment variables, and command-line arguments in that precedence order; it then configures the command app and executes the selected command. Completion is deliberately different: a suggest directive exits before normal configuration, telemetry, or Dataverse wiring and uses the same command-tree registration to derive candidates. See [runtime and command surface](architecture/overview.md) and [completion and observability](cli/completion-and-observability.md) before changing either path.

## Task-routing map

| Change intent | Owns behavior and read first | Source trail to inspect | Focused validation |
|---|---|---|---|
| Add, remove, rename, or rewire a public command; change options, aliases, help, or examples | [CLI runtime and command surface](architecture/overview.md) | `src/dgt.power/CommandTree.cs` → owning command/settings in its module; `src/dgt.power/Program.cs` for host registration | `tests/dgt.power.cli.tests`; also the owning module suite. Update `CommandTreeTests` for top-level paths and `SettingsParsingTests` for changed settings metadata. |
| Change completion, telemetry, interception, startup, DI, configuration layering, or process-level failure/shutdown behavior | [CLI runtime and command surface](architecture/overview.md), then [completion and observability](cli/completion-and-observability.md) | `src/dgt.power/Program.cs`, `src/dgt.power/Completion/`, `src/dgt.power/Telemetry/` | `tests/dgt.power.cli.tests` and the relevant telemetry tests; use the broader build flow for host-wide changes. |
| Change connection resolution, stored identity persistence, MSAL login, non-interactive behavior, connection strings, or Azure DevOps federation | [Dataverse connection and identity management](architecture/dataverse-access.md) | `src/dgt.power.common/` connection/profile abstractions and `src/modules/dgt.power.connection/` | `tests/dgt.power.connection.tests`; also `tests/dgt.power.profile.tests` when preserving the deprecated `profile` branch. |
| Add or evolve a JSON transfer artifact, mapper, export, import, or reconciliation rule | [Dataverse configuration export and import](workflows/configuration-transfer.md), then [Dataverse models and transfer contracts](architecture/dataverse-contracts.md) | `src/modules/dgt.power.export/`, `src/modules/dgt.power.import/`, and shared `DTO/` contracts | `tests/dgt.power.export.tests` and `tests/dgt.power.import.tests`. Test serialization, invalid/empty input, mutation, and partial-failure behavior. |
| Change a shared early-bound Dataverse entity, a partial entity extension, or a persisted DTO | [Dataverse models and transfer contracts](architecture/dataverse-contracts.md) | `src/dgt.power.common/DotNet/`, `src/dgt.power.common/DTO/`, and all workflow consumers | Transfer suites for DTOs plus each affected consumer suite. Preserve persisted JSON names and compatibility deliberately. |
| Change a solution/layer report or its paging/report-output rules | [Dataverse solution analysis](workflows/solution-analysis.md) | `src/modules/dgt.power.analyzer/` | `tests/dgt.power.analyzer.tests` |
| Change a live maintenance command, durable configuration input, retry/poll loop, or safety check | [Dataverse maintenance operations](workflows/maintenance.md) | `src/modules/dgt.power.maintenance/` | `tests/dgt.power.maintenance.tests` |
| Change C# or TypeScript generation, metadata retrieval, templates, versioned configuration, or schemas | [Dataverse C# and TypeScript code generation](workflows/code-generation.md) | `src/modules/dgt.power.codegeneration/`, `schemas/codegeneration/`, generated fixture consumers | `tests/dgt.power.codegeneration.tests`; also `pnpm test:tsl-jest` when TypeScript output or its consumers change. Keep schemas synchronized with configuration models. |
| Change plugin/package or web-resource deployment, migration, rollback, or destructive options | [Plugin and web-resource push deployment](workflows/push.md) | `src/modules/dgt.power.push/` and its registration/deployment processors | `tests/dgt.power.push.tests` |
| Change restore/build/test/package/release behavior or test infrastructure | [Build, test, package, and release workflow](reference/build-and-test.md) | `global.json`, `Directory.Build.props`, solution/project files, `.github/workflows/`, and the affected test project | Run the documented CI-equivalent sequence or the narrowest subset justified by the changed boundary. |

The command tree currently exposes `connection`, deprecated `profile`, `export`, `import`, `analyze`, `maintenance`, `codegeneration` (alias `cg`), `push`, and `complete`. Treat it as the public-surface authority; module directories explain implementation ownership but do not by themselves prove that a command is reachable.

## Operational guardrails

Do not turn an implementation investigation into an unintended live-environment operation. Before a Dataverse command that needs an MSAL connection, use:

```bash
dgtp connection status
```

`connection status` is a non-browser preflight: exit code 0 means no interactive login is required, and exit code 2 means user interaction is required. Use `dgtp connection refresh` only when an interactive browser login is appropriate. For unattended command execution, pass `--non-interactive` so a required MSAL login fails rather than opening a browser. Connection strings and Azure DevOps federated identities have distinct status/refresh semantics; follow the [Dataverse access](architecture/dataverse-access.md) page rather than applying the MSAL rule to every identity type.

Export/import, maintenance, and push are not generic transactional operations. Their individual pages define matching rules, destructive actions, ordering, retries/polling, and what can remain changed after a failure. Inspect those rules and their focused tests before modifying or running a mutating path.

## Validation ladder

Use the smallest check that proves the changed boundary, then expand only as risk requires:

```bash
# Command tree, settings, completion, and host-facing behavior
dotnet test --project tests/dgt.power.cli.tests/dgt.power.cli.tests.csproj

# Owning module pattern
dotnet test --project tests/dgt.power.<module>.tests/dgt.power.<module>.tests.csproj
```

For CI-equivalent validation—including locked restore, Release build, TUnit suites, and the TypeScript consumer gate—follow [Build, test, package, and release](reference/build-and-test.md). Source and focused tests remain the authority for behavior; update this routing page only when the ownership map or safe-change path changes.
