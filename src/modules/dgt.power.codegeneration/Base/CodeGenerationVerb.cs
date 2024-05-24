// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
    [Description("Full path to the directory where the generated model classes will be saved")]
    [DefaultValue(".")]
    public string TargetDirectory { get; init; }

    [CommandOption("-f|--folder")]
    [Description("Define an alternate name for the model folder. The default will be 'Model'")]
    [DefaultValue(Folders.Model)]
    public string Folder { get; init; } = Folders.Model ;

    [CommandOption("-c|--config")]
    [Description("Full path to the config file, e.g. C:\\temp\\config.json")]
    [DefaultValue("config.json")]
    public string Config { get; init; }
}
