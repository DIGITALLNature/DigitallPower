// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>
/// Resolves the push target argument (file, directory or glob pattern) to a sorted, distinct list of
/// full artifact paths. Directories are scanned top-level only; use a glob pattern to select deeper paths.
/// </summary>
internal static class DeploymentTargetResolver
{
    private static readonly char[] GlobCharacters = ['*', '?', '[', '{'];

    internal static IReadOnlyList<string> Resolve(string target)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(target);
        if (File.Exists(target))
        {
            if (!IsArtifact(target))
            {
                throw new DeploymentTargetException($"Unsupported file '{target}' - expected a .dll or .nupkg file");
            }

            return [Path.GetFullPath(target)];
        }

        if (Directory.Exists(target))
        {
            return Normalize(Directory.EnumerateFiles(target, "*", SearchOption.TopDirectoryOnly).Where(IsArtifact));
        }

        if (target.IndexOfAny(GlobCharacters) < 0)
        {
            throw new DeploymentTargetException($"Path '{target}' not found");
        }

        var split = SplitPattern(target);
        if (!Directory.Exists(split.Root))
        {
            throw new DeploymentTargetException($"Directory '{split.Root}' of pattern '{target}' not found");
        }

        var matcher = new Matcher(StringComparison.OrdinalIgnoreCase);
        matcher.AddInclude(split.Pattern);
        var matches = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(split.Root))).Files
            .Select(file => Path.GetFullPath(Path.Combine(split.Root, file.Path)))
            .Where(IsArtifact);
        var resolved = Normalize(matches);
        if (resolved.Count == 0)
        {
            throw new DeploymentTargetException($"Pattern '{target}' matched no .dll or .nupkg files");
        }

        return resolved;
    }

    // The root is the longest leading directory without wildcards; the remainder is matched relative to it.
    private static GlobPatternRoot SplitPattern(string target)
    {
        var segments = target.Replace('\\', '/').Split('/');
        var firstGlob = Array.FindIndex(segments, segment => segment.IndexOfAny(GlobCharacters) >= 0);
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
