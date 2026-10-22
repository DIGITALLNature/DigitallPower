// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using System.Text.Json.Serialization;

namespace dgt.power.common.Ownership;

public static class OwnerMappingLoader
{
    private static readonly JsonSerializerOptions s_jsonOptions = CreateJsonOptions();

    public static OwnerMapping Load(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        var resolvedPath = Path.GetFullPath(path, Environment.CurrentDirectory);

        try
        {
            return Parse(File.ReadAllText(resolvedPath));
        }
        catch (OwnerResolutionException)
        {
            throw;
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            throw new OwnerResolutionException(
                OwnerResolutionFailure.InvalidMapping,
                $"Could not load owner mapping file '{resolvedPath}'.",
                selector: resolvedPath,
                innerException: exception);
        }
    }

    public static OwnerMapping Parse(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        try
        {
            using var document = JsonDocument.Parse(json);
            EnsureNoDuplicateProperties(document.RootElement, "$");
            ValidateRequiredDiscriminators(document.RootElement);
            var model = JsonSerializer.Deserialize<OwnerMappingDocument>(json, s_jsonOptions)
                ?? throw Invalid("$", "The mapping document cannot be null.");
            var owners = new Dictionary<string, OwnerMappingEntry>(StringComparer.Ordinal);
            if (model.Owners is null)
            {
                throw Invalid("owners", "The owners object is required.");
            }

            foreach (var (key, entry) in model.Owners)
            {
                owners.Add(key, entry!);
            }

            var mapping = new OwnerMapping(ParseSchemaVersion(model.SchemaVersion), owners, model.Schema);
            OwnerMappingValidator.Validate(mapping);
            return mapping;
        }
        catch (OwnerResolutionException)
        {
            throw;
        }
        catch (Exception exception) when (exception is JsonException or ArgumentException or NotSupportedException)
        {
            throw new OwnerResolutionException(
                OwnerResolutionFailure.InvalidMapping,
                "The owner mapping is malformed or contains an unsupported value.",
                selector: exception is JsonException jsonException ? jsonException.Path : null,
                innerException: exception);
        }
    }

    private static void EnsureNoDuplicateProperties(JsonElement element, string path)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            var propertyNames = new HashSet<string>(StringComparer.Ordinal);
            foreach (var property in element.EnumerateObject())
            {
                if (!propertyNames.Add(property.Name))
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

    private static void ValidateRequiredDiscriminators(JsonElement root)
    {
        if (root.ValueKind == JsonValueKind.Object &&
            root.TryGetProperty("$schema", out var schema) &&
            (schema.ValueKind != JsonValueKind.String ||
             !Uri.TryCreate(schema.GetString(), UriKind.Absolute, out _)))
        {
            throw Invalid("$schema", "The optional schema reference must be an absolute URI string.");
        }

        if (root.ValueKind != JsonValueKind.Object ||
            !root.TryGetProperty("owners", out var owners) ||
            owners.ValueKind != JsonValueKind.Object)
        {
            return;
        }

        foreach (var owner in owners.EnumerateObject())
        {
            var path = $"owners.{owner.Name}";
            if (owner.Value.ValueKind != JsonValueKind.Object ||
                !owner.Value.TryGetProperty("kind", out var kind) ||
                kind.ValueKind != JsonValueKind.String)
            {
                throw Invalid($"{path}.kind", "Each owner entry requires a kind string.");
            }

            if (kind.GetString() is not ("user" or "applicationUser" or "team"))
            {
                throw Invalid($"{path}.kind", "The kind must be user, applicationUser, or team.");
            }

            foreach (var property in owner.Value.EnumerateObject())
            {
                if (property.Value.ValueKind == JsonValueKind.Null)
                {
                    throw Invalid($"{path}.{property.Name}", "Selector properties cannot be null.");
                }

                if (property.Name is "entraObjectId" or "applicationId" &&
                    (property.Value.ValueKind != JsonValueKind.String ||
                     !Guid.TryParseExact(property.Value.GetString(), "D", out var id) ||
                     id == Guid.Empty))
                {
                    throw Invalid($"{path}.{property.Name}", "GUID selectors must be nonempty canonical GUID strings.");
                }
            }

            if (owner.Value.TryGetProperty("membershipType", out var membership) &&
                (membership.ValueKind != JsonValueKind.String ||
                 membership.GetString() is not ("membersAndGuests" or "members" or "owners" or "guests")))
            {
                throw Invalid($"{path}.membershipType", "The membership type is not supported.");
            }
        }
    }

    private static OwnerResolutionException Invalid(string selector, string detail) =>
        new(OwnerResolutionFailure.InvalidMapping, $"Invalid owner mapping at '{selector}': {detail}", selector: selector);

    private static int ParseSchemaVersion(JsonElement value)
    {
        if (value.ValueKind != JsonValueKind.Number ||
            !value.TryGetDecimal(out var version) ||
            version != decimal.Truncate(version) ||
            version is < int.MinValue or > int.MaxValue)
        {
            throw Invalid("schemaVersion", "The schema version must be an integer.");
        }

        return decimal.ToInt32(version);
    }

    private static JsonSerializerOptions CreateJsonOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
        };
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }

    private sealed record OwnerMappingDocument(
        [property: JsonPropertyName("$schema")] string? Schema,
        JsonElement SchemaVersion,
        Dictionary<string, OwnerMappingEntry?>? Owners);
}
