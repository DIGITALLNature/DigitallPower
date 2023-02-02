using System.ComponentModel;
using dgt.power.common;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.analyzer.Base;

public class AnalyzeVerb : BaseProgramSettings
{
    [CommandArgument(1, "[Tasks]")]
    [Description("entityallassets|noactivelayer|redundantcomponents")]
    public string Tasks { get; init; }

    [CommandOption("--inline")] public string InlineData { get; init; }

    [CommandOption("--note-patches")] public bool NotePatch { get; init; }

    [CommandOption("--generate-report")] public bool GenerateReportFile { get; init; }


    [CommandOption("--generate-summary")] public bool GenerateSummaryFile { get; init; }

    [CommandOption("-c|--config")]
    [Description("Full path to the config file, e.g. C:\\temp\\config.json")]
    public string Config { get; init; } = "config.json";
}
