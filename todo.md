# Technical Debt & Backlog

## Async Migration: IOrganizationServiceAsync2

All `PowerLogic<T>` subclasses currently implement `InvokeAsync` but internally use the synchronous
`IOrganizationService` (via `DataContext` LINQ queries and blocking SDK calls).

**Goal:** Migrate each module's logic to use `IOrganizationServiceAsync2` with true `async/await`
instead of `Task.FromResult(InvokeCore(...))`.

### Why
- `Task.FromResult` wraps sync work — no thread is released on blocking I/O calls to Dataverse
- `IOrganizationServiceAsync2` enables true non-blocking SDK calls (`ExecuteAsync`, `RetrieveMultipleAsync`, etc.)
- Already used in truly async commands (`EnsureSdkStepStatus`, `UpdateWorkflowState`) — pattern proven

### Pattern (after migration)
```csharp
// Before (current interim)
protected override Task<bool> InvokeAsync(TVerb args, CancellationToken cancellationToken) =>
    Task.FromResult(InvokeCore(args));

private bool InvokeCore(TVerb args)
{
    var result = ((IOrganizationService)Connection).Execute(request);  // blocking
}

// After (target)
protected override async Task<bool> InvokeAsync(TVerb args, CancellationToken cancellationToken)
{
    var result = await ((IOrganizationServiceAsync2)Connection).ExecuteAsync(request);  // non-blocking
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

---

## Connection storage

The legacy profile module and profile service layer have been removed. `dgt.power.connection` is
the sole connection-management module; saved connections use typed definitions in the stable
`DGTP_HOME` data directory. Existing isolated-storage connections are not migrated. See
`.memory/implementation-typed-connection-storage.md`.

### Migration guide follow-up

After PR #193 (https://github.com/DIGITALLNature/DigitallPower/pull/193) is merged and the
connection rewrite branch is rebased, extend the migration guide introduced by that PR to
document the legacy profile removal:

- Map the removed `profile` commands to the supported `connection` commands.
- Explain that legacy isolated-storage connections are not imported; users must recreate them
  with typed authentication options.
- Document replacement of `--profile` with `--connection`, and the non-persisted
  `--connection-string` / `DGTP_CONNECTION_STRING` alternative to saved connection strings.
- Link the guide from the README and remove stale references to deleted profile types and projects
  from contributor documentation and examples, preserving explicitly historical notes.
