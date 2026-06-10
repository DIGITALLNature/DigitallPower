// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using dgt.power.common;
using Fluid;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates;

internal static class TslTemplateOptionsFactory
{
    internal const int DefaultTemplateMaxSteps = 20_000;
    internal const string StrictModeEnvironmentVariable = "DGT_POWER_TSL_STRICT_MODE";
    internal const string MaxStepsEnvironmentVariable = "DGT_POWER_TSL_MAX_STEPS";

    public static TemplateOptions Create() =>
        Create(
            Environment.GetEnvironmentVariable(StrictModeEnvironmentVariable),
            ExecutionEnvironment.IsCiAgent,
            Environment.GetEnvironmentVariable(MaxStepsEnvironmentVariable));

    internal static TemplateOptions Create(string? strictModeValue, bool isCiAgent, string? maxStepsValue)
    {
        var options = new TemplateOptions
        {
            MaxSteps = ResolveMaxSteps(maxStepsValue)
        };
        ConfigureStrictValidationMode(options, strictModeValue, isCiAgent);
        RegisterFilters(options);
        RegisterValueConverters(options);
        RegisterMemberAccess(options);
        return options;
    }

    private static int ResolveMaxSteps(string? maxStepsValue)
    {
        if (string.IsNullOrWhiteSpace(maxStepsValue))
        {
            return DefaultTemplateMaxSteps;
        }

        if (!int.TryParse(maxStepsValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var maxSteps) ||
            maxSteps <= 0)
        {
            throw new InvalidOperationException(
                $"Invalid {MaxStepsEnvironmentVariable} value '{maxStepsValue}'. Expected a positive integer.");
        }

        return maxSteps;
    }

    private static void ConfigureStrictValidationMode(TemplateOptions options, string? strictModeValue, bool isCiAgent)
    {
        ArgumentNullException.ThrowIfNull(options);

        var strictModeEnabled = !string.IsNullOrWhiteSpace(strictModeValue)
            ? ExecutionEnvironment.IsTruthy(strictModeValue)
            : isCiAgent;

        if (!strictModeEnabled)
        {
            return;
        }

        options.Undefined = name =>
            throw new InvalidOperationException(
                $"Undefined liquid value encountered in strict validation mode: '{name}'.");
    }

    private static void RegisterFilters(TemplateOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        LiquidTemplateEngine.RegisterCoreFilters(options);
        options.Filters.AddFilter("controltype", CustomLiquidFilters.Controltype);
        options.Filters.AddFilter("attributetype", CustomLiquidFilters.Attributetype);
        options.Filters.AddFilter("localize", CustomLiquidFilters.Localize);
        options.Filters.AddFilter("formControlType", CustomLiquidFilters.GetControlTypeFromFormControl);
        options.Filters.AddFilter("xrmMockAttributeType", CustomLiquidFilters.XrmMockAttributetype);
        options.Filters.AddFilter("xrmMockControlType", CustomLiquidFilters.GetControlXrmMockFormFromAttribute);
        options.Filters.AddFilter("requiredAttributeLevel", CustomLiquidFilters.GetRequiredAttributeLevel);
        options.Filters.AddFilter("getAttributeItem", CustomLiquidFilters.GetAttributeItem);
    }

    private static void RegisterValueConverters(TemplateOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        options.ValueConverters.Add(o => o is AttributeMetadata p ? new AttributeMetadataViewModel(p) : null);
        options.ValueConverters.Add(o => o is OptionMetadata l ? new OptionMetadataViewModel(l) : null);
        options.ValueConverters.Add(o => o is BpfControlDetail r ? new BpfControlViewModel(r) : null);
        options.ValueConverters.Add(o => o is FormXmlControlData r ? new FormControlViewModel(r) : null);
        options.ValueConverters.Add(o => o is WfParameter wp ? new CustomApiParameterViewModel(wp) : null);
        options.ValueConverters.Add(o => o is KeyValuePair<string, List<Option>> k ? new OptionViewModel(k) : null);
        options.ValueConverters.Add(o => o is KeyValuePair<string, SectionDetail> td ? new SectionDetailsViewModel(td) : null);
    }

    private static void RegisterMemberAccess(TemplateOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        options.MemberAccessStrategy.Register<AttributeMetadataViewModel>();
        options.MemberAccessStrategy.Register<OptionMetadataViewModel>();
        options.MemberAccessStrategy.Register<OptionViewModel>();
        options.MemberAccessStrategy.Register<Option>();
        options.MemberAccessStrategy.Register<SdkMessageViewModel>();
        options.MemberAccessStrategy.Register<FormDetail>();
        options.MemberAccessStrategy.Register<QuickViewFormControl>();
        options.MemberAccessStrategy.Register<FormAttributeData>();
        options.MemberAccessStrategy.Register<FormControlViewModel>();
        options.MemberAccessStrategy.Register<BpfControlViewModel>();
        options.MemberAccessStrategy.Register<CustomApiParameterViewModel>();
        options.MemberAccessStrategy.Register<TabDetail>();
        options.MemberAccessStrategy.Register<SectionDetailsViewModel>();
        options.MemberAccessStrategy.Register<SectionDetail>();
    }
}
