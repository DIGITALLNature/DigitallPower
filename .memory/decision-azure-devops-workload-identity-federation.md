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
dgtp connection create prod `
  --url https://contoso.crm4.dynamics.com `
  --azure-devops-federated `
  --tenant <tenantId> `
  --application-id <appId> `
  --service-connection-id <serviceConnectionGuid> `
  --no-verify
```

- `--azure-devops-federated` / `--adof` — activates this identity type (mutually exclusive with `--connection-string`).
- `--tenant` — Entra ID tenant backing the service connection.
- `--application-id` — app registration (or user-assigned managed identity) client ID.
- `--service-connection-id` — GUID of the Azure DevOps service connection (needed by `AzurePipelinesCredential` to resolve the correct OIDC exchange).
- The pipeline step must expose `SYSTEM_ACCESSTOKEN` as an env var (`env: SYSTEM_ACCESSTOKEN: $(System.AccessToken)`), which `AzurePipelinesConnector` reads to authenticate the OIDC exchange.
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

## CI/CD Integration — getting the 3 IDs into the pipeline

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

A reusable, tool-agnostic step template implementing this lookup —
`azure-pipeline-templates/xrm-connection/resolve-service-connection.yml` — was contributed to the
sibling `DIGITALLNature/DigitallPipelines` repo (not this repo) since it has no dependency on dgtp
and is useful to any consumer needing service-connection metadata in a pipeline. It publishes
`environmentUrl`/`tenantId`/`applicationId`/`serviceConnectionId` as step output variables. The
pipeline's build identity (`Project Collection Build Service`) needs **Reader** access to the
service connection for the lookup to succeed.

dgtp's README documents both the manual REST-lookup approach and the template approach in a
dedicated "CI/CD Integration" section (Azure Pipelines-specific; other CI systems use
`--connection-string` with their own secret management).

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
- `src/dgt.power.common/Logic/Identity.cs`, `Identities.cs`, `XrmConnection.cs` — wiring
- `src/dgt.power.common/dgt.power.common.csproj` — `Azure.Identity` v1.21.0
- `src/modules/dgt.power.connection/Commands/CreateConnectionSettings.cs`, `CreateConnectionCommand.cs` — new CLI options + validation
- `tests/dgt.power.connection.tests/CreateConnectionSettingsTests.cs` (new), `CreateConnectionCommandTests.cs` (extended)
- `README.md` — `connection` command reference (flag list) + new "CI/CD Integration" section
