// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Remote;
using System.Security.Cryptography;

namespace dgt.power.plugin.tests.Planning;

public class PluginComparisonTests
{
    private static string ContentHash(string base64Content) =>
        Convert.ToHexString(SHA256.HashData(Convert.FromBase64String(base64Content)));

    private static LocalPackage Package(string name, string version, string content) =>
        new(name, version, content, ContentHash(content));

    private static LocalAssembly Assembly(string version, string content = "YmFzZTY0") => new()
    {
        Name = "MyPlugins",
        Version = Version.Parse(version),
        Content = content,
        ContentHash = ContentHash(content),
        Kind = LocalAssemblyKind.Plugin
    };

    [Test]
    public async Task CompareAssembly_NoRemoteMatch_ReturnsCreate()
    {
        var comparison = new AssemblyComparison(Assembly("1.0.0.0"), Remote: null);

        await Assert.That(comparison.RequiresCreate).IsTrue();
        await Assert.That(comparison.Remote).IsNull();
    }

    [Test]
    [Arguments("1.0.0.0", "1.0.1.0")]
    [Arguments("1.0.0.0", "1.0.0.5")]
    [Arguments("2.3.0.0", "2.3.9.9")]
    public async Task CompareAssembly_SameMajorMinor_ReturnsUpdate(string localVersion, string remoteVersion)
    {
        var remote = new RemoteAssembly(Guid.NewGuid(), Version.Parse(remoteVersion), PackageId: null);

        var comparison = new AssemblyComparison(Assembly(localVersion), remote);

        await Assert.That(comparison.RequiresUpgrade).IsFalse();
        await Assert.That(comparison.RequiresUpdate).IsTrue();
        await Assert.That(comparison.Remote).IsEqualTo(remote);
    }

    [Test]
    [Arguments("1.0.0.0", "1.1.0.0")]
    [Arguments("1.0.0.0", "2.0.0.0")]
    [Arguments("2.3.0.0", "2.4.0.0")]
    public async Task CompareAssembly_DifferentMajorOrMinor_ReturnsUpgrade(string localVersion, string remoteVersion)
    {
        var remote = new RemoteAssembly(Guid.NewGuid(), Version.Parse(remoteVersion), PackageId: null);

        var comparison = new AssemblyComparison(Assembly(localVersion), remote);

        await Assert.That(comparison.RequiresUpgrade).IsTrue();
    }

    [Test]
    public async Task CompareAssembly_OwnedByPackage_ReturnsOwnedByPackageRegardlessOfVersion()
    {
        var remote = new RemoteAssembly(Guid.NewGuid(), Version.Parse("9.9.9.9"), Guid.NewGuid());

        var comparison = new AssemblyComparison(Assembly("1.0.0.0"), remote);

        await Assert.That(comparison.IsPackageOwned).IsTrue();
        await Assert.That(comparison.Remote).IsEqualTo(remote);
    }

    [Test]
    public async Task ComparePackage_NoRemoteMatch_ReturnsCreate()
    {
        var local = Package("MyPackage", "1.0.0", "YmFzZTY0");

        var comparison = new PackageComparison(local, Remote: null);

        await Assert.That(comparison.RequiresCreate).IsTrue();
        await Assert.That(comparison.Remote).IsNull();
    }

    [Test]
    [Arguments("1.0.0", "1.0.0")]
    [Arguments("1.0.0", "2.0.0")]
    [Arguments("1.0.0", "1.0.0-beta.1")]
    public async Task ComparePackage_IdenticalContentRegardlessOfVersion_ReturnsUnchanged(
        string localVersion,
        string remoteVersion)
    {
        var local = Package("MyPackage", localVersion, "YmFzZTY0");
        var remote = new RemotePackage(Guid.NewGuid(), ContentHash(local.Content));

        var comparison = new PackageComparison(local, remote);

        _ = remoteVersion; // Dataverse package versions are immutable and do not affect deployment.
        await Assert.That(comparison.RequiresUpdate).IsFalse();
        await Assert.That(comparison.Remote).IsEqualTo(remote);
    }

    [Test]
    public async Task ComparePackage_DifferentContent_ReturnsUpdate()
    {
        var local = Package("MyPackage", "1.0.0", "bmV3LWNvbnRlbnQ=");
        var remote = new RemotePackage(Guid.NewGuid(), ContentHash("b2xkLWNvbnRlbnQ="));

        var comparison = new PackageComparison(local, remote);

        await Assert.That(comparison.RequiresUpdate).IsTrue();
        await Assert.That(comparison.Remote).IsEqualTo(remote);
    }

    [Test]
    public async Task ComparePackage_MatchingFileHash_ReturnsUnchanged()
    {
        var local = Package("MyPackage", "1.0.0", "YmFzZTY0");
        var remote = new RemotePackage(Guid.NewGuid(), ContentHash(local.Content));

        var comparison = new PackageComparison(local, remote);

        await Assert.That(comparison.RequiresUpdate).IsFalse();
        await Assert.That(comparison.Remote).IsEqualTo(remote);
    }

    [Test]
    public async Task CompareAssembly_IdenticalContentAndVersion_RequiresNoUpdate()
    {
        var local = Assembly("1.0.0.0");
        var remote = new RemoteAssembly(
            Guid.NewGuid(),
            local.Version,
            PackageId: null,
            ContentHash: ContentHash(local.Content));

        var comparison = new AssemblyComparison(local, remote);

        using (Assert.Multiple())
        {
            await Assert.That(comparison.RequiresCreate).IsFalse();
            await Assert.That(comparison.RequiresUpgrade).IsFalse();
            await Assert.That(comparison.RequiresUpdate).IsFalse();
        }
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

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginTypes([local], []);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].RequiresCreate).IsTrue();
        await Assert.That(changes[0].Remote).IsNull();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginTypes_RemoteMatch_ReturnsUnchanged()
    {
        var local = PluginType("MyPlugin");
        var remote = new RemotePluginType(Guid.NewGuid(), "MyPlugin");

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginTypes([local], [remote]);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].RequiresCreate).IsFalse();
        await Assert.That(changes[0].Remote).IsEqualTo(remote);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginTypes_RemoteNoLocalMatch_ReturnsDeletion()
    {
        var remote = new RemotePluginType(Guid.NewGuid(), "Orphaned");

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginTypes([], [remote]);

        await Assert.That(changes).IsEmpty();
        await Assert.That(deletions).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task ComparePluginSteps_NoRemoteMatch_ReturnsCreate()
    {
        var local = Step();

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginSteps([local], []);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].RequiresCreate).IsTrue();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginSteps_RemoteMatchNoContentDiff_ReturnsUnchanged()
    {
        var local = Step(name: "step", filterAttributes: ["a", "b"]);
        var remote = new RemotePluginStep(Guid.NewGuid(), "step", 0, "Create", 40, "account", "none", ["b", "a"], 1, null);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].RequiresUpdate).IsFalse();
        await Assert.That(changes[0].Remote).IsEqualTo(remote);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginSteps_EmptyAndNullFilterAttributesAreEquivalent()
    {
        var local = Step(name: "step", filterAttributes: []);
        var remote = new RemotePluginStep(Guid.NewGuid(), "step", 0, "Create", 40, "account", "none", null, 1, null);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes[0].RequiresUpdate).IsFalse();
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

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].RequiresUpdate).IsTrue();
        await Assert.That(changes[0].Remote).IsEqualTo(remote);
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginSteps_MatchTreatsEmptyAndNoneEntityNamesAsEqual()
    {
        var local = Step(primaryEntityName: "none");
        var remote = new RemotePluginStep(Guid.NewGuid(), local.Name, local.Mode, local.MessageName, local.Stage, string.Empty, "none", null, local.ExecutionOrder, null);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].RequiresUpdate).IsFalse();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginSteps_MatchIsCaseInsensitiveForMessageName()
    {
        var local = Step(messageName: "create");
        var remote = new RemotePluginStep(
            Guid.NewGuid(), local.Name, local.Mode, "Create", local.Stage, "account", "none", null, local.ExecutionOrder, null);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes[0].RequiresUpdate).IsFalse();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginSteps_DifferentSecondaryEntityNamesDoNotMatch()
    {
        var local = Step() with { SecondaryEntityName = "contact" };
        var remote = new RemotePluginStep(
            Guid.NewGuid(), local.Name, local.Mode, local.MessageName, local.Stage, "account", "none", null, local.ExecutionOrder, null);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginSteps([local], [remote]);

        await Assert.That(changes[0].RequiresCreate).IsTrue();
        await Assert.That(deletions).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task ComparePluginSteps_RemoteNoLocalMatch_ReturnsDeletion()
    {
        var remote = new RemotePluginStep(Guid.NewGuid(), "orphaned", 0, "Create", 40, "account", "none", null, 1, null);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginSteps([], [remote]);

        await Assert.That(changes).IsEmpty();
        await Assert.That(deletions).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task ComparePluginStepImages_NoRemoteMatch_ReturnsCreate()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", null);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginStepImages([local], []);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].RequiresCreate).IsTrue();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginStepImages_RemoteMatchSameAttributes_ReturnsNoPlan()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["a", "b"]);
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "PreImage", 0, ["b", "a"]);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginStepImages([local], [remote]);

        await Assert.That(changes).IsEmpty();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginStepImages_EmptyAndNullAttributesAreEquivalent()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", []);
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "PreImage", 0, null);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginStepImages([local], [remote]);

        await Assert.That(changes).IsEmpty();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginStepImages_RemoteMatchDifferentAttributes_ReturnsUpdate()
    {
        var local = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["a"]);
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "PreImage", 0, ["a", "b"]);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginStepImages([local], [remote]);

        await Assert.That(changes).Count().IsEqualTo(1);
        await Assert.That(changes[0].RequiresUpdate).IsTrue();
        await Assert.That(deletions).IsEmpty();
    }

    [Test]
    public async Task ComparePluginStepImages_RemoteNoLocalMatch_ReturnsDeletion()
    {
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "orphaned", 1, null);

        var (changes, deletions) = PluginRegistrationComparer.ComparePluginStepImages([], [remote]);

        await Assert.That(changes).IsEmpty();
        await Assert.That(deletions).IsEquivalentTo([remote]);
    }

    [Test]
    public async Task CompareOutdatedTypes_MatchingReplacementByTypeName_HasReplacementTrue()
    {
        var outdated = new RemotePluginType(Guid.NewGuid(), "MyPlugin");
        var replacement = new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, []);

        var migrations = PluginRegistrationComparer.CompareOutdatedTypes([outdated], [replacement]);

        await Assert.That(migrations).Count().IsEqualTo(1);
        await Assert.That(migrations[0].OldTypeId).IsEqualTo(outdated.Id);
        await Assert.That(migrations[0].TypeName).IsEqualTo("MyPlugin");
        await Assert.That(migrations[0].HasReplacement).IsTrue();
    }

    [Test]
    public async Task CompareOutdatedTypes_NoMatchingReplacement_HasReplacementFalse()
    {
        var outdated = new RemotePluginType(Guid.NewGuid(), "RemovedPlugin");

        var migrations = PluginRegistrationComparer.CompareOutdatedTypes([outdated], []);

        await Assert.That(migrations).Count().IsEqualTo(1);
        await Assert.That(migrations[0].HasReplacement).IsFalse();
    }
}
