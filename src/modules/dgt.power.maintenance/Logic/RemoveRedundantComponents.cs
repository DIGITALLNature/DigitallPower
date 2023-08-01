// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Globalization;
using System.Text;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Model.Settings;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public class RemoveRedundantComponents : PowerLogic<RemoveRedundantComponentsVerb>
{
    protected Dictionary<int, string> ComponentTypeLookup { get; } = new();

    public RemoveRedundantComponents(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
        var type = typeof(SolutionComponent.Options.ComponentType);
        var fields = type.GetFields();

        foreach (var componentType in fields.OrderBy(f => f.Name))
        {
            var value = (int)componentType.GetValue(null)!;
            ComponentTypeLookup.Add(value, componentType.Name);
        }
    }

    protected override bool Invoke(RemoveRedundantComponentsVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);

        var request = new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity
        };
        var response = (RetrieveAllEntitiesResponse)Connection.Execute(request);
        var entities = response.EntityMetadata.ToList();

        using var context = new DataContext(Connection);
        var sourceComponents = GetSolutionComponents(context, args.SourceSolution).Select(s => s.ObjectId);
        var targetComponents = GetSolutionComponents(context, args.TargetSolution);

        foreach (var component in targetComponents.IntersectBy(sourceComponents,o => o.ObjectId).OrderByDescending(o => o.ComponentType.Value))
        {
            if (component.ComponentType?.Value == SolutionComponent.Options.ComponentType.Entity && !args.Entities)
            {
                var entity = entities.Single(e => e.MetadataId == component.ObjectId!.Value);
                AnsiConsole.WriteLine($"Found entity {entity.LogicalName} - ignore");

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

            AnsiConsole.WriteLine($"{component.ObjectId:D} <{typeColumn}> {nameColumn}");
            if (!args.DryRun)
            {
                Connection.Execute(new RemoveSolutionComponentRequest
                {
                    ComponentId = component.ObjectId!.Value,
                    ComponentType = component.ComponentType.Value,
                    SolutionUniqueName = args.TargetSolution
                });
                AnsiConsole.WriteLine(" - removed");
            }
        }

        return true;
    }

    protected IList<SolutionComponent> GetSolutionComponents(DataContext context, string uniqueName)
    {
        var solution = GetSolution(context, uniqueName);

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

        pagequery.PageInfo = new PagingInfo
        {
            Count = PageSize,
            PageNumber = 1,
            PagingCookie = null
        };
        var components = new List<SolutionComponent>();
        var moreRecords = true;
        while (moreRecords)
        {
            // Retrieve the page.
            var results = Connection.RetrieveMultiple(pagequery);
            components.AddRange(results.Entities.Select(e => e.ToEntity<SolutionComponent>()));
            if (results.MoreRecords)
            {
                pagequery.PageInfo.PageNumber++;
                pagequery.PageInfo.PagingCookie = results.PagingCookie;
            }

            moreRecords = results.MoreRecords;
        }

        return components;
    }

    private static Solution GetSolution(DataContext context, string uniqueName)
    {
        var solution = (from su in context.SolutionSet
            where su.UniqueName == uniqueName
            select su).Single();
        return solution;
    }

}
