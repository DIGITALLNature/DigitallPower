// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using CsvHelper;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.dto;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.analyzer.Base;

#pragma warning disable S1200
public abstract class BaseAnalyze : PowerLogic<AnalyzeVerb>
{
    protected BaseAnalyze(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
        var type = typeof(SolutionComponent.Options.ComponentType);
        var fields = type.GetFields();

        foreach (var componentType in fields.OrderBy(f => f.Name))
        {
            var value = (int)componentType.GetValue(null)!;
            ComponentTypeLookup.Add(value, componentType.Name);
        }
    }

    protected Dictionary<int, string> ComponentTypeLookup { get; } = new();
    internal  static string ResultFolder { get; } = "Analyze";

    /// <summary>
    /// Retrieves the solution components for a given solution.
    /// </summary>
    /// <param name="context">The data context.</param>
    /// <param name="uniqueName">The unique name of the solution.</param>
    /// <returns>A list of solution components.</returns>
    protected IList<SolutionComponent> GetSolutionComponents(DataContext context, string uniqueName)
    {
        // Get the solution using the provided unique name
        var solution = GetSolution(context, uniqueName);

        // Retrieve the solution components using the solution ID
        return GetSolutionComponents(solution.Id);
    }

    /// <summary>
    /// Retrieves solution components based on the provided solution Id.
    /// </summary>
    /// <param name="solutionId">The Id of the solution to retrieve components for.</param>
    /// <returns>A list of SolutionComponent objects.</returns>
    protected IList<SolutionComponent> GetSolutionComponents(Guid solutionId)
    {
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

    /// <summary>
    /// This method retrieves the top layer that is not active from a list of component layers.
    /// If there is only one layer, it is returned.
    /// If the first layer is active, it recursively calls itself with the remaining layers.
    /// </summary>
    /// <param name="layers">The list of component layers to search through.</param>
    /// <returns>The top component layer that is not active.</returns>
    protected static MsdynComponentlayer GetTopNotActiveLayer(IList<MsdynComponentlayer> layers)
    {
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

    protected IList<MsdynComponentlayer> GetSolutionLayers(SolutionComponent component)
    {
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
        var layers = Connection.RetrieveMultiple(query).Entities.Select(s => s.ToEntity<MsdynComponentlayer>()).ToList();
        return layers;
    }

    protected static Solution GetSolution(DataContext context, string uniqueName)
    {
        var solution = (from su in context.SolutionSet
                        where su.UniqueName == uniqueName
                        select su).Single();
        return solution;
    }

    protected static void WriteSummaryFile(string name, AnalyzerSummary summary)
    {
        var content = JsonSerializer.SerializeToUtf8Bytes(summary);

        if (!Directory.Exists(ResultFolder))
        {
            Directory.CreateDirectory(ResultFolder);
        }


        var work = Path.Combine(ResultFolder, $"{Guid.NewGuid():N}-{name}-summary");
        var file = Path.Combine(ResultFolder, $"{name}-summary.json");


        File.WriteAllBytes(work, content);
        if (File.Exists(file))
        {
            File.Replace(work, file, null, false);
        }
        else
        {
            File.Move(work, file);
        }
    }

    protected static void WriteReportFile<T>(string name, T resultTable) where T : IEnumerable
    {
        if (!Directory.Exists(ResultFolder))
        {
            Directory.CreateDirectory(ResultFolder);
        }

        var work = Path.Combine(ResultFolder, $"{Guid.NewGuid():N}-{name}-result");
        var file = Path.Combine(ResultFolder, $"{name}-result.csv");

        using (var writer = new StreamWriter(work))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(resultTable);
        }

        if (File.Exists(file))
        {
            File.Replace(work, file, null, false);
        }
        else
        {
            File.Move(work, file);
        }
    }


    protected static string GetComponentName(SolutionComponent component, IEnumerable<EntityMetadata> entities, MsdynComponentlayer first)
    {
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
