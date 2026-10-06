// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;
using dgt.power.common.Extensions;
using NuGet.Packaging;
using NuGet.Frameworks;
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
    private LocalPackage? ReadPackage(string packageFile)
    {
        try
        {
            var packageBytes = File.ReadAllBytes(packageFile);
            var content = Convert.ToBase64String(packageBytes);
            using var inputStream = new FileStream(packageFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new PackageArchiveReader(inputStream);
            var nuspec = reader.NuspecReader;

            return new LocalPackage(nuspec.GetId(), nuspec.GetVersion().OriginalVersion ?? nuspec.GetVersion().ToFullString(), content, Convert.ToHexString(SHA256.HashData(packageBytes)));
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
            var files = SelectAssemblyFiles(reader.GetFiles());

            foreach (var file in files)
            {
                using var fileStream = reader.GetStream(file);
                using var tempFile = new FileStream(Path.Combine(tempPath, Path.GetFileName(file)), FileMode.Create, FileAccess.Write);
                fileStream.CopyTo(tempFile);
            }

            using var loadContext = MetadataLoadContextFactory.Create(tempPath);

            foreach (var dll in Directory.GetFiles(tempPath, "*.dll"))
            {
                var assembly = _assemblyReader.Read(dll, loadContext) ??
                               throw new AssemblyException($"Assembly '{Path.GetFileName(dll)}' in package '{package.Name}' could not be read; aborting to avoid a partial deployment plan.");

                if (assembly.Kind == LocalAssemblyKind.None)
                {
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

    internal static IReadOnlyList<string> SelectAssemblyFiles(IEnumerable<string> packageFiles)
    {
        var assemblyFiles = packageFiles.Where(file => file.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            .Where(file => !file.Contains("/System.", StringComparison.Ordinal) && !file.Contains("/Microsoft", StringComparison.Ordinal)).ToList();
        var frameworkAssets = assemblyFiles.Select(file => (File: file, Parts: file.Split('/')))
            .Where(item => item.Parts.Length >= 3 && string.Equals(item.Parts[0], "lib", StringComparison.OrdinalIgnoreCase))
            .Select(item => (item.File, Framework: NuGetFramework.ParseFolder(item.Parts[1])))
            .ToList();

        List<string> selectedFiles;
        if (frameworkAssets.Count > 0)
        {
            // Select Framework assets for metadata inspection; Dataverse validates deployment support.
            var targetFramework = NuGetFramework.Parse("net48");
            var nearestFramework = new FrameworkReducer().GetNearest(
                targetFramework,
                frameworkAssets
                    .Where(asset => !asset.Framework.IsUnsupported)
                    .Select(asset => asset.Framework)
                    .Distinct());
            if (nearestFramework is null)
            {
                throw new InvalidOperationException($"The package has no DLL assets compatible with the local inspection target '{targetFramework.GetShortFolderName()}'.");
            }

            selectedFiles = frameworkAssets.Where(asset => asset.Framework.Equals(nearestFramework)).Select(asset => asset.File).ToList();
        }
        else
        {
            selectedFiles = assemblyFiles.Where(file => !file.Contains('/', StringComparison.Ordinal)).ToList();
        }

        var duplicateNames = selectedFiles.GroupBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase).Where(group => group.Count() > 1).Select(group => group.Key).ToList();
        if (duplicateNames.Count > 0)
        {
            throw new InvalidOperationException($"The selected package framework contains ambiguous DLL names: {string.Join(", ", duplicateNames)}.");
        }

        return selectedFiles;
    }
}
