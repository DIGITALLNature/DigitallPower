// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.analyzer.Base;
using dgt.power.analyzer.Reports;
using dgt.power.common;
using dgt.power.common.DTO;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.analyzer.Logic;

public sealed class TopLayerAnalyze(
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
            Task = "toplayer",
            Anomalies = 0
        };

        var resultTable = new List<ActiveLayerLine>();

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
                    table.AddColumn("Order");
                    table.AddColumn("Name");
                    table.AddColumn("Solution");

                    ctx.Refresh();

                    foreach (var component in components)
                    {
                        var layers = await reader.GetSolutionLayersAsync(service, component, cancellationToken);
                        if (!layers.Any())
                        {
                            continue;
                        }

                        var first = SolutionComponentReader.GetTopNotActiveLayer(layers);
                        if (!first.MsdynSolutionname!.ToUpperInvariant().StartsWith(uniqueName.ToUpperInvariant(),StringComparison.Ordinal))
                        {
                            var componentName = SolutionComponentReader.GetComponentName(component, entities, first);
                            table.AddRow($"{first.MsdynSolutioncomponentname}", $"{first.MsdynOrder:D}", componentName, uniqueName);
                            ctx.Refresh();

                            resultTable.Add(new ActiveLayerLine { Component = first.MsdynSolutioncomponentname, Order = first.MsdynOrder, Name = componentName, Solution = uniqueName });
                            summary.Anomalies++;
                        }
                    }
                });
        }

        if (args.GenerateSummaryFile)
        {
            AnalyzeResultWriter.WriteSummaryFile("TopLayer", summary);
        }

        if (args.GenerateReportFile)
        {
            AnalyzeResultWriter.WriteReportFile("TopLayer", resultTable.OrderBy(r => r.Solution).ThenBy(r => r.Component).ThenBy(r => r.Name).ThenBy(r => r.Order));
        }

        return true;
    }
}
