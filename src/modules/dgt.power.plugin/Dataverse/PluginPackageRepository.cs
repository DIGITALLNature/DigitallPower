// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Planning;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Dataverse;

/// <inheritdoc cref="IPluginPackageRepository" />
public sealed class PluginPackageRepository(IOrganizationServiceAsync2 service) : IPluginPackageRepository
{
    public async Task<RemotePackage?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(PluginPackage.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(PluginPackage.LogicalNames.PluginPackageId),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(PluginPackage.LogicalNames.Name, ConditionOperator.EndsWith, name) }
            },
            Orders = { new OrderExpression(PluginPackage.LogicalNames.CreatedOn, OrderType.Descending) }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        var entity = result.Entities.FirstOrDefault();
        return entity is null ? null : new RemotePackage(entity.Id);
    }

    public async Task<Guid> CreateAsync(string name, string version, string content, CancellationToken cancellationToken = default)
    {
        var package = new PluginPackage { Name = name, Version = version, Content = content };
        return await service.CreateAsync(package, cancellationToken);
    }

    public async Task UpdateContentAsync(Guid id, string content, CancellationToken cancellationToken = default)
    {
        var package = new PluginPackage(id) { Content = content };
        await service.UpdateAsync(package, cancellationToken);
    }
}
