// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using dgt.power.common.Extensions;
using NuGet.Packaging;
using Spectre.Console;

namespace dgt.power.plugin.Local;

/// <summary>
/// Parses a local plugin package (.nupkg) into a <see cref="LocalPluginPackage"/>: the package
/// metadata plus every plugin assembly bundled inside it. Purely local: never touches Dataverse.
/// </summary>
internal sealed class PluginPackageReader(IAnsiConsole console)
{
    private readonly AssemblyReflectionReader _assemblyReader = new(console);

    /// <summary>
    /// Reads only the package metadata (id/version/content) without unpacking bundled assemblies.
    /// </summary>
    public LocalPackage? ReadPackage(string packageFile)
    {
        try
        {
            var content = Convert.ToBase64String(File.ReadAllBytes(packageFile));
            using var inputStream = new FileStream(packageFile, FileMode.Open);
            using var reader = new PackageArchiveReader(inputStream);
            var nuspec = reader.NuspecReader;

            return new LocalPackage(
                nuspec.GetId(),
                nuspec.GetVersion().OriginalVersion ?? nuspec.GetVersion().ToFullString(),
                content);
        }
        catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
        {
            console.MarkupLine(Markup.Escape(e.RootMessage()));
            return null;
        }
    }

    /// <summary>
    /// Reads the package metadata and unpacks every bundled plugin assembly.
    /// </summary>
    public LocalPluginPackage? Read(string packageFile)
    {
        var package = ReadPackage(packageFile);
        if (package == null)
        {
            return null;
        }

        using var inputStream = new MemoryStream(Convert.FromBase64String(package.Content));
        using var reader = new PackageArchiveReader(inputStream);
        var assemblies = new List<LocalAssembly>();

        var tempPath = Path.Combine(Path.GetTempPath(), "dgtp.plugin", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempPath);
        try
        {
            var files = reader.GetFiles()
                .Where(f => !f.Contains("/System.", StringComparison.Ordinal) &&
                            !f.Contains("/Microsoft", StringComparison.Ordinal) &&
                            f.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var file in files)
            {
                using var fileStream = reader.GetStream(file);
                using var tempFile = new FileStream(Path.Combine(tempPath, Path.GetFileName(file)), FileMode.Create, FileAccess.Write);
                fileStream.CopyTo(tempFile);
            }

            var env = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll")
                .Concat(Directory.GetFiles(Path.GetDirectoryName(typeof(PluginPackageReader).Assembly.Location)!, "*.dll"))
                .Concat(Directory.GetFiles(tempPath, "*.dll"))
                .ToList();
            using var loadContext = new MetadataLoadContext(new PathAssemblyResolver(env));

            foreach (var dll in Directory.GetFiles(tempPath, "*.dll"))
            {
                var assembly = _assemblyReader.Read(dll, loadContext);
                if (assembly is null)
                {
                    continue;
                }

                if (assembly.Kind == LocalAssemblyKind.Undefined)
                {
                    console.MarkupLine(CultureInfo.InvariantCulture,
                        "Assembly [bold green]{0} ({1})[/] [bold red]does not contain[/] any plugins - skipping",
                        assembly.Name, assembly.Version);
                    continue;
                }

                assemblies.Add(assembly);
            }
        }
        finally
        {
            Directory.Delete(tempPath, true);
        }

        return new LocalPluginPackage(package, assemblies);
    }
}
