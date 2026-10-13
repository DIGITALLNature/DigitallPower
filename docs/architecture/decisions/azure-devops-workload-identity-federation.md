# Azure DevOps Workload Identity Federation

## Context

Azure DevOps Power Platform service connections can use Workload Identity Federation, so no client secret exists to extract. `pac` supports this only inside a task.json-based extension (it needs task-scoped `PAC_ADO_ID_TOKEN_*` variables and the `connectedService` input), and the Build Tools tasks expose no reusable output for plain script steps.

## Decision

Provide an `azureDevOpsFederated` connection type backed by `Azure.Identity.AzurePipelinesCredential` instead of a hand-rolled OIDC exchange. Resolve the service connection by name through the stable Azure DevOps REST API (`serviceendpoint/endpoints`, type `powerplatform-spn`) using a plain `HttpClient` and `SYSTEM_ACCESSTOKEN`, keeping explicit tenant/application/service-connection values as an escape hatch for missing Reader access, ambiguous names, or blocked REST access.

Derive `SYSTEM_OIDCREQUESTURI` in `AzurePipelinesCredentialFactory` from predefined job variables (`SYSTEM_COLLECTIONURI`, `SYSTEM_TEAMPROJECTID`, `SYSTEM_HOSTTYPE`, `SYSTEM_PLANID`, `SYSTEM_JOBID`), always overwriting an existing value.

## Rationale

- `AzurePipelinesCredential` reads the OIDC endpoint from `SYSTEM_OIDCREQUESTURI`, which Azure DevOps populates only for a few built-in tasks with an ARM service connection. Verified against a live pipeline (2026-09-15): a plain script step could obtain a valid OIDC token from the self-constructed URI, so no `connectedService` task input is required once the pipeline is authorized for the service connection.
- A pre-existing `SYSTEM_OIDCREQUESTURI` may belong to another service connection or task, so it is not trusted.
- The official Azure DevOps client SDK would add netfx-era dependencies for one stable REST call.
- The same service connection name can exist in several folders; ambiguity is an error.

## Consequences

- Managed identity as federated credential subject works unchanged; agent-assigned managed identity (IMDS) is unsupported and would need a `ManagedIdentityCredential`-based connector.
- Tokens are derived on every connect and never cached to disk.
- A tool-agnostic resolution step template lives in `DIGITALLNature/DigitallPipelines`.
