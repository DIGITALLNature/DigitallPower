// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.analyzer.Base;
using dgt.power.analyzer.Reports;
using dgt.power.common;
using dgt.power.common.DTO;
using dgt.power.dataverse;
using dgt.power.common.Extensions;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
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
        if (string.IsNullOrWhiteSpace(args.InlineData))
        {
            Console.MarkupLine("[red]--inline requires the unique name of a managed solution.[/]");
            return false;
        }

        var service = await connection.GetServiceAsync();
        return await RunAsync(args, service, cancellationToken);
    }

    private async Task<bool> RunAsync(AnalyzeVerb args, IOrganizationServiceAsync2 service, CancellationToken cancellationToken)
    {
        var summary = new AnalyzerSummary
        {
            Task = "redundantpatches",
            Anomalies = 0
        };

        var resultTable = new List<SolutionRedundantPatchLine>();


        var solution = await SolutionComponentReader.GetSolutionAsync(service, args.InlineData, cancellationToken);
        if (!solution.IsManaged.GetValueOrDefault(false))
        {
            Console.WriteLine($"Given Solution {solution.FriendlyName} is not Managed - this is not supported.");
            return false;
        }

        Guid parentSolutionId = solution.Id;
        if (solution.ParentSolutionId != null)
        {
            Console.WriteLine($"Given Solution {solution.FriendlyName} is not the Base Solution - Analyse will base on found base solution");
            parentSolutionId = solution.ParentSolutionId!.Id;
        }
        var patches = await GetPatchSolutionsAsync(service, parentSolutionId, cancellationToken);

        var table = new Table();
        await Console.Live(Align.Center(table))
            .StartAsync(async ctx =>
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
                    var components = await reader.GetSolutionComponentsAsync(service, patch.Id, cancellationToken);

                    var topLayers = 0;
                    var notTopLayers = 0;
                    foreach (var component in components)
                    {
                        var layers = await reader.GetSolutionLayersAsync(service, component, cancellationToken);
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

    private static async Task<List<Solution>> GetPatchSolutionsAsync(IOrganizationServiceAsync2 service, Guid parentSolutionId, CancellationToken cancellationToken)
    {
        var query = new QueryExpression(Solution.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                Solution.LogicalNames.SolutionId,
                Solution.LogicalNames.UniqueName,
                Solution.LogicalNames.FriendlyName),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(Solution.LogicalNames.ParentSolutionId, ConditionOperator.Equal, parentSolutionId)
                }
            }
        };
        var patches = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        return patches.Select(entity => entity.ToEntity<Solution>()).ToList();
    }
}
