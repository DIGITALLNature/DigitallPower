// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Reflection;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using Fluid;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.codegeneration.Generators.Worker;

public class TypescriptGeneratorWorkerLight : TypescriptGeneratorWorker, ITypescriptGenerator
{
    private readonly IMetadataService _metadataService;
    private readonly TemplateOptions _templateOptions;

    public TypescriptGeneratorWorkerLight(IMetadataService metadataService)
    {
        _metadataService = metadataService;
        _templateOptions = new TemplateOptions();
        _templateOptions.Filters.AddFilter("camelcase", CustomLiquidFilters.CamelCase);
        _templateOptions.Filters.AddFilter("sanitize", CustomLiquidFilters.Sanitize);
        _templateOptions.Filters.AddFilter("unique", CustomLiquidFilters.Unique);
        _templateOptions.Filters.AddFilter("controltype",CustomLiquidFilters.Controltype);
        _templateOptions.Filters.AddFilter("attributetype", CustomLiquidFilters.Attributetype);
        _templateOptions.Filters.AddFilter("localize", CustomLiquidFilters.Localize);
        _templateOptions.ValueConverters.Add(o => o is AttributeMetadata p ? new AttributeMetadataViewModel(p) : null);
        _templateOptions.ValueConverters.Add(o => o is OptionMetadata l ? new OptionMetadataViewModel(l) : null);
        _templateOptions.ValueConverters.Add(o => o is KeyValuePair<string, List<Option>> k ? new OptionViewModel(k) : null);
        _templateOptions.ValueConverters.Add(o => o is KeyValuePair<string, TabDetail> td ? new TabDetailsViewModel(td) : null);
        _templateOptions.ValueConverters.Add(o => o is KeyValuePair<string, SectionDetail> td ? new SectionDetaisViewModel(td) : null);
        _templateOptions.MemberAccessStrategy.Register<AttributeMetadataViewModel>();
        _templateOptions.MemberAccessStrategy.Register<OptionMetadataViewModel>();
        _templateOptions.MemberAccessStrategy.Register<OptionViewModel>();
        _templateOptions.MemberAccessStrategy.Register<Option>();
        _templateOptions.MemberAccessStrategy.Register<SdkMessageViewModel>();
        _templateOptions.MemberAccessStrategy.Register<FormDetail>();
        _templateOptions.MemberAccessStrategy.Register<TabDetail>();
        _templateOptions.MemberAccessStrategy.Register<TabDetailsViewModel>();
        _templateOptions.MemberAccessStrategy.Register<SectionDetail>();
        _templateOptions.MemberAccessStrategy.Register<SectionDetaisViewModel>();
    }

    private static IFluidTemplate InitializeLiquidTemplate(string templateName)
    {
        var reader = new StreamReader(Assembly.GetCallingAssembly()
            .GetManifestResourceStream($"dgt.power.codegeneration.Templates.tsl.{templateName}")!);
        var parser = new FluidParser();
        var liquidTemplate = parser.Parse(reader.ReadToEnd());
        return liquidTemplate;
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

        int? languageCode = null;
        if (config.UseBaseLanguage)
        {
            languageCode = _metadataService.RetrieveOrganizationLanguage();
            AnsiConsole.MarkupLine($"Using Base Language: {languageCode}");
        }

        // Iterate through each entity in the configuration
        foreach (var entity in config.Entities)
        {
            // Retrieve the metadata for the entity
            var metadata = _metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);

            var viewModel = new EntityViewModel
            {
                SchemaName = metadata.SchemaName,
                LogicalName = metadata.LogicalName,
                LanguageCode = languageCode,
                Attributes = metadata.Attributes
                    .Where(a =>
                        (a.IsValidForGrid == true || a.IsValidForForm == true || a.IsValidODataAttribute ||
                         a.IsPrimaryId == true) && (a.IsValidForCreate == true || a.IsValidForUpdate == true ||
                                                    a.IsValidForRead == true))
                    .Where(a => !a.LogicalName.Contains("entityimage"))
                    .Where(a => a.AttributeType != AttributeTypeCode.ManagedProperty)
                    .OrderBy(a => a.LogicalName).ToList()
            };
            var context = new TemplateContext(viewModel, _templateOptions);
            var content = liquidTemplate.Render(context);

            CreateFile(content, $"{metadata.LogicalName.ToLowerInvariant()}.{FileNames.Typescript.Entity}", args);
        }
    }

    public void GenerateEntityForms(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var liquidTemplate = InitializeLiquidTemplate("EntityForm.liquid");

        foreach (var entity in config.Entities)
        {
            var metadata = _metadataService.RetrieveEntityMetadata(entity, EntityFilters.Attributes | EntityFilters.Entity);

            var forms = config.OnlyFormsFromSolutions
                ? _metadataService.RetrieveFormsDetailsFromSolutions(metadata.LogicalName, config.Solutions)
                : _metadataService.RetrieveFormsDetails(metadata.LogicalName);

            foreach (var formDetail in forms)
            {
                var form =  $"{metadata.LogicalName}.{Formatter.Sanitize(formDetail.Key.ToLowerInvariant(), true).Replace(' ', '_')}.{FileNames.Typescript.Form}";
                if (config.Forms.Length != 0)
                {
                    config.Forms.Where(e => e.EndsWith(".ts", StringComparison.InvariantCulture))
                        .ToList()
                        .ForEach(e => AnsiConsole.MarkupLine(Warnings.TsExtensionDeprecation));
                    config.Forms = config.Forms.Select(e => e.EndsWith(".ts", StringComparison.InvariantCulture)
                                ? e.Remove(e.LastIndexOf(".ts", StringComparison.Ordinal))
                                : e)
                        .ToArray();
                }

                if (config.Forms.Length != 0 && !config.Forms.Contains(form))
                {
                    AnsiConsole.MarkupLine($"Skip: {form}");
                    continue;
                }

                var formname = formDetail.Key
                    .Replace(".main", "Main").Replace(".quickview", "QuickView")
                    .Replace(".quickcreate", "QuickCreate");

                var viewModel = new FormViewModel
                {
                    Name = formname,
                    FormDetail = formDetail.Value,
                    SchemaName = metadata.SchemaName,
                    Attributes = metadata.Attributes
                        .Where(a =>
                            (a.IsValidForGrid == true || a.IsValidForForm == true || a.IsValidODataAttribute ||
                             a.IsPrimaryId == true) && (a.IsValidForCreate == true || a.IsValidForUpdate == true ||
                                                        a.IsValidForRead == true))
                        .Where(a => !a.LogicalName.Contains("entityimage"))
                        .Where(a => a.AttributeType != AttributeTypeCode.ManagedProperty)
                        .OrderBy(a => a.LogicalName).ToList()
                };

                var context = new TemplateContext(viewModel, _templateOptions);
                var content = liquidTemplate.Render(context);

                CreateFile(content, form, args);

                // var template = new EntityLightFormTemplate(config.TypingPath, form, formname, formDetail.Value,
                //     metadata, config, _metadataService.RetrieveOrganizationLanguage());
                // CreateTemplateFile(template,
                //     $"{metadata.LogicalName}.{FileNames.Typescript.Form}.{Formatter.Sanitize(formDetail.Key.ToLowerInvariant(), true).Replace(' ', '_')}.d",
                //     args);
            }
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

        var liquidTemplate = InitializeLiquidTemplate("SdkMessages.liquid");

        // Retrieve SDK message names from the metadata service
        var sdkMessages = _metadataService.RetrieveSdkMessageNames(config);

        var viewModel = new SdkMessagesViewModel
        {
            SdkMessages = sdkMessages.Select(sdkm => new SdkMessageViewModel(sdkm)).ToList()
        };

        var context = new TemplateContext(viewModel, _templateOptions);
        var content = liquidTemplate.Render(context);

        CreateFile(content, FileNames.Typescript.SdkMessageNames, args);
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
        var optionSets = _metadataService.RetrieveOptionSets(config);

        var viewModel = new OptionSetViewModel
        {
            OptionSets = optionSets.ToList()
        };

        var context = new TemplateContext(viewModel, _templateOptions);
        var content = liquidTemplate.Render(context);

        CreateFile(content, FileNames.Typescript.OptionSetValues, args);
    }

    #endregion

    #region Not Implemented

    public void GenerateBoilerPlateFull(CodeGenerationVerb args, CodeGenerationConfig config) =>
        throw new NotImplementedException();

    public void GenerateEntityRefsFull(CodeGenerationVerb args, CodeGenerationConfig config) =>
        throw new NotImplementedException();

    public void GenerateBusinessProcessFlowsFull(CodeGenerationVerb args, CodeGenerationConfig config) =>
        throw new NotImplementedException();

    #endregion
}
