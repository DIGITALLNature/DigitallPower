// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.ComponentModel;
using Spectre.Console.Cli;

namespace dgt.power.Commands.Complete;

public class CompleteSetupSettings : CompleteSettings
{
    [CommandOption("--all")]
    [Description("Also install the shell completion shim into your shell's rc file")]
    public bool All { get; init; }

    [CommandOption("--shell <SHELL>")]
    [Description("Shell for --all mode (bash|zsh|pwsh|fish). Auto-detected if omitted.")]
    public string? Shell { get; init; }
}
