// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace dgt.power.codegeneration.Base;

/// <summary>
///     Loads V2 code generation configs and validates version-specific migration rules.
/// </summary>
public static class CodeGenerationConfigLoader
{
    private const string MigrationGuide = "docs/guides/migrations/2.x-to-3.x.md";
    private static readonly string[] s_removedSettings =
    [
        "TypescriptGeneratorVersion",
        "EntityFilters",
        "EntityRefFilters",
        "EntityFormFilters"
    ];

    private static readonly JsonSerializerOptions s_options = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        AllowTrailingCommas = true,
        AllowOutOfOrderMetadataProperties = true,
        AllowDuplicateProperties = false,
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    ///     Reads a V2 configuration file and deserializes its target polymorphically.
    /// </summary>
    public static CodeGenerationConfigBase LoadFromFile(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Config file not found: '{filePath}'.", filePath);
        }

        var json = File.ReadAllText(filePath, Encoding.UTF8);
        ArgumentException.ThrowIfNullOrWhiteSpace(json);

        using var doc = JsonDocument.Parse(json, new JsonDocumentOptions { AllowTrailingCommas = true });
        var root = doc.RootElement;
        if (root.ValueKind != JsonValueKind.Object)
        {
            throw new InvalidOperationException("Code generation config must be a JSON object.");
        }

        ValidateVersion(root);
        RejectRemovedSettings(root);

        return DeserializeConfig(root)
               ?? throw new InvalidOperationException("Failed to deserialize V2 code generation config.");
    }

    private static CodeGenerationConfigBase? DeserializeConfig(JsonElement root)
    {
        var buffer = new ArrayBufferWriter<byte>();
        using (var writer = new Utf8JsonWriter(buffer))
        {
            writer.WriteStartObject();
            foreach (var property in root.EnumerateObject())
            {
                // System.Text.Json reserves dollar-prefixed properties while processing polymorphic metadata.
                if (!property.Name.Equals("$schema", StringComparison.OrdinalIgnoreCase))
                {
                    property.WriteTo(writer);
                }
            }
            writer.WriteEndObject();
        }

        try
        {
            return JsonSerializer.Deserialize<CodeGenerationConfigBase>(buffer.WrittenSpan, s_options);
        }
        catch (Exception exception) when (
            (exception is JsonException or NotSupportedException) && HasInvalidDiscriminator(root))
        {
            throw new InvalidOperationException(
                "Config requires a case-sensitive 'type' property set to 'dotnet' or 'typescript'.",
                exception);
        }
    }

    private static void ValidateVersion(JsonElement root)
    {
        if (!TryGetProperty(root, "version", out var versionElement))
        {
            throw new InvalidOperationException(
                $"An explicit config version is required. V1 and unversioned configs are no longer supported; migrate to version 2. See {MigrationGuide}.");
        }

        if (versionElement.ValueKind != JsonValueKind.Number || !versionElement.TryGetInt32(out var version))
        {
            throw new InvalidOperationException("Config property 'version' must be the integer 2.");
        }

        if (version != 2)
        {
            throw new InvalidOperationException(
                $"Unsupported config version {version}. Supported version: 2. See {MigrationGuide} for migration guidance.");
        }
    }

    private static void RejectRemovedSettings(JsonElement root)
    {
        foreach (var property in root.EnumerateObject())
        {
            if (!s_removedSettings.Contains(property.Name, StringComparer.OrdinalIgnoreCase))
            {
                continue;
            }

            var message = property.Name.Equals("TypescriptGeneratorVersion", StringComparison.OrdinalIgnoreCase)
                ? "TypeScript generation no longer has a mode selector."
                : "Attribute-level filters were removed without replacement.";
            throw new InvalidOperationException(
                $"Config setting '{property.Name}' is no longer supported. {message} See {MigrationGuide}.");
        }
    }

    private static bool TryGetProperty(JsonElement element, string propertyName, out JsonElement property)
    {
        foreach (var candidate in element.EnumerateObject())
        {
            if (string.Equals(candidate.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            {
                property = candidate.Value;
                return true;
            }
        }

        property = default;
        return false;
    }

    private static bool HasInvalidDiscriminator(JsonElement root)
    {
        var discriminators = root.EnumerateObject()
            .Where(property => property.NameEquals("type"))
            .ToArray();

        if (discriminators.Length == 0)
        {
            return true;
        }

        if (discriminators.Length > 1)
        {
            return false;
        }

        if (discriminators[0].Value.ValueKind != JsonValueKind.String)
        {
            return true;
        }

        return discriminators[0].Value.GetString() is not ("dotnet" or "typescript");
    }
}
