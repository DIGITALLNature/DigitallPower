# Technical Debt & Backlog

## Async Migration: IOrganizationServiceAsync2

All command classes currently implement `RunAsync` but internally use the synchronous
`IOrganizationService` (via `DataContext` LINQ queries and blocking SDK calls).

**Goal:** Migrate each module's logic to use `IOrganizationServiceAsync2` with true `async/await`
instead of `Task.FromResult(...)` wrappers.

### Why
- `Task.FromResult` wraps sync work — no thread is released on blocking I/O calls to Dataverse
- `IOrganizationServiceAsync2` enables true non-blocking SDK calls (`ExecuteAsync`, `RetrieveMultipleAsync`, etc.)
- Already used in truly async commands (`EnsureSdkStepStatus`, `UpdateWorkflowState`) — pattern proven

### Pattern (after migration)
```csharp
// Before (current interim)
protected override Task<bool> RunAsync(TSettings settings, CancellationToken cancellationToken) =>
    Task.FromResult(RunCore(settings));

private bool RunCore(TSettings settings)
{
    var result = ((IOrganizationService)connection).Execute(request);  // blocking
}

// After (target)
protected override async Task<bool> RunAsync(TSettings settings, CancellationToken cancellationToken)
{
    var result = await (await connection.GetServiceAsync()).ExecuteAsync(request, cancellationToken);  // non-blocking
}
```

### Scope (per module)

| Module | Classes | Notes |
|--------|---------|-------|
| `dgt.power.analyzer` | 6 classes | Use `RetrieveMultipleAsync` instead of LINQ DataContext |
| `dgt.power.export` | 9 classes | Mix of LINQ DataContext + direct SDK calls |
| `dgt.power.import` | 10 classes | Direct SDK calls — most straightforward to migrate |
| `dgt.power.maintenance` | 7 classes | Mixed; some already async (`EnsureSdkStepStatus`, `UpdateWorkflowState`) |

### Note on DataContext
`DataContext` (LINQ to CRM) is fundamentally synchronous. Replacing it requires switching to
`QueryExpression` + `RetrieveMultipleAsync`. This is the main effort per class.

### Note on the connection
Commands already acquire the service lazily via `IDataverseConnection.GetServiceAsync()`, which returns
`IOrganizationServiceAsync2`. Only the remaining `(IOrganizationService)` casts and synchronous calls need to go.

---

## Shared paging helper

`IOrganizationServiceAsync2.RetrieveAllAsync` (`dgt.power.common`) is the shared paged read and is used by
the plugin and solution modules.

**Remaining:** the synchronous `MoreRecords`/`PagingCookie` loops in analyzer (`SolutionComponentReader`,
`RedundantComponentsAnalyze`), export (`QueueExport`, `UserRoleExport`), import (`QueueImport`) and
maintenance (`RemoveRedundantComponents`). Replace them with `RetrieveAllAsync` as part of the async
migration of those classes; they cannot use it before they have an `IOrganizationServiceAsync2`.

---

## Connection output cleanup

Two cosmetic issues in the console output every connecting command shows first. Both live in shared
connection code (`DataverseConnectionStringConnector`, `DataverseConnection`), so do them in a dedicated PR.

- **Outdated connection string hints:** `DataverseConnectionStringConnector` prints "It's recommended to use
  'SkipDiscovery=true'!" and "'RequireNewInstance=true'!" for every ad-hoc connection string without those
  flags. They are leftovers from the legacy `CrmServiceClient`. Discovery is skipped for modern `Url=`
  strings, and dgtp creates one client per run. Remove both hints and the two regexes.
- **Context-free connection lines:** `DataverseConnection` prints `Using ad-hoc connection string (not
  persisted) for <url>` and a bare `WhoAmI: <guid>`. Replace them with one dimmed line that says what was
  connected, for example `Connected to <url> as <user>`. Keep the WhoAmI request as the connection check.
- Update tests that assert the current lines.

---

## Global `--verbose` flag

Add a global `--verbose` option (shared settings base, tracer behavior and every module) for
optional diagnostic detail. First intended consumer: `plugin push`, which already tracks
assemblies skipped by the batch reader (`TargetReadResult.Skipped`) and could list them, plus the
reason, when verbose output is requested. Default output stays quiet.

Deliberately out of scope for the plugin rewrite: it changes shared settings and every module.

---

## Package replacement for removals

Idea: mirror the standalone replacement path for plugin packages. A removal would require a new
package identity: the tool creates a new package record, moves surviving steps and Custom API
links onto its types, then deletes the old package. A managed import could then replace the old
package without hitting "Unable to delete plugintype due to 1 step(s) registered on it", because
package assemblies are otherwise updated in place (same `pluginassemblyid` across assembly
versions).

Open questions to prototype:
- Identity scheme: the package unique name is `<prefix>_<package id>` and excludes the version,
  so a new record needs a derived name (for example a major-version suffix) or a new NuGet id.
- Moving steps onto types in a different assembly (see test case P12).
- Whether Dataverse allows two packages with the same assembly name side by side, and whether the
  import deletes the old package cleanly.
- Scope: trigger only for removals; keep in-place updates for everything else.
- User impact: package name change needs a migration guide entry.
