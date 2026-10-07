# Decision: Remove --insecure and --security-protocol CLI Options

## Context

The `profile create` command had two options:
- `--insecure` — skip SSL certificate validation
- `--security-protocol` — set `SecurityProtocolType` (tls12, tls13, ...)

Both were implemented via `System.Net.ServicePointManager`, which is a no-op on .NET 8+.
The options gave users false confidence that they were configuring something meaningful.

## Decision

**Remove both options entirely.** Mark as breaking change.

## Rationale

1. Both options were already non-functional on .NET 8+ (see `research-servicepointmanager-dotnet8.md`)
2. `Microsoft.PowerPlatform.Dataverse.Client` offers no `HttpClient`/`HttpMessageHandler` injection point
3. Keeping dead CLI options is misleading and a security anti-pattern (`--insecure` should never be a silent no-op)
4. The proper fix (OS trust store / custom CA) is outside the scope of this CLI tool

## Alternatives Considered

- **Emit warning at runtime** — rejected; adds noise without solving the problem
- **Keep with deprecation notice** — rejected; the options were never functional on .NET 8+, so there's no real user workflow to preserve

## Backward Compatibility

Before the typed connection rewrite, `Identity.SecurityProtocol` and `Identity.Insecure` were
retained as nullable fields so legacy profile data could still deserialize. The identity/profile
model is now removed, and 2.x storage is not imported. See
[`2.x-to-3.x.md`](../docs/migrations/2.x-to-3.x.md#profiles-and-saved-connections).

## Historical Files Changed

- `src/dgt.power.common/Logic/Identities.cs` — computed properties removed
- `src/dgt.power.common/Logic/Identity.cs` — fields kept nullable + ignored on write
- `src/dgt.power.common/Logic/DataverseConnection.cs` — all `ServicePointManager` usage removed
- `src/modules/dgt.power.profile/Commands/CreateProfileSettings.cs` — options removed
- `src/modules/dgt.power.profile/Commands/CreateProfileCommand.cs` — no longer passes fields
- `src/modules/dgt.power.profile/Commands/ListProfileCommand.cs` — columns removed
- `tests/dgt.power.profile.tests/CreateProfileSettingsTests.cs` — protocol tests replaced
- `README.md` — `profile create` signature updated
