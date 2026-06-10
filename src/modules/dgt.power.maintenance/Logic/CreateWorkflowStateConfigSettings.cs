// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.common;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.Logic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class CreateWorkflowStateConfigSettings : BaseProgramSettings
{
    [CommandOption("-o|--output")]
    [Description("Full path to the output file, e.g. C:\\temp\\config.json")]
    [DefaultValue("config.json")]
    public required string Config { get; init; }

    [CommandOption("--overwrite")]
    [Description("If set to true the output file will be overwritten if it exists")]
    [DefaultValue(false)]
    public bool Overwrite { get; init; }

    [CommandOption("-s|--solutions")]
    [Description("Comma separated list of solution uniquenames to consider. Wildcards (%) are allowed")]
    public string? Solutions { get; set; }

    [CommandOption("-p|--publishers")]
    [Description("Comma separated list of publisher names to consider. Wildcards (%) are allowed")]
    public string? Publishers { get; set; }

    [CommandOption("--tablereport")]
    [Description("Print a table report to the console after the config file has been created")]
    [DefaultValue(true)]
    public bool TableReport { get; init; }

    [CommandOption("--detailed")]
    [Description("If set to true, a full config of all processes will be created instead of only listing processes that are disabled or where the owner is changed")]
    [DefaultValue(false)]
    public bool Detailed { get; init; }
}
