// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.ComponentModel;
using dgt.power.plugin.Base;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.plugin.Commands;

public class PluginPushSettings : PluginSettings
{
    [CommandArgument(0, "<Target>")]
    [Description("Path to a plugin assembly (.dll), a plugin package (.nupkg), a directory (top level only) or a glob pattern such as 'bin/release/**/*.nupkg'")]
    public required string Target { get; init; }

    [CommandOption("--solution")]
    [Description("Add new/updated components to solution; default: none")]
    public string? Solution { get; set; }

    [CommandOption("--publisher-prefix")]
    [Description("Publisher customization prefix for plugin packages; required when processing a .nupkg")]
    public string? PublisherPrefix { get; set; }

    [CommandOption("--dry-run")]
    [Description("Only report what would be created/updated/deleted; do not write anything to Dataverse")]
    public bool DryRun { get; set; }

    [CommandOption("--confirm")]
    [Description("Prompt for confirmation after rendering each target plan")]
    public bool Confirm { get; set; }
}
