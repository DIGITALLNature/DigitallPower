using System.ComponentModel;
using dgt.power.common;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.export.Base;

public class ExportVerb : BaseProgramSettings
{
    [CommandArgument(1, "[Task]")]
    [Description("Possible tasks: userroles, duplicaterules, teamtemplates, bulkdeletes, savedquerys, queues, documenttemplates, calendars, slaconfigs, outlooktemplates")]
    public string Task { get; set; }

    [CommandOption("--filedir")]
    [Description("Full path to the file dir, e.g.C:\\temp")]
    [DefaultValue(".")]
    public string FileDir { get; init; }

    [CommandOption("--filename")]
    [Description("Exportfile name, default is <task>.json")]
    public string FileName { get; init; }


    [CommandOption("--inline")] public string InlineData { get; init; }
}
