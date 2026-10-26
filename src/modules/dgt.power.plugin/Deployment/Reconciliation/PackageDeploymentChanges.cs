// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Local;
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
    internal IReadOnlyList<RemovedPackageAssembly> RemovedAssemblies { get; init; } = [];

    internal int DestructiveCount => Assemblies.Sum(assembly => assembly.Registrations.DestructiveCount) +
        RemovedAssemblies.Sum(assembly => 1 + assembly.Registrations.DestructiveCount) +
        CustomApis.Count(api => api.PreviousHandlerId is not null) + Providers.Sum(provider => provider.DestructiveCount);

    internal bool HasChanges
    {
        get
        {
            if (Existing is null || UpdateContent || RemovedAssemblies.Count > 0)
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
