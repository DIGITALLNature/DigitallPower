// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.analyzer.Base;
using dgt.power.analyzer.Reports;
using dgt.power.common;
using dgt.power.common.DTO;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.analyzer.Logic;

public sealed class ActiveLayerAnalyze(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    SolutionComponentReader reader,
    IAnsiConsole console)
    : PowerCommand<AnalyzeVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(AnalyzeVerb args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);

        if (string.IsNullOrWhiteSpace(args.InlineData))
        {
            console.MarkupLine("[red]--inline requires a comma separated list of solution unique names.[/]");
            return false;
        }

        var service = await connection.GetServiceAsync();
        return Run(args, (IOrganizationService)service);
    }

    private bool Run(AnalyzeVerb args, IOrganizationService service)
    {
        var summary = new AnalyzerSummary
        {
            Task = "activelayer",
            Anomalies = 0
        };

        var resultTable = new List<ActiveLayerLine>();

        var request = new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity
        };
        var response = (RetrieveAllEntitiesResponse)service.Execute(request);
        var entities = response.EntityMetadata.ToList();

        using var context = new DataContext(service);
        foreach (var uniqueName in args.InlineData.Split(','))
        {
            var solutionTag = new Rule($"solution unique name: [lime]{uniqueName}[/]");
            solutionTag.LeftJustified();

            console.Write(solutionTag);

            var components = reader.GetSolutionComponents(service, context, uniqueName);

            var table = new Table();
            console.Live(Align.Center(table))
                .Start(ctx =>
                {
                    table.AddColumn("Component");
                    table.AddColumn("Order");
                    table.AddColumn("Name");
                    table.AddColumn("Solution");

                    ctx.Refresh();

                    for (var index = 0; index < components.Count; index++)
                    {
                        var component = components[index];

                        table.AddRow(component.ObjectId.ToString() ?? Guid.Empty.ToString(), $"{index}/{components.Count}", "", "Checking");
                        ctx.Refresh();
                        var layers = reader.GetSolutionLayers(service, component);
                        table.RemoveRow(table.Rows.Count - 1);
                        if (!layers.Any())
                        {
                            continue;
                        }

                        var first = layers[0];
                        if (first.MsdynSolutionname == "Active")
                        {
                            var componentName = SolutionComponentReader.GetComponentName(component, entities, first);
                            table.AddRow($"{first.MsdynSolutioncomponentname}", $"{first.MsdynOrder:D}", componentName.EscapeMarkup(),
                                uniqueName);

                            resultTable.Add(new ActiveLayerLine
                            {
                                Component = first.MsdynSolutioncomponentname, Order = first.MsdynOrder,
                                Name = componentName, Solution = uniqueName
                            });
                            summary.Anomalies++;
                        }

                        ctx.Refresh();
                    }
                });
        }

        if (args.GenerateSummaryFile)
        {
            AnalyzeResultWriter.WriteSummaryFile("ActiveLayer", summary);
        }

        if (args.GenerateReportFile)
        {
            AnalyzeResultWriter.WriteReportFile("ActiveLayer", resultTable.OrderBy(r => r.Solution).ThenBy(r => r.Component).ThenBy(r => r.Name).ThenBy(r => r.Order));
        }

        return true;
    }
}
