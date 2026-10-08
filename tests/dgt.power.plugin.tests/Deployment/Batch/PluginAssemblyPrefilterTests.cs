// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Batch;
using Microsoft.Xrm.Sdk;

namespace dgt.power.plugin.tests.Deployment.Batch;

public sealed class PluginAssemblyPrefilterTests : IDisposable
{
    private readonly string _root = Directory.CreateTempSubdirectory("dgtp-prefilter-").FullName;

    public void Dispose() => Directory.Delete(_root, recursive: true);

    [Test]
    public async Task CanContainPlugins_AssemblyReferencingSdk_ReturnsTrue()
    {
        await Assert.That(PluginAssemblyPrefilter.CanContainPlugins(typeof(PluginAssemblyPrefilterTests).Assembly.Location)).IsTrue();
    }

    [Test]
    public async Task CanContainPlugins_ManagedAssemblyWithoutSdkReference_ReturnsFalse()
    {
        await Assert.That(PluginAssemblyPrefilter.CanContainPlugins(typeof(object).Assembly.Location)).IsFalse();
    }

    [Test]
    public async Task CanContainPlugins_SdkAssemblyItself_ReturnsFalse()
    {
        await Assert.That(PluginAssemblyPrefilter.CanContainPlugins(typeof(IPlugin).Assembly.Location)).IsFalse();
    }

    [Test]
    public async Task CanContainPlugins_NativeOrGarbageFile_ReturnsFalse()
    {
        var path = Path.Combine(_root, "native.dll");
        await File.WriteAllBytesAsync(path, "MZ not a managed assembly"u8.ToArray());

        await Assert.That(PluginAssemblyPrefilter.CanContainPlugins(path)).IsFalse();
    }
}
