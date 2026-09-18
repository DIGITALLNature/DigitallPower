// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Dataverse;

/// <inheritdoc cref="IManagedIdentityRepository" />
public sealed class ManagedIdentityRepository(IOrganizationServiceAsync2 service) : IManagedIdentityRepository
{
    public async Task<Guid> EnsureAsync(string clientId, string? tenantId, CancellationToken cancellationToken = default)
    {
        var applicationId = Guid.Parse(clientId);

        var query = new QueryExpression(ManagedIdentity.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(ManagedIdentity.LogicalNames.ApplicationId, ManagedIdentity.LogicalNames.TenantId),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(ManagedIdentity.LogicalNames.ApplicationId, ConditionOperator.Equal, applicationId) }
            }
        };

        var existing = (await service.RetrieveMultipleAsync(query, cancellationToken)).Entities.FirstOrDefault();
        if (existing is not null)
        {
            return existing.Id;
        }

        var managedIdentity = new ManagedIdentity
        {
            ApplicationId = applicationId,
            CredentialSource = new OptionSetValue(ManagedIdentity.Options.CredentialSource.IsManaged),
            SubjectScope = new OptionSetValue(ManagedIdentity.Options.SubjectScope.EnviornmentScope)
        };

        if (!string.IsNullOrWhiteSpace(tenantId))
        {
            managedIdentity.TenantId = Guid.Parse(tenantId);
        }

        return await service.CreateAsync(managedIdentity, cancellationToken);
    }

    public async Task LinkToAssemblyAsync(Guid assemblyId, Guid managedIdentityId, CancellationToken cancellationToken = default) =>
        await service.UpdateAsync(new PluginAssembly(assemblyId)
        {
            ManagedIdentityId = new EntityReference(ManagedIdentity.EntityLogicalName, managedIdentityId)
        }, cancellationToken);

    public async Task LinkToPackageAsync(Guid packageId, Guid managedIdentityId, CancellationToken cancellationToken = default) =>
        await service.UpdateAsync(new PluginPackage(packageId)
        {
            Managedidentityid = new EntityReference(ManagedIdentity.EntityLogicalName, managedIdentityId)
        }, cancellationToken);
}
