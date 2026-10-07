// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Planning.Plans;
using dgt.power.plugin.Models;
using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record StandaloneDeploymentChanges(
    LocalAssembly Desired,
    Guid? ExistingAssemblyId,
    bool UpdateContent,
    RegistrationChanges Registrations,
    IReadOnlyDictionary<(string TypeName, LocalPluginStep Step), ResolvedSdkMessage> Messages,
    IReadOnlyList<RemoteAssembly> ExpectedAssemblies,
    RemoteAssembly? AssemblyToDelete,
    IReadOnlyList<CustomApiChange> CustomApis,
    StandaloneSolutionChanges? Solution,
    IReadOnlyList<DataProviderChange> Providers)
{
    internal bool HasChanges
    {
        get
        {
            if (ExistingAssemblyId is null || UpdateContent || AssemblyToDelete is not null)
            {
                return true;
            }

            return Registrations.HasChanges || CustomApis.Count > 0 || Solution is { HasChanges: true } || Providers.Count > 0;
        }
    }
}
