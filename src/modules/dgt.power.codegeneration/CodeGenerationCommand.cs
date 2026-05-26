// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.codegeneration;

public class CodeGenerationCommand : Command<CodeGenerationVerb>, IPowerLogic
{
    private readonly ITracer _tracer;
    private readonly IConfigResolver _configResolver;
    private readonly DotNetWorker _dotNetWorker;
    private readonly TypescriptWorker _typescriptWorker;
    private readonly MetadataWorker _metadataWorker;
    private readonly IMetadataService _metadataService;

    public CodeGenerationCommand(
        ITracer tracer,
        IConfigResolver configResolver,
        DotNetWorker dotNetWorker,
        TypescriptWorker typescriptWorker,
        MetadataWorker metadataWorker,
        IMetadataService metadataService)

    {
        _tracer = tracer;
        _configResolver = configResolver;
        _dotNetWorker = dotNetWorker;
        _typescriptWorker = typescriptWorker;
        _metadataWorker = metadataWorker;
        _metadataService = metadataService;
    }

    protected override int Execute([NotNull] CommandContext context, [NotNull] CodeGenerationVerb verb, CancellationToken cancellationToken)
    {
        _tracer.Start(this);
        if (!_configResolver.TryGetConfigFile<CodeGenerationConfig>(verb.Config, out var config))
        {
            return _tracer.End(this, false) ? 0 : -1;
        }

        var success = true;

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .Start("Generate Model ...", ctx =>
                {
                    _metadataService.PopulateEntitiesAndSolutions(config);

                    if (!config.SuppressDotNet)
                    {
                        success &= _dotNetWorker.Invoke(verb);
                    }

                    if (!config.SuppressTypeScript)
                    {
                        success &= _typescriptWorker.Invoke(verb);
                    }

                    if (!config.SuppressMetaData)
                    {
                        success &= _metadataWorker.Invoke(verb);
                    }
                }
            );

        return _tracer.End(this, success) ? 0 : -1;
    }
}
