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
        var result = CodeGenerationConfigFactory.ResolveFromFile(verb.Config, Console);

        var success = true;

        await Console.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .StartAsync("Generate Model ...", async _ =>
                {
                    success = result switch
                    {
                        CodeGenerationConfigResult.V2(var config) => await ExecuteV2Async(verb, config, cancellationToken),
                        CodeGenerationConfigResult.V1(var config) => await ExecuteV1Async(verb, config, cancellationToken),
                        _ => false
                    };
                }
            );

        return success;
    }

    private async Task<bool> ExecuteV2Async(CodeGenerationVerb verb, CodeGenerationConfigBase config, CancellationToken cancellationToken)
    {
        await metadataService.PopulateEntitiesAndSolutionsAsync(config, cancellationToken);

        return config switch
        {
            DotNetCodeGenerationConfig dotnetConfig => await dotNetGenerator.GenerateAsync(verb, dotnetConfig, cancellationToken),
            TypeScriptCodeGenerationConfig tsConfig => await typeScriptGenerator.GenerateAsync(verb, tsConfig, cancellationToken),
            _ => false
        };
    }

    private async Task<bool> ExecuteV1Async(CodeGenerationVerb verb, CodeGenerationConfig config, CancellationToken cancellationToken)
    {
        await metadataService.PopulateEntitiesAndSolutionsAsync(config, cancellationToken);

        var success = true;

        if (!config.SuppressDotNet)
        {
            success &= await dotNetGenerator.GenerateAsync(verb, config.ToDotNetConfig(), cancellationToken);
        }

        if (!config.SuppressTypeScript)
        {
            success &= await typeScriptGenerator.GenerateAsync(verb, config, cancellationToken);
        }

        return success;
    }
}
