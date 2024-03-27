// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates.ts;
using dgt.power.codegeneration.Templates.tsl;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using static dgt.power.codegeneration.Constants.FileNames;

namespace dgt.power.codegeneration.Generators;

public class TypescriptGenerator : ITypescriptGenerator
{
    private readonly IMetadataService _metadataService;
    private TypescriptGeneratorVersion _generatorVersion;

    public TypescriptGenerator(IMetadataService metadataService)
    {
        _metadataService = metadataService;
    }

    /// <summary>
    /// Prepares the directory for code generation.
    /// </summary>
    /// <param name="args">The code generation arguments.</param>
    public void PrepareDirectory(CodeGenerationVerb args)
    {
        // Ensure that the arguments are not null
        Debug.Assert(args != null, nameof(args) + " != null");

        // Create the main folder if it doesn't exist
        var mainFolderPath = Path.Combine(args.TargetDirectory, args.Folder);
        if (!Directory.Exists(mainFolderPath))
        {
            Directory.CreateDirectory(mainFolderPath);
        }

        // Create the Typescript folder if it doesn't exist
        var typescriptFolderPath = Path.Combine(mainFolderPath, Folders.Typescript);
        if (!Directory.Exists(typescriptFolderPath))
        {
            Directory.CreateDirectory(typescriptFolderPath);
        }
        else
        {
            // Delete all .ts files in the Typescript folder
            var typescriptFiles = Directory.GetFiles(typescriptFolderPath, "*.ts");
            foreach (var file in typescriptFiles)
            {
                File.Delete(file);
            }
        }
    }

    public void GenerateBoilerPlateFull(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");


            CreateTemplateFile(new D365ModelTemplate(config.TypingPath), Typescript.Model, args);
            CreateTemplateFile(new D365ServicesTemplate(config.TypingPath), Typescript.Services, args);
            CreateTemplateFile(new D365WebApiTemplate(config.TypingPath), Typescript.Webapi, args);
            CreateTemplateFile(new D365UtilsTemplate(config.TypingPath), Typescript.Utils, args);
            CreateTemplateFile(new D365ODataTemplate(config.TypingPath), Typescript.Odata, args);
    }

    /// <summary>
    /// Creates a template file using the provided template, name, and code generation arguments.
    /// </summary>
    /// <param name="template">The template to use for generating the file content.</param>
    /// <param name="name">The name of the file to be created.</param>
    /// <param name="args">The code generation arguments, including the target directory and folder.</param>
    public void CreateTemplateFile(ITemplate template, string name, CodeGenerationVerb args)
    {
        // Ensure that the template and args are not null
        Debug.Assert(template != null, nameof(template) + " != null");
        Debug.Assert(args != null, nameof(args) + " != null");

        // Combine the target directory, folder, and file name to create the full path
        var path = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript, $"{name}.ts");

        // Create a text file at the specified path
        using var file = File.CreateText(path);
        // Print a message indicating the file creation
        AnsiConsole.MarkupLine($"Creating File: [bold green] {path} [/]");
        // Generate the content using the provided template
        var content = template.GenerateTemplate();

        // Write the generated content to the file
        file.Write(content);
    }

    /// <summary>
    /// Generates TypeScript entities based on the provided code generation arguments and configuration.
    /// </summary>
    /// <param name="args">The code generation arguments.</param>
    /// <param name="config">The code generation configuration.</param>
    public void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        // Ensure that the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        // Iterate through each entity in the configuration
        foreach (var entity in config.Entities)
        {
            // Retrieve the metadata for the entity
            var metadata = _metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);

            // Determine the template to use based on the TypeScript generator version
            ITemplate template;
            if (config.TypescriptGeneratorVersion == TypescriptGeneratorVersion.Full)
            {
                // Use the D365EntityTemplate for the full TypeScript generator version
                template = new D365EntityTemplate(config.TypingPath, metadata, config, _metadataService.RetrieveOrganizationLanguage());
            }
            else
            {
                // Use the EntityLightTemplate for other TypeScript generator versions
                template = new EntityLightTemplate(config.TypingPath, metadata, config, _metadataService.RetrieveOrganizationLanguage());
            }

            // Create the template file
            CreateTemplateFile(template, $"{metadata.LogicalName.ToLowerInvariant()}.{Typescript.Entity}", args);
        }
    }

    public void GenerateEntityRefsFull(CodeGenerationVerb args, CodeGenerationConfig config)
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

    public void GenerateEntityFormsFull(CodeGenerationVerb args, CodeGenerationConfig config)
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

    public void GenerateBusinessProcessFlowsFull(CodeGenerationVerb args, CodeGenerationConfig config)
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

    public void GeneratorVersion(TypescriptGeneratorVersion generatorVersion) => _generatorVersion = generatorVersion;
}
