// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.tests.Planning;

public class PluginPushPlannerTests
{
    private static LocalAssembly Assembly(string version) => new()
    {
        Name = "MyPlugins",
        Version = System.Version.Parse(version),
        Content = "base64",
        Kind = LocalAssemblyKind.Plugin
    };

    [Test]
    public async Task PlanAssembly_NoRemoteMatch_ReturnsCreate()
    {
        var plan = PluginPushPlanner.PlanAssembly(Assembly("1.0.0.0"), remote: null);

        await Assert.That(plan.Action).IsEqualTo(AssemblyAction.Create);
        await Assert.That(plan.Existing).IsNull();
    }

    [Test]
    [Arguments("1.0.0.0", "1.0.1.0")]
    [Arguments("1.0.0.0", "1.0.0.5")]
    [Arguments("2.3.0.0", "2.3.9.9")]
    public async Task PlanAssembly_SameMajorMinor_ReturnsUpdate(string localVersion, string remoteVersion)
    {
        var remote = new RemoteAssembly(Guid.NewGuid(), System.Version.Parse(remoteVersion), PackageId: null);

        var plan = PluginPushPlanner.PlanAssembly(Assembly(localVersion), remote);

        await Assert.That(plan.Action).IsEqualTo(AssemblyAction.Update);
        await Assert.That(plan.Existing).IsEqualTo(remote);
    }

    [Test]
    [Arguments("1.0.0.0", "1.1.0.0")]
    [Arguments("1.0.0.0", "2.0.0.0")]
    [Arguments("2.3.0.0", "2.4.0.0")]
    public async Task PlanAssembly_DifferentMajorOrMinor_ReturnsUpgrade(string localVersion, string remoteVersion)
    {
        var remote = new RemoteAssembly(Guid.NewGuid(), System.Version.Parse(remoteVersion), PackageId: null);

        var plan = PluginPushPlanner.PlanAssembly(Assembly(localVersion), remote);

        await Assert.That(plan.Action).IsEqualTo(AssemblyAction.Upgrade);
    }

    [Test]
    public async Task PlanAssembly_OwnedByPackage_ReturnsOwnedByPackageRegardlessOfVersion()
    {
        var remote = new RemoteAssembly(Guid.NewGuid(), System.Version.Parse("9.9.9.9"), Guid.NewGuid());

        var plan = PluginPushPlanner.PlanAssembly(Assembly("1.0.0.0"), remote);

        await Assert.That(plan.Action).IsEqualTo(AssemblyAction.OwnedByPackage);
        await Assert.That(plan.Existing).IsEqualTo(remote);
    }

    [Test]
    public async Task PlanPackage_NoRemoteMatch_ReturnsCreate()
    {
        var local = new LocalPackage("MyPackage", "1.0.0", "base64");

        var plan = PluginPushPlanner.PlanPackage(local, remote: null);

        await Assert.That(plan.Action).IsEqualTo(PackageAction.Create);
        await Assert.That(plan.Existing).IsNull();
    }

    [Test]
    [Arguments("1.0.0", "1.0.0")]
    [Arguments("1.0.0", "2.0.0")]
    [Arguments("1.0.0", "1.0.0-beta.1")]
    public async Task PlanPackage_RemoteMatchRegardlessOfVersion_ReturnsUpdate(string localVersion, string remoteVersion)
    {
        var local = new LocalPackage("MyPackage", localVersion, "base64");
        var remote = new RemotePackage(Guid.NewGuid());

        var plan = PluginPushPlanner.PlanPackage(local, remote);

        _ = remoteVersion; // version is intentionally irrelevant to the decision - see PlanPackage docs.
        await Assert.That(plan.Action).IsEqualTo(PackageAction.Update);
        await Assert.That(plan.Existing).IsEqualTo(remote);
    }

    private static LocalPluginType PluginType(string typeName, params LocalPluginStep[] steps) =>
        new(typeName, typeName, CustomApi: string.Empty, IsPowerPlugin: true, steps);

    private static LocalPluginStep Step(
        string name = "MyPlugin|account|Synchronous|PostOperation|Create",
        string messageName = "Create",
        int mode = 0,
        int stage = 40,
        string primaryEntityName = "account",
        IReadOnlyList<string>? filterAttributes = null,
        int? executionOrder = 1,
        string? configuration = null) =>
        new(name, mode, messageName, stage, primaryEntityName, "none", filterAttributes, executionOrder, configuration, []);

    [Test]
    public async Task PlanPluginTypes_NoRemoteMatch_ReturnsCreate()
    {
        var local = PluginType("MyPlugin");

        var (plans, purge) = PluginPushPlanner.PlanPluginTypes([local], []);

        await Assert.That(plans).Count().IsEqualTo(1);
        await Assert.That(plans[0].Action).IsEqualTo(PluginTypeAction.Create);
        await Assert.That(plans[0].Existing).IsNull();
        await Assert.That(purge).IsEmpty();
    }

    [Test]
    public async Task PlanPluginTypes_RemoteMatch_ReturnsReconcile()
    {
        var local = PluginType("MyPlugin");
        var remote = new RemotePluginType(Guid.NewGuid(), "MyPlugin");

        var (plans, purge) = PluginPushPlanner.PlanPluginTypes([local], [remote]);

        await Assert.That(plans).Count().IsEqualTo(1);
        await Assert.That(plans[0].Action).IsEqualTo(PluginTypeAction.Reconcile);
        await Assert.That(plans[0].Existing).IsEqualTo(remote);
        await Assert.That(purge).IsEmpty();
    }

    [Test]
    public async Task PlanPluginTypes_RemoteNoLocalMatch_ReturnsPurge()
    {
        var remote = new RemotePluginType(Guid.NewGuid(), "Orphaned");

        var (plans, purge) = PluginPushPlanner.PlanPluginTypes([], [remote]);

        await Assert.That(plans).IsEmpty();
        await Assert.That(purge).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task PlanPluginSteps_NoRemoteMatch_ReturnsCreate()
    {
        var local = Step();

        var (plans, purge) = PluginPushPlanner.PlanPluginSteps([local], []);

        await Assert.That(plans).Count().IsEqualTo(1);
        await Assert.That(plans[0].Action).IsEqualTo(PluginStepAction.Create);
        await Assert.That(purge).IsEmpty();
    }

    [Test]
    public async Task PlanPluginSteps_RemoteMatchNoContentDiff_ReturnsKeep()
    {
        var local = Step(name: "step", filterAttributes: ["a", "b"]);
        var remote = new RemotePluginStep(Guid.NewGuid(), "step", 0, "Create", 40, "account", ["b", "a"], 1, null);

        var (plans, purge) = PluginPushPlanner.PlanPluginSteps([local], [remote]);

        await Assert.That(plans).Count().IsEqualTo(1);
        await Assert.That(plans[0].Action).IsEqualTo(PluginStepAction.Keep);
        await Assert.That(plans[0].Existing).IsEqualTo(remote);
        await Assert.That(purge).IsEmpty();
    }

    [Test]
    [Arguments("step", "renamed", 1, 1, null, null)]
    [Arguments("step", "step", 1, 2, null, null)]
    [Arguments("step", "step", 1, 1, "old", "new")]
    public async Task PlanPluginSteps_RemoteMatchWithContentDiff_ReturnsUpdate(
        string localName, string remoteName, int localOrder, int remoteOrder, string? localConfig, string? remoteConfig)
    {
        var local = Step(name: localName, executionOrder: localOrder, configuration: localConfig);
        var remote = new RemotePluginStep(Guid.NewGuid(), remoteName, 0, "Create", 40, "account", null, remoteOrder, remoteConfig);

        var (plans, purge) = PluginPushPlanner.PlanPluginSteps([local], [remote]);

        await Assert.That(plans).Count().IsEqualTo(1);
        await Assert.That(plans[0].Action).IsEqualTo(PluginStepAction.Update);
        await Assert.That(plans[0].Existing).IsEqualTo(remote);
        await Assert.That(purge).IsEmpty();
    }

    [Test]
    public async Task PlanPluginSteps_MatchTreatsEmptyAndNoneEntityNamesAsEqual()
    {
        var local = Step(primaryEntityName: "none");
        var remote = new RemotePluginStep(Guid.NewGuid(), local.Name, local.Mode, local.MessageName, local.Stage, string.Empty, null, local.ExecutionOrder, null);

        var (plans, purge) = PluginPushPlanner.PlanPluginSteps([local], [remote]);

        await Assert.That(plans).Count().IsEqualTo(1);
        await Assert.That(plans[0].Action).IsEqualTo(PluginStepAction.Keep);
        await Assert.That(purge).IsEmpty();
    }

    [Test]
    public async Task PlanPluginSteps_RemoteNoLocalMatch_ReturnsPurge()
    {
        var remote = new RemotePluginStep(Guid.NewGuid(), "orphaned", 0, "Create", 40, "account", null, 1, null);

        var (plans, purge) = PluginPushPlanner.PlanPluginSteps([], [remote]);

        await Assert.That(plans).IsEmpty();
        await Assert.That(purge).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task PlanPluginStepImages_NoRemoteMatch_ReturnsCreate()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", null);

        var (plans, purge) = PluginPushPlanner.PlanPluginStepImages([local], []);

        await Assert.That(plans).Count().IsEqualTo(1);
        await Assert.That(plans[0].Action).IsEqualTo(PluginStepImageAction.Create);
        await Assert.That(purge).IsEmpty();
    }

    [Test]
    public async Task PlanPluginStepImages_RemoteMatchSameAttributes_ReturnsNoPlan()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["a", "b"]);
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "PreImage", 0, ["b", "a"]);

        var (plans, purge) = PluginPushPlanner.PlanPluginStepImages([local], [remote]);

        await Assert.That(plans).IsEmpty();
        await Assert.That(purge).IsEmpty();
    }

    [Test]
    public async Task PlanPluginStepImages_RemoteMatchDifferentAttributes_ReturnsUpdate()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["a"]);
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "PreImage", 0, ["a", "b"]);

        var (plans, purge) = PluginPushPlanner.PlanPluginStepImages([local], [remote]);

        await Assert.That(plans).Count().IsEqualTo(1);
        await Assert.That(plans[0].Action).IsEqualTo(PluginStepImageAction.Update);
        await Assert.That(purge).IsEmpty();
    }

    [Test]
    public async Task PlanPluginStepImages_RemoteNoLocalMatch_ReturnsPurge()
    {
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "orphaned", 1, null);

        var (plans, purge) = PluginPushPlanner.PlanPluginStepImages([], [remote]);

        await Assert.That(plans).IsEmpty();
        await Assert.That(purge).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task PlanOutdatedTypeMigration_MatchingReplacementByTypeName_HasReplacementTrue()
    {
        var outdated = new RemotePluginType(Guid.NewGuid(), "MyPlugin");
        var replacement = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, []);

        var migrations = PluginPushPlanner.PlanOutdatedTypeMigration([outdated], [replacement]);

        await Assert.That(migrations).Count().IsEqualTo(1);
        await Assert.That(migrations[0].OldTypeId).IsEqualTo(outdated.Id);
        await Assert.That(migrations[0].TypeName).IsEqualTo("MyPlugin");
        await Assert.That(migrations[0].HasReplacement).IsTrue();
    }

    [Test]
    public async Task PlanOutdatedTypeMigration_NoMatchingReplacement_HasReplacementFalse()
    {
        var outdated = new RemotePluginType(Guid.NewGuid(), "RemovedPlugin");

        var migrations = PluginPushPlanner.PlanOutdatedTypeMigration([outdated], []);

        await Assert.That(migrations).Count().IsEqualTo(1);
        await Assert.That(migrations[0].HasReplacement).IsFalse();
    }
}
