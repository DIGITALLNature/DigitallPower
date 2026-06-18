// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using Spectre.Console.Cli;

namespace dgt.power.Commands.Complete;

public class CompleteInstallShellSettings : CompleteSettings
{
    [CommandOption("--shell <SHELL>")]
    [Description("Shell to install shim for (bash|zsh|pwsh|fish). Auto-detected if omitted.")]
    public string? Shell { get; init; }

    [CommandOption("--dry-run")]
    [Description("Show what would be done without making any changes")]
    public bool DryRun { get; init; }
}
