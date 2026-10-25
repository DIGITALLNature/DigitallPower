# CI/CD Integration

> **Scope:** this guide is specific to **Azure Pipelines**, since the auth mechanisms described
> here (service connections, Workload Identity Federation, `System.AccessToken`) are Azure
> DevOps concepts. Other CI systems (GitHub Actions, etc.) aren't covered here — use
> the global `DGTP_CONNECTION_STRING` environment variable or `--connection-string` option with
> whatever secret-management approach your platform provides.

dgtp is commonly driven from an Azure Pipelines job to authenticate against Dataverse without any
interactive login. How you provide credentials to a dgtp command depends on how the underlying
**Power Platform service connection** is configured. This guide covers the setups in use today; each subsection is self-contained.

## Client Secret service connection

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

## Workload Identity Federation (OIDC) service connection

When the service connection uses **Workload Identity Federation** instead of a client secret, no
secret is ever available to build a traditional connection string. Use `dgtp connection create
--azure-devops-federated` instead — dgtp exchanges the pipeline job's short-lived OIDC token for
an Entra ID access token at connect time, via
[`Azure.Identity.AzurePipelinesCredential`](https://aka.ms/azsdk/net/identity/azurepipelinescredential/usage).

### Recommended: `--service-connection-name`

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

### Advanced: explicit `--tenant` / `--client-id` / `--service-connection-id` / `--url`

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
