// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using dgt.power.teamtemplates.Configuration;

namespace dgt.power.teamtemplates.tests.Configuration;

public class TeamTemplateConfigurationTests
{
    [Test]
    public async Task Parse_Example_RoundTrips()
    {
        var json = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "Resources", "valid-v1.json"));
        var configuration = TeamTemplateConfiguration.Parse(json);
        var roundTrip = TeamTemplateConfiguration.Parse(configuration.Serialize());
        await Assert.That(roundTrip.Templates[0]).IsEqualTo(configuration.Templates[0]);
        await Assert.That(roundTrip.Templates[0].Id).IsEqualTo(Guid.Parse("4e17461c-efec-43a0-835b-08fa78af1f83"));
        await Assert.That(roundTrip.Templates[0].Description).IsNull();
        using var serialized = JsonDocument.Parse(configuration.Serialize());
        await Assert.That(serialized.RootElement.TryGetProperty("$schema", out _)).IsFalse();
        await Assert.That(serialized.RootElement.GetProperty("templates")[0].GetProperty("defaultaccessrightsmask").GetInt32()).IsEqualTo(1);
    }

    [Test]
    [Arguments("[]")]
    [Arguments("null")]
    [Arguments("""{"schemaVersion":2,"templates":[]}""")]
    [Arguments("""{"templates":[]}""")]
    [Arguments("""{"schemaVersion":1,"templates":null}""")]
    [Arguments("""{"schemaVersion":1,"templates":[null]}""")]
    [Arguments("""{"schemaVersion":1,"templates":[],"unknown":true}""")]
    [Arguments("""{"schemaVersion":1,"schemaVersion":1,"templates":[]}""")]
    [Arguments("""{"schemaVersion":1,"templates":[],"$schema":"not a uri"}""")]
    [Arguments("""{"schemaVersion":1,"templates":[],"$schema":null}""")]
    public async Task Parse_InvalidEnvelope_Rejects(string json)
    {
        await Assert.That(() => TeamTemplateConfiguration.Parse(json)).Throws<Exception>();
    }

    [Test]
    [Arguments("teamtemplateid", "\"00000000-0000-0000-0000-000000000000\"")]
    [Arguments("teamtemplateid", "null")]
    [Arguments("teamtemplateid", "\"not-a-guid\"")]
    [Arguments("entity", "\"*\"")]
    [Arguments("entity", "\" account\"")]
    [Arguments("teamtemplatename", "\" \"")]
    [Arguments("teamtemplatename", "null")]
    [Arguments("defaultaccessrightsmask", "0")]
    [Arguments("defaultaccessrightsmask", "32")]
    [Arguments("defaultaccessrightsmask", "-1")]
    [Arguments("defaultaccessrightsmask", "8")]
    [Arguments("defaultaccessrightsmask", "1.5")]
    [Arguments("defaultaccessrightsmask", "null")]
    [Arguments("description", "1")]
    [Arguments("issystem", "false")]
    public async Task Parse_InvalidField_Rejects(string property, string value)
    {
        var entry = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(EntryJson())!;
        entry[property] = JsonDocument.Parse(value).RootElement.Clone();
        var json = $$"""{"schemaVersion":1,"templates":[{{JsonSerializer.Serialize(entry)}}]}""";
        await Assert.That(() => TeamTemplateConfiguration.Parse(json)).Throws<Exception>();
    }

    [Test]
    [Arguments("teamtemplateid")]
    [Arguments("entity")]
    [Arguments("teamtemplatename")]
    [Arguments("defaultaccessrightsmask")]
    public async Task Parse_MissingRequiredField_Rejects(string property)
    {
        var entry = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(EntryJson())!;
        entry.Remove(property);
        await Assert.That(() => TeamTemplateConfiguration.Parse(
            $$"""{"schemaVersion":1,"templates":[{{JsonSerializer.Serialize(entry)}}]}""")).Throws<JsonException>();
    }

    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task Parse_DuplicateIdentity_Rejects(bool sameId)
    {
        var first = EntryJson();
        var second = sameId ? first.Replace("Readers", "Writers", StringComparison.Ordinal)
            : first.Replace("4e17461c-efec-43a0-835b-08fa78af1f83", Guid.NewGuid().ToString("D"), StringComparison.Ordinal);
        await Assert.That(() => TeamTemplateConfiguration.Parse(
            $$"""{"schemaVersion":1,"templates":[{{first}},{{second}}]}""")).Throws<TeamTemplateConfigurationException>();
    }

    [Test]
    public async Task Parse_OmittedDescription_IsNull()
    {
        var configuration = TeamTemplateConfiguration.Parse($$"""{"schemaVersion":1,"templates":[{{EntryJson()}}]}""");
        await Assert.That(configuration.Templates[0].Description).IsNull();
    }

    [Test]
    public async Task Rights_SchemaExactlyMatchesSupportedBitSet()
    {
        using var schema = JsonDocument.Parse(await File.ReadAllTextAsync(
            Path.Combine(AppContext.BaseDirectory, "schemas", "teamtemplates", "v1", "schema.json")));
        var ranges = schema.RootElement.GetProperty("$defs").GetProperty("template").GetProperty("properties")
            .GetProperty("defaultaccessrightsmask").GetProperty("anyOf").EnumerateArray()
            .Select(range => (Min: range.GetProperty("minimum").GetInt32(), Max: range.GetProperty("maximum").GetInt32())).ToArray();
        var values = ranges.SelectMany(range => Enumerable.Range(range.Min, range.Max - range.Min + 1)).ToHashSet();
        var valid = Enumerable.Range(1, TeamTemplateConfiguration.SupportedRights)
            .Where(mask => (mask & ~TeamTemplateConfiguration.SupportedRights) == 0).ToHashSet();
        await Assert.That(values.SetEquals(valid)).IsTrue();
        foreach (var mask in valid)
        {
            var json = $$"""{"schemaVersion":1,"templates":[{{EntryJson().Replace("\"defaultaccessrightsmask\":1", $"\"defaultaccessrightsmask\":{mask}", StringComparison.Ordinal)}}]}""";
            TeamTemplateConfiguration.Parse(json);
        }
    }

    private const string Entry = """{"teamtemplateid":"4e17461c-efec-43a0-835b-08fa78af1f83","entity":"account","teamtemplatename":"Readers","defaultaccessrightsmask":1}""";

    private static string EntryJson() => Entry;
}
