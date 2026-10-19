// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.RegularExpressions;
using dgt.power.analyzer.Base;
using dgt.power.analyzer.Base.Config;
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

public sealed class EntityAllAssetsAnalyze(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<AnalyzeVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(AnalyzeVerb args, CancellationToken cancellationToken)
    {
        //read config
        if (!configResolver.TryGetConfigFile<List<EntityAllAssets>>(args.Config, out var entitiesAllAssets))
        {
            return false;
        }

        //anything to do?
        if (entitiesAllAssets.Count == 0)
        {
            Console.MarkupLine("[red]No entity all assets configuration found.[/]");
            return false;
        }

        var service = await connection.GetServiceAsync();
        return await RunAsync(args, entitiesAllAssets, service, cancellationToken);
    }

    private async Task<bool> RunAsync(AnalyzeVerb args, List<EntityAllAssets> entitiesAllAssets, IOrganizationServiceAsync2 service, CancellationToken cancellationToken)
    {
        var result = true;

        var summary = new AnalyzerSummary
        {
            Task = "entityallassets",
            Anomalies = 0
        };

        var resultTable = new List<AllAssetLine>();

        var request = new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity
        };
        var response = (RetrieveAllEntitiesResponse)await service.ExecuteAsync(request, cancellationToken);
        var entities = response.EntityMetadata.ToList();

        foreach (var entityAllAssets in entitiesAllAssets)
        {
            var solutionTag = new Rule($"solution unique name: [lime]{entityAllAssets.Solution}[/]");
            solutionTag.LeftJustified();

            Console.Write(solutionTag);

            var solution = await SolutionComponentReader.GetSolutionAsync(service, entityAllAssets.Solution, cancellationToken);
            var components = await GetSolutionComponentsAsync(service, solution, cancellationToken);

            components.Sort((one, two) => string.Compare(entities.Single(e => e.MetadataId == one.ObjectId).LogicalName, entities.Single(e => e.MetadataId == two.ObjectId).LogicalName, StringComparison.Ordinal));

            foreach (var component in components)
            {
                var entity = entities.Single(e => e.MetadataId == component.ObjectId);
                Console.WriteLine($"{entity.LogicalName + " (" + entity.DisplayName.UserLocalizedLabel?.Label + ")"}:");

                //check whitelist
                if (component.RootComponentBehavior?.Value == SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents)
                {
                    Console.WriteLine("  -> AllAssets");
                    var approved = false;
                    foreach (var entryWl in entityAllAssets.WhiteList)
                    {
                        var patternWl = entryWl;
                        var matchWl = true;
                        if (entryWl.StartsWith('!'))
                        {
                            patternWl = entryWl.Remove(0, 1);
                            matchWl = false;
                        }

                        if (Regex.IsMatch(entity.LogicalName, patternWl) == matchWl)
                        {
                            approved = true;
                            foreach (var entryBl in entityAllAssets.BlackList)
                            {
                                var patternBl = entryBl;
                                var matchBl = true;
                                if (entryBl.StartsWith('!'))
                                {
                                    patternBl = entryBl.Remove(0, 1);
                                    matchBl = false;
                                }

                                if (Regex.IsMatch(entity.LogicalName, patternBl) == matchBl)
                                {
                                    approved = false;
                                    // TODO: Shouldn't this be continue?
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    if (approved)
                    {
                        Console.WriteLine("  -> approved: true");
                        resultTable.Add(new AllAssetLine
                        {
                            Solution = entityAllAssets.Solution,
                            Entity = entity.LogicalName,
                            Level = AllAssetLine.AllAssetsLevel.Approved
                        });
                    }
                    else
                    {
                        if (entityAllAssets.Strict)
                        {
                            result = false;
                            Console.MarkupLine("[red]        -> approved: false[/]");
                        }
                        else
                        {
                            Console.MarkupLine("[yellow]      -> approved: false[/]");
                        }

                        summary.Anomalies++;
                        resultTable.Add(new AllAssetLine
                        {
                            Solution = entityAllAssets.Solution,
                            Entity = entity.LogicalName,
                            Level = AllAssetLine.AllAssetsLevel.Bad
                        });
                    }
                }
                else if (component.RootComponentBehavior?.Value == SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents)
                {
                    var good = false;
                    foreach (var entryWl in entityAllAssets.WhiteList)
                    {
                        var patternWl = entryWl;
                        var matchWl = false;
                        if (entryWl.StartsWith('!'))
                        {
                            patternWl = entryWl.Remove(0, 1);
                            matchWl = true;
                        }

                        if (Regex.IsMatch(entity.LogicalName, patternWl) == matchWl)
                        {
                            good = true;
                            // TODO: Shouldn't this be continue?
                            break;
                        }

                        foreach (var entryBl in entityAllAssets.BlackList)
                        {
                            var patternBl = entryBl;
                            var matchBl = true;
                            if (entryBl.StartsWith('!'))
                            {
                                patternBl = entryBl.Remove(0, 1);
                                matchBl = false;
                            }

                            if (Regex.IsMatch(entity.LogicalName, patternBl) == matchBl)
                            {
                                good = true;
                                // TODO: Shouldn't this be continue?
                                break;
                            }
                        }
                    }

                    if (good)
                    {
                        Console.WriteLine("  -> good");
                        resultTable.Add(new AllAssetLine
                        {
                            Solution = entityAllAssets.Solution,
                            Entity = entity.LogicalName,
                            Level = AllAssetLine.AllAssetsLevel.Good
                        });
                    }
                    else
                    {
                        Console.MarkupLine("[yellow]      -> suspicious[/]");

                        resultTable.Add(new AllAssetLine
                        {
                            Solution = entityAllAssets.Solution,
                            Entity = entity.LogicalName,
                            Level = AllAssetLine.AllAssetsLevel.Suspicious
                        });
                        summary.Anomalies++;
                    }
                }
                else
                {
                    Console.WriteLine("  -> shell only");
                }
            }
        }

        if (args.GenerateSummaryFile)
        {
            AnalyzeResultWriter.WriteSummaryFile("EntityAllAssets", summary);
        }

        if (args.GenerateReportFile)
        {
            AnalyzeResultWriter.WriteReportFile("EntityAllAssets", resultTable.OrderBy(r => r.Solution).ThenBy(r => r.Entity));
        }

        return result;
    }

    private static async Task<List<SolutionComponent>> GetSolutionComponentsAsync(IOrganizationServiceAsync2 service, Solution solution, CancellationToken cancellationToken)
    {
        var query = new QueryExpression
        {
            EntityName = SolutionComponent.EntityLogicalName,
            NoLock = true,
            ColumnSet = new ColumnSet(
                SolutionComponent.LogicalNames.ComponentType,
                SolutionComponent.LogicalNames.RootComponentBehavior,
                SolutionComponent.LogicalNames.ObjectId,
                SolutionComponent.LogicalNames.IsMetadata
            )
        };
        var filter = new FilterExpression(LogicalOperator.And);
        filter.Conditions.Add(
            new ConditionExpression
            {
                AttributeName = SolutionComponent.LogicalNames.ComponentType,
                Operator = ConditionOperator.Equal,
                Values = { SolutionComponent.Options.ComponentType.Entity }
            }
        );
        filter.Conditions.Add(
            new ConditionExpression
            {
                AttributeName = SolutionComponent.LogicalNames.SolutionId,
                Operator = ConditionOperator.Equal,
                Values = { solution.Id }
            }
        );
        query.Criteria = filter;
        var components = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Select(s => s.ToEntity<SolutionComponent>()).ToList();
        return components;
    }
}
