<h1 align="center"> DigitallPower CLI </h1> <br>

<br/>
<p align="center">
    <a href="LICENSE" target="_blank">
        <img src="https://img.shields.io/github/license/DIGITALLNature/DigitallPower.svg" alt="GitHub license">
    </a>
    <a href="https://github.com/DIGITALLNature/DigitallPower/releases" target="_blank">
        <img src="https://img.shields.io/github/tag/DIGITALLNature/DigitallPower.svg" alt="GitHub tag (latest SemVer)">
    </a>
    <a href="https://www.nuget.org/packages/dgt.power" target="_blank">
        <img src="https://img.shields.io/nuget/v/dgt.power" alt="Nuget">
    </a>
    <a href="https://github.com/DIGITALLNature/DigitallPower/graphs/contributors" target="_blank">
        <img src="https://img.shields.io/github/contributors-anon/DIGITALLNature/DigitallPower.svg" alt="GitHub contributors">
    </a>
    <a href="https://sonarcloud.io/project/overview?id=DIGITALLNature_DigitallPower" target="_blank">
        <img src="https://sonarcloud.io/api/project_badges/measure?project=DIGITALLNature_DigitallPower&metric=alert_status" alt="Quality Gate Status">
    </a>
</p>
<br/>

> **Upgrading between major versions?** Review the
> [migration guides](docs/migrations/) for user-affecting breaking changes before upgrading.

# Introduction

**DIGITALLPOWER** — the .NET tool for the Microsoft Power Platform from DIGITALL. A swiss army knife for all ALM tasks where the Power Platform CLI (`pac`) still has weaknesses.

DigitallPower (`dgtp`) is a cross-platform global .NET tool that helps developers, makers and operators to **develop, deploy and maintain** Microsoft Dataverse / Power Platform solutions. It complements the official tooling and focuses on the day-to-day pain points DIGITALL encounters in real-world enterprise projects — with the goal of universally helping the wider community.

## Table of Contents

- [Features at a Glance](#-features-at-a-glance)
- [Installation](#-installation)
- [Quick Start](#-quick-start)
- [Tab Completion](#-tab-completion)
- [Configuration](#%EF%B8%8F-configuration)
  - [Portable owner mappings](#portable-owner-mappings)
- [Command Reference](#-command-reference)
  - [connection](#connection--authentication--environments)
    - [Authentication types](#authentication-types)
  - [export](#export--export-dataverse-artifacts)
  - [import](#import--import-dataverse-artifacts)
  - [teamtemplates](#teamtemplates--team-template-transfer)
  - [analyze](#analyze--solution-analysis)
  - [solution](#solution--solution-scoped-operations)
  - [maintenance](#maintenance--operational-tasks)
  - [process](#process--process-state-and-ownership)
  - [duplicaterules](#duplicaterules--duplicate-rule-publication-and-ownership)
  - [codegeneration](#codegeneration-cg--early-bound-code-generation)
  - [plugin](#plugin--manage-plugin-assembliespackages)
  - [webresource](#webresource--deploy-webresources)
  - [telemetry](#telemetry--manage-telemetry-settings)
- [CI/CD Integration](#-cicd-integration)
  - [Client Secret service connection](#client-secret-service-connection)
  - [Workload Identity Federation (OIDC) service connection](#workload-identity-federation-oidc-service-connection)
- [Solution Architecture](#-solution-architecture)
  - [Caller-specific Dataverse services](#caller-specific-dataverse-services)
- [Repository Layout](#-repository-layout)
- [Build & Test](#%EF%B8%8F-build--test)
- [Spec-Driven Development](#spec-driven-development)
- [Requirements](#-requirements)
- [Telemetry](#-telemetry)
  - [Managing telemetry](#managing-telemetry)
  - [Persistent application state](#persistent-application-state)
- [Community & Contributions](#%EF%B8%8F-community-and-contributions)
- [License](#-license)

## ✨ Features at a Glance

| Area | What it does |
|------|--------------|
| **Connections** | Manage typed Dataverse connections (interactive, device-code, client-secret, certificate, Azure DevOps federation) and check authentication status |
| **Export** | Extract configuration data (queues, SLAs, calendars, routing rules, document/Outlook templates, user roles, bulk delete jobs) from an environment |
| **Import** | Import the previously exported artifacts into another environment — ideal for ALM pipelines |
| **Team templates** | Transfer team templates with a versioned file, GUID identity, preflight validation, and explicit table-scoped cleanup |
| **Analyze** | Inspect solutions for redundant components, active-layer issues, top-layer problems and obsolete patches |
| **Solution** | Run configuration-driven Dataverse quality gates (`solution lint`) such as unmanaged field naming and table completeness checks against a single solution, increment solution versions (`solution version`), and copy solution components between solutions (`solution copy-components`) |
| **Maintenance** | Bulk-delete records, manage auto-number formats, protect calculated fields, filter PowerFx plugin steps, and more |
| **Process** | Capture and apply explicit process state and portable ownership by workflow ID, with preflight, dry-run and optional run-level impersonation |
| **Duplicate Rules** | Capture and apply publication state and portable ownership of existing duplicate-detection rules by rule ID, with capacity preflight, dry-run and bounded publish observation |
| **Code Generation** | Generate strongly-typed C# (early-bound), TypeScript and metadata files for Dataverse entities |
| **Plugin** | Deploy plugin assemblies and packages with `plugin push`; manage plugin steps (configuration, enable/disable, bulk state) |
| **Webresources** | Push webresources from a directory or a single file, with publisher-prefix naming and solution membership |

## 🚀 Installation

DigitallPower is published as a [.NET global tool](https://www.nuget.org/packages/dgt.power):

```bash
dotnet tool install -g dgt.power
```

Update to the latest version:

```bash
dotnet tool update -g dgt.power
```

After installation the command `dgtp` is available globally.

## ⚡ Quick Start

```bash
# 1. Create and select a connection
dgtp connection create dev --url https://contoso-dev.crm4.dynamics.com --tenant contoso.onmicrosoft.com
dgtp connection select dev

# 2. Verify the connection by listing connections
dgtp connection list

# 3. Export configuration data from the environment
dgtp export queues --filedir ./out/queues

# 4. Generate early-bound C# classes
dgtp codegeneration ./generated -c ./genconfig.json
```

Run `dgtp --help` or `dgtp <command> --help` to discover all options.

## 🔁 Tab Completion

`dgtp` supports shell tab completion via [dotnet-suggest](https://github.com/dotnet/command-line-api/blob/main/docs/dotnet-suggest.md).

### Quick setup (recommended)

Run both steps in one command:

```bash
# Install dotnet-suggest first if you haven't already
dotnet tool install -g dotnet-suggest

# Register dgtp AND install the shell shim
dgtp complete setup --all
```

Then reload your shell (`source ~/.zshrc` or open a new terminal).

### Manual setup

**1. Install the `dotnet-suggest` global tool:**

```bash
dotnet tool install -g dotnet-suggest
```

**2. Register `dgtp` with dotnet-suggest:**

```bash
dgtp complete setup
```

**3. Install the shell shim:**

```bash
dgtp complete install-shell
```

This auto-detects your current shell and writes the shim to your RC file.
Use `--shell bash|zsh|pwsh|fish` to override the detected shell.
Use `--dry-run` to preview what would be written without making changes.

The shim is written with idempotency markers — running the command again does nothing if already installed:

```
# >>> dgtp tab completion start >>>
...dotnet-suggest shim script...
# <<< dgtp tab completion end <<<
```

### `complete` command reference

| Command | Description |
|---------|-------------|
| `dgtp complete setup` | Registers dgtp with dotnet-suggest |
| `dgtp complete setup --all` | Registers AND installs shell shim |
| `dgtp complete setup --all --shell bash` | Same, with explicit shell override |
| `dgtp complete install-shell` | Installs shell shim (auto-detects shell) |
| `dgtp complete install-shell --shell zsh` | Installs shim for zsh explicitly |
| `dgtp complete install-shell --dry-run` | Preview without writing |

### What gets completed

| Input | Completions |
|-------|-------------|
| `dgtp <TAB>` | `export` `import` `teamtemplates` `maintenance` `process` `duplicaterules` `analyze` `connection` `codegeneration` `plugin` `webresource` `telemetry` `complete` |
| `dgtp export <TAB>` | `bulkdeletes` `queues` … |
| `dgtp export --<TAB>` | `--filedir` `--filename` `--inline` `--no-telemetry` |
| `dgtp teamtemplates <TAB>` | `import` `export` |
| `dgtp teamtemplates import --<TAB>` | `--dry-run` `--cleanup` `--cleanup-table` |
| `dgtp teamtemplates export --<TAB>` | `--dry-run` `--overwrite` `--table` |
| `dgtp connection <TAB>` | `list` `create` `delete` `select` `status` `refresh` |
| `dgtp telemetry <TAB>` | `status` `enable` `disable` |
| `dgtp plugin <TAB>` | `push` |
| `dgtp plugin step <TAB>` | `enable` `disable` `config` `state` |
| `dgtp plugin step config <TAB>` | `set` |
| `dgtp plugin step state <TAB>` | `set` |
| `dgtp process <TAB>` | `init` `apply` |
| `dgtp duplicaterules <TAB>` | `init` `apply` |

> **Note:** Tab completion is static (command names and option flags only). It does not connect to Dataverse and requires no network access.

## ⚙️ Configuration

The former global `dgtp.json` configuration and `dgtp:*` environment-variable binding have been removed. Commands accept their own options and configuration files. For example, `maintenance bulkdelete` accepts `--poll-interval` (seconds; default `5`) to control how often it checks the asynchronous job.

Saved connections and application state use a stable per-user data directory instead of assembly-scoped isolated storage. Set `DGTP_HOME` to override its location. Connections are stored in `connections.json`; client secrets and PFX passwords are stored separately using the platform's protected storage (DPAPI on Windows, Keychain on macOS, and Secret Service on Linux). No 2.x connections are migrated automatically.

For the removed `profile` commands and connection recreation steps, see
[Migrating from 2.x to 3.x](docs/migrations/2.x-to-3.x.md#profiles-and-saved-connections).

For interactive and device-code sign-in, `--tenant` is optional: if omitted, authentication targets the user's home tenant. Service-principal and explicitly configured Azure DevOps federated connections require a tenant ID.

Global connection/authentication environment variables:

| Variable | Purpose |
|---|---|
| `DGTP_CONNECTION` | Name of the connection to use (overrides the saved current selection) |
| `DGTP_CONNECTION_STRING` | One-off connection string; never persisted |
| `DGTP_NON_INTERACTIVE` | Disable interactive authentication when set to a truthy value |
| `DGTP_ALLOW_UNENCRYPTED_STORAGE` | Explicitly allow unencrypted token/secret storage when a Linux keyring is unavailable; warns when that backend is selected |

`--connection` and `--connection-string` override their corresponding environment variables. A command-line `--connection-string` or `DGTP_CONNECTION_STRING` takes precedence over the named connection.

JSON schemas for the various configuration files used by the modules live under [`schemas/`](schemas) and can be referenced from your own config files via the `$schema` property for autocomplete in modern editors.

### Portable owner mappings

Ownership-aware consumers use a V1 environment-specific mapping to translate stable logical keys
into existing Dataverse users, application users, or owning teams. Keep resource intent portable:
refer to keys such as `salesOwner`, and keep tenant-specific Entra IDs and names in the mapping for
the selected environment.

```json
{
  "$schema": "https://digitallpower.dev/schemas/ownership/v1/schema.json",
  "schemaVersion": 1,
  "owners": {
    "salesOwner": {
      "kind": "team",
      "namePattern": "Sales-*",
      "businessUnitName": "Sales"
    },
    "groupOwner": {
      "kind": "team",
      "entraObjectId": "11111111-1111-1111-1111-111111111111",
      "businessUnitName": "Sales",
      "membershipType": "members"
    },
    "humanOwner": {
      "kind": "user",
      "domainName": "deployment@example.org"
    },
    "entraUser": {
      "kind": "user",
      "entraObjectId": "33333333-3333-3333-3333-333333333333"
    },
    "automationOwner": {
      "kind": "applicationUser",
      "applicationId": "22222222-2222-2222-2222-222222222222"
    }
  }
}
```

The V1 schema is [`schemas/ownership/v1/schema.json`](schemas/ownership/v1/schema.json).
Ownership-aware consumers use `--owner-mapping <File>`; the
[`process` commands](#process--process-state-and-ownership) support this shared contract for
regular and application users; the
[`duplicaterules` commands](#duplicaterules--duplicate-rule-publication-and-ownership) also
support owning teams.
Relative mapping paths are resolved from the working directory. There is no implicit file search,
default owner, connection-identity fallback, or selector fallback. Explicitly supplied files are
validated even when a consumer invocation has no owner intent; state-only operations do not need a
mapping. Logical keys are ordinal case-sensitive; keys and selector strings must be nonblank and
must not have leading or trailing whitespace.

`user` selectors use exactly one of `entraObjectId` or `domainName` (matched to
`systemuser.domainname`); `applicationUser` uses `applicationId`. A `team` uses either
`entraObjectId`, optionally qualified by `businessUnitName` and `membershipType`, or a
`namePattern` together with `businessUnitName`. GUID selectors must be nonempty. Names and
domain names are compared as whole values using ordinal, case-insensitive comparison. Team
patterns match the whole name and only `*` expands to zero or more characters; `?`, `%`, `_`, and
brackets are literal. Business-unit names must identify exactly one unit, and team qualifiers
combine with AND. Membership values are `membersAndGuests`, `members`, `owners`, and `guests`.
An Entra group-ID selector resolves only group-backed security or Microsoft 365 teams; a name
pattern can also resolve an owner team.

Apply resolution validates the entire file and requested keys locally, then looks up only the
requested keys; unused entries do not cause Dataverse queries. Multiple logical keys may alias the
same principal for apply. Capture is deliberately stricter: it resolves the entire source mapping
and requires exactly one key for each captured principal. Aliases are allowed for principals not
being captured, but aliases for a captured principal are an ambiguity error. Captured owners
without a key fail by default. The built-in platform users SYSTEM and INTEGRATION (exact full
name, no domain name, Entra object ID, or application ID) are always skipped instead, because they
own platform-provided records and cannot be transported. Consumers can opt into skipping other
unmapped owners (`--unmapped-owners skip`); ambiguity still fails. Skipped records are written
without `owner`, so apply preserves the target owner, and each command warns per skipped principal.
Consumers must finish capture validation before writing configuration output.

Resolution returns principal identity, principal kind, and user-disabled/team-type metadata; it
does not assign records or claim resource-specific assignment eligibility. Disabled users remain
resolvable so a consumer can apply its own policy before writing. Owner, security-group, and
Microsoft 365 group teams can own records; access teams and unclassified teams are rejected.
Missing, ambiguous, wrong-kind, and ineligible matches fail explicitly with selector context.
Lookups use complete Dataverse paging, do not choose a first match, and do not retry or substitute
another selector. The [portable mapping example](tests/dgt.power.common.tests/Ownership/Fixtures/owner-mapping.example.json)
is shared with loader tests.

Resource configuration refers to the logical key rather than embedding a tenant identity, for
example `{ "owner": "salesOwner" }`. Process and duplicate-rule commands use this contract; additional resources
may adopt it with their own eligibility policies. Use the source environment's mapping file during
capture and the destination environment's
mapping file during apply. The same logical key can intentionally resolve to different principals
in those files. Apply permits aliases, while capture rejects aliases only when multiple keys resolve
to an owner being captured; aliases for uncaptured principals are harmless.

For resources that allow team owners (such as duplicate rules, but not process commands), the source mapping can pin the source
team's Entra group identity while the target mapping uses a target-specific name and business unit:

```json
{
  "schemaVersion": 1,
  "owners": {
    "salesOwner": {
      "kind": "team",
      "entraObjectId": "11111111-1111-1111-1111-111111111111"
    }
  }
}
```

```json
{
  "schemaVersion": 1,
  "owners": {
    "salesOwner": {
      "kind": "team",
      "namePattern": "Sales-*",
      "businessUnitName": "Sales"
    }
  }
}
```

The source file is resolved and reverse-matched during capture; the target file is lazily resolved
for requested keys during apply. Each consumer must validate resource-specific admissibility before
its first write and must finish capture validation before creating or replacing its output file.

## 📚 Command Reference

The CLI is organized into branches. The general invocation pattern is:

```
dgtp <branch> <command> [arguments] [options]
```

### `connection` — Authentication & environments

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

> **CI/CD pipelines:** see the [CI/CD Integration](#-cicd-integration) section for how to create
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

#### Authentication types

Choose one authentication type when creating a saved connection:

| Type | Suitable for | Credential source | Sensitive data stored by dgtp |
|---|---|---|---|
| Interactive browser | Local development with a user account | Browser sign-in, then cached tokens | OS-protected user-token cache |
| Device code | SSH sessions or terminals without a local browser | Code entered in a browser on another device, then cached tokens | OS-protected user-token cache |
| Client secret | Service-principal authentication | `--client-secret <secret>` | Client secret in the protected secret store |
| Client certificate | Service-principal authentication without a client secret | CurrentUser certificate store or a PFX file | PFX password, if using a file; no certificate/private-key copy |
| Azure DevOps federation | Azure Pipelines without long-lived client secrets | Short-lived pipeline OIDC token | No client secret or certificate password |

**Interactive browser and device code**

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

**Client secret**

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
[CI/CD Integration](#-cicd-integration).

**Client certificate: certificate store or PFX file**

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

**Azure DevOps Workload Identity Federation**

```bash
dgtp connection create pipeline --azure-devops-federated --service-connection-name PowerPlatform-Production
```

This runs in Azure Pipelines with an authorized, federated Power Platform service connection.
dgtp resolves its Dataverse URL, tenant, application ID, and service-connection ID, then exchanges
the job's short-lived OIDC token for an access token. Only those identifiers are saved; no
long-lived client secret is required. A saved definition alone cannot authenticate outside the
pipeline context. See the [federation setup and pipeline examples](#workload-identity-federation-oidc-service-connection)
for required environment variables and the explicit-ID alternative.

**One-off connection strings**

For auth modes/options outside these saved types, use `--connection-string` or
`DGTP_CONNECTION_STRING`. These are passed to the Dataverse SDK and are not saved as a connection
type. Prefer the environment variable for secret-bearing strings to avoid exposing them in
command-line arguments or shell history. `connection status` skips their authentication check;
successful status does not verify their credentials.

### `export` — Export Dataverse artifacts

Exports configuration data from the currently selected environment into JSON files.

| Command | Description |
|---------|-------------|
| `export bulkdeletes` | Bulk delete jobs |
| `export queues` | Queues |
| `export documenttemplates` | Document templates |
| `export calendars` | Calendars |
| `export slaconfigs` | SLAs |
| `export routingruleconfigs` | Routing rules |
| `export userroles` | User → security role assignments |
| `export outlooktemplates` | Outlook templates |

All export commands accept `--filedir <path>` to control the output directory.

```bash
dgtp export bulkdeletes --filedir ./out/bulkdeletes
```

### `solution` — solution-scoped operations

The `solution` branch hosts commands that act on Dataverse solutions: `version` and `lint` each act on a single solution, while `copy-components` copies components from one or more source solutions into a target.

| Command | Description |
|---------|-------------|
| `solution version <Solution> [--major\|--minor\|--build\|--revision]` | Increment a solution version (default: `--revision`) |
| `solution lint <Solution> -c ./lint.config.json` | Run the enabled lint rules against the given solution |
| `solution copy-components <Target> --source <Sol1,Sol2>` | Copy solution components from one or more source solutions into an unmanaged target solution |

```bash
dgtp solution version sample_solution --minor
```

`lint` options:

| Option | Description |
|--------|-------------|
| `--rules <id1,id2>` | Restrict this run to a subset of rule ids (intersected with the enabled rules from config) |
| `--fail-on <None\|Info\|Warning\|Error>` | Minimum severity that fails the command (exit code 1). Default `Error`. `None` disables the gate - the command always exits 0, useful for report-only runs |
| `--report <path>` | Write all findings as JSON (each entry includes a `Baselined` flag) |
| `--sarif-output <path>` | Write all findings as a SARIF 2.1.0 log (suppressed results are marked via SARIF `suppressions`) |
| `--baseline <path>` | Path to a SARIF baseline file. Findings whose `RuleId + Solution + ComponentType + ComponentLogicalName/Id` match a baseline entry are excluded from the `--fail-on` gate (they still show up in the console/report/SARIF output, flagged as baselined) |
| `--update-baseline` | Overwrite `--baseline` with the findings from this run instead of gating on them. Requires `--baseline`. Always exits 0 |

Example configuration:

```json
{
  "version": 1,
  "rules": {
    "naming.unmanaged-field-logicalname": {
      "enabled": true,
      "severity": "Error",
      "options": {
        "publisherPrefixes": ["dgt_"]
      }
    },
    "completeness.table-root-component-behavior": {
      "enabled": true,
      "severity": "Error"
    },
    "webresource.jscript-sourcemap": {
      "enabled": true,
      "severity": "Warning"
    }
  }
}
```

Built-in rules:

- **`naming.unmanaged-field-logicalname`** validates unmanaged custom field logical names against the [DIGITALL Nature naming convention](https://digitallnature.github.io/customizing/naming-conventions/): `prfx_fieldname[_type-suffix]`, where the suffix is derived from the attribute's Dataverse type (e.g. `_id` for Lookup, `_set` for Choice, `_cur` for Currency, `_dt`/`_rf`/`_cf`/`_fx` for rollup/calculated/formula modifiers, etc. - see the linked page for the full table). `publisherPrefixes` accepts one or more allowed prefixes (trailing underscore optional) and defaults to `["dgt_"]`. Fields whose logical name contains no underscore at all (e.g. Dataverse-provisioned defaults like `name`, `createdon`, or an auto-created `statecode` on a new custom table) are never flagged, since those are outside of what an unmanaged customization can control.
- **`completeness.table-root-component-behavior`** validates each table's `RootComponentBehavior` against whether the table itself is managed (e.g. ISV-owned), not whether the linted solution is managed - this linter only ever targets unmanaged solutions. An **unmanaged** table must always be added completely (`IncludeSubcomponents` / "Include Entity Metadata and All Assets"); a **managed** table must never be added completely - only its actual delta may be listed explicitly (`DoNotIncludeSubcomponents`) or referenced as a shell (`IncludeAsShellOnly`). No options.
- **`webresource.jscript-sourcemap`** (default severity: `Warning`) flags JScript web resources (`webresourcetype = Script (JScript)`) whose content still contains a source map reference (e.g. `//# sourceMappingURL=...`) - a sign the file was added to the solution unminified (a development build artifact rather than production output). No options.

**Baseline workflow** (accepting existing findings so only *new* violations fail the pipeline):

```bash
# One-time: snapshot the current findings as the accepted baseline
dgtp solution lint sample_solution -c lint.config.json --baseline lint-baseline.sarif.json --update-baseline

# CI: only NEW findings (not in the baseline) fail the build
dgtp solution lint sample_solution -c lint.config.json --baseline lint-baseline.sarif.json --fail-on Error
```

#### `copy-components` — copy solution components between solutions

Copies the `solutioncomponent` rows of one or more source solutions into an unmanaged target solution, using the same `AddSolutionComponentRequest` Dataverse SDK message a maker's "Add existing" action uses under the hood.

| Option | Description |
|--------|-------------|
| `-s, --source <Sol1,Sol2>` | Comma-separated unique names of the solutions to copy components from (required) |
| `--dry-run` | Print the planned changes without adding any component to the target solution |
| `--raw` | Disable best-practice normalization: for tables, preserve only complete vs. non-complete behavior (shell-only sources are treated as non-complete) and skip the managed-active-layer filter |
| `--apps <skip\|strip\|allow>` | How to handle model-driven apps (default `skip`), see below |

Dataverse rejects `DoNotIncludeSubcomponents` on anything but tables, so a model-driven app cannot be added without Dataverse's own expansion. `--apps` controls the trade-off:

| Mode | Behavior |
|------|----------|
| `skip` (default) | Apps are not copied, and neither are app-bound components (app settings, app module components and app elements), which are meaningless without their app. Skipped rows are shown in the plan |
| `strip` | Apps and their app-bound components are copied, then every subcomponent Dataverse added on its own (not part of the plan, not already in the target) is removed again and listed in the output. Verify the app afterwards (e.g. `ValidateApp`) - removing a table or view the app references leaves it with missing dependencies |
| `allow` | Apps and their app-bound components are copied and Dataverse adds whatever it considers part of the app |

By default (best-practice mode, no `--raw`), the command avoids two common causes of solution bloat:

- **Managed tables are never copied completely.** An unmanaged (first-party) table is always added with `RootComponentBehavior = IncludeSubcomponents` (complete); a managed (e.g. ISV-owned) table is added as a skeleton (`DoNotIncludeSubcomponents`) - only its own delta, not the whole table.
- **A managed component (attribute, form, view, workflow, ...) is only copied if it has its own active customization layer** (its top `msdyn_componentlayer` row is the synthetic `Active` layer). A managed component with no active layer is redundant - the managed baseline the target environment already has installed provides it - and is skipped.
- **`AddRequiredComponents` is always `false`.** This keeps Dataverse from adding the "required" dependencies of any component - only the components explicitly present in the source solution(s) are added (model-driven apps are the exception, see `--apps`).

```bash
# Preview what would be copied, without changing the target solution
dgtp solution copy-components target_solution --source dev_solution --dry-run

# Copy with best-practice filtering (default)
dgtp solution copy-components target_solution --source dev_solution_a,dev_solution_b

# Mirror every source component as-is (no managed/active-layer filtering)
dgtp solution copy-components target_solution --source dev_solution --raw

# Also copy model-driven apps, removing the subcomponents Dataverse adds on its own
dgtp solution copy-components target_solution --source dev_solution --apps strip
```

### `import` — Import Dataverse artifacts

Counterpart to `export`. Reads the previously exported JSON files and applies them to the currently selected environment.

| Command | Description |
|---------|-------------|
| `import outlooktemplates` | Outlook templates |
| `import userroles` | User → security role assignments |
| `import queues` | Queues |
| `import bulkdeletes` | Bulk delete jobs |
| `import documenttemplates` | Document templates |
| `import calendar` | Calendars |
| `import slaconfigs` | SLAs |
| `import routingruleconfigs` | Routing rules |

```bash
dgtp import outlooktemplates --filedir ./out/outlooktemplates
```

### `teamtemplates` — Team template transfer

Use resource-oriented commands to export or import Dataverse team templates. Both commands require
an explicit file path. Export includes all non-system templates by default; repeat `--table
<LogicalName>` to filter by table. Import reads the complete file and matches templates by GUID
only; names never substitute for identity. Existing export files are preserved unless
`--overwrite` is supplied.

```bash
# Export all non-system templates to a versioned resource file
dgtp teamtemplates export ./teamtemplates.json

# Export only templates associated with the selected logical tables
dgtp teamtemplates export ./teamtemplates.json --table account --table contact

# Preview an import without making Dataverse writes
dgtp teamtemplates import ./teamtemplates.json --dry-run

# Explicitly remove unlisted templates only within named tables
dgtp teamtemplates import ./teamtemplates.json \
  --cleanup --cleanup-table account --cleanup-table contact --dry-run
```

Table options are repeatable: pass one `--table` or `--cleanup-table` per logical table name;
comma-separated lists are not used. Cleanup requires both `--cleanup` and at least one explicit
`--cleanup-table`. There is no `--all` or other global cleanup option: scope is never inferred
from the file or export filters, and system templates are never updated or deleted. Access-team
enablement and valid Dataverse rights are checked during preflight.

Transfer files use `schemaVersion: 1` and a `templates` array. The schema is published at
[`schemas/teamtemplates/v1/schema.json`](schemas/teamtemplates/v1/schema.json).

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/teamtemplates/v1/schema.json",
  "schemaVersion": 1,
  "templates": [
    {
      "teamtemplateid": "4e17461c-efec-43a0-835b-08fa78af1f83",
      "entity": "account",
      "teamtemplatename": "Account readers",
      "description": null,
      "defaultaccessrightsmask": 1
    }
  ]
}
```

The `defaultaccessrightsmask` must contain a supported, non-zero combination of access-team
rights: Read (`1`), Write (`2`), Append (`4`), Append To (`16`), Delete (`65536`), Share
(`262144`), and Assign (`524288`). Create (`32`) is not supported for access-team templates.
Names are limited to 100 characters and descriptions to 1,500 characters. Table names use
lowercase Dataverse logical names matching `^[a-z][a-z0-9_]*$`. Name duplicates and target
collisions are compared case-insensitively.

Import validates the entire file and target plan before its first write. A missing or invalid file,
unsupported version, invalid rights, a GUID assigned to another table, a table/name collision
under another GUID, a system-template target, or invalid table metadata fails preflight with no
writes. Omitted or null `description` clears the existing description. Unlisted templates are
preserved by default. A valid empty `templates` array is a no-op only without cleanup; empty input
with cleanup is rejected rather than interpreted as a request to clear the selected tables.

Dry-run performs the same validation and reports planned creates, updates, unchanged templates,
and scoped deletes without writing to Dataverse or creating/replacing an export file. Apply is not
transactional: if a mutation fails, completed changes are not rolled back. Execution stops at the
first failure, reports partial results, and cleanup starts only after all creates and updates
succeed.

### `analyze` — Solution analysis

Static analysis of one or many Dataverse solutions.

| Command | Description |
|---------|-------------|
| `analyze entityallassets` | Scan solutions for entities containing all assets |
| `analyze noactivelayer` | Find unmanaged solution components without an active layer |
| `analyze activelayer` | Find managed solution components that already received an active layer |
| `analyze toplayer` | Find managed solution components where the given solution is not the top layer |
| `analyze redundantcomponents` | Find components contained in multiple solutions |
| `analyze redundantpatches` | Find patches that are no longer needed because their components are no longer top-layer |

```bash
dgtp analyze noactivelayer --inline solution1,solution2
```

### `maintenance` — Operational tasks

Day-to-day administrative actions against a live environment.

| Command | Description |
|---------|-------------|
| `maintenance bulkdelete` | Run a bulk delete job for a given FetchXML and wait for completion |
| `maintenance autonumber` | Set auto-number formats for columns from a JSON config |
| `maintenance protectfields` | Prevent all calculated fields from receiving an active layer |
| `maintenance carrierinfo` | Export carrier solutions metadata to JSON |
| `maintenance removeredundantcomponents <SourceSolutions> <TargetSolution> [--dryrun] [--includeEntities]` | Remove components from `TargetSolution` that already exist in `SourceSolutions` (comma-separated) |
| `maintenance filterfxplugins` | Add message filtering for PowerFx plugin steps |

`maintenance bulkdelete` accepts `--poll-interval <seconds>` (default `5`) to set the interval
between asynchronous job status checks. The option is specific to this command.

```bash
dgtp maintenance bulkdelete --inline "<fetchxml>...</fetchxml>" --poll-interval 10
```

### `process` — Process state and ownership

Capture and apply state and ownership for classic workflows (category 0), business rules (2),
actions (3), business process flows (4), and cloud flows (5). Only process definitions are
supported; dialogs, AI flows and desktop flows are excluded. These commands do not deploy
definitions or manage connection references.

| Command | Description |
|---------|-------------|
| `process init <File> --solution <UniqueName>` | Capture process state from one or more solutions |
| `process apply <File>` | Apply only the state and owner intent recorded in the file |

Both commands require an explicit file path; relative paths use the working directory.

```bash
# Capture state only (the default); repeat --solution to form a union
dgtp process init ./processes.json --solution ContosoCore --solution ContosoFlows

# Capture ownership as logical keys using a source-environment mapping
dgtp process init ./processes.json --solution ContosoCore \
  --capture both --owner-mapping ./owners.source.json --overwrite

# Review the same target resolution and transitions without writes
dgtp process apply ./processes.json --owner-mapping ./owners.target.json --dry-run

# Run all state transitions as one mapped principal; assignments use the connection identity
dgtp process apply ./processes.json --owner-mapping ./owners.target.json \
  --activate-as integrationUser
```

#### V1 configuration

The schema is available at [`schemas/process/v1/schema.json`](schemas/process/v1/schema.json).
`processId` is the Dataverse `workflowid`, preserved by solution import. `name` is informational:
apply never matches a process by display name, unique name or table.

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/process/v1/schema.json",
  "schemaVersion": 1,
  "processes": [
    {
      "processId": "265a263f-958b-4d32-b89d-6b3fdeac0458",
      "name": "Create additional Document Location",
      "active": true,
      "owner": "integrationUser"
    }
  ]
}
```

Each entry requires a nonempty GUID and at least one of `active` or `owner`. Omitted properties
preserve target values, and unlisted processes are untouched. Explicit nulls, unknown or duplicate
JSON properties, duplicate IDs, unsupported versions and root-level defaults are rejected.
Owner keys must be nonblank without surrounding whitespace. Use environment-specific
[portable owner mappings](#portable-owner-mappings); regular and application users are supported,
but team owners are not.

#### Capture and output

| Option | Behavior |
|--------|----------|
| `--solution <UniqueName>` | Required, repeatable exact solution unique name; case-insensitive, no wildcards |
| `--capture state\|owner\|both` | Defaults to `state`; `owner` and `both` require `--owner-mapping` |
| `--owner-mapping <File>` | Source owner mapping; if supplied, always validated locally |
| `--unmapped-owners fail\|skip` | Defaults to `fail`; `skip` writes processes with unmapped owners without `owner` and warns |
| `--overwrite` | Explicitly allow replacement of existing output |

Capture retrieves all pages of supported process definitions in the selected solution union,
including indirect business rules of tables included with all subcomponents. Tables included
without subcomponents do not contribute indirect rules. Unknown solutions fail capture.
Entries are ordered by ID and include informational names. Ownership capture requires exactly one
logical key per captured owner; missing or ambiguous reverse mappings fail before output is
created or replaced. Processes owned by SYSTEM or INTEGRATION, and with `--unmapped-owners skip`
other unmapped owners, are reported as warnings and written without `owner` (`--capture both`) or
omitted (`--capture owner`). Selection and mapping failures leave existing output unchanged, even with
`--overwrite`. No matches produces a valid empty `processes` array.

#### Apply, impersonation and failures

| Option | Behavior |
|--------|----------|
| `--owner-mapping <File>` | Target owner mapping, required for requested owner keys and `--activate-as`; supplied mappings are always validated |
| `--activate-as <OwnerKey>` | Use this mapped user for every activation and deactivation |
| `--activate-as-owner` | Use each process's configured owner for its state transitions; entries requesting `active` must also specify `owner` |
| `--dry-run` | Resolve the same complete plan, visibly report transitions or unchanged entries, and perform no writes or retry rounds |

The two impersonation options are mutually exclusive. Assignments always use the connection
identity; requested impersonation never falls back to it. All process IDs, categories, owners and
required caller services are resolved before the first write. Disabled users are rejected for an
actual owner change or impersonation, but an already satisfied disabled owner does not prevent an
otherwise valid state-only operation. A missing ID never falls back to a similarly named process.
An empty array is a successful no-op without authentication; invalid supplied mappings still fail
local validation.

Already satisfied effects perform no writes. Changing the owner of an active process first
deactivates it, assigns the owner, then restores activation unless `active: false` was requested.
If assignment fails after deactivation, exactly one reactivation attempts to restore the original
state; the report identifies whether that restoration succeeded. Failed requested activations are
retried in rounds only while the failed set shrinks, allowing child flows to activate before
parents. Deactivation, assignment and restoration failures are not retried.

Every run reports per-process outcomes. Independent processes continue after a runtime failure;
any failed requested operation yields an unsuccessful exit. Cancellation stops further work and
reports completed transitions. There is no transaction or general rollback, and failed restoration
can leave a process inactive. Dry-run does not prove operation permissions or activation
dependencies; missing impersonation privileges surface when Dataverse executes a request.

### `duplicaterules` — Duplicate-rule publication and ownership

Capture and apply the publication state and owner of existing duplicate-detection rules. Rule
definitions and conditions travel through solutions; these commands never create, delete, or
change definitions, match rules by name, or clean up unlisted rules.

| Command | Description |
|---------|-------------|
| `duplicaterules init <File>` | Capture state and/or ownership of existing rules |
| `duplicaterules apply <File>` | Apply only the publication and owner intent recorded in the file |

Both commands require an explicit file path; relative paths use the working directory.

```bash
# Capture the publication state of all rules (the default)
dgtp duplicaterules init ./duplicaterules.json

# Capture state and ownership of contact rules using a source-environment mapping
dgtp duplicaterules init ./duplicaterules.json --entity contact \
  --capture both --owner-mapping ./owners.source.json --overwrite

# Capture selected rules only; repeat --rule-id for several rules
dgtp duplicaterules init ./duplicaterules.json \
  --rule-id 6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d --rule-id 0a9b8c7d-6e5f-4a3b-2c1d-0e9f8a7b6c5d

# Review the resolved plan, owners and capacity without writes
dgtp duplicaterules apply ./duplicaterules.json --owner-mapping ./owners.target.json --dry-run

# Apply and wait up to 10 minutes for all publish jobs together
dgtp duplicaterules apply ./duplicaterules.json --owner-mapping ./owners.target.json --timeout 600
```

#### V1 configuration

The schema is available at
[`schemas/duplicaterules/v1/schema.json`](schemas/duplicaterules/v1/schema.json). `ruleId` is the
Dataverse `duplicateruleid`, preserved by solution import. `name` is informational: apply never
matches a rule by name, table, or definition, and a missing GUID fails preflight even if a rule
with the same name exists.

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/duplicaterules/v1/schema.json",
  "schemaVersion": 1,
  "rules": [
    {
      "ruleId": "6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d",
      "name": "Contacts with the same e-mail address",
      "published": true,
      "owner": "integrationUser"
    },
    {
      "ruleId": "0a9b8c7d-6e5f-4a3b-2c1d-0e9f8a7b6c5d",
      "name": "Accounts with the same name",
      "published": false
    }
  ]
}
```

Each entry requires a nonempty GUID and at least one of `published` or `owner`. Omitted properties
preserve target values, and unlisted rules are untouched. Explicit nulls, unknown or duplicate JSON
properties, duplicate IDs, and unsupported versions are rejected. Owner keys must be nonblank
without surrounding whitespace and use
[portable owner mappings](#portable-owner-mappings); regular users, application users, and owning
teams are supported.

#### Capture and output

| Option | Behavior |
|--------|----------|
| `--entity <BaseTable>` | Restrict capture to rules of one exact base-table logical name |
| `--rule-id <Guid>` | Repeatable; restrict capture to these rule IDs |
| `--capture state\|owner\|both` | Defaults to `state`; `owner` and `both` require `--owner-mapping` |
| `--owner-mapping <File>` | Source owner mapping; if supplied, always validated locally |
| `--unmapped-owners fail\|skip` | Defaults to `fail`; `skip` writes rules with unmapped owners without `owner` and warns |
| `--overwrite` | Explicitly allow replacement of existing output |

Without filters, capture includes all rules across all pages. `--entity` and `--rule-id` combine
with AND: a missing selected ID or an ID belonging to another table fails capture. Selected rules
that are currently publishing or in an unknown state fail capture, so capture only stable states.
Entries are ordered by ID and include informational names. Ownership capture requires exactly one
logical key per captured owner. Out-of-the-box rules are owned by SYSTEM: rules owned by SYSTEM or
INTEGRATION, and with `--unmapped-owners skip` other unmapped owners, are reported as warnings and
written without `owner` (`--capture both`) or omitted (`--capture owner`). All validation finishes
before output is created or replaced; an
existing file is never replaced without `--overwrite`, and failures leave it unchanged. No matches
produces a valid empty `rules` array.

#### Apply, capacity and completion

| Option | Behavior |
|--------|----------|
| `--owner-mapping <File>` | Target owner mapping, required for requested owner keys; supplied mappings are always validated |
| `--dry-run` | Resolve the same complete plan and report operations, owners and capacity without writes (also with `--no-wait`) |
| `--no-wait` | Report accepted publish submissions with job IDs instead of waiting for Published |
| `--timeout <Seconds>` | Shared publish observation budget; positive integer, default `300` |

Preflight resolves all rule IDs and owner keys before the first write; any invalid entry prevents
all writes. Listed rules that are currently publishing are rejected. New owners must not be
disabled users and must hold the duplicate-rule read privilege (`prvReadDuplicateRule`), checked through the user's
or owning team's privileges; already satisfied owners are accepted unchanged. This
is not a complete permission check. Dataverse allows at most five Published or Publishing rules
per base table. For tables receiving new publishes, preflight counts all such rules, including
unlisted ones, applies the requested unpublishes, and fails before writes if the end state would
exceed the limit. Rules are never unpublished implicitly to free slots. An empty `rules` array is a
successful no-op without authentication; invalid supplied mappings still fail locally.

Execution runs in ordered phases:

1. Requested unpublishes run sequentially; each is confirmed by read-back before other writes.
2. Owner changes are assigned directly, preserving the current publication state.
3. Requested publishes are submitted in file order without waiting for each job.
4. All accepted publishes are then observed together every two seconds within one shared
   `--timeout` budget.

Already satisfied effects perform no writes. A failed unpublish or assignment skips the dependent
publish of that rule. Publish faults are reported per rule, for example a rule without criteria or
a table with no free slot (capacity changed concurrently); publishes are never queued or retried.
Failed or canceled publish jobs are terminal failures. On timeout, unresolved rules fail with their
last observed state and job ID. With `--no-wait`, unpublish confirmation and owner changes still
complete, accepted publishes count as successful submissions, and the report states that the
later server-side outcome is unverified.

Every run reports per-rule outcomes. Independent rules continue after a runtime failure; any failed
requested operation yields an unsuccessful exit. Cancellation stops further client work. Timeout or
cancellation does not cancel submitted server jobs or roll back completed changes.

### `codegeneration` (`cg`) — Early-bound code generation

Generates `.cs`, `.ts` and `metadata.xml` model files for Dataverse based on a JSON configuration.

```bash
dgtp codegeneration ./generated -c ./genconfig.json
# alias
dgtp cg ./generated -c ./genconfig.json
```

| Option | Description |
|--------|-------------|
| `-f`, `--folder` | Alternate name for the model folder (default: `Model`) |
| `-c`, `--config` | Full path to the config file (default: `config.json`) |

Supported V2 JSON schemas are available under [`schemas/codegeneration/v2/`](schemas/codegeneration/v2).
See the [2.x-to-3.x migration guide](docs/migrations/2.x-to-3.x.md#code-generation) when upgrading.

#### Supported outputs

Code generation reads Dataverse metadata and requires an authenticated Dataverse connection. Each
config selects one target; create separate config files and invoke the command once for every target
you want to generate:

- **.NET** — C# early-bound classes for Modern or Framework targets, request wrappers and message
  constants, and global option-set constants. XML metadata files are optional.
- **TypeScript** — ambient entity and form typings, option-set and SDK message declarations,
  parameterized Custom API wrappers, and optional XrmMock form test helpers.

Generated files are written below `<target-directory>/<folder>` (the default folder is `Model`):
`.NET` files go in `DotNet`, optional XML metadata in `MetaData`, and TypeScript output in
`TypeScript`. Generation replaces generated files in the selected output area; keep custom files
and edits elsewhere or back them up before regenerating. TypeScript generation clears its entire
`TypeScript` subtree.

#### Configuration

Every config must declare `"version": 2` and a case-sensitive `"type"` value of `"dotnet"` or
`"typescript"`. Put `$schema` in the config to enable editor validation and completion. The
supported schemas list all settings and defaults:

- [.NET config schema](schemas/codegeneration/v2/dotnet.schema.json)
- [TypeScript config schema](schemas/codegeneration/v2/typescript.schema.json)

Entity scope can combine explicit logical names (`entities.names`), all entities from listed
solutions (`entities.fromSolutions`), and a wildcard logical-name mask (`entities.mask`). These
are additive: matching any of the configured selectors includes the entity. Requests and global
option sets are independently selected with `requests` and `optionSets`.

#### .NET generation

The default target is `Modern`, intended for .NET 8 and later. Select `Framework` for .NET Framework
4.6.2 projects. The `output.include` settings control context, option-set constants, logical-name
constants, relationship constants, navigation properties, entity type codes, alternate keys, and
optional metadata XML. These sections are enabled by default except metadata. `output.virtual`
makes entity properties virtual, while `output.editableReadOnly` emits setters for read-only
attributes.

This example generates entities from a solution and writes metadata XML as well:

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/codegeneration/v2/dotnet.schema.json",
  "version": 2,
  "type": "dotnet",
  "namespace": "Contoso.Dataverse.Model",
  "entities": { "fromSolutions": ["ContosoCore"] },
  "requests": ["contoso_ApproveOrder"],
  "optionSets": ["contoso_orderstatus"],
  "output": {
    "target": "Modern",
    "include": { "metadata": true }
  }
}
```

`requests` generates SDK message name constants, and messages with parameter metadata also produce
typed request/response wrappers. `optionSets` selects global option sets to emit as constants.

#### TypeScript generation

TypeScript generation emits entity declarations for scoped entities and form typings for their
forms. By default, all forms for each scoped entity are generated. Set `output.forms.filter` to an
allowlist of form identifiers to narrow this, or set `output.forms.fromSolutions` to include only
forms belonging to the configured entity solutions. Set `output.forms.testHelpers` to generate
XrmMock helpers alongside form typings; this is disabled by default.

Form identifiers include the entity, environment-specific form name, and form type. Generate without
a filter first, then use the generated form declaration's filename without `.d.ts` as its identifier
(for example, `account.account_main.form` for a main form named `Account`).

Requests generate SDK message name declarations. Parameterized Custom APIs also generate typed
wrappers by default; set `output.customApis` to `false` to omit those wrappers. Option-set
declarations are controlled by `optionSets`.

The optional `language` is a Dataverse LCID used to localize generated labels. When omitted or
`null`, the organization base language is used. Form names are resolved using the connecting user's
Dataverse UI language; if that differs from the configured label language, `dgtp` warns because
form names and form filters may not match. The TypeScript `namespace` property is accepted but is
currently unused.

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/codegeneration/v2/typescript.schema.json",
  "version": 2,
  "type": "typescript",
  "entities": { "names": ["account"], "fromSolutions": ["ContosoCore"] },
  "requests": ["contoso_ApproveOrder"],
  "optionSets": ["contoso_orderstatus"],
  "output": {
    "forms": {
      "testHelpers": true
    }
  }
}
```

#### Run generation

Save the .NET and TypeScript examples as separate config files, then run the command once per
target. Both invocations can write to the same model directory because the outputs use separate
subdirectories:

```bash
dgtp codegeneration ./generated -c ./genconfig.dotnet.json
dgtp codegeneration ./generated -c ./genconfig.typescript.json
```

Use `--folder` (or `-f`) to change the `Model` folder name, and `--config` (or `-c`) to select the
config file. Without `--config`, the command reads `config.json`.

#### TypeScript template settings

| Environment variable | Purpose | Default |
|---|---|---|
| `DGTP_TSL_STRICT_MODE` | Enables fail-fast errors for undefined Liquid template values (`1`, `true`, or `yes`) | Enabled automatically in CI environments |
| `DGTP_TSL_MAX_STEPS` | Sets the positive maximum number of Fluid template execution steps | `20000` |

See the [2.x-to-3.x migration guide](docs/migrations/2.x-to-3.x.md#code-generation) for changes
when upgrading existing generation configs or code that consumes generated output.

### `plugin` — Manage plugin assemblies/packages

Commands for deploying Dataverse plugin assemblies and packages.

#### `push` — Deploy plugin assemblies/packages

Registers a single plugin assembly (`.dll`), a plugin package (`.nupkg`), every `.dll`/`.nupkg`
found directly in a directory, or every file matched by a glob pattern (for example
`bin/release/**/*.nupkg`, quote it to prevent shell expansion). DLLs without plugins, such as
dependencies, are ignored. Targets are processed one after another; a failing target does not stop
the others but fails the command.

The whole batch is validated before anything is written: unreadable artifacts, a missing
`--publisher-prefix` for packages, and two targets declaring the same assembly, package, data
provider or Custom API reject the batch. Narrow the target to resolve a conflict. A path or glob
that matches no files, or matched files that contain no plugins, fails the command.

##### Usage

```bash
dgtp plugin push ./bin/Release/MyPlugin.dll --solution mysolution
dgtp plugin push ./bin/Release/MyPlugin.1.0.0.nupkg --publisher-prefix contoso --solution mysolution
dgtp plugin push ./bin/Release --publisher-prefix contoso --solution mysolution
dgtp plugin push "./src/**/bin/Release/*.nupkg" --publisher-prefix contoso
```

##### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `--solution` | No | Ensures package, standalone assembly, and declared plugin step membership in the given solution |
| `--publisher-prefix` | For `.nupkg` targets | Publisher customization prefix for plugin packages |
| `--dry-run` | No | Previews the deployment and solution membership plan without writing to Dataverse |
| `--confirm` | No | Prompts before executing each rendered target plan; ignored by dry-run, non-interactive, and CI execution |

Existing plugin package records must have a version in Dataverse; deployment fails explicitly
if the version is missing. A package without an attached file is treated as requiring a content upload.

##### Supported registration attributes

`plugin push` requires registration attributes from `Digitall.Plugins.Registration` 3.0.0 or
later. Other registration namespaces are not recognized.

| Attribute | Behavior |
|-----------|----------|
| `PluginRegistrationAttribute` | Registers plugin steps, including message, stage, mode, entity filters, and images |
| `CustomApiRegistrationAttribute` | Links a plugin type to the declared Custom API |
| `CustomDataProviderRegistrationAttribute` | Creates/updates a custom data provider record and links its operation handlers (experimental support); the configuration table must already exist |

Workflow activity registration (`WorkflowRegistrationAttribute`) is not supported by `plugin push`.
Every concrete `IPlugin` type must carry one of the supported registration attributes; assemblies
with manually maintained plugin registrations are not supported.

##### Custom data providers

**Experimental support:** Custom data-provider deployment is offered as experimental support.
Validate it in a non-production environment before production rollout.

Requires `Digitall.Plugins.Registration` 3.0.0 or later and its three-argument constructor:
data-source configuration-table schema name, operation, and provider name on every declaration.
Rebuild assemblies using the old two-argument constructor before deployment. See the
[registration library](https://github.com/DIGITALLNature/DigitallRegistrationPower) for attribute
usage and examples.

The data-source configuration table is a prerequisite: `plugin push` does not create, update, or
delete it. Provision the table in each environment first. A missing table, or one that is not an
organization-owned table backed by the `JsonConverter` data provider, is rejected before any
deployment writes. The target environment must contain exactly one data provider named
`JsonConverter`; its ID is resolved automatically.

`plugin push` creates or updates the provider record and assigns the Retrieve, RetrieveMultiple,
Create, Update, and Delete handlers. A declaration owns all handler slots of its provider:
undeclared handlers are cleared, even when they reference types from other assemblies. Omitted
optional metadata such as the description is preserved. If a provider is no longer declared, only
handlers referring to plugin types owned by the target are cleared; the provider record and its
table are never deleted. `--dry-run` previews the deployment.

With `--solution`, only missing provider records are added to the solution. The configuration
table, its schema, and its data are not managed by `plugin push` and are not added; transport the
table to downstream environments through your own solution, as with Custom API records.

Data-source records, custom configuration columns, business virtual tables, mappings, and
bulk-operation handlers are outside scope. Existing ordinary tables are not converted into
data-source tables.

##### Step configuration

`plugin push` does not read, write, or reconcile unsecure or secure plugin-step configuration.
Existing configuration is preserved when a matching step is updated. New steps have no
configuration until it is provisioned by the target environment's deployment pipeline. Keep
configuration values in CI/CD secret or environment-variable providers, not registration
attributes or source-controlled configuration files.

##### Managed identity

Managed identity provisioning and binding are outside `plugin push`. Configure the Dataverse
managed identity record and assembly/package binding separately in each environment, together
with that environment's Entra app registration or user-assigned managed identity, federated
credential, and resource permissions.

The same signed artifact can be promoted across stages; the federated credential's subject
includes the Dataverse environment ID. Identity client/tenant IDs do not belong in registration
attributes. See [Microsoft's managed identity setup guide](https://learn.microsoft.com/en-us/power-platform/admin/set-up-managed-identity).

Push leaves identity bindings on existing assembly/package records untouched. A newly created
replacement assembly does not inherit the superseded assembly's binding; provision its identity
binding separately. Push never creates or deletes shared managed identity records.

##### Planning and execution

Before any writes, the command renders a deployment tree for plugin types, steps, images, Custom
API links, and assembly upgrades. It then shows completed operations, or reports that no changes
are required.
Data providers appear in a separate tree after the package or assembly tree; handler entries
reference plugin types rather than nesting the provider beneath them.
`--dry-run` stops after rendering the plan. Missing declared Custom APIs and unresolved step
messages fail before Dataverse changes occur.
Custom API unique names must resolve to exactly one record; ambiguous matches are rejected.
`--confirm` prompts after each target plan in interactive sessions; declining leaves that target
unchanged. Non-interactive and CI execution suppress the prompt.
For assembly upgrades, the tree represents the effective replacement-assembly state, including
steps and images migrated from the superseded assembly. A separate message then states whether
the outdated assembly will be deleted.

CLI-host errors and per-target `plugin push` failures include contextual exception messages and,
for Dataverse service faults, hexadecimal error codes and inner-fault messages. This output is
available in release builds without a diagnostic flag. Stack traces, fault trace text, and arbitrary
fault-detail values are not printed. Service messages may still contain environment-specific
information; review output before sharing it.

##### Solution membership

When `--solution` is set, missing package, standalone assembly, and declared step memberships are
listed below the deployment tree, including in dry-run output. Plugin types, images, Custom APIs,
and managed identities are not added implicitly. When every managed component is already present,
the same section confirms that no membership additions are needed. An unknown solution fails
planning with `MissingSolutionException` before any Dataverse writes occur.
Package membership requires an unambiguous solution component definition for `pluginpackage`
in the target environment; missing or duplicate definitions fail planning rather than skipping
the requested membership.

##### Updates and upgrades

Plugin packages are named using the explicit `<publisher-prefix>_<package-name>` value; DLL-only targets do not
require `--publisher-prefix`. Existing packages and same-version standalone assemblies are updated
only when their content differs. Package version differences alone do not cause an update. A
standalone assembly is updated in place when its major/minor version matches the single existing
same-name standalone assembly; build/revision changes in either direction are in-place updates.

When a local assembly's major or minor version differs, the command creates a
replacement, migrates matching declared plugin steps to preserve their environment configuration,
then removes the superseded assembly and registrations. More than one same-name standalone
assembly is unsupported: `plugin push` fails before writing and requires manual cleanup. Use
`--dry-run` to preview the supported update or replacement outcome.

##### Removals and solution import

`plugin push` only changes the environment it targets. A cleared Custom API handler link is not
propagated by managed solution import (a Dataverse platform limitation), and the linked plugin
type or assembly cannot be deleted while the link exists. Workaround: unlink the handler manually
in each downstream environment before the release that removes the type or assembly. This
creates no unmanaged layer, and the next import then removes the type or assembly.

For plugin packages the same applies to steps: package assemblies are updated in place, even
when the assembly version changes. A managed import that removes a plugin type fails with "Unable
to delete plugintype due to 1 step(s) registered on it" while the type still has a step
downstream, or while a Custom API is still linked to it (Dataverse reports both as a registered
step). Delete the step or unlink the handler manually in each downstream environment before
deploying the release that removes the type.

#### `step config set` — Set plugin step configuration

Sets the unsecure and/or secure configuration for an existing plugin step. Steps can be identified
either by their SDK message processing step ID or by a composite key (plugin type, message, stage, entity).
Configuration values can be provided inline or read from UTF-8 encoded files.

##### Usage

```bash
dgtp plugin step config set --step-id 00000000-0000-0000-0000-000000000000 --unsecure "{\"key\":\"value\"}"
dgtp plugin step config set --plugin-type MyNamespace.MyPlugin --message Create --stage PreOperation --entity account --unsecure "{\"key\":\"value\"}"
dgtp plugin step config set --step-id 00000000-0000-0000-0000-000000000000 --secure "secret-value" --unsecure "{\"key\":\"value\"}"
dgtp plugin step config set --step-id 00000000-0000-0000-0000-000000000000 --unsecure-file ./config.json --secure-file ./secrets.json
```

##### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `--step-id` | No (mutually exclusive with composite key) | SDK message processing step ID |
| `--plugin-type` | No (part of composite key) | Fully qualified plugin type name |
| `--message` | No (part of composite key) | SDK message name (Create, Update, Delete, etc.) |
| `--stage` | No (part of composite key) | Stage name or numeric value: PreValidation/10, PreOperation/20, MainOperation/30, PostOperation/40. Other numeric values are rejected. |
| `--entity` | No (part of composite key) | Primary entity logical name |
| `--secondary-entity` | No (part of composite key) | Secondary entity logical name (for Associate, Disassociate, etc.) |
| `--execution-order` | No (part of composite key) | Rank/execution order of the step |
| `--unsecure` | No (at least one config required) | Inline unsecure configuration value. Use empty string to clear |
| `--unsecure-file` | No (mutually exclusive with --unsecure) | Path to UTF-8 file containing unsecure configuration |
| `--secure` | No (at least one config required) | Inline secure configuration value. Use empty string to clear |
| `--secure-file` | No (mutually exclusive with --secure) | Path to UTF-8 file containing secure configuration |

##### Notes

- Either `--step-id` or all required composite key options (`--plugin-type`, `--message`, `--stage`, `--entity`) must be provided, but not both. `--secondary-entity` and `--execution-order` are optional composite-key filters and cannot be used with `--step-id`.
- `--stage` accepts the listed names (case-insensitive) or numeric Dataverse stage values.
- `MainOperation` is supported for custom APIs and virtual table data providers. Internal-only stages 80 and 90 are not supported.
- At least one of `--unsecure`, `--unsecure-file`, `--secure`, or `--secure-file` must be provided.
- When using file-based configuration, the file must exist and be UTF-8 encoded.
- Composite key matching requires exactly one step to match; multiple matches or no matches will fail. Multiple matches list step display names and IDs so you can retry with `--step-id`.
- Secure configuration is stored in the `SdkMessageProcessingStepSecureConfig` entity.

#### `enable` — Enable a plugin step

Enables an existing plugin step. Steps can be identified by step ID, composite key, or solution name (applies to ALL steps in the solution).

##### Usage

```bash
dgtp plugin step enable --step-id 00000000-0000-0000-0000-000000000000
dgtp plugin step enable --plugin-type MyNamespace.MyPlugin --message Create --stage PreOperation --entity account
dgtp plugin step enable --solution mysolution
```

##### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `--step-id` | No (mutually exclusive with composite key and --solution) | SDK message processing step ID |
| `--plugin-type` | No (part of composite key, mutually exclusive with --step-id and --solution) | Fully qualified plugin type name |
| `--message` | No (part of composite key, mutually exclusive with --step-id and --solution) | SDK message name (Create, Update, Delete, etc.) |
| `--stage` | No (part of composite key, mutually exclusive with --step-id and --solution) | Stage name or numeric value: PreValidation/10, PreOperation/20, MainOperation/30, PostOperation/40 |
| `--entity` | No (part of composite key, mutually exclusive with --step-id and --solution) | Primary entity logical name |
| `--secondary-entity` | No (part of composite key, mutually exclusive with --step-id and --solution) | Secondary entity logical name (for Associate, Disassociate, etc.) |
| `--execution-order` | No (part of composite key, mutually exclusive with --step-id and --solution) | Rank/execution order of the step |
| `-s, --solution` | No (mutually exclusive with --step-id and composite key) | Unique name of the solution; applies to ALL steps in that solution |
| `--reason` | No | Reason for the state change; replaces the step's description when the state actually changes |
| `--dry-run` | No | Show which steps would change without modifying anything |

##### Notes

- Steps that are already in the desired state are left untouched (no write, reason not stored) and reported as unchanged.
- The solution name must match exactly. Solutions without plugin steps report that no steps were found.
- A failing step does not stop the remaining steps in `--solution` mode; the command exits with an error if any step failed.
- Exactly one of the following must be provided: `--step-id`, all required composite key options (`--plugin-type`, `--message`, `--stage`, `--entity`), or `--solution`.
- `--secondary-entity` and `--execution-order` are optional composite-key filters and cannot be used with `--step-id` or `--solution`.
- Composite key matching requires exactly one step to match; multiple matches or no matches will fail. Multiple matches list step display names and IDs so you can retry with `--step-id`.

#### `disable` — Disable a plugin step

Disables an existing plugin step. Uses the same identification options as `enable`.

##### Usage

```bash
dgtp plugin step disable --step-id 00000000-0000-0000-0000-000000000000
dgtp plugin step disable --plugin-type MyNamespace.MyPlugin --message Create --stage PreOperation --entity account
dgtp plugin step disable --solution mysolution
```

##### Options

Same as [`enable`](#enable--enable-a-plugin-step).

#### `state set` — Bulk enable/disable plugin steps from config file

Applies enable/disable state to multiple plugin steps defined in a JSON configuration file. Steps can be identified by ID or by composite key (plugin type, message, stage, entity).

##### Usage

```bash
dgtp plugin step state set -c step-states.json
```

##### Config file format

```json
{
  "steps": [
    {
      "stepId": "00000000-0000-0000-0000-000000000000",
      "enabled": false,
      "reason": "Temporarily disabled for deployment"
    },
    {
      "pluginType": "MyNamespace.MyPlugin",
      "message": "Create",
      "stage": 20,
      "entity": "account",
      "enabled": true
    }
  ]
}
```

##### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `-c, --config` | Yes | Path to the JSON config file |
| `--dry-run` | No | Show which steps would change without modifying anything |

##### Notes

- The config file can reference the [JSON schema](schemas/plugin/state/schema.json) for editor validation.
- Each step must set `enabled` and have either `stepId` or a complete composite key (`pluginType`, `message`, `stage`, `entity`); mixing both is rejected.
- Invalid or unresolvable entries are reported and do not stop the remaining entries; the command exits with an error if any entry failed.
- Steps already in the desired state are left untouched and reported as unchanged.
- `stage` accepts numeric values: 10 (PreValidation), 20 (PreOperation), 30 (MainOperation), 40 (PostOperation).
- Composite key matching requires exactly one step to match; multiple matches or no matches will fail.
- If a `reason` is provided in the config, it replaces the step's description when the state changes.
- The command reports a summary of enabled, disabled, and failed steps.

### `webresource` — Manage webresources

Commands for deploying Dataverse webresources.

#### `push` — Deploy webresources

Pushes webresources from a directory or a single file. Directory targets are scanned recursively
and require an explicit publisher prefix. A solution is used for membership and obsolete-resource
scope; it does not determine the publisher prefix. Created and updated webresources are each
published in a separate request after resource writes and solution-membership additions complete.

##### Usage

```bash
# Push all supported files below ./webresources
dgtp webresource push ./webresources \
  --publisher-prefix contoso \
  --solution ContosoCore

# Push one file with an explicit Dataverse logical name
dgtp webresource push ./webresources/app/main.js \
  --name contoso_/app/main.js \
  --solution ContosoCore
```

##### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `--solution` | No | Ensures resource membership in the solution and scopes `--delete-obsolete` |
| `--publisher-prefix` | Directory targets | Prefix used to derive unmapped Dataverse logical names |
| `--mapping-file` | No | JSON file mapping directory-relative paths to logical names |
| `--name` | Single-file targets | Explicit Dataverse logical name |
| `--delete-obsolete` | No | Deletes unmanaged resources in the selected solution that are absent from a directory target |
| `--dry-run` | No | Renders the complete plan without Dataverse writes |
| `--confirm` | No | Prompts before applying a plan with changes; suppressed by `--non-interactive` and CI |

##### Mapping file

Use `--mapping-file` when the local build layout does not match the desired Dataverse names:

```bash
dgtp webresource push ./webresources \
  --publisher-prefix contoso \
  --mapping-file ./webresource-mappings.json \
  --solution ContosoCore
```

The mapping file is JSON. Its keys are paths relative to the directory target, and its values are
complete Dataverse webresource logical names:

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/webresource/schema.json",
  "mappings": {
    "app/main.js": "contoso_/scripts/main.js",
    "pages/index.html": "contoso_/pages/home.html"
  }
}
```

Mappings override the default `publisherPrefix_/relative/path` naming convention for the paths they
specify; other supported files continue to use the default naming convention. This allows selective
remapping, such as migrating only legacy webresources. The schema is available at
[`schemas/webresource/schema.json`](schemas/webresource/schema.json). The file must contain a
non-null `mappings` object; malformed mapping files stop deployment with an error. Every mapping
key must match a supported file path relative to the target directory. Unmatched entries (including
mappings for files that are absent or unsupported) stop deployment rather than being silently
ignored.

##### Planning and execution

The command first renders the local deployment hierarchy and the resulting Dataverse name for
each resource:

```text
./webresources
├── app
│   └── main.js → contoso_/scripts/main.js ~ Update
└── pages
    └── index.html → contoso_/pages/home.html

Solution membership: ContosoCore
WebResource contoso_/scripts/main.js + Add

Obsolete webresources
WebResource contoso_/obsolete.js - Delete

Destructive operations: 1
```

When `--confirm` is specified, the command asks before executing a plan that creates, updates,
adds resources to a solution, or deletes obsolete resources. Declining leaves Dataverse unchanged.
The prompt is skipped in CI and when `--non-interactive` is set; `--dry-run` never prompts.

The tree shows local directory structure; mapping files may intentionally produce a different
Dataverse naming structure. For a normal run, the tree is followed by an execution phase with
checkmarks for completed operations. With `--dry-run`, the tree, missing solution memberships,
and any obsolete resources to delete are rendered without applying changes. Obsolete deletion
requires a directory target and `--solution`. An empty directory is a no-op, including when
`--delete-obsolete` is specified; it cannot be used to delete every webresource from a solution.

### `telemetry` — Manage telemetry settings

| Command | Description |
|---------|-------------|
| `telemetry status` | Show current telemetry state and the telemetry notice |
| `telemetry enable` | Persistently enable telemetry |
| `telemetry disable` | Persistently disable telemetry |

## 🔄 CI/CD Integration

> **Scope:** this section is specific to **Azure Pipelines**, since the auth mechanisms described
> here (service connections, Workload Identity Federation, `System.AccessToken`) are Azure
> DevOps concepts. Other CI systems (GitHub Actions, etc.) aren't covered here — use
> the global `DGTP_CONNECTION_STRING` environment variable or `--connection-string` option with
> whatever secret-management approach your platform provides.

dgtp is commonly driven from an Azure Pipelines job to authenticate against Dataverse without any
interactive login. How you provide credentials to a dgtp command depends on how the underlying
**Power Platform service connection** is configured. This section covers the setups
in use today; each subsection is self-contained.

### Client Secret service connection

If the service connection still uses a **Client Secret** (an app registration's `applicationId` +
`clientSecret`), extract a full Dataverse connection string from it using the Power Platform Build
Tools' `PowerPlatformSetConnectionVariables` task — wrapped by the reusable
[`azure-pipeline-templates/xrm-connection/build-connectionstring-from-service-connection.yml`](https://github.com/DIGITALLNature/DigitallPipelines/blob/beta/azure-pipeline-templates/xrm-connection/build-connectionstring-from-service-connection.yml)
template in [DIGITALLNature/DigitallPipelines](https://github.com/DIGITALLNature/DigitallPipelines)
— and pass the resulting secret variable to the command as `DGTP_CONNECTION_STRING`. The string is
used for that invocation only; it is not saved as a named connection:

```yaml
resources:
  repositories:
    - repository: pipelinetemplates
      type: github
      name: DIGITALLNature/DigitallPipelines
      endpoint: DIGITALL Pipelines Service Connection

steps:
  - template: azure-pipeline-templates/xrm-connection/build-connectionstring-from-service-connection.yml@pipelinetemplates
    parameters:
      serviceConnection: 'MyPowerPlatformConnection'
      url: 'https://contoso.crm4.dynamics.com'   # optional; falls back to $(BuildTools.EnvironmentUrl) / $(PowerPlatformUrl) if omitted

  - script: dgtp export bulkdeletes --filedir ./output
    env:
      DGTP_CONNECTION_STRING: $(PowerPlatformConnectionString)
    displayName: 'Export bulk-delete jobs using the client-secret service connection'
```

This flow depends on the service connection actually having a client secret to extract — which is
exactly what breaks when a service connection is switched to Workload Identity Federation (see
below), since there's no secret left to read.

### Workload Identity Federation (OIDC) service connection

When the service connection uses **Workload Identity Federation** instead of a client secret, no
secret is ever available to build a traditional connection string. Use `dgtp connection create
--azure-devops-federated` instead — dgtp exchanges the pipeline job's short-lived OIDC token for
an Entra ID access token at connect time, via
[`Azure.Identity.AzurePipelinesCredential`](https://aka.ms/azsdk/net/identity/azurepipelinescredential/usage).

#### Recommended: `--service-connection-name`

Pass just the service connection's name — dgtp resolves the environment URL, tenant, application
and service-connection IDs itself via the Azure DevOps REST API
(`GET .../_apis/serviceendpoint/endpoints?endpointNames=<name>&type=powerplatform-spn&api-version=7.1`).
No external template or manual REST call is needed:

```yaml
steps:
  - script: >-
      dgtp connection create prod
      --azure-devops-federated
      --service-connection-name "MyPowerPlatformConnection"
      --no-verify
    env:
      SYSTEM_ACCESSTOKEN: $(System.AccessToken)
    displayName: 'Create dgtp connection via Azure DevOps workload identity federation'
```

`SYSTEM_ACCESSTOKEN` is the only variable that needs explicit mapping — `System.TeamFoundationCollectionUri`
and `System.TeamProjectId` (used to build the lookup URL) are already available as environment
variables on every pipeline job without any extra configuration.

> **No extra pipeline task is needed for the OIDC exchange itself.**
> `Azure.Identity.AzurePipelinesCredential` needs a `SYSTEM_OIDCREQUESTURI` value, which Azure
> DevOps only populates automatically for a few built-in tasks (e.g. `AzureCLI@2`,
> `AzurePowerShell@5`) that declare an ARM service connection input — a plain `script` step never
> gets it for free. Rather than requiring one of those tasks purely as a trigger, dgtp derives the
> same URL itself from predefined job variables (`System.CollectionUri`, `System.TeamProjectId`,
> `System.HostType`, `System.PlanId`, `System.JobId`) that Azure DevOps always exposes as
> environment variables, matching the [OIDC token creation REST endpoint](https://learn.microsoft.com/en-us/rest/api/azure/devops/distributedtask/oidctoken/create).

> **Permissions:** the pipeline's build identity (usually `Project Collection Build Service`)
> needs at least **Reader** access to the service connection to call the endpoints API —
> grant it under the service connection's **Security** tab if dgtp reports a `401`/`403` or
> "no service connection found".

> **Duplicate names:** service connections can be organized into folders, so the same name can
> exist more than once within a project. If the lookup finds more than one match, dgtp fails with
> an error listing the candidate IDs — switch to the explicit option below to disambiguate.

#### Advanced: explicit `--tenant` / `--client-id` / `--service-connection-id` / `--url`

Bypass the REST lookup entirely by passing all four values yourself. Useful when the build
identity can't be granted Reader access, the service connection name is ambiguous, or the agent's
network policy blocks calls to the Azure DevOps REST API:

```yaml
steps:
  - script: >-
      dgtp connection create prod
      --url https://contoso.crm4.dynamics.com
      --azure-devops-federated
      --tenant <tenantId>
      --client-id <clientId>
      --service-connection-id <serviceConnectionId>
      --no-verify
    env:
      SYSTEM_ACCESSTOKEN: $(System.AccessToken)
    displayName: 'Create dgtp connection via Azure DevOps workload identity federation'
```

These four values are non-secret and can still be read from the service connection ahead of time —
either with a manual REST call, or via the reusable
[`azure-pipeline-templates/xrm-connection/resolve-service-connection.yml`](https://github.com/DIGITALLNature/DigitallPipelines/blob/beta/azure-pipeline-templates/xrm-connection/resolve-service-connection.yml)
template in [DIGITALLNature/DigitallPipelines](https://github.com/DIGITALLNature/DigitallPipelines)
if you want the lookup to happen as a separate, dgtp-independent step. Both are optional now that
`--service-connection-name` covers the common case directly.

No environment URL, tenant/application/service-connection ID needs to be hardcoded when using
`--service-connection-name` — dgtp resolves all four from the service connection **name** at
runtime, the same way you'd reference it in any built-in task's `azureSubscription` input.

## 🏗 Solution Architecture

DigitallPower is built as a modular CLI. The host project (`dgt.power`) wires up dependency injection (`Microsoft.Extensions.DependencyInjection`), configuration (`Microsoft.Extensions.Configuration`) and the [Spectre.Console.Cli](https://spectreconsole.net/cli/) command framework, and then registers commands contributed by independent feature modules.

```
                ┌──────────────────────────────────────────┐
                │              dgt.power (dgtp)            │
                │  Program.cs · DI container · CLI host    │
                └────────────────┬─────────────────────────┘
                                 │ references
       ┌─────────────────────────┼─────────────────────────────┐
       │            │            │            │                │
┌──────┴─────┐ ┌────┴─────┐ ┌────┴─────┐ ┌────┴──────┐  ┌──────┴───────┐
│ connection │ │  export  │ │  import  │ │  analyze  │  │ maintenance  │
└────────────┘ └──────────┘ └──────────┘ └───────────┘  └──────────────┘
       │            │            │            │                │
       └──────┬─────┴────────────┴────────────┴────────────────┘
              │
       ┌──────────┴───────────────┬──────────────────────────────┐
       │ codegeneration / process │ plugin       │ webresource   │
       └──────────────────────────┴──────────────┴───────────────┘
                               │
                               ▼
                  ┌──────────────────────────┐
                  │     dgt.power.common     │
                  │  (Dataverse connection,  │
                  │   file access, command   │
                  │   lifecycle, connection  │
                  │   management)            │
                  └──────────────────────────┘
                               │
                               ▼
                ┌──────────────────────────────────┐
                │  Microsoft.PowerPlatform         │
                │  .Dataverse.Client / Xrm SDK     │
                └──────────────────────────────────┘
```

Key design principles:

- **Module isolation.** Every feature area (`analyzer`, `codegeneration`, `connection`, `export`, `import`, `maintenance`, `plugin`, `process`, `teamtemplates`, `webresource`) is an independent project under `src/modules/`. Modules expose `Spectre.Console.Cli`-style command classes that are registered by the host. The process module uses shared ownership and caller-service contracts without depending on maintenance internals.
- **Shared kernel.** `dgt.power.common` provides the cross-cutting infrastructure: `IDataverseConnection`, parsed per-invocation connection settings, typed connection storage and credential construction, protected-storage warnings, file I/O helpers (including the public `FileAccess/AtomicFileWriter` helper for shared atomic file writes), shared Dataverse query paging, the `PowerCommand<TSettings>` lifecycle base class, telemetry and exception types, plus shared runtime environment helpers (`ExecutionEnvironment`) used by multiple modules. The process module now delegates its atomic writes to this shared helper without changing process command behavior.
- **DI everywhere.** Long-lived services (HTTP/NuGet clients, connection store, caches, JSON options) are singletons; per-command services (metadata, config resolver, generators, file service) are scoped. Commands that need Dataverse request `IDataverseConnection` and call `GetServiceAsync()`; the connection is established on first use and cached, so commands that do not need Dataverse never authenticate.
- **Command output.** Every command ends with a rule showing the result and duration (`✔ Command finished in 2.3s` or `✘ Command failed in 0.4s`). Exit codes are unchanged: `0` success, `1` failure, `2` authentication required.
- **Stable connection storage.** `connections.json` and `state.json` live under the per-user dgtp home; secrets and the Azure.Identity token cache use OS-protected persistence.
- **Update awareness.** A `VersionCheckInterceptor` queries NuGet on each run to warn the user when a newer version of `dgt.power` is available.

### Caller-specific Dataverse services

Consumers of `IDataverseConnection` can acquire an isolated service for a Dataverse
`systemuser` ID (not an Entra object ID):

```csharp
var standardService = await connection.GetServiceAsync();
var callerService = await connection.GetServiceAsync(systemUserId);
await callerService.ExecuteAsync(request, cancellationToken);
```

The invocation retains its selected environment and authentication configuration and lazily
caches one independent service per caller. An empty ID is rejected before authentication.
Services are borrowed: the connection disposes the standard and caller services; consumers
must not dispose them or mutate their caller state.

Acquisition guarantees a ready, caller-configured service, not that the caller exists, is
enabled, or has permission for subsequent operations. There is no caller-specific `WhoAmI`
or privilege probe, and Dataverse faults propagate without an identity fallback. Consumers
requiring no writes on acquisition failure must acquire all required services before writing;
later operations can still fail. The API itself adds no global CLI options; `process apply`
exposes resource-specific impersonation options described above.

## 📁 Repository Layout

The working specifications and active change proposals live in `openspec/`; concise records of durable architectural rationale live in `docs/architecture/decisions/`.

```
DigitallPower/
├── src/
│   ├── dgt.power/                # CLI host project (produces the `dgtp` tool)
│   ├── dgt.power.common/         # Shared infrastructure (typed connections, IO, command lifecycle)
│   ├── models/                   # Shared DTOs / data contracts
│   └── modules/
│       ├── dgt.power.analyzer/        # `analyze` commands
│       ├── dgt.power.codegeneration/  # `codegeneration` / `cg` command
│       ├── dgt.power.connection/      # `connection` commands (auth management)
│       ├── dgt.power.export/          # `export` commands
│       ├── dgt.power.import/          # `import` commands
│       ├── dgt.power.maintenance/     # `maintenance` commands
│       ├── dgt.power.plugin/          # `plugin` commands
│       ├── dgt.power.process/         # `process init/apply` commands
│       ├── dgt.power.duplicaterules/  # `duplicaterules init/apply` commands
│       ├── dgt.power.solution/        # `solution` commands (e.g. `solution lint`)
│       └── dgt.power.webresource/     # `webresource push` command
├── tests/                        # Unit and integration tests
├── samples/                      # Example inputs (configs, plugin samples)
├── schemas/                      # JSON schemas for configuration files
├── docs/                         # Migration guides and developer design proposals
├── scripts/                      # Repository maintenance scripts
├── Directory.Build.props         # Common MSBuild properties
├── global.json                   # Pinned .NET SDK
└── DigitallPower.sln             # Solution file
```

## 🛠️ Build & Test

```bash
pnpm install                 # Install JS tooling, TypeScript compiler, and local OpenSpec CLI
dotnet restore                # Restore dependencies (uses lock files)
dotnet build                  # Build the solution
dotnet test                   # Run all tests
dotnet test --filter "Name~Foo"   # Run a subset of tests
pnpm exec openspec validate --specs  # Validate current OpenSpec specifications
```

## Spec-Driven Development

Use [OpenSpec](https://github.com/Fission-AI/OpenSpec) for significant changes to public CLI/API
behavior, configuration and schemas, architecture, module boundaries, or behavior spanning
capabilities. Start with `/opsx-propose`; after review, implement with `/opsx-apply`, update the
main specs, validate them, and submit the implementation PR. Keep the change active until that PR
has successfully merged into `beta` or `main`. Archive with `/opsx-archive` only in a separate
post-merge follow-up, after verifying the PR is `MERGED`, has a merge timestamp, and targets one
of those exact branch names. Completed tasks, an open or approved PR, green CI, a closed-but-unmerged
PR, or a merge into another branch do not permit archiving. If merge verification is unavailable,
leave the change active and report the blocker. Small fixes, test-only changes, and
behavior-preserving refactors do not require a proposal. The CLI is installed locally with pnpm
and is run as `pnpm exec openspec`.

Current requirements live in [`openspec/specs/`](openspec/specs/). Durable design rationale lives in
[`docs/architecture/decisions/`](docs/architecture/decisions/). The README remains the user-facing
command and configuration reference; implementation details belong beside their code and tests.
Run `pnpm run openspec:update` when refreshing generated Copilot files; it restores local `pnpm exec`
CLI examples and the post-merge archive gate after OpenSpec updates them. Validate this
normalization with `node --test scripts/normalize-openspec-commands.test.mjs`.

GitHub Copilot cloud agent support is opt-in through `githubCopilot.cloudAgent` in
[`openspec/config.yaml`](openspec/config.yaml). Its setup workflow installs the locked pnpm
dependencies and .NET SDK before an agent session; OpenSpec commands continue to use
`pnpm exec openspec`. The setup workflow must be present on the default branch, and cloud-agent
access must be enabled by the repository or organization administrator.

### Graphify Code-Only Pilot

The pilot pins Graphify in [`graphify-version.txt`](graphify-version.txt) and builds a fresh C# code
graph locally, in GitHub Copilot cloud-agent setup, and in a GitHub Actions smoke check. The graph,
manifest, and cache stay under ignored `graphify-out/`; they are regenerated from the current
checkout and are not committed. Code extraction uses local AST parsing only. Documentation, images,
and semantic LLM extraction are out of scope.

For a local clone, install the pinned CLI and create the initial graph:

```bash
uv tool install graphifyy==0.9.80
graphify vscode install
graphify extract . --code-only
```

`graphify vscode install` adds the local VS Code skill and refreshes the project Copilot instructions.
The same tracked instructions guide GitHub Web agents. `pnpm install` activates Husky hooks:
Graphify refreshes after commits and branch switches, while post-merge and post-rewrite hooks refresh
after pulls and rebases. These are best-effort; if a refresh is skipped, run `sh scripts/graphify-refresh.sh`.
Cloud-agent setup builds the graph for each current checkout, and the GitHub Actions smoke workflow
checks that code-only extraction and a representative plugin path succeed without committing output.

Use Graphify for cross-module navigation and verify conclusions against source and tests. The measured
results and retention criteria are in [the evaluation note](docs/architecture/graphify-pilot-evaluation.md).

The separate DigitallPower pilot override in `.github/copilot-instructions.md` preserves code-only
mode when the Graphify-owned section is refreshed. `graphify copilot install` targets Copilot CLI
and is not needed for VS Code or GitHub Web.

To produce a local NuGet package of the tool:

```bash
dotnet pack src/dgt.power/dgt.power.csproj -c Release
```

The resulting `.nupkg` is placed in the `packages/` folder and can be installed locally with:

```bash
dotnet tool install --global --add-source ./packages dgt.power --version <version>
```

## ✅ Requirements

- **.NET SDK 10.0** (the SDK version is pinned via [`global.json`](global.json))
- **Node.js 22 + pnpm** (needed for the TypeScript compile gate in `dgt.power.codegeneration.tests`)
- Network access to your Dataverse environment (`*.dynamics.com`) and to `api.nuget.org` (for the version check)
- An account with sufficient privileges on the target Dataverse environment

## 📡 Telemetry

DigitallPower collects anonymous usage telemetry to help improve the tool. Telemetry is **opt-out** — it is enabled by default but can be easily disabled.

### What is collected

| Data | Example | Purpose |
|------|---------|---------|
| Command name | `UserRoleImport` | Understand which modules are used |
| Success/failure | `true` | Track reliability |
| CI environment | `true` | Distinguish interactive vs automated usage |
| OS platform | `Unix` | Platform distribution |
| Tool version | `2.1.0` | Version adoption |
| Anonymous install ID | `a1b2c3d4-...` | Count unique installations |
| Crash data | `ExceptionType`, `anonymized stacktrace` | Improve reliability by identifying common crashes |

**No personally identifiable information is collected.** Error messages and stack traces are automatically anonymized before being sent — the following data is stripped or replaced with fixed placeholders:

| Found in error data | Replaced with |
|---|---|
| GUIDs (record IDs, solution IDs, ...) | `00000000-0000-0000-0000-000000000000` |
| Local home-directory paths (`/Users/<name>/...`, `/home/<name>/...`, `C:\Users\<name>\...`) | just the file name, e.g. `Program.cs` |
| Dataverse organization URLs (`https://contoso.crm4.dynamics.com/...`) | `[dataverse-org-url-redacted]` |
| Entra ID tenant URLs (`https://contoso.onmicrosoft.com/...`) | `[entra-tenant-url-redacted]` |

This anonymization is implemented in `TelemetryAnonymizer` and covered by unit tests for both Unix and Windows path formats. It is a best-effort, regex-based approach; it does not detect arbitrary free-form personal information.

CI detection is centralized in `dgt.power.common.ExecutionEnvironment` and currently recognizes `TF_BUILD`, `BUILD_BUILDURI`, `GITHUB_ACTIONS`, `GITLAB_CI`, `JENKINS_URL`, and `CI`.

### Managing telemetry

```bash
dgtp telemetry status    # Show current state and the telemetry notice
dgtp telemetry enable    # Persistently enable telemetry
dgtp telemetry disable   # Persistently disable telemetry
```

**Per invocation:**

```bash
dgtp export --no-telemetry
```

**Environment variable (takes precedence over the saved preference):**

```bash
export DGTP_TELEMETRY_OPTOUT=1
```

Set `DGTP_TELEMETRY_OPTOUT` or the standard `DO_NOT_TRACK` variable to `1`, `true`, or `yes` to permanently disable telemetry.

**Override telemetry endpoint (advanced):**

```bash
export DGTP_TELEMETRY_CONNECTION_STRING="InstrumentationKey=..."
```

Set `DGTP_TELEMETRY_CONNECTION_STRING` to an Azure Monitor connection string to route telemetry to a custom endpoint. When not set, the build-time embedded connection string is used (or telemetry is disabled if none was embedded).

### Example query

To count how often each module was invoked, split by CI and non-CI usage:

```kusto
dependencies
| where timestamp > ago(30d)
| extend
    module = tostring(customDimensions["dgtp.command"]),
    is_ci = tolower(tostring(customDimensions["dgtp.is_ci"])) == "true"
| where isnotempty(module)
| summarize CI = countif(is_ci), NonCI = countif(not(is_ci)) by module
| order by CI + NonCI desc
```

### Retrieving crash/error data in Application Insights

Every exception (whether it terminates a command, or crashes the process entirely) is recorded both as an OpenTelemetry span attribute (for quick filtering alongside command telemetry) **and** as a standard OpenTelemetry exception event, which the Azure Monitor exporter surfaces natively in the **`exceptions`** table / the **Failures** blade of the Application Insights resource.

**To look at it in the Azure Portal:**

1. Open the Application Insights resource associated with the connection string configured via `DGTP_TELEMETRY_CONNECTION_STRING` (or the build's embedded default).
2. Go to **Investigate → Failures** for a quick overview of the most frequent exception types and their trend over time, or
3. Go to **Monitoring → Logs** and run a KQL query directly, e.g.:

```kusto
// Most frequent (anonymized) exception types over the last 30 days
exceptions
| where timestamp > ago(30d)
| extend
    exceptionType = tostring(customDimensions["exception.type"]),
    version = tostring(customDimensions["dgtp.version"]),
    isFatal = operation_Name == "fatal_exception"
| summarize Count = count() by exceptionType, version, isFatal
| order by Count desc
```

```kusto
// Full anonymized message + stack trace for a specific exception type
exceptions
| where timestamp > ago(30d)
| where tostring(customDimensions["exception.type"]) == "System.IO.DirectoryNotFoundException"
| project timestamp, message = tostring(customDimensions["exception.message"]), stack = tostring(customDimensions["exception.stacktrace"]), version = tostring(customDimensions["dgtp.version"])
| order by timestamp desc
```

Since crashes outside of a command's lifecycle (startup failures, unobserved task exceptions) are recorded as their own `fatal_exception` operation rather than under a `command.*` operation, filter on `operation_Name == "fatal_exception"` to isolate those from regular command-scoped exceptions.

### First-run notice

On the first command invocation with telemetry enabled, the CLI creates an anonymous installation
ID and displays a one-time notice explaining collection and opt-out. Invocations with
`--no-telemetry` or an environment opt-out do not create this ID or display the notice. Help,
version output, and shell completion do not initialize telemetry.

The installation ID is retained across tool upgrades, including major releases, so the notice
does not repeat. Disabling and re-enabling telemetry does not reset it. Using a new application
home causes the notice to appear again on the next telemetry-enabled command.

### Persistent application state

The anonymous installation ID, telemetry preference, and last update-check date are stored in
`state.json` alongside `connections.json` in the per-user application data directory.
Set `DGTP_HOME` to override this location. The directory is shared across tool versions.
On Windows, the default is `%LOCALAPPDATA%\dgtp`; on Linux, it is `$XDG_DATA_HOME/dgtp`
or `~/.local/share/dgtp`. Other platforms use the local application data directory's `dgtp` folder.

To change the saved telemetry preference, use `dgtp telemetry enable` or `dgtp telemetry disable`.
For advanced use, `state.json` can still be edited directly while dgtp is not running — set
`telemetry.enabled` to `false` or `true`, leaving the other fields unchanged. It defaults to `true`.
Environment opt-outs and `--no-telemetry` always take precedence over this preference.

## ❤️ Community and Contributions

DigitallPower CLI is a **community-driven open source project** backed by DIGITALL. We are committed to a fully transparent development process and **highly appreciate any contributions**. Whether you are helping us fixing bugs, proposing new features, improving our documentation or spreading the word — **we would love to have you as part of the DigitallPower community**.

### 📫 Have a question? Want to chat? Ran into a problem?

We are happy to answer your questions via [GitHub Discussions](https://github.com/DIGITALLNature/DigitallPower/discussions).

### 🤝 Found a bug? Missing a specific feature?

Feel free to **file a new issue** with a descriptive title on the [DigitallPower](https://github.com/DIGITALLNature/DigitallPower/issues) repository. If you already found a solution to your problem, **we would love to review your pull request**. Have a look at our [contribution guidelines](https://github.com/DIGITALLNature/DigitallPower/blob/main/contributing.md) to find out about our coding standards and the conventional-commits workflow used in this repository.

## 📘 License

DigitallPower CLI is released under the terms of the [MS-RL License](Licence.md).
