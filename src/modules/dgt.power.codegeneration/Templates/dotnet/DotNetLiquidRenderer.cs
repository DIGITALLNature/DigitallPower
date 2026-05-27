// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Reflection;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using Fluid;
using Fluid.Values;

namespace dgt.power.codegeneration.Templates.dotnet;

public static class DotNetLiquidRenderer
{
    private static readonly FluidParser Parser = new(new FluidParserOptions { AllowParentheses = true });

    private static readonly TemplateOptions Options;

    private static readonly string[] ProtectedNames = ["ExtensionData", "Parameters", "RequestId", "RequestName"];

    static DotNetLiquidRenderer()
    {
        Options = new TemplateOptions();
        Options.Filters.AddFilter("camelcase", CustomLiquidFilters.CamelCase);
        Options.Filters.AddFilter("sanitize", CustomLiquidFilters.Sanitize);
        Options.Filters.AddFilter("unique", CustomLiquidFilters.Unique);
        Options.Filters.AddFilter("maskoverrides", MaskOverrides);
        Options.MemberAccessStrategy.Register<SdkMessageViewModel>();
        Options.MemberAccessStrategy.Register<OptionViewModel>();
        Options.MemberAccessStrategy.Register<Option>();
        Options.MemberAccessStrategy.Register<WfAction>();
        Options.MemberAccessStrategy.Register<WfParameter>();
        Options.MemberAccessStrategy.Register<DotNetAttributeModel>();
        Options.MemberAccessStrategy.Register<DotNetNavigationPropertyModel>();
        Options.MemberAccessStrategy.Register<DotNetOptionFieldModel>();
        Options.MemberAccessStrategy.Register<DotNetOptionModel>();
        Options.ValueConverters.Add(o => o is KeyValuePair<string, List<Option>> k ? new OptionViewModel(k) : null);
    }

#pragma warning disable IDE0060
    public static ValueTask<FluidValue> MaskOverrides(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning restore IDE0060
    {
        var value = input.ToStringValue();
        return new StringValue(ProtectedNames.Contains(value) ? $"{value}Parameter" : value);
    }

    public static string Render(string templateName, TemplateContext context)
    {
        var liquidTemplate = LoadTemplate(templateName);
        return liquidTemplate.Render(context);
    }

    public static TemplateContext CreateContext()
    {
        return new TemplateContext(Options);
    }

    private static IFluidTemplate LoadTemplate(string templateName)
    {
        using var reader = new StreamReader(Assembly.GetExecutingAssembly()
            .GetManifestResourceStream($"dgt.power.codegeneration.Templates.dotnet.{templateName}")!);
        return Parser.Parse(reader.ReadToEnd());
    }
}
