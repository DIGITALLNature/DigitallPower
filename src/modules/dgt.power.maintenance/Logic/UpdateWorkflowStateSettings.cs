// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.common;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.Logic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class UpdateWorkflowStateSettings : BaseProgramSettings
{
    [CommandOption("-c|--config")]
    [Description("Full path to the config file, e.g. C:\\temp\\config.json")]
    [DefaultValue("config.json")]
    public required string Config { get; init; }

    [CommandOption("--tablereport")]
    [Description("Print a table report to the console after the config file has been created")]
    [DefaultValue(true)]
    public bool TableReport { get; init; }
}
