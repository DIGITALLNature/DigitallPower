// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace dgt.power.common.Logic;

public class ConfigResolver : IConfigResolver
{
    private readonly JsonSerializerOptions _options = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        AllowTrailingCommas = true
    };

    private readonly ITracer _tracer;

    public ConfigResolver(ITracer tracer) => _tracer = tracer;

    public bool GetConfigFile<TC>(string fileDir, string file, out TC config) where TC : new() => ConfigFromFileCached(Path.Combine(fileDir, file), out config);

    public bool GetConfigFile<TC>(string file, out TC config) where TC : new() => ConfigFromFileCached(file, out config);

    public bool ConfigFromFile<T>(string file, [NotNull] out T obj) where T : new()
    {
        if (string.IsNullOrWhiteSpace(file))
        {
            obj = new T();
            return true;
        }

        try
        {
            _tracer.Log($"Read {file} ", TraceEventType.Information);
            obj = JsonSerializer.Deserialize<T>(File.ReadAllText(file, Encoding.UTF8), _options) ?? throw new InvalidOperationException();
            return true;
        }
        catch (Exception e)
        {
            _tracer.Log(e.Message, TraceEventType.Error);
            obj = new T();
            return false;
        }
    }

    private bool ConfigFromFileCached<TC>(string file, out TC config) where TC : new()
    {
        if (MemoryCache.Default.Contains($"cfg-{file}"))
        {
            var cached = (TC)MemoryCache.Default.Get($"cfg-{file}");
            if (cached != null)
            {
                config = cached;
                return true;
            }
        }

        var result = ConfigFromFile(file, out config);
        if (result)
        {
            MemoryCache.Default.Add($"cfg-{file}", config, new CacheItemPolicy { SlidingExpiration = TimeSpan.FromHours(1) });
        }

        return result;
    }
}
