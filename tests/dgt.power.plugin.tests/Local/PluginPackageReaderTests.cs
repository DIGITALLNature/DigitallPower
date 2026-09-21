// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using NuGet.Packaging;
using NuGet.Versioning;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Local;

public class PluginPackageReaderTests
{
    [Test]
    public async Task Read_PackageWithNonPluginDependencyDll_OnlyReturnsTheAssemblyContainingPlugins()
    {
        var pluginDllPath = typeof(SamplePlugin).Assembly.Location;
        var nonPluginDllPath = typeof(dgt.power.common.IPowerLogic).Assembly.Location;

        var nupkgPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.nupkg");
        try
        {
            CreatePackage(nupkgPath, pluginDllPath, nonPluginDllPath);

            var console = new TestConsole();
            var reader = new PluginPackageReader(console);

            var result = reader.Read(nupkgPath);

            using (Assert.Multiple())
            {
                await Assert.That(result).IsNotNull();
                await Assert.That(result!.Assemblies).Count().IsEqualTo(1);
                await Assert.That(result.Assemblies[0].Name).IsEqualTo(Path.GetFileNameWithoutExtension(pluginDllPath));
                await Assert.That(console.Output).Contains("does not contain");
                await Assert.That(console.Output).Contains(Path.GetFileNameWithoutExtension(nonPluginDllPath));
            }
        }
        finally
        {
            File.Delete(nupkgPath);
        }
    }

    private static void CreatePackage(string nupkgPath, params string[] dllPaths)
    {
        var builder = new PackageBuilder
        {
            Id = "test.package",
            Version = NuGetVersion.Parse("1.0.0"),
            Description = "Test package for PluginPackageReaderTests"
        };
        builder.Authors.Add("test");

        foreach (var dllPath in dllPaths)
        {
            builder.Files.Add(new PhysicalPackageFile
            {
                SourcePath = dllPath,
                TargetPath = $"lib/net10.0/{Path.GetFileName(dllPath)}"
            });
        }

        using var stream = File.Create(nupkgPath);
        builder.Save(stream);
    }
}
