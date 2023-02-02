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
    private readonly IXrmConnection _connection;
    private readonly DotNetCommand _dotNetCommand;
    private readonly TypescriptCommand _typescriptCommand;
    private readonly MetadataCommand _metadataCommand;
    private readonly IMetadataService _metadataService;

    public CodeGenerationCommand(
        ITracer tracer,
        IConfigResolver configResolver,
        IXrmConnection connection,
        DotNetCommand dotNetCommand,
        TypescriptCommand typescriptCommand,
        MetadataCommand metadataCommand,
        IMetadataService metadataService)

    {
        _tracer = tracer;
        _configResolver = configResolver;
        _connection = connection;
        _dotNetCommand = dotNetCommand;
        _typescriptCommand = typescriptCommand;
        _metadataCommand = metadataCommand;
        _metadataService = metadataService;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] CodeGenerationVerb verb)
    {
        _tracer.Start(this);
        if (!_configResolver.GetConfigFile<CodeGenerationConfig>(verb.Config, out var config))
        {
            return _tracer.End(this, false) ? 0 : -1;
        }

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .Start("Generate Model ...", ctx =>
                {
                    _metadataService.PopulateEntitiesAndSolutions(config);

                    if (!config.SuppressDotNet)
                    {
                        _dotNetCommand.Execute(context, verb);
                    }

                    if (!config.SuppressTypeScript)
                    {
                        _typescriptCommand.Execute(context, verb);
                    }

                    if (!config.SuppressMetaData)
                    {
                        _metadataCommand.Execute(context, verb);
                    }
                }
            );

        return _tracer.End(this, true) ? 0 : -1;
    }
}
