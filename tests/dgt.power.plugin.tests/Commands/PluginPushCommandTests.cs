// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Commands;
using dgt.power.tests;

namespace dgt.power.plugin.tests.Commands;

public sealed class PluginPushCommandTests : CommandTestsBase<PluginPushCommand, PluginPushSettings>
{
    private readonly string _root = Directory.CreateTempSubdirectory("dgtp-push-").FullName;

    public override void Dispose()
    {
        Directory.Delete(_root, recursive: true);
        base.Dispose();
    }

    private bool Run(string target) => GetContext().Execute(new PluginPushSettings { Target = target });

    [Test]
    public async Task Execute_GlobMatchingNothing_Fails()
    {
        await Assert.That(Run(Path.Combine(_root, "**", "*.nupkg"))).IsFalse();
    }

    [Test]
    public async Task Execute_MissingPath_Fails()
    {
        await Assert.That(Run(Path.Combine(_root, "missing.dll"))).IsFalse();
    }

    [Test]
    public async Task Execute_FilesWithoutPlugins_FailsWithCount()
    {
        await File.WriteAllBytesAsync(Path.Combine(_root, "native.dll"), "MZ not managed"u8.ToArray());
        File.Copy(typeof(object).Assembly.Location, Path.Combine(_root, "runtime.dll"));

        await Assert.That(Run(_root)).IsFalse();
        // The console soft-wraps at its width, and the wrap point depends on the temp path length.
        var output = string.Join(' ', TestConsole.Output.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
        await Assert.That(output).Contains("2 file(s) matched").And.Contains("Narrow the target");
    }

    [Test]
    public async Task Execute_TargetWithMarkupBrackets_IsRenderedLiterally()
    {
        var directory = Directory.CreateDirectory(Path.Combine(_root, "[red]x")).FullName;
        File.Copy(typeof(object).Assembly.Location, Path.Combine(directory, "runtime.dll"));

        await Assert.That(Run(directory)).IsFalse();
        var output = string.Join(' ', TestConsole.Output.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
        await Assert.That(output).Contains("[red]x").And.Contains("none contain plugins");
    }

    [Test]
    public async Task Execute_UnreadablePackage_FailsBeforeAnyWrite()
    {
        await File.WriteAllBytesAsync(Path.Combine(_root, "broken.nupkg"), "not a zip"u8.ToArray());

        await Assert.That(Run(_root)).IsFalse();
        await Assert.That(TestConsole.Output).Contains("Nothing was deployed");
    }
}
