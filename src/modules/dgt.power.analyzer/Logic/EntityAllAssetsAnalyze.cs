// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Text.RegularExpressions;
using dgt.power.analyzer.Base;
using dgt.power.analyzer.Base.Config;
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

public sealed class EntityAllAssetsAnalyze : BaseAnalyze
{
    public EntityAllAssetsAnalyze(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(AnalyzeVerb args)
    {
        var result = true;
        Tracer.Start(this);

        //read config
        if (!ConfigResolver.GetConfigFile<List<EntityAllAssets>>(args.Config, out var entitiesAllAssets))
        {
            return Tracer.End(this, false);
        }

        //anything to do?
        if (!entitiesAllAssets.Any())
        {
            return Tracer.NotConfigured(this);
        }

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
        var response = (RetrieveAllEntitiesResponse)Connection.Execute(request);
        var entities = response.EntityMetadata.ToList();

        foreach (var entityAllAssets in entitiesAllAssets)
        {
            var solutionTag = new Rule($"solution unique name: [lime]{entityAllAssets.Solution}[/]");
            solutionTag.LeftJustified();

            AnsiConsole.Write(solutionTag);

            using var context = new DataContext(Connection);
            var solution = GetSolution(context, entityAllAssets);
            var components = GetSolutionComponents(solution);

            components.Sort((one, two) => string.Compare(entities.Single(e => e.MetadataId == one.ObjectId).LogicalName, entities.Single(e => e.MetadataId == two.ObjectId).LogicalName, StringComparison.Ordinal));

            foreach (var component in components)
            {
                var entity = entities.Single(e => e.MetadataId == component.ObjectId);
                Tracer.Log($"{entity.LogicalName + " (" + entity.DisplayName.UserLocalizedLabel?.Label + ")"}:", TraceEventType.Information);

                //check whitelist
                if (component.RootComponentBehavior?.Value == SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents)
                {
                    Tracer.Log("  -> AllAssets", TraceEventType.Information);
                    var approved = false;
                    foreach (var entryWl in entityAllAssets.WhiteList)
                    {
                        var patternWl = entryWl;
                        var matchWl = true;
                        if (entryWl.StartsWith("!", StringComparison.InvariantCulture))
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
                                if (entryBl.StartsWith("!", StringComparison.InvariantCulture))
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
                        Tracer.Log("  -> approved: true", TraceEventType.Information);
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
                            Tracer.Log("        -> approved: false", TraceEventType.Error);
                        }
                        else
                        {
                            Tracer.Log("      -> approved: false", TraceEventType.Warning);
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
                        if (entryWl.StartsWith("!", StringComparison.InvariantCulture))
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
                            if (entryBl.StartsWith("!", StringComparison.InvariantCulture))
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
                        Tracer.Log("  -> good", TraceEventType.Information);
                        resultTable.Add(new AllAssetLine
                        {
                            Solution = entityAllAssets.Solution,
                            Entity = entity.LogicalName,
                            Level = AllAssetLine.AllAssetsLevel.Good
                        });
                    }
                    else
                    {
                        Tracer.Log("      -> suspicious", TraceEventType.Warning);

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
                    Tracer.Log("  -> shell only", TraceEventType.Information);
                }
            }
        }

        if (args.GenerateSummaryFile)
        {
            WriteSummaryFile("EntityAllAssets", summary);
        }

        if (args.GenerateReportFile)
        {
            WriteReportFile("EntityAllAssets", resultTable.OrderBy(r => r.Solution).ThenBy(r => r.Entity));
        }

        return Tracer.End(this, result);
    }

    private List<SolutionComponent> GetSolutionComponents(Solution solution)
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
        var components = Connection.RetrieveMultiple(query).Entities.Select(s => s.ToEntity<SolutionComponent>()).ToList();
        return components;
    }

    private static Solution GetSolution(DataContext context, EntityAllAssets entityAllAssets) =>
        (from su in context.SolutionSet
            where su.UniqueName == entityAllAssets.Solution
            select su).Single();
}
