// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="ISolutionComponentRepository" />
public sealed class SolutionComponentRepository(IOrganizationServiceAsync2 service) : ISolutionComponentRepository
{
    public async Task<int?> GetComponentTypeAsync(string entityLogicalName, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(SolutionComponentDefinition.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(SolutionComponentDefinition.LogicalNames.SolutionComponentType),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(SolutionComponentDefinition.LogicalNames.PrimaryEntityName,
                        ConditionOperator.Equal, entityLogicalName)
                }
            }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        return result.Entities.FirstOrDefault()?.ToEntity<SolutionComponentDefinition>().SolutionComponentType;
    }

    public async Task AddToSolutionAsync(int componentType, Guid componentId, string solutionUniqueName, CancellationToken cancellationToken = default)
    {
        var request = new AddSolutionComponentRequest
        {
            AddRequiredComponents = false,
            ComponentType = componentType,
            ComponentId = componentId,
            SolutionUniqueName = solutionUniqueName
        };

        await service.ExecuteAsync(request, cancellationToken);
    }

}
