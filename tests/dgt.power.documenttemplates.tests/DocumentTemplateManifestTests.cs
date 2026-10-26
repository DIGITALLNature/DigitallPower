// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using System.Text.Json.Nodes;
using dgt.power.documenttemplates.Configuration;

namespace dgt.power.documenttemplates.tests;

public class DocumentTemplateManifestTests
{
    [Test]
    public async Task Parse_ExplicitMetadataRoundTripsWithoutDefaults()
    {
        var entry = TemplateTestData.Entry("Example.docx") with { LanguageCode = 0, Description = null };
        var json = new DocumentTemplateManifest([entry]).ToJson();
        var parsed = DocumentTemplateManifest.Parse(json).Templates.Single();
        await Assert.That(parsed).IsEqualTo(entry);
        await Assert.That(json).DoesNotContain("status");
    }

    [Test]
    public async Task ToJson_NonAsciiName_IsLiteralAndEscapedInputStillParses()
    {
        var entry = TemplateTestData.Entry("Gesch\u00E4ftsbericht\u00A0<&>");
        var json = new DocumentTemplateManifest([entry]).ToJson();
        var escaped = json.Replace("\u00E4", "\\u00E4", StringComparison.Ordinal).Replace("\u00A0", "\\u00A0", StringComparison.Ordinal);

        await Assert.That(json).Contains("Gesch\u00E4ftsbericht\u00A0<&>");
        await Assert.That(json).DoesNotContain("\\u00");
        await Assert.That(DocumentTemplateManifest.Parse(json).Templates.Single()).IsEqualTo(entry);
        await Assert.That(DocumentTemplateManifest.Parse(escaped).Templates.Single()).IsEqualTo(entry);
    }

    [Test]
    [Arguments("sourceId")]
    [Arguments("name")]
    [Arguments("documentType")]
    [Arguments("entity")]
    [Arguments("file")]
    [Arguments("languageCode")]
    [Arguments("description")]
    public async Task Parse_MissingRequiredEntryPropertyRejects(string property)
    {
        var json = JsonNode.Parse(new DocumentTemplateManifest([TemplateTestData.Entry()]).ToJson())!;
        json["templates"]![0]!.AsObject().Remove(property);
        await Assert.That(() => DocumentTemplateManifest.Parse(json.ToJsonString())).Throws<InvalidDataException>();
    }

    [Test]
    [Arguments("document_status")]
    [Arguments("force_update")]
    [Arguments("ignore_missing")]
    [Arguments("status")]
    [Arguments("unknown")]
    public async Task Parse_UnknownOrLegacyEntryPropertyRejects(string property)
    {
        var json = JsonNode.Parse(new DocumentTemplateManifest([TemplateTestData.Entry()]).ToJson())!;
        json["templates"]![0]![property] = false;
        await Assert.That(() => DocumentTemplateManifest.Parse(json.ToJsonString())).Throws<InvalidDataException>();
    }

    [Test]
    [Arguments("""{}""")]
    [Arguments("""{"schemaVersion":2,"templates":[]}""")]
    [Arguments("""{"schemaVersion":"1","templates":[]}""")]
    [Arguments("""{"schemaVersion":null,"templates":[]}""")]
    [Arguments("""{"schemaVersion":1,"templates":null}""")]
    [Arguments("""{"schemaVersion":1,"templates":[],"unknown":false}""")]
    [Arguments("""{"schemaVersion":1,"schemaVersion":1,"templates":[]}""")]
    [Arguments("""{"schemaVersion":1,"templates":[],"$schema":"relative.json"}""")]
    [Arguments("""{"schemaVersion":1,"templates":[],"$schema":null}""")]
    [Arguments("""{"schemaVersion":1,"templates":[],"$schema":" https://example.org/schema"}""")]
    public async Task Parse_InvalidRootRejects(string json) =>
        await Assert.That(() => DocumentTemplateManifest.Parse(json)).Throws<InvalidDataException>();

    [Test]
    [Arguments("name", "\" padded\"")]
    [Arguments("name", "\"padded \"")]
    [Arguments("name", "\" \"")]
    [Arguments("documentType", "\"WORD\"")]
    [Arguments("documentType", "2")]
    [Arguments("sourceId", "\"00000000-0000-0000-0000-000000000000\"")]
    [Arguments("sourceId", "\"bad\"")]
    [Arguments("languageCode", "null")]
    [Arguments("languageCode", "1033.5")]
    [Arguments("description", "false")]
    public async Task Parse_InvalidEntryRejects(string property, string value)
    {
        var json = JsonNode.Parse(new DocumentTemplateManifest([TemplateTestData.Entry()]).ToJson())!;
        json["templates"]![0]![property] = JsonNode.Parse(value);
        await Assert.That(() => DocumentTemplateManifest.Parse(json.ToJsonString())).Throws<InvalidDataException>();
    }

    [Test]
    public async Task Parse_DuplicatePropertiesKeysAndIdsReject()
    {
        var entry = TemplateTestData.Entry();
        var json = new DocumentTemplateManifest([entry]).ToJson().Replace("\"description\": null", "\"description\": null, \"description\": null", StringComparison.Ordinal);
        await Assert.That(() => DocumentTemplateManifest.Parse(json)).Throws<InvalidDataException>();
        await Assert.That(() => DocumentTemplateManifest.Parse(new DocumentTemplateManifest([entry, entry with { SourceId = Guid.NewGuid(), Name = "EXAMPLE" }]).ToJson()))
            .Throws<InvalidDataException>();
        await Assert.That(() => DocumentTemplateManifest.Parse(new DocumentTemplateManifest([entry, entry with { Name = "Other" }]).ToJson()))
            .Throws<InvalidDataException>();
    }

    [Test]
    public async Task Parse_EmptyAndSchemaUriAccepted()
    {
        var parsed = DocumentTemplateManifest.Parse("""{"$schema":"https://example.org/schema","schemaVersion":1,"templates":[]}""");
        await Assert.That(parsed.Templates).IsEmpty();
    }

    [Test]
    public async Task Schema_RequiredPropertiesMatchRuntimeContract()
    {
        using var document = JsonDocument.Parse(await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "schema.json")));
        var root = document.RootElement;
        await Assert.That(root.GetProperty("additionalProperties").GetBoolean()).IsFalse();
        var template = root.GetProperty("$defs").GetProperty("template");
        await Assert.That(template.GetProperty("required").GetArrayLength()).IsEqualTo(7);
        await Assert.That(template.GetProperty("additionalProperties").GetBoolean()).IsFalse();
        await Assert.That(template.GetProperty("properties").GetProperty("description").GetProperty("type").GetArrayLength()).IsEqualTo(2);
    }
}
