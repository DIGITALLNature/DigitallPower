// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.connection.Base;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.connection.Commands;

public class CreateConnectionSettings : ConnectionSettings
{
    [CommandArgument(0, "<Name>")]
    [Description("Name of the connection")]
    public string Name { get; init; } = string.Empty;

    [CommandOption("--url")]
    [Description("Environment URL for MSAL (interactive/device-flow) authentication. Example: https://contoso.crm4.dynamics.com")]
    public string? Url { get; init; }

    [CommandOption("--connection-string")]
    [Description("Full Dataverse connection string for service principal or other non-interactive auth. Example: AuthType=ClientSecret;Url=...;ClientId=...;ClientSecret=...")]
    public string? ConnectionString { get; init; }

    [CommandOption("--no-verify")]
    [Description("Skip the post-create connectivity check (WhoAmI request). Use when the environment is temporarily unavailable or when pre-configuring connections in a CI pipeline.")]
    [DefaultValue(false)]
    public bool NoVerify { get; init; }
}
