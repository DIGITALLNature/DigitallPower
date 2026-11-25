// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace dgt.power.webresource.Scaffolding;

internal static partial class WebResourceTemplateCustomizer
{
    private static readonly UTF8Encoding StrictUtf8 = new(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
    private static readonly HashSet<string> JavaScriptExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".js",
        ".mjs",
        ".cjs",
        ".ts",
        ".tsx",
        ".jsx"
    };

    private static readonly IReadOnlyDictionary<string, Func<WebResourceProjectValues, string>> Replacements =
        new Dictionary<string, Func<WebResourceProjectValues, string>>(StringComparer.Ordinal)
        {
            ["__DGTP_PROJECT_NAME__"] = values => values.Name,
            ["__DGTP_GLOBAL_NAME__"] = values => values.Global,
            ["__DGTP_SOLUTION__"] = values => values.Solution ?? string.Empty,
            ["__DGTP_PUBLISHER_PREFIX__"] = values => values.PublisherPrefix ?? string.Empty
        };

    public static Task CustomizeAsync(
        string stagingDirectory,
        WebResourceProjectValues values,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(stagingDirectory);
        ArgumentNullException.ThrowIfNull(values);
        return CustomizeCoreAsync(stagingDirectory, values, cancellationToken);
    }

    private static async Task CustomizeCoreAsync(
        string stagingDirectory,
        WebResourceProjectValues values,
        CancellationToken cancellationToken)
    {
        var files = new List<(string Path, string Content)>();
        var foundMarkers = new HashSet<string>(StringComparer.Ordinal);
        foreach (var path in Directory.EnumerateFiles(stagingDirectory, "*", SearchOption.AllDirectories))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var content = await ReadUtf8TextAsync(path, cancellationToken);
            if (content is null)
            {
                continue;
            }

            files.Add((path, content));
            foreach (var marker in Replacements.Keys)
            {
                if (content.Contains(marker, StringComparison.Ordinal))
                {
                    foundMarkers.Add(marker);
                }
            }
        }

        var missingMarkers = Replacements.Keys
            .Where(marker => !foundMarkers.Contains(marker))
            .Order(StringComparer.Ordinal)
            .ToArray();
        if (missingMarkers.Length > 0)
        {
            throw new WebResourceScaffoldingException(
                $"The template is missing required customization markers: {string.Join(", ", missingMarkers)}.");
        }

        var customizedFiles = new List<(string Path, string Content)>();
        var unresolvedMarkers = new SortedSet<string>(StringComparer.Ordinal);
        foreach (var (path, content) in files)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var customized = ReservedMarkerRegex().Replace(
                content,
                match => Replacements.TryGetValue(match.Value, out var valueFactory)
                    ? EscapeValue(path, content, match, valueFactory(values))
                    : match.Value);

            foreach (Match unresolved in ReservedMarkerRegex().Matches(customized))
            {
                unresolvedMarkers.Add(unresolved.Value);
            }

            if (!string.Equals(content, customized, StringComparison.Ordinal))
            {
                customizedFiles.Add((path, customized));
            }
        }

        if (unresolvedMarkers.Count > 0)
        {
            throw new WebResourceScaffoldingException(
                $"The template contains unsupported customization markers: {string.Join(", ", unresolvedMarkers)}. " +
                "Coordinate marker support between the template and dgtp before retrying.");
        }

        foreach (var (path, content) in customizedFiles)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await File.WriteAllTextAsync(path, content, StrictUtf8, cancellationToken);
        }
    }

    private static async Task<string?> ReadUtf8TextAsync(string path, CancellationToken cancellationToken)
    {
        var bytes = await File.ReadAllBytesAsync(path, cancellationToken);
        if (bytes.AsSpan().Contains((byte)0))
        {
            return null;
        }

        try
        {
            return StrictUtf8.GetString(bytes);
        }
        catch (DecoderFallbackException)
        {
            return null;
        }
    }

    private static string EscapeValue(string path, string content, Match marker, string value)
    {
        if (value.Length == 0 || IsSafeUnquotedValue(value))
        {
            return value;
        }

        var closingIndex = marker.Index + marker.Length;
        if (marker.Index == 0 ||
            closingIndex >= content.Length ||
            content[marker.Index - 1] != content[closingIndex] ||
            content[marker.Index - 1] is not ('\'' or '"' or '`'))
        {
            throw UnsafeValue(marker.Value);
        }

        var quote = content[marker.Index - 1];
        var extension = Path.GetExtension(path);
        if (quote == '`' && extension.Equals(".md", StringComparison.OrdinalIgnoreCase))
        {
            return value.Contains('`', StringComparison.Ordinal) || value.Any(char.IsControl)
                ? throw UnsafeValue(marker.Value)
                : value;
        }

        if (quote == '\'' && (extension.Equals(".ps1", StringComparison.OrdinalIgnoreCase) ||
                              extension.Equals(".yaml", StringComparison.OrdinalIgnoreCase) ||
                              extension.Equals(".yml", StringComparison.OrdinalIgnoreCase)))
        {
            return value.Replace("'", "''", StringComparison.Ordinal);
        }

        if (quote == '\'' && IsJavaScriptText(extension))
        {
            return value.Replace("\\", "\\\\", StringComparison.Ordinal)
                .Replace("'", "\\'", StringComparison.Ordinal);
        }

        if (quote == '"' && extension.Equals(".ps1", StringComparison.OrdinalIgnoreCase))
        {
            return value.Replace("`", "``", StringComparison.Ordinal)
                .Replace("$", "`$", StringComparison.Ordinal)
                .Replace("\"", "`\"", StringComparison.Ordinal);
        }

        if (quote == '"' && IsSupportedDoubleQuotedText(extension))
        {
            var serialized = JsonSerializer.Serialize(value);
            return serialized[1..^1];
        }

        throw UnsafeValue(marker.Value);
    }

    private static bool IsSafeUnquotedValue(string value)
    {
        foreach (var character in value)
        {
            if (!IsSafeUnquotedCharacter(character))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsSafeUnquotedCharacter(char character) =>
        char.IsAsciiLetterOrDigit(character) || character is '_' or '-' or '.';

    private static bool IsJavaScriptText(string extension) => JavaScriptExtensions.Contains(extension);

    private static bool IsSupportedDoubleQuotedText(string extension) =>
        IsJavaScriptText(extension) ||
        extension.Equals(".json", StringComparison.OrdinalIgnoreCase) ||
        extension.Equals(".yaml", StringComparison.OrdinalIgnoreCase) ||
        extension.Equals(".yml", StringComparison.OrdinalIgnoreCase);

    private static WebResourceScaffoldingException UnsafeValue(string marker) =>
        new($"The supplied value for {marker} cannot be safely inserted at its template marker.");

    [GeneratedRegex("__DGTP_[A-Za-z0-9_]+__", RegexOptions.CultureInvariant)]
    private static partial Regex ReservedMarkerRegex();
}
