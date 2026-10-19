// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.analyzer.Base;

/// <summary>
/// Reads solution components, their layers and solutions for the analyzers. Holds no Dataverse
/// connection; every method receives the async service it should use.
/// </summary>
public sealed class SolutionComponentReader
{
    private const int PageSize = 5000;

    public SolutionComponentReader()
    {
        var type = typeof(SolutionComponent.Options.ComponentType);
        var fields = type.GetFields();

        foreach (var componentType in fields.OrderBy(f => f.Name))
        {
            var value = (int)componentType.GetValue(null)!;
            ComponentTypeLookup.Add(value, componentType.Name);
        }
    }

    public Dictionary<int, string> ComponentTypeLookup { get; } = new();

    /// <summary>
    /// Retrieves the solution components for a given solution.
    /// </summary>
    /// <param name="service">The async organization service.</param>
    /// <param name="uniqueName">The unique name of the solution.</param>
    /// <returns>A list of solution components.</returns>
    public async Task<IList<SolutionComponent>> GetSolutionComponentsAsync(IOrganizationServiceAsync2 service, string uniqueName, CancellationToken cancellationToken)
    {
        var solution = await GetSolutionAsync(service, uniqueName, cancellationToken);
        return await GetSolutionComponentsAsync(service, solution.Id, cancellationToken);
    }

    /// <summary>
    /// Retrieves solution components based on the provided solution Id.
    /// </summary>
    /// <param name="service">The async organization service.</param>
    /// <param name="solutionId">The Id of the solution to retrieve components for.</param>
    /// <returns>A list of SolutionComponent objects.</returns>
    public async Task<IList<SolutionComponent>> GetSolutionComponentsAsync(IOrganizationServiceAsync2 service, Guid solutionId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(service);

        // Create query expression to retrieve solution components
        var pagequery = new QueryExpression
        {
            EntityName = SolutionComponent.EntityLogicalName,
            NoLock = true,
            ColumnSet = new ColumnSet(
                SolutionComponent.LogicalNames.ComponentType,
                SolutionComponent.LogicalNames.RootComponentBehavior,
                SolutionComponent.LogicalNames.RootSolutionComponentId,
                SolutionComponent.LogicalNames.ObjectId,
                SolutionComponent.LogicalNames.IsMetadata,
                SolutionComponent.LogicalNames.SolutionId
            )
        };

        // Create filter expression based on solution Id
        var pagefilter = new FilterExpression(LogicalOperator.And);
        pagefilter.Conditions.Add(
            new ConditionExpression
            {
                AttributeName = SolutionComponent.LogicalNames.SolutionId,
                Operator = ConditionOperator.Equal,
                Values = { solutionId }
            }
        );

        // Add component type filter conditions
        var componentTypeFilter = pagefilter.AddFilter(LogicalOperator.Or);
        foreach (var key in ComponentTypeLookup.Keys)
        {
            componentTypeFilter.AddCondition(SolutionComponent.LogicalNames.ComponentType, ConditionOperator.Equal, key);
        }

        pagequery.Criteria = pagefilter;

        // Add link to root solution component
        var rootLink = pagequery.AddLink(
            SolutionComponent.EntityLogicalName,
            SolutionComponent.LogicalNames.RootSolutionComponentId,
            SolutionComponent.LogicalNames.SolutionComponentId,
            JoinOperator.LeftOuter
            );
        rootLink.Columns.AddColumns(SolutionComponent.LogicalNames.ComponentType, SolutionComponent.LogicalNames.ObjectId);
        rootLink.EntityAlias = "root";

        // Add link to attribute entity
        var attributeLink = pagequery.AddLink("attribute", "objectid", "attributeid", JoinOperator.LeftOuter);
        attributeLink.Columns.AddColumn("logicalname");
        attributeLink.EntityAlias = "attr";

        // Add link to workflow entity
        var workflowLink = pagequery.AddLink("workflow", "objectid", "workflowid", JoinOperator.LeftOuter);
        workflowLink.Columns.AddColumn("name");
        workflowLink.EntityAlias = "workflow";

        // Add ordering by component type
        pagequery.Orders.Add(new OrderExpression
        {
            AttributeName = SolutionComponent.LogicalNames.ComponentType,
            OrderType = OrderType.Ascending
        });

        // Set paging information
        var results = await service.RetrieveAllAsync(pagequery, PageSize, cancellationToken);
        return results.Select(e => e.ToEntity<SolutionComponent>()).ToList();
    }

    /// <summary>
    /// This method retrieves the top layer that is not active from a list of component layers.
    /// If there is only one layer, it is returned.
    /// If the first layer is active, it recursively calls itself with the remaining layers.
    /// </summary>
    /// <param name="layers">The list of component layers to search through.</param>
    /// <returns>The top component layer that is not active.</returns>
    public static MsdynComponentlayer GetTopNotActiveLayer(IList<MsdynComponentlayer> layers)
    {
        ArgumentNullException.ThrowIfNull(layers);
        // If there is only one layer, return it
        if (layers.Count == 1)
        {
            return layers.Single();
        }

        // Get the first layer
        var first = layers[0];

        // If the first layer is active, recursively call this method with the remaining layers
        // Otherwise, return the first layer
        return first.MsdynSolutionname == "Active" ? GetTopNotActiveLayer(layers.Skip(1).ToList()) : first;
    }

    public async Task<IList<MsdynComponentlayer>> GetSolutionLayersAsync(IOrganizationServiceAsync2 service, SolutionComponent component, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(service);
        Debug.Assert(component != null, nameof(component) + " != null");

        var query = new QueryExpression
        {
            EntityName = MsdynComponentlayer.EntityLogicalName,
            NoLock = true,
            ColumnSet = new ColumnSet(
                MsdynComponentlayer.LogicalNames.MsdynName,
                MsdynComponentlayer.LogicalNames.MsdynSolutioncomponentname,
                MsdynComponentlayer.LogicalNames.MsdynSolutionname,
                MsdynComponentlayer.LogicalNames.MsdynOrder
            )
        };
        var filter = new FilterExpression(LogicalOperator.And);
        filter.Conditions.Add(
            new ConditionExpression
            {
                AttributeName = MsdynComponentlayer.LogicalNames.MsdynSolutioncomponentname,
                Operator = ConditionOperator.Equal,
                Values = { ComponentTypeLookup[component.ComponentType!.Value] }
            }
        );
        filter.Conditions.Add(
            new ConditionExpression
            {
                AttributeName = MsdynComponentlayer.LogicalNames.MsdynComponentid,
                Operator = ConditionOperator.Equal,
                Values = { $"{component.ObjectId:B}" }
            }
        );
        query.Criteria = filter;

        query.AddOrder(MsdynComponentlayer.LogicalNames.MsdynOrder, OrderType.Descending);
        var layers = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Select(s => s.ToEntity<MsdynComponentlayer>()).ToList();
        return layers;
    }

    public static async Task<Solution> GetSolutionAsync(IOrganizationServiceAsync2 service, string uniqueName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(uniqueName);
        var query = new QueryExpression(Solution.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                Solution.LogicalNames.SolutionId,
                Solution.LogicalNames.UniqueName,
                Solution.LogicalNames.FriendlyName,
                Solution.LogicalNames.IsManaged,
                Solution.LogicalNames.ParentSolutionId),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(Solution.LogicalNames.UniqueName, ConditionOperator.Equal, uniqueName) }
            }
        };
        var solutions = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        return solutions.Select(entity => entity.ToEntity<Solution>()).Single();
    }

    public static string GetComponentName(SolutionComponent component, IEnumerable<EntityMetadata> entities, MsdynComponentlayer first)
    {
        ArgumentNullException.ThrowIfNull(component);
        ArgumentNullException.ThrowIfNull(first);
        string componentName;
        if (component.RootSolutionComponentId != null &&
            ((OptionSetValue)component
                .GetAttributeValue<AliasedValue>($"root.{SolutionComponent.LogicalNames.ComponentType}").Value).Value ==
            SolutionComponent.Options.ComponentType.Entity)
        {
            var entity = entities.Single(e =>
                e.MetadataId == (Guid?)component
                    .GetAttributeValue<AliasedValue>($"root.{SolutionComponent.LogicalNames.ObjectId}").Value);
            componentName = $"{first.MsdynName} ({entity.LogicalName})";
        }
        else
        {
            componentName = first.MsdynName!;
        }

        return componentName;
    }
}
