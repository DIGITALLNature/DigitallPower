// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Services;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using dgt.power.dataverse;
using Fluid;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.codegeneration.Generators;

public class TypeScriptGenerator(IMetadataService metadataService, IAnsiConsole console) : ITypeScriptGenerator
{
    protected IAnsiConsole Console { get; } = console;

    private readonly TemplateOptions _templateOptions = TslTemplateOptionsFactory.Create();

    private const string TslResourcePrefix = "dgt.power.codegeneration.Templates.tsl";

    private void CreateFile(string content, string name, CodeGenerationVerb args, string extension, string[]? outputPath = null)
    {
        Debug.Assert(content != null, $"{nameof(content)} != null");
        Debug.Assert(args != null, $"{nameof(args)} != null");
        Debug.Assert(name != null, $"{nameof(name)} != null");

        var path = Path.Combine([
            args.TargetDirectory,
            args.Folder,
            Folders.Typescript,
            ..outputPath ?? [],
            $"{name}.{extension}"
        ]);

        CreateDirectoryWhenNeeded(Path.GetDirectoryName(path));
        using var file = File.CreateText(path);
        Console.MarkupLine($"Creating File: [bold green] {path} [/]");
        file.Write(content);
    }

    private void CopyTemplateFileContent(CodeGenerationVerb args, string templateFileName, string extension)
    {
        Debug.Assert(templateFileName != null, $"{nameof(templateFileName)} != null");
        Debug.Assert(args != null, $"{nameof(args)} != null");
        Debug.Assert(extension != null, $"{nameof(extension)} != null");

        Console.MarkupLine($"Reading File: [bold green] {templateFileName}.{extension} [/]");

        using var reader = new StreamReader(typeof(TypeScriptGenerator).Assembly
            .GetManifestResourceStream($"dgt.power.codegeneration.Templates.tsl.{templateFileName}.{extension}")!);

        var content = reader.ReadToEnd();
        CreateFile(content, templateFileName, args, extension);
    }

    private static void PrepareDirectory(CodeGenerationVerb args)
    {
        Debug.Assert(args != null, $"{nameof(args)} != null");

        var mainFolderPath = Path.Combine(args.TargetDirectory, args.Folder);
        CreateDirectoryWhenNeeded(mainFolderPath);

        var typescriptFolderPath = Path.Combine(mainFolderPath, Folders.Typescript);
        if (CreateDirectoryWhenNeeded(typescriptFolderPath))
        {
            return;
        }

        var directory = new DirectoryInfo(typescriptFolderPath);
        foreach (var file in directory.GetFiles())
        {
            file.Delete();
        }

        foreach (var dir in directory.GetDirectories())
        {
            dir.Delete(true);
        }
    }

    private static bool CreateDirectoryWhenNeeded(string? directoryPath)
    {
        Debug.Assert(directoryPath != null, $"{nameof(directoryPath)} != null");
        if (Directory.Exists(directoryPath))
        {
            return false;
        }

        Directory.CreateDirectory(directoryPath);
        return true;
    }

    private static IFluidTemplate InitializeLiquidTemplate(string templateName)
    {
        return LiquidTemplateEngine.LoadTemplate(TslResourcePrefix, templateName);
    }

    public bool Generate(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        PrepareDirectory(args);
        GenerateEntities(args, config);
        GenerateEntityForms(args, config);
        GenerateOptionSets(args, config);
        GenerateSdkMessages(args, config);
        GenerateCustomApis(args, config);
        return true;
    }

    private void GenerateEntities(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var liquidTemplate = InitializeLiquidTemplate(LiquidTemplates.Entity);

        var languageCode = config.Language;
        if (config.Language == null)
        {
            languageCode = metadataService.RetrieveOrganizationLanguage();
            Console.MarkupLine($"Using Base Language: {languageCode}");
        }

        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.TsAuxiliaryExtTypes, FileNames.Typescript.FileExtension.TypeExtension);

        foreach (var entity in config.Entities.Names.OrderBy(entityName => entityName))
        {
            var metadata = metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);

            var viewModel = new EntityViewModel
            {
                SchemaName = metadata.SchemaName,
                LogicalName = metadata.LogicalName,
                LanguageCode = languageCode,
                Attributes = FilterEntityMetadataAttributes(metadata)
            };

            var formEntityName = metadata.LogicalName.ToLowerInvariant().Trim();
            var artifactName = $"{formEntityName}.{FileNames.Typescript.FileNamePart.Entity}.{FileNames.Typescript.FileExtension.TypeExtension}";
            var content = RenderTemplateWithDiagnostics(
                liquidTemplate,
                LiquidTemplates.Entity,
                viewModel,
                entityKey: metadata.LogicalName,
                artifact: artifactName);

            CreateFile(content,
                $"{formEntityName}.{FileNames.Typescript.FileNamePart.Entity}",
                args,
                FileNames.Typescript.FileExtension.TypeExtension,
                GetEntityFolderName(metadata.LogicalName, Folders.TypescriptEntity));
        }
    }

    private void GenerateEntityForms(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var liquidTemplateForm = InitializeLiquidTemplate(LiquidTemplates.EntityForm);
        var liquidTemplateFormTestHelpers = InitializeLiquidTemplate(LiquidTemplates.EntityFormTestHelper);

        var languageCode = config.Language ?? metadataService.RetrieveOrganizationLanguage();
        Console.MarkupLine($"Using Language: {languageCode}");

        WarnIfSessionLanguageMismatch(languageCode);

        var bpfControls = GetCompleteEntityBpfControlList(config);
        var entityWithParsedFormList = GenerateEntityWithMetadata(config, bpfControls);
        var flatListParseForm = GetFlatFormDetail(entityWithParsedFormList);
        foreach (var entityParsedForm in entityWithParsedFormList)
        {
            var entityMetada = entityParsedForm.EntityMetadata;
            var bpfControlsForEntity = bpfControls.GetValueOrDefault(entityMetada.LogicalName) ?? [];

            foreach (var parsedForm in entityParsedForm.ParsedFormDetail)
            {
                var formName = $"{entityMetada.LogicalName.Trim()}.{Formatter.Sanitize(parsedForm.Key.ToLowerInvariant().Trim(), true).Replace(' ', '_')}.{FileNames.Typescript.FileNamePart.Form}";
                if (ShouldSkipFormForConfig(config.Output.Forms?.Filter ?? [], formName))
                {
                    Console.MarkupLine($"Skip: {formName}");
                    continue;
                }
                FormParser.MapQuickFormId(parsedForm.Value, flatListParseForm);
                var renderContext = new FormFileRenderContext(
                    parsedForm,
                    entityMetada,
                    liquidTemplateForm,
                    LiquidTemplates.EntityForm,
                    args,
                    formName,
                    bpfControlsForEntity,
                    languageCode);

                CreateFormFile(renderContext);
                if (config.Output.Forms?.TestHelpers ?? false)
                {
                    CreateFormTestHelperFile(renderContext with
                    {
                        LiquidTemplate = liquidTemplateFormTestHelpers,
                        TemplateName = LiquidTemplates.EntityFormTestHelper,
                        FormName = $"{formName}.{FileNames.Typescript.FileNamePart.TestHelper}",
                    });
                }
            }
        }

        if (!(config.Output.Forms?.TestHelpers ?? false))
        {
            return;
        }

        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormODataFilter, FileNames.Typescript.FileExtension.TsExtension);
        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormContextBuilder, FileNames.Typescript.FileExtension.TsExtension);
        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormContextTypes, FileNames.Typescript.FileExtension.TsExtension);
        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockTestTypingsFileName, FileNames.Typescript.FileExtension.TypeExtension);
    }

    private void GenerateSdkMessages(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.Requests.Count == 0)
        {
            return;
        }

        var liquidTemplate = InitializeLiquidTemplate(LiquidTemplates.SdkMessages);
        var sdkMessages = metadataService.RetrieveSdkMessageNames(config.Requests);

        var viewModel = new SdkMessagesViewModel
        {
            SdkMessages = sdkMessages.Select(sdkm => new SdkMessageViewModel(sdkm)).ToList()
        };

        var content = RenderTemplateWithDiagnostics(
            liquidTemplate,
            LiquidTemplates.SdkMessages,
            viewModel,
            artifact: $"{FileNames.Typescript.FileNames.SdkMessageNames}.{FileNames.Typescript.FileExtension.TypeExtension}");

        CreateFile(content, FileNames.Typescript.FileNames.SdkMessageNames, args, FileNames.Typescript.FileExtension.TypeExtension);
    }

    private void GenerateOptionSets(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.OptionSets.Count == 0)
        {
            return;
        }

        var liquidTemplate = InitializeLiquidTemplate(LiquidTemplates.OptionSets);
        var optionSets = metadataService.RetrieveOptionSets(config.OptionSets);

        var viewModel = new OptionSetViewModel
        {
            OptionSets = [..optionSets]
        };

        var content = RenderTemplateWithDiagnostics(
            liquidTemplate,
            LiquidTemplates.OptionSets,
            viewModel,
            artifact: $"{FileNames.Typescript.FileNames.OptionSetValues}.{FileNames.Typescript.FileExtension.TypeExtension}");

        CreateFile(content, FileNames.Typescript.FileNames.OptionSetValues, args, FileNames.Typescript.FileExtension.TypeExtension);
    }

    private void GenerateCustomApis(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.Requests.Count == 0 || !config.Output.CustomApis)
        {
            return;
        }

        // RetrieveRequests auto-detects custom APIs vs actions vs built-in messages.
        // Filter to only entries that have parameters (i.e. actual custom APIs, not plain name constants).
        var allRequests = metadataService.RetrieveRequests(config.Requests);
        var customApis = allRequests.Where(r => r.InParameters.Count > 0 || r.OutParameters.Count > 0).ToList();

        if (customApis.Count == 0)
        {
            return;
        }

        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmWebApiTypingsFileName, FileNames.Typescript.FileExtension.TypeExtension);
        var liquidTemplate = InitializeLiquidTemplate(LiquidTemplates.CustomApi);

        foreach (var customApi in customApis)
        {
            var viewModel = new CustomApiViewModel
            {
                Name = customApi.LogicalName,
                InParameters = customApi.InParameters,
                OutParameters = customApi.OutParameters
            };

            var customApiName = $"{Formatter.Sanitize(customApi.LogicalName.ToLowerInvariant().Trim(), true).Replace(' ', '_')}.{FileNames.Typescript.FileNamePart.CustomApi}";
            var content = RenderTemplateWithDiagnostics(
                liquidTemplate,
                LiquidTemplates.CustomApi,
                viewModel,
                entityKey: customApi.LogicalName,
                artifact: $"{customApiName}.{FileNames.Typescript.FileExtension.TypeExtension}");

            CreateFile(content, customApiName, args, FileNames.Typescript.FileExtension.TypeExtension, [Folders.TypescriptCustomApis]);
        }
    }

    private List<EntityWithMetadataFormData> GenerateEntityWithMetadata(TypeScriptCodeGenerationConfig config, Dictionary<string, SortedSet<BpfControlDetail>> bpfControls)
    {
        var entityWithMetadataForms = new List<EntityWithMetadataFormData>();

        foreach (var entity in config.Entities.Names.OrderBy(entityName => entityName))
        {
            var entityMetadata = metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);
            if (entityMetadata.IsBPFEntity == true)
            {
                Console.MarkupLine($"Skip form generation for BPF Entity {entityMetadata.LogicalName}");
                continue;
            }

            var bpfControlsForEntity = bpfControls.GetValueOrDefault(entity) ?? [];

            var forms = config.Output.Forms?.FromSolutions ?? false
                ? metadataService.RetrieveFormsDetailsFromSolutions(entityMetadata.LogicalName, [.. config.Entities.FromSolutions], bpfControlsForEntity)
                : metadataService.RetrieveFormsDetails(entityMetadata.LogicalName, bpfControlsForEntity);

            entityWithMetadataForms.Add(new EntityWithMetadataFormData
            {
                EntityMetadata = entityMetadata,
                ParsedFormDetail = forms
            });
        }
        return entityWithMetadataForms;
    }

    private Dictionary<string, SortedSet<BpfControlDetail>> GetCompleteEntityBpfControlList(TypeScriptCodeGenerationConfig config)
    {
        var bpfControls = new Dictionary<string, SortedSet<BpfControlDetail>>();
        foreach (var entityName in config.Entities.Names.OrderBy(entityName => entityName))
        {
            var bpfControlsForEntityMain = metadataService.RetrieveBusinessProcessFlowControlsForMainEntity(config.Requests, entityName);
            foreach (var bpfControlForEntityMain in bpfControlsForEntityMain)
            {
                if (bpfControls.TryGetValue(bpfControlForEntityMain.EntityName, out var value))
                {
                    value.Add(bpfControlForEntityMain);
                }
                else
                {
                    bpfControls.Add(bpfControlForEntityMain.EntityName, [bpfControlForEntityMain]);
                }
            }
        }
        return bpfControls;
    }

    #region Shared Helpers

    private sealed record FormFileRenderContext(
        KeyValuePair<string, FormDetail> FormDetail,
        EntityMetadata Metadata,
        IFluidTemplate LiquidTemplate,
        string TemplateName,
        CodeGenerationVerb Args,
        string FormName,
        SortedSet<BpfControlDetail> BpfControls,
        int LanguageCode);

    private void CreateFormFile(FormFileRenderContext context)
    {
        var artifact = $"{context.FormName}.{FileNames.Typescript.FileExtension.TypeExtension}";
        var content = CreateFormFileContent(context, artifact);
        CreateFile(content, context.FormName, context.Args, FileNames.Typescript.FileExtension.TypeExtension, GetEntityFolderName(context.Metadata.LogicalName, Folders.TypescriptEntityForms));
    }

    private void CreateFormTestHelperFile(FormFileRenderContext context)
    {
        var artifact = $"{context.FormName}.{FileNames.Typescript.FileExtension.TsExtension}";
        var content = CreateFormFileContent(context, artifact);
        CreateFile(content, context.FormName, context.Args, FileNames.Typescript.FileExtension.TsExtension, GetEntityFolderName(context.Metadata.LogicalName, Folders.TypescriptEntityTestHelper));
    }

    private string CreateFormFileContent(FormFileRenderContext context, string artifact)
    {
        var formname = context.FormDetail.Key
                    .Replace(".main", "Main", StringComparison.Ordinal)
                    .Replace(".quickview", "QuickView", StringComparison.Ordinal)
                    .Replace(".quickcreate", "QuickCreate", StringComparison.Ordinal);

        var viewModel = new FormViewModel
        {
            Name = formname,
            FormDetail = context.FormDetail.Value,
            Attributes = FilterEntityMetadataAttributes(context.Metadata),
            BpfControls = context.FormDetail.Value.FormType == SystemForm.Options.Type.QuickViewForm ? [] : context.BpfControls.ToList(),
            LanguageCode = context.LanguageCode
        };
        return RenderTemplateWithDiagnostics(
            context.LiquidTemplate,
            context.TemplateName,
            viewModel,
            entityKey: context.Metadata.LogicalName,
            formKey: context.FormDetail.Key,
            artifact: artifact);
    }

    private static List<FormDetail> GetFlatFormDetail(List<EntityWithMetadataFormData> entityWithMetadataFormDatas)
    {
        return entityWithMetadataFormDatas.Select(x => x.ParsedFormDetail.Values.ToList()).SelectMany(x => x).ToList();
    }

    private bool ShouldSkipFormForConfig(IReadOnlyCollection<string> configForms, string form)
    {
        if (configForms.Count == 0)
        {
            return false;
        }
        configForms.Where(e => e.EndsWith(".ts", StringComparison.InvariantCulture))
            .ToList()
            .ForEach(_ => Console.MarkupLine(Warnings.TsExtensionDeprecation));
        configForms = configForms.Select(e => e.EndsWith(".ts", StringComparison.InvariantCulture)
                    ? e.Remove(e.LastIndexOf(".ts", StringComparison.Ordinal))
                    : e)
            .ToArray();

        return !configForms.Contains(form);
    }

    private static List<AttributeMetadata> FilterEntityMetadataAttributes(EntityMetadata metadata)
    {
        return metadata.Attributes
            .Where(IsVisibleInTsEntity)
            .Where(a => !a.LogicalName.Contains("entityimage", StringComparison.Ordinal))
            .Where(a => a.AttributeType != AttributeTypeCode.ManagedProperty)
            .OrderBy(a => a.LogicalName).ToList();
    }

    private static bool IsVisibleInTsEntity(AttributeMetadata attribute)
    {
        var isVisibleInLayout = attribute.IsValidForGrid == true
                                || attribute.IsValidForForm == true
                                || attribute.IsValidODataAttribute
                                || attribute.IsPrimaryId == true;
        if (!isVisibleInLayout)
        {
            return false;
        }

        return attribute.IsValidForCreate == true
               || attribute.IsValidForUpdate == true
               || attribute.IsValidForRead == true;
    }

    private static string[] GetEntityFolderName(string entityLogicalName, string subFolder)
    {
        var formEntityName = entityLogicalName.ToLowerInvariant().Trim();
        return [Folders.TypescriptEntities, Formatter.CamelCase(Formatter.Sanitize(formEntityName)), subFolder];
    }

    /// <summary>
    ///     Warns once when the connecting user's session UI language differs from the language configured for
    ///     code generation, since Dataverse resolves translatable out-of-box record text (e.g. system form
    ///     <c>name</c>) using the session language - not the configured one. See <see cref="Warnings.FormNameLanguageMismatch"/>.
    /// </summary>
    private void WarnIfSessionLanguageMismatch(int configuredLanguageCode)
    {
        var sessionLanguageCode = metadataService.RetrieveConnectionUserLanguage();
        if (sessionLanguageCode != configuredLanguageCode)
        {
            Console.MarkupLine(Warnings.FormNameLanguageMismatch(sessionLanguageCode, configuredLanguageCode));
        }
    }

    private TemplateContext CreateTemplateContext(object viewModel)
    {
        var context = new TemplateContext(viewModel, _templateOptions);
        CustomLiquidFilters.ConfigureTemplateContext(context, Console);
        return context;
    }

    private string RenderTemplateWithDiagnostics(
        IFluidTemplate template,
        string templateName,
        object viewModel,
        string? entityKey = null,
        string? formKey = null,
        string? artifact = null)
    {
        var context = CreateTemplateContext(viewModel);
        return TslRenderDiagnostics.Render(template, context, templateName, entityKey, formKey, artifact);
    }


    #endregion

    private static class LiquidTemplates
    {
        public const string Entity = "Entity.liquid";
        public const string EntityForm = "EntityForm.liquid";
        public const string EntityFormTestHelper = "EntityFormTestHelper.liquid";
        public const string SdkMessages = "SdkMessages.liquid";
        public const string OptionSets = "OptionSets.liquid";
        public const string CustomApi = "CustomApi.liquid";
    }
}
