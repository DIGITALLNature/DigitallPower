// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates.ts;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using static dgt.power.codegeneration.Constants.FileNames;

namespace dgt.power.codegeneration.Generators;

public class TypescriptGenerator : ITypescriptGenerator
{
    private readonly IMetadataService _metadataService;

    public TypescriptGenerator(IMetadataService metadataService)
    {
        _metadataService = metadataService;
    }

    public void PrepareDirectory(CodeGenerationVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");

        if (!Directory.Exists(Path.Combine(args.TargetDirectory, args.Folder)))
        {
            Directory.CreateDirectory(Path.Combine(args.TargetDirectory, args.Folder));
        }

        if (!Directory.Exists(Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript)))
        {
            Directory.CreateDirectory(Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript));
        }
        else
        {
            Directory.GetFiles(Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript), $"*.ts")
                .ToList()
                .ForEach(File.Delete);
        }
    }

    public void GenerateBoilerPlate(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        CreateTemplateFile(new D365ModelTemplate(config.TypingPath), Typescript.Model, args);
        // Temp Solution
        CreateTemplateFile(new D365ServicesTemplate(config.TypingPath), Typescript.Services, args);
        CreateTemplateFile(new D365WebApiTemplate(config.TypingPath), Typescript.Webapi, args);
        CreateTemplateFile(new D365UtilsTemplate(config.TypingPath), Typescript.Utils, args);
        CreateTemplateFile(new D365ODataTemplate(config.TypingPath), Typescript.Odata, args);
    }

    public void CreateTemplateFile(ITemplate template, string name, CodeGenerationVerb args)
    {
        Debug.Assert(template != null, nameof(template) + " != null");
        Debug.Assert(args != null, nameof(args) + " != null");

        var path = Path.Combine(args.TargetDirectory, args.Folder, "TypeScript", $"{name}.ts");

        using var file = File.CreateText(path);
        AnsiConsole.MarkupLine($"Creating File: {path}");
        var content = template.GenerateTemplate();

        file.Write(content);
    }

    public void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        foreach (var entity in config.Entities)
        {
            var metadata = _metadataService
                .RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);

            var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript,
                $"{metadata.LogicalName.ToLowerInvariant()}.{Typescript.Entity}.ts");

            using var file = File.CreateText(fileName);
            AnsiConsole.MarkupLine($"Creating File: [bold green] {fileName} [/]");
            var content =
                new D365EntityTemplate(config.TypingPath, metadata, config,
                        _metadataService.RetrieveOrganizationLanguage())
                    .TransformText();

            file.Write(content);
        }
    }

    public void GenerateEntityRefs(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        foreach (var entity in config.Entities)
        {
            var metadata =
                _metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);

            var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript,
                $"{metadata.LogicalName.ToLowerInvariant()}.{Typescript.EntityRef}.ts");

            using var file = File.CreateText(fileName);
            AnsiConsole.MarkupLine($"Creating File: [bold green] {fileName} [/]");
            var content =
                new D365EntityRefTemplate(config.TypingPath, metadata, config,
                        _metadataService.RetrieveOrganizationLanguage())
                    .TransformText();

            file.Write(content);
        }
    }

    public void GenerateEntityForms(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        foreach (var entity in config.Entities)
        {
            var metadata =
                _metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);

            var forms = config.OnlyFormsFromSolutions
                ? _metadataService.RetrieveFormsDetailsFromSolutions(metadata.LogicalName,
                    config.Solutions)
                : _metadataService.RetrieveFormsDetails(metadata.LogicalName);

            foreach (var formDetail in forms)
            {
                var form =
                    $"{metadata.LogicalName}.{
                        Formatter.Sanitize(formDetail.Key.ToLowerInvariant(), true).Replace(' ', '_')
                    }.{Typescript.Form}";
                var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript, $"{form}.ts");
                if (config.Forms.Any())
                {
                    config.Forms.Where(e => e.EndsWith(".ts", StringComparison.InvariantCulture))
                        .ToList()
                        .ForEach(e =>
                            AnsiConsole.MarkupLine(Warnings.TsExtensionDeprecation));
                    config.Forms = config.Forms.Select(e => e.EndsWith(".ts", StringComparison.InvariantCulture) ? e.Remove(e.LastIndexOf(".ts",StringComparison.Ordinal)) : e)
                        .ToArray();
                }

                if (config.Forms.Any() && !config.Forms.Contains(form))
                {
                    AnsiConsole.MarkupLine($"Skip: {form}");
                    continue;
                }

                using var file = File.CreateText(fileName);
                AnsiConsole.MarkupLine($"Creating File: [bold green] {fileName} [/]");
                //TODO: not smart, but works
                var content = new D365EntityFormTemplate(config.TypingPath, form,
                    formDetail.Key
                        .Replace(".main", "Main").Replace(".quickview", "QuickView")
                        .Replace(".quickcreate", "QuickCreate"),
                    formDetail.Value, metadata, config,
                    _metadataService.RetrieveOrganizationLanguage()).TransformText();

                file.Write(content);
            }
        }
    }

    public void GenerateSdkMessages(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.SuppressSdkMessages)
        {
            return;
        }

        var sdkMessages = _metadataService.RetrieveSdkMessageNames(config);
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript,
            $"{Typescript.SdkMessageNames}.ts");

        using var file = File.CreateText(fileName);
        AnsiConsole.MarkupLine($"Creating File: [bold green] {fileName} [/]");
        var content = new D365SdkMessagesTemplate(sdkMessages, config).TransformText();

        file.Write(content);
    }

    public void GenerateOptionSets(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (!config.GlobalOptionSets.Any())
        {
            return;
        }

        var optionSets = _metadataService.RetrieveOptionSets(config);
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript,
            $"{Typescript.OptionSetValues}.ts");

        using var file = File.CreateText(fileName);
        AnsiConsole.MarkupLine($"Creating File: [bold green] {fileName} [/]");
        var content = new D365OptionSetsTemplate(optionSets, config).TransformText();

        file.Write(content);
    }

    public void GenerateBusinessProcessFlows(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (!config.BusinessProcessFlows.Any())
        {
            return;
        }

        var bpfs = _metadataService.RetrieveBusinessProcessFlows(config);
        foreach (var bpf in bpfs)
        {
            var stages = _metadataService.RetrieveBusinessProcessFlowStages(bpf.Item3);
            var fileName = Path.Combine(args.TargetDirectory, args.Folder, "TypeScript", $"{bpf.Item4}.bpf.ts");

            using var file = File.CreateText(fileName);
            AnsiConsole.MarkupLine($"Creating File: [bold green] {fileName} [/]");
            var content =
                new D365BusinessProcessFlowTemplate(config.TypingPath, bpf, stages, config).TransformText();

            file.Write(content);
        }
    }
}
