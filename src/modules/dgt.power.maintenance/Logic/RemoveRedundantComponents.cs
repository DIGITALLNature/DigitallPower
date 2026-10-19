// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.maintenance.Model.Settings;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public class RemoveRedundantComponents : PowerCommand<RemoveRedundantComponentsVerb>
{
    private readonly IDataverseConnection _connection;
    private readonly IAnsiConsole _console;

    protected Dictionary<int, string> ComponentTypeLookup { get; } = new();

    public RemoveRedundantComponents(ICommandSpanFactory spans, IDataverseConnection connection, IAnsiConsole console) : base(spans, console)
    {
        _connection = connection;
        _console = console;

        var type = typeof(SolutionComponent.Options.ComponentType);
        var fields = type.GetFields();

        foreach (var componentType in fields.OrderBy(f => f.Name))
        {
            var value = (int)componentType.GetValue(null)!;
            ComponentTypeLookup.Add(value, componentType.Name);
        }
    }

    protected override async Task<bool> RunAsync(RemoveRedundantComponentsVerb args, CancellationToken cancellationToken)
    {
        var service = await _connection.GetServiceAsync();
        return await RunAsync(service, args, cancellationToken);
    }

    private async Task<bool> RunAsync(
        IOrganizationServiceAsync2 service,
        RemoveRedundantComponentsVerb args,
        CancellationToken cancellationToken)
    {
        var request = new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity
        };
        var response = (RetrieveAllEntitiesResponse)await service.ExecuteAsync(request, cancellationToken);
        var entities = response.EntityMetadata.ToList();

        var sourceComponents = new HashSet<Guid?>();
        foreach (var sourceSolution in args.SourceSolutions.Split(','))
        {
            cancellationToken.ThrowIfCancellationRequested();
            _console.WriteLine($"Fetch components for source {sourceSolution}");
            sourceComponents.UnionWith((await GetSolutionComponentsAsync(service, sourceSolution, cancellationToken)).Select(s => s.ObjectId));
        }
        _console.WriteLine($"Fetch components for target {args.TargetSolution}");
        var targetComponents = await GetSolutionComponentsAsync(service, args.TargetSolution, cancellationToken);

        var componentsToProcess = targetComponents
            .Where(component => component.ComponentType?.Value != null)
            .IntersectBy(sourceComponents, component => component.ObjectId)
            .OrderByDescending(component => component.ComponentType!.Value);

        foreach (var component in componentsToProcess)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (component.ComponentType?.Value == SolutionComponent.Options.ComponentType.Entity && !args.Entities)
            {
                var entity = entities.Single(e => e.MetadataId == component.ObjectId!.Value);
                _console.WriteLine($"Found entity {entity.LogicalName} - ignore");
                continue;
            }

            var nameColumn = new StringBuilder();
            if (component.RootSolutionComponentId != null && ((OptionSetValue)component.GetAttributeValue<AliasedValue>($"root.{SolutionComponent.LogicalNames.ComponentType}").Value).Value == SolutionComponent.Options.ComponentType.Entity)
            {
                var entity = entities.Single(e => e.MetadataId == (Guid?)component.GetAttributeValue<AliasedValue>($"root.{SolutionComponent.LogicalNames.ObjectId}").Value);
                nameColumn.Append(CultureInfo.InvariantCulture, $"({entity.LogicalName}) ");
            }

            switch (component.ComponentType?.Value)
            {
                case SolutionComponent.Options.ComponentType.Entity:
                    var entity = entities.Single(e => e.MetadataId == component.ObjectId!.Value);
                    nameColumn.Append(entity.LogicalName);
                    break;
                case SolutionComponent.Options.ComponentType.Attribute:
                    nameColumn.Append(component.GetAttributeValue<AliasedValue>("attr.logicalname").Value);
                    break;
                case SolutionComponent.Options.ComponentType.Workflow:
                    nameColumn.Append(component.GetAttributeValue<AliasedValue>("workflow.name").Value);
                    break;
            }

            if (!ComponentTypeLookup.TryGetValue(component.ComponentType!.Value, out var typeColumn))
            {
                typeColumn = component.ComponentType.Value.ToString(CultureInfo.InvariantCulture);
            }

            _console.WriteLine($"Remove Component {component.ObjectId:D} <{typeColumn}> {nameColumn}");
            if (!args.DryRun)
            {
                await service.ExecuteAsync(new RemoveSolutionComponentRequest
                {
                    ComponentId = component.ObjectId!.Value,
                    ComponentType = component.ComponentType.Value,
                    SolutionUniqueName = args.TargetSolution
                }, cancellationToken);
                _console.WriteLine(" - removed");
            }
        }

        return true;
    }

    private async Task<IReadOnlyList<SolutionComponent>> GetSolutionComponentsAsync(
        IOrganizationServiceAsync2 service,
        string uniqueName,
        CancellationToken cancellationToken)
    {
        var solution = await GetSolutionAsync(service, uniqueName, cancellationToken);

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
        var pagefilter = new FilterExpression(LogicalOperator.And);
        pagefilter.Conditions.Add(
            new ConditionExpression
            {
                AttributeName = SolutionComponent.LogicalNames.SolutionId,
                Operator = ConditionOperator.Equal,
                Values = { solution.Id }
            }
        );

        var componentTypeFilter = pagefilter.AddFilter(LogicalOperator.Or);
        foreach (var key in ComponentTypeLookup.Keys)
        {
            componentTypeFilter.AddCondition(SolutionComponent.LogicalNames.ComponentType, ConditionOperator.Equal, key);
        }

        pagequery.Criteria = pagefilter;

        var rootLink = pagequery.AddLink(
            SolutionComponent.EntityLogicalName,
            SolutionComponent.LogicalNames.RootSolutionComponentId,
            SolutionComponent.LogicalNames.SolutionComponentId,
            JoinOperator.LeftOuter
            );
        rootLink.Columns.AddColumns(SolutionComponent.LogicalNames.ComponentType, SolutionComponent.LogicalNames.ObjectId);
        rootLink.EntityAlias = "root";

        var attributeLink = pagequery.AddLink("attribute", "objectid", "attributeid", JoinOperator.LeftOuter);
        attributeLink.Columns.AddColumn("logicalname");
        attributeLink.EntityAlias = "attr";

        var workflowLink = pagequery.AddLink("workflow", "objectid", "workflowid", JoinOperator.LeftOuter);
        workflowLink.Columns.AddColumn("name");
        workflowLink.EntityAlias = "workflow";

        pagequery.Orders.Add(new OrderExpression
        {
            AttributeName = SolutionComponent.LogicalNames.ComponentType,
            OrderType = OrderType.Ascending
        });

        var results = await service.RetrieveAllAsync(pagequery, cancellationToken: cancellationToken);
        return results.Select(static entity => entity.ToEntity<SolutionComponent>()).ToArray();
    }

    private static async Task<Solution> GetSolutionAsync(
        IOrganizationServiceAsync2 service,
        string uniqueName,
        CancellationToken cancellationToken)
    {
        var query = new QueryExpression(Solution.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(Solution.LogicalNames.SolutionId)
        };
        query.Criteria.AddCondition(Solution.LogicalNames.UniqueName, ConditionOperator.Equal, uniqueName);
        var solutions = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        return solutions.Select(static entity => entity.ToEntity<Solution>()).Single();
    }

}
