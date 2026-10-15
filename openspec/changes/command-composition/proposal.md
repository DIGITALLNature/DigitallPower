# Proposal

## Why

Commands inherit from `PowerLogic<T>`, a base class that is mostly a bag of injected services (`Tracer`, `Console`, `ConfigResolver`, `Connection`) plus an `InvokeAsync` indirection. Its real responsibilities are not enforced:

- `Program.cs` registers an eager `IOrganizationService` singleton that calls `ConnectAsync().GetAwaiter().GetResult()` during command construction. Every command that inherits `PowerLogic` authenticates, even if it never touches Dataverse, and the async `IOrganizationServiceAsync2` is downcast again in 12 files.
- The telemetry span lifecycle lives in `Tracer` (`Start`/`End` plus a hidden `_currentActivity` field) and is repeated manually in ~45 commands. A forgotten `End` or an early return leaks the span, and `Tracer.Exception` only records onto a span if one happens to be open.
- `IPowerLogic` is an empty marker that exists only to constrain `ITracer.Start<T>`; `ITracer` mixes console banners, telemetry spans and diagnostics.
- `BaseMaintenance`, `BaseImport`, `BaseExport` and `BaseAnalyze` are helper bags rather than templates, and `ConfigResolver` is exposed to commands that have no config file option.

Telemetry and diagnostics are the priority; the current shape makes them unreliable and ties every command to an eager connection. The change is internal to the module layer and does not alter the command-line surface, so it is feasible to do now.

## What Changes

- Replace `PowerLogic<T>` and the `Base*` command classes with composition: commands are `AsyncCommand<T>` (or derive from one thin lifecycle base) and receive only the services they use through constructor injection.
- Introduce a thin lifecycle base class that owns the run: it opens one telemetry span, records success or failure and exceptions on it, prints a quiet start line and an end line (command path, result, duration), and maps the result to an exit code. It carries no services except a span factory (telemetry only) and the console it prints to.
- Split `ITracer` into a run/span concern and a diagnostics concern (`Log`, `Exception`) that records onto `Activity.Current`. Remove `IPowerLogic` and the `Start<T>/End<T>` generics.
- Make `IDataverseConnection` the single entry point to Dataverse: rename `ConnectAsync` to `GetServiceAsync`, make it a process-wide singleton that connects on first call and caches the result (safe under concurrent first calls). Remove the eager `IOrganizationService` registration; nothing in DI exposes an organization service directly.
- Services, repositories and helpers that need Dataverse receive `IDataverseConnection` and resolve the service inside methods. Short-lived helpers created after connecting (for example `LintContext`) continue to take the resolved `IOrganizationServiceAsync2`.
- Register `IConfigResolver`, `IFileService` and shared export/import/analyze helpers as plain DI services; commands request only what they need.
- Keep exit code semantics: `0` success, `1` failure, `2` authentication required (via the existing exception path). "Not configured" stays a failure (`1`) and "skipped" stays `0`; they are no longer separate lifecycle outcomes.
- **BREAKING (internal only):** the abstract signature `InvokeAsync(args, ct)` and the `PowerLogic` constructor contract disappear; all command constructors change. No user-facing break is intended. The class-name start/end rules (`<Class> - Start/End:OK`) are replaced by command-path start and end lines; this is the only intentional output change.
- Add a developer guideline: constructors and field initializers MUST NOT perform I/O or block on tasks (`GetAwaiter().GetResult()`, `.Result`, `.Wait()`); Dataverse access happens only inside async methods.

## Capabilities

### New Capabilities
- `command-execution`: How a command run is executed and observed: Dataverse connection is acquired on demand rather than at command construction, one telemetry span per run, start and end status lines, and the exit code contract (`0`, `1`, `2`).

### Modified Capabilities
<!-- None. Existing connection-management requirements (selection precedence, non-interactive exit code 2) are preserved unchanged. -->

## Impact

- `src/dgt.power.common`: `PowerLogic`, `IPowerLogic`, `ITracer`, `IDataverseConnection`, `DataverseConnection`, `ConfigResolver`.
- `src/dgt.power`: `Program.cs` registrations, `Tracer`, exception handler (unchanged behavior), telemetry interceptors.
- All modules under `src/modules` (about 46 command and base classes) and their repositories.
- `tests/`: `dgt.power.tests` builder and doubles (`TestConnection`, `TestServiceCollection`, `TestTracer`), `TracerTests`, per-module command tests.
- Docs: `todo.md` (remove the two TODO sections), the `AGENTS.md` async checklist and a new rule on I/O-free constructors, an ADR for the composition decision, `docs/architecture/decisions/README.md`.
- No NuGet dependency changes and no configuration/schema changes.
