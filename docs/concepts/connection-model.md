# Connection Model

How dgtp manages Dataverse connections, authentication, and credential storage.

## Named connections

A **connection** is a named, stored authentication configuration. You create one with
[`dgtp connection create`](../reference/commands/connection.md) and select it with
`dgtp connection select`. Every subsequent command operates against the selected connection.

Connections are stored in `connections.json` in the per-user dgtp home directory (see
[Persistent application state](telemetry-and-privacy.md#persistent-application-state)).

## Connection resolution order

Every command that talks to Dataverse resolves its connection in this order:

1. **Command-line `--connection-string`** — a one-off connection string passed directly; never persisted
2. **`DGTP_CONNECTION_STRING` environment variable** — same, via environment; preferred for CI to avoid secrets in command lines
3. **Command-line `--connection <name>`** — overrides the saved current selection
4. **`DGTP_CONNECTION` environment variable** — same, via environment
5. **Selected connection** — the connection set with `dgtp connection select`
6. Otherwise the command fails with a missing-connection error

## Authentication types

| Type | Use case | What's stored |
|------|----------|---------------|
| Interactive browser | Local development | OS-protected token cache |
| Device code | SSH / headless terminals | OS-protected token cache |
| Client secret | Service principal | Secret in OS-protected store |
| Client certificate | Service principal without secret | PFX password (if any) in protected store |
| Azure DevOps federation | CI pipelines | No secrets — uses pipeline OIDC token |

See [`connection` command reference](../reference/commands/connection.md#authentication-types) for detailed setup instructions.

## Credential storage

- **Tokens** are cached by Azure.Identity in the OS-protected token cache (DPAPI on Windows, Keychain on macOS, Secret Service on Linux).
- **Client secrets** and **PFX passwords** are stored separately from `connections.json` using the platform's protected storage.
- **Connection metadata** (name, URL, tenant, client ID, auth type) is stored in plain JSON in `connections.json` — it contains no secrets.
- Set `DGTP_ALLOW_UNENCRYPTED_STORAGE` to explicitly allow unencrypted storage when a Linux keyring is unavailable; dgtp warns when that backend is selected.

## Non-interactive mode

Set `DGTP_NON_INTERACTIVE` or pass `--non-interactive` to prevent browser/device-code authentication. If sign-in is needed, authenticate separately with `dgtp connection refresh`.

## CI/CD

For pipeline authentication, see [CI/CD integration](../guides/ci-cd.md). The recommended approach is Workload Identity Federation, which requires no long-lived secrets.
