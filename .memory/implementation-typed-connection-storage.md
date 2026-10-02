# Implementation: Typed, Version-Stable Connection Storage

## Persistent locations

`DgtpHome` selects the per-user data directory: `DGTP_HOME` overrides it; Linux otherwise uses
`XDG_DATA_HOME/dgtp` or `~/.local/share/dgtp`, and other platforms use
`LocalApplicationData/dgtp`. The directory is restricted to the current user on Unix.

- `connections.json` stores schema-versioned typed connection definitions and the current selection.
- `state.json` stores telemetry and version-check state.
- `secrets/secrets.bin` stores client secrets and PFX passwords through
  `Microsoft.Identity.Client.Extensions.Msal` platform storage.
- Azure.Identity user-credential token caches use the persistent cache name `dgtp`.

No 2.x isolated-storage migration is performed. Recreate stored connections after upgrading.

## Authentication and invocation

Connection definition discriminators are `interactive`, `deviceCode`, `clientSecret`,
`clientCertificate`, and `azureDevOpsFederated`. Interactive and device-code records contain an
Azure.Identity authentication record; client secrets and PFX passwords are stored separately.
Certificate thumbprints refer to the CurrentUser certificate store.

`--connection` / `DGTP_CONNECTION` select a named connection. `--connection-string` /
`DGTP_CONNECTION_STRING` provide a non-persisted fallback and take precedence over a named
connection. `--non-interactive` / `DGTP_NON_INTERACTIVE` prevent browser/device authentication.
Unencrypted storage is opt-in via `DGTP_ALLOW_UNENCRYPTED_STORAGE` and emits a startup warning.

`DGTP_TELEMETRY_OPTOUT` or `DO_NOT_TRACK` disables telemetry. TSL controls use the `DGTP_TSL_*`
prefix; the former `dgtp.json` and `dgtp:*` configuration binding are removed.

## Mutation and command behavior

`CreateConnectionCommand` stages secrets, verifies the candidate with a Dataverse `WhoAmI` request,
then persists the connection. Verification failures restore staged secrets and leave existing
metadata/current selection intact. `--no-verify` explicitly bypasses this Dataverse check.

The only poll interval is `maintenance bulkdelete --poll-interval <seconds>` (default 5); it is
validated as positive and does not apply to other commands.

The `profile` branch and legacy profile services are removed. `connection` is the sole saved
connection command branch.

`connection logout <name>` removes only the matching account from Azure.Identity's shared
persistent MSAL cache and retains the connection definition. Deleting one connection removes its
cached account only when no remaining connection references the same home account ID; deleting all
connections removes each unique referenced user account.

## Design reference

`CONNECTION-STORAGE-DESIGN.md` records the rationale, JSON examples, security trade-offs, and the
intentional lack of migration from legacy isolated storage.
