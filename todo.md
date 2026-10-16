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

### Note on `Connection` type
Commands now acquire the connection lazily through `IDataverseConnection.GetServiceAsync()`, which already returns `IOrganizationServiceAsync2`, so
the `(IOrganizationService)` casts and synchronous calls can simply be dropped.

---

## Shared paging helper

`DataverseQueryReader.ReadAllAsync` (plugin module, internal) is the only reusable paged read. About 11
other places hand-copy the `MoreRecords`/`PagingCookie` loop (solution, analyzer, export, import,
maintenance, `EntityDataProviderRepository`).

**Goal:** move it to `dgt.power.common` as an extension and use it everywhere.

- Signature: `IOrganizationServiceAsync2.RetrieveAllAsync(QueryExpression query, int pageSize = 5000, CancellationToken cancellationToken = default)`.
- The method owns `query.PageInfo` and overwrites it. Document that on the method; callers must not rely on their own paging settings.
- `QueryExpression` only; FetchXML and `QueryByAttribute` loops stay as they are.
- Replace `DataverseQueryReader` and `LintContext.PageSize`, then the hand-written loops.
- Add a common test with a multi-page fake (page size, cookie, page number, cancellation). Do this first in the async migration branch.

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
