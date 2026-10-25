# `connection` — Authentication & environments

Manages connections to Dataverse environments.

| Command | Description |
|---------|-------------|
| `connection list` | List configured connections |
| `connection create <name> --url <url>` | Create an interactive user connection; `--tenant` is optional |
| `connection create <name> --url <url> --device-code` | Create a user connection using device-code authentication; `--tenant` is optional |
| `connection create <name> --url <url> --tenant <tenant> --client-id <id> --client-secret <secret>` | Create a service-principal connection and store the supplied secret |
| `connection create <name> --url <url> --tenant <tenant> --client-id <id> --certificate-thumbprint <thumbprint>` | Create a service-principal connection using a certificate in the CurrentUser store |
| `connection create <name> --url <url> --tenant <tenant> --client-id <id> --certificate-path <path> [--certificate-password <password>]` | Create a service-principal connection using a PFX file; omit the password for a passwordless file |
| `connection create <name> --azure-devops-federated --service-connection-name <name>` | Create a connection using Azure DevOps Workload Identity Federation (OIDC), resolving the URL/tenant/client/service-connection IDs automatically from the service connection name — no client secret required or stored |
| `connection create <name> --url <url> --azure-devops-federated --tenant <tenantId> --client-id <clientId> --service-connection-id <id>` | Same as above, with the tenant/client/service-connection IDs passed explicitly instead of resolved by name |
| `connection create ... --no-verify` | Skip Dataverse connectivity verification; user sign-in still applies |
| `connection select <name>` | Set the active connection |
| `connection delete <name>` | Delete a specific connection; its cached user account is removed only if no other connection refers to it |
| `connection delete --all` | Delete all connections and their unique cached user accounts |
| `connection status` | Check token acquisition for the selected connection without opening a browser (exit 0 = acquired or ad-hoc check skipped, 2 = authentication required/failed) |
| `connection refresh` | Force an interactive login for the selected user connection and save its authentication record |

Example:

```bash
dgtp connection create prod --url https://contoso.crm4.dynamics.com --tenant contoso.onmicrosoft.com
```

> **CI/CD pipelines:** see [CI/CD Integration](../../guides/ci-cd.md) for how to create
> connections non-interactively in Azure Pipelines, including Workload Identity Federation (OIDC)
> setups where no client secret is ever available.

Agent-friendly auth workflow:

```bash
dgtp connection status       # exit 0 = valid, exit 2 = login required
dgtp connection refresh      # re-authenticate interactively
dgtp connection status       # confirm valid before proceeding
```

`connection status` is intended as a pre-flight check for CI/automation before running other Dataverse commands. It never opens a browser and returns one of the following exit codes:

| Exit code | Meaning |
|-----------|---------|
| `0` | A token was acquired without interactive login, or the check was skipped for an ad-hoc connection string |
| `2` | Authentication is required or token acquisition failed; user connections may need sign-in, while service connections need their credentials or pipeline configuration checked |

This checks authentication, not Dataverse permissions or connectivity. `connection refresh` applies
only to interactive and device-code connections; it does not rotate service-principal credentials.

## Authentication types

Choose one authentication type when creating a saved connection:

| Type | Suitable for | Credential source | Sensitive data stored by dgtp |
|---|---|---|---|
| Interactive browser | Local development with a user account | Browser sign-in, then cached tokens | OS-protected user-token cache |
| Device code | SSH sessions or terminals without a local browser | Code entered in a browser on another device, then cached tokens | OS-protected user-token cache |
| Client secret | Service-principal authentication | `--client-secret <secret>` | Client secret in the protected secret store |
| Client certificate | Service-principal authentication without a client secret | CurrentUser certificate store or a PFX file | PFX password, if using a file; no certificate/private-key copy |
| Azure DevOps federation | Azure Pipelines without long-lived client secrets | Short-lived pipeline OIDC token | No client secret or certificate password |

### Interactive browser and device code

```bash
# Browser sign-in; omit --tenant to use the account's home tenant
dgtp connection create dev --url https://contoso-dev.crm4.dynamics.com

# Device-code sign-in; follow the displayed browser/code instructions
dgtp connection create remote-dev --url https://contoso-dev.crm4.dynamics.com --device-code
```

Both authenticate as the signed-in user and use that user's Dataverse permissions. An optional
`--tenant <tenant-id-or-domain>` selects a tenant explicitly, for example when using a guest account.
The connection stores an authentication record containing account identifiers, including the username,
but no tokens. Tokens are kept separately in the shared, OS-protected Azure.Identity cache.

Later commands reuse cached authentication when possible. With `--non-interactive` or
`DGTP_NON_INTERACTIVE`, dgtp does not start browser/device authentication; if sign-in is needed,
authenticate separately with `dgtp connection refresh`.

### Client secret

```bash
dgtp connection create test-spn --url https://contoso-test.crm4.dynamics.com --tenant <tenant-id> --client-id <application-id> --client-secret <secret>
```

`--client-secret` requires a value; creation does not prompt. The secret is stored separately from `connections.json`, associated with the
connection name. At runtime, dgtp retrieves it and uses the configured tenant and application ID to
acquire a token. If the secret expires or is rotated, recreate the connection with the new secret.

The Entra application must have a Dataverse application user with the required security roles.
Client-secret and certificate connections can be created unattended with these options.
Supply values through your pipeline's secret management and masking, and avoid echoing commands
or enabling logging that reveals them. CLI secrets/passwords may be exposed in process arguments,
shell history, or pipeline logs; neither argument expansion nor environment variables alone
guarantee confidentiality. For pipelines, federation avoids long-lived secrets; see
[CI/CD Integration](../../guides/ci-cd.md).

### Client certificate: certificate store or PFX file

Both alternatives authenticate as an Entra application. Register the certificate's **public
certificate** on that application, and configure its Dataverse application user/security roles.
The certificate available to dgtp must include an accessible **private key**; a public-only
certificate cannot authenticate.

```powershell
# Certificate already imported into the running user's Personal (My) store
dgtp connection create prod-store --url https://contoso.crm4.dynamics.com --tenant <tenant-id> --client-id <application-id> --certificate-thumbprint <thumbprint>

# Certificate and private key contained in a PFX file
dgtp connection create prod-file --url https://contoso.crm4.dynamics.com --tenant <tenant-id> --client-id <application-id> --certificate-path "C:\certificates\dataverse.pfx" --certificate-password <password>
```

| | Thumbprint | PFX file |
|---|---|---|
| Certificate location | CurrentUser Personal (`My`) certificate store; not LocalMachine | Referenced PFX file |
| Saved metadata | Thumbprint | File path |
| Password handling | No PFX password is requested or stored by dgtp; any import password was used when installing the certificate | Supply `--certificate-password <password>`; omit it for a passwordless PFX. No prompt occurs |
| Runtime requirement | The running user can access the certificate's private key | The file remains accessible at the saved path and its password is available in dgtp's protected secret store |

For PFX connections, **the private key stays in the original file; only the password is stored by
dgtp**. The key is loaded ephemerally rather than persistently imported into a certificate store.
Protect the PFX file with suitable filesystem permissions. For thumbprint connections, the OS
cryptographic provider controls private-key access; hardware-backed providers may require a PIN
or user interaction.

The private key signs a client assertion; neither it nor the PFX password is sent to Entra ID.
When replacing a certificate, update the Entra application's registration and recreate the
connection if its thumbprint, path, or password changes.

### Azure DevOps Workload Identity Federation

```bash
dgtp connection create pipeline --azure-devops-federated --service-connection-name PowerPlatform-Production
```

This runs in Azure Pipelines with an authorized, federated Power Platform service connection.
dgtp resolves its Dataverse URL, tenant, application ID, and service-connection ID, then exchanges
the job's short-lived OIDC token for an access token. Only those identifiers are saved; no
long-lived client secret is required. A saved definition alone cannot authenticate outside the
pipeline context. See [CI/CD Integration](../../guides/ci-cd.md#workload-identity-federation-oidc-service-connection)
for required environment variables and the explicit-ID alternative.

### One-off connection strings

For auth modes/options outside these saved types, use `--connection-string` or
`DGTP_CONNECTION_STRING`. These are passed to the Dataverse SDK and are not saved as a connection
type. Prefer the environment variable for secret-bearing strings to avoid exposing them in
command-line arguments or shell history. `connection status` skips their authentication check;
successful status does not verify their credentials.
