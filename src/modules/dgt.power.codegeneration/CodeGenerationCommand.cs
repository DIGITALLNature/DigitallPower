// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using Spectre.Console;

namespace dgt.power.codegeneration;

public class CodeGenerationCommand(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IDotNetGenerator dotNetGenerator,
    ITypeScriptGenerator typeScriptGenerator,
    IMetadataService metadataService,
    IAnsiConsole console)
    : PowerCommand<CodeGenerationVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(CodeGenerationVerb verb, CancellationToken cancellationToken)
    {
        var config = CodeGenerationConfigLoader.LoadFromFile(verb.Config);

        // The synchronous metadata service reads the connection's cached service; connect before it is used.
        await connection.GetServiceAsync();

        var success = true;

        Console.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .Start("Generate Model ...", _ =>
                {
                    success = config switch
                    {
                        DotNetCodeGenerationConfig dotnetConfig => ExecuteV2(verb, dotnetConfig),
                        TypeScriptCodeGenerationConfig tsConfig => ExecuteV2(verb, tsConfig),
                        _ => throw new InvalidOperationException($"Unsupported configuration type '{config.GetType().Name}'.")
                    };
                }
            );

        return success;
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

}
