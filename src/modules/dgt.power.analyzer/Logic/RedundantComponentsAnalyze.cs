// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text;
using dgt.power.analyzer.Base;
using dgt.power.analyzer.Reports;
using dgt.power.common;
using dgt.power.common.DTO;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.analyzer.Logic;

public sealed class RedundantComponentsAnalyze(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    SolutionComponentReader reader,
    IAnsiConsole console)
    : PowerCommand<AnalyzeVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(AnalyzeVerb args, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(args.InlineData))
        {
            Console.MarkupLine("[red]--inline requires a comma separated list of solution unique names.[/]");
            return false;
        }

        var service = await connection.GetServiceAsync();
        return await RunAsync(args, service, cancellationToken);
    }

    private async Task<bool> RunAsync(AnalyzeVerb args, IOrganizationServiceAsync2 service, CancellationToken cancellationToken)
    {
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
        var response = (RetrieveAllEntitiesResponse)await service.ExecuteAsync(request, cancellationToken);
        var entities = response.EntityMetadata.ToList();

        foreach (var uniqueName in args.InlineData.Split(','))
        {
            var solutionTag = new Rule($"solution unique name: [lime]{uniqueName}[/]");
            solutionTag.LeftJustified();

            Console.Write(solutionTag);

            var components = await reader.GetSolutionComponentsAsync(service, uniqueName, cancellationToken);

            var table = new Table();
            await Console.Live(Align.Center(table))
                .StartAsync(async ctx =>
                {
                    table.AddColumn("Component");
                    table.AddColumn("Type");
                    table.AddColumn("Name");
                    table.AddColumn("Solution");
                    table.AddColumn("Solution Unique");

                    ctx.Refresh();


                    foreach (var component in components)
                    {
                        var otherSolutions = await GetSolutionsForComponentAsync(service, component, args.NotePatch, cancellationToken);
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

                            if (!reader.ComponentTypeLookup.TryGetValue(component.ComponentType!.Value, out var typeColumn))
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

            Console.WriteLine();
            Console.WriteLine($"Found {resultTable.Count(r => r.OriginSolution == uniqueName)} potential redudant components in {groupedMetrics.Count} unique Solutions:");
            var summaryTable = new Table();


            summaryTable.AddColumn(new TableColumn("Solution").Centered());
            summaryTable.AddColumn("Count");

            foreach (var groupedMetric in groupedMetrics.OrderBy(m => m.Key))
            {
                summaryTable.AddRow(groupedMetric.Key!, $"[green]{groupedMetric.Count()}[/]");
            }

            Console.Write(summaryTable);
        }

        if (args.GenerateSummaryFile)
        {
            AnalyzeResultWriter.WriteSummaryFile("RedundantComponents", summary);
        }

        if (args.GenerateReportFile)
        {
            AnalyzeResultWriter.WriteReportFile("RedundantComponents", resultTable.OrderBy(r => r.OriginSolution).ThenBy(r => r.AlsoInSolution));
        }

        return true;
    }

    private async Task<List<Solution>> GetSolutionsForComponentAsync(IOrganizationServiceAsync2 service, SolutionComponent component, bool notePatches, CancellationToken cancellationToken)
    {
        var parallelSolutionComponents = await GetParallelSolutionComponentsForComponentAsync(service, component, notePatches, cancellationToken);

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

    private async Task<List<SolutionComponent>> GetParallelSolutionComponentsForComponentAsync(
        IOrganizationServiceAsync2 service, SolutionComponent originComponent, bool notePatches, CancellationToken cancellationToken)
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
        foreach (var key in reader.ComponentTypeLookup.Keys)
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

        var components = await service.RetrieveAllAsync(pagequery, cancellationToken: cancellationToken);
        return components.Select(e => e.ToEntity<SolutionComponent>()).ToList();
    }
}
