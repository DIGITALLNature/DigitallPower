# Proposal

## Why

Commands that must act on behalf of another Dataverse user have no supported way to do so. The legacy workflow command sets `CallerId` by reflection on the shared service and silently continues without impersonation when the property is missing. `introduce-process-resource` needs explicit, testable impersonation that never falls back to the connection identity.

## What Changes

- Extend `IDataverseConnection` with a way to obtain a service that executes requests on behalf of a given Dataverse user (`CallerId`).
- Keep the run's shared service unchanged; impersonated requests use a separate service instance, so callers need no set/restore protocol.
- Make impersonation observable in test doubles so consumers can unit-test which requests ran under which caller.
- Fail explicitly when a connection implementation cannot provide impersonation; no default implementation that ignores the caller.
- Out of scope: Microsoft Graph, Entra lookups, resolving owner keys to user IDs (see `introduce-portable-owner-resolution`), privilege preflight for `prvActOnBehalfOfAnotherUser`, and CLI options. Consumers own their options.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

- `connection-management`: Adds the contract for obtaining a caller-impersonated service from the invocation's connection without changing the shared service or falling back to the connection identity.

## Impact

`src/dgt.power.common/IDataverseConnection.cs`, `src/dgt.power.common/Logic/DataverseConnection.cs`, and every `IDataverseConnection` implementation in tests (`TestConnection`, `FakeDataverseConnection`, `StubConnection`, mocks). No new package dependency. `introduce-process-resource` depends on this change for its `--activate-as` / `--activate-as-owner` options.
