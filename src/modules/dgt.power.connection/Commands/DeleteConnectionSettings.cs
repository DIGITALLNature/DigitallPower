// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.connection.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.connection.Commands;

public class DeleteConnectionSettings : ConnectionSettings
{
    [CommandArgument(0, "[Name]")]
    [Description("Name of the connection to delete")]
    public string? Name { get; init; }

    [CommandOption("--all")]
    [Description("Delete all connections")]
    [DefaultValue(false)]
    public bool All { get; init; }

    [CommandOption("--yes|-y")]
    [Description("Skip the confirmation prompt when deleting all connections")]
    [DefaultValue(false)]
    public bool Yes { get; init; }

    public override ValidationResult Validate()
    {
        if (All && Name != null)
            return ValidationResult.Error("Specify either a connection name or --all, not both.");
        if (!All && Name == null)
            return ValidationResult.Error("Provide a connection name or use --all to delete all connections.");
        return ValidationResult.Success();
    }
}
