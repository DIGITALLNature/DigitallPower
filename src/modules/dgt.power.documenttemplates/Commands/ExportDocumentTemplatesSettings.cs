// Copyright (c) DIGITALL Nature. All rights reserved

using System.ComponentModel;
using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.documenttemplates.Commands;

public sealed class ExportDocumentTemplatesSettings : BaseProgramSettings
{
    [CommandArgument(0, "<Directory>")]
    [Description("Destination directory for active Word and Excel templates.")]
    public string? Directory { get; init; }

    [CommandOption("--entity <LogicalName>")]
    [Description("Narrow the active selection to one table.")]
    public string? Entity { get; init; }

    [CommandOption("--type <word|excel>")]
    [Description("Narrow the active selection to Word or Excel.")]
    public string? Type { get; init; }

    [CommandOption("--overwrite")]
    [Description("Replace generated destinations only; preserve unrelated files.")]
    public bool Overwrite { get; init; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(Directory))
            return ValidationResult.Error("A destination directory is required.");
        if (Type is not (null or "word" or "excel"))
            return ValidationResult.Error("--type must be word or excel.");
        if (Entity is not null && (string.IsNullOrWhiteSpace(Entity) || Entity != Entity.Trim()))
            return ValidationResult.Error("--entity must be an unpadded table logical name.");
        return ValidationResult.Success();
    }
}
