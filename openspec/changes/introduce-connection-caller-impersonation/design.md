# Design

## Context

`IDataverseConnection.GetServiceAsync()` returns `IOrganizationServiceAsync2`; `DataverseConnection` always backs it with a `ServiceClient` created by a connector (connection string or `uri` + token callback) and caches one instance per invocation. `ServiceClient.CallerId` is mutable instance state that applies to every subsequent request. Tests replace the connection with `TestConnection`, which wraps a fake `IOrganizationServiceAsync2` that is not a `ServiceClient`.

Consequences for consumers today: a module can only impersonate by casting to `ServiceClient` (always false in tests, so the success path is untestable) or by reflection (the legacy approach with a silent fallback).

## Goals / Non-Goals

**Goals:** One explicit, testable connection-level entry point for caller impersonation; no mutation of the shared service; no silent fallback.

**Non-Goals:** Exposing `ServiceClient` from `IDataverseConnection`; a general request-pipeline or decorator framework; resolving principals; checking impersonation privileges up front.

## Decisions

### Proposed API

Add one overload to `IDataverseConnection`:

```csharp
Task<IOrganizationServiceAsync2> GetServiceAsync(Guid callerId);
```

`callerId` is a Dataverse `systemuser` ID. `Guid.Empty` is rejected with `ArgumentException`.

Alternatives considered:

| Option | Rejected because |
| --- | --- |
| Return `ServiceClient` from `GetServiceAsync()` | Touches ~44 consumers and all fakes for one feature; leaks the SDK type everywhere |
| Cast to `ServiceClient` inside the consuming module | Unit tests can never reach the success path; every consumer repeats the check |
| Mutable `CallerId` property on the connection or shared service | Requires set/restore discipline in every caller and breaks under any concurrent use |
| Separate `IImpersonatingConnection` interface | Extra abstraction with a single implementation; same DI and test surface as the overload |
| Default interface method that ignores `callerId` | Recreates the silent fallback this change exists to remove |

### Production behavior

`DataverseConnection` obtains the shared `ServiceClient`, creates a derived instance for the caller, sets `CallerId` on that instance only, caches it per `callerId` for the invocation, and disposes it with the connection. The shared service is never mutated. Creation shares the existing connect gate, so no request runs before the instance is fully configured.

The preferred way to derive the instance is `ServiceClient.Clone()`; if cloning does not work with the token-callback constructor, create a second instance through the same connector. The choice is invisible to callers.

### Test doubles

`TestConnection` returns the same fake service and records the requested `callerId` per returned instance (or wraps the fake to tag requests), so consumers can assert which operations ran impersonated. Other fakes implement the overload with `NotSupportedException`. No implementation may return a non-impersonated service for this overload.

### Failure semantics

- An implementation that cannot impersonate throws; consumers surface it before their first write when they acquire the service during preflight.
- Missing `prvActOnBehalfOfAnotherUser` or a disabled caller surfaces as the Dataverse fault on the first request. It is not pre-validated here; consumers decide whether it is a per-object runtime failure.
- Cancellation and authentication failures propagate unchanged.

## Risks / Trade-offs

- [Clone behavior with token-callback clients is unverified] -> spike first; fall back to a second connector-created instance.
- [Extra `ServiceClient` per caller increases connections] -> cache per `callerId` within the invocation; typical runs use one caller.
- [Interface change touches all test doubles] -> mechanical update; no consumer of `GetServiceAsync()` changes.

## Migration Plan

Additive for commands; breaking only for in-repo `IDataverseConnection` implementations, which are updated in the same change. No user-facing migration. Rollback is a code revert.

## Open Questions

To be discussed (Marcel/Lars) before specs and tasks are written:

1. Is an overload on `IDataverseConnection` acceptable, or does the team prefer a separately injected interface despite the single implementation?
2. Does `ServiceClient.Clone()` work with the `uri` + token-callback constructor and connection-string clients without reauthentication? (Spike.)
3. Should the per-caller cache live for the whole invocation, or should consumers own and dispose the returned instance?
