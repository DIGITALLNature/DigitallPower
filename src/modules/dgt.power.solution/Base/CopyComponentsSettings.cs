// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using Spectre.Console.Cli;

namespace dgt.power.solution.Base;

public class CopyComponentsSettings : SolutionSettings
{
    [CommandArgument(0, "<Target>")]
    [Description("The unique name of the unmanaged solution to copy components into")]
    public string Target { get; init; } = string.Empty;

    [CommandOption("-s|--source")]
    [Description("Comma-separated unique names of the solutions to copy components from")]
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global — set via Spectre.Console.Cli reflection binding
    public string Source { get; init; } = string.Empty;

    [CommandOption("--dry-run")]
    [Description("Print the planned changes without adding any component to the target solution")]
    public bool DryRun { get; init; }

    [CommandOption("--raw")]
    [Description("Disable best-practice normalization: mirror each source component's own root component behavior and skip the managed-active-layer filter")]
    public bool Raw { get; init; }
}
