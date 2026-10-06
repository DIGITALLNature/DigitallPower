// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.tests.Planning.Deployment;

public class PluginDeploymentPlanExtensionsTests
{
    [Test]
    [Arguments(0, false)]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(4, true)]
    public async Task HasChanges_ProviderOnlyChange_DetectedForAssemblyAndPackage(int change, bool expected)
    {
        var local = new LocalAssembly
        {
            Name = "MyPlugins", Version = new Version(1, 0, 0, 0), Content = "Y29udGVudA==", ContentHash = "hash"
        };
        var remote = new RemoteAssembly(Guid.NewGuid(), local.Version, PackageId: null, ContentHash: local.ContentHash);
        var provider = new DataProviderDeployment(
            new LocalDataProvider("dgt_Source", "Provider", null, null, null, new Dictionary<DataProviderOperation, string>()),
            new RemoteDataProvider(Guid.NewGuid(), "dgt_source", "Provider", null, new Dictionary<DataProviderOperation, Guid>()),
            DataProviderDeploymentPlannerTests.Table(), change == 2, change == 1, new Dictionary<DataProviderOperation, string>(),
            change == 3 ? new SolutionLink(78, "Provider", "solution") : null,
            change == 4 ? new SolutionLink(1, "dgt_source", "solution") : null);
        var assemblyPlan = new AssemblyDeploymentPlan(new AssemblyComparison(local, remote),
            new PluginTypeDeployment([], []) { DataProviders = [provider] }, new OutdatedAssemblyDeployment([]), false, null, null);
        var package = new LocalPackage("Package", "1.0.0", string.Empty, "hash");
        var packagePlan = new PackageDeploymentPlan(new LocalPluginPackage(package, []), "dgt_Package",
            new PackageComparison(package, new RemotePackage(Guid.NewGuid(), "hash")), [], false, null, null)
        {
            DataProviders = [provider]
        };
        using (Assert.Multiple())
        {
            await Assert.That(assemblyPlan.HasChanges()).IsEqualTo(expected);
            await Assert.That(packagePlan.HasChanges()).IsEqualTo(expected);
        }
    }

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
