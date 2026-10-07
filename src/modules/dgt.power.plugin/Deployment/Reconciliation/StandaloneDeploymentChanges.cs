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
    RemoteAssembly? AssemblyToDelete)
{
    internal bool HasChanges => ExistingAssemblyId is null || UpdateContent || Registrations.HasChanges || AssemblyToDelete is not null;
}
