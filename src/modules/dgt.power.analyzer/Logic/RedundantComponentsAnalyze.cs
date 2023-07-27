// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text;
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

public sealed class RedundantComponentsAnalyze : BaseAnalyze
{
    public RedundantComponentsAnalyze(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
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
            Task = "redundantcomponents",
            Anomalies = 0
        };
        var resultTable = new List<SolutionRedundantComponentLine>();

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
                    table.AddColumn("Type");
                    table.AddColumn("Name");
                    table.AddColumn("Solution");
                    table.AddColumn("Solution Unique");

                    ctx.Refresh();


                    foreach (var component in components)
                    {
                        var otherSolutions = GetSolutionsForComponent(component, args.NotePatch);
                        foreach (var otherSolution in otherSolutions)
                        {
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

                            table.AddRow($"{component.ObjectId:D}", typeColumn, nameColumn.ToString(), otherSolution.FriendlyName!, otherSolution.UniqueName!);
                            ctx.Refresh();

                            summary.Anomalies++;
                            resultTable.Add(new SolutionRedundantComponentLine
                            {
                                OriginSolution = uniqueName,
                                ObjectId = component.ObjectId.GetValueOrDefault(),
                                AlsoInSolution = otherSolution.UniqueName!,
                                ObjectType = typeColumn
                            });
                        }
                    }
                });

            var groupedMetrics = resultTable.Where(r => r.OriginSolution == uniqueName).GroupBy(rc => rc.AlsoInSolution).ToList();

            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine($"Found {resultTable.Count(r => r.OriginSolution == uniqueName)} potential redudant components in {groupedMetrics.Count} unique Solutions:");
            var summaryTable = new Table();


            summaryTable.AddColumn(new TableColumn("Solution").Centered());
            summaryTable.AddColumn("Count");

            foreach (var groupedMetric in groupedMetrics.OrderBy(m => m.Key))
            {
                summaryTable.AddRow(groupedMetric.Key!, $"[green]{groupedMetric.Count()}[/]");
            }

            AnsiConsole.Write(summaryTable);
        }

        if (args.GenerateSummaryFile)
        {
            WriteSummaryFile("RedundantComponents", summary);
        }

        if (args.GenerateReportFile)
        {
            WriteReportFile("RedundantComponents", resultTable.OrderBy(r => r.OriginSolution).ThenBy(r => r.AlsoInSolution));
        }

        return true;
    }

    private List<Solution> GetSolutionsForComponent(SolutionComponent component, bool notePatches)
    {
        var parallelSolutionComponents = GetParallelSolutionComponentsForComponent(component, notePatches);

        var result = new List<Solution>();
        foreach (var solutionComponent in parallelSolutionComponents)
        {
            var friendlySolutionName = (string)solutionComponent.GetAttributeValue<AliasedValue>($"solution.{Solution.LogicalNames.FriendlyName}").Value;
            var uniqueSolutionName = (string)solutionComponent.GetAttributeValue<AliasedValue>($"solution.{Solution.LogicalNames.UniqueName}").Value;
            result.Add(new Solution(solutionComponent.SolutionId!.Id)
            {
                FriendlyName = friendlySolutionName,
                UniqueName = uniqueSolutionName
            });
        }

        return result;
    }

    private List<SolutionComponent> GetParallelSolutionComponentsForComponent(SolutionComponent originComponent, bool notePatches)
    {
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
                SolutionComponent.LogicalNames.SolutionId,
                SolutionComponent.LogicalNames.RootSolutionComponentId
            )
        };
        var pagefilter = new FilterExpression(LogicalOperator.And);
        pagefilter.Conditions.Add(
            new ConditionExpression
            {
                AttributeName = SolutionComponent.LogicalNames.SolutionId,
                Operator = ConditionOperator.NotEqual,
                Values = { originComponent.SolutionId!.Id }
            }
        );
        pagefilter.Conditions.Add(
            new ConditionExpression
            {
                AttributeName = SolutionComponent.LogicalNames.ComponentType,
                Operator = ConditionOperator.Equal,
                Values = { originComponent.ComponentType!.Value }
            }
        );
        pagefilter.Conditions.Add(
            new ConditionExpression
            {
                AttributeName = SolutionComponent.LogicalNames.ObjectId,
                Operator = ConditionOperator.Equal,
                Values = { originComponent.ObjectId }
            }
        );

        var componentTypeFilter = pagefilter.AddFilter(LogicalOperator.Or);
        foreach (var key in ComponentTypeLookup.Keys)
        {
            componentTypeFilter.AddCondition(SolutionComponent.LogicalNames.ComponentType, ConditionOperator.Equal, key);
        }

        pagequery.Criteria = pagefilter;

        var solutionLink = pagequery.AddLink("solution", "solutionid", "solutionid");
        solutionLink.LinkCriteria.AddCondition("isvisible", ConditionOperator.Equal, true);
        solutionLink.LinkCriteria.AddCondition("ismanaged", ConditionOperator.Equal, false);
        solutionLink.LinkCriteria.AddCondition("isinternal", ConditionOperator.Equal, false);
        if (!notePatches)
        {
            solutionLink.LinkCriteria.AddCondition("parentsolutionid", ConditionOperator.NotEqual, originComponent.SolutionId.Id);
        }

        solutionLink.Columns.AddColumns("friendlyname", "uniquename");
        solutionLink.EntityAlias = "solution";


        pagequery.Orders.Add(new OrderExpression
        {
            AttributeName = SolutionComponent.LogicalNames.SolutionId,
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
}
