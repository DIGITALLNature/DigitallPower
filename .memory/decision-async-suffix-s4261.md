# Decision: Enforce Async Suffix on Async Methods (S4261)

## Rule

Sonar rule **S4261** requires that all `async` methods have an `Async` suffix.

## Decision

**Rename all async methods in `src/` to add the `Async` suffix.**  
Test methods are **exempted** (see below).

## Rationale

- Standard .NET convention; improves call-site readability
- Prevents confusion between sync and async overloads

## Test Method Exemption

Test framework methods (TUnit) use the method name as the test display name.  
Adding `Async` to test method names clutters test output without benefit.

**Solution:** Created `tests/.editorconfig` with:
```ini
[*.cs]
dotnet_diagnostic.S4261.severity = none
```

This suppresses S4261 for all `*.cs` files under `tests/` without touching `src/`.

## Methods Renamed in src/

| Class | Old Name | New Name |
|-------|----------|----------|
| `WorkflowStateManager` | `Execute` | `ExecuteAsync` |
| `WorkflowStateManager` | `GetWorkflowState` | `GetWorkflowStateAsync` |
| `WorkflowStateManager` | `GetWorkflowStates` | `GetWorkflowStatesAsync` |
| `WorkflowStateManager` | `ActivateWorkflow` | `ActivateWorkflowAsync` |
| `WorkflowStateManager` | `DeactivateWorkflow` | `DeactivateWorkflowAsync` |
| `WorkflowStateManager` | `SetWorkflowState` | `SetWorkflowStateAsync` |
| `WorkflowStateManager` | `CheckAndReport` | `CheckAndReportAsync` |
| `UpdateWorkflowState` | `UpdateState` | `UpdateStateAsync` |
| `UpdateWorkflowState` | `VerifyState` | `VerifyStateAsync` |
