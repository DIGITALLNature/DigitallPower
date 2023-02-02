using dgt.power.analyzer.Base;
using dgt.power.analyzer.Reports;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.dto;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.analyzer.Logic;

public sealed class NoActiveLayerAnalyze : BaseAnalyze
{
    public NoActiveLayerAnalyze(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(AnalyzeVerb args)
    {
        Tracer.Start(this);

        if (string.IsNullOrWhiteSpace(args.InlineData))
        {
            return Tracer.NotConfigured(this);
        }

        var summary = new AnalyzerSummary
        {
            Task = "noactivelayer",
            Anomalies = 0
        };

        var resultTable = new List<ActiveLayerLine>();

        var request = new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity
        };
        var response = (RetrieveAllEntitiesResponse)Connection.Execute(request);
        var entities = response.EntityMetadata.ToList();

        using var context = new DataContext(Connection);
        foreach (var uniqueName in args.InlineData.Split(','))
        {
            var solutionTag = new Rule($"solution unique name: [lime]{uniqueName}[/]");
            solutionTag.LeftJustified();

            AnsiConsole.Write(solutionTag);

            var components = GetSolutionComponents(context, uniqueName);

            var table = new Table().Centered();
            AnsiConsole.Live(table)
                .Start(ctx =>
                {
                    table.AddColumn("Component");
                    table.AddColumn("Order");
                    table.AddColumn("Name");
                    table.AddColumn("Solution");

                    ctx.Refresh();

                    foreach (var component in components)
                    {
                        var layers = GetSolutionLayers(component);
                        if (!layers.Any())
                        {
                            continue;
                        }

                        var first = layers.First();
                        if (first.MsdynSolutionname != "Active")
                        {
                            string componentName;
                            if (component.RootSolutionComponentId != null && ((OptionSetValue)component.GetAttributeValue<AliasedValue>($"root.{SolutionComponent.LogicalNames.ComponentType}").Value).Value == SolutionComponent.Options.ComponentType.Entity)
                            {
                                var entity = entities.Single(e => e.MetadataId == (Guid?)component.GetAttributeValue<AliasedValue>($"root.{SolutionComponent.LogicalNames.ObjectId}").Value);
                                componentName = $"{first.MsdynName} ({entity.LogicalName})";
                            }
                            else
                            {
                                componentName = first.MsdynName!;
                            }

                            table.AddRow($"{first.MsdynSolutioncomponentname}", $"{first.MsdynOrder:D}", componentName, $"{first.MsdynSolutionname}");
                            ctx.Refresh();

                            resultTable.Add(new ActiveLayerLine { Component = first.MsdynSolutioncomponentname, Order = first.MsdynOrder, Name = componentName, Solution = first.MsdynSolutionname });
                            summary.Anomalies++;
                        }
                    }
                });
        }

        if (args.GenerateSummaryFile)
        {
            WriteSummaryFile("NoActiveLayer", summary);
        }

        if (args.GenerateReportFile)
        {
            WriteReportFile("NoActiveLayer", resultTable.OrderBy(r => r.Solution).ThenBy(r => r.Component).ThenBy(r => r.Name).ThenBy(r => r.Order));
        }

        return Tracer.End(this, true);
    }

    private List<MsdynComponentlayer> GetSolutionLayers(SolutionComponent component)
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
                Values = { component.FormattedValues[SolutionComponent.LogicalNames.ComponentType] }
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
}
