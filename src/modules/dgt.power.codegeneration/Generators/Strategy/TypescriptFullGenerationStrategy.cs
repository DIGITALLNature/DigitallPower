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
    public async Task<bool> GenerateAsync(CodeGenerationVerb args, CodeGenerationConfig config, CancellationToken cancellationToken = default)
    {
        PrepareDirectory(args);
        await GenerateEntitiesAsync(args, config, cancellationToken);
        await GenerateEntityFormsAsync(args, config, cancellationToken);
        await GenerateOptionSetsAsync(args, config, cancellationToken);
        await GenerateSdkMessagesAsync(args, config, cancellationToken);
        GenerateBoilerPlateFull(args, config);
        await GenerateEntityRefsFullAsync(args, config, cancellationToken);
        await GenerateBusinessProcessFlowsFullAsync(args, config, cancellationToken);
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

    private async Task GenerateEntitiesAsync(CodeGenerationVerb args, CodeGenerationConfig config, CancellationToken cancellationToken)
    {
        // Ensure that the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var systemLanguage = await metadataService.RetrieveOrganizationLanguageAsync(cancellationToken);

        // Iterate through each entity in the configuration
        foreach (var entity in config.Entities)
        {
            // Retrieve the metadata for the entity
            var metadata =
                await metadataService.RetrieveEntityMetadataAsync(entity, EntityFilters.Attributes | EntityFilters.Entity, cancellationToken);

            var model = TsLiquidTemplateModelFactory.CreateEntityModel(config.TypingPath, metadata, config, systemLanguage);
            CreateLiquidTemplateFile("D365Entity.liquid", model, $"{metadata.LogicalName.ToLowerInvariant()}.{FileNames.Typescript.FileNamePart.Entity}", args);
        }
    }

    private async Task GenerateEntityRefsFullAsync(CodeGenerationVerb args, CodeGenerationConfig config, CancellationToken cancellationToken)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var systemLanguage = await metadataService.RetrieveOrganizationLanguageAsync(cancellationToken);

        foreach (var entity in config.Entities)
        {
            var metadata =
                await metadataService.RetrieveEntityMetadataAsync(entity, EntityFilters.Attributes | EntityFilters.Entity, cancellationToken);

            var model = TsLiquidTemplateModelFactory.CreateEntityRefModel(config.TypingPath, metadata, config, systemLanguage);
            CreateLiquidTemplateFile("D365EntityRef.liquid", model, $"{metadata.LogicalName.ToLowerInvariant()}.{FileNames.Typescript.FileNamePart.EntityRef}", args);
        }
    }

    private async Task GenerateEntityFormsAsync(CodeGenerationVerb args, CodeGenerationConfig config, CancellationToken cancellationToken)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        foreach (var entity in config.Entities)
        {
            await GenerateEntityFormForEntityAsync(args, config, entity, cancellationToken);
        }
    }

    private async Task GenerateSdkMessagesAsync(CodeGenerationVerb args, CodeGenerationConfig config, CancellationToken cancellationToken)
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
        var sdkMessages = await metadataService.RetrieveSdkMessageNamesAsync(config, cancellationToken);

        var model = TsLiquidTemplateModelFactory.CreateSdkMessagesModel(sdkMessages, config);
        CreateLiquidTemplateFile("D365SdkMessages.liquid", model, $"{FileNames.Typescript.FileNames.SdkMessageNames}", args);
    }

    private async Task GenerateOptionSetsAsync(CodeGenerationVerb args, CodeGenerationConfig config, CancellationToken cancellationToken)
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
        var optionSets = await metadataService.RetrieveOptionSetsAsync(config, cancellationToken);

        var model = TsLiquidTemplateModelFactory.CreateOptionSetsModel(optionSets);
        CreateLiquidTemplateFile("D365OptionSets.liquid", model, $"{FileNames.Typescript.FileNames.OptionSetValues}", args);
    }

    private async Task GenerateBusinessProcessFlowsFullAsync(CodeGenerationVerb args, CodeGenerationConfig config, CancellationToken cancellationToken)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.BusinessProcessFlows.Count == 0)
        {
            return;
        }

        var bpfs = await metadataService.RetrieveBusinessProcessFlowsAsync(config, cancellationToken);
        foreach (var bpf in bpfs)
        {
            var stages = await metadataService.RetrieveBusinessProcessFlowStagesAsync(bpf.Item3, cancellationToken);
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
    private async Task GenerateEntityFormForEntityAsync(CodeGenerationVerb args, CodeGenerationConfig config, string entityLogicalName, CancellationToken cancellationToken)
    {
        var entityMetadata =
                await metadataService.RetrieveEntityMetadataAsync(entityLogicalName, EntityFilters.Attributes | EntityFilters.Entity, cancellationToken);

        var languageCode = config.UseBaseLanguage
            ? await metadataService.RetrieveOrganizationLanguageAsync(cancellationToken)
            : 1031;

        var sessionLanguageCode = await metadataService.RetrieveConnectionUserLanguageAsync(cancellationToken);
        if (sessionLanguageCode != languageCode)
        {
            Console.MarkupLine(Warnings.FormNameLanguageMismatch(sessionLanguageCode, languageCode));
        }

        var forms = config.OnlyFormsFromSolutions
            ? await metadataService.RetrieveFormsDetailsFromSolutionsAsync(entityMetadata.LogicalName, [.. config.Solutions], null, cancellationToken)
            : await metadataService.RetrieveFormsDetailsAsync(entityMetadata.LogicalName, null, cancellationToken);

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
