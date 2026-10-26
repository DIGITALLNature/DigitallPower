// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>
/// Resolves a path pattern (file, directory or glob) to the sorted, distinct full paths of the plugin
/// artifacts (.dll, .nupkg) it selects. Directories are scanned top-level only; use a glob pattern to
/// select deeper paths. Every kind of pattern is filtered the same way, and the result is never empty:
/// a pattern that selects no artifact throws <see cref="ArtifactPathException"/>.
/// </summary>
internal static class ArtifactPathResolver
{
    private static readonly char[] s_globCharacters = ['*', '?', '[', '{'];

    internal static IReadOnlyList<string> Resolve(string pattern)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pattern);
        if (File.Exists(pattern))
        {
            return IsArtifact(pattern)
                ? [Path.GetFullPath(pattern)]
                : throw new ArtifactPathException($"Unsupported file '{pattern}' - expected a .dll or .nupkg file");
        }

        if (Directory.Exists(pattern))
        {
            return RequireArtifacts(Directory.EnumerateFiles(pattern, "*", SearchOption.TopDirectoryOnly).Where(IsArtifact), pattern);
        }

        if (pattern.IndexOfAny(s_globCharacters) < 0)
        {
            throw new ArtifactPathException($"Path '{pattern}' not found");
        }

        var split = SplitPattern(pattern);
        if (!Directory.Exists(split.Root))
        {
            throw new ArtifactPathException($"Directory '{split.Root}' for '{pattern}' not found");
        }

        var matcher = new Matcher(StringComparison.OrdinalIgnoreCase);
        matcher.AddInclude(split.Pattern);
        var matches = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(split.Root))).Files
            .Select(file => Path.GetFullPath(Path.Combine(split.Root, file.Path)))
            .Where(IsArtifact);
        return RequireArtifacts(matches, pattern);
    }

    private static List<string> RequireArtifacts(IEnumerable<string> paths, string pattern)
    {
        var artifacts = Normalize(paths);
        return artifacts.Count > 0 ? artifacts : throw new ArtifactPathException($"'{pattern}' contains or matches no .dll or .nupkg files");
    }

    // The root is the longest leading directory without wildcards; the remainder is matched relative to it.
    private static GlobPatternRoot SplitPattern(string target)
    {
        var segments = target.Replace('\\', '/').Split('/');
        var firstGlob = Array.FindIndex(segments, segment => segment.IndexOfAny(s_globCharacters) >= 0);
        var root = string.Join('/', segments.Take(firstGlob));
        if (root.Length == 0 && target.StartsWith('/'))
        {
            root = "/";
        }
        else if (root.Length == 0)
        {
            root = ".";
        }
        else if (root.EndsWith(':'))
        {
            root += "/";
        }

        return new GlobPatternRoot(Path.GetFullPath(root), string.Join('/', segments.Skip(firstGlob)));
    }

    private static List<string> Normalize(IEnumerable<string> paths) =>
        paths.Select(Path.GetFullPath).Distinct(StringComparer.OrdinalIgnoreCase).Order(StringComparer.OrdinalIgnoreCase).ToList();

    private static bool IsArtifact(string path) =>
        path.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) || path.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase);
}
