// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Local;
using dgt.power.webresource.Output;
using dgt.power.webresource.Planning;
using dgt.power.webresource.Remote;
using Spectre.Console.Testing;

namespace dgt.power.webresource.tests.Output;

public class WebResourcePlanRendererTests
{
    [Test]
    public async Task Render_ShowsSolutionMembershipAndObsoleteResources()
    {
        using var console = new TestConsole();
        console.Profile.Width = 200;
        var local = new LocalWebResource(
            3,
            "contoso_/app/main.js",
            "main.js",
            Convert.ToBase64String("content"u8.ToArray()),
            "hash",
            "app/main.js");
        var obsolete = new RemoteSolutionWebResource(Guid.NewGuid(), 3, "contoso_/obsolete.js", false);
        var plan = new WebResourcePushPlan(
            [new WebResourcePlanItem(local, WebResourceAction.Create, null, true)],
            [obsolete],
            "ContosoCore");

        new WebResourcePlanRenderer(console).Render(plan, @"C:\build\webresources");

        await Assert.That(console.Output).Contains(@"C:\build\webresources");
        await Assert.That(console.Output).Contains("Solution membership: ContosoCore");
        await Assert.That(console.Output).Contains("contoso_/app/main.js");
        await Assert.That(console.Output).Contains("Obsolete webresources");
        await Assert.That(console.Output).Contains("contoso_/obsolete.js");
        var output = console.Output.Replace("\r\n", "\n", StringComparison.Ordinal);
        await Assert.That(output).Contains("main.js → contoso_/app/main.js + Create");
        await Assert.That(output).Contains("\n\nSolution membership: ContosoCore\nWebResource contoso_/app/main.js + Add\n");
        await Assert.That(output).Contains("\n\nObsolete webresources\nWebResource contoso_/obsolete.js - Delete\n");
        await Assert.That(output).Contains("\n\nDestructive operations: 1\n");
    }

    [Test]
    public async Task Render_ShowsCheckmarkWhenAllResourcesAreInSolution()
    {
        using var console = new TestConsole();
        var plan = new WebResourcePushPlan([], [], "ContosoCore");

        new WebResourcePlanRenderer(console).Render(plan, @"C:\build\webresources");

        await Assert.That(console.Output).Contains("All components are already in the solution");
        await Assert.That(console.Output).Contains("✔");
    }

    [Test]
    [Arguments(WebResourceAction.Create, " + Create")]
    [Arguments(WebResourceAction.Update, " ~ Update")]
    [Arguments(WebResourceAction.Unchanged, "")]
    public async Task Render_ResourceAction_UsesTrailingPluginStyleMarker(WebResourceAction action, string marker)
    {
        using var console = new TestConsole();
        console.Profile.Width = 200;
        var local = new LocalWebResource(3, "contoso_/main.js", "main.js", "content", "hash", "main.js");
        var plan = new WebResourcePushPlan([new WebResourcePlanItem(local, action, null, false)], [], null);

        new WebResourcePlanRenderer(console).Render(plan, @"C:\build\webresources");

        var output = console.Output.Replace("\r\n", "\n", StringComparison.Ordinal);
        await Assert.That(output).Contains($"main.js → contoso_/main.js{marker}\n");
        await Assert.That(output).DoesNotContain("Unchanged");
        await Assert.That(output).DoesNotContain("Solution membership");
        await Assert.That(output).DoesNotContain("Obsolete webresources");
        await Assert.That(output).DoesNotContain("Destructive operations");
    }

    [Test]
    public async Task Render_NamesWithMarkupBrackets_AreRenderedLiterally()
    {
        using var console = new TestConsole();
        console.Profile.Width = 200;
        var local = new LocalWebResource(3, "contoso_/[main].js", "[main].js", "content", "hash", "[main].js");
        var obsolete = new RemoteSolutionWebResource(Guid.NewGuid(), 3, "contoso_/[old].js", false);
        var plan = new WebResourcePushPlan(
            [new WebResourcePlanItem(local, WebResourceAction.Create, null, true)], [obsolete], "[solution]");

        new WebResourcePlanRenderer(console).Render(plan, @"C:\build\[webresources]");

        await Assert.That(console.Output).Contains(@"C:\build\[webresources]");
        await Assert.That(console.Output).Contains("[main].js → contoso_/[main].js + Create");
        await Assert.That(console.Output).Contains("Solution membership: [solution]");
        await Assert.That(console.Output).Contains("WebResource contoso_/[main].js + Add");
        await Assert.That(console.Output).Contains("WebResource contoso_/[old].js - Delete");
    }
}
