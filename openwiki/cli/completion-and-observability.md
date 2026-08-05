---
type: subsystem
title: Completion, telemetry, and CLI interceptors
description: The side-effect-free completion protocol, idempotent shell shim installation, and cross-cutting CLI observability.
tags: [cli, completion, telemetry]
---
# Completion, telemetry, and CLI interceptors

Completion is an explicit early execution mode. `DotnetSuggestHandler.IsSuggestMode` recognizes `[suggest:<position>]`; `Program.cs` exits through `HandleAsync` before configuration, telemetry, NuGet version checks, or Dataverse DI. The handler builds a silent Spectre command model using the production `RegisterCommands` delegate, calls `CompletionEngine`, and writes candidates one per stdout line. This isolation is an invariant: completion must not connect or emit non-candidate output.

`complete setup` registers with dotnet-suggest; `complete install-shell` uses `ShellDetector` and `ShellShimInstaller`. The installer finds `dotnet-suggest`, requests `dotnet-suggest script <shell>`, and appends a marker-delimited block to the selected RC file. It returns `AlreadyInstalled` if `MarkerStart` already exists, so repeated setup is idempotent. `--dry-run` belongs to the command wrapper and must not change files.

Normal invocations configure a `CompositeInterceptor` in this order: `TelemetryInterceptor`, `VersionCheckInterceptor`, `DeprecationInterceptor`. Telemetry is opt-out through `TelemetryConfig`; first-run notice and installation identifier use isolated storage. If enabled and a connection string is available, `Program.cs` creates an OpenTelemetry Azure Monitor trace provider. Its unhandled-exception and unobserved-task handlers call `Tracer.TrackFatalException` (the task handler also marks the task observed), and the configured command exception handler records handled failures before exit mapping. The provider is force-flushed/disposed in `finally`.

## Change discipline

For command names/options, update production registration first because completion captures that model. Exercise `CompletionEngineTests`, `SuggestDirectiveTests`, setup/installer tests, and `CommandTreeTests` where its test-only tree is intentionally maintained. Do not write credentials, telemetry connection values, or real shell profile contents into tests or documentation.

Focused validation: `dotnet test --project tests/dgt.power.cli.tests/dgt.power.cli.tests.csproj`.
