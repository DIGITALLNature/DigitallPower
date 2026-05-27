// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates;
using dgt.power.codegeneration.Templates.ts;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using Fluid;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.codegeneration.Generators.Worker;

public class TypescriptGeneratorWorkerFull(IMetadataService metadataService, IAnsiConsole console)
    : TypescriptGeneratorWorker(console), ITypescriptGenerator
{
    #region ITypescriptGenerator Members

    public void GenerateBoilerPlateFull(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var context = TsLiquidRenderer.CreateContext();
        context.SetValue("TypingPath", config.TypingPath);

        CreateLiquidTemplateFile("D365Model.liquid", context, FileNames.Typescript.FileNamePart.Model, args);
        CreateLiquidTemplateFile("D365Services.liquid", context, FileNames.Typescript.FileNamePart.Services, args);
        CreateLiquidTemplateFile("D365WebApi.liquid", context, FileNames.Typescript.FileNamePart.Webapi, args);
        CreateLiquidTemplateFile("D365Utils.liquid", context, FileNames.Typescript.FileNamePart.Utils, args);
        CreateLiquidTemplateFile("D365OData.liquid", context, FileNames.Typescript.FileNamePart.Odata, args);
    }

    /// <summary>
    ///     Creates a template file using the provided template, name, and code generation arguments.
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
        _console.MarkupLine($"Creating File: [bold green] {path} [/]");
        // Generate the content using the provided template
        var content = template.GenerateTemplate();

        // Write the generated content to the file
        file.Write(content);
    }

    private void CreateLiquidTemplateFile(string templateName, TemplateContext context, string name, CodeGenerationVerb args)
    {
        var path = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript, $"{name}.ts");
        using var file = File.CreateText(path);
        _console.MarkupLine($"Creating File: [bold green] {path} [/]");
        file.Write(TsLiquidRenderer.Render(templateName, context));
    }

    /// <summary>
    ///     Generates TypeScript entities based on the provided code generation arguments and configuration.
    /// </summary>
    /// <param name="args">The code generation arguments.</param>
    /// <param name="config">The code generation configuration.</param>
    public void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        // Ensure that the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        CustomLiquidFilters.SetConsole(_console);
        var options = new TemplateOptions();
        options.Filters.AddFilter("camelcase", CustomLiquidFilters.CamelCase);
        options.Filters.AddFilter("sanitize", CustomLiquidFilters.Sanitize);
        options.Filters.AddFilter("unique", CustomLiquidFilters.Unique);
        options.ValueConverters.Add(o => o is AttributeMetadata p ? new AttributeMetadataViewModel(p) : null);
        options.ValueConverters.Add(o => o is OptionMetadata l ? new OptionMetadataViewModel(l) : null);
        options.MemberAccessStrategy.Register<AttributeMetadataViewModel>();
        options.MemberAccessStrategy.Register<OptionMetadataViewModel>();

        // Iterate through each entity in the configuration
        foreach (var entity in config.Entities)
        {
            // Retrieve the metadata for the entity
            var metadata =
                metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);


            ITemplate template =
                new D365EntityTemplate(config.TypingPath, metadata, config,
                metadataService.RetrieveOrganizationLanguage());
            // Create the template file
            CreateTemplateFile(template, $"{metadata.LogicalName.ToLowerInvariant()}.{FileNames.Typescript.FileNamePart.Entity}", args);
        }
    }

    public void GenerateEntityRefsFull(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        foreach (var entity in config.Entities)
        {
            var metadata =
                metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);

            var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript,
                $"{metadata.LogicalName.ToLowerInvariant()}.{FileNames.Typescript.FileNamePart.EntityRef}.ts");

            using var file = File.CreateText(fileName);
            _console.MarkupLine($"Creating File: [bold green] {fileName} [/]");
            var content =
                new D365EntityRefTemplate(config.TypingPath, metadata, config,
                        metadataService.RetrieveOrganizationLanguage())
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
            GenerateEntityFormForEntity(args, config, entity);
        }
    }

    /// <summary>
    ///     Generates SDK messages for code generation.
    /// </summary>
    /// <param name="args">The code generation verb arguments.</param>
    /// <param name="config">The code generation configuration.</param>
    public void GenerateSdkMessages(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        // Check if the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        // Check if SDK messages should be suppressed
        if (config.SuppressSdkMessages)
        {
            return;
        }

        // Retrieve SDK message names from the metadata service
        var sdkMessages = metadataService.RetrieveSdkMessageNames(config);

        // Determine the template to use based on the TypeScript generator version
        ITemplate template;

        // Use the D365EntityTemplate for the full TypeScript generator version
        template = new D365SdkMessagesTemplate(sdkMessages, config);

        // Create the template file
        CreateTemplateFile(template, $"{FileNames.Typescript.FileNames.SdkMessageNames}", args);
    }

    /// <summary>
    ///     Generates option sets based on the provided arguments and configuration
    /// </summary>
    /// <param name="args">The code generation verb</param>
    /// <param name="config">The code generation configuration</param>
    public void GenerateOptionSets(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        // Ensure that the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        // Check if there are global option sets
        if (config.GlobalOptionSets.Count == 0)
        {
            return;
        }

        // Retrieve option sets from the metadata service
        var optionSets = metadataService.RetrieveOptionSets(config);

        // Determine the template to use based on the TypeScript generator version
        ITemplate template;

        // Use the D365EntityTemplate for the full TypeScript generator version
        template = new D365OptionSetsTemplate(optionSets, config);

        // Create the template file
        CreateTemplateFile(template, $"{FileNames.Typescript.FileNames.OptionSetValues}", args);
    }

    public void GenerateBusinessProcessFlowsFull(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.BusinessProcessFlows.Count == 0)
        {
            return;
        }

        var bpfs = metadataService.RetrieveBusinessProcessFlows(config);
        foreach (var bpf in bpfs)
        {
            var stages = metadataService.RetrieveBusinessProcessFlowStages(bpf.Item3);
            var fileName = Path.Combine(args.TargetDirectory, args.Folder, "TypeScript", $"{bpf.Item4}.bpf.ts");

            using var file = File.CreateText(fileName);
            _console.MarkupLine($"Creating File: [bold green] {fileName} [/]");
            var content =
                new D365BusinessProcessFlowTemplate(config.TypingPath, bpf, stages, config).TransformText();

            file.Write(content);
        }
    }

    #endregion

    /// <summary>
    /// Inner logic of generate entity forms
    /// </summary>
    /// <param name="args"></param>
    /// <param name="config"></param>
    /// <param name="entityLogicalName"></param>
    private void GenerateEntityFormForEntity(CodeGenerationVerb args, CodeGenerationConfig config, string entityLogicalName)
    {
        var entityMetadata =
                metadataService.RetrieveEntityMetadata(entityLogicalName, EntityFilters.Attributes | EntityFilters.Entity);

        var forms = config.OnlyFormsFromSolutions
            ? metadataService.RetrieveFormsDetailsFromSolutions(entityMetadata.LogicalName, config.Solutions, null)
            : metadataService.RetrieveFormsDetails(entityMetadata.LogicalName, null);

        foreach (var formDetail in forms)
        {
            GenerateFormDetailFile(args, config, entityMetadata, formDetail);
        }
    }

    /// <summary>
    /// Internal loop logic for generating the form detail file
    /// </summary>
    /// <param name="args"></param>
    /// <param name="config"></param>
    /// <param name="entityMetadata"></param>
    /// <param name="formDetail"></param>
    private void GenerateFormDetailFile(CodeGenerationVerb args, CodeGenerationConfig config, EntityMetadata entityMetadata, KeyValuePair<string, FormDetail> formDetail)
    {
        var form =
               $"{entityMetadata.LogicalName}.{Formatter.Sanitize(formDetail.Key.ToLowerInvariant(), true).Replace(' ', '_')}.{FileNames.Typescript.FileNamePart.Form}";
        if (config.Forms.Length != 0)
        {
            config.Forms.Where(e => e.EndsWith(".ts", StringComparison.InvariantCulture))
                .ToList()
                .ForEach(e =>
                    _console.MarkupLine(Warnings.TsExtensionDeprecation));
            config.Forms = config.Forms.Select(e =>
                    e.EndsWith(".ts", StringComparison.InvariantCulture)
                        ? e.Remove(e.LastIndexOf(".ts", StringComparison.Ordinal))
                        : e)
                .ToArray();
        }

        if (config.Forms.Length != 0 && !config.Forms.Contains(form))
        {
            _console.MarkupLine($"Skip: {form}");
            return;
        }

        var formname = formDetail.Key
            .Replace(".main", "Main").Replace(".quickview", "QuickView")
            .Replace(".quickcreate", "QuickCreate");


        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript, $"{form}.ts");
        using var file = File.CreateText(fileName);
        _console.MarkupLine($"Creating File: [bold green] {fileName} [/]");
        //TODO: not smart, but works
        var content = new D365EntityFormTemplate(config.TypingPath, form,
            formname, formDetail.Value, entityMetadata, config,
            metadataService.RetrieveOrganizationLanguage()).TransformText();

        file.Write(content);
    }

    #region NotImplemented
    public void GenerateCustomApis(CodeGenerationVerb arg, CodeGenerationConfig config) => throw new NotImplementedException();
    #endregion
}
