// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.dataverse;
using dgt.power.duplicaterules.Configuration;
using dgt.power.duplicaterules.Logic;
using Microsoft.Xrm.Sdk;

namespace dgt.power.duplicaterules.tests.Logic;

public class DuplicateRuleConfigurationWriterTests
{
    private static readonly Guid FirstId = Guid.Parse("0a9b8c7d-6e5f-4a3b-2c1d-0e9f8a7b6c5d");
    private static readonly Guid SecondId = Guid.Parse("6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d");

    [Test]
    [Arguments("state", true, false)]
    [Arguments("owner", false, true)]
    [Arguments("both", true, true)]
    public async Task Serialize_Capture_RoundTripsThroughStrictParser(string capture, bool expectState, bool expectOwner)
    {
        var owners = new Dictionary<Guid, string> { [FirstId] = "integrationUser", [SecondId] = "dupTeam" };

        var json = DuplicateRuleConfigurationWriter.Serialize([Rule(SecondId, published: true), Rule(FirstId, published: false)], capture, owners);
        var configuration = DuplicateRuleConfiguration.Parse(json);

        await Assert.That(json).Contains("schemas/duplicaterules/v1/schema.json");
        await Assert.That(configuration.Rules.Select(rule => rule.RuleId)).IsEquivalentTo([FirstId, SecondId]);
        await Assert.That(configuration.Rules[0].RuleId).IsEqualTo(FirstId);
        await Assert.That(configuration.Rules[0].Name).IsEqualTo($"Rule {FirstId:D}");
        await Assert.That(configuration.Rules[0].Published).IsEqualTo(expectState ? false : null);
        await Assert.That(configuration.Rules[1].Published).IsEqualTo(expectState ? true : null);
        await Assert.That(configuration.Rules[1].Owner).IsEqualTo(expectOwner ? "dupTeam" : null);
    }

    [Test]
    public async Task Serialize_Empty_WritesEmptyRulesArray()
    {
        var json = DuplicateRuleConfigurationWriter.Serialize([], "state", new Dictionary<Guid, string>());

        await Assert.That(DuplicateRuleConfiguration.Parse(json).Rules).IsEmpty();
    }

    [Test]
    public async Task Serialize_OwnerCaptureWithoutKey_Fails()
    {
        await Assert.That(() => DuplicateRuleConfigurationWriter.Serialize([Rule(FirstId, published: true)], "owner", new Dictionary<Guid, string>()))
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Serialize_OwnerlessRules_OmitOwnerOrEntry()
    {
        var owners = new Dictionary<Guid, string> { [FirstId] = "integrationUser" };
        var ownerlessIds = new HashSet<Guid> { SecondId };
        var rules = new[] { Rule(FirstId, published: true), Rule(SecondId, published: false) };

        var both = DuplicateRuleConfiguration.Parse(DuplicateRuleConfigurationWriter.Serialize(rules, "both", owners, ownerlessIds));
        var ownerOnly = DuplicateRuleConfiguration.Parse(DuplicateRuleConfigurationWriter.Serialize(rules, "owner", owners, ownerlessIds));

        await Assert.That(both.Rules).Count().IsEqualTo(2);
        await Assert.That(both.Rules[1].Owner).IsNull();
        await Assert.That(both.Rules[1].Published).IsFalse();
        await Assert.That(ownerOnly.Rules).Count().IsEqualTo(1);
        await Assert.That(ownerOnly.Rules[0].RuleId).IsEqualTo(FirstId);
    }

    [Test]
    public async Task WriteAtomicallyAsync_ExistingFileWithoutOverwrite_PreservesContent()
    {
        var directory = Path.Combine(AppContext.BaseDirectory, $"writer-{Guid.NewGuid():N}");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "existing.json");
        await File.WriteAllTextAsync(path, "preserve");
        try
        {
            await Assert.That(() => DuplicateRuleConfigurationWriter.WriteAtomicallyAsync(path, "new", false, CancellationToken.None))
                .Throws<IOException>();
            await DuplicateRuleConfigurationWriter.WriteAtomicallyAsync(path, "replaced", true, CancellationToken.None);

            await Assert.That(await File.ReadAllTextAsync(path)).IsEqualTo("replaced");
            await Assert.That(Directory.EnumerateFiles(directory, "*.tmp")).IsEmpty();
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }

    private static Entity Rule(Guid id, bool published) => new(DuplicateRule.EntityLogicalName, id)
    {
        ["name"] = $"Rule {id:D}",
        ["baseentityname"] = "contact",
        ["statecode"] = new OptionSetValue(published ? DuplicateRule.Options.StateCode.Active : DuplicateRule.Options.StateCode.Inactive),
        ["statuscode"] = new OptionSetValue(published ? DuplicateRule.Options.StatusCode.Published : DuplicateRule.Options.StatusCode.Unpublished)
    };
}
