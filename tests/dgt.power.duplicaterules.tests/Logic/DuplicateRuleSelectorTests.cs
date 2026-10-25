// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.duplicaterules.Logic;
using dgt.power.duplicaterules.tests.Fakes;
using Microsoft.Xrm.Sdk;

namespace dgt.power.duplicaterules.tests.Logic;

public class DuplicateRuleSelectorTests
{
    private readonly DuplicateRuleTestEnvironment _environment = new();
    private readonly EntityReference _owner;

    public DuplicateRuleSelectorTests()
    {
        _owner = _environment.AddUser("owner@example.org");
    }

    [Test]
    public async Task SelectAsync_NoSelectors_ReturnsAllRulesOrderedById()
    {
        var first = _environment.AddRule("contact", DuplicateRulePublicationState.Published, _owner);
        var second = _environment.AddRule("account", DuplicateRulePublicationState.Unpublished, _owner);

        var rules = await new DuplicateRuleSelector(_environment.Service).SelectAsync(null, []);

        await Assert.That(rules.Select(rule => rule.Id).SequenceEqual(new[] { first, second }.Order())).IsTrue();
    }

    [Test]
    public async Task SelectAsync_Entity_FiltersByBaseTable()
    {
        var contact = _environment.AddRule("contact", DuplicateRulePublicationState.Published, _owner);
        _environment.AddRule("account", DuplicateRulePublicationState.Published, _owner);

        var rules = await new DuplicateRuleSelector(_environment.Service).SelectAsync("contact", []);

        await Assert.That(rules.Select(rule => rule.Id)).IsEquivalentTo([contact]);
    }

    [Test]
    public async Task SelectAsync_RuleIds_ReturnsOnlyRequestedRules()
    {
        var requested = _environment.AddRule("contact", DuplicateRulePublicationState.Published, _owner);
        _environment.AddRule("contact", DuplicateRulePublicationState.Published, _owner);

        var rules = await new DuplicateRuleSelector(_environment.Service).SelectAsync("contact", [requested, requested]);

        await Assert.That(rules.Select(rule => rule.Id)).IsEquivalentTo([requested]);
    }

    [Test]
    public async Task SelectAsync_MissingRuleId_Fails()
    {
        var missing = Guid.NewGuid();

        await Assert.That(async () => await new DuplicateRuleSelector(_environment.Service).SelectAsync(null, [missing]))
            .Throws<InvalidOperationException>()
            .WithMessageContaining(missing.ToString("D"));
    }

    [Test]
    public async Task SelectAsync_RuleIdFromOtherTable_FailsAsConflict()
    {
        var account = _environment.AddRule("account", DuplicateRulePublicationState.Published, _owner);

        await Assert.That(async () => await new DuplicateRuleSelector(_environment.Service).SelectAsync("contact", [account]))
            .Throws<InvalidOperationException>()
            .WithMessageContaining("do not belong to base table 'contact'");
    }

    [Test]
    [Arguments(DuplicateRulePublicationState.Publishing)]
    [Arguments(DuplicateRulePublicationState.Unknown)]
    public async Task SelectAsync_UnstableRule_Fails(DuplicateRulePublicationState state)
    {
        _environment.AddRule("contact", DuplicateRulePublicationState.Published, _owner);
        _environment.AddRule("contact", state, _owner);

        await Assert.That(async () => await new DuplicateRuleSelector(_environment.Service).SelectAsync("contact", []))
            .Throws<InvalidOperationException>()
            .WithMessageContaining("not in a stable");
    }

    [Test]
    public async Task SelectAsync_NoMatches_ReturnsEmpty()
    {
        var rules = await new DuplicateRuleSelector(_environment.Service).SelectAsync("lead", []);

        await Assert.That(rules).IsEmpty();
    }
}
