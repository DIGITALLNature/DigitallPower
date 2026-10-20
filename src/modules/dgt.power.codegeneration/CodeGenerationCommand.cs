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
    IDotNetGenerator dotNetGenerator,
    ITypeScriptGenerator typeScriptGenerator,
    IMetadataService metadataService,
    IAnsiConsole console)
    : PowerCommand<CodeGenerationVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(CodeGenerationVerb verb, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var config = CodeGenerationConfigLoader.LoadFromFile(verb.Config);

        var success = true;

        await Console.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .StartAsync("Generate Model ...", async _ =>
                {
                    await metadataService.PopulateEntitiesAndSolutionsAsync(config, cancellationToken);
                    success = config switch
                    {
                        DotNetCodeGenerationConfig dotnetConfig => await dotNetGenerator.GenerateAsync(verb, dotnetConfig, cancellationToken),
                        TypeScriptCodeGenerationConfig tsConfig => await typeScriptGenerator.GenerateAsync(verb, tsConfig, cancellationToken),
                        _ => throw new InvalidOperationException($"Unsupported configuration type '{config.GetType().Name}'.")
                    };
                }
            );

        return success;
    }
}
