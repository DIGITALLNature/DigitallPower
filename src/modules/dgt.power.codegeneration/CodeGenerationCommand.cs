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
    IMetadataService metadataService,
    IAnsiConsole console)
    : Command<CodeGenerationVerb>, IPowerLogic
{
    protected override int Execute(CommandContext context, CodeGenerationVerb verb, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(verb);
        tracer.Start(this);

        // Try V2 config first (has "type" discriminator)
        var configPath = verb.Config;
        CodeGenerationConfigBase? v2Config = null;
        try
        {
            if (File.Exists(configPath))
            {
                v2Config = CodeGenerationConfigFactory.ResolveFromFile(configPath, console);
            }
        }
        catch
        {
            // Fall through to legacy path
        }

        var success = true;

        console.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .Start("Generate Model ...", _ =>
                {
                    if (v2Config != null)
                    {
                        metadataService.PopulateEntitiesAndSolutions(v2Config);

                        switch (v2Config)
                        {
                            case DotNetCodeGenerationConfig:
                                success &= dotNetWorker.Invoke(verb);
                                break;
                            case TypeScriptCodeGenerationConfig:
                                success &= typescriptWorker.Invoke(verb);
                                break;
                        }
                    }
                    else
                    {
                        // Legacy V1 path
#pragma warning disable CS0618
                        if (!configResolver.TryGetConfigFile<CodeGenerationConfig>(configPath, out var config))
                        {
                            success = false;
                            return;
                        }

                        metadataService.PopulateEntitiesAndSolutions(config);

                        if (!config.SuppressDotNet)
                        {
                            success &= dotNetWorker.Invoke(verb);
                        }

                        if (!config.SuppressTypeScript)
                        {
                            success &= typescriptWorker.Invoke(verb);
                        }
#pragma warning restore CS0618
                    }
                }
            );

        return tracer.End(this, success) ? 0 : -1;
    }
}
