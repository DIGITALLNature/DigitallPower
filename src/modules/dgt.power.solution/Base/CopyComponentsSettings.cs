// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.ComponentModel;
using Spectre.Console.Cli;

namespace dgt.power.solution.Base;

public class CopyComponentsSettings : SolutionSettings
{
    [CommandArgument(0, "<Target>")]
    [Description("The unique name of the unmanaged target solution; best-practice mode also protects included unmanaged " +
        "Calculated, Rollup and Power Fx Formula columns in shared environment metadata")]
    public string Target { get; init; } = string.Empty;

    [CommandOption("-s|--source")]
    [Description("Comma-separated unique names of the solutions to copy components from")]
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global — set via Spectre.Console.Cli reflection binding
    public string Source { get; init; } = string.Empty;

    [CommandOption("--dry-run")]
    [Description("Prepare and print the copy and field-protection plans without adding, removing, updating or publishing anything")]
    public bool DryRun { get; init; }

    [CommandOption("--raw")]
    [Description("Disable best-practice normalization: for tables, preserve only complete vs. non-complete behavior " +
        "(shell-only sources are treated as non-complete), skip the managed-active-layer filter and bypass field-protection reads and updates")]
    public bool Raw { get; init; }

    [CommandOption("--apps <MODE>")]
    [Description("How to handle model-driven apps: skip (default) = do not copy them; strip = copy, then remove subcomponents Dataverse added; allow = copy and keep what Dataverse adds")]
    [DefaultValue(AppHandling.Skip)]
    public AppHandling Apps { get; init; } = AppHandling.Skip;
}
