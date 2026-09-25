// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
        var decisions = await context.BuildDecisionsAsync(orgService, bestPractices, cancellationToken);

        RenderPlan(decisions, bestPractices);

        if (settings.DryRun)
        {
            Console.MarkupLine("[yellow]--dry-run: no changes were made[/]");
            return Tracer.End(this, true);
        }

        await ExecuteAsync(orgService, target.UniqueName!, decisions, cancellationToken);

        return Tracer.End(this, true);
    }

    private async Task ExecuteAsync(IOrganizationServiceAsync2 orgService, string targetUniqueName, IReadOnlyList<ComponentCopyDecision> decisions, CancellationToken cancellationToken)
    {
        // Entities first: they act as the anchor row that subsequent (non-required-component) additions attach to.
        var orderedDecisions = decisions
            .Where(static decision => decision.Include)
            .OrderBy(static decision => decision.ComponentType == SolutionComponent.Options.ComponentType.Entity ? 0 : 1);

        var addedCount = 0;
        foreach (var decision in orderedDecisions)
        {
            await orgService.ExecuteAsync(new AddSolutionComponentRequest
            {
                ComponentId = decision.ObjectId,
                ComponentType = decision.ComponentType,
                SolutionUniqueName = targetUniqueName,
                AddRequiredComponents = false,
                DoNotIncludeSubcomponents = decision.DoNotIncludeSubcomponents
            }, cancellationToken);

            addedCount++;
        }

        Console.MarkupLine($"Added [green]{addedCount}[/] component(s) to solution [green]{targetUniqueName}[/]");
    }

    private void RenderPlan(IReadOnlyList<ComponentCopyDecision> decisions, bool bestPractices)
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
