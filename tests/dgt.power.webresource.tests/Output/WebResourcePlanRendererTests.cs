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
    }

    [Test]
    public async Task Render_ShowsCheckmarkWhenAllResourcesAreInSolution()
    {
        using var console = new TestConsole();
        var plan = new WebResourcePushPlan([], [], "ContosoCore");

        new WebResourcePlanRenderer(console).Render(plan, @"C:\build\webresources");

        await Assert.That(console.Output).Contains("All managed components are already present");
        await Assert.That(console.Output).Contains("✔");
    }
}
