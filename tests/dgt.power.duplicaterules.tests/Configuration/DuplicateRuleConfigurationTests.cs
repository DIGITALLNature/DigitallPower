// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using dgt.power.duplicaterules.Configuration;

namespace dgt.power.duplicaterules.tests.Configuration;

public class DuplicateRuleConfigurationTests
{
    [Test]
    public async Task Parse_ReadmeExampleFixture_AcceptsStrictV1AndSchema()
    {
        var json = await File.ReadAllTextAsync(
            Path.Combine(AppContext.BaseDirectory, "Resources", "DuplicateRuleConfiguration", "valid-v1.json"));

        var configuration = DuplicateRuleConfiguration.Parse(json);

        await Assert.That(configuration.Rules).Count().IsEqualTo(2);
        await Assert.That(configuration.Rules[0].RuleId).IsEqualTo(Guid.Parse("6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d"));
        await Assert.That(configuration.Rules[0].Name).IsEqualTo("Contacts with the same e-mail address");
        await Assert.That(configuration.Rules[0].Published).IsTrue();
        await Assert.That(configuration.Rules[0].Owner).IsEqualTo("integrationUser");
        await Assert.That(configuration.Rules[1].Published).IsFalse();
        await Assert.That(configuration.Rules[1].Owner).IsNull();

        using var schema = JsonDocument.Parse(await File.ReadAllTextAsync(
            Path.Combine(AppContext.BaseDirectory, "schemas", "duplicaterules", "v1", "schema.json")));
        await Assert.That(schema.RootElement.GetProperty("properties").GetProperty("schemaVersion").GetProperty("const").GetInt32())
            .IsEqualTo(1);
        await Assert.That(schema.RootElement.GetProperty("$defs").GetProperty("rule").GetProperty("properties")
            .GetProperty("ruleId").GetProperty("format").GetString()).IsEqualTo("uuid");
    }

    [Test]
    public async Task Parse_EmptyRulesArray_IsValid()
    {
        var configuration = DuplicateRuleConfiguration.Parse("""{"schemaVersion":1,"rules":[]}""");

        await Assert.That(configuration.Rules).IsEmpty();
    }

    [Test]
    [Arguments("""{"schemaVersion":1,"rules":[{"ruleId":"6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d","name":"No intent"}]}""")]
    [Arguments("""{"schemaVersion":1,"rules":[{"ruleId":"6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d","published":null}]}""")]
    [Arguments("""{"schemaVersion":1,"rules":[{"ruleId":"6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d","published":"true"}]}""")]
    [Arguments("""{"schemaVersion":1,"rules":[{"ruleId":"6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d","owner":null}]}""")]
    [Arguments("""{"schemaVersion":1,"rules":[{"ruleId":"6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d","owner":" integrationUser"}]}""")]
    [Arguments("""{"schemaVersion":1,"rules":[{"ruleId":"6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d","name":1,"published":true}]}""")]
    [Arguments("""{"schemaVersion":1,"rules":[{"ruleId":"6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d","baseTable":"contact","published":true}]}""")]
    [Arguments("""{"schemaVersion":1,"rules":[{"name":"Missing id","published":true}]}""")]
    [Arguments("""{"schemaVersion":1,"owner":"default","rules":[]}""")]
    [Arguments("""{"schemaVersion":2,"rules":[]}""")]
    [Arguments("""{"rules":[]}""")]
    [Arguments("""{"schemaVersion":1}""")]
    [Arguments("""{"schemaVersion":1,"rules":[{"ruleId":"00000000-0000-0000-0000-000000000000","published":true}]}""")]
    [Arguments("""{"schemaVersion":1,"rules":[{"ruleId":"6f1c2b4e8d3a4f5b9c7d1e2f3a4b5c6d","published":true}]}""")]
    [Arguments("""{"$schema":"relative/schema.json","schemaVersion":1,"rules":[]}""")]
    public async Task Parse_InvalidV1Document_Rejects(string json)
    {
        await Assert.That(() => DuplicateRuleConfiguration.Parse(json)).Throws<DuplicateRuleConfigurationException>();
    }

    [Test]
    public async Task Parse_DuplicateRuleIds_Rejects()
    {
        const string json = """
        {"schemaVersion":1,"rules":[
          {"ruleId":"6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d","published":true},
          {"ruleId":"6F1C2B4E-8D3A-4F5B-9C7D-1E2F3A4B5C6D","owner":"integrationUser"}
        ]}
        """;

        await Assert.That(() => DuplicateRuleConfiguration.Parse(json)).Throws<DuplicateRuleConfigurationException>();
    }

    [Test]
    public async Task Parse_DuplicateJsonProperty_Rejects()
    {
        const string json = """{"schemaVersion":1,"rules":[{"ruleId":"6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d","published":true,"published":false}]}""";

        await Assert.That(() => DuplicateRuleConfiguration.Parse(json)).Throws<DuplicateRuleConfigurationException>();
    }

    [Test]
    public async Task Load_MissingFile_ReportsPath()
    {
        var path = Path.Combine(AppContext.BaseDirectory, $"missing-{Guid.NewGuid():N}.json");

        await Assert.That(() => DuplicateRuleConfiguration.Load(path))
            .Throws<DuplicateRuleConfigurationException>()
            .WithMessageContaining(path);
    }
}
