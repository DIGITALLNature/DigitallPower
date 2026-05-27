// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.codegeneration;

public class CodeGenerationCommand(
    ITracer tracer,
    IConfigResolver configResolver,
    DotNetWorker dotNetWorker,
    TypescriptWorker typescriptWorker,
    MetadataWorker metadataWorker,
    IMetadataService metadataService,
    IAnsiConsole console)
    : Command<CodeGenerationVerb>, IPowerLogic
{
    protected override int Execute(CommandContext context, CodeGenerationVerb verb, CancellationToken cancellationToken)
    {
        tracer.Start(this);
        if (!configResolver.TryGetConfigFile<CodeGenerationConfig>(verb.Config, out var config))
        {
            return tracer.End(this, false) ? 0 : -1;
        }

        var success = true;

        console.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .Start("Generate Model ...", _ =>
                {
                    metadataService.PopulateEntitiesAndSolutions(config);

                    if (!config.SuppressDotNet)
                    {
                        success &= dotNetWorker.Invoke(verb);
                    }

                    if (!config.SuppressTypeScript)
                    {
                        success &= typescriptWorker.Invoke(verb);
                    }

                    if (!config.SuppressMetaData)
                    {
                        success &= metadataWorker.Invoke(verb);
                    }
                }
            );

        return tracer.End(this, success) ? 0 : -1;
    }
}
