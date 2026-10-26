// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Spectre.Console;

namespace dgt.power.common.Logic;

public class ConfigResolver(IAnsiConsole console) : IConfigResolver
{
    private readonly JsonSerializerOptions _options = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true
    };

    public bool TryGetConfigFile<TC>(string file, out TC config) where TC : class, new() => ConfigFromFileCached(file, out config);

    public bool TryConfigFromFile<T>(string file, [NotNull] out T obj) where T : new()
    {
        if (string.IsNullOrWhiteSpace(file))
        {
            obj = new T();
            return true;
        }

        try
        {
            console.WriteLine($"Read {file}");
            obj = JsonSerializer.Deserialize<T>(File.ReadAllText(file, Encoding.UTF8), _options) ?? throw new InvalidOperationException();
            return true;
        }
        catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{e.Message}[/]");
            obj = new T();
            return false;
        }
    }

    private bool ConfigFromFileCached<TC>(string file, out TC config) where TC : class, new()
    {
        if (MemoryCache.Default.Contains($"cfg-{file}"))
        {
            var cached = MemoryCache.Default.Get($"cfg-{file}") as TC;
            if (cached != null)
            {
                config = cached;
                return true;
            }
        }

        var result = TryConfigFromFile(file, out config);
        if (result)
        {
            MemoryCache.Default.Add($"cfg-{file}", config, new CacheItemPolicy { SlidingExpiration = TimeSpan.FromHours(1) });
        }

        return result;
    }
}
