# Decision: Azure DevOps Workload Identity Federation (OIDC) Connections

## Context

Azure DevOps Power Platform service connections can be configured with **Workload Identity
Federation** instead of a client secret. When a pipeline's service connection is switched to this
scheme, no client secret is ever available, so the previous pattern of extracting a full Dataverse
connection string via Power Platform Build Tools (`PowerPlatformSetConnectionVariables`) no longer
works — there's nothing secret to extract.

`pac` (Power Platform CLI) supports `--azureDevOpsFederated` internally, but only works when
invoked from inside a proper task.json-based Azure DevOps extension (relies on
`PAC_ADO_ID_TOKEN_REQUEST_URL`/`PAC_ADO_ID_TOKEN_REQUEST_TOKEN` env vars set by the wrapping task,
plus the `connectedService:PowerPlatformSPN` task input to resolve the connection GUID and get
`SYSTEMVSSCONNECTION`'s OAuth token). None of the Build Tools tasks (`whoami-v2`,
`tool-installer-v2`) expose these as reusable output variables for a plain script step, so dgtp
cannot piggy-back on `pac`'s auth session.

## Decision

Implement a dedicated `AzureDevOpsFederatedIdentity` connection type in dgtp, backed by
`Azure.Identity.AzurePipelinesCredential` (the official SDK class purpose-built for ADO WIF
service connections) rather than hand-rolling the OIDC token exchange.

### CLI surface (`dgtp connection create`)

```bash
# Recommended: resolve everything from the service connection name at connect time
dgtp connection create prod `
  --azure-devops-federated `
  --service-connection-name "MyPowerPlatformConnection" `
  --no-verify

# Advanced: bypass the REST lookup by passing the resolved values explicitly
dgtp connection create prod `
  --url https://contoso.crm4.dynamics.com `
  --azure-devops-federated `
  --tenant <tenantId> `
  --application-id <appId> `
  --service-connection-id <serviceConnectionGuid> `
  --no-verify
```

- `--azure-devops-federated` / `--adof` — activates this identity type (mutually exclusive with `--connection-string`).
- `--service-connection-name` — resolves `--url`/`--tenant`/`--application-id`/`--service-connection-id`
  automatically via the Azure DevOps REST API at connect time (see "Resolving by name" below).
  Mutually exclusive with providing any of those four explicitly.
- `--tenant` — Entra ID tenant backing the service connection.
- `--application-id` — app registration (or user-assigned managed identity) client ID.
- `--service-connection-id` — GUID of the Azure DevOps service connection (needed by `AzurePipelinesCredential` to resolve the correct OIDC exchange).
- The pipeline step must expose `SYSTEM_ACCESSTOKEN` as an env var (`env: SYSTEM_ACCESSTOKEN: $(System.AccessToken)`), which `AzurePipelinesConnector` reads to authenticate the OIDC exchange. This is also the only variable that needs explicit mapping for `--service-connection-name` resolution — `SYSTEM_TEAMFOUNDATIONCOLLECTIONURI`/`SYSTEM_TEAMPROJECTID` are already available as env vars on every pipeline job.
- No token is cached to disk — a fresh access token is derived on every connect, scoped to that pipeline job.

## Architecture

- `Identity` (base) → `TokenIdentity` (MSAL, existing) / `AzureDevOpsFederatedIdentity` (new: `TenantId`, `ClientId`, `ServiceConnectionId`).
- `IConnector`: `CrmConnector` (raw connection string, existing) / `TokenConnector` (MSAL, existing) / `AzurePipelinesConnector` (new) — wraps `AzurePipelinesCredential`, same `ServiceClient(Uri, Func<string,Task<string>>)` token-provider pattern as `TokenConnector`.
- Wired into `XrmConnection.ConnectWithProfileAsync` (dispatch on identity type) and `Identities.Infos` (identity registry/deserialization).
- `Azure.Identity` v1.21.0 added as a package reference to `dgt.power.common`.

## Why `AzurePipelinesCredential` over alternatives

- **Hand-rolled OIDC exchange**: would duplicate SDK logic (token endpoint discovery, client assertion construction, retries) that Azure.Identity already implements and maintains against Entra ID's federated credential flow.
- **Reusing `pac`'s auth session**: not possible from a plain script step — see Context above; `pac`'s WIF support is tightly coupled to running inside an ADO task extension.
- **Requiring the Power Platform Tool Installer task before dgtp**: does not expose any auth-related env vars either — it only resolves/exposes the `pac` CLI path (checked `tool-installer-v2/index.ts` and `task.json` in `microsoft/powerplatform-build-tools`).

## CI/CD Integration — resolving the service connection by name

`--tenant`/`--application-id`/`--service-connection-id`/`--url` are all non-secret and can be
resolved from the service connection by name at runtime via the Azure DevOps REST API
(`GET .../_apis/serviceendpoint/endpoints?endpointNames=<name>&type=powerplatform-spn&api-version=7.1`,
stable, not preview), avoiding any hardcoded IDs in the pipeline YAML:

- `authorization.parameters.tenantid` → `--tenant`
- `authorization.parameters.serviceprincipalid` → `--application-id`
- `id` → `--service-connection-id`
- `url` → `--url` (the Dataverse environment URL entered when the connection was created)

Power Platform service connections register endpoint `type: "powerplatform-spn"` (confirmed via
`extension/service-connections.json` in `microsoft/powerplatform-build-tools`), supporting 3 auth
schemes: client secret, Managed Service Identity, and Workload Identity Federation. The lookup
only ever reads non-secret metadata, so it's the same regardless of which of the 3 schemes is
active.

**dgtp performs this lookup itself** (`AzureDevOpsServiceConnectionResolver` in
`dgt.power.common/Logic`) rather than requiring an external pipeline template — pass
`--service-connection-name` to `dgtp connection create --azure-devops-federated` and dgtp resolves
the other four values via a plain `HttpClient` call, authenticated with the same `SYSTEM_ACCESSTOKEN`
already required for the OIDC exchange itself. `SYSTEM_TEAMFOUNDATIONCOLLECTIONURI`/
`SYSTEM_TEAMPROJECTID` (used to build the lookup URL) are predefined Azure Pipelines variables,
already available as env vars without any extra step. This was chosen over the official Azure
DevOps client SDK (`Microsoft.TeamFoundationServer.Client`, exposing a typed
`ServiceEndpointHttpClient`) because that package still drags in netfx-era dependencies for a
single, stable, well-documented REST call — not worth the added dependency surface.

Because service connections can be organized into folders, the same name can exist more than once
within a project; the resolver treats 0 or >1 matches as an error and tells the user to fall back
to explicit `--tenant`/`--application-id`/`--service-connection-id` instead. That explicit path is
kept as a deliberate escape hatch (not removed) for cases where the build identity can't be granted
**Reader** access to the service connection, the name is ambiguous, or the agent's network policy
blocks the Azure DevOps REST API — `--service-connection-name` and the explicit values are mutually
exclusive as a whole group (no partial overrides), to keep the CLI surface simple.

The reusable, tool-agnostic step template
`azure-pipeline-templates/xrm-connection/resolve-service-connection.yml`, contributed to the
sibling `DIGITALLNature/DigitallPipelines` repo, still exists and remains useful for consumers who
want the same lookup as a dgtp-independent step (e.g. to feed other tools), but it is no longer the
primary/recommended path now that dgtp resolves service connection names natively.

dgtp's README documents both `--service-connection-name` (recommended) and the explicit
tenant/application/service-connection-id flags (advanced) in a dedicated "CI/CD Integration"
section (Azure Pipelines-specific; other CI systems use `--connection-string` with their own
secret management).

## Out of Scope

- **Managed Identity connections**: two distinct mechanisms share this name for ADO service
  connections. (1) WIF-backed Managed Identity (a user-assigned managed identity as the
  federated credential subject) is mechanically identical to the app-registration case above and
  already works with `--azure-devops-federated` today, no extra code needed. (2) Legacy
  agent-assigned Managed Identity (self-hosted agent on an Azure VM/VMSS using its local identity
  via IMDS, no OIDC/ADO exchange at all) is NOT supported by dgtp and was explicitly deferred to a
  future PR — would require a new `Azure.Identity.ManagedIdentityCredential`-based connector.

## Files Changed

- `src/dgt.power.common/Logic/AzureDevOpsFederatedIdentity.cs` — new identity type
- `src/dgt.power.common/Logic/AzurePipelinesConnector.cs` — new `IConnector` implementation
- `src/dgt.power.common/Logic/AzureDevOpsServiceConnectionResolver.cs` — resolves a service connection name to its `Url`/`TenantId`/`ClientId`/`ServiceConnectionId` via the Azure DevOps REST API
- `src/dgt.power.common/Exceptions/ServiceConnectionResolutionException.cs` — new exception type for resolution failures (missing/ambiguous name, missing permissions, unreachable API)
- `src/dgt.power.common/Logic/Identity.cs`, `Identities.cs`, `XrmConnection.cs` — wiring
- `src/dgt.power.common/dgt.power.common.csproj` — `Azure.Identity` v1.21.0
- `src/modules/dgt.power.connection/Commands/CreateConnectionSettings.cs` — `--service-connection-name` option + validation
- `src/modules/dgt.power.connection/Commands/CreateConnectionCommand.cs` — resolves via `AzureDevOpsServiceConnectionResolver` when `--service-connection-name` is used
- `tests/dgt.power.connection.tests/CreateConnectionSettingsTests.cs`, `CreateConnectionCommandTests.cs` — extended
- `README.md` — `connection` command reference (flag list) + rewritten "CI/CD Integration" section
