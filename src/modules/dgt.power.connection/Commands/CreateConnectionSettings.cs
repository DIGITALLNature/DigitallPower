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
    [Description("Dataverse environment URL")]
#pragma warning disable CA1056, S3996 // CLI argument is intentionally a string, not Uri
    public string? Url { get; init; }
#pragma warning restore CA1056, S3996

    [CommandOption("--tenant")]
    [Description("Entra ID tenant GUID or verified domain; optional for user sign-in, required for service-principal and explicit federated connections")]
    public string? TenantId { get; init; }

    [CommandOption("--client-id")]
    [Description("Client ID for service-principal connections")]
    public string? ClientId { get; init; }

    [CommandOption("--device-code")]
    [Description("Use device-code login instead of opening a browser")]
    [DefaultValue(false)]
    public bool DeviceCode { get; init; }

    [CommandOption("--client-secret <secret>")]
    [Description("Client secret to store in the OS secret store; protect command-line arguments and logs")]
    public string? ClientSecret { get; init; }

    [CommandOption("--certificate-thumbprint")]
    [Description("Thumbprint of a certificate in the CurrentUser certificate store")]
    public string? CertificateThumbprint { get; init; }

    [CommandOption("--certificate-path")]
    [Description("Path to a PFX certificate file")]
    public string? CertificatePath { get; init; }

    [CommandOption("--certificate-password <password>")]
    [Description("PFX password; omit for a passwordless file. Protect command-line arguments and logs")]
    public string? CertificatePassword { get; init; }

    [CommandOption("--azure-devops-federated|--adof")]
    [Description("Use Azure DevOps Workload Identity Federation (OIDC)")]
    [DefaultValue(false)]
    public bool AzureDevOpsFederated { get; init; }

    [CommandOption("--service-connection-name")]
    [Description("Resolve the Azure DevOps service connection by name")]
    public string? ServiceConnectionName { get; init; }

    [CommandOption("--service-connection-id")]
    [Description("GUID of the Azure DevOps service connection")]
    public string? ServiceConnectionId { get; init; }

    [CommandOption("--no-verify")]
    [Description("Skip the post-create Dataverse connectivity check")]
    [DefaultValue(false)]
    public bool NoVerify { get; init; }

    public override ValidationResult Validate()
    {
        if (ClientSecret is not null && string.IsNullOrWhiteSpace(ClientSecret))
        {
            return ValidationResult.Error("--client-secret requires a non-empty secret.");
        }

        if (CertificatePassword is not null && CertificatePath is null)
        {
            return ValidationResult.Error("--certificate-password requires --certificate-path.");
        }

        if (ConnectionString is not null)
        {
            return ValidationResult.Error(
                "connection create no longer accepts --connection-string. Use a typed connection option, " +
                "or pass --connection-string to the Dataverse command for a one-off connection.");
        }

        if (AzureDevOpsFederated)
        {
            if (ClientSecret is not null || DeviceCode || CertificateThumbprint is not null || CertificatePath is not null)
            {
                return ValidationResult.Error("Specify only one connection authentication type.");
            }

            if (!string.IsNullOrWhiteSpace(ServiceConnectionName))
            {
                var hasExplicitDetails = Url is not null
                                         || TenantId is not null
                                         || ClientId is not null
                                         || ServiceConnectionId is not null;
                if (!hasExplicitDetails)
                {
                    return ValidationResult.Success();
                }

                return ValidationResult.Error(
                    "--service-connection-name resolves all Azure DevOps service connection details; " +
                    "do not combine it with --url, --tenant, --client-id or --service-connection-id.");
            }

            var hasUrl = !string.IsNullOrWhiteSpace(Url);
            var hasTenantId = !string.IsNullOrWhiteSpace(TenantId);
            var hasClientId = !string.IsNullOrWhiteSpace(ClientId);
            var hasServiceConnectionId = !string.IsNullOrWhiteSpace(ServiceConnectionId);
            if (hasUrl && hasTenantId && hasClientId && hasServiceConnectionId)
            {
                return ValidationResult.Success();
            }

            return ValidationResult.Error(
                "--azure-devops-federated requires --service-connection-name or --url, --tenant, " +
                "--client-id and --service-connection-id.");
        }

        if (ServiceConnectionName is not null || ServiceConnectionId is not null)
        {
            return ValidationResult.Error(
                "--service-connection-name and --service-connection-id require --azure-devops-federated.");
        }

        var hasCertificate = CertificateThumbprint is not null || CertificatePath is not null;
        var hasUserMode = ClientSecret is null && !hasCertificate;
        if (DeviceCode && (ClientSecret is not null || hasCertificate))
        {
            return ValidationResult.Error("--device-code is only valid for user authentication.");
        }

        if (ClientSecret is not null && hasCertificate)
        {
            return ValidationResult.Error("Specify either --client-secret or certificate options, not both.");
        }

        if (CertificateThumbprint is not null && CertificatePath is not null)
        {
            return ValidationResult.Error("Specify either --certificate-thumbprint or --certificate-path, not both.");
        }

        if (string.IsNullOrWhiteSpace(Url))
        {
            return ValidationResult.Error("Provide --url for every connection type.");
        }

        if (hasUserMode)
        {
            return string.IsNullOrWhiteSpace(ClientId)
                ? ValidationResult.Success()
                : ValidationResult.Error("--client-id is only valid with --client-secret or certificate options.");
        }

        return !string.IsNullOrWhiteSpace(TenantId) && !string.IsNullOrWhiteSpace(ClientId)
            ? ValidationResult.Success()
            : ValidationResult.Error("--client-secret and certificate connections require --tenant and --client-id.");
    }
}
