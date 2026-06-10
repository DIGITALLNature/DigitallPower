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

public class RedundantPatchAnalyze(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IAnsiConsole console) : BaseAnalyze(tracer, connection, configResolver, console)
{
    protected override bool Invoke(AnalyzeVerb args)
    {
        ArgumentNullException.ThrowIfNull(args);
        Tracer.Start(this);

        if (string.IsNullOrWhiteSpace(args.InlineData))
        {
            return Tracer.NotConfigured(this);
        }

        var summary = new AnalyzerSummary
        {
            Task = "redundantpatches",
            Anomalies = 0
        };

        var resultTable = new List<SolutionRedundantPatchLine>();


        using var context = new DataContext(Connection);
        var solution = GetSolution(context, args.InlineData);
        if (!solution.IsManaged.GetValueOrDefault(false))
        {
            Console.WriteLine($"Given Solution {solution.FriendlyName} is not Managed - this is not supported.");
            return Tracer.End(this, false);
        }

        Guid parentSolutionId = solution.Id;
        if (solution.ParentSolutionId != null)
        {
            Console.WriteLine($"Given Solution {solution.FriendlyName} is not the Base Solution - Analyse will base on found base solution");
            parentSolutionId = solution.ParentSolutionId!.Id;
        }
        var patches = GetPatchSolutions(context, parentSolutionId);

        var table = new Table();
        Console.Live(Align.Center(table))
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
                    var components = GetSolutionComponents(patch.Id);

                    var topLayers = 0;
                    var notTopLayers = 0;
                    foreach (var component in components)
                    {
                        var layers = GetSolutionLayers(component);
                        if (!layers.Any())
                        {
                            continue;
                        }

                        var first = GetTopNotActiveLayer(layers);
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
            WriteSummaryFile("RedundantPatch", summary);
        }

        if (args.GenerateReportFile)
        {
            WriteReportFile("RedundantPatch", resultTable.OrderBy(r => r.Solution ?? string.Empty, StringComparer.OrdinalIgnoreCase));
        }

        return Tracer.End(this, true);
    }

    private static List<Solution> GetPatchSolutions(DataContext context, Guid parentSolutionId)
    {
        return (from su in context.SolutionSet
            where su.ParentSolutionId != null && su.ParentSolutionId.Id == parentSolutionId
                select su).ToList();
    }
}
