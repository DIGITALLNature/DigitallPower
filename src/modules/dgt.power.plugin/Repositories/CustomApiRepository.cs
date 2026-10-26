// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common.Extensions;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="ICustomApiRepository" />
public sealed class CustomApiRepository(IOrganizationServiceAsync2 service) : ICustomApiRepository
{
    public async Task<Guid?> FindIdByUniqueNameAsync(string uniqueName, CancellationToken cancellationToken = default)
    {
        return (await FindByUniqueNameAsync(uniqueName, cancellationToken))?.Id;
    }

    internal async Task<(Guid Id, Guid? PluginTypeId)?> FindByUniqueNameAsync(string uniqueName, CancellationToken cancellationToken)
    {
        var query = new QueryExpression(CustomAPI.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(CustomAPI.LogicalNames.PluginTypeId),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(CustomAPI.LogicalNames.UniqueName, ConditionOperator.Equal, uniqueName) }
            }
        };

        var result = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        if (result.Count > 1)
        {
            throw new InvalidOperationException($"Custom API '{uniqueName}' has multiple matching records; its handler cannot be resolved unambiguously.");
        }

        return result.Count == 0 ? null : (result[0].Id, result[0].GetAttributeValue<EntityReference>(CustomAPI.LogicalNames.PluginTypeId)?.Id);
    }

    public async Task<IReadOnlyList<Guid>> ListLinkedToPluginTypeAsync(Guid pluginTypeId, CancellationToken cancellationToken = default)
    {
        return (await ListAssociationsAsync(pluginTypeId, cancellationToken)).Select(api => api.Id).ToList();
    }

    internal async Task<IReadOnlyList<(Guid Id, string Name)>> ListAssociationsAsync(Guid pluginTypeId, CancellationToken cancellationToken)
    {
        var query = new QueryExpression(CustomAPI.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(CustomAPI.LogicalNames.UniqueName),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(CustomAPI.LogicalNames.PluginTypeId, ConditionOperator.Equal, pluginTypeId) }
            }
        };

        var result = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        return result.Select(entity => (entity.Id, entity.GetAttributeValue<string>(CustomAPI.LogicalNames.UniqueName) ?? entity.Id.ToString())).ToList();
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
