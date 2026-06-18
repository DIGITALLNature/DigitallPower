// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Commands.Complete;
using Spectre.Console;
using Spectre.Console.Cli;
using TUnit.Assertions.Extensions;

namespace dgt.power.telemetry.tests.Completion;

public class CompleteSetupCommandTests
{
    private readonly StringWriter _output = new();
    private readonly IAnsiConsole _console;

    public CompleteSetupCommandTests()
    {
        _console = AnsiConsole.Create(new AnsiConsoleSettings
        {
            Out = new AnsiConsoleOutput(_output),
            Ansi = AnsiSupport.No,
            ColorSystem = ColorSystemSupport.NoColors
        });
    }

    [Test]
    public async Task ExecuteAsync_WhenDotnetSuggestMissing_ReturnsError()
    {
        var installer = new FakeShellShimInstaller(dotnetSuggestFound: false, ShimInstallResult.Installed);
        ICommand<CompleteSetupSettings> command = new CompleteSetupCommand(_console, installer);

        var result = await command.ExecuteAsync(GetContext(), new CompleteSetupSettings(), CancellationToken.None);

        await Assert.That(result).IsEqualTo(1);
        await Assert.That(_output.ToString()).Contains("dotnet-suggest not found");
    }

    [Test]
    public async Task ExecuteAsync_WithAll_AndUnknownShell_ReturnsError()
    {
        var installer = new FakeShellShimInstaller(dotnetSuggestFound: true, ShimInstallResult.Installed);
        ICommand<CompleteSetupSettings> command = new CompleteSetupCommand(_console, installer);

        var result = await command.ExecuteAsync(GetContext(),
            new CompleteSetupSettings { All = true, Shell = "unknownshell" },
            CancellationToken.None);

        // Fails either at ProcessPath=null or shell validation — either way exit 1
        await Assert.That(result).IsEqualTo(1);
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static CommandContext GetContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "setup", null);

    private sealed class FakeShellShimInstaller(bool dotnetSuggestFound, ShimInstallResult installResult) : ShellShimInstaller
    {
        public bool InstallCalled { get; private set; }

        public override string? FindDotnetSuggest() =>
            dotnetSuggestFound ? "/fake/dotnet-suggest" : null;

        public override Task<ShimInstallResult> InstallAsync(string shell, string rcFilePath, CancellationToken cancellationToken = default)
        {
            InstallCalled = true;
            return Task.FromResult(installResult);
        }
    }
}

