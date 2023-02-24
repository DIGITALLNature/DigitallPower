using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.push.Model;
using Microsoft.Xrm.Sdk;
using NuGet.Packaging;
using Spectre.Console;

namespace dgt.power.push.Logic;

internal class PackageBuilder : BuilderBase
{
    public static Package BuildFromFile(string packageFile)
    {
        var result = default(Package);
        try
        {
            var content = Convert.ToBase64String(File.ReadAllBytes(packageFile));
            using var inputStream = new FileStream(packageFile, FileMode.Open);
            using var reader = new PackageArchiveReader(inputStream);
            var nuspec = reader.NuspecReader;

            result = new Package
            {
                Name = nuspec.GetId(),
                Version = nuspec.GetVersion().OriginalVersion,
                Content = content
            };
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine(e.RootMessage());
        }

        return result;
    }

    public static Package BuildFromCrm(string name, string version, string solutionPrefix, IOrganizationService service)
    {
        var result = new Package();
        var state = GetPluginPackage($"{solutionPrefix}_{name}", version, service, out var pluginPackage);
        result.State = state;
        if (state != AssemblyState.Create)
        {
            result.Name = pluginPackage!.Name!;
            result.Version = pluginPackage.Version!;
            result.Content = pluginPackage.Content!;
            result.Id = pluginPackage.PluginPackageId!.Value;
        }

        return result;
    }

    private static AssemblyState GetPluginPackage(string name, string version, IOrganizationService service, out PluginPackage? pluginPackage)
    {
        using var context = new DataContext(service);
        pluginPackage = default;
        var packages = (from pa in context.PluginPackageSet
            where pa.Name == name
            orderby pa.Version
            select pa).ToList();
        if (packages.Count == 0)
        {
            return AssemblyState.Create;
        }


        var split1 = GetVersion(version);
        var majorMinor1 = $"{split1[0]}.{split1[1]}";

        var package = packages.Single();

        pluginPackage = package;
        var split2 = GetVersion(package.Version!);
        var majorMinor2 = $"{split2[0]}.{split2[1]}";
        if (majorMinor1.Equals(majorMinor2, StringComparison.Ordinal))
        {
            pluginPackage = package;
            return AssemblyState.Update;
        }

        return AssemblyState.Upgrade;
    }

    public static List<Assembly?> UnpackPackage(string packageFile, IOrganizationService service)
    {
        using var inputStream = new FileStream(packageFile, FileMode.Open);
        using var reader = new PackageArchiveReader(inputStream);
        var results = new List<Assembly?>();

        foreach (var file in reader.GetFiles().Where(f => !f.Contains("/System.") && f.EndsWith(".dll")))
        {
            var filestream = reader.GetStream(file);
            results.Add(AssemblyBuilder.BuildFromStream(filestream, service));
        }

        return results;
    }
}
