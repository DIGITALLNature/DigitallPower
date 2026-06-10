// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Fluid;

namespace dgt.power.codegeneration.Templates.ts;

public static class TsLiquidRenderer
{
    private const string ResourcePrefix = "dgt.power.codegeneration.Templates.ts";

    private static readonly TemplateOptions s_options = InitializeOptions();

    private static TemplateOptions InitializeOptions()
    {
        var options = new TemplateOptions();
        LiquidTemplateEngine.RegisterCoreFilters(options);
        options.MemberAccessStrategy.Register<TsSdkMessagesTemplateModel>();
        options.MemberAccessStrategy.Register<TsSdkMessageModel>();
        options.MemberAccessStrategy.Register<TsOptionSetsTemplateModel>();
        options.MemberAccessStrategy.Register<TsOptionSetModel>();
        options.MemberAccessStrategy.Register<TsOptionValueModel>();
        options.MemberAccessStrategy.Register<TsBusinessProcessFlowTemplateModel>();
        options.MemberAccessStrategy.Register<TsBusinessProcessFlowStageModel>();
        options.MemberAccessStrategy.Register<TsNamedIdModel>();
        options.MemberAccessStrategy.Register<TsEntityRefTemplateModel>();
        options.MemberAccessStrategy.Register<TsEntityTemplateModel>();
        options.MemberAccessStrategy.Register<TsEntityFormTemplateModel>();
        options.MemberAccessStrategy.Register<TsAttributeConstantModel>();
        options.MemberAccessStrategy.Register<TsAttributeFieldModel>();
        options.MemberAccessStrategy.Register<TsNamedValueModel>();
        options.MemberAccessStrategy.Register<TsFormTabModel>();
        options.MemberAccessStrategy.Register<TsFormTabSectionClassModel>();
        return options;
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
