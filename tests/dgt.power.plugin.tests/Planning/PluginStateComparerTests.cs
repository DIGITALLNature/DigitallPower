// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Remote;
using System.Security.Cryptography;

namespace dgt.power.plugin.tests.Planning;

public class PluginStateComparerTests
{
    private static string PackageHash(string base64Content) =>
        Convert.ToHexString(SHA256.HashData(Convert.FromBase64String(base64Content)));

    private static LocalAssembly Assembly(string version) => new()
    {
        Name = "MyPlugins",
        Version = Version.Parse(version),
        Content = "base64",
        Kind = LocalAssemblyKind.Plugin
    };

    [Test]
    public async Task CompareAssembly_NoRemoteMatch_ReturnsCreate()
    {
        var change = PluginStateComparer.CompareAssembly(Assembly("1.0.0.0"), remote: null);

        await Assert.That(change is CreateAssemblyChange).IsTrue();
        await Assert.That(change.Existing).IsNull();
    }

    [Test]
    [Arguments("1.0.0.0", "1.0.1.0")]
    [Arguments("1.0.0.0", "1.0.0.5")]
    [Arguments("2.3.0.0", "2.3.9.9")]
    public async Task CompareAssembly_SameMajorMinor_ReturnsUpdate(string localVersion, string remoteVersion)
    {
        var remote = new RemoteAssembly(Guid.NewGuid(), Version.Parse(remoteVersion), PackageId: null);

        var change = PluginStateComparer.CompareAssembly(Assembly(localVersion), remote);

        await Assert.That(change is UpdateAssemblyChange).IsTrue();
        await Assert.That(change.Existing).IsEqualTo(remote);
    }

    [Test]
    [Arguments("1.0.0.0", "1.1.0.0")]
    [Arguments("1.0.0.0", "2.0.0.0")]
    [Arguments("2.3.0.0", "2.4.0.0")]
    public async Task CompareAssembly_DifferentMajorOrMinor_ReturnsUpgrade(string localVersion, string remoteVersion)
    {
        var remote = new RemoteAssembly(Guid.NewGuid(), Version.Parse(remoteVersion), PackageId: null);

        var change = PluginStateComparer.CompareAssembly(Assembly(localVersion), remote);

        await Assert.That(change is UpgradeAssemblyChange).IsTrue();
    }

    [Test]
    public async Task CompareAssembly_OwnedByPackage_ReturnsOwnedByPackageRegardlessOfVersion()
    {
        var remote = new RemoteAssembly(Guid.NewGuid(), Version.Parse("9.9.9.9"), Guid.NewGuid());

        var change = PluginStateComparer.CompareAssembly(Assembly("1.0.0.0"), remote);

        await Assert.That(change is PackageOwnedAssemblyChange).IsTrue();
        await Assert.That(change.Existing).IsEqualTo(remote);
    }

    [Test]
    public async Task ComparePackage_NoRemoteMatch_ReturnsCreate()
    {
        var local = new LocalPackage("MyPackage", "1.0.0", "base64");

        var change = PluginStateComparer.ComparePackage(local, remote: null);

        await Assert.That(change.Action).IsEqualTo(PackageAction.Create);
        await Assert.That(change.Existing).IsNull();
    }

    [Test]
    [Arguments("1.0.0", "1.0.0")]
    [Arguments("1.0.0", "2.0.0")]
    [Arguments("1.0.0", "1.0.0-beta.1")]
    public async Task ComparePackage_IdenticalContentRegardlessOfVersion_ReturnsUnchanged(
        string localVersion,
        string remoteVersion)
    {
        var local = new LocalPackage("MyPackage", localVersion, "YmFzZTY0");
        var remote = new RemotePackage(Guid.NewGuid(), PackageHash(local.Content));

        var change = PluginStateComparer.ComparePackage(local, remote);

        _ = remoteVersion; // Dataverse package versions are immutable and do not affect deployment.
        await Assert.That(change.Action).IsEqualTo(PackageAction.Unchanged);
        await Assert.That(change.Existing).IsEqualTo(remote);
    }

    [Test]
    public async Task ComparePackage_DifferentContent_ReturnsUpdate()
    {
        var local = new LocalPackage("MyPackage", "1.0.0", "bmV3LWNvbnRlbnQ=");
        var remote = new RemotePackage(Guid.NewGuid(), PackageHash("b2xkLWNvbnRlbnQ="));

        var change = PluginStateComparer.ComparePackage(local, remote);

        await Assert.That(change.Action).IsEqualTo(PackageAction.Update);
        await Assert.That(change.Existing).IsEqualTo(remote);
    }

    [Test]
    public async Task ComparePackage_MatchingFileHash_ReturnsUnchanged()
    {
        var local = new LocalPackage("MyPackage", "1.0.0", "YmFzZTY0");
        var remote = new RemotePackage(Guid.NewGuid(), PackageHash(local.Content));

        var change = PluginStateComparer.ComparePackage(local, remote);

        await Assert.That(change.Action).IsEqualTo(PackageAction.Unchanged);
        await Assert.That(change.Existing).IsEqualTo(remote);
    }

    private static LocalPluginType PluginType(string typeName, params LocalPluginStep[] steps) =>
        new(typeName, typeName, CustomApi: string.Empty, HasRegistrationAttribute: true, steps);

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Major Code Smell", "S107", Justification = "Test fixture factory parameters map directly to LocalPluginStep fields.")]
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
    public async Task ComparePluginTypes_NoRemoteMatch_ReturnsCreate()
    {
        var local = PluginType("MyPlugin");

        var (changes, deletions) = PluginStateComparer.ComparePluginTypes([local], []);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].Action).IsEqualTo(PluginTypeAction.Create);
        await Assert.That(changes[0].Existing).IsNull();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginTypes_RemoteMatch_ReturnsUnchanged()
    {
        var local = PluginType("MyPlugin");
        var remote = new RemotePluginType(Guid.NewGuid(), "MyPlugin");

        var (changes, deletions) = PluginStateComparer.ComparePluginTypes([local], [remote]);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].Action).IsEqualTo(PluginTypeAction.Unchanged);
        await Assert.That(changes[0].Existing).IsEqualTo(remote);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginTypes_RemoteNoLocalMatch_ReturnsDeletion()
    {
        var remote = new RemotePluginType(Guid.NewGuid(), "Orphaned");

        var (changes, deletions) = PluginStateComparer.ComparePluginTypes([], [remote]);

        await Assert.That(changes).IsEmpty();
        await Assert.That(deletions).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task ComparePluginSteps_NoRemoteMatch_ReturnsCreate()
    {
        var local = Step();

        var (changes, deletions) = PluginStateComparer.ComparePluginSteps([local], []);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].Action).IsEqualTo(PluginStepAction.Create);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginSteps_RemoteMatchNoContentDiff_ReturnsUnchanged()
    {
        var local = Step(name: "step", filterAttributes: ["a", "b"]);
        var remote = new RemotePluginStep(Guid.NewGuid(), "step", 0, "Create", 40, "account", "none", ["b", "a"], 1, null);

        var (changes, deletions) = PluginStateComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].Action).IsEqualTo(PluginStepAction.Unchanged);
        await Assert.That(changes[0].Existing).IsEqualTo(remote);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginSteps_EmptyAndNullFilterAttributesAreEquivalent()
    {
        var local = Step(name: "step", filterAttributes: []);
        var remote = new RemotePluginStep(Guid.NewGuid(), "step", 0, "Create", 40, "account", "none", null, 1, null);

        var (changes, deletions) = PluginStateComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes[0].Action).IsEqualTo(PluginStepAction.Unchanged);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    [Arguments("step", "renamed", 1, 1, null, null)]
    [Arguments("step", "step", 1, 2, null, null)]
    [Arguments("step", "step", 1, 1, "old", "new")]
    public async Task ComparePluginSteps_RemoteMatchWithContentDiff_ReturnsUpdate(
        string localName, string remoteName, int localOrder, int remoteOrder, string? localConfig, string? remoteConfig)
    {
        var local = Step(name: localName, executionOrder: localOrder, configuration: localConfig);
        var remote = new RemotePluginStep(Guid.NewGuid(), remoteName, 0, "Create", 40, "account", "none", null, remoteOrder, remoteConfig);

        var (changes, deletions) = PluginStateComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].Action).IsEqualTo(PluginStepAction.Update);
        await Assert.That(changes[0].Existing).IsEqualTo(remote);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginSteps_MatchTreatsEmptyAndNoneEntityNamesAsEqual()
    {
        var local = Step(primaryEntityName: "none");
        var remote = new RemotePluginStep(Guid.NewGuid(), local.Name, local.Mode, local.MessageName, local.Stage, string.Empty, "none", null, local.ExecutionOrder, null);

        var (changes, deletions) = PluginStateComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].Action).IsEqualTo(PluginStepAction.Unchanged);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginSteps_MatchIsCaseInsensitiveForMessageName()
    {
        var local = Step(messageName: "create");
        var remote = new RemotePluginStep(
            Guid.NewGuid(), local.Name, local.Mode, "Create", local.Stage, "account", "none", null, local.ExecutionOrder, null);

        var (changes, deletions) = PluginStateComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes[0].Action).IsEqualTo(PluginStepAction.Unchanged);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginSteps_DifferentSecondaryEntityNamesDoNotMatch()
    {
        var local = Step() with { SecondaryEntityName = "contact" };
        var remote = new RemotePluginStep(
            Guid.NewGuid(), local.Name, local.Mode, local.MessageName, local.Stage, "account", "none", null, local.ExecutionOrder, null);

        var (changes, deletions) = PluginStateComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes[0].Action).IsEqualTo(PluginStepAction.Create);
        await Assert.That(deletions).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task ComparePluginSteps_RemoteNoLocalMatch_ReturnsDeletion()
    {
        var remote = new RemotePluginStep(Guid.NewGuid(), "orphaned", 0, "Create", 40, "account", "none", null, 1, null);

        var (changes, deletions) = PluginStateComparer.ComparePluginSteps([], [remote]);

        await Assert.That(changes).IsEmpty();
        await Assert.That(deletions).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task ComparePluginStepImages_NoRemoteMatch_ReturnsCreate()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", null);

        var (changes, deletions) = PluginStateComparer.ComparePluginStepImages([local], []);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].Action).IsEqualTo(PluginStepImageAction.Create);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginStepImages_RemoteMatchSameAttributes_ReturnsNoPlan()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["a", "b"]);
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "PreImage", 0, ["b", "a"]);

        var (changes, deletions) = PluginStateComparer.ComparePluginStepImages([local], [remote]);

        await Assert.That(changes).IsEmpty();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginStepImages_EmptyAndNullAttributesAreEquivalent()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", []);
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "PreImage", 0, null);

        var (changes, deletions) = PluginStateComparer.ComparePluginStepImages([local], [remote]);

        await Assert.That(changes).IsEmpty();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginStepImages_RemoteMatchDifferentAttributes_ReturnsUpdate()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["a"]);
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "PreImage", 0, ["a", "b"]);

        var (changes, deletions) = PluginStateComparer.ComparePluginStepImages([local], [remote]);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].Action).IsEqualTo(PluginStepImageAction.Update);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginStepImages_RemoteNoLocalMatch_ReturnsDeletion()
    {
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "orphaned", 1, null);

        var (changes, deletions) = PluginStateComparer.ComparePluginStepImages([], [remote]);

        await Assert.That(changes).IsEmpty();
        await Assert.That(deletions).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task CompareOutdatedTypes_MatchingReplacementByTypeName_HasReplacementTrue()
    {
        var outdated = new RemotePluginType(Guid.NewGuid(), "MyPlugin");
        var replacement = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, []);

        var migrations = PluginStateComparer.CompareOutdatedTypes([outdated], [replacement]);

        await Assert.That(migrations).Count().IsEqualTo(1);
        await Assert.That(migrations[0].OldTypeId).IsEqualTo(outdated.Id);
        await Assert.That(migrations[0].TypeName).IsEqualTo("MyPlugin");
        await Assert.That(migrations[0].HasReplacement).IsTrue();
    }

    [Test]
    public async Task CompareOutdatedTypes_NoMatchingReplacement_HasReplacementFalse()
    {
        var outdated = new RemotePluginType(Guid.NewGuid(), "RemovedPlugin");

        var migrations = PluginStateComparer.CompareOutdatedTypes([outdated], []);

        await Assert.That(migrations).Count().IsEqualTo(1);
        await Assert.That(migrations[0].HasReplacement).IsFalse();
    }
}
