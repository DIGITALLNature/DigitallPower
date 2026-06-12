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

namespace dgt.power.codegeneration.Generators.Strategy;

public class TypescriptLightGenerationStrategy(IMetadataService metadataService, IAnsiConsole console) : TypescriptGenerationStrategyBase(console), ITypescriptGenerationStrategy
{
    private readonly TemplateOptions _templateOptions = TslTemplateOptionsFactory.Create();

    private const string TslResourcePrefix = "dgt.power.codegeneration.Templates.tsl";

    private static IFluidTemplate InitializeLiquidTemplate(string templateName)
    {
        return LiquidTemplateEngine.LoadTemplate(TslResourcePrefix, templateName);
    }

    #region ITypescriptGenerationStrategy (V1)

    /// <inheritdoc />
    bool ITypescriptGenerationStrategy.Generate(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        PrepareDirectory(args);
        GenerateEntities(args, config);
        GenerateEntityForms(args, config);
        GenerateOptionSets(args, config);
        GenerateSdkMessages(args, config);
        GenerateCustomApis(args, config);
        return true;
    }

    #endregion

    #region V1 Step Methods

    /// <summary>
    ///     Generates TypeScript entities based on the provided code generation arguments and configuration.
    /// </summary>
    /// <param name="args">The code generation arguments.</param>
    /// <param name="config">The code generation configuration.</param>
    private void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        // Ensure that the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var liquidTemplate = InitializeLiquidTemplate(LiquidTemplates.Entity);

        int? languageCode = 1031;
        if (config.UseBaseLanguage)
        {
            languageCode = metadataService.RetrieveOrganizationLanguage();
            Console.MarkupLine($"Using Base Language: {languageCode}");
        }

        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.TsAuxiliaryExtTypes, FileNames.Typescript.FileExtension.TypeExtension);

        // Iterate through each entity in the configuration
        foreach (var entity in config.Entities.OrderBy(entityName => entityName))
        {
            // Retrieve the metadata for the entity
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

    private void GenerateEntityForms(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var liquidTemplateForm = InitializeLiquidTemplate(LiquidTemplates.EntityForm);
        var liquidTemplateFormTestHelpers = InitializeLiquidTemplate(LiquidTemplates.EntityFormTestHelper);

        var bpfControls = GetCompleteEntityBpfControlList(config);
        var entityWithParsedFormList = GenerateEntityWithMetadata(config, bpfControls);
        var flatListParseForm = GetFlatFormDetail(entityWithParsedFormList);
        foreach (var entityParsedForm in entityWithParsedFormList)
        {
            var entityMetadata = entityParsedForm.EntityMetadata;
            var bpfControlsForEntity = bpfControls.GetValueOrDefault(entityMetadata.LogicalName) ?? [];

            foreach (var parsedForm in entityParsedForm.ParsedFormDetail)
            {
                var formName = $"{entityMetadata.LogicalName.Trim()}.{Formatter.Sanitize(parsedForm.Key.ToLowerInvariant().Trim(), true).Replace(' ', '_')}.{FileNames.Typescript.FileNamePart.Form}";
                if (ShouldSkipFormForConfig(config.Forms, formName))
                {
                    Console.MarkupLine($"Skip: {formName}");
                    continue;
                }
                FormParser.MapQuickFormId(parsedForm.Value, flatListParseForm);
                CreateFormFile(parsedForm, entityMetadata, liquidTemplateForm, LiquidTemplates.EntityForm, args, formName, bpfControlsForEntity);
                if (config.XrmMockFormHelpers)
                {
                    CreateFormTestHelperFile(parsedForm, entityMetadata, liquidTemplateFormTestHelpers, LiquidTemplates.EntityFormTestHelper, args,
                        $"{formName}.{FileNames.Typescript.FileNamePart.TestHelper}", bpfControlsForEntity);
                }
            }
        }

        if (!config.XrmMockFormHelpers)
        {
            return;
        }

        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormODataFilter, FileNames.Typescript.FileExtension.TsExtension);
        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormContextBuilder, FileNames.Typescript.FileExtension.TsExtension);
        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormContextTypes, FileNames.Typescript.FileExtension.TsExtension);
        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockTestTypingsFileName, FileNames.Typescript.FileExtension.TypeExtension);
    }

    /// <summary>
    /// Generate intermediate structure as postprocessing of the overall list is needed to link forms
    /// </summary>
    private List<EntityWithMetadataFormData> GenerateEntityWithMetadata(CodeGenerationConfig config, Dictionary<string, SortedSet<BpfControlDetail>> bpfControls)
    {
        List<EntityWithMetadataFormData> entityWithMetadataForms = new List<EntityWithMetadataFormData>();

        foreach (var entity in config.Entities.OrderBy(entityName => entityName))
        {
            var entityMetadata = metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);
            if (entityMetadata.IsBPFEntity == true)
            {
                Console.MarkupLine($"Skip form generation for BPF Entity {entityMetadata.LogicalName}");
                continue;
            }

            var bpfControlsForEntity = bpfControls.GetValueOrDefault(entity) ?? [];

            var forms = config.OnlyFormsFromSolutions
                ? metadataService.RetrieveFormsDetailsFromSolutions(entityMetadata.LogicalName, [.. config.Solutions], bpfControlsForEntity)
                : metadataService.RetrieveFormsDetails(entityMetadata.LogicalName, bpfControlsForEntity);

            entityWithMetadataForms.Add(new EntityWithMetadataFormData
            {
                EntityMetadata = entityMetadata,
                ParsedFormDetail = forms
            });
        }
        return entityWithMetadataForms;
    }

    /// <summary>
    ///     Generates SDK messages for code generation.
    /// </summary>
    private void GenerateSdkMessages(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.SuppressSdkMessages)
        {
            return;
        }

        var liquidTemplate = InitializeLiquidTemplate(LiquidTemplates.SdkMessages);
        var sdkMessages = metadataService.RetrieveSdkMessageNames(config);

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

    /// <summary>
    /// Generates Custom Apis type for code generation
    /// </summary>
    private void GenerateCustomApis(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.CustomAPIs.Count < 1)
        {
            return;
        }

        var customApis = metadataService.RetrieveCustomApis(config);
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

    /// <summary>
    ///     Generates option sets based on the provided arguments and configuration
    /// </summary>
    private void GenerateOptionSets(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.GlobalOptionSets.Count == 0)
        {
            return;
        }

        var liquidTemplate = InitializeLiquidTemplate(LiquidTemplates.OptionSets);
        var optionSets = metadataService.RetrieveOptionSets(config);

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

    private Dictionary<string, SortedSet<BpfControlDetail>> GetCompleteEntityBpfControlList(CodeGenerationConfig config)
    {
        var bpfControls = new Dictionary<string, SortedSet<BpfControlDetail>>();
        foreach (var entityName in config.Entities.OrderBy(entityName => entityName))
        {
            var bpfControlsForEntityMain = metadataService.RetrieveBusinessProcessFlowControlsForMainEntity(config, entityName);
            foreach(var bpfControlForEntityMain in bpfControlsForEntityMain)
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

    #endregion

    #region V2 Generate (TypeScriptCodeGenerationConfig)

    public bool Generate(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        PrepareDirectory(args);
        GenerateEntitiesV2(args, config);
        GenerateEntityFormsV2(args, config);
        GenerateOptionSetsV2(args, config);
        GenerateSdkMessagesV2(args, config);
        GenerateCustomApisV2(args, config);
        return true;
    }

    private void GenerateEntitiesV2(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
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

        foreach (var entity in config.Entities.OrderBy(entityName => entityName))
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

    private void GenerateEntityFormsV2(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var liquidTemplateForm = InitializeLiquidTemplate(LiquidTemplates.EntityForm);
        var liquidTemplateFormTestHelpers = InitializeLiquidTemplate(LiquidTemplates.EntityFormTestHelper);

        var bpfControls = GetCompleteEntityBpfControlListV2(config);
        var entityWithParsedFormList = GenerateEntityWithMetadataV2(config, bpfControls);
        var flatListParseForm = GetFlatFormDetail(entityWithParsedFormList);
        foreach (var entityParsedForm in entityWithParsedFormList)
        {
            var entityMetada = entityParsedForm.EntityMetadata;
            var bpfControlsForEntity = bpfControls.GetValueOrDefault(entityMetada.LogicalName) ?? [];

            foreach (var parsedForm in entityParsedForm.ParsedFormDetail)
            {
                var formName = $"{entityMetada.LogicalName.Trim()}.{Formatter.Sanitize(parsedForm.Key.ToLowerInvariant().Trim(), true).Replace(' ', '_')}.{FileNames.Typescript.FileNamePart.Form}";
                if (ShouldSkipFormForConfig(config.Forms, formName))
                {
                    Console.MarkupLine($"Skip: {formName}");
                    continue;
                }
                FormParser.MapQuickFormId(parsedForm.Value, flatListParseForm);
                CreateFormFile(
                    parsedForm,
                    entityMetada,
                    liquidTemplateForm,
                    LiquidTemplates.EntityForm,
                    args,
                    formName,
                    bpfControlsForEntity);
                if (config.XrmMockFormHelpers)
                {
                    CreateFormTestHelperFile(
                        parsedForm,
                        entityMetada,
                        liquidTemplateFormTestHelpers,
                        LiquidTemplates.EntityFormTestHelper,
                        args,
                        $"{formName}.{FileNames.Typescript.FileNamePart.TestHelper}",
                        bpfControlsForEntity);
                }
            }
        }

        if (!config.XrmMockFormHelpers)
        {
            return;
        }

        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormODataFilter, FileNames.Typescript.FileExtension.TsExtension);
        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormContextBuilder, FileNames.Typescript.FileExtension.TsExtension);
        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormContextTypes, FileNames.Typescript.FileExtension.TsExtension);
        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockTestTypingsFileName, FileNames.Typescript.FileExtension.TypeExtension);
    }

    private void GenerateSdkMessagesV2(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (!config.Include.SdkMessages)
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

    private void GenerateOptionSetsV2(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.GlobalOptionSets.Count == 0)
        {
            return;
        }

        var liquidTemplate = InitializeLiquidTemplate(LiquidTemplates.OptionSets);
        var optionSets = metadataService.RetrieveOptionSets(config.GlobalOptionSets);

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

    private void GenerateCustomApisV2(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.Requests.Count == 0)
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

    private List<EntityWithMetadataFormData> GenerateEntityWithMetadataV2(TypeScriptCodeGenerationConfig config, Dictionary<string, SortedSet<BpfControlDetail>> bpfControls)
    {
        var entityWithMetadataForms = new List<EntityWithMetadataFormData>();

        foreach (var entity in config.Entities.OrderBy(entityName => entityName))
        {
            var entityMetadata = metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);
            if (entityMetadata.IsBPFEntity == true)
            {
                Console.MarkupLine($"Skip form generation for BPF Entity {entityMetadata.LogicalName}");
                continue;
            }

            var bpfControlsForEntity = bpfControls.GetValueOrDefault(entity) ?? [];

            var forms = config.OnlyFormsFromSolutions
                ? metadataService.RetrieveFormsDetailsFromSolutions(entityMetadata.LogicalName, [.. config.Solutions], bpfControlsForEntity)
                : metadataService.RetrieveFormsDetails(entityMetadata.LogicalName, bpfControlsForEntity);

            entityWithMetadataForms.Add(new EntityWithMetadataFormData
            {
                EntityMetadata = entityMetadata,
                ParsedFormDetail = forms
            });
        }
        return entityWithMetadataForms;
    }

    private Dictionary<string, SortedSet<BpfControlDetail>> GetCompleteEntityBpfControlListV2(TypeScriptCodeGenerationConfig config)
    {
        var bpfControls = new Dictionary<string, SortedSet<BpfControlDetail>>();
        foreach (var entityName in config.Entities.OrderBy(entityName => entityName))
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


    #endregion

    #region Shared Helpers

    private void CreateFormFile(
        KeyValuePair<string, FormDetail> formDetail,
        EntityMetadata metadata,
        IFluidTemplate liquidTemplate,
        string templateName,
        CodeGenerationVerb args,
        string form,
        SortedSet<BpfControlDetail> bpfControls)
    {
        var artifact = $"{form}.{FileNames.Typescript.FileExtension.TypeExtension}";
        var content = CreateFormFileContent(formDetail, metadata, liquidTemplate, templateName, bpfControls, artifact);
        CreateFile(content, form, args, FileNames.Typescript.FileExtension.TypeExtension, GetEntityFolderName(metadata.LogicalName, Folders.TypescriptEntityForms));
    }

    private void CreateFormTestHelperFile(
        KeyValuePair<string, FormDetail> formDetail,
        EntityMetadata metadata,
        IFluidTemplate liquidTemplate,
        string templateName,
        CodeGenerationVerb args,
        string form,
        SortedSet<BpfControlDetail> bpfControls)
    {
        var artifact = $"{form}.{FileNames.Typescript.FileExtension.TsExtension}";
        var content = CreateFormFileContent(formDetail, metadata, liquidTemplate, templateName, bpfControls, artifact);
        CreateFile(content, form, args, FileNames.Typescript.FileExtension.TsExtension, GetEntityFolderName(metadata.LogicalName, Folders.TypescriptEntityTestHelper));
    }

    private string CreateFormFileContent(
        KeyValuePair<string, FormDetail> formDetail,
        EntityMetadata metadata,
        IFluidTemplate liquidTemplate,
        string templateName,
        SortedSet<BpfControlDetail> bpfControls,
        string artifact)
    {
        var formname = formDetail.Key
                    .Replace(".main", "Main", StringComparison.Ordinal)
                    .Replace(".quickview", "QuickView", StringComparison.Ordinal)
                    .Replace(".quickcreate", "QuickCreate", StringComparison.Ordinal);

        var viewModel = new FormViewModel
        {
            Name = formname,
            FormDetail = formDetail.Value,
            Attributes = FilterEntityMetadataAttributes(metadata),
            BpfControls = formDetail.Value.FormType == SystemForm.Options.Type.QuickViewForm ? [] : bpfControls.ToList()
        };
        return RenderTemplateWithDiagnostics(
            liquidTemplate,
            templateName,
            viewModel,
            entityKey: metadata.LogicalName,
            formKey: formDetail.Key,
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

