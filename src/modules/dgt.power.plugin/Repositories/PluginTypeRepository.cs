// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="IPluginTypeRepository" />
public sealed class PluginTypeRepository(IOrganizationServiceAsync2 service) : IPluginTypeRepository
{
    public async Task<IReadOnlyList<RemotePluginType>> ListByAssemblyAsync(Guid assemblyId, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(PluginType.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(PluginType.LogicalNames.PluginTypeId, PluginType.LogicalNames.TypeName),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(PluginType.LogicalNames.PluginAssemblyId, ConditionOperator.Equal, assemblyId) }
            }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        return result.Entities
            .Select(e => e.ToEntity<PluginType>())
            .Select(t => new RemotePluginType(t.Id, t.TypeName!))
            .ToList();
    }

    public async Task<Guid> CreateAsync(Guid assemblyId, string typeName, string name, CancellationToken cancellationToken = default)
    {
        var pluginType = new PluginType
        {
            PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId),
            TypeName = typeName,
            Name = name
        };

        return await service.CreateAsync(pluginType, cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetDependentStepIdsAsync(Guid pluginTypeId, CancellationToken cancellationToken = default)
    {
        var request = new RetrieveDependenciesForDeleteRequest
        {
            ComponentType = IPluginTypeRepository.ComponentType,
            ObjectId = pluginTypeId
        };

        var response = (RetrieveDependenciesForDeleteResponse)await service.ExecuteAsync(request, cancellationToken);
        return response.EntityCollection.Entities
            .Select(e => e.ToEntity<Dependency>())
            .Where(d => d.DependentComponentType!.Value == ISdkMessageProcessingStepRepository.ComponentType)
            .Select(d => d.DependentComponentObjectId!.Value)
            .ToList();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await service.DeleteAsync(PluginType.EntityLogicalName, id, cancellationToken);
}
