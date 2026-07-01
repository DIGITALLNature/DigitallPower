// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Templates;
using Fluid;

namespace dgt.power.codegeneration.tests;

public class TslRenderDiagnosticsTests
{
    [Test]
    public async Task ShouldIncludeTemplateEntityAndArtifactContextWhenRenderFails()
    {
        var options = new TemplateOptions
        {
            Undefined = name => throw new InvalidOperationException($"Undefined liquid value: {name}")
        };
        var template = ParseTemplate("{{ missing.path }}");
        var context = new TemplateContext(new { }, options);

        try
        {
            _ = TslRenderDiagnostics.Render(
                template,
                context,
                "Entity.liquid",
                entityKey: "account",
                artifact: "account.entity.d.ts");
            throw new InvalidOperationException("Expected render to fail.");
        }
        catch (InvalidOperationException ex)
        {
            await Assert.That(ex.Message).Contains("template='Entity.liquid'");
            await Assert.That(ex.Message).Contains("entity='account'");
            await Assert.That(ex.Message).Contains("form='<n/a>'");
            await Assert.That(ex.Message).Contains("artifact='account.entity.d.ts'");
            await Assert.That(ex.Message).Contains("filter='<n/a>'");
            await Assert.That(ex.InnerException is not null).IsTrue();
        }
    }

    [Test]
    public async Task ShouldIncludeFormAndFilterContextWhenFilterFails()
    {
        var options = new TemplateOptions();
        options.Filters.AddFilter("localize", CustomLiquidFilters.Localize);
        var template = ParseTemplate("{{ 'invalid' | localize: 1031 }}");
        var context = new TemplateContext(options);

        try
        {
            _ = TslRenderDiagnostics.Render(
                template,
                context,
                "EntityForm.liquid",
                entityKey: "account",
                formKey: "account.main",
                artifact: "account.main.form.d.ts");
            throw new InvalidOperationException("Expected render to fail.");
        }
        catch (InvalidOperationException ex)
        {
            await Assert.That(ex.Message).Contains("template='EntityForm.liquid'");
            await Assert.That(ex.Message).Contains("entity='account'");
            await Assert.That(ex.Message).Contains("form='account.main'");
            await Assert.That(ex.Message).Contains("artifact='account.main.form.d.ts'");
            await Assert.That(ex.Message).Contains("filter='localize'");
            await Assert.That(ex.InnerException is not null).IsTrue();
        }
    }

    private static IFluidTemplate ParseTemplate(string source)
    {
        var parser = new FluidParser(new FluidParserOptions { AllowParentheses = true });
        if (!parser.TryParse(source, out var template, out var error))
        {
            throw new InvalidOperationException($"Failed to parse test template: {error}");
        }

        return template ?? throw new InvalidOperationException("Template parser returned no template for test case.");
    }
}
