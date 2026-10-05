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
- [Command Reference](#-command-reference)
  - [connection](#connection--authentication--environments)
    - [Authentication types](#authentication-types)
  - [export](#export--export-dataverse-artifacts)
  - [import](#import--import-dataverse-artifacts)
  - [analyze](#analyze--solution-analysis)
  - [solution](#solution--solution-scoped-operations)
  - [maintenance](#maintenance--operational-tasks)
  - [codegeneration](#codegeneration-cg--early-bound-code-generation)
  - [plugin](#plugin--manage-plugin-assembliespackages)
  - [webresource](#webresource--deploy-webresources)
- [CI/CD Integration](#-cicd-integration)
  - [Client Secret service connection](#client-secret-service-connection)
  - [Workload Identity Federation (OIDC) service connection](#workload-identity-federation-oidc-service-connection)
- [Solution Architecture](#-solution-architecture)
- [Repository Layout](#-repository-layout)
- [Build & Test](#%EF%B8%8F-build--test)
- [Requirements](#-requirements)
- [Community & Contributions](#%EF%B8%8F-community-and-contributions)
- [License](#-license)

## ✨ Features at a Glance

| Area | What it does |
|------|--------------|
| **Connections** | Manage typed Dataverse connections (interactive, device-code, client-secret, certificate, Azure DevOps federation) and check authentication status |
| **Export** | Extract configuration data (team templates, queues, SLAs, calendars, routing rules, document/Outlook templates, user roles, bulk delete jobs) from an environment |
| **Import** | Import the previously exported artifacts into another environment — ideal for ALM pipelines |
| **Analyze** | Inspect solutions for redundant components, active-layer issues, top-layer problems and obsolete patches |
| **Solution** | Run configuration-driven Dataverse quality gates (`solution lint`) such as unmanaged field naming and table completeness checks against a single solution, increment solution versions (`solution version`), and copy solution components between solutions (`solution copy-components`) |
| **Maintenance** | Bulk-delete records, manage auto-number formats, protect calculated fields, increment solution versions, update workflow states, filter PowerFx plugin steps, ensure SDK step status, and more |
| **Code Generation** | Generate strongly-typed C# (early-bound), TypeScript and metadata files for Dataverse entities |
| **Plugin** | Deploy plugin assemblies and packages with `plugin push`; manage step configuration with `plugin step config set` |
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
| `dgtp <TAB>` | `export` `import` `maintenance` `analyze` `connection` `codegeneration` `plugin` `webresource` `complete` |
| `dgtp export <TAB>` | `teamtemplates` `bulkdeletes` `queues` … |
| `dgtp export --<TAB>` | `--filedir` `--filename` `--inline` `--no-telemetry` |
| `dgtp connection <TAB>` | `list` `create` `delete` `select` `status` `refresh` |

> **Note:** Tab completion is static (command names and option flags only). It does not connect to Dataverse and requires no network access.

## ⚙️ Configuration

The former global `dgtp.json` configuration and `dgtp:*` environment-variable binding have been removed. Commands accept their own options and configuration files. For example, `maintenance bulkdelete` accepts `--poll-interval` (seconds; default `5`) to control how often it checks the asynchronous job.

Saved connections and application state use a stable per-user data directory instead of assembly-scoped isolated storage. Set `DGTP_HOME` to override its location. Connections are stored in `connections.json`; client secrets and PFX passwords are stored separately using the platform's protected storage (DPAPI on Windows, Keychain on macOS, and Secret Service on Linux). No 2.x connections are migrated automatically.

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
| `export teamtemplates` | Team templates |
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
| `import teamtemplates` | Team templates |
| `import bulkdeletes` | Bulk delete jobs |
| `import documenttemplates` | Document templates |
| `import secureconfigs` | Secure configurations of plugin steps |
| `import calendar` | Calendars |
| `import slaconfigs` | SLAs |
| `import routingruleconfigs` | Routing rules |

```bash
dgtp import outlooktemplates --filedir ./out/outlooktemplates
```

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
| `maintenance createworkflowstate` | Generate a workflow-state configuration file |
| `maintenance workflowstate` | Apply a workflow-state configuration |
| `maintenance removeredundantcomponents <SourceSolutions> <TargetSolution> [--dryrun] [--includeEntities]` | Remove components from `TargetSolution` that already exist in `SourceSolutions` (comma-separated) |
| `maintenance filterfxplugins` | Add message filtering for PowerFx plugin steps |
| `maintenance ensuresdksteps` | Enable/disable SDK steps within a solution |

`maintenance bulkdelete` accepts `--poll-interval <seconds>` (default `5`) to set the interval
between asynchronous job status checks. The option is specific to this command.

```bash
dgtp maintenance bulkdelete --inline "<fetchxml>...</fetchxml>" --poll-interval 10
```

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

JSON schemas for all config versions are available under [`schemas/codegeneration/`](schemas/codegeneration).

#### V2 config (recommended)

V2 configs use `"version": 2` and a `"type"` discriminator to produce one focused file per output target. The design separates two concerns:

- **Scope** — what to load from Dataverse (`entities`, `requests`, `optionSets`)
- **Output** — what artefacts to write (type-specific `output` object)

Only `"type"` is required; every other property has a sensible default and may be omitted.

> **Tip:** Run one command per config file — `dgtp cg ./generated -c ./genconfig.dotnet.json` and `dgtp cg ./generated -c ./genconfig.typescript.json`.

##### Shared root properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `version` | integer | `1` | Must be `2` to use the V2 engine |
| `type` | `"dotnet"` \| `"typescript"` | — | **Required.** Selects generator and schema |
| `namespace` | string \| null | `null` (TS) / `"Digitall.Dataverse.Model"` (.NET) | Root namespace for generated classes |
| `language` | integer \| null | `null` | LCID for label localization (e.g. `1033` for English). `null` or omitted = use the organization's base language |

##### Scope properties (shared by both types)

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `entities.names` | string[] | `[]` | Explicit list of entity logical names |
| `entities.fromSolutions` | string[] | `[]` | Include all entities from these solutions |
| `entities.mask` | string \| null | `null` | Publisher-prefix wildcard (e.g. `"contoso_*"`) |
| `requests` | string[] | `[]` | SDK message / custom action names; generates message constants |
| `optionSets` | string[] | `[]` | Global option set logical names |

The three entity inputs are combined as an **additive union** — an entity matches if it appears in `names`, belongs to any listed solution, or matches the `mask` pattern.

##### TypeScript output properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `output.forms` | object \| absent | *(all forms)* | Omit entirely to generate all forms for all scoped entities |
| `output.forms.filter` | string[] | `[]` | Restrict to specific forms by `"entityLogicalName.formName"`; empty = all forms |
| `output.forms.fromSolutions` | boolean | `false` | Only include forms that belong to the solutions listed in `entities.fromSolutions` |
| `output.forms.testHelpers` | boolean | `false` | Generate XrmMock test helper files alongside form helpers |
| `output.customApis` | boolean | `true` | Generate typed Custom API request/response wrappers for parameterised messages |

##### .NET output properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `output.target` | `"Modern"` \| `"Framework"` | `"Modern"` | `Modern` = net8.0+ (nullable, implicit usings); `Framework` = .NET Framework 4.6.2 (Dataverse plugins, no nullable) |
| `output.virtual` | boolean | `false` | Add the `virtual` keyword to generated entity properties, enabling mocking and subclass overrides |
| `output.editableReadOnly` | boolean | `false` | Treat read-only attributes as editable |
| `output.include.context` | boolean | `true` | Generate `DataContext` class |
| `output.include.options` | boolean | `true` | Generate `OptionSetValues` enum classes |
| `output.include.logicalNames` | boolean | `true` | Generate logical-name string constants |
| `output.include.relations` | boolean | `true` | Generate relationship metadata |
| `output.include.navigationProps` | boolean | `true` | Generate navigation properties |
| `output.include.entityTypeCode` | boolean | `true` | Generate entity type code constants |
| `output.include.alternateKeys` | boolean | `true` | Generate alternate-key members |
| `output.include.metadata` | boolean | `false` | Write `metadata.xml` sidecar files |

##### Minimal examples

Minimal .NET config (entities from a solution, all defaults):

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/codegeneration/v2/dotnet.schema.json",
  "version": 2,
  "type": "dotnet",
  "entities": { "fromSolutions": ["ContosoCore"] }
}
```

Minimal TypeScript config (entities from a solution, all forms, no test helpers):

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/codegeneration/v2/typescript.schema.json",
  "version": 2,
  "type": "typescript",
  "entities": { "fromSolutions": ["ContosoCore"] }
}
```

##### Full examples

**.NET** — `genconfig.dotnet.json`:

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/codegeneration/v2/dotnet.schema.json",
  "version": 2,
  "type": "dotnet",
  "namespace": "Contoso.Dataverse.Model",
  "entities": {
    "names": ["account", "contact"],
    "fromSolutions": ["ContosoCore"],
    "mask": "contoso_*"
  },
  "requests": ["contoso_ApproveOrder"],
  "optionSets": ["contoso_status"],
  "output": {
    "target": "Modern",
    "virtual": false,
    "editableReadOnly": false,
    "include": {
      "context": true,
      "options": true,
      "logicalNames": true,
      "relations": true,
      "navigationProps": true,
      "entityTypeCode": true,
      "alternateKeys": true,
      "metadata": false
    }
  }
}
```

**TypeScript** — `genconfig.typescript.json`:

```json
{
  "$schema": "https://raw.githubusercontent.com/DIGITALLNature/DigitallPower/main/schemas/codegeneration/v2/typescript.schema.json",
  "version": 2,
  "type": "typescript",
  "entities": {
    "names": ["account", "contact"],
    "fromSolutions": ["ContosoCore"],
    "mask": "contoso_*"
  },
  "requests": ["contoso_ApproveOrder"],
  "optionSets": ["contoso_status"],
  "output": {
    "forms": {
      "filter": ["account.Account Main Form"],
      "fromSolutions": false,
      "testHelpers": true
    },
    "customApis": true
  }
}
```

#### V1 config (legacy, deprecated)

V1 configs use a single file for both .NET and TypeScript output and are detected by `"version": 1` (or the absence of a `version` field). They continue to work unchanged — they are mapped internally to the V2 runtime shape before execution.

```json
{
  "version": 1,
  "Entities": ["account", "contact"],
  "Solutions": ["ContosoCore"],
  "Actions": ["contoso_ApproveOrder"],
  "GlobalOptionSets": ["contoso_status"],
  "NameSpace": "Contoso.Dataverse.Model",
  "TypescriptGeneratorVersion": "Light",
  "SuppressMetaData": true
}
```

##### V1 → V2 migration

| V1 property | V2 equivalent |
|-------------|--------------|
| `Entities` | `entities.names` |
| `Solutions` | `entities.fromSolutions` |
| `EntityMask` | `entities.mask` |
| `Actions` / `SdkMessages` | `requests` |
| `GlobalOptionSets` | `optionSets` |
| `NameSpace` | `namespace` |
| `XrmMockFormHelpers` | `output.forms.testHelpers` |
| `OnlyFormsFromSolutions` | `output.forms.fromSolutions` |
| `SuppressMetaData` | `output.include.metadata: false` |
| `Target` | `output.target` |

> **Note:** V1 attribute-level filters (`EntityFilters`, `EntityRefFilters`, `EntityFormFilters`) have no V2 equivalent — they were removed by design to keep the V2 schema focused.

#### TypeScript environment variables

| Environment variable | Purpose | Default |
|---|---|---|
| `DGTP_TSL_STRICT_MODE` | Enables fail-fast handling for undefined Liquid values (`1` / `true` / `yes`) | Falls back to CI-agent detection |
| `DGTP_TSL_MAX_STEPS` | Overrides Fluid template execution step limit with a positive integer | `20000` |

#### Known limitations

**Form names depend on the connection's UI language, not the configured `language`.** Dataverse resolves the
`name` attribute of out-of-box records (including system forms) using the *connecting user's personal UI
language setting* (`usersettings.uilanguageid`), not any per-request parameter. This means:

- Labels rendered *inside* generated form/option-set typings (e.g. option set labels) correctly follow the
  `language`/`UseBaseLanguage` config value, since those come from metadata `Label`/`LocalizedLabels`, which
  *do* support per-request LCID resolution.
- The system form's own `name` — used to build the generated file name and to match entries in `Forms` —
  is retrieved in whatever language the connecting user's Dataverse profile is set to. If that differs from
  the language configured for code generation, form names may not match `Forms` filter entries, and matching
  forms can be silently skipped.

`dgtp` detects this mismatch and prints a warning at generation time when the connection's UI language differs
from the configured language. **Mitigation:** set the connecting user's personal Dataverse UI language (Settings
→ Personalization Settings → Language) to match the `language` configured for code generation.

### `plugin` — Manage plugin assemblies/packages

Commands for deploying Dataverse plugin assemblies and packages.

#### `push` — Deploy plugin assemblies/packages

Registers a single plugin assembly (`.dll`), a plugin package (`.nupkg`), or every `.dll`/`.nupkg`
found directly in a directory (mixed content in one directory is supported; each file is processed
independently).

##### Usage

```bash
dgtp plugin push ./bin/Release/MyPlugin.dll --solution mysolution
dgtp plugin push ./bin/Release/MyPlugin.1.0.0.nupkg --publisher-prefix contoso --solution mysolution
dgtp plugin push ./bin/Release --publisher-prefix contoso --solution mysolution
```

##### Options

| Option | Required | Behavior |
|--------|----------|----------|
| `--solution` | No | Ensures package, standalone assembly, and declared plugin step membership in the given solution |
| `--publisher-prefix` | For `.nupkg` targets | Publisher customization prefix for plugin packages |
| `--dry-run` | No | Previews the deployment and solution membership plan without writing to Dataverse |
| `--confirm` | No | Prompts before executing each rendered target plan; ignored by dry-run, non-interactive, and CI execution |

##### Supported registration attributes

`plugin push` requires registration attributes from `Digitall.Plugins.Registration` 3.0.0 or
later. Other registration namespaces are not recognized.

| Attribute | Behavior |
|-----------|----------|
| `PluginRegistrationAttribute` | Registers plugin steps, including message, stage, mode, entity filters, and images |
| `CustomApiRegistrationAttribute` | Links a plugin type to the declared Custom API |
| `CustomDataProviderRegistrationAttribute` | Creates/updates a custom data provider and links its operation handlers |
| `ManagedIdentityRegistrationAttribute` | Links the assembly, or the package containing it, to a managed identity |

Workflow activity registration (`WorkflowRegistrationAttribute`) is not supported by `plugin push`.
Every concrete `IPlugin` type must carry one of the supported registration attributes; assemblies
with manually maintained plugin registrations are not supported.

##### Custom data providers

**Experimental:** Custom data-provider deployment is still experimental. Validate it in a
non-production environment before production rollout.

Requires `Digitall.Plugins.Registration` 3.0.0 or later and its three-argument constructor:
data-source configuration-table schema name, operation, and provider name on every declaration.
Rebuild assemblies using the old two-argument constructor before deployment. See the
[registration library](https://github.com/DIGITALLNature/DigitallRegistrationPower) for attribute
usage and examples.

`plugin push` creates or updates the provider, creates or validates its configuration table, and
assigns the Retrieve, RetrieveMultiple, Create, Update, and Delete handlers. Undeclared handlers and
omitted optional metadata are preserved. With `--solution`, the provider and configuration-table
schema are included; `--dry-run` previews the deployment.

On new providers, undeclared handlers are left unset.
New data-source tables use `<publisher-prefix>_name` as their primary-name column; existing columns are unchanged.
Existing tables incompatible with data-source configuration are rejected before deployment writes.
The target environment must contain exactly one data provider named `JsonConverter` to back the
configuration table; its ID is resolved automatically.

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

When an assembly has `ManagedIdentityRegistrationAttribute`, `plugin push` finds or creates the
managed identity for its client ID and links it to the assembly. For a package, the first bundled
assembly with this attribute also determines the package identity. If no tenant ID is declared, the
environment tenant is used.

##### Planning and execution

Before any writes, the command renders a deployment tree for plugin types, steps, images, Custom
API links, and assembly upgrades. It then shows completed operations, or reports that no changes
are required.
Data providers appear in a separate tree after the package or assembly tree; handler entries
reference plugin types rather than nesting the provider beneath them.
`--dry-run` stops after rendering the plan. Missing declared Custom APIs and unresolved step
messages fail before Dataverse changes occur.
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
│   └── main.js → contoso_/scripts/main.js Update
└── pages
    └── index.html → contoso_/pages/home.html Unchanged

Solution membership: ContosoCore
  + WebResource contoso_/scripts/main.js

Obsolete webresources
  − WebResource contoso_/obsolete.js
```

When `--confirm` is specified, the command asks before executing a plan that creates, updates,
adds resources to a solution, or deletes obsolete resources. Declining leaves Dataverse unchanged.
The prompt is skipped in CI and when `--non-interactive` is set; `--dry-run` never prompts.

The tree shows local directory structure; mapping files may intentionally produce a different
Dataverse naming structure. For a normal run, the tree is followed by an execution phase with
checkmarks for completed operations. With `--dry-run`, the tree, missing solution memberships,
and any obsolete resources to delete are rendered, then execution stops. Obsolete deletion
requires a directory target and `--solution`. An empty directory is a no-op, including when
`--delete-obsolete` is specified; it cannot be used to delete every webresource from a solution.

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
       │  codegeneration          │ plugin       │ webresource   │
       └──────────────────────────┴──────────────┴───────────────┘
                               │
                               ▼
                  ┌──────────────────────────┐
                  │     dgt.power.common     │
                  │  (Dataverse connection,  │
                  │   file access, tracer,   │
                  │   connection management, │
                  │   shared base commands)  │
                  └──────────────────────────┘
                               │
                               ▼
                ┌──────────────────────────────────┐
                │  Microsoft.PowerPlatform         │
                │  .Dataverse.Client / Xrm SDK     │
                └──────────────────────────────────┘
```

Key design principles:

- **Module isolation.** Every feature area (`analyzer`, `codegeneration`, `connection`, `export`, `import`, `maintenance`, `plugin`, `webresource`) is an independent project under `src/modules/`. Modules expose `Spectre.Console.Cli`-style command classes that are registered by the host.
- **Shared kernel.** `dgt.power.common` provides the cross-cutting infrastructure: `IDataverseConnection`, parsed per-invocation connection settings, typed connection storage and credential construction, protected-storage warnings, file I/O helpers, base commands, tracing and exception types, plus shared runtime environment helpers (`ExecutionEnvironment`) used by multiple modules.
- **DI everywhere.** Long-lived services (HTTP/NuGet clients, connection store, caches, JSON options) are singletons; per-command services (metadata, config resolver, generators, file service) are scoped; the `IOrganizationService` is lazily resolved from the active connection via `IDataverseConnection.ConnectAsync()`.
- **Stable connection storage.** `connections.json` and `state.json` live under the per-user dgtp home; secrets and the Azure.Identity token cache use OS-protected persistence.
- **Update awareness.** A `VersionCheckInterceptor` queries NuGet on each run to warn the user when a newer version of `dgt.power` is available.

## 📁 Repository Layout

```
DigitallPower/
├── src/
│   ├── dgt.power/                # CLI host project (produces the `dgtp` tool)
│   ├── dgt.power.common/         # Shared infrastructure (typed connections, IO, tracer)
│   ├── models/                   # Shared DTOs / data contracts
│   └── modules/
│       ├── dgt.power.analyzer/        # `analyze` commands
│       ├── dgt.power.codegeneration/  # `codegeneration` / `cg` command
│       ├── dgt.power.connection/      # `connection` commands (auth management)
│       ├── dgt.power.export/          # `export` commands
│       ├── dgt.power.import/          # `import` commands
│       ├── dgt.power.maintenance/     # `maintenance` commands
│       ├── dgt.power.plugin/          # `plugin` commands
│       ├── dgt.power.solution/        # `solution` commands (e.g. `solution lint`)
│       └── dgt.power.webresource/     # `webresource push` command
├── tests/                        # Unit and integration tests
├── samples/                      # Example inputs (configs, plugin samples)
├── schemas/                      # JSON schemas for configuration files
├── Directory.Build.props         # Common MSBuild properties
├── global.json                   # Pinned .NET SDK
└── DigitallPower.sln             # Solution file
```

## 🛠️ Build & Test

```bash
pnpm install                 # Install JS tooling dependencies (includes TypeScript compiler for TSL gates)
dotnet restore                # Restore dependencies (uses lock files)
dotnet build                  # Build the solution
dotnet test                   # Run all tests
dotnet test --filter "Name~Foo"   # Run a subset of tests
```

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

This anonymization is implemented in `TelemetryAnonymizer` and covered by unit tests for both Unix and Windows path formats. It is a best-effort, regex-based approach — see `.memory/decision-error-telemetry-anonymization.md` for its exact scope and known limitations.

CI detection is centralized in `dgt.power.common.ExecutionEnvironment` and currently recognizes `TF_BUILD`, `BUILD_BUILDURI`, `GITHUB_ACTIONS`, `GITLAB_CI`, `JENKINS_URL`, and `CI`.

### How to disable telemetry

**Per invocation:**

```bash
dgtp export --no-telemetry
```

**Permanently (environment variable):**

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

On first use, the CLI displays a one-time notice informing you about telemetry collection and how to opt out. This notice is shown only once per installation.

## ❤️ Community and Contributions

DigitallPower CLI is a **community-driven open source project** backed by DIGITALL. We are committed to a fully transparent development process and **highly appreciate any contributions**. Whether you are helping us fixing bugs, proposing new features, improving our documentation or spreading the word — **we would love to have you as part of the DigitallPower community**.

### 📫 Have a question? Want to chat? Ran into a problem?

We are happy to answer your questions via [GitHub Discussions](https://github.com/DIGITALLNature/DigitallPower/discussions).

### 🤝 Found a bug? Missing a specific feature?

Feel free to **file a new issue** with a descriptive title on the [DigitallPower](https://github.com/DIGITALLNature/DigitallPower/issues) repository. If you already found a solution to your problem, **we would love to review your pull request**. Have a look at our [contribution guidelines](https://github.com/DIGITALLNature/DigitallPower/blob/main/contributing.md) to find out about our coding standards and the conventional-commits workflow used in this repository.

## 📘 License

DigitallPower CLI is released under the terms of the [MS-RL License](Licence.md).
