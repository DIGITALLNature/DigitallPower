// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Spectre.Console;

namespace dgt.power.codegeneration.Base;

/// <summary>
///     Resolves a code generation config file to the appropriate V2 typed config.
///     Supports V1 legacy configs by detecting absence of <c>"type"</c> discriminator
///     and mapping to the new hierarchy.
/// </summary>
public static class CodeGenerationConfigFactory
{
    private static readonly JsonSerializerOptions s_options = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    ///     Reads a JSON config file and returns the appropriate <see cref="CodeGenerationConfigBase"/> subclass.
    /// </summary>
    public static CodeGenerationConfigBase Resolve(string json, IAnsiConsole? console = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(json);

        using var doc = JsonDocument.Parse(json);

        // V2: has "type" discriminator — route to concrete type manually.
        // We cannot use STJ polymorphic deserialization (JsonPolymorphic attribute) because:
        // - STJ requires the discriminator to appear before other properties, and
        // - AllowOutOfOrderMetadataProperties conflicts with "$schema" in config files.
        if (doc.RootElement.TryGetProperty("type", out var typeElement))
        {
            var typeValue = typeElement.GetString();
            return typeValue switch
            {
                "dotnet" => JsonSerializer.Deserialize<DotNetCodeGenerationConfig>(json, s_options)
                            ?? throw new InvalidOperationException("Failed to deserialize V2 .NET config."),
                "typescript" => JsonSerializer.Deserialize<TypeScriptCodeGenerationConfig>(json, s_options)
                                ?? throw new InvalidOperationException("Failed to deserialize V2 TypeScript config."),
                _ => throw new InvalidOperationException($"Unknown config type '{typeValue}'. Expected 'dotnet' or 'typescript'.")
            };
        }

        // V1: legacy flat config — map to V2
        console?.MarkupLine("[yellow]Warning:[/] V1 config format detected. Migrate to V2 typed config (add '\"type\": \"dotnet\"' or '\"type\": \"typescript\"'). V1 support will be removed in a future major version.");

#pragma warning disable CS0618 // Type or member is obsolete — intentional V1 compat
        var legacy = JsonSerializer.Deserialize<CodeGenerationConfig>(json, s_options)
                     ?? throw new InvalidOperationException("Failed to deserialize V1 legacy config.");

        // Determine which type to produce based on suppress flags
        if (!legacy.SuppressDotNet)
        {
            return legacy.ToDotNetConfig();
        }

        if (!legacy.SuppressTypeScript)
        {
            return legacy.ToTypeScriptConfig();
        }

        // Neither suppressed or both suppressed — default to dotnet
        return legacy.ToDotNetConfig();
#pragma warning restore CS0618
    }

    /// <summary>
    ///     Reads a JSON config file from disk and resolves it.
    /// </summary>
    public static CodeGenerationConfigBase ResolveFromFile(string filePath, IAnsiConsole? console = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        var json = File.ReadAllText(filePath, Encoding.UTF8);
        return Resolve(json, console);
    }
}

