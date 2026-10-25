// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.Ownership;
using dgt.power.dataverse;
using dgt.power.process.Configuration;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.process.Logic;

public sealed class ProcessApplyEngine(
    IDataverseConnection connection,
    IOwnerResolver ownerResolver,
    IAnsiConsole console)
{
    private static readonly int[] s_supportedCategories = [0, 2, 3, 4, 5];

    public async Task<bool> ApplyAsync(
        ProcessConfiguration configuration,
        OwnerMapping? mapping,
        string? activateAs,
        bool activateAsOwner,
        bool dryRun,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        if (configuration.Processes.Count == 0)
        {
            console.MarkupLine("[green]The process configuration is empty; nothing to do.[/]");
            return true;
        }

        if (dryRun)
        {
            DryRunBanner.Write(console);
        }

        IOrganizationServiceAsync2 service;
        try
        {
            service = await connection.GetServiceAsync();
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            console.MarkupLine("[yellow]Cancelled before Dataverse work began; 0 transitions completed.[/]");
            return false;
        }

        IReadOnlyList<ResolvedProcessPlan> plans;
        Dictionary<Guid, IOrganizationServiceAsync2> callerServices;
        try
        {
            plans = await ResolvePlansAsync(service, configuration, mapping, activateAs, activateAsOwner, cancellationToken);
            callerServices = await ResolveCallerServicesAsync(service, plans, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            console.MarkupLine("[yellow]Cancelled during preflight; 0 transitions completed.[/]");
            return false;
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            console.MarkupLine($"[red]Process preflight failed: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        foreach (var plan in plans)
        {
            console.MarkupLine($"[{(plan.NeedsDeactivation || plan.NeedsAssignment || plan.NeedsActivation ? "yellow" : "grey")}]{plan.Intent.ProcessId:D}: {Describe(plan)}[/]");
        }

        if (dryRun)
        {
            return true;
        }

        var failed = new HashSet<Guid>();
        var activations = new List<ResolvedProcessPlan>();
        var completed = 0;
        try
        {
            foreach (var plan in plans)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var callerService = GetCallerService(service, callerServices, plan.CallerId);
                if (plan.NeedsDeactivation)
                {
                    try
                    {
                        await SetActiveAsync(callerService, plan.Intent.ProcessId, false, cancellationToken);
                        completed++;
                        console.MarkupLine($"[green]{plan.Intent.ProcessId:D}: deactivated[/]");
                    }
                    catch (Exception exception) when (exception is not OperationCanceledException)
                    {
                        failed.Add(plan.Intent.ProcessId);
                        console.MarkupLine($"[red]{plan.Intent.ProcessId:D}: deactivation failed: {exception.Message.EscapeMarkup()}[/]");
                        continue;
                    }
                }

                if (plan.NeedsAssignment)
                {
                    try
                    {
                        await service.ExecuteAsync(
                            new AssignRequest
                            {
                                Assignee = plan.DesiredOwner!.Principal,
                                Target = new EntityReference(Workflow.EntityLogicalName, plan.Intent.ProcessId)
                            },
                            cancellationToken);
                        completed++;
                        console.MarkupLine($"[green]{plan.Intent.ProcessId:D}: owner assigned to '{plan.DesiredOwner.LogicalKey.EscapeMarkup()}'[/]");
                    }
                    catch (Exception exception) when (exception is not OperationCanceledException)
                    {
                        failed.Add(plan.Intent.ProcessId);
                        console.MarkupLine($"[red]{plan.Intent.ProcessId:D}: owner assignment failed: {exception.Message.EscapeMarkup()}[/]");
                        if (plan.NeedsDeactivation)
                        {
                            try
                            {
                                await SetActiveAsync(callerService, plan.Intent.ProcessId, true, cancellationToken);
                                completed++;
                                console.MarkupLine($"[yellow]{plan.Intent.ProcessId:D}: original active state restored[/]");
                            }
                            catch (Exception restoreException) when (restoreException is not OperationCanceledException)
                            {
                                console.MarkupLine($"[red]{plan.Intent.ProcessId:D}: restoration failed; process may remain inactive: {restoreException.Message.EscapeMarkup()}[/]");
                            }
                        }

                        continue;
                    }
                }

                if (plan.NeedsActivation)
                {
                    activations.Add(plan);
                }
            }

            var pending = activations;
            while (pending.Count > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var nextRound = new List<ResolvedProcessPlan>();
                var successes = 0;
                foreach (var plan in pending)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    try
                    {
                        await SetActiveAsync(
                            GetCallerService(service, callerServices, plan.CallerId),
                            plan.Intent.ProcessId,
                            true,
                            cancellationToken);
                        completed++;
                        successes++;
                        console.MarkupLine($"[green]{plan.Intent.ProcessId:D}: activated[/]");
                    }
                    catch (Exception exception) when (exception is not OperationCanceledException)
                    {
                        console.MarkupLine($"[yellow]{plan.Intent.ProcessId:D}: activation attempt failed: {exception.Message.EscapeMarkup()}[/]");
                        nextRound.Add(plan);
                    }
                }

                if (nextRound.Count == 0)
                {
                    break;
                }

                if (successes == 0)
                {
                    foreach (var plan in nextRound)
                    {
                        failed.Add(plan.Intent.ProcessId);
                        console.MarkupLine($"[red]{plan.Intent.ProcessId:D}: activation failed after a no-progress retry round[/]");
                    }

                    break;
                }

                pending = nextRound;
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            console.MarkupLine($"[yellow]Cancelled after {completed} completed state or owner transitions.[/]");
            return false;
        }

        console.MarkupLine($"Process apply completed {completed} transition(s); {failed.Count} process(es) failed.");
        return failed.Count == 0;
    }

    private async Task<IReadOnlyList<ResolvedProcessPlan>> ResolvePlansAsync(
        IOrganizationServiceAsync2 service,
        ProcessConfiguration configuration,
        OwnerMapping? mapping,
        string? activateAs,
        bool activateAsOwner,
        CancellationToken cancellationToken)
    {
        var intents = configuration.Processes;
        var query = new QueryExpression(Workflow.EntityLogicalName)
        {
            ColumnSet = new ColumnSet("workflowid", "name", "category", "type", "statecode", "ownerid")
        };
        query.Criteria.AddCondition("workflowid", ConditionOperator.In, intents.Select(intent => (object)intent.ProcessId).ToArray());
        var processes = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        var byId = processes.ToDictionary(process => process.Id);
        var missing = intents.Where(intent => !byId.ContainsKey(intent.ProcessId)).Select(intent => intent.ProcessId).ToArray();
        if (missing.Length > 0)
        {
            throw new InvalidOperationException($"Process definitions were not found for ID(s): {string.Join(", ", missing.Select(id => id.ToString("D")))}.");
        }

        foreach (var intent in intents)
        {
            var process = byId[intent.ProcessId];
            var type = process.GetAttributeValue<OptionSetValue>("type")?.Value;
            var category = process.GetAttributeValue<OptionSetValue>("category")?.Value;
            if (type != Workflow.Options.Type.Definition)
            {
                throw new InvalidOperationException($"Process '{intent.ProcessId:D}' is not a definition.");
            }

            if (category is null || !s_supportedCategories.Contains(category.Value))
            {
                throw new InvalidOperationException($"Process '{intent.ProcessId:D}' has unsupported category '{category}'.");
            }

            if (activateAsOwner && intent.Active.HasValue && intent.Owner is null)
            {
                throw new InvalidOperationException($"Process '{intent.ProcessId:D}' needs an owner when --activate-as-owner is used with active intent.");
            }
        }

        var ownerKeys = intents.Where(intent => intent.Owner is not null).Select(intent => intent.Owner!).ToList();
        if (activateAs is not null)
        {
            ownerKeys.Add(activateAs);
        }

        IReadOnlyDictionary<string, ResolvedOwner> resolvedOwners = new Dictionary<string, ResolvedOwner>(StringComparer.Ordinal);
        if (ownerKeys.Count > 0)
        {
            if (mapping is null)
            {
                throw new InvalidOperationException("An owner mapping is required to resolve owner keys.");
            }

            resolvedOwners = await ownerResolver.ResolveAsync(service, mapping, ownerKeys, cancellationToken);
        }

        ResolvedOwner? runCaller = null;
        if (activateAs is not null)
        {
            runCaller = RequireImpersonationUser(resolvedOwners[activateAs], activateAs);
        }

        var plans = new List<ResolvedProcessPlan>();
        foreach (var intent in intents)
        {
            var process = byId[intent.ProcessId];
            var currentOwner = process.GetAttributeValue<EntityReference>("ownerid")
                ?? throw new InvalidOperationException($"Process '{intent.ProcessId:D}' has no owner reference.");
            var isActive = process.GetAttributeValue<OptionSetValue>("statecode")?.Value == Workflow.Options.StateCode.Activated;
            ResolvedOwner? desiredOwner = null;
            if (intent.Owner is not null)
            {
                desiredOwner = resolvedOwners[intent.Owner];
                if (desiredOwner.Kind == OwnerKind.Team)
                {
                    throw new InvalidOperationException($"Process '{intent.ProcessId:D}' owner key '{intent.Owner}' resolves to a team; process owners must be users.");
                }
            }

            var needsAssignment = desiredOwner is not null &&
                                  (desiredOwner.Principal.Id != currentOwner.Id ||
                                   !string.Equals(desiredOwner.Principal.LogicalName, currentOwner.LogicalName, StringComparison.Ordinal));
            if (needsAssignment && desiredOwner!.IsDisabled is true)
            {
                throw new InvalidOperationException($"Owner key '{desiredOwner.LogicalKey}' resolves to a disabled user.");
            }

            var desiredActive = intent.Active ?? isActive;
            var needsDeactivation = isActive && (needsAssignment || !desiredActive);
            var needsActivation = desiredActive && (!isActive || needsDeactivation);
            Guid? callerId = runCaller?.Principal.Id;
            if (activateAsOwner && (needsDeactivation || needsActivation))
            {
                if (intent.Owner is null)
                {
                    throw new InvalidOperationException($"Process '{intent.ProcessId:D}' requires a configured owner for --activate-as-owner state transitions.");
                }

                var ownerCaller = RequireImpersonationUser(resolvedOwners[intent.Owner], intent.Owner);
                callerId = ownerCaller.Principal.Id;
            }

            plans.Add(new ResolvedProcessPlan(
                intent,
                process,
                isActive,
                currentOwner,
                desiredOwner,
                callerId,
                needsDeactivation,
                needsAssignment,
                needsActivation));
        }

        return plans;
    }

    private async Task<Dictionary<Guid, IOrganizationServiceAsync2>> ResolveCallerServicesAsync(
        IOrganizationServiceAsync2 standardService,
        IReadOnlyList<ResolvedProcessPlan> plans,
        CancellationToken cancellationToken)
    {
        var callers = plans.Where(plan => plan.CallerId.HasValue)
            .Select(plan => plan.CallerId!.Value)
            .Distinct()
            .ToArray();
        var result = new Dictionary<Guid, IOrganizationServiceAsync2>();
        foreach (var caller in callers)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var impersonatedService = await connection.GetServiceAsync(caller);
            if (impersonatedService is null)
            {
                throw new InvalidOperationException($"Could not obtain an impersonated service for caller '{caller:D}'.");
            }

            result.Add(caller, impersonatedService);
        }

        _ = standardService;
        return result;
    }

    private static ResolvedOwner RequireImpersonationUser(ResolvedOwner owner, string key)
    {
        if (owner.Kind == OwnerKind.Team)
        {
            throw new InvalidOperationException($"Impersonation key '{key}' resolves to a team.");
        }

        if (owner.IsDisabled is true)
        {
            throw new InvalidOperationException($"Impersonation key '{key}' resolves to a disabled user.");
        }

        return owner;
    }

    private static IOrganizationServiceAsync2 GetCallerService(
        IOrganizationServiceAsync2 standardService,
        IReadOnlyDictionary<Guid, IOrganizationServiceAsync2> callerServices,
        Guid? callerId) =>
        callerId.HasValue ? callerServices[callerId.Value] : standardService;

    private static async Task SetActiveAsync(
        IOrganizationServiceAsync2 service,
        Guid processId,
        bool active,
        CancellationToken cancellationToken)
    {
        await service.UpdateAsync(
            new Workflow(processId)
            {
                StateCode = new OptionSetValue(active ? Workflow.Options.StateCode.Activated : Workflow.Options.StateCode.Draft),
                StatusCode = new OptionSetValue(active ? Workflow.Options.StatusCode.Activated : Workflow.Options.StatusCode.Draft)
            },
            cancellationToken);
    }

    private static string Describe(ResolvedProcessPlan plan)
    {
        var changes = new List<string>();
        if (plan.NeedsDeactivation)
        {
            changes.Add("deactivate");
        }

        if (plan.NeedsAssignment)
        {
            changes.Add($"assign owner '{plan.DesiredOwner!.LogicalKey}'");
        }

        if (plan.NeedsActivation)
        {
            changes.Add("activate");
        }

        return changes.Count == 0 ? "unchanged" : string.Join(" -> ", changes);
    }
}
