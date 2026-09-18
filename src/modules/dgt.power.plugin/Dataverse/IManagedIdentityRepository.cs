// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Dataverse;

/// <summary>
/// Thin access to <c>managedidentity</c> records and linking them to plugin assemblies/packages.
/// </summary>
public interface IManagedIdentityRepository
{
    /// <summary>
    /// Finds an existing managed identity by application (client) id, or creates a new one linked
    /// to the given tenant id (environment tenant is used when <paramref name="tenantId"/> is
    /// <see langword="null"/>).
    /// </summary>
    /// <returns>The id of the existing or newly created managed identity record.</returns>
    Task<Guid> EnsureAsync(string clientId, string? tenantId, CancellationToken cancellationToken = default);

    /// <summary>Sets <c>pluginassembly.managedidentityid</c>.</summary>
    Task LinkToAssemblyAsync(Guid assemblyId, Guid managedIdentityId, CancellationToken cancellationToken = default);

    /// <summary>Sets <c>pluginpackage.managedidentityid</c>.</summary>
    Task LinkToPackageAsync(Guid packageId, Guid managedIdentityId, CancellationToken cancellationToken = default);
}
