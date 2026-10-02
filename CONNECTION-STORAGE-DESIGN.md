# Design: Version-Stable Connection Storage

| | |
|---|---|
| **Status** | Proposed |
| **Scope** | `dgt.power.connection`, `dgt.power.common/Logic` (connection + auth), `dgt.power` host (storage consumers) |
| **Target** | 3.0.0 (before GA) |

---

## 1. Problem

Every major release of `dgtp` loses all stored connections. Users must recreate every connection and log in again.

### Root cause

Connections are stored in `identities.dat` inside `IsolatedStorageFile.GetUserStoreForAssembly()`
(`ProfileManager`, `Program.cs`). For strong-named assemblies (`SignAssembly=True` in
`Directory.Build.props`), .NET derives the store directory from
`IdentityHelper.GetNormalizedStrongNameHash`, which hashes:

```text
public key + AssemblyName.Version.Major + AssemblyName.Name
```

`AssemblyVersion` is derived from `<Version>`, so each new **major** version gets a new, empty
store directory. Minor and patch updates keep the same store, which is why only major updates are
affected.

These consumers use the same store and lose their state too:

| Consumer | Data | Effect of loss |
|---|---|---|
| `ProfileManager` | `identities.dat` (connections, MSAL token blobs) | All connections lost |
| `TelemetryConfig` | install ID | New anonymous install ID per major |
| `TelemetryNotice` | first-run marker | Telemetry notice shown again |
| `VersionCheckInterceptor` | version-check state | Check state reset |
| `ProfileNamesProvider` | reads `identities.dat` | Completion empty after update |

### Secondary issues in the current design

- **Tokens are stored inside the connection record.** `TokenIdentity.Token` contains a full
  serialized MSAL cache for each connection, and `TokenConnector` rewrites the whole
  `identities.dat` on every token refresh.
- **Secrets and metadata are mixed.** `Identity.ConnectionString` stores client secrets/passwords
  in plaintext inside the encrypted blob. Listing or completing connection names requires
  decrypting every secret.
- **Weak protection on non-Windows.** On macOS/Linux, `ProfileManager` uses
  `DataProtectionProvider.Create("dgtp")`. Its keys are stored **unencrypted** in
  `~/.aspnet/DataProtection-Keys`, so the protection is only obfuscation.
- **Fixed DPAPI entropy** (`ProfileEnv.Seed`) is in the source code. It is harmless, but it adds
  no security.

---

## 2. Goals

1. Connections and login state survive **any** tool update (major, minor, reinstall, install path change).
2. Non-sensitive connection metadata is stored separately from secrets and tokens.
3. MSAL tokens are persisted by MSAL/Azure.Identity, not by our own code.
4. Secrets are protected by the OS (Windows DPAPI, macOS Keychain, Linux Secret Service).
5. CI and headless scenarios work without a keychain and without storing secrets.
6. Starting with 3.0, connections survive all future updates. Importing connections from 1.x/2.x is
   **out of scope for the first version** (see §10).
7. Stored connections are **explicitly typed** (see §7). Each type has validated fields and a
   defined secret-handling path. Arbitrary connection strings are supported only as a
   non-persisted fallback.

## 3. Non-goals

- A custom token cache format.
- A general-purpose cross-platform secret vault.
- Replacing pipeline secret stores or Azure Key Vault in CI.
- Silently storing unencrypted secrets.

---

## 4. Overview

```text
+-------------------------------------------------------------------+
| dgtp home  (DGTP_HOME or <LocalApplicationData>/dgtp)             |
|                                                                   |
|   connections.json   metadata only, no secrets                    |
|   state.json         install id, telemetry notice, version check  |
+-------------------------------------------------------------------+
            | references by connection name
            v
+-----------------------------+   +---------------------------------+
| Secret store (ISecretStore) |   | Token cache (Azure.Identity)    |
| DPAPI / Keychain / libsecret|   | TokenCachePersistenceOptions    |
| client secrets,             |   | name "dgtp", shared by all      |
| PFX passwords               |   | user-based connections          |
|                             |   | DPAPI / Keychain / libsecret    |
+-----------------------------+   +---------------------------------+
            \                              /
             v                            v
        +-----------------------------------------+
        | CredentialFactory -> Azure.Core         |
        |   TokenCredential per connection type   |
        +-----------------------------------------+
                          |
                          v
        ServiceClient(uri, tokenProviderFunction)
```

Main decisions:

- **One stable home directory** replaces isolated storage for all `dgtp` state.
- **Every connection becomes an `Azure.Core.TokenCredential`.** `Azure.Identity` is already a
  dependency (`AzurePipelinesCredential`). It provides persistent, OS-protected token caching
  through `TokenCachePersistenceOptions` and supports all required flows. The custom MSAL code in
  `TokenConnector` is removed.
- **Secrets are stored per connection name** in the OS store and are never written to `connections.json`.

---

## 5. Storage location

```csharp
public static string GetHome() =>
    Environment.GetEnvironmentVariable("DGTP_HOME") is { Length: > 0 } custom
        ? custom
        : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dgtp");
```

| OS | Default path |
|---|---|
| Windows | `%LOCALAPPDATA%\dgtp` |
| macOS | `~/Library/Application Support/dgtp` |
| Linux | `$XDG_DATA_HOME/dgtp`, falling back to `~/.local/share/dgtp` |

Rationale:

- The path depends on neither the version, the assembly identity nor the install path.
- `LocalApplicationData` rather than roaming `ApplicationData`: token caches and DPAPI secrets are
  machine-bound and must not roam.
- `DGTP_HOME` supports isolated CI agents, tests and containers with a mounted volume.
- On Unix the directory is created with mode `0700` and files with `0600`.

Telemetry install ID, telemetry notice marker and version-check state move to `state.json`. Legacy
values are not imported: 3.0 generates a new install ID once, as every previous major did.

---

## 6. `connections.json`

### Format

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/connection/v1/schema.json",
  "schemaVersion": 1,
  "current": "dev",
  "connections": {
    "dev": {
      "type": "interactive",
      "url": "https://contoso-dev.crm4.dynamics.com",
      "tenantId": "00000000-0000-0000-0000-000000000000",
      "authenticationRecord": {
        "username": "jane@contoso.com",
        "authority": "login.microsoftonline.com",
        "homeAccountId": "uid.utid",
        "tenantId": "00000000-0000-0000-0000-000000000000",
        "clientId": "51f81489-12ee-4a9e-aaae-a2591f45987d"
      }
    },
    "test-spn": {
      "type": "clientSecret",
      "url": "https://contoso-test.crm4.dynamics.com",
      "tenantId": "00000000-0000-0000-0000-000000000000",
      "clientId": "22222222-2222-2222-2222-222222222222"
    },
    "prod-cert": {
      "type": "clientCertificate",
      "url": "https://contoso.crm4.dynamics.com",
      "tenantId": "00000000-0000-0000-0000-000000000000",
      "clientId": "33333333-3333-3333-3333-333333333333",
      "certificate": { "thumbprint": "ABCDEF…", "storeLocation": "CurrentUser" }
    },
    "pipeline": {
      "type": "azureDevOpsFederated",
      "url": "https://contoso.crm4.dynamics.com",
      "tenantId": "00000000-0000-0000-0000-000000000000",
      "clientId": "44444444-4444-4444-4444-444444444444",
      "serviceConnectionId": "55555555-5555-5555-5555-555555555555"
    }
  }
}
```

### Rules

- **No secrets.** The file contains no tokens, client secrets, passwords or secret-bearing
  connection strings. Secrets of `clientSecret` and `clientCertificate` (PFX password) connections
  live in the secret store, keyed by connection name. A unit test enforces this (see §12).
- **Case-insensitive names.** Names are stored as entered and compared with
  `StringComparer.OrdinalIgnoreCase`. This replaces the `ToUpperInvariant()` normalization in
  `Identities`, which made `connection list` show upper-cased names.
- **Atomic writes.** Write to `connections.json.tmp`, then `File.Move(tmp, target, overwrite: true)`.
  A crash cannot leave a truncated file.
- **Concurrency.** Writes take an exclusive lock on `connections.lock` via `FileShare.None`, then
  reload, apply the change and write. Two parallel `dgtp` processes (common in pipelines) cannot
  overwrite each other's changes.
- **Not rewritten during token refresh.** Token refresh only touches the Azure.Identity cache.
  `connections.json` changes only through `connection` commands and the first interactive login,
  which stores the `authenticationRecord`.
- **JSON schema.** Add `schemas/connection/v1/schema.json` per the repository's schema rules.
  `schemaVersion` allows future in-place upgrades.

---

## 7. Connection types and credentials

All types are created by a single `CredentialFactory`:

```csharp
internal interface ICredentialFactory
{
    TokenCredential Create(ConnectionDefinition connection, CredentialContext context);
}
```

| `type` | Azure.Identity credential | Stored secret | Token cache |
|---|---|---|---|
| `interactive` | `InteractiveBrowserCredential` | none | persistent, shared |
| `deviceCode` | `DeviceCodeCredential` | none | persistent, shared |
| `clientSecret` | `ClientSecretCredential` | client secret | in-memory (cheap to reacquire) |
| `clientCertificate` | `ClientCertificateCredential` | optional PFX password | in-memory |
| `azureDevOpsFederated` | `AzurePipelinesCredential` (existing) | none | in-memory |

There is deliberately **no** `connectionString` type. A connection string can encode any
`ServiceClient` auth mode and option, so it cannot be validated, stored without secrets, or
exported in a predictable way. Raw connection strings are only supported ad hoc (see
"Ad-hoc connection strings" below).

Dataverse is called through the existing token-provider constructor of `ServiceClient`:

```csharp
var scope = new TokenRequestContext([$"{url.GetLeftPart(UriPartial.Authority)}/.default"]);
var client = new ServiceClient(url, async _ =>
    (await credential.GetTokenAsync(scope, cancellationToken)).Token);
```

### User-based connections (`interactive`, `deviceCode`)

```csharp
var options = new InteractiveBrowserCredentialOptions
{
    ClientId = DataverseClientId,                  // 51f81489-12ee-4a9e-aaae-a2591f45987d (unchanged)
    TenantId = connection.TenantId,                // required, see below
    RedirectUri = new Uri("http://localhost"),
    AuthenticationRecord = connection.AuthenticationRecord,
    DisableAutomaticAuthentication = context.NonInteractive,
    TokenCachePersistenceOptions = new TokenCachePersistenceOptions
    {
        Name = "dgtp",
        UnsafeAllowUnencryptedStorage = context.AllowUnencryptedStorage,
    },
};
```

- **Tenant is required.** `connection create --url … --tenant …` accepts a tenant GUID or a
  verified domain (e.g. `contoso.onmicrosoft.com`). An explicit tenant makes the login
  deterministic, including for guest accounts. No `organizations` default and no tenant discovery.
- `connection create --url … --tenant …` runs `AuthenticateAsync()` once and stores the returned
  `AuthenticationRecord` (username, home account ID, tenant, client ID; it contains no secrets) in
  `connections.json`.
- Later runs pass the record, so Azure.Identity selects the correct account from the shared cache
  and acquires tokens silently.
- **One shared cache, not one per connection.** Several connections for the same user and tenant
  share one refresh token, so a single login covers all of them.
- **Non-interactive mode** (`--non-interactive` / `DGTP_NON_INTERACTIVE`) sets
  `DisableAutomaticAuthentication = true`. Azure.Identity then throws
  `AuthenticationRequiredException`, which we map to the existing
  `InteractiveLoginRequiredException` and exit code `2`. `connection status` and
  `connection refresh` keep their current behaviour and contract.
- `deviceCode` is new and useful for SSH sessions and dev containers without a browser.

### Service principals

- `clientSecret`: the secret is always stored in the secret store (§8). There is no option that
  references an environment variable instead. CI should use `azureDevOpsFederated` or the ad-hoc
  `DGTP_CONNECTION_STRING` (below), so that no secret is stored on build agents.
- `clientCertificate`: use a thumbprint from the OS certificate store, or a PFX path. The optional
  PFX password goes into the secret store. Never store the private key ourselves.

### Ad-hoc connection strings (fallback, never persisted)

For exotic scenarios that no typed connection covers, a raw Dataverse connection string can be
supplied **for a single invocation**. It is passed unchanged to `ServiceClient` (existing
`CrmConnector`) and is never written to disk.

```bash
# interactive one-off
dgtp <command> … --connection-string "AuthType=…;Url=…;…"

# CI: preferred, because the value does not appear in the process list, shell history or logs
export DGTP_CONNECTION_STRING="AuthType=ClientSecret;Url=…;ClientId=…;ClientSecret=…"
dgtp <command> …
```

Rules:

- **Global option.** `--connection-string` is accepted by every command that connects to Dataverse.
  It replaces today's `xrm:connection` configuration key (`--xrm:connection`, `dgtp:xrm:connection`,
  `dgtp.json`), which is **removed** in 3.0 (see §10a).
- **Precedence:** `--connection-string` → `DGTP_CONNECTION_STRING` → `--connection <name>` →
  `DGTP_CONNECTION` → current stored connection.
- **Visible but redacted.** On use, print `Using ad-hoc connection string (not persisted)` together
  with the parsed URL. Never print the string itself.
- **No interaction with stored state:** no token cache, secret store, or `current` changes.
  `connection status/refresh` report that an ad-hoc connection is active and do nothing else.
- **Non-interactive mode** still applies. If the string's auth mode needs a prompt, `ServiceClient`
  fails and the error is surfaced unchanged.

> **To verify during implementation:** Spectre.Console.Cli rejects unknown options by default.
> Global options (`--connection`, `--connection-string`, `--non-interactive`) therefore need a
> shared base `CommandSettings` or must be removed from `args` before command parsing.

### `connection create --connection-string` is removed

`connection create` no longer accepts `--connection-string`. Users create typed connections
(`--client-secret`, `--certificate-*`, `--azure-devops-federated`, `--url --tenant`)
instead. This is a **breaking change** for 3.0 and needs a migration note in the README, especially
for the documented Azure DevOps client-secret pipeline flow. That flow switches to
`DGTP_CONNECTION_STRING` or, preferably, to Workload Identity Federation.

A helpful error is kept for 3.x: if `--connection-string` is passed to `connection create`,
`dgtp` parses the `AuthType` and prints the equivalent typed command. Secret values are replaced by
placeholders.

---

## 8. Secret store

```csharp
public interface ISecretStore
{
    string? Get(string connectionName, string key);
    void Set(string connectionName, string key, string value);
    void Delete(string connectionName);   // removes all secrets of a connection
}
```

### Implementation

Use the cross-platform storage primitive in `Microsoft.Identity.Client.Extensions.Msal`, which
Azure.Identity already uses for its token cache. It adds no new dependency and has the same OS
coverage as the token cache:

| OS | Backend |
|---|---|
| Windows | DPAPI (CurrentUser) encrypted file in `DGTP_HOME/secrets/` |
| macOS | Keychain (service `dgtp`, account = `<connection>:<key>`) |
| Linux | Secret Service / libsecret (schema `com.digitall.dgtp`, attributes: connection, key) |

> **To verify during implementation:** confirm that the MSAL extensions storage class
> (`Storage` / `StorageCreationPropertiesBuilder`) is public and suitable for arbitrary payloads.
> **Fallback:** store a single protected JSON blob (`secrets.bin`) that maps
> `connection:key → value`. Protect it with DPAPI on Windows; on macOS/Linux store only a random
> AES key in Keychain/libsecret and encrypt the file with AES-GCM.

### Linux without a keyring (headless servers, WSL, containers)

Do not silently fall back to plaintext. Instead:

1. Fail with a clear message that lists alternatives: certificate, federated identity,
   `DGTP_CONNECTION_STRING`, or running a keyring.
2. Allow explicit opt-in with `DGTP_ALLOW_UNENCRYPTED_STORAGE=true`. This writes the secret and
   token cache to a `0600` file, maps to `UnsafeAllowUnencryptedStorage`, and prints a warning on
   every use. The Azure CLI and `pac` behave the same way.

---

## 9. Command surface

The `connection` branch keeps its verbs. Only the creation options grow:

```bash
# user login (browser, default) / device code; --tenant is required
dgtp connection create dev  --url https://contoso-dev.crm4.dynamics.com --tenant <id|domain> [--device-code]

# service principal, secret prompted securely and stored in the secret store
dgtp connection create test --url … --tenant <id> --application-id <id> --client-secret

# certificate
dgtp connection create prod --url … --tenant <id> --application-id <id> --certificate-thumbprint <tp>
dgtp connection create prod --url … --tenant <id> --application-id <id> --certificate-path cert.pfx

# Azure DevOps WIF (unchanged)
dgtp connection create pipe   --azure-devops-federated --service-connection-name <name>

dgtp connection list | select | delete [--all] | status | refresh
dgtp connection logout <name>                           # new: removes account from token cache
```

- `--client-secret` without a value prompts with `IAnsiConsole.Prompt(new TextPrompt<string>(…).Secret())`.
  Secrets passed as plain CLI arguments end up in shell history; the help text recommends the
  prompt.
- `delete` removes metadata **and** all secret-store entries of the connection. For user
  connections it removes the account from the token cache only if no other connection references
  the same `homeAccountId`.
- Removed in 3.0:
  - the deprecated `profile` branch (`profile list/create/delete/select/purge`); `connection` is the
    only entry point;
  - `connection create --connection-string` (see §7). Raw connection strings are only accepted
    through the global, non-persisted `--connection-string` option and `DGTP_CONNECTION_STRING`.

---

## 10. No migration from isolated storage (3.0)

3.0 does **not** import connections from 1.x/2.x. Users recreate their connections once with the
new typed `connection create` commands. Reasons: the old connection-string profiles would have to
be re-typed anyway (§7), every previous major already required recreating connections, and
migration adds a lot of code (store location, two decryption schemes, mapping) for a one-time
benefit. Migration can be added later as a nice-to-have (§15).

What users see:

- If no connection exists and a command needs one, the existing "missing connection" error adds a
  hint: `Connections from dgtp 2.x are not migrated. Recreate them with 'dgtp connection create'.`
- The old isolated-storage files are neither read nor deleted. The README explains where they are
  (`%LOCALAPPDATA%\IsolatedStorage` / `<LocalApplicationData>/IsolatedStorage`) for users who want
  to remove them manually.

---

## 10a. Environment variables and global options

As part of 3.0, the generic configuration layering is **removed**:

- `dgtp.json` (`AddJsonFile`)
- `dgtp:`-prefixed environment variables (`AddEnvironmentVariables("dgtp:")`)
- generic `--key:value` command-line configuration (`AddCommandLine`)

Reasons: this layer was rarely used and few users know about it; it invites secrets in a
committed file; the `dgtp:` names cannot be set in bash; and it hides which settings exist at all.
It is replaced by a **fixed, documented set** of environment variables, each paired with a real
Spectre option where useful.

### Conventions

- Prefix `DGTP_`, upper snake case.
- A command-line option always beats the env var.
- All booleans are parsed by one helper (`ExecutionEnvironment.IsTruthy`: `1` / `true` / `yes`).
- All names are defined in one class (`DgtpEnvironment`) and listed in one README table.
- External variables we only read (`SYSTEM_*` from Azure DevOps, CI detection such as `TF_BUILD`,
  `GITHUB_ACTIONS`, `CI`) keep their names.
- `dgtp` never writes to its own environment variables at runtime. Today `PowerLogic` sets
  `DGTP_NON_INTERACTIVE`; that state is passed explicitly instead.

### Variables

| Env var | Option | Purpose | Replaces |
|---|---|---|---|
| `DGTP_CONNECTION` | `--connection <name>` | stored connection to use | `--profile` / `dgtp:profile` |
| `DGTP_CONNECTION_STRING` | `--connection-string` | ad-hoc, non-persisted connection (§7) | `xrm:connection` |
| `DGTP_NON_INTERACTIVE` | `--non-interactive` | never prompt; exit code 2 if login required | `dgtp:non-interactive` |
| `DGTP_HOME` | – | override of the data directory (§5) | new |
| `DGTP_ALLOW_UNENCRYPTED_STORAGE` | – | explicit plaintext opt-in without keyring (§8) | new |
| `DGTP_TELEMETRY_OPTOUT` | – | disable telemetry | `DGT_TELEMETRY_OPTOUT` |
| `DO_NOT_TRACK` | – | disable telemetry ([de-facto standard](https://consoledonottrack.com/)) | new |
| `DGTP_TELEMETRY_CONNECTION_STRING` | – | custom Azure Monitor endpoint | `DGT_TELEMETRY_CONNECTION_STRING` |
| `DGTP_TSL_STRICT_MODE` | – | TSL fail-fast on undefined values | `DGT_POWER_TSL_STRICT_MODE` |
| `DGTP_TSL_MAX_STEPS` | – | TSL execution step limit | `DGT_POWER_TSL_MAX_STEPS` |

### Global `pollrate` is removed

The global `pollrate` setting (default 5000 ms) has no global meaning. It is used by two commands
and is replaced by command-specific handling:

| Command | Current use | 3.0 |
|---|---|---|
| `maintenance bulkdelete` (`BulkDeleteUtil`) | interval between status checks of the async bulk-delete job | dedicated option `--poll-interval <seconds>` (default 5) |
| `import outlooktemplates` (`OutlookTemplateImport`) | fixed pause between updating, deleting and re-creating saved queries (not polling) | check whether the pauses are still needed; if yes, a private named constant, no option |

Both commands receive a `TimeProvider` and use `Task.Delay(delay, timeProvider, ct)`. Tests use a
fake time provider instead of today's `pollrate` override in `CommandTestContextBuilder` and
`CodeGenerationTestsBase`.

### Migration

Old names are **removed in 3.0 without aliases**. To avoid silent behaviour changes, 3.x prints a
one-line warning when it detects a removed setting. The value is **not** applied:

- `dgtp.json` in the working directory → `dgtp.json is no longer supported; use DGTP_* environment variables`
- any `dgtp:*` environment variable, or `DGT_TELEMETRY_*` / `DGT_POWER_TSL_*` → name of the replacement
- `--profile` → `use --connection`

There is no exception for the old telemetry opt-out: `DGT_TELEMETRY_OPTOUT` is **not** honoured in
3.0. Opting out requires `DGTP_TELEMETRY_OPTOUT` or `DO_NOT_TRACK`. The warning for the old name
and the README breaking-changes section make this explicit.

The README breaking-changes section lists the full mapping table above.

---

## 11. Code structure

```text
src/dgt.power.common/
  Storage/
    DgtpHome.cs                  path resolution, permissions
    StateStore.cs                state.json (install id, notice, version check)
  Connections/
    ConnectionDefinition.cs      + one record per type (polymorphic JSON, "type" discriminator)
    ConnectionStore.cs           IConnectionStore: load/save/lock/atomic write
    ISecretStore.cs / SecretStore.cs
    CredentialFactory.cs         ConnectionDefinition -> TokenCredential
    DataverseConnector.cs        TokenCredential -> ServiceClient (replaces TokenConnector)
    AdHocConnectionResolver.cs   --connection-string / env var -> CrmConnector (never persisted)
    ConnectionStringHint.cs      AuthType -> suggested typed `connection create` command (error hint only)
src/modules/dgt.power.connection/   commands rewritten against IConnectionStore / ISecretStore
```

Removed afterwards: `ProfileManager`, `IProfileManager`, `Identities`, `IIdentities`,
`TokenConnector`, the `Identity` hierarchy, the `dgt.power.profile` module, the
`Microsoft.AspNetCore.DataProtection` dependency, and all `IsolatedStorageFile` registrations in
`Program.cs`. `ProfileNamesProvider` reads `connections.json` directly. Completion no longer
needs to decrypt anything.

`IXrmConnection` keeps its public shape (`ConnectAsync`, `CheckAuthAsync`, `RefreshAuthAsync`), so
the business-logic modules are not affected.

---

## 12. Testing

| Area | Tests |
|---|---|
| `DgtpHome` | `DGTP_HOME` override, default per OS, Unix permissions |
| `ConnectionStore` | round-trip per type, case-insensitive names, atomic write, concurrent writers, unknown `schemaVersion` |
| No-secrets invariant | create each type with known secret values, assert `connections.json` contains none of them |
| `ConnectionStringHint` | each supported `AuthType` produces the right typed command, unsupported types point to `DGTP_CONNECTION_STRING`, secrets never in output |
| Ad-hoc connection string | precedence (`--connection-string` > `DGTP_CONNECTION_STRING` > `--connection` > `DGTP_CONNECTION` > current), nothing written to disk, value redacted in output |
| Environment / options | each `DGTP_*` variable read correctly; option beats env var; shared boolean parsing; `DGTP_TELEMETRY_OPTOUT` and `DO_NOT_TRACK` both disable telemetry; removed settings (`dgtp.json`, `dgtp:*`, `DGT_*`, `--profile`) produce a warning and are not applied |
| `CredentialFactory` | correct credential type/options per connection, non-interactive → `DisableAutomaticAuthentication`, tenant required for user logins |
| Exit code contract | `AuthenticationRequiredException` → `InteractiveLoginRequiredException` → exit 2 |
| Secret store | in-memory fake for unit tests; Windows DPAPI integration test; keyring-unavailable path |
| CLI | `CommandTreeTests` / `SettingsParsingTests` for new options, `logout`, removed `profile` branch |

Manual checklist: install 3.x → create interactive, client-secret, certificate and federated
connections → close the shell → silent auth works → update to a fake 4.0 package → connections
listed, no re-login.

---

## 13. Rollout

1. **3.0.0-beta.N:** new storage, credential factory, typed connections, environment variables,
   all consumers moved off isolated storage, `profile` branch removed. Documentation: README
   connection section, `schemas/connection/v1`, and a breaking-changes section covering: connections
   must be recreated (§10), removed `connection create --connection-string` (including the Azure
   DevOps client-secret pipeline example), removed `profile` branch, and the environment-variable
   mapping table (§10a).
2. **3.0.0:** GA.

Optional hardening, independent of this design: pin `<AssemblyVersion>` (e.g. `3.0.0.0`) so
assembly identity stops changing with the package version. Once isolated storage is gone this is
no longer required. It only matters for anything else that keys off assembly identity.

---

## 14. Decisions and remaining verification

Decided:

| # | Topic | Decision |
|---|---|---|
| 1 | Telemetry opt-out | `DGTP_TELEMETRY_OPTOUT` or `DO_NOT_TRACK`; `DGT_TELEMETRY_OPTOUT` is not honoured (breaking) |
| 2 | Migration from 2.x | Not in the first version; possible later (§15) |
| 3 | `profile` branch | Removed in 3.0 |
| 4 | Client secrets | Only `--client-secret` (stored in the secret store); no env-var reference; CI uses WIF or `DGTP_CONNECTION_STRING` |
| 5 | `azureCli` connection type | Later (§15) |
| 6 | Tenant for user logins | `--tenant` is required (GUID or verified domain) |
| 7 | Connection strings | Not persisted; ad-hoc via `--connection-string` / `DGTP_CONNECTION_STRING` only |
| 8 | Configuration | `dgtp.json`, `dgtp:*` and `pollrate` removed; fixed `DGTP_*` variables (§10a) |

To verify during implementation:

1. **MSAL extensions storage API:** usable directly for arbitrary secrets, or use the AES-GCM
   fallback (§8)?
2. **Global options in Spectre.Console.Cli:** shared base settings vs. stripping `args` (§7).

---

## 15. Future extensions (nice to have)

Not required for 3.0. Most become possible because `connections.json` is stable and contains no secrets.

### Migration from 1.x/2.x

The old stores are still on disk and readable from 3.x, so a later `dgtp connection migrate` could
import them (see `.memory/research-isolated-storage-major-version-scoping.md`):

- **Locate:** compute the `StrongName.<hash>` folder name (SHA1 over public key + int32 major +
  assembly name, formatted like the runtime's `IdentityHelper`) for previous majors and search
  `<IsolatedStorage root>/*/*/StrongName.<hash>/AssemFiles/identities.dat`. Offer `--from <path>`
  as a manual override.
- **Decrypt:** Windows DPAPI CurrentUser with the fixed entropy `[22, 4, 19, 8, 6]`; macOS/Linux
  ASP.NET DataProtection (`"dgtp"` / `"dgtp-Identity"`, keys in `~/.aspnet/DataProtection-Keys`).
- **Map:** `TokenIdentity` → `interactive` (tenant must be supplied or derived from the MSAL
  account); `AzureDevOpsFederatedIdentity` → `azureDevOpsFederated`; connection strings with
  `AuthType=ClientSecret` / `Certificate` → typed connections; anything else is reported, not
  imported. No token import; one login per account.
- Never modify or delete the legacy store.

This reintroduces the DataProtection dependency, so keep it in an isolated `Legacy/` folder.

### `azureCli` connection type

Reuse an existing `az login` via `AzureCliCredential`. No stored secret, no own token cache.
Useful for developers who already work with the Azure CLI.

### Portability (new machine setup)

`connections.json` can be copied, synced or versioned in a private dotfiles repo. On the new
machine:

| Connection type | Works after copy? |
|---|---|
| `azureDevOpsFederated`, `clientCertificate` (cert present) | Yes |
| `interactive` / `deviceCode` | Metadata yes; one login per account (token cache is machine-bound) |
| `clientSecret` | Metadata yes; secret must be re-entered |

Possible CLI support:

- `dgtp connection export [--file <path>]`: writes the metadata without `authenticationRecord`
  (contains username/tenant).
- `dgtp connection import <file> [--overwrite]`: merges and reports which connections need a login
  or a secret.
- `dgtp connection status --all`: shows what is missing per connection after the import.

### Project-level connections

An optional `.dgtp/connections.json` in a repository, layered over the user file. Same-named
connections defined by the user take precedence. Only shareable fields are allowed: URL, type,
tenant/client IDs, env-var secret references, and no `authenticationRecord`. This lets a team
share environment definitions through Git.

---

## 16. Alternatives considered

| Alternative | Why rejected |
|---|---|
| Keep isolated storage, pin `AssemblyVersion` | Still tied to assembly name and public key (signing-key rotation or renaming breaks it again); keeps tokens and secrets mixed; does not fix weak protection on non-Windows |
| Keep MSAL directly with `MsalCacheHelper` (as in the brainstorming draft) | Works, but duplicates what Azure.Identity already provides, and federated, certificate and secret flows would still need separate code paths |
| Persist arbitrary connection strings (whole string in secret store) | Opaque: cannot be validated, may encode deprecated auth (username/password), cannot be exported predictably, and adds a second storage path. The ad-hoc fallback covers the exotic cases without persistence |
| Cache file per connection | Each connection needs its own login; no refresh-token sharing; more files to clean up |
| ASP.NET DataProtection for everything | Keys are unencrypted on macOS/Linux; not a real secret store |
| Third-party keychain libraries | Extra dependency and native interop risk for no gain over the MSAL extensions already in the dependency tree |
| Bridge release in 2.x to export data | Unnecessary: 3.x can compute the legacy location itself if migration is added later (§15); users would have to install the bridge release first |
