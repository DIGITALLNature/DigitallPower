# Design

## Context

See proposal.md for motivation. Current state relevant to the approach:

- Commands derive from `PowerLogic<T>` (`AsyncCommand<T>` plus `Tracer`, `Console`, `ConfigResolver`, `Connection`) or, in `connection/*`, `Complete*` and `CodeGeneration`, from Spectre base types directly. Both styles are plain Spectre commands, so they coexist.
- `Program.cs` registers `IDataverseConnection` as a singleton and an eager `IOrganizationService` that blocks on `ConnectAsync()` during command resolution. Spectre binds settings and runs interceptors before resolving the command, so connection options are already known at that point.
- `Tracer` owns telemetry gating (`--no-telemetry`, install id), banner rules and a single `_currentActivity` field set by `Start` and cleared by `End`. `TrackFatalException` runs from the global exception handler, which also maps `InteractiveLoginRequiredException` to exit code `2`.
- Plugin and web-resource repositories already take `IOrganizationServiceAsync2` in their constructors and are created by the command after connecting; solution helpers (`LintContext`, resolvers) follow the same pattern.
- Test doubles `TestConnection(service)` and `FakeDataverseConnection` already implement `IDataverseConnection`.

## Goals / Non-Goals

**Goals:**
- Reliable, scoped telemetry and diagnostics for every command run.
- Dataverse access on demand, with a lifecycle that is visible in code and enforced by the compiler where possible.
- Commands declare only the services they use; no inheritance for service access.
- A path to full constructor injection of services and repositories.

**Non-Goals:**
- Rewriting the business logic of import, export, analyze or maintenance (including replacing `DataContext` LINQ with async queries). Existing sync code receives the async service, which extends `IOrganizationService`.
- Changing command names, options, configuration formats or exit codes.
- Converting `MetadataService`/code generation to async, or supporting several Dataverse connections in one run.

## Decisions

### D1. Compose services; keep one thin lifecycle base class

Commands are plain `AsyncCommand<T>` classes with primary-constructor dependencies. `PowerLogic`, `BaseMaintenance`, `BaseImport`, `BaseExport`, `BaseAnalyze` and `IPowerLogic` are removed. Helpers formerly in the bases become injected services (for example an export writer for JSON/file output, a solution-component reader), created when their first command is migrated.

One abstract class `TracedCommand<TSettings>` remains, because it enforces an invariant that composition cannot: every run starts and ends exactly once.

```csharp
public sealed override async Task<int> ExecuteAsync(CommandContext ctx, TSettings settings, CancellationToken ct)
{
    using var run = runs.Begin(GetType().Name);
    try
    {
        var outcome = await RunAsync(settings, ct);
        run.Complete(outcome);
        return outcome.ToExitCode();   // Succeeded/Skipped -> 0, Failed/NotConfigured -> 1
    }
    catch (Exception ex)
    {
        run.Fail(ex);
        throw;
    }
}
```

It holds no services other than the run factory. `RunOutcome` is a small enum or record (`Succeeded`, `Failed`, `NotConfigured`, `Skipped`).

Alternatives considered:
- *Spectre interceptor*: works for every command, but `InterceptResult` only sees an integer exit code (no `NotConfigured`/`Skipped`) and is not invoked when the command throws. Rejected as the primary mechanism.
- *Explicit calls in each command* (status quo): keeps the leak and forgotten-`End` risk.
- *Two-tier `PowerLogic` / `DataverseLogic`*: no consumer today; commands without Dataverse already skip `PowerLogic`.

Commands that need no span (`connection/*`, `Complete*`) stay plain `AsyncCommand<T>`. They may adopt `TracedCommand` later.

### D2. Split `ITracer`

- **Run/span** (`ICommandRunFactory` or similar): the only code that starts an `Activity`, owns telemetry gating (`--no-telemetry`, install id) and sets `dgtp.command`, `dgtp.success` and a new `dgtp.outcome` tag. Span name stays `command.<TypeName>`.
- **Diagnostics** (`IDiagnostics`: `Log`, `Exception`): records onto `Activity.Current` instead of a field, so it follows async context. `ConfigResolver` and other services depend on it.
- `TrackFatalException` stays for exceptions that escape before or outside a run. The lifecycle base marks an exception as already recorded (for example via `Exception.Data`) so the global handler does not create a second span for it.

The console banner rules (`X - Start`, `X - End:OK`) are presentation, not telemetry, and are removed from the telemetry path. Failure and not-configured messages that commands print themselves are unchanged.

Alternative considered: keep `ITracer` as is and only drop the marker. Rejected because the hidden `_currentActivity` state is the actual defect.

### D3. `IDataverseConnection` as the single, cached entry point

- Rename `ConnectAsync()` to `GetServiceAsync()`; it returns `Task<IOrganizationServiceAsync2>`. The name states that it returns the run's service, and documents that the first call connects and later calls reuse it.
- The singleton caches one `Lazy<Task<...>>`-style value so concurrent first calls share one connect and one verification request.
- A failed connect is not cached, so a transient failure may be retried within the run; authentication-required failures are terminal and propagate as today.
- `CheckAuthAsync()` is unchanged.
- The eager `IOrganizationService` registration is deleted. Nothing in DI exposes `IOrganizationService` or `IOrganizationServiceAsync2`; tests register `IDataverseConnection` (`TestConnection(service)`) only.
- Connection options continue to flow through `ConnectionInvocationContext`, filled by the interceptor before command resolution.

### D4. Who receives what

| Consumer | Receives |
|---|---|
| Commands | `IDataverseConnection` only if they use Dataverse; call `GetServiceAsync()` inside `RunAsync`. |
| DI-registered services and repositories | `IDataverseConnection`; resolve the service inside methods. |
| Short-lived helpers created after connecting (`LintContext`, resolvers, existing plugin and web-resource repositories) | `IOrganizationServiceAsync2` directly; they are not registered in DI. |
| Sync code paths (`DataContext`, `TryXxx` extensions) | The resolved service, passed as parameter; `IOrganizationServiceAsync2` extends `IOrganizationService`. |

Existing plugin and web-resource repositories keep their constructor shape; they migrate to `IDataverseConnection` only when they are registered in DI.

### D5. I/O-free construction is a rule, not a convention

Constructors cannot be `async`, so `GetServiceAsync()` cannot be awaited there. The remaining way to violate the rule is blocking (`.GetAwaiter().GetResult()`, `.Result`, `.Wait()`). A rule in `AGENTS.md` forbids blocking on tasks and Dataverse/auth I/O in constructors and field initializers, and the existing analyzer setup is checked for a rule that can flag it. This replaces the eager registration, which was the only violation.

### D6. Exit codes

Commands return `0` or `1` (or throw). The global exception handler keeps mapping `InteractiveLoginRequiredException` to `2` (`ExitCode.AuthRequired`), which preserves the documented non-interactive contract for agents and CI. `NotConfigured` and `Skipped` are telemetry attributes only.

### D7. Migration without a flag day

`PowerLogic` subclasses and composed commands are both Spectre commands, so they coexist. Order:

1. Foundation: `IDataverseConnection` rename/caching, run/diagnostics types, `TracedCommand`, test doubles. `Program.cs` keeps the eager registration only until the last `PowerLogic` subclass moves.
2. Modules already close to the target: solution, plugin, webresource.
3. Maintenance, analyze, export, import (introducing injected helpers replacing `Base*`).
4. Delete `PowerLogic`, `IPowerLogic`, the eager registration and obsolete tests; update docs.

Until step 4, legacy `PowerLogic` subclasses keep using the eager registration unchanged; no temporary dual-path adapter is introduced.

## Risks / Trade-offs

- [Behavior drift in tracing: spans now close on exceptions or after early returns that previously leaked] → Port `TracerTests` to the new types and add tests for success, failure, exception, not-configured and skipped paths; compare tag sets with today's.
- [Duplicate span for escaping exceptions (run records it, global handler tracks it again)] → Mark recorded exceptions and skip them in `TrackFatalException`; cover with a test.
- [`Activity.Current` is null when a diagnostics call happens outside a run] → Diagnostics write to the console regardless and only attach to a span when one exists.
- [Caching a connect task under concurrency] → Share one in-flight task; do not cache failures; unit-test concurrent first calls and retry after failure.
- [Large mechanical diff (~46 classes) hides regressions] → Migrate per module, keep each commit buildable and green, run each module's test project before moving on.
- [Console banner removal changes visible output] → Limited to the start/end rules; error and status messages are untouched. Called out in the release notes if kept as removed.
- [Constructor I/O rule relies on review] → Documented in `AGENTS.md`; no eager `IOrganizationService` remains to tempt it.

## Migration Plan

Implement on `refactor/command-composition` in per-module commits that each build and pass tests; no user-facing migration guide entry is needed because command surface and exit codes are unchanged (internal-only break). Rollback is reverting the branch; there are no data or configuration migrations.

## Open Questions

- Whether to keep a single-line start/end status message from the lifecycle base or drop console output entirely (default: drop; telemetry carries the lifecycle).
- Whether `CodeGenerationCommand` (sync `Command<T>`, calls `Tracer` and implements `IPowerLogic`) becomes async on `TracedCommand` or keeps a sync path that opens a run explicitly; either way it must stop using `IPowerLogic` before step 4.
