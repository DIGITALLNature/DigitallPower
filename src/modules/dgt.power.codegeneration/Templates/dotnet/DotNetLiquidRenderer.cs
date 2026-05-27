// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Reflection;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using Fluid;

namespace dgt.power.codegeneration.Templates.dotnet;

public static class DotNetLiquidRenderer
{
    private static readonly FluidParser Parser = new(new FluidParserOptions { AllowParentheses = true });

    private static readonly TemplateOptions Options;

    static DotNetLiquidRenderer()
    {
        Options = new TemplateOptions();
        Options.Filters.AddFilter("camelcase", CustomLiquidFilters.CamelCase);
        Options.Filters.AddFilter("sanitize", CustomLiquidFilters.Sanitize);
        Options.Filters.AddFilter("unique", CustomLiquidFilters.Unique);
        Options.MemberAccessStrategy.Register<SdkMessageViewModel>();
        Options.MemberAccessStrategy.Register<OptionViewModel>();
        Options.MemberAccessStrategy.Register<Option>();
        Options.ValueConverters.Add(o => o is KeyValuePair<string, List<Option>> k ? new OptionViewModel(k) : null);
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
