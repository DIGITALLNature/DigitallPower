# Guide: Persist-After-Verify for Connection Mutation Commands

## Problem

Creating a named connection may stage a client secret or PFX password before its optional
Dataverse connectivity check. Persisting the connection metadata or making it current before that
check succeeds can leave a broken connection selected after a failed create.

## Root cause insight

The current `CreateConnectionCommand` can construct its credential directly from the candidate
definition and staged secret; the connection does not need to be inserted into `ConnectionStore`
before it is verified. Persist only after authentication/verification succeeds:

```csharp
var credential = credentialFactory.Create(name, definition, ...);
await credential.GetTokenAsync(scope, cancellationToken);
connectionStore.Upsert(name, definition);
```

When secret material is staged before verification, restore the previous secret or remove the new
one if the command fails. If `--no-verify` is supplied, persist the typed definition without the
Dataverse token check, as explicitly requested by the user.

## Where this applies

- `src/modules/dgt.power.connection/Commands/CreateConnectionCommand.cs`

Future create/update commands should keep the invariant **stage → verify → persist**, and treat
metadata and separately stored secret values as one logical mutation that needs rollback on failure.

## Test pattern

Use a temporary `DgtpHome` and the same `ConnectionStore` instance as the command, then assert the
file-backed definition and current selection remain unchanged after a simulated verification
failure. Verify secret rollback independently through the fake `ISecretStore`.
`CreateConnectionCommandTests` covers successful persistence, existing-definition/selection
preservation, secret restoration, and the `--no-verify` bypass.
