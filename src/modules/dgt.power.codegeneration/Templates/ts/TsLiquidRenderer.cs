// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Fluid;

namespace dgt.power.codegeneration.Templates.ts;

public static class TsLiquidRenderer
{
    private const string ResourcePrefix = "dgt.power.codegeneration.Templates.ts";

    private static readonly TemplateOptions Options;

    static TsLiquidRenderer()
    {
        Options = new TemplateOptions();
        LiquidTemplateEngine.RegisterCoreFilters(Options);
        Options.MemberAccessStrategy.Register<TsSdkMessagesTemplateModel>();
        Options.MemberAccessStrategy.Register<TsSdkMessageModel>();
        Options.MemberAccessStrategy.Register<TsOptionSetsTemplateModel>();
        Options.MemberAccessStrategy.Register<TsOptionSetModel>();
        Options.MemberAccessStrategy.Register<TsOptionValueModel>();
        Options.MemberAccessStrategy.Register<TsBusinessProcessFlowTemplateModel>();
        Options.MemberAccessStrategy.Register<TsBusinessProcessFlowStageModel>();
        Options.MemberAccessStrategy.Register<TsNamedIdModel>();
        Options.MemberAccessStrategy.Register<TsEntityRefTemplateModel>();
        Options.MemberAccessStrategy.Register<TsEntityTemplateModel>();
        Options.MemberAccessStrategy.Register<TsEntityFormTemplateModel>();
        Options.MemberAccessStrategy.Register<TsAttributeConstantModel>();
        Options.MemberAccessStrategy.Register<TsAttributeFieldModel>();
        Options.MemberAccessStrategy.Register<TsNamedValueModel>();
        Options.MemberAccessStrategy.Register<TsFormTabModel>();
        Options.MemberAccessStrategy.Register<TsFormTabSectionClassModel>();
    }

    public static string Render(string templateName, TemplateContext context)
    {
        return LiquidTemplateEngine.Render(ResourcePrefix, templateName, context);
    }

    public static TemplateContext CreateContext()
    {
        return LiquidTemplateEngine.CreateContext(Options);
    }
}
