// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;

namespace dgt.power.Commands.Complete;

public enum ShimInstallResult
{
    Installed,
    AlreadyInstalled,
    UnknownShell,
    DotnetSuggestNotFound
}

/// <summary>
/// Handles installing the dotnet-suggest shell shim into a shell's RC file.
/// Virtual methods allow overriding in tests without hitting the real filesystem or process.
/// </summary>
public class ShellShimInstaller
{
    public const string MarkerStart = "# >>> dgtp tab completion start >>>";
    public const string MarkerEnd   = "# <<< dgtp tab completion end <<<";

    /// <summary>Finds the dotnet-suggest executable on the current machine.</summary>
    public virtual string? FindDotnetSuggest()
    {
        var exeName = OperatingSystem.IsWindows() ? "dotnet-suggest.exe" : "dotnet-suggest";

        var toolsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".dotnet", "tools", exeName);
        if (File.Exists(toolsPath))
            return toolsPath;

        foreach (var dir in (Environment.GetEnvironmentVariable("PATH") ?? string.Empty)
                     .Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries))
        {
            var candidate = Path.Combine(dir, exeName);
            if (File.Exists(candidate))
                return candidate;
        }

        return null;
    }

    /// <summary>Runs <c>dotnet-suggest script &lt;shell&gt;</c> and returns the script content.</summary>
    protected virtual async Task<string?> GetDotnetSuggestScriptAsync(string shell, CancellationToken cancellationToken = default)
    {
        var dotnetSuggest = FindDotnetSuggest();
        if (dotnetSuggest is null)
            return null;

        var suggestShellName = ShellDetector.ToDotnetSuggestName(shell);
        // ReSharper disable once UsingStatementResourceInitialization
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = dotnetSuggest,
                Arguments = $"script {suggestShellName}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        var script = await process.StandardOutput.ReadToEndAsync(cancellationToken);
        await process.WaitForExitAsync(cancellationToken);

        return process.ExitCode == 0 && !string.IsNullOrWhiteSpace(script) ? script : null;
    }

    /// <summary>
    /// Installs the dotnet-suggest shell shim into <paramref name="rcFilePath"/>.
    /// Idempotent: does nothing if the marker is already present.
    /// </summary>
    public virtual async Task<ShimInstallResult> InstallAsync(string shell, string rcFilePath, CancellationToken cancellationToken = default)
    {
        if (ShellDetector.GetRcFilePath(shell) is null)
            return ShimInstallResult.UnknownShell;

        // Idempotency: look for our own marker
        if (File.Exists(rcFilePath))
        {
            var existing = await File.ReadAllTextAsync(rcFilePath, cancellationToken);
            if (existing.Contains(MarkerStart, StringComparison.Ordinal))
                return ShimInstallResult.AlreadyInstalled;
        }

        var script = await GetDotnetSuggestScriptAsync(shell, cancellationToken);
        if (script is null)
            return ShimInstallResult.DotnetSuggestNotFound;

        // Ensure parent directory exists (fish conf.d, pwsh profile dir may not exist yet)
        var dir = Path.GetDirectoryName(rcFilePath);
        if (dir is not null && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        var block = $"\n{MarkerStart}\n{script.TrimEnd()}\n{MarkerEnd}\n";
        await File.AppendAllTextAsync(rcFilePath, block, cancellationToken);

        return ShimInstallResult.Installed;
    }
}
