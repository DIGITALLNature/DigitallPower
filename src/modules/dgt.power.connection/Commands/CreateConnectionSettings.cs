// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.connection.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace dgt.power.connection.Commands;

public class CreateConnectionSettings : ConnectionSettings
{
    [CommandArgument(0, "<Name>")]
    [Description("Name of the connection")]
    public string Name { get; init; } = string.Empty;

    [CommandOption("--url")]
    [Description("Environment URL for MSAL (interactive/device-flow) authentication. Example: https://contoso.crm4.dynamics.com")]
#pragma warning disable CA1056, S3996 // CLI argument is intentionally a string, not Uri
    public string? Url { get; init; }
#pragma warning restore CA1056, S3996

    [CommandOption("--connection-string")]
    [Description("Full Dataverse connection string for service principal or other non-interactive auth. Example: AuthType=ClientSecret;Url=...;ClientId=...;ClientSecret=...")]
    public string? ConnectionString { get; init; }

    [CommandOption("--azure-devops-federated|--adof")]
    [Description("Use Azure DevOps Workload Identity Federation (OIDC) for service principal auth. " +
                  "Requires --url, --tenant, --application-id and --service-connection-id. No client secret is required or stored.")]
    [DefaultValue(false)]
    public bool AzureDevOpsFederated { get; init; }

    [CommandOption("--tenant")]
    [Description("Entra ID tenant ID hosting the app registration used by the Azure DevOps service connection. Required with --azure-devops-federated.")]
    public string? TenantId { get; init; }

    [CommandOption("--application-id")]
    [Description("Application (client) ID of the app registration used by the Azure DevOps service connection. Required with --azure-devops-federated.")]
    public string? ApplicationId { get; init; }

    [CommandOption("--service-connection-id")]
    [Description("GUID of the Azure DevOps service connection (visible in its URL under Project Settings > Service connections). Required with --azure-devops-federated.")]
    public string? ServiceConnectionId { get; init; }

    [CommandOption("--no-verify")]
    [Description("Skip the post-create connectivity check (WhoAmI request). Use when the environment is temporarily unavailable or when pre-configuring connections in a CI pipeline.")]
    [DefaultValue(false)]
    public bool NoVerify { get; init; }

    public override ValidationResult Validate()
    {
        if (AzureDevOpsFederated)
        {
            if (ConnectionString != null)
                return ValidationResult.Error("Specify either --connection-string or --azure-devops-federated, not both.");
            if (Url == null)
                return ValidationResult.Error("--azure-devops-federated requires --url (the environment to connect to).");
            if (string.IsNullOrWhiteSpace(TenantId) || string.IsNullOrWhiteSpace(ApplicationId) || string.IsNullOrWhiteSpace(ServiceConnectionId))
                return ValidationResult.Error("--azure-devops-federated requires --tenant, --application-id and --service-connection-id.");
            return ValidationResult.Success();
        }

        if (TenantId != null || ApplicationId != null || ServiceConnectionId != null)
            return ValidationResult.Error("--tenant, --application-id and --service-connection-id are only valid together with --azure-devops-federated.");
        if (Url != null && ConnectionString != null)
            return ValidationResult.Error("Specify either --url or --connection-string, not both.");
        if (Url == null && ConnectionString == null)
            return ValidationResult.Error("Provide either --url (for MSAL authentication) or --connection-string.");
        return ValidationResult.Success();
    }
}
