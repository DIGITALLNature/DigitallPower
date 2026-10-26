// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;

namespace dgt.power.duplicaterules.Configuration;

public sealed record DuplicateRuleConfiguration(IReadOnlyList<DuplicateRuleIntent> Rules)
{
    public static DuplicateRuleConfiguration Parse(string json)
    {
        ArgumentNullException.ThrowIfNull(json);
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        EnsureNoDuplicateProperties(root, "$");
        RequireObject(root, "$");
        EnsureOnlyProperties(root, ["$schema", "schemaVersion", "rules"], "$");

        if (root.TryGetProperty("$schema", out var schema) &&
            (schema.ValueKind != JsonValueKind.String ||
             !Uri.TryCreate(schema.GetString(), UriKind.Absolute, out _)))
        {
            throw Invalid("$schema", "The optional schema reference must be an absolute URI.");
        }

        if (!root.TryGetProperty("schemaVersion", out var version) ||
            version.ValueKind != JsonValueKind.Number ||
            !version.TryGetInt32(out var schemaVersion) ||
            schemaVersion != 1)
        {
            throw Invalid("schemaVersion", "The duplicate-rule configuration requires schemaVersion 1.");
        }

        if (!root.TryGetProperty("rules", out var rules) || rules.ValueKind != JsonValueKind.Array)
        {
            throw Invalid("rules", "The rules array is required.");
        }

        var entries = new List<DuplicateRuleIntent>();
        var identifiers = new HashSet<Guid>();
        var index = 0;
        foreach (var item in rules.EnumerateArray())
        {
            var path = $"rules[{index++}]";
            RequireObject(item, path);
            EnsureOnlyProperties(item, ["ruleId", "name", "published", "owner"], path);

            if (!item.TryGetProperty("ruleId", out var ruleIdValue) ||
                ruleIdValue.ValueKind != JsonValueKind.String ||
                !Guid.TryParseExact(ruleIdValue.GetString(), "D", out var ruleId) ||
                ruleId == Guid.Empty)
            {
                throw Invalid($"{path}.ruleId", "A nonempty duplicate-rule GUID is required.");
            }

            if (!identifiers.Add(ruleId))
            {
                throw Invalid($"{path}.ruleId", $"Duplicate rule ID '{ruleId:D}'.");
            }

            string? name = null;
            if (item.TryGetProperty("name", out var nameValue))
            {
                if (nameValue.ValueKind != JsonValueKind.String)
                {
                    throw Invalid($"{path}.name", "The informational name must be a string.");
                }

                name = nameValue.GetString();
            }

            bool? published = null;
            if (item.TryGetProperty("published", out var publishedValue))
            {
                if (publishedValue.ValueKind is not (JsonValueKind.True or JsonValueKind.False))
                {
                    throw Invalid($"{path}.published", "The requested publication state must be a boolean.");
                }

                published = publishedValue.GetBoolean();
            }

            string? owner = null;
            if (item.TryGetProperty("owner", out var ownerValue))
            {
                if (ownerValue.ValueKind != JsonValueKind.String)
                {
                    throw Invalid($"{path}.owner", "The owner logical key must be a string.");
                }

                owner = ownerValue.GetString();
                if (string.IsNullOrWhiteSpace(owner) || !string.Equals(owner, owner.Trim(), StringComparison.Ordinal))
                {
                    throw Invalid($"{path}.owner", "Owner keys must be nonblank and have no surrounding whitespace.");
                }
            }

            if (published is null && owner is null)
            {
                throw Invalid(path, "Each rule must specify at least one of published or owner.");
            }

            entries.Add(new DuplicateRuleIntent(ruleId, name, published, owner));
        }

        return new DuplicateRuleConfiguration(entries);
    }

    public static DuplicateRuleConfiguration Load(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        var resolvedPath = Path.GetFullPath(path, Environment.CurrentDirectory);
        try
        {
            return Parse(File.ReadAllText(resolvedPath));
        }
        catch (DuplicateRuleConfigurationException)
        {
            throw;
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or JsonException)
        {
            throw new DuplicateRuleConfigurationException($"Could not load duplicate-rule configuration '{resolvedPath}'.", exception);
        }
    }

    private static void EnsureNoDuplicateProperties(JsonElement element, string path)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            var names = new HashSet<string>(StringComparer.Ordinal);
            foreach (var property in element.EnumerateObject())
            {
                if (!names.Add(property.Name))
                {
                    throw Invalid($"{path}.{property.Name}", "Duplicate JSON properties are not allowed.");
                }

                EnsureNoDuplicateProperties(property.Value, $"{path}.{property.Name}");
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            var index = 0;
            foreach (var item in element.EnumerateArray())
            {
                EnsureNoDuplicateProperties(item, $"{path}[{index++}]");
            }
        }
    }

    private static void EnsureOnlyProperties(JsonElement element, IReadOnlyCollection<string> allowed, string path)
    {
        foreach (var property in element.EnumerateObject())
        {
            if (!allowed.Contains(property.Name, StringComparer.Ordinal))
            {
                throw Invalid($"{path}.{property.Name}", "Unexpected configuration properties are rejected.");
            }
        }
    }

    private static void RequireObject(JsonElement element, string path)
    {
        if (element.ValueKind != JsonValueKind.Object)
        {
            throw Invalid(path, "An object is required.");
        }
    }

    private static DuplicateRuleConfigurationException Invalid(string path, string detail) =>
        new($"Invalid duplicate-rule configuration at '{path}': {detail}");
}
