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

namespace dgt.power.codegeneration.Generators.Worker;

public class TypescriptGeneratorWorkerLight(IMetadataService metadataService, IAnsiConsole console) : TypescriptGeneratorWorker(console), ITypescriptGenerator
{
    private readonly TemplateOptions _templateOptions = TslTemplateOptionsFactory.Create();

    private const string TslResourcePrefix = "dgt.power.codegeneration.Templates.tsl";

    private static IFluidTemplate InitializeLiquidTemplate(string templateName)
    {
        return LiquidTemplateEngine.LoadTemplate(TslResourcePrefix, templateName);
    }

    #region ITypescriptGenerator Members

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

        var liquidTemplate = InitializeLiquidTemplate("Entity.liquid");

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
                "Entity.liquid",
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

    public void GenerateEntityForms(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var liquidTemplateForm = InitializeLiquidTemplate("EntityForm.liquid");
        var liquidTemplateFormTestHelpers = InitializeLiquidTemplate("EntityFormTestHelper.liquid");

        var bpfControls = GetCompleteEntityBpfControlList(config);
        var  entityWithParsedFormList = GenerateEntityWithMetadata(config, bpfControls);
        var flatListParseForm = GetFlatFormDetail(entityWithParsedFormList);
        foreach (var entityParsedForm in entityWithParsedFormList)
        {
            var entityMetada = entityParsedForm.EntityMetadata;
            var bpfControlsForEntity = bpfControls.GetValueOrDefault(entityMetada.LogicalName) ?? [];

            foreach (var parsedForm in entityParsedForm.ParsedFormDetail)
            {
                var formName =  $"{entityMetada.LogicalName.Trim()}.{Formatter.Sanitize(parsedForm.Key.ToLowerInvariant().Trim(), true).Replace(' ', '_')}.{FileNames.Typescript.FileNamePart.Form}";
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
                    "EntityForm.liquid",
                    args,
                    formName,
                    bpfControlsForEntity);
                if (config.XrmMockFormHelpers)
                {
                    CreateFormTestHelperFile(
                        parsedForm,
                        entityMetada,
                        liquidTemplateFormTestHelpers,
                        "EntityFormTestHelper.liquid",
                        args,
                        $"{formName}.{FileNames.Typescript.FileNamePart.TestHelper}",
                        bpfControlsForEntity);
                }
            }
        }

        if (config.XrmMockFormHelpers)
        {
            CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormODataFilter, FileNames.Typescript.FileExtension.TsExtension);
            CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormContextBuilder, FileNames.Typescript.FileExtension.TsExtension);
            CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockFormContextTypes, FileNames.Typescript.FileExtension.TsExtension);
            CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmMockTestTypingsFileName, FileNames.Typescript.FileExtension.TypeExtension);
        }
    }

    /// <summary>
    /// Generate intermediate structure as postprocessing of the overall list is needed to link forms
    /// </summary>
    /// <param name="config"></param>
    /// <param name="bpfControls"></param>
    /// <returns></returns>
    private List<EntityWithMetadataFormData> GenerateEntityWithMetadata(CodeGenerationConfig config, Dictionary<string, SortedSet<BpfControlDetail>> bpfControls)
    {
        List<EntityWithMetadataFormData> entityWithMetadataForms = new List<EntityWithMetadataFormData>();

        foreach (var entity in config.Entities.OrderBy(entityName => entityName))
        {
            var entityMetadata = metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);
            // do not create forms for bpf entities
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
    /// Get flat form detail
    /// </summary>
    /// <param name="entityWithMetadataFormDatas"></param>
    /// <returns></returns>
    private static List<FormDetail> GetFlatFormDetail(List<EntityWithMetadataFormData> entityWithMetadataFormDatas)
    {
        return entityWithMetadataFormDatas.Select(x => x.ParsedFormDetail.Values.ToList()).SelectMany(x => x).ToList();
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

        var liquidTemplate = InitializeLiquidTemplate("SdkMessages.liquid");

        // Retrieve SDK message names from the metadata service
        var sdkMessages = metadataService.RetrieveSdkMessageNames(config);

        var viewModel = new SdkMessagesViewModel
        {
            SdkMessages = sdkMessages.Select(sdkm => new SdkMessageViewModel(sdkm)).ToList()
        };

        var content = RenderTemplateWithDiagnostics(
            liquidTemplate,
            "SdkMessages.liquid",
            viewModel,
            artifact: $"{FileNames.Typescript.FileNames.SdkMessageNames}.{FileNames.Typescript.FileExtension.TypeExtension}");

        CreateFile(content, FileNames.Typescript.FileNames.SdkMessageNames, args, FileNames.Typescript.FileExtension.TypeExtension);
    }

    /// <summary>
    /// Generates Custom Apis type for code generation
    /// </summary>
    /// <param name="args"></param>
    /// <param name="config"></param>
    public void GenerateCustomApis(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        // Check if the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        // Check if SDK messages should be suppressed
        if (config.CustomAPIs.Count < 1)
        {
            return;
        }

        var customApis = metadataService.RetrieveCustomApis(config);
        CopyTemplateFileContent(args, FileNames.Typescript.FileNames.XrmWebApiTypingsFileName, FileNames.Typescript.FileExtension.TypeExtension);
        var liquidTemplate = InitializeLiquidTemplate("CustomApi.liquid");

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
                "CustomApi.liquid",
                viewModel,
                entityKey: customApi.LogicalName,
                artifact: $"{customApiName}.{FileNames.Typescript.FileExtension.TypeExtension}");

            CreateFile(content, customApiName, args, FileNames.Typescript.FileExtension.TypeExtension, [Folders.TypescriptCustomApis]);
        }
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

        var liquidTemplate = InitializeLiquidTemplate("OptionSets.liquid");

        // Retrieve option sets from the metadata service
        var optionSets = metadataService.RetrieveOptionSets(config);

        var viewModel = new OptionSetViewModel
        {
            OptionSets = optionSets.ToList()
        };

        var content = RenderTemplateWithDiagnostics(
            liquidTemplate,
            "OptionSets.liquid",
            viewModel,
            artifact: $"{FileNames.Typescript.FileNames.OptionSetValues}.{FileNames.Typescript.FileExtension.TypeExtension}");

        CreateFile(content, FileNames.Typescript.FileNames.OptionSetValues, args, FileNames.Typescript.FileExtension.TypeExtension);
    }

    /// <summary>
    /// Wrapper function that maps the model and creates the form file
    /// </summary>
    /// <param name="formDetail"></param>
    /// <param name="metadata"></param>
    /// <param name="liquidTemplate"></param>
    /// <param name="templateName"></param>
    /// <param name="args"></param>
    /// <param name="form"></param>
    /// <param name="bpfControls"></param>
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

    /// <summary>
    /// Wrapper function that maps the model and creates the form file
    /// </summary>
    /// <param name="formDetail"></param>
    /// <param name="metadata"></param>
    /// <param name="liquidTemplate"></param>
    /// <param name="templateName"></param>
    /// <param name="args"></param>
    /// <param name="form"></param>
    /// <param name="bpfControls"></param>
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


    /// <summary>
    /// Create form content for input form and the input template
    /// </summary>
    /// <param name="formDetail"></param>
    /// <param name="metadata"></param>
    /// <param name="liquidTemplate"></param>
    /// <param name="templateName"></param>
    /// <param name="bpfControls"></param>
    /// <param name="artifact"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Checks given the configu and the form name if the form should be skipped for some reason or not
    /// </summary>
    /// <param name="configForms"></param>
    /// <param name="form"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Filter metadata attributes before mapping them to entity or form models
    /// </summary>
    /// <param name="metadata"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Given the entity logical name calculates a folder name for the given ts files
    /// </summary>
    /// <param name="entityLogicalName"></param>
    /// <param name="subFolder"></param>
    /// <returns></returns>
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

    #region Not Supported

    public void GenerateBoilerPlateFull(CodeGenerationVerb args, CodeGenerationConfig config) =>
        throw new NotSupportedException("Full boilerplate generation is only available in full TypeScript generator mode.");

    public void GenerateEntityRefsFull(CodeGenerationVerb args, CodeGenerationConfig config) =>
        throw new NotSupportedException("Entity reference generation is only available in full TypeScript generator mode.");

    public void GenerateBusinessProcessFlowsFull(CodeGenerationVerb args, CodeGenerationConfig config) =>
        throw new NotSupportedException("Business process flow generation is only available in full TypeScript generator mode.");

    #endregion
}
