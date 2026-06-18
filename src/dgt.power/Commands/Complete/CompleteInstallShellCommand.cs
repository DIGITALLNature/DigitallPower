// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.Commands.Complete;

public class CompleteInstallShellCommand(IAnsiConsole console, ShellShimInstaller installer)
    : AsyncCommand<CompleteInstallShellSettings>
{
    protected override async Task<int> ExecuteAsync(CommandContext context, CompleteInstallShellSettings settings, CancellationToken cancellationToken)
    {
        var shell = settings.Shell is not null
            ? ShellDetector.NormalizeShellName(settings.Shell)
            : ShellDetector.Detect();

        if (shell is null)
        {
            console.MarkupLine("[yellow]Could not detect current shell.[/] Use [bold]--shell[/] to specify: bash, zsh, pwsh, fish.");
            return 1;
        }

        var rcPath = ShellDetector.GetRcFilePath(shell);
        if (rcPath is null)
        {
            console.MarkupLine($"[red]Unsupported shell: [bold]{Markup.Escape(shell)}[/]. Supported: bash, zsh, pwsh, fish.[/]");
            return 1;
        }

        console.MarkupLine($"Detected shell: [bold]{shell}[/]");

        if (settings.DryRun)
        {
            console.MarkupLine($"Would write shell shim to: [dim]{Markup.Escape(rcPath)}[/]");
            console.MarkupLine("[yellow]Dry run — no changes made.[/]");
            return 0;
        }

        return await InstallAndReportAsync(console, installer, shell, rcPath, cancellationToken);
    }

    /// <summary>Performs the shim installation and writes the result to the console. Shared with CompleteSetupCommand.</summary>
    internal static async Task<int> InstallAndReportAsync(
        IAnsiConsole console,
        ShellShimInstaller installer,
        string shell,
        string rcPath,
        CancellationToken cancellationToken)
    {
        var result = await installer.InstallAsync(shell, rcPath, cancellationToken);
        switch (result)
        {
            case ShimInstallResult.Installed:
                console.MarkupLine($"[green]✓[/] Shell shim installed in [bold]{Markup.Escape(rcPath)}[/]");
                console.WriteLine();
                console.MarkupLine("Reload your shell to activate tab completion:");
                console.MarkupLine($"  [bold]{GetReloadCommand(shell)}[/]");
                return 0;

            case ShimInstallResult.AlreadyInstalled:
                console.MarkupLine($"[green]✓[/] Tab completion already installed in [bold]{Markup.Escape(rcPath)}[/] (nothing to do).");
                return 0;

            case ShimInstallResult.DotnetSuggestNotFound:
                console.MarkupLine("[red]✗ dotnet-suggest not found.[/]");
                console.MarkupLine("Install it first: [bold]dotnet tool install -g dotnet-suggest[/]");
                return 1;

            default:
                return 1;
        }
    }

    private static string GetReloadCommand(string shell) => shell switch
    {
        "bash" => "source ~/.bashrc",
        "zsh"  => "source ~/.zshrc",
        "pwsh" => ". $PROFILE",
        "fish" => "(fish reloads automatically)",
        _      => ""
    };
}
