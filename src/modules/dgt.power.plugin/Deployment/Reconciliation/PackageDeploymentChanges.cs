// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Planning.Plans;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record PackageDeploymentChanges(
    LocalPluginPackage Desired,
    string DataverseName,
    RemotePackage? Existing,
    bool UpdateContent,
    IReadOnlyList<RemoteAssembly> ExpectedAssemblies,
    IReadOnlyList<PackageAssemblyChanges> Assemblies,
    IReadOnlyList<CustomApiChange> CustomApis,
    PackageSolutionChanges? Solution,
    IReadOnlyList<DataProviderChange> Providers)
{
    internal bool HasChanges
    {
        get
        {
            if (Existing is null || UpdateContent)
            {
                return true;
            }

            if (Providers.Count > 0)
            {
                return true;
            }

            return Assemblies.Any(assembly => assembly.Registrations.HasChanges) || CustomApis.Count > 0 || Solution is { HasChanges: true };
        }
    }
}
