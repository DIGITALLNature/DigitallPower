// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.webresource.Scaffolding;

internal sealed class WebResourceProjectScaffolder(HttpClient httpClient)
{
    private readonly WebResourceTemplateDownloader _downloader = new(httpClient);
    private readonly WebResourceArchiveExtractor _extractor = new();

    public async Task<string> CreateAsync(
        string destination,
        string? templateVersion,
        WebResourceProjectValues values,
        CancellationToken cancellationToken)
    {
        var target = ValidateDestination(destination);
        var parent = Path.GetDirectoryName(target.Path)!;
        var staging = Path.Combine(parent, $".dgtp-webresource-{Guid.NewGuid():N}.tmp");
        var ownershipMarker = Path.Combine(parent, $".dgtp-webresource-{Guid.NewGuid():N}.owner");
        var ownershipToken = Guid.NewGuid().ToString("N");
        var staged = false;
        var published = false;

        try
        {
            var downloaded = await _downloader.DownloadAsync(templateVersion, cancellationToken);
            Directory.CreateDirectory(parent);
            EnsureNoReparsePoints(target.Path);
            await using (var marker = new FileStream(
                             ownershipMarker,
                             FileMode.CreateNew,
                             FileAccess.Write,
                             FileShare.None,
                             4096,
                             useAsync: true))
            {
                await marker.WriteAsync(System.Text.Encoding.UTF8.GetBytes(ownershipToken), cancellationToken);
            }

            if (Directory.Exists(staging) || File.Exists(staging))
            {
                throw new IOException("The temporary staging path is already in use.");
            }

            Directory.CreateDirectory(staging);
            staged = true;
            await _extractor.ExtractAsync(downloaded.Archive, staging, cancellationToken);
            await WebResourceTemplateCustomizer.CustomizeAsync(staging, values, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            var finalTarget = ValidateDestination(target.Path);
            if (finalTarget.Exists)
            {
                Directory.Delete(target.Path, recursive: false);
            }

            try
            {
                Directory.Move(staging, target.Path);
                published = true;
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                if (finalTarget.Exists && !File.Exists(target.Path) && !Directory.Exists(target.Path))
                {
                    Directory.CreateDirectory(target.Path);
                }

                throw;
            }

            File.Delete(ownershipMarker);
            return downloaded.CommitSha;
        }
        catch (WebResourceScaffoldingException)
        {
            throw;
        }
        catch (InvalidDataException exception)
        {
            throw new WebResourceScaffoldingException(
                "The downloaded template archive is invalid or corrupt.",
                exception);
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or ArgumentException or NotSupportedException)
        {
            throw new WebResourceScaffoldingException(
                $"Could not create the webresource project at '{target.Path}'. Check the {nameof(destination)} and filesystem permissions.",
                exception);
        }
        finally
        {
            if (IsOwned(ownershipMarker, ownershipToken))
            {
                if (!published && staged && TryGetAttributes(staging, out var attributes) &&
                    (attributes & FileAttributes.ReparsePoint) == 0)
                {
                    Directory.Delete(staging, recursive: true);
                }

                File.Delete(ownershipMarker);
            }
        }
    }

    private static Destination ValidateDestination(string requestedPath)
    {
        if (string.IsNullOrWhiteSpace(requestedPath))
        {
            throw new WebResourceScaffoldingException("A destination directory is required.");
        }

        string fullPath;
        try
        {
            fullPath = Path.GetFullPath(requestedPath);
        }
        catch (Exception exception) when (
            exception is ArgumentException or NotSupportedException or PathTooLongException)
        {
            throw new WebResourceScaffoldingException($"The destination path '{requestedPath}' is invalid.", exception);
        }

        var root = Path.GetPathRoot(fullPath)!;
        if (string.Equals(
                Path.TrimEndingDirectorySeparator(fullPath),
                Path.TrimEndingDirectorySeparator(root),
                OperatingSystem.IsWindows() ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
        {
            throw new WebResourceScaffoldingException("The destination cannot be a filesystem root.");
        }

        EnsureNoReparsePoints(fullPath);
        if (File.Exists(fullPath))
        {
            throw new WebResourceScaffoldingException($"The destination '{fullPath}' is a file.");
        }

        var exists = Directory.Exists(fullPath);
        if (exists && Directory.EnumerateFileSystemEntries(fullPath).Any())
        {
            throw new WebResourceScaffoldingException(
                $"The destination '{fullPath}' must be absent or an empty directory.");
        }

        return new Destination(fullPath, exists);
    }

    private static void EnsureNoReparsePoints(string path)
    {
        var root = Path.GetPathRoot(path)!;
        var current = Path.TrimEndingDirectorySeparator(Path.GetFullPath(path));
        while (current.Length >= root.Length)
        {
            if (TryGetAttributes(current, out var attributes))
            {
                if ((attributes & FileAttributes.ReparsePoint) != 0)
                {
                    throw new WebResourceScaffoldingException(
                        $"The destination path contains a symbolic link or reparse point: '{current}'.");
                }

                if (!string.Equals(current, Path.TrimEndingDirectorySeparator(path),
                        OperatingSystem.IsWindows() ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) &&
                    (attributes & FileAttributes.Directory) == 0)
                {
                    throw new WebResourceScaffoldingException(
                        $"The destination path has a non-directory ancestor: '{current}'.");
                }
            }

            if (string.Equals(current, Path.TrimEndingDirectorySeparator(root),
                    OperatingSystem.IsWindows() ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
            {
                break;
            }

            current = Path.GetDirectoryName(current)!;
        }
    }

    private static bool TryGetAttributes(string path, out FileAttributes attributes)
    {
        try
        {
            attributes = File.GetAttributes(path);
            return true;
        }
        catch (FileNotFoundException)
        {
            attributes = default;
            return false;
        }
        catch (DirectoryNotFoundException)
        {
            attributes = default;
            return false;
        }
    }

    private static bool IsOwned(string markerPath, string token)
    {
        if (!File.Exists(markerPath))
        {
            return false;
        }

        if (TryGetAttributes(markerPath, out var attributes) &&
            (attributes & FileAttributes.ReparsePoint) != 0)
        {
            return false;
        }

        return string.Equals(File.ReadAllText(markerPath), token, StringComparison.Ordinal);
    }

    private sealed record Destination(string Path, bool Exists);
}
