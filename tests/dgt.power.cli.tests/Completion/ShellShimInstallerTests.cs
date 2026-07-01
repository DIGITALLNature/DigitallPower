// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Commands.Complete;

namespace dgt.power.cli.tests.Completion;

public class ShellShimInstallerTests : IDisposable
{
    private readonly string _tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

    public void Dispose()
    {
        if (File.Exists(_tempFile))
            File.Delete(_tempFile);
        GC.SuppressFinalize(this);
    }

    [Test]
    public async Task InstallAsync_WhenFileAlreadyHasMarker_ReturnsAlreadyInstalled()
    {
        await File.WriteAllTextAsync(_tempFile,
            $"# existing content\n{ShellShimInstaller.MarkerStart}\necho shim\n{ShellShimInstaller.MarkerEnd}\n");

        var installer = new FakeShellShimInstaller("fake-shim");
        var result = await installer.InstallAsync("bash", _tempFile);

        await Assert.That(result).IsEqualTo(ShimInstallResult.AlreadyInstalled);
    }

    [Test]
    public async Task InstallAsync_WhenFileHasNoMarker_AppendsWithMarkers()
    {
        await File.WriteAllTextAsync(_tempFile, "# existing content\n");

        var installer = new FakeShellShimInstaller("echo shim_content");
        var result = await installer.InstallAsync("bash", _tempFile);

        var content = await File.ReadAllTextAsync(_tempFile);
        await Assert.That(result).IsEqualTo(ShimInstallResult.Installed);
        await Assert.That(content).Contains(ShellShimInstaller.MarkerStart);
        await Assert.That(content).Contains("echo shim_content");
        await Assert.That(content).Contains(ShellShimInstaller.MarkerEnd);
    }

    [Test]
    public async Task InstallAsync_WhenFileDoesNotExist_CreatesFileWithMarkers()
    {
        var newFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.fish");
        try
        {
            var installer = new FakeShellShimInstaller("echo shim");
            var result = await installer.InstallAsync("fish", newFile);

            await Assert.That(result).IsEqualTo(ShimInstallResult.Installed);
            await Assert.That(File.Exists(newFile)).IsTrue();
            var content = await File.ReadAllTextAsync(newFile);
            await Assert.That(content).Contains(ShellShimInstaller.MarkerStart);
        }
        finally
        {
            if (File.Exists(newFile))
                File.Delete(newFile);
        }
    }

    [Test]
    public async Task InstallAsync_WhenDotnetSuggestNotFound_ReturnsDotnetSuggestNotFound()
    {
        var installer = new FakeShellShimInstaller(null); // null = dotnet-suggest not found
        var result = await installer.InstallAsync("zsh", _tempFile);

        await Assert.That(result).IsEqualTo(ShimInstallResult.DotnetSuggestNotFound);
    }

    [Test]
    public async Task InstallAsync_IsIdempotent_OnSecondCall()
    {
        var installer = new FakeShellShimInstaller("echo shim");
        await installer.InstallAsync("bash", _tempFile);
        var second = await installer.InstallAsync("bash", _tempFile);

        await Assert.That(second).IsEqualTo(ShimInstallResult.AlreadyInstalled);

        var content = await File.ReadAllTextAsync(_tempFile);
        await Assert.That(content.IndexOf(ShellShimInstaller.MarkerStart, StringComparison.Ordinal))
            .IsEqualTo(content.LastIndexOf(ShellShimInstaller.MarkerStart, StringComparison.Ordinal));
    }

    // ── Test double ──────────────────────────────────────────────────────────

    private sealed class FakeShellShimInstaller(string? scriptContent) : ShellShimInstaller
    {
        public override string? FindDotnetSuggest() => scriptContent is not null ? "/fake/dotnet-suggest" : null;

        protected override Task<string?> GetDotnetSuggestScriptAsync(string shell, CancellationToken cancellationToken = default)
            => Task.FromResult(scriptContent);
    }
}
