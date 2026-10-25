// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.common.FileAccess;

public static class AtomicFileWriter
{
    public static Task WriteAsync(string outputPath, string contents, bool overwrite, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(outputPath);
        ArgumentNullException.ThrowIfNull(contents);
        var path = Path.GetFullPath(outputPath, Environment.CurrentDirectory);
        return WriteCoreAsync(path, contents, overwrite, cancellationToken);
    }

    private static async Task WriteCoreAsync(string path, string contents, bool overwrite, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (!overwrite && File.Exists(path))
        {
            throw new IOException($"Output file '{path}' already exists. Use --overwrite to replace it.");
        }

        var directory = Path.GetDirectoryName(path) ?? Environment.CurrentDirectory;
        Directory.CreateDirectory(directory);
        var temporaryPath = Path.Combine(directory, $".{Path.GetFileName(path)}.{Guid.NewGuid():N}.tmp");
        try
        {
            await using (var stream = new FileStream(temporaryPath, FileMode.CreateNew, System.IO.FileAccess.Write, FileShare.None))
            await using (var writer = new StreamWriter(stream))
            {
                await writer.WriteAsync(contents.AsMemory(), cancellationToken);
                await writer.FlushAsync(cancellationToken);
            }

            cancellationToken.ThrowIfCancellationRequested();
            File.Move(temporaryPath, path, overwrite);
        }
        finally
        {
            if (File.Exists(temporaryPath))
            {
                File.Delete(temporaryPath);
            }
        }
    }
}
