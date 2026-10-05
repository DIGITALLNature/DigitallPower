// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace dgt.power.webresource.Local;

public sealed class WebResourceMappingFile
{
    [JsonRequired]
    [JsonPropertyName("mappings")]
    public Dictionary<string, string> Mappings { get; init; } = new(StringComparer.Ordinal);

    public static WebResourceMappingFile Load(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);

        if (!File.Exists(path))
        {
            throw new WebResourceMappingException(path, "the file was not found");
        }

        try
        {
            using var stream = File.OpenRead(path);
            var result = JsonSerializer.Deserialize<WebResourceMappingFile>(stream);
            if (result is null)
            {
                throw new WebResourceMappingException(path, "the file is empty");
            }

            if (result.Mappings is null)
            {
                throw new WebResourceMappingException(path, "'mappings' must be a JSON object and cannot be null");
            }

            return result;
        }
        catch (JsonException exception)
        {
            throw new WebResourceMappingException(
                path,
                "the file must be valid JSON with a non-null 'mappings' object",
                exception);
        }
    }
}
