// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;

namespace dgt.power.process.Configuration;

public sealed record ProcessConfiguration(IReadOnlyList<ProcessIntent> Processes)
{
    public static ProcessConfiguration Parse(string json)
    {
        ArgumentNullException.ThrowIfNull(json);
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        EnsureNoDuplicateProperties(root, "$");
        RequireObject(root, "$");
        EnsureOnlyProperties(root, ["$schema", "schemaVersion", "processes"], "$");

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
            throw Invalid("schemaVersion", "The process configuration requires schemaVersion 1.");
        }

        if (!root.TryGetProperty("processes", out var processes) || processes.ValueKind != JsonValueKind.Array)
        {
            throw Invalid("processes", "The processes array is required.");
        }

        var entries = new List<ProcessIntent>();
        var identifiers = new HashSet<Guid>();
        var index = 0;
        foreach (var item in processes.EnumerateArray())
        {
            var path = $"processes[{index++}]";
            RequireObject(item, path);
            EnsureOnlyProperties(item, ["processId", "name", "active", "owner"], path);

            if (!item.TryGetProperty("processId", out var processIdValue) ||
                processIdValue.ValueKind != JsonValueKind.String ||
                !Guid.TryParseExact(processIdValue.GetString(), "D", out var processId) ||
                processId == Guid.Empty)
            {
                throw Invalid($"{path}.processId", "A nonempty process GUID is required.");
            }

            if (!identifiers.Add(processId))
            {
                throw Invalid($"{path}.processId", $"Duplicate process ID '{processId:D}'.");
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

            bool? active = null;
            if (item.TryGetProperty("active", out var activeValue))
            {
                if (activeValue.ValueKind is not (JsonValueKind.True or JsonValueKind.False))
                {
                    throw Invalid($"{path}.active", "The requested state must be a boolean.");
                }

                active = activeValue.GetBoolean();
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

            if (active is null && owner is null)
            {
                throw Invalid(path, "Each process must specify at least one of active or owner.");
            }

            entries.Add(new ProcessIntent(processId, name, active, owner));
        }

        return new ProcessConfiguration(entries);
    }

    public static ProcessConfiguration Load(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        var resolvedPath = Path.GetFullPath(path, Environment.CurrentDirectory);
        try
        {
            return Parse(File.ReadAllText(resolvedPath));
        }
        catch (ProcessConfigurationException)
        {
            throw;
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or JsonException)
        {
            throw new ProcessConfigurationException($"Could not load process configuration '{resolvedPath}'.", exception);
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
                throw Invalid($"{path}.{property.Name}", "Unknown properties are not allowed.");
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

    private static ProcessConfigurationException Invalid(string path, string detail) =>
        new($"Invalid process configuration at '{path}': {detail}");
}
