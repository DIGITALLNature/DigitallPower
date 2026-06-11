// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Spectre.Console;

namespace dgt.power.codegeneration.Base;

/// <summary>
///     Single entry point for code generation config loading.
///     Routes by <c>version</c> (missing or 1 → V1, 2 → V2) and validates strictly.
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
    ///     Parses a JSON config string and returns a typed result.
    /// </summary>
    public static CodeGenerationConfigResult Resolve(string json, IAnsiConsole? console = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(json);

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        // Determine version (default: 1 for backward compat with existing configs)
        var version = 1;
        if (root.TryGetProperty("version", out var versionElement) && versionElement.ValueKind == JsonValueKind.Number)
        {
            version = versionElement.GetInt32();
        }

        return version switch
        {
            1 => ResolveV1(json, console),
            2 => ResolveV2(root, json),
            _ => throw new InvalidOperationException(
                $"Unsupported config version {version}. Supported versions: 1 (legacy), 2 (typed).")
        };
    }

    /// <summary>
    ///     Reads a JSON config file from disk and resolves it.
    /// </summary>
    public static CodeGenerationConfigResult ResolveFromFile(string filePath, IAnsiConsole? console = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Config file not found: '{filePath}'.", filePath);
        }

        var json = File.ReadAllText(filePath, Encoding.UTF8);
        return Resolve(json, console);
    }

    private static CodeGenerationConfigResult.V2 ResolveV2(JsonElement root, string json)
    {
        if (!root.TryGetProperty("type", out var typeElement) || typeElement.ValueKind != JsonValueKind.String)
        {
            throw new InvalidOperationException(
                "V2 config requires a 'type' property. Set \"type\": \"dotnet\" or \"type\": \"typescript\".");
        }

        var typeValue = typeElement.GetString();
        CodeGenerationConfigBase config = typeValue switch
        {
            "dotnet" => JsonSerializer.Deserialize<DotNetCodeGenerationConfig>(json, s_options)
                        ?? throw new InvalidOperationException("Failed to deserialize V2 .NET config."),
            "typescript" => JsonSerializer.Deserialize<TypeScriptCodeGenerationConfig>(json, s_options)
                            ?? throw new InvalidOperationException("Failed to deserialize V2 TypeScript config."),
            _ => throw new InvalidOperationException(
                $"Unknown config type '{typeValue}'. Expected 'dotnet' or 'typescript'.")
        };

        return new CodeGenerationConfigResult.V2(config);
    }

    private static CodeGenerationConfigResult.V1 ResolveV1(string json, IAnsiConsole? console)
    {
        console?.MarkupLine(
            "[yellow]Warning:[/] V1 config detected. Migrate to V2 (add '\"version\": 2' and '\"type\": \"dotnet\"' or '\"type\": \"typescript\"'). " +
            "V1 support will be removed in a future major version.");

        var config = JsonSerializer.Deserialize<CodeGenerationConfig>(json, s_options)
                     ?? throw new InvalidOperationException("Failed to deserialize V1 config.");

        return new CodeGenerationConfigResult.V1(config);
    }
}
