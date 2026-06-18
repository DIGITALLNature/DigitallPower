// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.Commands.Complete;

public class CompleteSetupCommand(IAnsiConsole console, ShellShimInstaller installer)
    : AsyncCommand<CompleteSetupSettings>
{
    private static readonly string RegistrationFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".dotnet-suggest-registration.json");

    protected override async Task<int> ExecuteAsync(CommandContext context, CompleteSetupSettings settings, CancellationToken cancellationToken)
    {
        var processPath = Environment.ProcessPath;
        if (processPath is null)
        {
            console.MarkupLine("[red]Unable to determine the dgtp executable path.[/]");
            return 1;
        }

        var dotnetSuggest = installer.FindDotnetSuggest();
        if (dotnetSuggest is null)
        {
            console.MarkupLine("[red]✗ dotnet-suggest not found.[/]");
            console.MarkupLine("Install it first: [bold]dotnet tool install -g dotnet-suggest[/]");
            return 1;
        }

        if (IsAlreadyRegistered(processPath))
        {
            console.MarkupLine("[green]✓[/] dgtp is already registered with dotnet-suggest.");
        }
        else
        {
            var exitCode = await RunDotnetSuggestRegisterAsync(dotnetSuggest, processPath, cancellationToken);
            if (exitCode != 0)
            {
                console.MarkupLine("[red]✗ Registration with dotnet-suggest failed.[/]");
                return 1;
            }

            console.MarkupLine("[green]✓[/] dgtp registered with dotnet-suggest.");
        }

        if (!settings.All)
        {
            console.WriteLine();
            console.MarkupLine("To also install the shell completion shim, run:");
            console.MarkupLine("  [bold]dgtp complete setup --all[/]");
            console.MarkupLine("  or: [bold]dgtp complete install-shell[/]");
            return 0;
        }

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
            console.MarkupLine($"[red]Unsupported shell: [bold]{Markup.Escape(shell)}[/].[/]");
            return 1;
        }

        console.MarkupLine($"Installing shell shim for [bold]{shell}[/]…");
        return await CompleteInstallShellCommand.InstallAndReportAsync(console, installer, shell, rcPath, cancellationToken);
    }

    private static bool IsAlreadyRegistered(string processPath)
    {
        if (!File.Exists(RegistrationFilePath))
            return false;

        try
        {
            var json = File.ReadAllText(RegistrationFilePath);
            return json.Contains(processPath, StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    private static async Task<int> RunDotnetSuggestRegisterAsync(string dotnetSuggest, string processPath, CancellationToken cancellationToken)
    {
        try
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = dotnetSuggest,
                    Arguments = $"register --command-path \"{processPath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            await process.WaitForExitAsync(cancellationToken);
            return process.ExitCode;
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            return 1;
        }
    }
}
