# Typed, Version-Stable Connection Storage

## Context

The 2.x profile model stored opaque connection strings and token blobs in .NET isolated storage. `IsolatedStorageFile.GetUserStoreForAssembly()` derives its directory from the strong-name public key, the assembly **major** version, and the assembly name, so every major release silently started with an empty store.

## Decision

Replace profiles with typed connection definitions in a stable per-user home directory (`DgtpHome`). Metadata (`connections.json`, `schemas/connection/v1`) and application state (`state.json`) are separate schema-versioned JSON files; secrets live in a protected store provided by `Microsoft.Identity.Client.Extensions.Msal` (Windows DPAPI, macOS Keychain, Linux Secret Service); user token caches use Azure.Identity's persistent cache named `dgtp`. Legacy storage is neither imported nor deleted.

## Rationale

- Local, not roaming, application data is used because protected secrets and token caches are machine-bound.
- Metadata contains no secrets, but authentication records include account identifiers, so it is not anonymous.
- `DGTP_HOME` relocates metadata, state, and secrets, but not Azure.Identity's shared token cache.
- Importing legacy data would require mapping opaque strings to typed records and supporting both old decryption schemes (DPAPI with a fixed entropy; ASP.NET DataProtection with unencrypted keys on macOS/Linux) for one-time value. Old stores remain recoverable on disk.

## Alternatives rejected

| Alternative | Reason |
|---|---|
| Pin the assembly version and keep isolated storage | Still depends on assembly name and signing key and mixes metadata, secrets, and tokens |
| Custom MSAL authentication and token persistence | Duplicates Azure.Identity flows and cache management |
| Persist arbitrary connection strings | Opaque auth and secret-bearing values undermine typed validation; strings remain available as a non-persisted fallback |
| One token cache per connection | Prevents account sharing and complicates cleanup |
| ASP.NET DataProtection as secret vault | Default non-Windows keys are unencrypted |
| Additional keychain libraries | MSAL Extensions already provides platform storage |

## Consequences

- Token-cache removal must target the same cache Azure.Identity uses: the filename is the configured name plus `.nocae` (`dgtp.nocae`) under `LocalApplicationData/.IdentityService`; macOS uses Keychain service `Microsoft.Developer.IdentityService`; Linux uses Secret Service schema `msal.cache`, collection `default`. When unencrypted storage is allowed, Azure.Identity tries protected storage first, so removal must do the same. Remove only the account matching `AuthenticationRecord.HomeAccountId`, never the whole cache.
- `schemaVersion` guards file formats; incompatible changes need an explicit migration. Binaries predating the contract must not write the new state format.
- Do not use isolated storage for anything that must survive updates.
- Tests exercise fake-backed command behavior; live authentication, keyring availability, concurrent writers, and cross-platform permissions need separate targeted coverage.
