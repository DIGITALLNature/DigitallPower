// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
    [Description("Full path to a plugin assembly (.dll), a plugin package (.nupkg), or a directory containing multiple such files")]
    public required string Target { get; set; }

    [CommandOption("--solution")]
    [Description("Add new/updated components to solution; default: none")]
    public string? Solution { get; set; }

    [CommandOption("--dry-run")]
    [Description("Only report what would be created/updated/deleted; do not write anything to Dataverse")]
    public bool DryRun { get; set; }
}
