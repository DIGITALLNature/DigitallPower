// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Reconciliation;
using dgt.power.plugin.Exceptions;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

public class PluginStepMatcherTests
{
    private static LocalPluginStep Step() => new("desired", 0, "Update", 20, "account", "none", null, 100, []);

    private static LocalPluginType Type(params LocalPluginStep[] steps) => new("Plugin", "Plugin", string.Empty, true, steps);

    private static RemotePluginStep Remote(LocalPluginStep step) =>
        new(Guid.NewGuid(), step.Name, step.Mode, step.MessageName, step.Stage, step.PrimaryEntityName, step.SecondaryEntityName, step.FilterAttributes, step.ExecutionOrder);

    [Test]
    public async Task Match_NoCandidates_ReturnsNoMatches()
    {
        await Assert.That(PluginStepMatcher.Match(Type(Step()), [])).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Match_UniqueCandidate_PreservesExistingId()
    {
        var step = Step();
        var remote = Remote(step);

        var matches = PluginStepMatcher.Match(Type(step), [remote]);

        await Assert.That(matches[step].Id).IsEqualTo(remote.Id);
    }

    [Test]
    [Arguments("", "NONE")]
    [Arguments("none", "")]
    [Arguments("NoNe", "none")]
    public async Task Match_MessageAndEntityCasingAndUnsetEntities_Match(string localSecondary, string remoteSecondary)
    {
        var step = Step() with { SecondaryEntityName = localSecondary };
        var remote = Remote(step) with { MessageName = "UPDATE", PrimaryEntityName = "ACCOUNT", SecondaryEntityName = remoteSecondary };

        await Assert.That(PluginStepMatcher.Match(Type(step), [remote])[step].Id).IsEqualTo(remote.Id);
    }

    [Test]
    public async Task Match_NameOrderAttributesAndImagesDiffer_StillMatches()
    {
        var step = Step() with
        {
            FilterAttributes = ["name"],
            Images = [new LocalPluginStepImage(0, "PreImage", "PreImage", "Target", ["name"])]
        };
        var remote = Remote(step) with { Name = "old", ExecutionOrder = 1, FilterAttributes = ["telephone1"] };

        await Assert.That(PluginStepMatcher.Match(Type(step), [remote])[step].Id).IsEqualTo(remote.Id);
    }

    [Test]
    [Arguments("message")]
    [Arguments("stage")]
    [Arguments("mode")]
    [Arguments("primary")]
    [Arguments("secondary")]
    public async Task Match_IdentityFieldDiffers_DoesNotMatch(string field)
    {
        var step = Step();
        var remote = field switch
        {
            "message" => Remote(step) with { MessageName = "Create" },
            "stage" => Remote(step) with { Stage = 40 },
            "mode" => Remote(step) with { Mode = 1 },
            "primary" => Remote(step) with { PrimaryEntityName = "contact" },
            "secondary" => Remote(step) with { SecondaryEntityName = "contact" },
            _ => throw new ArgumentOutOfRangeException(nameof(field))
        };

        await Assert.That(PluginStepMatcher.Match(Type(step), [remote])).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Match_DuplicateDeclarationsDifferentOrder_Throws()
    {
        var step = Step();
        var duplicate = step with { Name = "another", ExecutionOrder = 200 };

        await Assert.That(() => PluginStepMatcher.Match(Type(step, duplicate), [])).ThrowsExactly<InvalidPluginStepException>();
    }

    [Test]
    public async Task Match_DuplicateDeclarationsNormalizedIdentity_Throws()
    {
        var step = Step();
        var duplicate = step with { MessageName = "UPDATE", PrimaryEntityName = "ACCOUNT", SecondaryEntityName = "" };

        await Assert.That(() => PluginStepMatcher.Match(Type(step, duplicate), [])).ThrowsExactly<InvalidPluginStepException>();
    }

    [Test]
    public async Task Match_AmbiguousCandidates_ReportsBothIds()
    {
        var step = Step();
        var first = Remote(step);
        var second = Remote(step) with { Name = "another", ExecutionOrder = 200 };

        var exception = await Assert.That(() => PluginStepMatcher.Match(Type(step), [first, second])).ThrowsExactly<InvalidPluginStepException>();

        await Assert.That(exception!.Message).Contains(first.Id.ToString());
        await Assert.That(exception.Message).Contains(second.Id.ToString());
    }

    [Test]
    public async Task Match_UnrelatedCandidates_DoNotCreateAmbiguity()
    {
        var step = Step();
        var remote = Remote(step);
        var unrelated = Remote(step) with { MessageName = "Delete" };

        await Assert.That(PluginStepMatcher.Match(Type(step), [unrelated, remote, unrelated with { Id = Guid.NewGuid() }])[step].Id).IsEqualTo(remote.Id);
    }

    [Test]
    public async Task Match_MultipleDistinctDeclarations_MatchesIndependently()
    {
        var first = Step();
        var second = first with { Name = "second", PrimaryEntityName = "contact" };
        var firstRemote = Remote(first);
        var secondRemote = Remote(second);

        var matches = PluginStepMatcher.Match(Type(first, second), [secondRemote, firstRemote]);

        await Assert.That(matches).Count().IsEqualTo(2);
        await Assert.That(matches[first].Id).IsEqualTo(firstRemote.Id);
        await Assert.That(matches[second].Id).IsEqualTo(secondRemote.Id);
    }
}
