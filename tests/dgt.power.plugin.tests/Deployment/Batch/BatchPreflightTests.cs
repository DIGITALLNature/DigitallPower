// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Batch;
using dgt.power.plugin.Deployment.Local;

namespace dgt.power.plugin.tests.Deployment.Batch;

public class BatchPreflightTests
{
    internal static LocalAssembly Assembly(string name, string customApi = "", string? dataSource = null) => new()
    {
        Name = name, Version = new Version(1, 0, 0, 0), Content = "Y29udGVudA==", ContentHash = Convert.ToHexString(System.Security.Cryptography.SHA256.HashData("content"u8)),
        Kind = LocalAssemblyKind.DeclarativePlugin,
        PluginTypes = [new LocalPluginType("Plugin", name + ".Plugin", customApi, true, [])
        {
            DataProviders = dataSource is null
                ? []
                : [new LocalDataProviderRegistration(dataSource, DataProviderOperation.Retrieve, "Provider", null, null, null)]
        }]
    };

    internal static LocalDeploymentTarget Standalone(string path, LocalAssembly assembly) => new(path, assembly, null);

    internal static LocalDeploymentTarget Package(string path, string name, params LocalAssembly[] assemblies) =>
        new(path, null, new LocalPluginPackage(new LocalPackage(name, "1.0.0", "Y29udGVudA==", name), assemblies));

    [Test]
    public async Task FindConflicts_DistinctTargets_ReturnsNone()
    {
        var conflicts = BatchPreflight.FindConflicts([Standalone("a.dll", Assembly("A")), Package("p.nupkg", "P", Assembly("B"))]);

        await Assert.That(conflicts).IsEmpty();
    }

    [Test]
    public async Task FindConflicts_PackageAndItsConstituentDll_ReportsAssembly()
    {
        var conflicts = BatchPreflight.FindConflicts([Standalone("bin/B.dll", Assembly("B")), Package("bin/p.nupkg", "P", Assembly("B"))]);

        await Assert.That(conflicts).Count().IsEqualTo(1);
        await Assert.That(conflicts[0]).Contains("Assembly 'B'").And.Contains("bin/B.dll").And.Contains("bin/p.nupkg");
    }

    [Test]
    public async Task FindConflicts_SameAssemblyNameInTwoBuildOutputs_Reports()
    {
        var conflicts = BatchPreflight.FindConflicts([Standalone("Debug/A.dll", Assembly("A")), Standalone("Release/a.dll", Assembly("a"))]);

        await Assert.That(conflicts).Count().IsEqualTo(1);
    }

    [Test]
    public async Task FindConflicts_SamePackageNameInTwoTargets_Reports()
    {
        var conflicts = BatchPreflight.FindConflicts([Package("1/p.nupkg", "Pkg", Assembly("A")), Package("2/p.nupkg", "pkg", Assembly("B"))]);

        await Assert.That(conflicts).Count().IsEqualTo(1);
        await Assert.That(conflicts[0]).Contains("Package");
    }

    [Test]
    public async Task FindConflicts_SameDataProviderInSeparateTargets_Reports()
    {
        var conflicts = BatchPreflight.FindConflicts([Standalone("a.dll", Assembly("A", dataSource: "dgt_Source")), Standalone("b.dll", Assembly("B", dataSource: "DGT_source"))]);

        await Assert.That(conflicts).Count().IsEqualTo(1);
        await Assert.That(conflicts[0]).Contains("Data provider");
    }

    [Test]
    public async Task FindConflicts_SameCustomApiInSeparateTargets_Reports()
    {
        var conflicts = BatchPreflight.FindConflicts([Standalone("a.dll", Assembly("A", customApi: "dgt_Api")), Standalone("b.dll", Assembly("B", customApi: "dgt_api"))]);

        await Assert.That(conflicts).Count().IsEqualTo(1);
        await Assert.That(conflicts[0]).Contains("Custom API");
    }

    [Test]
    public async Task FindConflicts_SameIdentityInsideOneTarget_IsNotACrossTargetConflict()
    {
        var conflicts = BatchPreflight.FindConflicts([Package("p.nupkg", "P", Assembly("A", dataSource: "dgt_Source"), Assembly("B", dataSource: "dgt_source"))]);

        await Assert.That(conflicts).IsEmpty();
    }
}
