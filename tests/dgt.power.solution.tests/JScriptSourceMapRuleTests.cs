// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text;
using dgt.power.dataverse;
using dgt.power.solution.Rules;
using dgt.power.solution.tests.Base;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;

namespace dgt.power.solution.tests;

public class JScriptSourceMapRuleTests : LintTestsBase<SolutionLintCommand>
{
    private const string SolutionName = "sample_solution";

    protected override CommandTestContext<SolutionLintCommand, SolutionLintSettings> GetContext() => CreateContext();

    [Test]
    public async Task EvaluateAsync_FlagsOnlyJScriptResourcesContainingASourceMap()
    {
        var findings = await EvaluateAsync();

        var flaggedNames = findings.Select(finding => finding.ComponentLogicalName!).ToHashSet(StringComparer.Ordinal);
        await Assert.That(flaggedNames).IsEquivalentTo(["unminified.js"]); // false-positive.js and string-literal.js only mention/embed the token in a string literal, not a real trailing directive

        var finding = findings.Single();
        await Assert.That(finding.RuleId).IsEqualTo("webresource.jscript-sourcemap");
        await Assert.That(finding.ComponentType).IsEqualTo("WebResource");
        await Assert.That(finding.SolutionUniqueName).IsEqualTo(SolutionName);
    }

    [Test]
    public async Task EvaluateAsync_DefaultSeverity_IsWarning()
    {
        var findings = await EvaluateAsync();

        await Assert.That(findings.Single().Severity).IsEqualTo(LintSeverity.Warning);
    }

    private async Task<IReadOnlyList<LintFinding>> EvaluateAsync()
    {
        var testContext = CreateContext();
        var context = await LintContext.CreateAsync(testContext.FakedService, [SolutionName], CancellationToken.None);
        return await new JScriptSourceMapRule().EvaluateAsync(context, ruleConfig: null, CancellationToken.None);
    }

    private CommandTestContext<SolutionLintCommand, SolutionLintSettings> CreateContext()
    {
        return GetBuilder()
            .WithFakeMessageExecutor(new RetrieveAllEntitiesExecutor())
            .WithFakeMessageExecutor(new RetrieveEntityExecutor())
            .WithData(PrepareData)
            .Build();
    }

    private static IEnumerable<Entity> PrepareData(FakeOrganizationServiceAsync service)
    {
        var solution = new Solution(Guid.NewGuid()) { UniqueName = SolutionName };

        var minified = CreateWebResource("minified.js", "var a=1;", WebResource.Options.WebResourceType.ScriptJScript);
        var unminified = CreateWebResource("unminified.js", "var a = 1;\n//# sourceMappingURL=unminified.js.map", WebResource.Options.WebResourceType.ScriptJScript);
        var falsePositive = CreateWebResource("false-positive.js", "var msg = 'Please avoid sourceMappingURL in production bundles';", WebResource.Options.WebResourceType.ScriptJScript);
        var stringLiteralDirective = CreateWebResource("string-literal.js", "var x = '//# sourceMappingURL=foo';", WebResource.Options.WebResourceType.ScriptJScript);
        var stylesheet = CreateWebResource("styles.css", "body { }\n/*# sourceMappingURL=styles.css.map */", WebResource.Options.WebResourceType.StyleSheetCSS);

        var components = new[] { minified, unminified, falsePositive, stringLiteralDirective, stylesheet }.Select(webResource => new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.WebResource),
            [SolutionComponent.LogicalNames.ObjectId] = webResource.Id,
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
            [SolutionComponent.LogicalNames.IsMetadata] = false
        }).Cast<Entity>();

        return [solution, minified, unminified, falsePositive, stringLiteralDirective, stylesheet, .. components];
    }

    private static WebResource CreateWebResource(string name, string content, int webResourceType) =>
        new(Guid.NewGuid())
        {
            Name = name,
            Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content)),
            WebResourceType = new OptionSetValue(webResourceType)
        };
}
