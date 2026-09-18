// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Dataverse;

/// <inheritdoc cref="ICustomApiRepository" />
public sealed class CustomApiRepository(IOrganizationServiceAsync2 service) : ICustomApiRepository
{
    public async Task<Guid?> FindIdByUniqueNameAsync(string uniqueName, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(CustomAPI.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(false),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(CustomAPI.LogicalNames.UniqueName, ConditionOperator.Equal, uniqueName) }
            }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        return result.Entities.FirstOrDefault()?.Id;
    }

    public async Task<IReadOnlyList<Guid>> ListLinkedToPluginTypeAsync(Guid pluginTypeId, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(CustomAPI.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(false),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(CustomAPI.LogicalNames.PluginTypeId, ConditionOperator.Equal, pluginTypeId) }
            }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        return result.Entities.Select(e => e.Id).ToList();
    }

    public async Task LinkPluginTypeAsync(Guid customApiId, Guid pluginTypeId, CancellationToken cancellationToken = default)
    {
        var customApi = new CustomAPI(customApiId) { PluginTypeId = new EntityReference(PluginType.EntityLogicalName, pluginTypeId) };
        await service.UpdateAsync(customApi, cancellationToken);
    }

    public async Task UnlinkPluginTypeAsync(Guid customApiId, CancellationToken cancellationToken = default)
    {
        var customApi = new CustomAPI(customApiId) { PluginTypeId = null };
        await service.UpdateAsync(customApi, cancellationToken);
    }
}
