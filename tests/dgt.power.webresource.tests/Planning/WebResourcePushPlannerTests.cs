// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Local;
using dgt.power.webresource.Planning;
using dgt.power.webresource.Remote;

namespace dgt.power.webresource.tests.Planning;

public class WebResourcePushPlannerTests
{
    [Test]
    public async Task Plan_ClassifiesCreateUpdateAndUnchanged()
    {
        var local = new[]
        {
            CreateLocal("contoso_/new.js", "new"),
            CreateLocal("contoso_/changed.js", "new"),
            CreateLocal("contoso_/same.js", "same")
        };
        var remote = new[]
        {
            CreateRemote("contoso_/changed.js", "old"),
            CreateRemote("contoso_/same.js", "same")
        };

        var plan = WebResourcePushPlanner.Plan(local, remote);

        await Assert.That(plan.Resources).Count().IsEqualTo(3);
        await Assert.That(plan.Resources.Single(item => item.Local.Name == "contoso_/new.js").Action)
            .IsEqualTo(WebResourceAction.Create);
        await Assert.That(plan.Resources.Single(item => item.Local.Name == "contoso_/changed.js").Action)
            .IsEqualTo(WebResourceAction.Update);
        await Assert.That(plan.Resources.Single(item => item.Local.Name == "contoso_/same.js").Action)
            .IsEqualTo(WebResourceAction.Unchanged);
    }

    [Test]
    public async Task Plan_DeletesOnlyUnmanagedResourcesMissingFromLocalSet()
    {
        var local = new[] { CreateLocal("contoso_/kept.js", "same") };
        var solutionResources = new[]
        {
            new RemoteSolutionWebResource(Guid.NewGuid(), 3, "contoso_/kept.js", false),
            new RemoteSolutionWebResource(Guid.NewGuid(), 3, "contoso_/obsolete.js", false),
            new RemoteSolutionWebResource(Guid.NewGuid(), 3, "contoso_/managed.js", true)
        };

        var plan = WebResourcePushPlanner.Plan(
            local,
            [],
            solutionResources,
            deleteObsolete: true);

        await Assert.That(plan.Obsolete).Count().IsEqualTo(1);
        await Assert.That(plan.Obsolete[0].Name).IsEqualTo("contoso_/obsolete.js");
    }

    [Test]
    public async Task Plan_RejectsManagedResourceUpdates()
    {
        await Assert.That(Action).Throws<ManagedWebResourceException>();
        return;

        static WebResourcePushPlan Action() => WebResourcePushPlanner.Plan([CreateLocal("contoso_/managed.js", "new")], [new RemoteWebResource(Guid.NewGuid(), 3, "contoso_/managed.js", "old", true)]);
    }

    [Test]
    public async Task Plan_AllowsUnchangedManagedResource()
    {
        var local = CreateLocal("contoso_/managed.js", "same");
        var remote = new RemoteWebResource(
            Guid.NewGuid(),
            3,
            "contoso_/managed.js",
            local.Content,
            true);

        var plan = WebResourcePushPlanner.Plan([local], [remote]);

        await Assert.That(plan.Resources[0].Action).IsEqualTo(WebResourceAction.Unchanged);
    }

    [Test]
    public async Task Plan_MatchesRemoteNamesCaseInsensitively()
    {
        var local = CreateLocal("contoso_/app/main.js", "same");
        var remote = new RemoteWebResource(
            Guid.NewGuid(),
            3,
            "CONTOSO_/APP/MAIN.JS",
            local.Content,
            false);

        var plan = WebResourcePushPlanner.Plan([local], [remote]);

        await Assert.That(plan.Resources[0].Action).IsEqualTo(WebResourceAction.Unchanged);
    }

    [Test]
    public async Task Plan_RecordsMissingSolutionMembership()
    {
        var existingId = Guid.NewGuid();
        var existing = CreateLocal("contoso_/existing.js", "same");
        var created = CreateLocal("contoso_/new.js", "new");
        var remote = new RemoteWebResource(existingId, 3, existing.Name, existing.Content, false);

        var plan = WebResourcePushPlanner.Plan(
            [existing, created],
            [remote],
            [],
            solutionUniqueName: "ContosoCore");

        await Assert.That(plan.SolutionUniqueName).IsEqualTo("ContosoCore");
        await Assert.That(plan.Resources.All(resource => resource.AddToSolution)).IsTrue();
    }

    private static LocalWebResource CreateLocal(string name, string content) =>
        new(3, name, Path.GetFileName(name), Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(content)), "hash", name);

    private static RemoteWebResource CreateRemote(string name, string content) =>
        new(Guid.NewGuid(), 3, name, Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(content)), false);
}
