// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ServiceModel;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.solution.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.solution;

// ReSharper disable once ClassNeverInstantiated.Global — instantiated by the DI container via Spectre.Console.Cli
public class CopyComponentsCommand(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<CopyComponentsSettings>(tracer, connection, configResolver, console)
{
    protected override Task<bool> InvokeAsync(CopyComponentsSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return InvokeCoreAsync(settings, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(CopyComponentsSettings settings, CancellationToken cancellationToken)
    {
        Tracer.Start(this);

        if (string.IsNullOrWhiteSpace(settings.Target))
        {
            Console.MarkupLine("[red]Invalid or empty target solution name[/]");
            return Tracer.End(this, false);
        }

        var sourceNames = settings.Source.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (sourceNames.Length == 0)
        {
            Console.MarkupLine("[red]--source is required and must contain at least one solution unique name[/]");
            return Tracer.End(this, false);
        }

        var orgService = (IOrganizationServiceAsync2)Connection;

        var target = await RetrieveSolutionAsync(orgService, settings.Target, cancellationToken);
        if (target == null)
        {
            Console.MarkupLine($"[red]Target solution '{settings.Target}' not found[/]");
            return Tracer.End(this, false);
        }

        if (target.IsManaged == true)
        {
            Console.MarkupLine($"[red]Target solution '{settings.Target}' is managed - components can only be added to an unmanaged solution[/]");
            return Tracer.End(this, false);
        }

        var sourceSolutions = await RetrieveSolutionsAsync(orgService, sourceNames, cancellationToken);
        var missingSourceNames = sourceNames
            .Where(name => !sourceSolutions.Exists(solution => string.Equals(solution.UniqueName, name, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (missingSourceNames.Count > 0)
        {
            Console.MarkupLine($"[red]Source solution(s) not found: {string.Join(", ", missingSourceNames)}[/]");
            return Tracer.End(this, false);
        }

        var bestPractices = !settings.Raw;
        var context = await CopyComponentsContext.CreateAsync(orgService, sourceSolutions.Select(static solution => solution.Id).ToArray(), cancellationToken);
        var decisions = ApplyAppHandling(await context.BuildDecisionsAsync(orgService, bestPractices, cancellationToken), settings.Apps);

        RenderPlan(decisions, bestPractices);

        if (settings.DryRun)
        {
            Console.MarkupLine("[yellow]--dry-run: no changes were made[/]");
            return Tracer.End(this, true);
        }

        await ExecuteAsync(orgService, target, decisions, settings.Apps == AppHandling.Strip, cancellationToken);

        return Tracer.End(this, true);
    }

    private async Task ExecuteAsync(
        IOrganizationServiceAsync2 orgService,
        Solution target,
        IReadOnlyList<ComponentCopyDecision> decisions,
        bool stripAppSubcomponents,
        CancellationToken cancellationToken)
    {
        var targetUniqueName = target.UniqueName!;

        // Entities first: they act as the anchor row that subsequent (non-required-component) additions attach to.
        // App-bound components and apps last: adding any of them makes Dataverse pull in the app and its subcomponents
        // on its own, so all of them form one "app phase" that is cleaned up afterwards when stripping (see below).
        var orderedDecisions = decisions
            .Where(static decision => decision.Include)
            .OrderBy(static decision => decision switch
            {
                { ComponentType: SolutionComponent.Options.ComponentType.Entity } => 0,
                { ComponentType: CopyComponentsContext.AppModuleComponentType } => 3,
                { IsAppBound: true } => 2,
                _ => 1
            })
            .ToList();

        var plannedKeys = orderedDecisions.Select(static decision => (decision.ComponentType, decision.ObjectId)).ToHashSet();
        var removedSubcomponents = new List<(int ComponentType, Guid ObjectId)>();
        var alreadyGoneSubcomponents = new List<(int ComponentType, Guid ObjectId)>();
        IReadOnlyDictionary<int, SolutionComponentDefinitionInfo> definitionsByType = new Dictionary<int, SolutionComponentDefinitionInfo>();
        var stripReports = new List<string>();
        HashSet<(int ComponentType, Guid ObjectId)>? membershipBeforeAppPhase = null;

        var firstAppPhaseIndex = stripAppSubcomponents ? orderedDecisions.FindIndex(IsAppPhase) : -1;
        var lastAppPhaseIndex = stripAppSubcomponents ? orderedDecisions.FindLastIndex(IsAppPhase) : -1;
        if (firstAppPhaseIndex >= 0)
        {
            definitionsByType = await CopyComponentsContext.RetrieveComponentDefinitionsAsync(orgService, cancellationToken);
        }

        string DescribeKey((int ComponentType, Guid ObjectId) key) => $"{TypeNameOf(definitionsByType, key.ComponentType)} {key.ObjectId}";

        await Console.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .StartAsync($"Adding {orderedDecisions.Count} component(s) to solution '{targetUniqueName}'...", async status =>
        {
            for (var index = 0; index < orderedDecisions.Count; index++)
            {
                var decision = orderedDecisions[index];
                status.Status($"Adding {decision.ComponentTypeName} {decision.ObjectId} ({index + 1}/{orderedDecisions.Count})...");

                if (index == firstAppPhaseIndex)
                {
                    membershipBeforeAppPhase = await RetrieveSolutionComponentKeysAsync(orgService, target.Id, cancellationToken);
                }

                // DoNotIncludeSubcomponents is only accepted by Dataverse for Entity roots - any other componenttype is rejected.
                await orgService.ExecuteAsync(new AddSolutionComponentRequest
                {
                    ComponentId = decision.ObjectId,
                    ComponentType = decision.ComponentType,
                    SolutionUniqueName = targetUniqueName,
                    AddRequiredComponents = false,
                    DoNotIncludeSubcomponents = decision.DoNotIncludeSubcomponents
                }, cancellationToken);

                if (index != lastAppPhaseIndex || membershipBeforeAppPhase == null)
                {
                    continue;
                }

                // Apps cannot be added without their subcomponents via the request flags (--apps strip), so everything
                // the platform added on its own during the app phase (not present before, not part of the plan) is removed again.
                status.Status("Removing subcomponents added together with the apps...");
                var membershipAfter = await RetrieveSolutionComponentKeysAsync(orgService, target.Id, cancellationToken);
                var newKeys = membershipAfter.Where(key => !membershipBeforeAppPhase.Contains(key)).ToList();
                // Entities last: removing a table from the solution also drops its views/forms/etc., which would
                // otherwise be gone already when they are removed individually afterwards.
                var keysToRemove = newKeys
                    .Where(key => !plannedKeys.Contains(key))
                    .OrderBy(static key => key.ComponentType == SolutionComponent.Options.ComponentType.Entity ? 1 : 0)
                    .ToList();
                foreach (var key in keysToRemove)
                {
                    status.Status($"Removing subcomponents added together with the apps... ({DescribeKey(key)})");
                    try
                    {
                        await orgService.ExecuteAsync(new RemoveSolutionComponentRequest
                        {
                            ComponentId = key.ObjectId,
                            ComponentType = key.ComponentType,
                            SolutionUniqueName = targetUniqueName
                        }, cancellationToken);
                        removedSubcomponents.Add(key);
                    }
                    catch (FaultException<OrganizationServiceFault>)
                    {
                        // Already gone (e.g. removed together with its parent table) is fine - anything else is a real failure.
                        var currentMembership = await RetrieveSolutionComponentKeysAsync(orgService, target.Id, cancellationToken);
                        if (currentMembership.Contains(key))
                        {
                            throw;
                        }

                        alreadyGoneSubcomponents.Add(key);
                    }
                }

                stripReports.Add(
                    $"app phase ({lastAppPhaseIndex - firstAppPhaseIndex + 1} app/app-bound component(s)): target held {membershipBeforeAppPhase.Count} component(s) before and " +
                    $"{membershipAfter.Count} after ({newKeys.Count} new): {removedSubcomponents.Count} removed, {alreadyGoneSubcomponents.Count} already gone, " +
                    $"{newKeys.Count - keysToRemove.Count} kept because they are part of the plan");
            }
        });

        await ReportLateAdditionsAsync(orgService, target.Id, membershipBeforeAppPhase, plannedKeys, definitionsByType, stripReports, cancellationToken);

        Console.MarkupLine($"Added [green]{orderedDecisions.Count}[/] component(s) to solution [green]{targetUniqueName}[/]");

        foreach (var report in stripReports)
        {
            Console.MarkupLine($"[grey]--apps strip: {report}[/]");
        }

        if (removedSubcomponents.Count > 0)
        {
            Console.MarkupLine($"[yellow]Removed {removedSubcomponents.Count} subcomponent(s) that Dataverse added together with a model-driven app (--apps strip):[/]");
            foreach (var key in removedSubcomponents)
            {
                Console.MarkupLine($"  [grey]{Markup.Escape(TypeNameOf(definitionsByType, key.ComponentType))}: {key.ObjectId}[/]");
            }
        }
    }

    private static string TypeNameOf(IReadOnlyDictionary<int, SolutionComponentDefinitionInfo> definitionsByType, int componentType) =>
        definitionsByType.GetValueOrDefault(componentType).Name ?? $"componenttype {componentType}";

    private static bool IsAppPhase(ComponentCopyDecision decision) =>
        decision.IsAppBound || decision.ComponentType == CopyComponentsContext.AppModuleComponentType;

    // Diagnostic for --apps strip: rows that show up only after the app-phase snapshot diff ran (e.g. asynchronous expansion by Dataverse).
    private static async Task ReportLateAdditionsAsync(
        IOrganizationServiceAsync2 orgService,
        Guid solutionId,
        HashSet<(int ComponentType, Guid ObjectId)>? membershipBeforeAppPhase,
        HashSet<(int ComponentType, Guid ObjectId)> plannedKeys,
        IReadOnlyDictionary<int, SolutionComponentDefinitionInfo> definitionsByType,
        List<string> stripReports,
        CancellationToken cancellationToken)
    {
        if (membershipBeforeAppPhase == null)
        {
            return;
        }

        var finalMembership = await RetrieveSolutionComponentKeysAsync(orgService, solutionId, cancellationToken);
        var late = finalMembership.Where(key => !membershipBeforeAppPhase.Contains(key) && !plannedKeys.Contains(key)).ToList();
        if (late.Count > 0)
        {
            var sample = string.Join(", ", late.Take(20).Select(key => $"{TypeNameOf(definitionsByType, key.ComponentType)}/{key.ObjectId}"));
            stripReports.Add($"[yellow]{late.Count} unplanned component(s) are still in the target after stripping: {sample}[/]");
        }
    }

    private static async Task<HashSet<(int ComponentType, Guid ObjectId)>> RetrieveSolutionComponentKeysAsync(
        IOrganizationServiceAsync2 orgService,
        Guid solutionId,
        CancellationToken cancellationToken)
    {
        var query = new QueryExpression(SolutionComponent.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(SolutionComponent.LogicalNames.ComponentType, SolutionComponent.LogicalNames.ObjectId),
            PageInfo = new PagingInfo { Count = PageSize, PageNumber = 1 }
        };
        query.Criteria.AddCondition(SolutionComponent.LogicalNames.SolutionId, ConditionOperator.Equal, solutionId);

        var keys = new HashSet<(int, Guid)>();
        bool moreRecords;
        do
        {
            var page = await orgService.RetrieveMultipleAsync(query, cancellationToken);
            foreach (var component in page.Entities.Select(static entity => entity.ToEntity<SolutionComponent>()))
            {
                if (component.ComponentType?.Value is { } type && component.ObjectId.HasValue)
                {
                    keys.Add((type, component.ObjectId.Value));
                }
            }

            moreRecords = page.MoreRecords;
            if (moreRecords)
            {
                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = page.PagingCookie;
            }
        } while (moreRecords);

        return keys;
    }

    private static List<ComponentCopyDecision> ApplyAppHandling(IReadOnlyList<ComponentCopyDecision> decisions, AppHandling appHandling) =>
        decisions
            .Select(decision =>
            {
                if (decision.ComponentType == CopyComponentsContext.AppModuleComponentType)
                {
                    return appHandling switch
                    {
                        AppHandling.Skip => decision with { Include = false, Reason = "Model-driven app - skipped (use --apps strip|allow to copy apps)" },
                        AppHandling.Strip => decision with { Reason = "Model-driven app - subcomponents added by Dataverse are removed afterwards (--apps strip)" },
                        _ => decision with { Reason = "Model-driven app - Dataverse may add its subcomponents (--apps allow)" }
                    };
                }

                // App-bound components are meaningless without their app, so they follow the app's skip.
                return decision.IsAppBound && appHandling == AppHandling.Skip
                    ? decision with { Include = false, Reason = "App-bound component - skipped together with model-driven apps (use --apps strip|allow)" }
                    : decision;
            })
            .ToList();

    private void RenderPlan(List<ComponentCopyDecision> decisions, bool bestPractices)
    {
        Console.MarkupLine(bestPractices
            ? "Best-practice mode: managed tables/components are added as skeleton/delta only, unmanaged ones completely"
            : "[yellow]--raw: tables keep only their complete/non-complete distinction (shell-only sources become non-complete); other components are copied as-is, no managed/active-layer filtering[/]");

        var table = new Table();
        table.AddColumn("Type");
        table.AddColumn("Object Id");
        table.AddColumn("Include");
        table.AddColumn("Behavior");
        table.AddColumn("Reason");

        foreach (var decision in decisions.OrderBy(static decision => decision.ComponentTypeName, StringComparer.OrdinalIgnoreCase))
        {
            var includeMarkup = decision.Include ? "[green]yes[/]" : "[grey]skip[/]";
            var behavior = "-";
            if (decision.ComponentType == SolutionComponent.Options.ComponentType.Entity)
            {
                behavior = decision.DoNotIncludeSubcomponents ? "skeleton" : "complete";
            }

            table.AddRow(decision.ComponentTypeName, decision.ObjectId.ToString(), includeMarkup, behavior, decision.Reason);
        }

        Console.Write(table);

        var includedCount = decisions.Count(static decision => decision.Include);
        Console.MarkupLine($"Plan: [green]{includedCount}[/] to include, [grey]{decisions.Count - includedCount}[/] to skip (of {decisions.Count} total)");
    }

    private static async Task<Solution?> RetrieveSolutionAsync(IOrganizationServiceAsync2 orgService, string uniqueName, CancellationToken cancellationToken)
    {
        var solutions = await RetrieveSolutionsAsync(orgService, [uniqueName], cancellationToken);
        return solutions.SingleOrDefault();
    }

    private static async Task<List<Solution>> RetrieveSolutionsAsync(IOrganizationServiceAsync2 orgService, IReadOnlyCollection<string> uniqueNames, CancellationToken cancellationToken)
    {
        var query = new QueryExpression(Solution.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(Solution.LogicalNames.UniqueName, Solution.LogicalNames.IsManaged)
        };
        query.Criteria.AddCondition(Solution.LogicalNames.UniqueName, ConditionOperator.In, uniqueNames.Cast<object>().ToArray());

        return (await orgService.RetrieveMultipleAsync(query, cancellationToken)).Entities
            .Select(static entity => entity.ToEntity<Solution>())
            .ToList();
    }
}
