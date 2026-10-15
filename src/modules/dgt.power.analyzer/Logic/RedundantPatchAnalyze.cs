// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.analyzer.Base;
using dgt.power.analyzer.Reports;
using dgt.power.common;
using dgt.power.common.DTO;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.analyzer.Logic;

public class RedundantPatchAnalyze(
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
            console.MarkupLine("[red]--inline requires the unique name of a managed solution.[/]");
            return false;
        }

        var service = await connection.GetServiceAsync();
        return Run(args, (IOrganizationService)service);
    }

    private bool Run(AnalyzeVerb args, IOrganizationService service)
    {
        var summary = new AnalyzerSummary
        {
            Task = "redundantpatches",
            Anomalies = 0
        };

        var resultTable = new List<SolutionRedundantPatchLine>();


        using var context = new DataContext(service);
        var solution = SolutionComponentReader.GetSolution(context, args.InlineData);
        if (!solution.IsManaged.GetValueOrDefault(false))
        {
            console.WriteLine($"Given Solution {solution.FriendlyName} is not Managed - this is not supported.");
            return false;
        }

        Guid parentSolutionId = solution.Id;
        if (solution.ParentSolutionId != null)
        {
            console.WriteLine($"Given Solution {solution.FriendlyName} is not the Base Solution - Analyse will base on found base solution");
            parentSolutionId = solution.ParentSolutionId!.Id;
        }
        var patches = GetPatchSolutions(context, parentSolutionId);

        var table = new Table();
        console.Live(Align.Center(table))
            .Start(ctx =>
            {
                table.AddColumn("Solution");
                table.AddColumn("Name");
                table.AddColumn("Top Layers");
                table.AddColumn("Other Layers");
                table.AddColumn("Suggestion");

                ctx.Refresh();
                foreach (var patch in patches)
                {
                    var patchUniqueName = patch.UniqueName ?? string.Empty;
                    table.AddRow(patchUniqueName, patch.FriendlyName ?? patchUniqueName, "", "", "Checking");
                    ctx.Refresh();
                    table.RemoveRow(table.Rows.Count - 1);
                    var components = reader.GetSolutionComponents(service, patch.Id);

                    var topLayers = 0;
                    var notTopLayers = 0;
                    foreach (var component in components)
                    {
                        var layers = reader.GetSolutionLayers(service, component);
                        if (!layers.Any())
                        {
                            continue;
                        }

                        var first = SolutionComponentReader.GetTopNotActiveLayer(layers);
                        var layerSolutionName = first.MsdynSolutionname;
                        if (string.IsNullOrWhiteSpace(patchUniqueName) ||
                            string.IsNullOrWhiteSpace(layerSolutionName) ||
                            !layerSolutionName.StartsWith(patchUniqueName, StringComparison.OrdinalIgnoreCase))
                        {
                            notTopLayers++;
                        }
                        else
                        {
                            topLayers++;
                        }
                    }

                    resultTable.Add(new SolutionRedundantPatchLine
                    {
                        Solution = patch.UniqueName,
                        TopLayers = topLayers,
                        OtherLayers = notTopLayers
                    });
                    if (topLayers == 0)
                    {
                        summary.Anomalies++;
                    }


                    table.AddRow(
                        patchUniqueName,
                        patch.FriendlyName ?? string.Empty,
                        topLayers.ToString("D", CultureInfo.InvariantCulture),
                        notTopLayers.ToString("D", CultureInfo.InvariantCulture),
                        topLayers == 0 ? "Obsolete" : "Needed");
                    ctx.Refresh();
                }

                ctx.Refresh();
            });

        if (args.GenerateSummaryFile)
        {
            AnalyzeResultWriter.WriteSummaryFile("RedundantPatch", summary);
        }

        if (args.GenerateReportFile)
        {
            AnalyzeResultWriter.WriteReportFile("RedundantPatch", resultTable.OrderBy(r => r.Solution ?? string.Empty, StringComparer.OrdinalIgnoreCase));
        }

        return true;
    }

    private static List<Solution> GetPatchSolutions(DataContext context, Guid parentSolutionId)
    {
        return (from su in context.SolutionSet
            where su.ParentSolutionId != null && su.ParentSolutionId.Id == parentSolutionId
                select su).ToList();
    }
}
