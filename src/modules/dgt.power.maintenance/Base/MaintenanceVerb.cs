// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.common;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.maintenance.Base;

public class MaintenanceVerb : BaseProgramSettings
{
    [CommandOption("-c|--config")]
    [Description("Full path to the config file, e.g. C:\\temp\\config.json")]
    [DefaultValue("config.json")]
    public string Config { get; init; }

    [CommandOption("--inline")]
    [Description("Inline data instead of files, only supported for some single tasks!")]
    public string InlineData { get; set; }
}
