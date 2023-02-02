using dgt.power.common;
using Spectre.Console.Cli;
using System.ComponentModel;
using dgt.power.codegeneration.Constants;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.codegeneration.Base;

public class CodeGenerationVerb : BaseProgramSettings
{
    [CommandArgument(1, "[TargetDirectory]")]
    [Description("Full path to the directory where die generated Model classes will be saved")]
    public string TargetDirectory { get; init; }

    [CommandOption("-f|--folder")]
    [Description("Define an alternate name for the model folder. The default will be 'Model'")]
    [DefaultValue(Folders.Model)]
    public string Folder { get; init; } = Folders.Model;

    [CommandOption("-c|--config")]
    [Description("Full path to the config file, e.g. C:\\temp\\config.json")]
    [DefaultValue("config.json")]
    public string Config { get; init; }
}
