// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates.ts;
using Fluid;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.codegeneration.Generators.Strategy;

public class TypescriptFullGenerationStrategy(IMetadataService metadataService, IAnsiConsole console)
    : TypescriptGenerationStrategyBase(console), ITypescriptGenerationStrategy
{
    /// <inheritdoc />
    public bool Generate(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        PrepareDirectory(args);
        GenerateEntities(args, config);
        GenerateEntityForms(args, config);
        GenerateOptionSets(args, config);
        GenerateSdkMessages(args, config);
        GenerateBoilerPlateFull(args, config);
        GenerateEntityRefsFull(args, config);
        GenerateBusinessProcessFlowsFull(args, config);
        return true;
    }

    #region Private Members

    private void GenerateBoilerPlateFull(CodeGenerationVerb args, CodeGenerationConfig config)
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

    private void CreateLiquidTemplateFile(string templateName, TemplateContext context, string name, CodeGenerationVerb args)
    {
        var path = Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript, $"{name}.ts");
        using var file = File.CreateText(path);
        Console.MarkupLine($"Creating File: [bold green] {path} [/]");
        file.Write(TsLiquidRenderer.Render(templateName, context));
    }

    private void CreateLiquidTemplateFile(string templateName, object model, string name, CodeGenerationVerb args)
    {
        var context = TsLiquidRenderer.CreateContext();
        context.SetValue("model", model);
        CreateLiquidTemplateFile(templateName, context, name, args);
    }

    private void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        // Ensure that the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var systemLanguage = metadataService.RetrieveOrganizationLanguage();

        // Iterate through each entity in the configuration
        foreach (var entity in config.Entities)
        {
            // Retrieve the metadata for the entity
            var metadata =
                metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);

            var model = TsLiquidTemplateModelFactory.CreateEntityModel(config.TypingPath, metadata, config, systemLanguage);
            CreateLiquidTemplateFile("D365Entity.liquid", model, $"{metadata.LogicalName.ToLowerInvariant()}.{FileNames.Typescript.FileNamePart.Entity}", args);
        }
    }

    private void GenerateEntityRefsFull(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var systemLanguage = metadataService.RetrieveOrganizationLanguage();

        foreach (var entity in config.Entities)
        {
            var metadata =
                metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);

            var model = TsLiquidTemplateModelFactory.CreateEntityRefModel(config.TypingPath, metadata, config, systemLanguage);
            CreateLiquidTemplateFile("D365EntityRef.liquid", model, $"{metadata.LogicalName.ToLowerInvariant()}.{FileNames.Typescript.FileNamePart.EntityRef}", args);
        }
    }

    private void GenerateEntityForms(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        foreach (var entity in config.Entities)
        {
            GenerateEntityFormForEntity(args, config, entity);
        }
    }

    private void GenerateSdkMessages(CodeGenerationVerb args, CodeGenerationConfig config)
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

        var model = TsLiquidTemplateModelFactory.CreateSdkMessagesModel(sdkMessages, config);
        CreateLiquidTemplateFile("D365SdkMessages.liquid", model, $"{FileNames.Typescript.FileNames.SdkMessageNames}", args);
    }

    private void GenerateOptionSets(CodeGenerationVerb args, CodeGenerationConfig config)
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

        var model = TsLiquidTemplateModelFactory.CreateOptionSetsModel(optionSets);
        CreateLiquidTemplateFile("D365OptionSets.liquid", model, $"{FileNames.Typescript.FileNames.OptionSetValues}", args);
    }

    private void GenerateBusinessProcessFlowsFull(CodeGenerationVerb args, CodeGenerationConfig config)
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
            var model = TsLiquidTemplateModelFactory.CreateBusinessProcessFlowModel(config.TypingPath, bpf, stages);
            CreateLiquidTemplateFile("D365BusinessProcessFlow.liquid", model, $"{bpf.Item4}.bpf", args);
        }
    }

    #endregion

    /// <summary>
    /// Inner logic of generating entity forms
    /// </summary>
    /// <param name="args"></param>
    /// <param name="config"></param>
    /// <param name="entityLogicalName"></param>
    private void GenerateEntityFormForEntity(CodeGenerationVerb args, CodeGenerationConfig config, string entityLogicalName)
    {
        var entityMetadata =
                metadataService.RetrieveEntityMetadata(entityLogicalName, EntityFilters.Attributes | EntityFilters.Entity);

        int languageCode = config.UseBaseLanguage
            ? metadataService.RetrieveOrganizationLanguage()
            : 1031;

        var sessionLanguageCode = metadataService.RetrieveConnectionUserLanguage();
        if (sessionLanguageCode != languageCode)
        {
            Console.MarkupLine(Warnings.FormNameLanguageMismatch(sessionLanguageCode, languageCode));
        }

        var forms = config.OnlyFormsFromSolutions
            ? metadataService.RetrieveFormsDetailsFromSolutions(entityMetadata.LogicalName, [.. config.Solutions], null)
            : metadataService.RetrieveFormsDetails(entityMetadata.LogicalName, null);

        foreach (var formDetail in forms)
        {
            GenerateFormDetailFile(args, config, entityMetadata, formDetail, languageCode);
        }
    }

    /// <summary>
    /// Internal loop logic for generating the form detail file
    /// </summary>
    /// <param name="args"></param>
    /// <param name="config"></param>
    /// <param name="entityMetadata"></param>
    /// <param name="formDetail"></param>
    /// <param name="languageCode">Language LCID to use for localizing option set labels rendered on the form.</param>
    private void GenerateFormDetailFile(CodeGenerationVerb args, CodeGenerationConfig config, EntityMetadata entityMetadata, KeyValuePair<string, FormDetail> formDetail, int languageCode)
    {
        var form =
               $"{entityMetadata.LogicalName}.{Formatter.Sanitize(formDetail.Key.ToLowerInvariant(), true).Replace(' ', '_')}.{FileNames.Typescript.FileNamePart.Form}";
        if (config.Forms.Count != 0)
        {
            config.Forms.Where(e => e.EndsWith(".ts", StringComparison.InvariantCulture))
                .ToList()
                .ForEach(_ =>
                    Console.MarkupLine(Warnings.TsExtensionDeprecation));
            config.Forms = config.Forms.Select(e =>
                    e.EndsWith(".ts", StringComparison.InvariantCulture)
                        ? e.Remove(e.LastIndexOf(".ts", StringComparison.Ordinal))
                        : e)
                .ToArray();
        }

        if (config.Forms.Count != 0 && !config.Forms.Contains(form))
        {
            Console.MarkupLine($"Skip: {form}");
            return;
        }

        var formname = formDetail.Key
            .Replace(".main", "Main", StringComparison.Ordinal).Replace(".quickview", "QuickView", StringComparison.Ordinal)
            .Replace(".quickcreate", "QuickCreate", StringComparison.Ordinal);


        var model = TsLiquidTemplateModelFactory.CreateEntityFormModel(config.TypingPath, form, formname, formDetail.Value, entityMetadata, config, languageCode);
        CreateLiquidTemplateFile("D365EntityForm.liquid", model, form, args);
    }
}
