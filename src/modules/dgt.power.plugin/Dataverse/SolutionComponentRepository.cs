// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Dataverse;

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

    public async Task<string> GetPublisherPrefixAsync(string? solution, string defaultValue = "new", CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(solution))
        {
            return defaultValue;
        }

        var query = new QueryExpression(Solution.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(false),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(Solution.LogicalNames.UniqueName, ConditionOperator.Equal, solution) }
            },
            LinkEntities =
            {
                new LinkEntity(Solution.EntityLogicalName, Publisher.EntityLogicalName,
                    Solution.LogicalNames.PublisherId, Publisher.LogicalNames.PublisherId, JoinOperator.Inner)
                {
                    EntityAlias = "publisher",
                    Columns = new ColumnSet(Publisher.LogicalNames.CustomizationPrefix)
                }
            }
        };

        var entity = (await service.RetrieveMultipleAsync(query, cancellationToken)).Entities.FirstOrDefault();
        return entity?.GetAttributeValue<AliasedValue>($"publisher.{Publisher.LogicalNames.CustomizationPrefix}")?.Value as string ?? defaultValue;
    }
}
