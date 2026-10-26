// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using dgt.power.common.FileAccess;
using Microsoft.Crm.Sdk.Messages;

namespace dgt.power.teamtemplates.Configuration;

public sealed partial record TeamTemplateConfiguration
{
    private static readonly JsonSerializerOptions s_options = new()
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        WriteIndented = true
    };

    // Access teams share existing records, so CreateAccess is not a supported template right.
    // https://learn.microsoft.com/power-apps/developer/data-platform/use-access-teams-owner-teams-collaborate-share-information
    public const int SupportedRights = (int)(AccessRights.ReadAccess | AccessRights.WriteAccess |
        AccessRights.AppendAccess | AccessRights.AppendToAccess | AccessRights.DeleteAccess |
        AccessRights.ShareAccess | AccessRights.AssignAccess);

    [JsonPropertyName("$schema")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Schema { get; init; }

    [JsonPropertyName("schemaVersion")]
    public required int SchemaVersion { get; init; }

    [JsonPropertyName("templates")]
    public required IReadOnlyList<TeamTemplateEntry> Templates { get; init; }

    public static TeamTemplateConfiguration Parse(string json)
    {
        ArgumentNullException.ThrowIfNull(json);
        using var document = JsonDocument.Parse(json);
        CheckDuplicateProperties(document.RootElement);
        if (document.RootElement.ValueKind == JsonValueKind.Object &&
            document.RootElement.TryGetProperty("$schema", out var schema) && schema.ValueKind != JsonValueKind.String)
        {
            throw new TeamTemplateConfigurationException("$schema must be an absolute URI string.");
        }
        var configuration = JsonSerializer.Deserialize<TeamTemplateConfiguration>(json, s_options)
            ?? throw new TeamTemplateConfigurationException("A versioned teamtemplate object is required.");
        configuration.Validate();
        return configuration;
    }

    public string Serialize()
    {
        Validate();
        return JsonFileOutput.Serialize(this, s_options);
    }

    public void Validate()
    {
        if (SchemaVersion != 1 || Templates is null)
        {
            throw new TeamTemplateConfigurationException("Teamtemplates require schemaVersion 1 and a templates array.");
        }

        if (Schema is not null && !Uri.TryCreate(Schema, UriKind.Absolute, out _))
        {
            throw new TeamTemplateConfigurationException("$schema must be an absolute URI.");
        }

        var ids = new HashSet<Guid>();
        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var entry in Templates)
        {
            if (entry is null)
            {
                throw new TeamTemplateConfigurationException("Null template entries are not allowed.");
            }
            var validIdentity = entry.Id != Guid.Empty && IsTableName(entry.Table);
            var validText = !string.IsNullOrWhiteSpace(entry.Name) && entry.Name.Length <= 100 && entry.Description?.Length is not > 1500;
            var validRights = entry.Rights > 0 && (entry.Rights & ~SupportedRights) == 0;
            if (!validIdentity || !validText || !validRights)
            {
                throw new TeamTemplateConfigurationException(
                    "Each template requires a nonempty GUID, logical table name, name (1-100 characters), " +
                    "description (at most 1500 characters), and a nonzero supported access-team rights mask (no CreateAccess).");
            }

            if (!ids.Add(entry.Id) || !names.Add($"{entry.Table}\0{entry.Name}"))
            {
                throw new TeamTemplateConfigurationException($"Duplicate template GUID or table/name pair: '{entry.Id:D}', '{entry.Table}/{entry.Name}'.");
            }
        }
    }

    public static bool IsTableName(string? name) => name is not null && LogicalName().IsMatch(name);

    private static void CheckDuplicateProperties(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            var names = new HashSet<string>(StringComparer.Ordinal);
            foreach (var property in element.EnumerateObject())
            {
                if (!names.Add(property.Name))
                {
                    throw new TeamTemplateConfigurationException($"Duplicate JSON property '{property.Name}'.");
                }
                CheckDuplicateProperties(property.Value);
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                CheckDuplicateProperties(item);
            }
        }
    }

    [GeneratedRegex("^[a-z][a-z0-9_]*$", RegexOptions.CultureInvariant)]
    private static partial Regex LogicalName();
}
