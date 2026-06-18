// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Commands.Complete;
using Spectre.Console;
using Spectre.Console.Cli;
using TUnit.Assertions.Extensions;

namespace dgt.power.cli.tests.Completion;

public class CompleteInstallShellCommandTests : IDisposable
{
    private readonly StringWriter _output = new();
    private readonly IAnsiConsole _console;
    private readonly string _tempFile = Path.GetTempFileName();

    public CompleteInstallShellCommandTests()
    {
        _console = AnsiConsole.Create(new AnsiConsoleSettings
        {
            Out = new AnsiConsoleOutput(_output),
            Ansi = AnsiSupport.No,
            ColorSystem = ColorSystemSupport.NoColors
        });
    }

    public void Dispose()
    {
        if (File.Exists(_tempFile))
            File.Delete(_tempFile);
        _output.Dispose();
        GC.SuppressFinalize(this);
    }

    [Test]
    public async Task ExecuteAsync_WhenShellOverrideProvided_InstallsShim()
    {
        var installer = new FakeShellShimInstaller(ShimInstallResult.Installed);
        ICommand<CompleteInstallShellSettings> command = new CompleteInstallShellCommand(_console, installer);

        var result = await command.ExecuteAsync(GetContext(), new CompleteInstallShellSettings { Shell = "zsh" }, CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(_output.ToString()).Contains("Shell shim installed");
    }

    [Test]
    public async Task ExecuteAsync_WhenAlreadyInstalled_ShowsNothingToDoMessage()
    {
        var installer = new FakeShellShimInstaller(ShimInstallResult.AlreadyInstalled);
        ICommand<CompleteInstallShellSettings> command = new CompleteInstallShellCommand(_console, installer);

        var result = await command.ExecuteAsync(GetContext(), new CompleteInstallShellSettings { Shell = "bash" }, CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(_output.ToString().Replace(Environment.NewLine, " ").Replace("\n", " ")).Contains("nothing to do");
    }

    [Test]
    public async Task ExecuteAsync_WhenDotnetSuggestMissing_ReturnsError()
    {
        var installer = new FakeShellShimInstaller(ShimInstallResult.DotnetSuggestNotFound);
        ICommand<CompleteInstallShellSettings> command = new CompleteInstallShellCommand(_console, installer);

        var result = await command.ExecuteAsync(GetContext(), new CompleteInstallShellSettings { Shell = "zsh" }, CancellationToken.None);

        await Assert.That(result).IsEqualTo(1);
        await Assert.That(_output.ToString()).Contains("dotnet-suggest not found");
    }

    [Test]
    public async Task ExecuteAsync_WhenUnknownShellOverride_ReturnsError()
    {
        var installer = new FakeShellShimInstaller(ShimInstallResult.Installed);
        ICommand<CompleteInstallShellSettings> command = new CompleteInstallShellCommand(_console, installer);

        var result = await command.ExecuteAsync(GetContext(), new CompleteInstallShellSettings { Shell = "unknownshell" }, CancellationToken.None);

        await Assert.That(result).IsEqualTo(1);
    }

    [Test]
    public async Task ExecuteAsync_WithDryRun_DoesNotCallInstaller()
    {
        var installer = new FakeShellShimInstaller(ShimInstallResult.Installed);
        ICommand<CompleteInstallShellSettings> command = new CompleteInstallShellCommand(_console, installer);

        var result = await command.ExecuteAsync(GetContext(), new CompleteInstallShellSettings { Shell = "zsh", DryRun = true }, CancellationToken.None);

        await Assert.That(result).IsEqualTo(0);
        await Assert.That(installer.InstallCalled).IsFalse();
        await Assert.That(_output.ToString()).Contains("Dry run");
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static CommandContext GetContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "install-shell", null);

    private sealed class FakeShellShimInstaller(ShimInstallResult result) : ShellShimInstaller
    {
        public bool InstallCalled { get; private set; }

        public override string? FindDotnetSuggest() => "/fake/dotnet-suggest";

        public override Task<ShimInstallResult> InstallAsync(string shell, string rcFilePath, CancellationToken cancellationToken = default)
        {
            InstallCalled = true;
            return Task.FromResult(result);
        }
    }
}

