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

    [CommandArgument(1, "[Tasks]")]
    [Description("bulkdelete|autonumber")]
    public string Tasks { get; set; }

    [CommandOption("--inline")]
    [Description("Inline data instead of files, only supported for some single tasks!")]
    public string InlineData { get; set; }
}
