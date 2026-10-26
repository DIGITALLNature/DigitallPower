// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json.Nodes;
using dgt.power.documenttemplates.Configuration;
using Json.Schema;

namespace dgt.power.documenttemplates.tests;

public class DocumentTemplateSchemaTests
{
    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Schema_ExportedEmptyAndPopulatedManifestsValidate(bool populated)
    {
        var schema = JsonSchema.FromText(await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "schema.json")));
        var manifest = new DocumentTemplateManifest(populated ? [TemplateTestData.Entry()] : []);
        var node = JsonNode.Parse(manifest.ToJson());
        var result = schema.Evaluate(node, new EvaluationOptions { RequireFormatValidation = true });
        await Assert.That(result.IsValid).IsTrue();
    }

    [Test]
    [Arguments("sourceId", "\"00000000-0000-0000-0000-000000000000\"")]
    [Arguments("sourceId", "\"bad\"")]
    [Arguments("name", "\" padded\"")]
    [Arguments("name", "\"trailing \"")]
    [Arguments("documentType", "\"WORD\"")]
    [Arguments("languageCode", "null")]
    [Arguments("languageCode", "1.5")]
    [Arguments("description", "false")]
    [Arguments("file", "\"/outside.docx\"")]
    public async Task Schema_InvalidEntryRejected(string property, string json)
    {
        var schema = JsonSchema.FromText(await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "schema.json")));
        var node = JsonNode.Parse(new DocumentTemplateManifest([TemplateTestData.Entry()]).ToJson())!;
        node["templates"]![0]![property] = JsonNode.Parse(json);
        await Assert.That(schema.Evaluate(node, new EvaluationOptions { RequireFormatValidation = true }).IsValid).IsFalse();
    }

    [Test]
    [Arguments("sourceId")]
    [Arguments("name")]
    [Arguments("documentType")]
    [Arguments("entity")]
    [Arguments("file")]
    [Arguments("languageCode")]
    [Arguments("description")]
    public async Task Schema_RequiredEntryPropertiesCannotBeOmitted(string property)
    {
        var schema = JsonSchema.FromText(await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "schema.json")));
        var node = JsonNode.Parse(new DocumentTemplateManifest([TemplateTestData.Entry()]).ToJson())!;
        node["templates"]![0]!.AsObject().Remove(property);
        await Assert.That(schema.Evaluate(node).IsValid).IsFalse();
    }

    [Test]
    [Arguments("""{"schemaVersion":1,"templates":[],"$schema":"relative.json"}""")]
    [Arguments("""{"schemaVersion":2,"templates":[]}""")]
    [Arguments("""{"schemaVersion":1,"templates":[],"status":false}""")]
    public async Task Schema_UnknownPropertiesVersionsAndRelativeSchemaUrisReject(string json)
    {
        var schema = JsonSchema.FromText(await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "schema.json")));
        await Assert.That(schema.Evaluate(JsonNode.Parse(json), new EvaluationOptions { RequireFormatValidation = true }).IsValid).IsFalse();
    }
}
