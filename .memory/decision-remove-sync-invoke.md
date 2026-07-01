# Decision: Remove Sync `Invoke` from `PowerLogic<T>`

## Context

`PowerLogic<T>` originally had two methods:
```csharp
protected abstract bool Invoke(TConfig args);
protected virtual Task<bool> InvokeAsync(TConfig args, CancellationToken cancellationToken) => Task.FromResult(Invoke(args));
```

This forced every subclass — including truly async-only commands — to implement `Invoke`. Async-only
commands worked around this by implementing `Invoke` as `throw new NotSupportedException(...)`, which
was misleading and unnecessary.

## Decision

Remove `Invoke` entirely. Make `InvokeAsync` the single abstract entry point:

```csharp
protected abstract Task<bool> InvokeAsync(TConfig args, CancellationToken cancellationToken);
```

## Migration Pattern for Previously Sync Commands

Sync commands extracted their body into a private `InvokeCore` and wrap with `Task.FromResult`:

```csharp
protected override Task<bool> InvokeAsync(TVerb args, CancellationToken cancellationToken) =>
    Task.FromResult(InvokeCore(args));

private bool InvokeCore(TVerb args)
{
    // original body unchanged
}
```

This is an interim compatibility pattern while command implementations are still a mix of sync and async
Dataverse calls. Classes can migrate to true async (`IOrganizationServiceAsync2`) incrementally without
changing the `PowerLogic<T>` contract again.

## Rationale

- Eliminates `NotSupportedException` anti-pattern for async-only commands
- Consistent contract: every command is `Task<bool>` at the boundary
- `Task.FromResult` is acceptable for CLI tools (no thread-pool starvation risk)
- Paves the way for incremental `IOrganizationServiceAsync2` adoption per class

## Alternatives Considered

- **Virtual `Invoke` with default throw**: Would still require subclasses to know about the sync method.
  Rejected: adds confusion, not clarity.
- **Two separate base classes (sync/async)**: Over-engineering. Rejected.
