// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace dgt.power.webresource.Scaffolding;

internal sealed partial class WebResourceArchiveExtractor
{
    private const int DefaultMaximumEntryCount = 10_000;
    private const long DefaultMaximumExpandedBytes = 100L * 1024 * 1024;
    private const int UnixFileTypeMask = 0xF000;
    private const int UnixRegularFile = 0x8000;
    private const int UnixDirectory = 0x4000;
    private const int UnixSymbolicLink = 0xA000;
    private const string InvalidPathCharacters = "<>:\"|?*";
    private static readonly HashSet<string> s_reservedDeviceNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "CON",
        "PRN",
        "AUX",
        "NUL"
    };
    private readonly int _maximumEntryCount;
    private readonly long _maximumExpandedBytes;

    public WebResourceArchiveExtractor(
        int maximumEntryCount = DefaultMaximumEntryCount,
        long maximumExpandedBytes = DefaultMaximumExpandedBytes)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumEntryCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumExpandedBytes);
        _maximumEntryCount = maximumEntryCount;
        _maximumExpandedBytes = maximumExpandedBytes;
    }

    public Task ExtractAsync(
        byte[] archiveBytes,
        string stagingDirectory,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(archiveBytes);
        ArgumentException.ThrowIfNullOrWhiteSpace(stagingDirectory);
        return ExtractCoreAsync(archiveBytes, stagingDirectory, cancellationToken);
    }

    private async Task ExtractCoreAsync(
        byte[] archiveBytes,
        string stagingDirectory,
        CancellationToken cancellationToken)
    {
        await using var archiveStream = new MemoryStream(archiveBytes, writable: false);
        await using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Read);
        if (archive.Entries.Count is 0 || archive.Entries.Count > _maximumEntryCount)
        {
            throw new WebResourceScaffoldingException(
                $"The template archive must contain between 1 and {_maximumEntryCount:N0} entries.");
        }

        var descriptors = ValidateEntries(archive.Entries, _maximumExpandedBytes);
        var stagingRoot = Path.GetFullPath(stagingDirectory);
        Directory.CreateDirectory(stagingRoot);
        long expandedBytes = 0;

        foreach (var descriptor in descriptors)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (descriptor.RelativePath.Length == 0)
            {
                continue;
            }

            var destinationPath = GetSafeDestinationPath(stagingRoot, descriptor.RelativePath);
            if (descriptor.IsDirectory)
            {
                Directory.CreateDirectory(destinationPath);
                continue;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
            await using var input = await descriptor.Entry.OpenAsync(cancellationToken);
            await using var output = new FileStream(
                destinationPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                81920,
                useAsync: true);

            var buffer = new byte[81920];
            while (true)
            {
                var bytesRead = await input.ReadAsync(buffer, cancellationToken);
                if (bytesRead == 0)
                {
                    break;
                }

                if (expandedBytes > _maximumExpandedBytes - bytesRead)
                {
                    throw new WebResourceScaffoldingException(
                        $"The expanded template archive exceeds the {_maximumExpandedBytes / 1024 / 1024} MiB limit.");
                }

                expandedBytes += bytesRead;
                await output.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
            }
        }
    }

    private static List<ArchiveEntryDescriptor> ValidateEntries(
        ReadOnlyCollection<ZipArchiveEntry> entries,
        long maximumExpandedBytes)
    {
        var descriptors = new List<ArchiveEntryDescriptor>(entries.Count);
        string? archiveRoot = null;
        long declaredExpandedBytes = 0;

        foreach (var entry in entries)
        {
            var normalizedName = entry.FullName.Replace('\\', '/');
            var unixType = (entry.ExternalAttributes >> 16) & UnixFileTypeMask;
            var isDirectory = normalizedName.EndsWith('/') ||
                              unixType == UnixDirectory ||
                              (entry.ExternalAttributes & (int)FileAttributes.Directory) != 0;
            if (unixType == UnixSymbolicLink ||
                (entry.ExternalAttributes & (int)FileAttributes.ReparsePoint) != 0)
            {
                throw new WebResourceScaffoldingException(
                    $"The template archive contains a link entry '{entry.FullName}'.");
            }

            if (unixType != 0 && unixType is not UnixRegularFile && unixType is not UnixDirectory)
            {
                throw new WebResourceScaffoldingException(
                    $"The template archive contains an unsupported special entry '{entry.FullName}'.");
            }

            if (IsRootedArchivePath(normalizedName))
            {
                throw new WebResourceScaffoldingException(
                    $"The template archive contains a rooted path '{entry.FullName}'.");
            }

            var pathWithoutTrailingSeparator = isDirectory
                ? normalizedName.TrimEnd('/')
                : normalizedName;
            var segments = pathWithoutTrailingSeparator.Split('/');
            if (segments.Length == 0 || Array.Exists(segments, segment => segment.Length == 0))
            {
                throw new WebResourceScaffoldingException(
                    $"The template archive contains an invalid path '{entry.FullName}'.");
            }

            var safeSegments = segments.Select(ValidateSegment).ToArray();
            if (Array.Exists(safeSegments, segment => segment is "." or ".."))
            {
                throw new WebResourceScaffoldingException(
                    $"The template archive contains a traversal path '{entry.FullName}'.");
            }

            if (archiveRoot is null)
            {
                archiveRoot = safeSegments[0];
            }
            else if (!string.Equals(archiveRoot, safeSegments[0], StringComparison.Ordinal))
            {
                throw new WebResourceScaffoldingException(
                    "The template archive must contain exactly one root directory.");
            }

            if (safeSegments.Length == 1 && !isDirectory)
            {
                throw new WebResourceScaffoldingException(
                    "The template archive root entry must be a directory.");
            }

            var relativeSegments = safeSegments.Skip(1).ToArray();
            var relativePath = string.Join('/', relativeSegments);
            if (entry.Length > maximumExpandedBytes - declaredExpandedBytes)
            {
                throw new WebResourceScaffoldingException(
                    $"The expanded template archive exceeds the {maximumExpandedBytes / 1024 / 1024} MiB limit.");
            }

            declaredExpandedBytes += entry.Length;
            descriptors.Add(new ArchiveEntryDescriptor(
                entry,
                relativePath,
                isDirectory,
                string.Join('/', relativeSegments.Select(segment => segment.Normalize(NormalizationForm.FormC)))));
        }

        ValidatePathCollisions(descriptors);
        return descriptors;
    }

    private static string ValidateSegment(string pathComponent)
    {
        if (IsInvalidSegment(pathComponent))
        {
            throw new WebResourceScaffoldingException(
                $"The template archive contains an invalid path component '{pathComponent}'.");
        }

        if (IsReservedDeviceName(pathComponent))
        {
            throw new WebResourceScaffoldingException(
                $"The template archive contains the reserved platform path component '{pathComponent}'.");
        }

        return pathComponent.Normalize(NormalizationForm.FormC);
    }

    private static bool IsInvalidSegment(string segment)
    {
        if (segment is "." or ".." || segment.EndsWith(' ') || segment.EndsWith('.'))
        {
            return true;
        }

        foreach (var character in segment)
        {
            if (IsInvalidPathCharacter(character))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsInvalidPathCharacter(char character) =>
        character < ' ' || InvalidPathCharacters.Contains(character, StringComparison.Ordinal);

    private static bool IsReservedDeviceName(string segment)
    {
        var deviceName = segment.Split('.')[0];
        return s_reservedDeviceNames.Contains(deviceName) ||
               ReservedDeviceNumberRegex().IsMatch(deviceName);
    }

    private static bool IsRootedArchivePath(string path) =>
        path.StartsWith('/') || Path.IsPathRooted(path) || path.Contains(':', StringComparison.Ordinal);

    private static void ValidatePathCollisions(IReadOnlyCollection<ArchiveEntryDescriptor> descriptors)
    {
        var normalizedEntries = new Dictionary<string, ArchiveEntryDescriptor>(StringComparer.OrdinalIgnoreCase);
        foreach (var descriptor in descriptors)
        {
            if (descriptor.RelativePath.Length == 0)
            {
                continue;
            }

            if (!normalizedEntries.TryAdd(descriptor.CanonicalPath, descriptor))
            {
                throw new WebResourceScaffoldingException(
                    $"The template archive has colliding paths after platform normalization: '{descriptor.RelativePath}'.");
            }
        }

        foreach (var descriptor in normalizedEntries.Values)
        {
            var segments = descriptor.CanonicalPath.Split('/');
            for (var index = 1; index < segments.Length; index++)
            {
                var parentPath = string.Join('/', segments[..index]);
                if (normalizedEntries.TryGetValue(parentPath, out var parent) && !parent.IsDirectory)
                {
                    throw new WebResourceScaffoldingException(
                        $"The template archive has a file/directory collision at '{parent.RelativePath}'.");
                }
            }
        }
    }

    private static string GetSafeDestinationPath(string stagingRoot, string relativePath)
    {
        var destinationPath = Path.GetFullPath(
            Path.Combine(stagingRoot, relativePath.Replace('/', Path.DirectorySeparatorChar)));
        var stagingPrefix = Path.TrimEndingDirectorySeparator(stagingRoot) + Path.DirectorySeparatorChar;
        var comparison = OperatingSystem.IsWindows() ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        if (!destinationPath.StartsWith(stagingPrefix, comparison))
        {
            throw new WebResourceScaffoldingException(
                $"The template archive path '{relativePath}' escapes its staging directory.");
        }

        return destinationPath;
    }

    [GeneratedRegex("^(COM|LPT)[1-9]$", RegexOptions.IgnoreCase)]
    private static partial Regex ReservedDeviceNumberRegex();

    private sealed record ArchiveEntryDescriptor(
        ZipArchiveEntry Entry,
        string RelativePath,
        bool IsDirectory,
        string CanonicalPath);
}
