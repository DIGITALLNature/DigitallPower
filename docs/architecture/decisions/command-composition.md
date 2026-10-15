# Command Composition with a Thin Lifecycle Base

## Context

Commands derived from `PowerLogic<T>`, which injected a tracer, a configuration resolver and an eagerly connected `IOrganizationService` into every subclass. Every command connected and authenticated while it was constructed, including commands that never use Dataverse. The tracer mixed console banners, telemetry spans and diagnostics, and the base class exposed the synchronous service interface.

## Decision

- `PowerCommand<TSettings>` is a thin lifecycle base. It owns exactly one telemetry span and the start/end console lines, and maps `RunAsync` results to exit codes `0`/`1`. Exceptions are recorded on the span and rethrown.
- Everything else is composed. Commands request `IDataverseConnection` and call `GetServiceAsync()` inside `RunAsync`; `IConfigResolver` and other services are requested only where used.
- `IDataverseConnection.GetServiceAsync()` caches the connected service: concurrent first calls share one connect, and failures are not cached.
- `IOrganizationService` is no longer registered in DI. Synchronous code receives the same instance through a cast, because `ServiceClient` implements both interfaces.
- User-facing messages go to `IAnsiConsole`; telemetry is recorded only by the span. Escaped exceptions are reported by `FatalExceptionReporter` unless the run already recorded them.

## Rationale

A base class that hands every property to every subclass couples commands to infrastructure they do not use and forces an eager connection. Composition keeps the dependency visible in the constructor, lets non-Dataverse commands skip authentication, and keeps the lifecycle concern small enough to test on its own. Passing the service as a local rather than a property also prevents use before it exists.

## Consequences

New commands extend `PowerCommand<TSettings>` and acquire the service after validation that needs no Dataverse. Constructors and field initializers perform no I/O. Synchronous helpers such as `MetadataService` read the cached service and rely on the command having connected first.
