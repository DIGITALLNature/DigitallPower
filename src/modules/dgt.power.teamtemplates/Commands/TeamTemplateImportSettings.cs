// Copyright (c) DIGITALL Nature. All rights reserved

using System.ComponentModel;
using dgt.power.common;
using dgt.power.teamtemplates.Configuration;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.teamtemplates.Commands;

public sealed class TeamTemplateImportSettings : BaseProgramSettings
{
    [CommandArgument(0, "<File>")]
    [Description("Version 1 teamtemplate resource file to import.")]
    public string? File { get; init; }

    [CommandOption("--dry-run")]
    [Description("Validate and report the complete plan without writing.")]
    public bool DryRun { get; init; }

    [CommandOption("--cleanup")]
    [Description("Remove unlisted non-system templates only in explicit cleanup tables, after successful imports.")]
    public bool Cleanup { get; init; }

    [CommandOption("--cleanup-table <LogicalName>")]
    [Description("Exact logical table name eligible for cleanup; repeat for multiple tables. Requires --cleanup.")]
#pragma warning disable CA1819 // Spectre.Console binds repeatable options through arrays.
    public string[]? CleanupTables { get; init; }
#pragma warning restore CA1819

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(File))
        {
            return ValidationResult.Error("An input file path is required.");
        }
        var hasTables = CleanupTables is { Length: > 0 };
        var invalidTable = CleanupTables is not null && Array.Exists(CleanupTables, table => !TeamTemplateConfiguration.IsTableName(table));
        if (Cleanup != hasTables || invalidTable)
        {
            return ValidationResult.Error("Cleanup requires --cleanup and explicit --cleanup-table logical names; global scopes are not supported.");
        }
        return ValidationResult.Success();
    }
}
