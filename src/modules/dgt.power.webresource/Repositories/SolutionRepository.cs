// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.webresource.Remote;
using dgt.power.webresource;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.webresource.Repositories;

public sealed class SolutionRepository(IOrganizationServiceAsync2 service) : ISolutionRepository
{
    public async Task<IReadOnlyList<RemoteSolutionWebResource>> ListWebResourcesAsync(
        string solutionUniqueName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(solutionUniqueName);

        var solutionQuery = new QueryExpression(Solution.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(Solution.LogicalNames.SolutionId),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(Solution.LogicalNames.UniqueName, ConditionOperator.Equal, solutionUniqueName)
                }
            }
        };
        var solution = (await service.RetrieveMultipleAsync(solutionQuery, cancellationToken))
            .Entities
            .Select(entity => entity.ToEntity<Solution>())
            .SingleOrDefault();
        if (solution?.SolutionId is null)
        {
            throw new WebResourceSolutionNotFoundException(solutionUniqueName);
        }

        var query = new QueryExpression(WebResource.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                WebResource.LogicalNames.WebResourceId,
                WebResource.LogicalNames.Name,
                WebResource.LogicalNames.WebResourceType,
                WebResource.LogicalNames.IsManaged)
        };
        query.LinkEntities.Add(new LinkEntity(
            WebResource.EntityLogicalName,
            SolutionComponent.EntityLogicalName,
            WebResource.LogicalNames.WebResourceId,
            SolutionComponent.LogicalNames.ObjectId,
            JoinOperator.Inner)
        {
            LinkCriteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(
                        SolutionComponent.LogicalNames.SolutionId,
                        ConditionOperator.Equal,
                        solution.SolutionId.Value),
                    new ConditionExpression(
                        SolutionComponent.LogicalNames.ComponentType,
                        ConditionOperator.Equal,
                        SolutionComponent.Options.ComponentType.WebResource)
                }
            }
        });

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        return result.Entities.Select(entity =>
        {
            var webResource = entity.ToEntity<WebResource>();
            var name = webResource.Name;
            var type = webResource.WebResourceType?.Value;
            if (string.IsNullOrWhiteSpace(name) || type is null)
            {
                throw new InvalidOperationException("Dataverse returned a solution webresource without name or type.");
            }

            return new RemoteSolutionWebResource(
                webResource.Id,
                type.Value,
                name,
                webResource.IsManaged ?? false);
        }).ToList();
    }

    public async Task AddWebResourceAsync(
        Guid webresourceId,
        string solutionUniqueName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(solutionUniqueName);

        await service.ExecuteAsync(
            new AddSolutionComponentRequest
            {
                AddRequiredComponents = false,
                ComponentType = SolutionComponent.Options.ComponentType.WebResource,
                ComponentId = webresourceId,
                SolutionUniqueName = solutionUniqueName
            },
            cancellationToken);
    }
}
