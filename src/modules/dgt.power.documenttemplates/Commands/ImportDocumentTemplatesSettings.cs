// Copyright (c) DIGITALL Nature. All rights reserved

using System.ComponentModel;
using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.documenttemplates.Commands;

public sealed class ImportDocumentTemplatesSettings : BaseProgramSettings
{
    [CommandArgument(0, "<Manifest>")]
    [Description("Strict V1 documenttemplates manifest path.")]
    public string? Manifest { get; init; }

    [CommandOption("--dry-run")]
    [Description("Prepare all templates against the target without writing.")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global -- bound by Spectre.Console.Cli via reflection.
    public bool DryRun { get; init; }

    public override ValidationResult Validate() => string.IsNullOrWhiteSpace(Manifest)
        ? ValidationResult.Error("A manifest path is required.")
        : ValidationResult.Success();
}
