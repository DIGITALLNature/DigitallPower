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

## Design rationale and storage boundaries

The home directory is independent of assembly identity, version, and installation path.
Local rather than roaming application data is used because protected secrets and token caches
are machine-bound. `DGTP_HOME` relocates connection metadata, application state, and secret
storage; it does not relocate Azure.Identity's shared token cache. See
`research-persistent-msal-token-cache-removal.md` for that cache's location and platform settings.

`ConnectionStore` preserves name casing and uses ordinal case-insensitive lookup. Mutations
take an exclusive `connections.lock`, reload the document, and write through a unique temporary
file that is flushed to disk before replacement. Unsupported schema versions and invalid JSON
raise errors rather than resetting saved state. The document uses `schemaVersion: 1` and
polymorphic `type` discriminators; its schema is `schemas/connection/v1/schema.json`.

Metadata contains URLs, tenant/client IDs, certificate references, and user authentication records,
but no client secrets, PFX passwords, or token blobs. Authentication records contain account
identifiers, including the username, so metadata is not anonymous. Routine token acquisition
updates Azure.Identity's cache rather than rewriting connection metadata.

`SecretStore` uses MSAL Extensions storage directly for a single protected JSON payload indexed
by connection name and secret key, with a separate exclusive lock. Windows uses DPAPI;
macOS uses Keychain service `dgtp` / account `secrets`; Linux uses Secret Service schema
`com.digitall.dgtp`. Linux's explicit unencrypted-storage opt-in selects an unprotected secret
file; user-token cache fallback follows Azure.Identity's protected-first behavior instead.

Rejected alternatives:

| Alternative | Reason |
|---|---|
| Pin assembly version and retain isolated storage | Still depends on assembly name/signing key and retains mixed metadata, secrets, and tokens |
| Custom MSAL authentication and token persistence | Duplicates Azure.Identity's credential flows and cache management |
| Persist arbitrary connection strings | Opaque auth/options and secret-bearing values undermine typed validation and metadata separation; ad-hoc strings cover unsupported scenarios |
| One user-token cache per connection | Prevents account/token sharing and complicates cleanup |
| ASP.NET DataProtection as a cross-platform secret vault | Its default non-Windows keys are stored unencrypted |
| Additional keychain libraries | Unnecessary native interop/dependency surface when MSAL Extensions already provides platform storage |

Legacy storage is neither imported nor deleted. Mapping opaque connection strings to typed
records and supporting both old decryption schemes would add substantial one-time migration
complexity. Historical storage scoping and recoverability are documented in
`research-isolated-storage-major-version-scoping.md`.

## Authentication and invocation

Connection definition discriminators are `interactive`, `deviceCode`, `clientSecret`,
`clientCertificate`, and `azureDevOpsFederated`. Interactive and device-code records contain an
Azure.Identity authentication record; client secrets and PFX passwords are stored separately.
Certificate thumbprints refer to the CurrentUser certificate store.

Certificate resolution uses the CurrentUser `My` store for thumbprints, or
`X509CertificateLoader.LoadPkcs12FromFile` with `EphemeralKeySet` for PFX paths. The private key
remains in the store/file; only a file's password is managed by `SecretStore`, under
`certificatePassword` for the connection name. Thumbprint lookup does not consume a PFX password.
`CredentialFactory` currently enables persistent token caching for certificate credentials as
well as user credentials; client-secret credentials use the default in-memory caching behavior.

`CheckAuthAsync` attempts token acquisition for every saved auth type; it is not a Dataverse
permission/connectivity check. Ad-hoc strings skip this check. `RefreshAuthAsync` only reauthenticates
user connections, not service-principal credentials. The user-facing explanation and creation
examples for all auth types are in README's "Authentication types" section.

Creation takes `--client-secret <secret>` directly and an optional
`--certificate-password <password>` only with `--certificate-path`. Neither flow prompts.
An omitted PFX password stages an empty password, replacing any previously stored password
for that connection. Both inputs remain outside connection metadata and use the existing
verification/rollback path. CLI values favor unattended creation; callers must protect process
arguments, shell history, and pipeline logs. User authentication still requires sign-in during
creation even with `--no-verify`.

`--connection` / `DGTP_CONNECTION` select a named connection. `--connection-string` /
`DGTP_CONNECTION_STRING` provide a non-persisted fallback and take precedence over a named
connection. `--non-interactive` / `DGTP_NON_INTERACTIVE` prevent browser/device authentication.
Unencrypted storage is opt-in via `DGTP_ALLOW_UNENCRYPTED_STORAGE`; a warning is emitted only when an unencrypted backend is selected.

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

Deleting one connection removes its cached account only when no remaining connection references
the same home account ID; deleting all connections removes each unique referenced user account.

User sign-in records allow an omitted tenant ID. Azure.Identity then authenticates to the user's
home tenant; service-principal and explicit federation connections still require a tenant.
The CLI uses the MSAL-aligned `--client-id` spelling and binds invocation options from parsed
`BaseProgramSettings`, not by scanning raw command-line arguments.

`IDataverseConnection` is the application-owned connection abstraction. External SDK terminology
such as `Microsoft.Xrm.Sdk` remains unchanged.

## Verification boundaries

Connection-module tests cover command behavior, verification rollback, account-scoped deletion,
selected storage round-trips, invocation capture, and a Windows protected-secret-storage check.
CLI command-tree/settings tests live separately in `tests/dgt.power.cli.tests`.

These tests are not exhaustive authentication or platform-storage coverage. Credential construction,
connection/auth resolution, persistent cache removal, keyring availability, concurrent writers,
and cross-platform permissions require additional targeted coverage. Live authentication and
survival across tool updates must not be inferred solely from fake-backed command tests.
