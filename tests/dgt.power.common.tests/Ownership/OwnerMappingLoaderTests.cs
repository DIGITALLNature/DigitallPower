// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Ownership;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace dgt.power.common.tests.Ownership;

public class OwnerMappingLoaderTests
{
    [Test]
    public async Task Load_ExampleFromWorkingDirectory_ParsesAllSelectors()
    {
        var fixture = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "../../../Ownership/Fixtures/owner-mapping.example.json"));
        var relativePath = Path.GetRelativePath(Environment.CurrentDirectory, fixture);

        var mapping = OwnerMappingLoader.Load(relativePath);

        await Assert.That(mapping.SchemaVersion).IsEqualTo(1);
        await Assert.That(mapping.Owners.Keys).Count().IsEqualTo(5);
        await Assert.That(mapping.Owners["groupOwner"].MembershipType).IsEqualTo(OwnerMembershipType.Members);
        await Assert.That(mapping.Owners["humanOwner"].DomainName).IsEqualTo("deployment@example.org");
        await Assert.That(mapping.Owners["automationOwner"].ApplicationId).IsEqualTo(Guid.Parse("22222222-2222-2222-2222-222222222222"));
    }

    [Test]
    public async Task Parse_EmptyMappingAndMissingOptionalSchema_AreValid()
    {
        var mapping = OwnerMappingLoader.Parse("""{"schemaVersion":1,"owners":{}}""");

        await Assert.That(mapping.Schema).IsNull();
        await Assert.That(mapping.Owners).IsEmpty();
    }

    [Test]
    [Arguments("""{"schemaVersion":1.0,"owners":{}}""")]
    [Arguments("""{"schemaVersion":1e0,"owners":{}}""")]
    public async Task Parse_IntegerEquivalentJsonNumbers_AreValidSchemaVersions(string json)
    {
        var mapping = OwnerMappingLoader.Parse(json);

        await Assert.That(mapping.SchemaVersion).IsEqualTo(1);
    }

    [Test]
    [Arguments("""{"schemaVersion":1.5,"owners":{}}""")]
    [Arguments("""{"schemaVersion":"1","owners":{}}""")]
    public async Task Parse_NonIntegerSchemaVersion_IsRejected(string json)
    {
        var exception = await Assert.That(() => OwnerMappingLoader.Parse(json))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.InvalidMapping);
    }

    [Test]
    public async Task Parse_CaseSensitiveOwnerKeys_ArePreserved()
    {
        var mapping = OwnerMappingLoader.Parse("""
            {"schemaVersion":1,"owners":{
              "salesOwner":{"kind":"user","domainName":"sales@example.org"},
              "SalesOwner":{"kind":"user","entraObjectId":"11111111-1111-1111-1111-111111111111"}
            }}
            """);

        await Assert.That(mapping.Owners.Keys).Count().IsEqualTo(2);
        await Assert.That(mapping.Owners.ContainsKey("salesOwner")).IsTrue();
        await Assert.That(mapping.Owners.ContainsKey("SalesOwner")).IsTrue();
    }

    [Test]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"domainName":"a@example.org"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","entraObjectId":"00000000-0000-0000-0000-000000000000"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","entraObjectId":"11111111-1111-1111-1111-111111111111","domainName":"a@example.org"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","domainName":" a@example.org "}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","domainName":"a@example.org\n"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a\n":{"kind":"user","domainName":"a@example.org"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","domainName":" \t\n "}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"applicationUser","applicationId":"00000000-0000-0000-0000-000000000000"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"team","namePattern":"Sales-*"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"team","entraObjectId":"11111111-1111-1111-1111-111111111111","namePattern":"Sales-*"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"team","entraObjectId":"11111111-1111-1111-1111-111111111111","membershipType":"owners","businessUnitName":" BU "}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","domainName":"a@example.org","unexpected":true}}}""")]
    [Arguments("""{"schemaVersion":2,"owners":{}}""")]
    [Arguments("""{"schemaVersion":1}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"domainName":"a@example.org"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":null}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"team","entraObjectId":"11111111-1111-1111-1111-111111111111","membershipType":"unknown"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"team","entraObjectId":"11111111-1111-1111-1111-111111111111","businessUnitName":"Sales","membershipType":"owners","namePattern":"Sales-*"}}}""")]
    [Arguments("""{"$schema":null,"schemaVersion":1,"owners":{}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","domainName":null}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","entraObjectId":"{11111111-1111-1111-1111-111111111111}"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","domainName":"a@example.org"}""")]
    public async Task Parse_InvalidMapping_IsRejected(string json)
    {
        await Assert.That(() => OwnerMappingLoader.Parse(json)).Throws<OwnerResolutionException>();
    }

    [Test]
    [Arguments("""{"schemaVersion":1,"schemaVersion":1,"owners":{}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","kind":"user","domainName":"a@example.org"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"a":{"kind":"user","domainName":"a@example.org"},"a":{"kind":"user","domainName":"b@example.org"}}}""")]
    public async Task Parse_DuplicateJsonPropertiesOrOwnerKeys_AreRejected(string json)
    {
        await Assert.That(() => OwnerMappingLoader.Parse(json))
            .Throws<OwnerResolutionException>()
            .WithMessageContaining("Duplicate");
    }

    [Test]
    public async Task Load_MissingFile_FailsAsInvalidMapping()
    {
        var exception = await Assert.That(() => OwnerMappingLoader.Load($"missing-owner-{Guid.NewGuid():N}.json"))
            .Throws<OwnerResolutionException>();

        await Assert.That(exception!.Failure).IsEqualTo(OwnerResolutionFailure.InvalidMapping);
    }

    [Test]
    public async Task Schema_DeclaresV1AndStrictSelectorAlternatives()
    {
        var schemaPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../schemas/ownership/v1/schema.json"));
        using var schema = JsonDocument.Parse(File.ReadAllText(schemaPath));

        await Assert.That(schema.RootElement.GetProperty("properties").GetProperty("schemaVersion").GetProperty("const").GetInt32())
            .IsEqualTo(1);
        await Assert.That(schema.RootElement.GetProperty("additionalProperties").GetBoolean()).IsFalse();
        await Assert.That(schema.RootElement.GetProperty("properties").GetProperty("owners").GetProperty("additionalProperties").GetProperty("oneOf").GetArrayLength())
            .IsEqualTo(5);
        await Assert.That(schema.RootElement.GetProperty("required").GetArrayLength()).IsEqualTo(2);
        await Assert.That(schema.RootElement.GetProperty("$defs").GetProperty("membershipType").GetProperty("enum").GetArrayLength())
            .IsEqualTo(4);
        await Assert.That(schema.RootElement.GetProperty("$defs").GetProperty("guid").GetProperty("pattern").GetString())
            .Contains("00000000-0000-0000-0000-000000000000");
    }

    [Test]
    public async Task Schema_NonblankTrimmedPatternRejectsAnyWhitespaceAtEitherEnd()
    {
        var schemaPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../schemas/ownership/v1/schema.json"));
        using var schema = JsonDocument.Parse(File.ReadAllText(schemaPath));
        var pattern = schema.RootElement.GetProperty("$defs").GetProperty("nonblankTrimmed").GetProperty("pattern").GetString();

        await Assert.That(Regex.IsMatch("salesOwner", pattern!)).IsTrue();
        await Assert.That(Regex.IsMatch(" salesOwner", pattern!)).IsFalse();
        await Assert.That(Regex.IsMatch("salesOwner ", pattern!)).IsFalse();
        await Assert.That(Regex.IsMatch("salesOwner\n", pattern!)).IsFalse();
        await Assert.That(Regex.IsMatch("\nsalesOwner", pattern!)).IsFalse();
        await Assert.That(Regex.IsMatch(" \t\n ", pattern!)).IsFalse();
        await Assert.That(Regex.IsMatch("sales\nowner", pattern!)).IsTrue();
    }

    [Test]
    public async Task ReadmeOwnerMappingExamples_AreAcceptedByTheLoader()
    {
        var readmePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../README.md"));
        var readme = File.ReadAllText(readmePath);
        var section = readme.Split("### Portable owner mappings", 2)[1].Split("## 📚 Command Reference", 2)[0];
        var examples = Regex.Matches(section, "```json\\s*(.*?)\\s*```", RegexOptions.Singleline);

        await Assert.That(examples.Count).IsEqualTo(3);
        foreach (Match example in examples)
        {
            var mapping = OwnerMappingLoader.Parse(example.Groups[1].Value);
            await Assert.That(mapping.SchemaVersion).IsEqualTo(1);
            await Assert.That(mapping.Owners).IsNotNull();
        }
    }
}
