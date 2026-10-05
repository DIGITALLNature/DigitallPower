// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;

namespace dgt.power.webresource.Local;

public static class WebResourceDiscovery
{
    public static IReadOnlyList<LocalWebResource> Discover(
        string target,
        string? publisherPrefix = null,
        IReadOnlyDictionary<string, string>? mappings = null,
        string? singleResourceName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(target);

        if (File.Exists(target))
        {
            if (singleResourceName is null)
            {
                throw new ArgumentException(
                    "A single-file target requires --name.", nameof(singleResourceName));
            }

            if (!WebResourceTypeResolver.TryResolve(target, out var type))
            {
                throw new ArgumentException(
                    $"Unsupported webresource file extension: '{Path.GetExtension(target)}'.", nameof(target));
            }

            return [ReadFile(target, singleResourceName, type, Path.GetFileName(target))];
        }

        if (!Directory.Exists(target))
        {
            throw new WebResourceTargetNotFoundException(target);
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(publisherPrefix);

        var root = Path.GetFullPath(target);
        Dictionary<string, string>? normalizedMappings = null;
        if (mappings is not null)
        {
            normalizedMappings = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var (key, value) in mappings)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(key);
                var normalizedKey = NormalizePath(key);
                if (!normalizedMappings.TryAdd(normalizedKey, value))
                {
                    throw new WebResourceMappingException(
                        $"Multiple mapping entries resolve to the same path '{normalizedKey}'.");
                }
            }
        }

        var usedMappings = new HashSet<string>(StringComparer.Ordinal);
        var resources = new List<LocalWebResource>();
        foreach (var file in Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories)
                     .OrderBy(path => path, StringComparer.OrdinalIgnoreCase))
        {
            if (!WebResourceTypeResolver.TryResolve(file, out var type))
            {
                continue;
            }

            var relativePath = NormalizePath(Path.GetRelativePath(root, file));
            var name = ResolveName(relativePath, publisherPrefix, normalizedMappings, usedMappings);
            resources.Add(ReadFile(file, name, type, relativePath));
        }

        if (normalizedMappings is not null)
        {
            var unmatchedMappings = normalizedMappings.Keys
                .Except(usedMappings, StringComparer.Ordinal)
                .Order(StringComparer.Ordinal)
                .ToArray();
            if (unmatchedMappings.Length > 0)
            {
                throw new WebResourceMappingException(
                    $"Mapping entries do not match supported files under '{root}': {string.Join(", ", unmatchedMappings)}.");
            }
        }

        var duplicates = resources
            .GroupBy(resource => resource.Name, StringComparer.OrdinalIgnoreCase)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToArray();
        if (duplicates.Length > 0)
        {
            throw new ArgumentException(
                $"Multiple files resolve to the same webresource name: {string.Join(", ", duplicates)}.",
                nameof(target));
        }

        return resources;
    }

    private static string ResolveName(
        string relativePath,
        string publisherPrefix,
        Dictionary<string, string>? mappings,
        HashSet<string> usedMappings)
    {
        if (mappings is not null && mappings.TryGetValue(relativePath, out var mappedName))
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(mappedName);
            usedMappings.Add(relativePath);
            return mappedName;
        }

        var prefixPath = $"{publisherPrefix}_/";
        return relativePath.StartsWith(prefixPath, StringComparison.OrdinalIgnoreCase)
            ? relativePath
            : prefixPath + relativePath;
    }

    private static LocalWebResource ReadFile(string path, string name, int type, string relativePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        var normalizedName = NormalizePath(name);
        var content = File.ReadAllBytes(path);
        return new LocalWebResource(
            type,
            normalizedName,
            Path.GetFileName(normalizedName),
            Convert.ToBase64String(content),
            Convert.ToHexString(SHA256.HashData(content)),
            relativePath);
    }

    private static string NormalizePath(string path) => path.Replace('\\', '/');
}
