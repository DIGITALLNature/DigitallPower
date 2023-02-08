using System.Collections;
using System.Globalization;
using System.Text.Json;
using CsvHelper;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.dto;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.analyzer.Base;

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

    protected List<SolutionComponent> GetSolutionComponents(DataContext context, string uniqueName)
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

        var rootLink = pagequery.AddLink(SolutionComponent.EntityLogicalName, SolutionComponent.LogicalNames.RootSolutionComponentId, SolutionComponent.LogicalNames.SolutionComponentId, JoinOperator.LeftOuter);
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
            Count = 5000,
            PageNumber = 1,
            PagingCookie = null
        };
        var components = new List<SolutionComponent>();
        while (true)
        {
            // Retrieve the page.
            var results = Connection.RetrieveMultiple(pagequery);
            components.AddRange(results.Entities.Select(e => e.ToEntity<SolutionComponent>()));
            if (results.MoreRecords)
            {
                pagequery.PageInfo.PageNumber++;
                pagequery.PageInfo.PagingCookie = results.PagingCookie;
            }
            else
            {
                break;
            }
        }

        return components;
    }
    protected List<MsdynComponentlayer> GetSolutionLayers(SolutionComponent component)
    {
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

    private static Solution GetSolution(DataContext context, string uniqueName)
    {
        var solution = (from su in context.SolutionSet
                        where su.UniqueName == uniqueName
                        select su).Single();
        return solution;
    }

    protected void WriteSummaryFile(string name, AnalyzerSummary summary)
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

    protected void WriteReportFile<T>(string name, T resultTable) where T : IEnumerable
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
}
