using dgt.power.common;
using Spectre.Console.Cli;
using System.ComponentModel;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618


namespace dgt.power.push.Base;

public class PushVerb : BaseProgramSettings
{
    [CommandArgument(1, "[DllFile]")]
    [Description("Full path to the dll file, e.g. C:\\temp\\plugin.dll")]
    public required string DllFile { get; set; }

    [CommandOption("--solution")]
    [Description("Add assembly to Solution; default: none")]
    public string Solution { get; set; }
}
