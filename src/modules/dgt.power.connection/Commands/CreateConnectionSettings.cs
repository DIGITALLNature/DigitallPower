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

    [CommandOption("--no-verify")]
    [Description("Skip the post-create connectivity check (WhoAmI request). Use when the environment is temporarily unavailable or when pre-configuring connections in a CI pipeline.")]
    [DefaultValue(false)]
    public bool NoVerify { get; init; }

    public override ValidationResult Validate()
    {
        if (Url != null && ConnectionString != null)
            return ValidationResult.Error("Specify either --url or --connection-string, not both.");
        if (Url == null && ConnectionString == null)
            return ValidationResult.Error("Provide either --url (for MSAL authentication) or --connection-string.");
        return ValidationResult.Success();
    }
}
