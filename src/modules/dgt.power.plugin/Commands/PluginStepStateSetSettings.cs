// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.plugin.Base;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
#pragma warning disable CS8618

namespace dgt.power.plugin.Commands;

/// <summary>
/// Settings for the <c>dgtp plugin step state set</c> command.
/// </summary>
public class PluginStepStateSetSettings : PluginSettings
{
    [CommandOption("-c|--config")]
    [Description(@"Full path to the config file, e.g. C:\temp\config.json")]
    public string Config { get; init; } = string.Empty;

    [CommandOption("--dry-run")]
    [Description("Only report which steps would change; do not write anything to Dataverse.")]
    public bool DryRun { get; init; }
}
