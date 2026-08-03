# Guide: Persist-After-Verify for Connection/Profile Mutation Commands

## Problem

Commands that create or update a Dataverse connection/profile (`CreateConnectionCommand`,
`CreateProfileCommand`) mutate an in-memory `IIdentities` store via `Identities.Upsert(name, identity)`.
`Upsert` immediately sets `Current` to the new identity, in memory, as a side effect.

If the command then calls `profileManager.Save()` *before* running the post-create connectivity
check (`connection.ConnectAsync()` / `WhoAmI`), a failed check leaves the broken identity already
persisted to disk **and** selected as the active connection — the CLI is left in a broken state
until the user manually deletes/recreates the profile.

## Root cause insight

`XrmConnection.ConnectAsync()` reads `profileManager.CurrentIdentity`, which reflects the in-memory
`Identities.Current` set by `Upsert()`. The connectivity check does **not** require `Save()` to have
run first — it only needs the in-memory mutation, which already happened via `Upsert`.

This means the fix is a pure **reordering**, not a rollback/snapshot mechanism:

```csharp
identities.Upsert(name, identity);          // in-memory only; ConnectAsync can already see it

if (!settings.NoVerify)
{
    try { await connection.ConnectAsync(); }
    catch (FailedConnectionException fc) { console.WriteLine(fc.RootMessage()); throw; }
}

profileManager.Save();                      // only persisted after a successful check
```

If `ConnectAsync()` throws, the method returns before `Save()` runs, so nothing is ever written to
disk — whatever was previously persisted (a known-good connection) remains untouched and active on
next run.

## Where this applies

- `src/modules/dgt.power.connection/Commands/CreateConnectionCommand.cs`
- `src/modules/dgt.power.profile/Commands/CreateProfileCommand.cs` (deprecated alias, same bug,
  fixed for consistency since `dgt.power.connection` was ported from it)

Any future command that upserts an identity and then verifies connectivity should follow the same
order: **mutate in-memory → verify → persist**.

## Test pattern

Regression tests assert the *persisted* state, not command-local in-memory state, to actually catch
ordering regressions. This requires the `IProfileManager` to be registered **Transient** in the test
DI container (see `ProfileTestsBase` / `ConnectionTestsBase`), so that a fresh `ProfileManager`
instance is created each time the test calls `GetIdentities()` — reading from the same isolated
storage file, but decoupled from the command's own singleton in-memory instance.

Key tests added:
- `ShouldNotPersistIdentity_WhenConnectionCheckFails` — asserts `GetIdentities().Contains(name)` is
  `false` after a failing create.
- `ShouldNotChangeCurrentIdentity_WhenNewIdentityCreationFails` — creates a good profile first, then
  a failing one, and asserts `Current` still points at the good profile.

Applied in:
- `tests/dgt.power.profile.tests/CreateProfileCommandTests.cs`
- `tests/dgt.power.connection.tests/CreateConnectionCommandTests.cs` (new test project — the
  `dgt.power.connection` module previously had **zero** test coverage; `ConnectionTestsBase` mirrors
  `ProfileTestsBase`)
