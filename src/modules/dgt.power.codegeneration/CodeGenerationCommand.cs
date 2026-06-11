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
    IConfigResolver configResolver,
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
                    success = v2Config != null
                        ? ExecuteV2(verb, v2Config)
                        : ExecuteV1(verb);
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

#pragma warning disable CS0618
    private bool ExecuteV1(CodeGenerationVerb verb)
    {
        if (!configResolver.TryGetConfigFile<CodeGenerationConfig>(verb.Config, out var config))
        {
            return false;
        }

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
#pragma warning restore CS0618
}
