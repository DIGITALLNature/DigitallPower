// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Reconciliation;
using dgt.power.plugin.Exceptions;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

public class RegistrationDiffTests
{
    private static LocalPluginStep Step() => new("step", 0, "Update", 20, "account", "none", null, 100, []);
    private static LocalPluginType Type(LocalPluginStep step) => new("Plugin", "Plugin", string.Empty, true, [step]);

    private static RegistrationSnapshot Snapshot(LocalPluginStep step, params RemotePluginStepImage[] images)
    {
        var typeId = Guid.NewGuid();
        var stepId = Guid.NewGuid();
        return new RegistrationSnapshot([new RemotePluginType(typeId, "Plugin")],
            new Dictionary<Guid, IReadOnlyList<RemotePluginStep>>
            {
                [typeId] = [new RemotePluginStep(stepId, step.Name, step.Mode, step.MessageName, step.Stage, step.PrimaryEntityName, step.SecondaryEntityName, step.FilterAttributes, step.ExecutionOrder)]
            },
            new Dictionary<Guid, IReadOnlyList<RemotePluginStepImage>> { [stepId] = images });
    }

    [Test]
    public async Task Compare_SupersededAssemblyWithSameTypeName_DeletesOldTypeAndMovesStep()
    {
        var step = Step();
        var superseded = Snapshot(step);
        var empty = new RegistrationSnapshot([], new Dictionary<Guid, IReadOnlyList<RemotePluginStep>>(), new Dictionary<Guid, IReadOnlyList<RemotePluginStepImage>>());

        var changes = RegistrationDiff.Compare([Type(step)], empty, superseded);

        await Assert.That(changes.TypesToCreate.Single().TypeName).IsEqualTo("Plugin");
        await Assert.That(changes.TypesToDelete.Single().Id).IsEqualTo(superseded.Types.Single().Id);
        await Assert.That(changes.StepsToWrite.Single().ExistingId).IsEqualTo(superseded.StepsByType.Values.Single().Single().Id);
        await Assert.That(changes.StepsToDelete).IsEmpty();
    }

    [Test]
    public async Task Compare_IdenticalRegistrations_HasNoChanges()
    {
        var step = Step();

        await Assert.That(RegistrationDiff.Compare([Type(step)], Snapshot(step)).HasChanges).IsFalse();
    }

    [Test]
    [Arguments(30)]
    [Arguments(80)]
    [Arguments(90)]
    public async Task Compare_InternalStep_IsNotDeleted(int stage)
    {
        var step = Step();
        var snapshot = Snapshot(step with { Stage = stage });

        var changes = RegistrationDiff.Compare([Type(step) with { Steps = [] }], snapshot);

        await Assert.That(changes.HasChanges).IsFalse();
    }

    [Test]
    public async Task Compare_ImageAttributesChanged_UpdatesExistingImageOnly()
    {
        var image = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["name"]);
        var step = Step() with { Images = [image] };
        var remote = new RemotePluginStepImage(Guid.NewGuid(), image.Name, image.ImageType, ["telephone1"]);

        var changes = RegistrationDiff.Compare([Type(step)], Snapshot(step, remote));

        await Assert.That(changes.StepsToWrite).Count().IsEqualTo(0);
        await Assert.That(changes.ImagesToWrite.Single().ExistingId).IsEqualTo(remote.Id);
        await Assert.That(changes.HasChanges).IsTrue();
    }

    [Test]
    public async Task Compare_DuplicateImages_Throws()
    {
        var image = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", null);
        var step = Step() with { Images = [image, image] };

        await Assert.That(() => RegistrationDiff.Compare([Type(step)], Snapshot(step))).ThrowsExactly<InvalidPluginStepException>();
    }

    [Test]
    public async Task Compare_AmbiguousRemoteImages_Throws()
    {
        var image = new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", null);
        var step = Step() with { Images = [image] };
        var remote = new RemotePluginStepImage(Guid.NewGuid(), image.Name, image.ImageType, null);

        await Assert.That(() => RegistrationDiff.Compare([Type(step)], Snapshot(step, remote, remote with { Id = Guid.NewGuid() })))
            .ThrowsExactly<InvalidPluginStepException>();
    }

    [Test]
    public async Task Compare_RemovedImage_ProducesDeletion()
    {
        var step = Step();
        var remote = new RemotePluginStepImage(Guid.NewGuid(), "removed", 0, null);

        var changes = RegistrationDiff.Compare([Type(step)], Snapshot(step, remote));

        await Assert.That(changes.ImagesToDelete.Single().Id).IsEqualTo(remote.Id);
        await Assert.That(changes.HasChanges).IsTrue();
    }

    [Test]
    public async Task Compare_NullAndEmptyAttributeLists_AreEquivalent()
    {
        var step = Step() with { FilterAttributes = [] };

        await Assert.That(RegistrationDiff.Compare([Type(step)], Snapshot(step with { FilterAttributes = null })).HasChanges).IsFalse();
    }

    [Test]
    public async Task Compare_PluginTypeNameCasingDiffers_IsDifferentType()
    {
        var step = Step();
        var changes = RegistrationDiff.Compare([Type(step) with { TypeName = "plugin" }], Snapshot(step));

        await Assert.That(changes.TypesToCreate).Count().IsEqualTo(1);
        await Assert.That(changes.TypesToDelete).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Compare_DuplicateTypeNames_Throws()
    {
        var step = Step();

        await Assert.That(() => RegistrationDiff.Compare([Type(step), Type(step)], Snapshot(step))).ThrowsExactly<AssemblyException>();
    }
}
