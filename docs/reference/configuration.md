# Configuration

The former global `dgtp.json` configuration and `dgtp:*` environment-variable binding have been removed. Commands accept their own options and configuration files. For example, `maintenance bulkdelete` accepts `--poll-interval` (seconds; default `5`) to control how often it checks the asynchronous job.

Saved connections and application state use a stable per-user data directory instead of assembly-scoped isolated storage. Set `DGTP_HOME` to override its location. Connections are stored in `connections.json`; client secrets and PFX passwords are stored separately using the platform's protected storage (DPAPI on Windows, Keychain on macOS, and Secret Service on Linux). No 2.x connections are migrated automatically.

For the removed `profile` commands and connection recreation steps, see
[Migrating from 2.x to 3.x](../guides/migrations/2.x-to-3.x.md#profiles-and-saved-connections).

For interactive and device-code sign-in, `--tenant` is optional: if omitted, authentication targets the user's home tenant. Service-principal and explicitly configured Azure DevOps federated connections require a tenant ID.

## Global connection/authentication environment variables

| Variable | Purpose |
|---|---|
| `DGTP_CONNECTION` | Name of the connection to use (overrides the saved current selection) |
| `DGTP_CONNECTION_STRING` | One-off connection string; never persisted |
| `DGTP_NON_INTERACTIVE` | Disable interactive authentication when set to a truthy value |
| `DGTP_ALLOW_UNENCRYPTED_STORAGE` | Explicitly allow unencrypted token/secret storage when a Linux keyring is unavailable; warns when that backend is selected |

`--connection` and `--connection-string` override their corresponding environment variables. A command-line `--connection-string` or `DGTP_CONNECTION_STRING` takes precedence over the named connection.

## JSON schemas

JSON schemas for the various configuration files used by the modules live under [`schemas/`](../../schemas) and can be referenced from your own config files via the `$schema` property for autocomplete in modern editors.

## Shared configuration formats

Some configuration file formats are shared across multiple commands:

| Format | Used by | Reference |
|--------|---------|-----------|
| Portable owner mappings | `process`, `duplicaterules` | [owner-mappings.md](owner-mappings.md) |
