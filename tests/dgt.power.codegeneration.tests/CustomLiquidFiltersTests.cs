// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Templates;
using Fluid;

namespace dgt.power.codegeneration.tests;

public class CustomLiquidFiltersTests
{
    [Test]
    public async Task UniqueFilter_ShouldStayWithinScopeInsideSingleRenderContext()
    {
        var template = ParseTemplate("{{ \"Token\" | unique: \"scope_a\" }}|{{ \"Token\" | unique: \"scope_a\" }}|{{ \"Token\" | unique: \"scope_b\" }}");
        var context = new TemplateContext(new TemplateOptions());
        LiquidTemplateEngine.RegisterCoreFilters(context.Options);

        var output = template.Render(context);

        await Assert.That(output).IsEqualTo("Token|Token_|Token");
    }

    [Test]
    public async Task UniqueFilter_ShouldNotLeakBetweenTemplateContexts()
    {
        var template = ParseTemplate("{{ \"Token\" | unique: \"scope_a\" }}|{{ \"Token\" | unique: \"scope_a\" }}");
        var options = new TemplateOptions();
        LiquidTemplateEngine.RegisterCoreFilters(options);

        var firstOutput = template.Render(new TemplateContext(options));
        var secondOutput = template.Render(new TemplateContext(options));

        await Assert.That(firstOutput).IsEqualTo("Token|Token_");
        await Assert.That(secondOutput).IsEqualTo("Token|Token_");
    }

    private static IFluidTemplate ParseTemplate(string templateSource)
    {
        var parser = new FluidParser(new FluidParserOptions { AllowParentheses = true });
        if (!parser.TryParse(templateSource, out var template, out var error))
        {
            throw new InvalidOperationException($"Failed to parse test template: {error}");
        }

        return template ?? throw new InvalidOperationException("Template parser returned no template for test case.");
    }
}
