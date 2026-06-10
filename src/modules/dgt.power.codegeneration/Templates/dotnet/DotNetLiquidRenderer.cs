// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using Fluid;
using Fluid.Values;

namespace dgt.power.codegeneration.Templates.dotnet;

// Fluid callback names are part of the template DSL and must stay stable.
[SuppressMessage(
    "Naming",
    "VSTHRD200:Use Async suffix for async methods",
    Justification = "Filter callback names map to Liquid DSL tokens and are intentionally non-Async.")]
public static class DotNetLiquidRenderer
{
    private const string ResourcePrefix = "dgt.power.codegeneration.Templates.dotnet";

    private static readonly TemplateOptions s_options = InitializeOptions();

    private static readonly string[] s_protectedNames = ["ExtensionData", "Parameters", "RequestId", "RequestName"];

    private static TemplateOptions InitializeOptions()
    {
        var options = new TemplateOptions();
        LiquidTemplateEngine.RegisterCoreFilters(options);
        options.Filters.AddFilter("maskoverrides", MaskOverrides);
        options.MemberAccessStrategy.Register<SdkMessageViewModel>();
        options.MemberAccessStrategy.Register<OptionViewModel>();
        options.MemberAccessStrategy.Register<Option>();
        options.MemberAccessStrategy.Register<WfAction>();
        options.MemberAccessStrategy.Register<WfParameter>();
        options.MemberAccessStrategy.Register<DotNetAttributeModel>();
        options.MemberAccessStrategy.Register<DotNetNavigationPropertyModel>();
        options.MemberAccessStrategy.Register<DotNetOptionFieldModel>();
        options.MemberAccessStrategy.Register<DotNetOptionModel>();
        options.ValueConverters.Add(o => o is KeyValuePair<string, List<Option>> k ? new OptionViewModel(k) : null);
        return options;
    }

#pragma warning disable IDE0060
    public static ValueTask<FluidValue> MaskOverrides(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning restore IDE0060
    {
        ArgumentNullException.ThrowIfNull(input);
        var value = input.ToStringValue();
        return new StringValue(s_protectedNames.Contains(value) ? $"{value}Parameter" : value);
    }

    public static string Render(string templateName, TemplateContext context)
    {
        return LiquidTemplateEngine.Render(ResourcePrefix, templateName, context);
    }

    public static TemplateContext CreateContext()
    {
        return LiquidTemplateEngine.CreateContext(s_options);
    }
}
