// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using dgt.power.common.FileAccess;

namespace dgt.power.documenttemplates.Configuration;

public sealed record DocumentTemplateManifest(IReadOnlyList<DocumentTemplateEntry> Templates)
{
    public static DocumentTemplateManifest Parse(string json)
    {
        ArgumentNullException.ThrowIfNull(json);
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        ValidateProperties(root, ["$schema", "schemaVersion", "templates"]);
        if (root.TryGetProperty("$schema", out var schema) &&
            (schema.ValueKind != JsonValueKind.String ||
             schema.GetString()!.Any(char.IsWhiteSpace) ||
             !Uri.TryCreate(schema.GetString(), UriKind.Absolute, out _)))
            throw new InvalidDataException("$schema must be an absolute URI.");
        if (!root.TryGetProperty("schemaVersion", out var version) || version.ValueKind != JsonValueKind.Number ||
            !version.TryGetInt32(out var number) || number != 1)
            throw new InvalidDataException("schemaVersion must be 1.");
        if (!root.TryGetProperty("templates", out var templates) || templates.ValueKind != JsonValueKind.Array)
            throw new InvalidDataException("templates must be an array.");
        var entries = new List<DocumentTemplateEntry>();
        var ids = new HashSet<Guid>();
        var keys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var value in templates.EnumerateArray())
        {
            ValidateProperties(value, ["sourceId", "name", "documentType", "entity", "file", "languageCode", "description"]);
            var idText = RequiredString(value, "sourceId");
            if (!Guid.TryParseExact(idText, "D", out var id) || id == Guid.Empty || !ids.Add(id))
                throw new InvalidDataException($"Invalid or duplicate sourceId '{idText}'.");
            var name = RequiredString(value, "name");
            var type = RequiredString(value, "documentType");
            if (type is not ("word" or "excel"))
                throw new InvalidDataException("documentType must be word or excel.");
            var entity = RequiredString(value, "entity");
            var file = RequiredString(value, "file");
            if (!value.TryGetProperty("languageCode", out var language) || language.ValueKind != JsonValueKind.Number ||
                !language.TryGetInt32(out var languageCode))
                throw new InvalidDataException("languageCode must be an explicit integer.");
            if (!value.TryGetProperty("description", out var description) ||
                description.ValueKind is not (JsonValueKind.String or JsonValueKind.Null))
                throw new InvalidDataException("description must be a string or explicit null.");
            var entry = new DocumentTemplateEntry(id, name, type, entity, file, languageCode, description.GetString());
            if (!keys.Add(entry.Identity))
                throw new InvalidDataException($"Duplicate template identity '{name}' ({type}, {entity}).");
            entries.Add(entry);
        }
        return new DocumentTemplateManifest(entries);
    }

    public string ToJson() => JsonFileOutput.Serialize(new
    {
        schemaVersion = 1,
        templates = Templates.Select(entry => new
        {
            sourceId = entry.SourceId, name = entry.Name, documentType = entry.DocumentType,
            entity = entry.Entity, file = entry.File, languageCode = entry.LanguageCode, description = entry.Description
        })
    }, new JsonSerializerOptions { WriteIndented = true });

    private static string RequiredString(JsonElement value, string property)
    {
        if (!value.TryGetProperty(property, out var item) || item.ValueKind != JsonValueKind.String ||
            string.IsNullOrWhiteSpace(item.GetString()) || item.GetString() != item.GetString()!.Trim())
            throw new InvalidDataException($"{property} must be a nonblank string without surrounding whitespace.");
        return item.GetString()!;
    }

    private static void ValidateProperties(JsonElement value, string[] allowed)
    {
        if (value.ValueKind != JsonValueKind.Object)
            throw new InvalidDataException("Manifest and template entries must be objects.");
        var properties = new HashSet<string>(StringComparer.Ordinal);
        foreach (var property in value.EnumerateObject())
        {
            if (!properties.Add(property.Name) || !allowed.Contains(property.Name, StringComparer.Ordinal))
                throw new InvalidDataException($"Unknown or duplicate property '{property.Name}'.");
        }
    }
}
