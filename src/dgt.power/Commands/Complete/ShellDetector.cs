// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.Commands.Complete;

internal static class ShellDetector
{
    internal static readonly string[] SupportedShells = ["bash", "zsh", "fish", "pwsh"];

    /// <summary>
    /// Detects the current shell from environment variables.
    /// Returns a normalized shell name (bash|zsh|fish|pwsh) or null if unknown.
    /// </summary>
    public static string? Detect()
    {
        var shell = Environment.GetEnvironmentVariable("SHELL");
        if (shell is not null)
            return NormalizeShellName(Path.GetFileNameWithoutExtension(shell));

        // On Windows without $SHELL, default to PowerShell
        if (OperatingSystem.IsWindows())
            return "pwsh";

        return null;
    }

    /// <summary>
    /// Maps an internal shell name to the argument accepted by <c>dotnet-suggest script</c>.
    /// </summary>
    public static string ToDotnetSuggestName(string shell) => shell switch
    {
        "pwsh" => "powershell",
        _ => shell
    };

    /// <summary>
    /// Returns the RC file path that should receive the shell shim for the given shell.
    /// Returns null for unrecognised shells.
    /// </summary>
    public static string? GetRcFilePath(string shell)
    {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return NormalizeShellName(shell) switch
        {
            "bash" => Path.Combine(home, ".bashrc"),
            "zsh"  => Path.Combine(home, ".zshrc"),
            "fish" => Path.Combine(home, ".config", "fish", "conf.d", "dgtp-complete.fish"),
            "pwsh" => GetPwshProfilePath(),
            _      => null
        };
    }

    /// <summary>
    /// Returns the PowerShell profile path for the current OS.
    /// </summary>
    private static string GetPwshProfilePath() =>
        OperatingSystem.IsWindows()
            ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PowerShell", "Microsoft.PowerShell_profile.ps1")
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "powershell", "Microsoft.PowerShell_profile.ps1");

    /// <summary>Normalises a shell basename to a canonical name (bash|zsh|fish|pwsh) or null.</summary>
    internal static string? NormalizeShellName(string? name) => name?.ToLowerInvariant() switch
    {
        "bash"                 => "bash",
        "zsh"                  => "zsh",
        "fish"                 => "fish",
        "pwsh" or "powershell" => "pwsh",
        _                      => null
    };
}
