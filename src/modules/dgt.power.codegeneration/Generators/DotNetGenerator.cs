using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates.dotnet;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using static dgt.power.codegeneration.Constants.FileNames;

namespace dgt.power.codegeneration.Generators;

public class DotNetGenerator : IDotNetGenerator
{
    private readonly IMetadataService _metadataService;

    public DotNetGenerator(IMetadataService metadataService)
    {
        _metadataService = metadataService;
    }

    public void PrepareDirectory(CodeGenerationVerb args)
    {
        if (!Directory.Exists(Path.Combine(args.TargetDirectory, args.Folder)))
        {
            Directory.CreateDirectory(Path.Combine(args.TargetDirectory, args.Folder));
        }

        if (!Directory.Exists(Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet)))
        {
            Directory.CreateDirectory(Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet));
        }
        else
        {
            Directory.GetFiles(Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet),
                "*.cs").ToList().ForEach(File.Delete);
        }
    }

    public void GenerateActions(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        if (config.SuppressActions) return;


        var actions = _metadataService.RetrieveActions(config);
        var apis = _metadataService.RetrieveCustomAPIs(config);
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.Actions}.cs");

        AnsiConsole.MarkupLine($"Creating File: [bold green]{fileName}[/]");

        using var file = File.CreateText(fileName);
        var content = new ActionTemplate(actions.Concat(apis), config.NameSpace).TransformText();
        file.Write(content);
    }

    public void GenerateSdkMessages(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        if (config.SuppressSdkMessages) return;
        var sdkMessages = _metadataService.RetrieveSdkMessageNames(config);
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.SdkMessageNames}.cs");
        using var file = File.CreateText(fileName);
        AnsiConsole.MarkupLine($"Creating File: [bold green]{fileName}[/]");
        var content = new SdkMessagesTemplate(sdkMessages, config).TransformText();

        file.Write(content);
    }

    public void GenerateOptionSets(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        if (!config.GlobalOptionSets.Any()) return;

        var optionSets = _metadataService.RetrieveOptionSets(config);
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.OptionSetValues}.cs");

        using var file = File.CreateText(fileName);
        AnsiConsole.MarkupLine($"Creating File: [bold green]{fileName}[/]");
        var content = new OptionSetsTemplate(optionSets, config).TransformText();

        file.Write(content);
    }

    public void GenerateContext(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        var contextFile = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.Context}.cs");

        using var file = File.CreateText(contextFile);
        AnsiConsole.MarkupLine($"Creating File: [bold green]{contextFile}[/]");
        var content = new ContextTemplate(config.NameSpace).TransformText();

        file.Write(content);
    }

    public void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        foreach (var entity in config.Entities)
        {
            var metadata = _metadataService.RetrieveEntityMetadata(entity,
                EntityFilters.Attributes | EntityFilters.Relationships | EntityFilters.Entity);

            var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet,
                $"{Formatter.CamelCase(metadata.SchemaName)}.cs");

            using var file = File.CreateText(fileName);
            AnsiConsole.MarkupLine($"Creating File: [bold green]{fileName}[/]");
            var content = new EntityTemplate(metadata,
                logicalName => _metadataService.RetrieveEntityMetadata(logicalName),
                config,
                _metadataService.RetrieveOrganizationLanguage()).TransformText();

            file.Write(content);
        }
    }
}
