# Design

## Context

See [proposal.md](proposal.md) for motivation and scope.

`IDataverseConnection.GetServiceAsync()` returns `IOrganizationServiceAsync2`; `DataverseConnection` creates a `ServiceClient` through either a connection-string connector or a credential-backed token callback and caches it for the invocation. `ServiceClient.CallerId` is mutable instance state. Changing it on the shared service affects subsequent requests from every consumer.

Tests use services that are not necessarily `ServiceClient` instances. Impersonation must therefore be part of the connection contract, not a consumer-side cast or reflection convention.

The expected workload uses one caller in most runs and up to a few dozen distinct callers occasionally. The number of distinct callers, rather than the number of processed objects, determines resource growth.

## Goals / Non-Goals

**Goals:** One explicit, testable connection-level entry point; isolation between the standard service and each caller; connection-owned lifetime; explicit failures without identity fallback.

**Non-Goals:** Exposing `ServiceClient`; a request-pipeline or decorator framework; principal resolution; privilege preflight; CLI options; changing legacy workflow commands here; global client pools, eviction, leases, or a new authentication abstraction.

## Decisions

### Connection API

Add one overload to the existing interface:

```csharp
Task<IOrganizationServiceAsync2> GetServiceAsync(Guid callerId);
```

`callerId` is a Dataverse `systemuser` ID, not an Entra object ID or an owner key. Reject `Guid.Empty` with `ArgumentException` before authentication or connection creation. Keep the parameterless overload unchanged. Do not add a cancellation-token overload as part of this change.

Alternatives considered:

| Option | Rejected because |
| --- | --- |
| Return `ServiceClient` or cast to it in consumers | Leaks the SDK implementation and prevents non-SDK test services from exercising the same contract |
| Mutable caller on the shared connection or service | Requires set/restore discipline and allows concurrent requests to use the wrong identity |
| Separate impersonation interface | Adds an abstraction without a separate responsibility or lifecycle |
| Default interface method that ignores `callerId` | Recreates the silent fallback this change exists to remove |

### One explicit client-creation path

Create caller clients through the existing connector paths, using the same environment and authentication configuration selected for the standard service. Retain that selection for the invocation; do not resolve it again from saved state for each caller.

Force an independent SDK connection for each caller client. Distinct `ServiceClient` objects alone are not sufficient if SDK-internal connection reuse shares caller state. Cover both the token-callback and connection-string paths, including connection strings that otherwise permit reuse.

Set `CallerId` before publishing the caller service. Never mutate the standard service or another caller service. Reuse credential/token infrastructure where possible without introducing a new authentication layer. Additional clients do not imply a guaranteed additional interactive login, but neither does this design promise that authentication can never be required again.

Do not use `ServiceClient.Clone()`. Its documented authentication restrictions and unverified compatibility with the installed SDK's token-callback path do not justify a second creation strategy. There is no try-clone-then-reconnect fallback.

### Invocation ownership and concurrency

Create caller clients lazily and cache one successful service per caller ID for the invocation. Repeated acquisition of the same caller returns the same service. The connection owns the standard client and every caller client and disposes them at the end of its lifetime. Consumers borrow these services: they must neither dispose them nor change their caller state.

Use the existing connection gate to protect initialization and cache publication. Concurrent acquisition of one caller must not publish duplicate or partially configured clients. Do not hold this gate around business requests or introduce request scheduling.

With `U` distinct callers, the connection retains one standard client and up to `U` caller clients. Accept this invocation-bounded cost for the expected workload. Do not add a caller limit, global cache, LRU, or leases. Eviction would be unsafe while consumers hold raw service references; short-lived consumer-owned sessions would require a different API and a revised consumer preflight contract.

### Failure and preflight boundaries

Successful acquisition means that the connection has initialized a ready, isolated client and configured its caller. It does not prove that the caller exists, is enabled, or has permission for a later operation. Principal resolution belongs to consumers; Dataverse enforces permissions on actual requests.

Retain the existing standard connection verification. Do not add a `WhoAmI` request per caller or a privilege probe. Client initialization may itself perform SDK-internal network requests; this is not an I/O-free acquisition guarantee.

If caller-client creation or configuration fails, dispose the unfinished client, do not cache it, and propagate the exception. A subsequent acquisition may retry. Never substitute the standard service or another caller. Implementations without impersonation support throw explicitly.

Authentication failures, cancellation, and faults from business requests propagate without an identity fallback. Consumers that require zero writes on acquisition failure must acquire all required services before their first write. This does not guarantee that later writes will succeed.

### Connection feedback

Preserve the existing standard connection feedback and sanitized error handling. Do not emit an additional "Connected" success line for each caller that could imply verified impersonation privileges. Acquisition errors flow through existing command error handling; never print tokens or raw connection strings.

### Explicit caller services in tests

Test doubles support caller services through explicit configuration, with distinguishable services for the standard identity and different callers. An unconfigured caller request throws `NotSupportedException`; it never returns the standard fake as a success-shaped fallback.

Consumer tests assert the actual requests executed on the configured services, not merely the caller IDs passed to acquisition. Cover interleaved standard requests and requests for at least two callers. Do not build a general request recorder or simulate Dataverse security roles.

Connection tests cover same-caller reuse, different-caller isolation, concurrent acquisition, validation before authentication, failed initialization followed by retry, and ownership/disposal. Verify SDK connection independence for both production connector paths rather than treating separate mock references as proof of SDK isolation.

## Risks / Trade-offs

- [Resources grow with distinct callers] -> Cache only for the invocation and reuse each caller client. Revisit the API only if large caller cardinality becomes a demonstrated workload.
- [SDK connection reuse can undermine isolation] -> Force independent connections and verify caller state for both supported connector paths.
- [New clients incur connection and authentication work] -> Reuse the invocation's selected configuration and available credential infrastructure; do not promise a free clone or invent unmeasured resource estimates.
- [Acquisition can succeed before a later permission failure] -> State the preflight boundary explicitly and preserve Dataverse faults on actual requests.
- [Interface expansion breaks custom implementations] -> Update every repository implementation and mock together and document the required overload.

## Migration Plan

Update `IDataverseConnection`, its production implementation, and all repository test doubles in one implementation change. Keep existing parameterless consumers and CLI behavior unchanged. Legacy workflow reflection is removed by `introduce-process-resource`, not by this change.

Document the overload, borrowed-service ownership, and limited preflight guarantee in README. Add a short 3.x migration note for custom interface implementations: implement the overload or throw explicitly; never ignore the caller. No configuration schema or package dependency changes are required.

Rollback reverts the interface, implementation, tests, and documentation together. It does not undo Dataverse operations already performed by consumers.
