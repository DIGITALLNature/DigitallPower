// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Batch;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Deployment.Batch;

public sealed class DeploymentTargetReaderTests : IDisposable
{
    private readonly string _root = Directory.CreateTempSubdirectory("dgtp-reader-").FullName;

    public void Dispose() => Directory.Delete(_root, recursive: true);

    private string Write(string name, byte[] content)
    {
        var path = Path.Combine(_root, name);
        File.WriteAllBytes(path, content);
        return path;
    }

    [Test]
    public async Task Read_NonManagedDll_IsSkippedNotFailed()
    {
        var native = Write("native.dll", "MZ not a managed assembly"u8.ToArray());
        using var console = new TestConsole();

        var result = new DeploymentTargetReader(console).Read([native]);

        await Assert.That(result.Skipped).IsEquivalentTo([native]);
        await Assert.That(result.Failures).IsEmpty();
        await Assert.That(result.Targets).IsEmpty();
    }

    [Test]
    public async Task Read_ManagedAssemblyWithoutSdkReference_IsSkippedWithoutReflection()
    {
        var managed = typeof(object).Assembly.Location;
        using var console = new TestConsole();

        var result = new DeploymentTargetReader(console).Read([managed]);

        await Assert.That(result.Skipped).IsEquivalentTo([managed]);
        await Assert.That(result.Failures).IsEmpty();
        await Assert.That(result.Targets).IsEmpty();
    }

    [Test]
    public async Task Read_CorruptPackage_IsFailureThatBlocksTheBatch()
    {
        var package = Write("broken.nupkg", "not a zip"u8.ToArray());
        using var console = new TestConsole();

        var result = new DeploymentTargetReader(console).Read([package]);

        await Assert.That(result.Failures.Select(failure => failure.FilePath)).IsEquivalentTo([package]);
        await Assert.That(result.Targets).IsEmpty();
    }

    [Test]
    public async Task Read_Cancelled_Throws()
    {
        using var console = new TestConsole();
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        var token = cts.Token;
        var reader = new DeploymentTargetReader(console);

        await Assert.That(() => reader.Read(["a.dll"], token)).Throws<OperationCanceledException>();
    }
}
