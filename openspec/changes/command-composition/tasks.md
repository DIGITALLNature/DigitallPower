# Tasks

Assumptions: the class-name start/end rules are replaced by command-path status lines (design D2a); `CodeGenerationCommand` is migrated in group 5. Each group lands on `refactor/command-composition` as its own buildable, green commit(s).

## 1. Connection foundation

- [x] 1.1 Rename `IDataverseConnection.ConnectAsync` to `GetServiceAsync` (interface, `DataverseConnection`, `TestConnection`, `FakeDataverseConnection`, callers) and verify `rtk dotnet build` succeeds
- [x] 1.2 Cache the connected service in `DataverseConnection` so concurrent first calls share one connect and verification, and failures are not cached; add tests for single connect under concurrency, reuse on later calls and retry after a failure, and verify they pass
- [x] 1.3 Add an `AGENTS.md` rule that constructors and field initializers perform no I/O and never block on tasks (`GetAwaiter().GetResult()`, `.Result`, `.Wait()`), and verify whether an existing analyzer rule can enforce it (record the result in the rule text)

## 2. Run lifecycle and diagnostics

- [x] 2.1 Introduce the span factory and `IDiagnostics` (`Log`, `Exception` recording onto `Activity.Current`), moving telemetry gating from `Tracer` without changing span name or tags, and verify ported `TracerTests` pass for span name, tags (`dgtp.command`, `dgtp.success`), suppression and anonymization
- [x] 2.2 Add a `CommandAppTester` test that records what `CommandContext.Name` holds for top-level and nested commands; if it is not the full path, attach the path with `WithData` in `CommandTree.Register` and verify the test asserts the path for every registered command
- [x] 2.3 Print the start line and the left-aligned end `Rule` with result and duration (Spectre `Emoji.Known` symbols, normal console) from the lifecycle base class, with no separate writer class, and verify tests cover success, failure and thrown exception
- [x] 2.4 Add `PowerCommand<TSettings>` (span via `using`, `RunAsync` returning `Task<bool>` mapped to `0`/`1`, exceptions recorded then rethrown, status lines printed in `finally`) and verify tests cover success, failure, thrown exception and that the span is closed in every case
- [x] 2.5 Mark exceptions already recorded by a run and skip them in `TrackFatalException`; verify with a test that an escaping exception produces one span and the exception handler still returns `1` or `2`
- [x] 2.6 Register the new services in `Program.cs` next to the existing `ITracer` (legacy path stays functional) and verify `rtk dotnet test` for `dgt.power.cli.tests` passes
- [ ] 2.7 Update `README.md` for the changed start/end output and verify the README exit code table is unchanged

## 3. Migrate modules already close to the target

- [x] 3.1 Migrate solution commands (`SolutionVersion`, `SolutionLint`, `CopyComponents`) to `PowerCommand` with `IDataverseConnection`, removing the casts, and verify `dgt.power.solution.tests` pass
- [x] 3.2 Migrate plugin commands (`PluginPush`, `PluginStep*`) the same way, keeping repositories constructed after connecting, and verify `dgt.power.plugin.tests` pass
- [x] 3.3 Migrate `WebResourcePushCommand` and verify `dgt.power.webresource.tests` pass
- [x] 3.4 Update `TestConnection`/`TestServiceCollection`/`CommandTestContextBuilder` so migrated commands resolve `IDataverseConnection` only, and verify migrated and not-yet-migrated tests both pass

## 4. Migrate maintenance, analyze, export and import

- [ ] 4.1 Replace `BaseMaintenance` and migrate maintenance logic, passing the resolved service to existing sync code, and verify `dgt.power.maintenance.tests` pass
- [ ] 4.2 Replace `BaseAnalyze` helpers (solution component queries, component type lookup) with an injected service, migrate analyze commands, and verify `dgt.power.analyzer.tests` pass
- [ ] 4.3 Replace `BaseExport` helpers (JSON serialization, file export) with an injected writer service, migrate export commands, and verify `dgt.power.export.tests` pass
- [ ] 4.4 Replace `BaseImport` helpers with injected services, migrate import commands, and verify `dgt.power.import.tests` pass
- [ ] 4.5 Request `IConfigResolver` only in commands that read config files and verify no command still receives it through inheritance (search shows no `ConfigResolver` property on a base)

## 5. Remove legacy and finish

- [ ] 5.1 Migrate `CodeGenerationCommand` off `IPowerLogic` (async on `PowerCommand`, or an explicit run in a sync path) and verify `dgt.power.codegeneration.tests` pass
- [ ] 5.2 Delete `PowerLogic`, `IPowerLogic`, the `Base*` classes, the old `ITracer` lifecycle members and the eager `IOrganizationService` registration in `Program.cs`; the remaining `Tracer` (only `TrackFatalException`) is renamed to a specific name such as `FatalExceptionReporter`; verify `rtk dotnet build` has zero warnings-as-errors and no references remain (search)
- [ ] 5.3 Update `todo.md` (remove both TODO sections), the `AGENTS.md` async checklist (no `(IOrganizationServiceAsync2)Connection` pattern), the `NoOpCommand` doc comment and `README.md` project structure, and verify no stale references to `PowerLogic` remain in docs
- [ ] 5.4 Add an ADR under `docs/architecture/decisions/` for composition with a thin lifecycle base and a cached `IDataverseConnection`, link it in the ADR index, and verify the index lists it
- [ ] 5.5 Run `rtk dotnet test`, `pnpm exec openspec validate --all`, and a manual `dgtp connection list` run showing no authentication attempt; verify all pass and exit codes `0`, `1`, `2` behave as specified

## Workflow follow-up

- Sync the `command-execution` delta into main specs and archive the change after review.
- Refresh generated Copilot instructions with `pnpm run openspec:update` if the OpenSpec guidance changed.
