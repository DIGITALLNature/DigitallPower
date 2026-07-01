// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.codegeneration;

public class CodeGenerationCommand(
    ITracer tracer,
    IDotNetGenerator dotNetGenerator,
    ITypeScriptGenerator typeScriptGenerator,
    IMetadataService metadataService,
    IAnsiConsole console)
    : Command<CodeGenerationVerb>, IPowerLogic
{
    protected override int Execute(CommandContext context, CodeGenerationVerb verb, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(verb);
        tracer.Start(this);

        var result = CodeGenerationConfigFactory.ResolveFromFile(verb.Config, console);

        var success = true;

        console.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .Start("Generate Model ...", _ =>
                {
                    success = result switch
                    {
                        CodeGenerationConfigResult.V2(var config) => ExecuteV2(verb, config),
                        CodeGenerationConfigResult.V1(var config) => ExecuteV1(verb, config),
                        _ => false
                    };
                }
            );

        return tracer.End(this, success) ? 0 : -1;
    }

    private bool ExecuteV2(CodeGenerationVerb verb, CodeGenerationConfigBase config)
    {
        metadataService.PopulateEntitiesAndSolutions(config);

        return config switch
        {
            DotNetCodeGenerationConfig dotnetConfig => dotNetGenerator.Generate(verb, dotnetConfig),
            TypeScriptCodeGenerationConfig tsConfig => typeScriptGenerator.Generate(verb, tsConfig),
            _ => false
        };
    }

    private bool ExecuteV1(CodeGenerationVerb verb, CodeGenerationConfig config)
    {
        metadataService.PopulateEntitiesAndSolutions(config);

        var success = true;

        if (!config.SuppressDotNet)
        {
            success &= dotNetGenerator.Generate(verb, config.ToDotNetConfig());
        }

        if (!config.SuppressTypeScript)
        {
            success &= typeScriptGenerator.Generate(verb, config);
        }

        return success;
    }
}
