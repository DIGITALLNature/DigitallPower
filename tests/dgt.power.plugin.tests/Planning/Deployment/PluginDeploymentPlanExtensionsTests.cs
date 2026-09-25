// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Planning.Comparison;
using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.tests.Planning.Deployment;

public class PluginDeploymentPlanExtensionsTests
{
    [Test]
    public async Task HasChanges_IdenticalAssemblyPlan_ReturnsFalse()
    {
        var local = new LocalAssembly
        {
            Name = "MyPlugins",
            Version = Version.Parse("1.0.0.0"),
            Content = "Y29udGVudA==",
            ContentHash = "hash"
        };
        var remote = new RemoteAssembly(Guid.NewGuid(), local.Version, PackageId: null, ContentHash: local.ContentHash);
        var plan = new AssemblyDeploymentPlan(
            new AssemblyComparison(local, remote),
            PluginTypes: null,
            new OutdatedAssemblyDeployment([]),
            LinkManagedIdentity: false,
            SolutionMembership: null,
            Solution: null);

        await Assert.That(plan.HasChanges()).IsFalse();
    }

    [Test]
    public async Task HasChanges_SolutionMembershipOnly_ReturnsTrue()
    {
        var local = new LocalAssembly
        {
            Name = "MyPlugins",
            Version = Version.Parse("1.0.0.0"),
            Content = "Y29udGVudA==",
            ContentHash = "hash"
        };
        var remote = new RemoteAssembly(Guid.NewGuid(), local.Version, PackageId: null, ContentHash: local.ContentHash);
        var plan = new AssemblyDeploymentPlan(
            new AssemblyComparison(local, remote),
            PluginTypes: null,
            new OutdatedAssemblyDeployment([]),
            LinkManagedIdentity: false,
            SolutionMembership: new SolutionMembershipPlan("test_solution"),
            Solution: new SolutionLink(91, local.Name, "test_solution"));

        await Assert.That(plan.HasChanges()).IsTrue();
    }
}
