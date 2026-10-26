// Copyright (c) DIGITALL Nature. All rights reserved

using System.ComponentModel;
using dgt.power.common;
using dgt.power.teamtemplates.Configuration;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.teamtemplates.Commands;

public sealed class TeamTemplateExportSettings : BaseProgramSettings
{
    [CommandArgument(0, "<File>")]
    [Description("Version 1 teamtemplate resource output path.")]
    public string? File { get; init; }

    [CommandOption("--dry-run")]
    [Description("Validate and report export without creating or replacing files.")]
    public bool DryRun { get; init; }

    [CommandOption("--overwrite")]
    [Description("Allow replacing an existing output file.")]
    public bool Overwrite { get; init; }

    [CommandOption("--table <LogicalName>")]
    [Description("Export only this logical table; repeat for multiple tables. Default: all non-system templates.")]
#pragma warning disable CA1819 // Spectre.Console binds repeatable options through arrays.
    public string[]? Tables { get; init; }
#pragma warning restore CA1819

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(File))
        {
            return ValidationResult.Error("An output file path is required.");
        }
        if (Tables is not null && Array.Exists(Tables, table => !TeamTemplateConfiguration.IsTableName(table)))
        {
            return ValidationResult.Error("--table requires exact logical table names, not wildcards or global selectors.");
        }
        return ValidationResult.Success();
    }
}
