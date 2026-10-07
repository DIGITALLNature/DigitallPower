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
