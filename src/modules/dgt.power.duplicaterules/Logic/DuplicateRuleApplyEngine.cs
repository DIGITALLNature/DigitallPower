// Copyright (c) DIGITALL Nature. All rights reserved

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.Ownership;
using dgt.power.dataverse;
using dgt.power.duplicaterules.Configuration;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.duplicaterules.Logic;

public sealed class DuplicateRuleApplyEngine(
    IDataverseConnection connection,
    IOwnerResolver ownerResolver,
    IAnsiConsole console,
    TimeProvider timeProvider)
{
    public static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(2);

    private const int JobCompleted = 3;
    private const int JobFailed = 31;
    private const int JobCanceled = 32;

    public Task<bool> ApplyAsync(
        DuplicateRuleConfiguration configuration,
        OwnerMapping? mapping,
        DuplicateRuleApplyOptions options,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(options);
        return ApplyCoreAsync(configuration, mapping, options, cancellationToken);
    }

    private async Task<bool> ApplyCoreAsync(
        DuplicateRuleConfiguration configuration,
        OwnerMapping? mapping,
        DuplicateRuleApplyOptions options,
        CancellationToken cancellationToken)
    {
        if (configuration.Rules.Count == 0)
        {
            console.MarkupLine("[green]The duplicate-rule configuration is empty: 0 rules, no changes.[/]");
            return true;
        }

        if (options.DryRun)
        {
            DryRunBanner.Write(console);
        }

        IOrganizationServiceAsync2 service;
        IReadOnlyList<DuplicateRulePlan> plans;
        IReadOnlyList<DuplicateRuleCapacity> capacity;
        try
        {
            service = await connection.GetServiceAsync();
            (plans, capacity) = await new DuplicateRulePreflight(ownerResolver).PrepareAsync(service, configuration, mapping, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            console.MarkupLine("[yellow]Cancelled during preflight; 0 operations completed.[/]");
            return false;
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            console.MarkupLine($"[red]Duplicate-rule preflight failed; no rules were changed: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        WritePlan(plans, capacity, options);
        if (options.DryRun)
        {
            return true;
        }

        return await ExecuteAsync(service, plans, capacity, options, cancellationToken);
    }

    private void WritePlan(
        IReadOnlyList<DuplicateRulePlan> plans,
        IReadOnlyList<DuplicateRuleCapacity> capacity,
        DuplicateRuleApplyOptions options)
    {
        foreach (var plan in plans)
        {
            console.MarkupLine($"[{(plan.HasChanges ? "yellow" : "grey")}]{plan.Label.EscapeMarkup()}: {Describe(plan).EscapeMarkup()}[/]");
        }

        foreach (var table in capacity)
        {
            console.MarkupLine(
                $"Capacity '{table.BaseTable.EscapeMarkup()}': {table.Occupied} occupied, -{table.Released} unpublish, " +
                $"+{table.Added} publish, projected {table.Projected}/{DuplicateRulePreflight.MaximumPublishedRulesPerTable}");
        }

        console.MarkupLine("Phases: 1) unpublish with read-back, 2) owner assignment and publish submission in file order, 3) publish observation.");
        console.MarkupLine(options.NoWait
            ? "Mode: submission only (--no-wait); server-side publish outcomes are not verified."
            : $"Mode: wait up to {options.Timeout.TotalSeconds:0} second(s) for submitted publishes to complete.");
    }

    private async Task<bool> ExecuteAsync(
        IOrganizationServiceAsync2 service,
        IReadOnlyList<DuplicateRulePlan> plans,
        IReadOnlyList<DuplicateRuleCapacity> capacity,
        DuplicateRuleApplyOptions options,
        CancellationToken cancellationToken)
    {
        var occupancy = capacity.ToDictionary(table => table.BaseTable, table => table.Occupied, StringComparer.OrdinalIgnoreCase);
        var failed = new HashSet<Guid>();
        var submitted = new List<SubmittedPublish>();
        var completed = 0;
        try
        {
            foreach (var plan in plans.Where(plan => plan.NeedsUnpublish))
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (await UnpublishAsync(service, plan, cancellationToken))
                {
                    completed++;
                    if (occupancy.TryGetValue(plan.BaseTable, out var occupied))
                    {
                        occupancy[plan.BaseTable] = occupied - 1;
                    }
                }
                else
                {
                    failed.Add(plan.Intent.RuleId);
                }
            }

            foreach (var plan in plans.Where(plan => !failed.Contains(plan.Intent.RuleId)))
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (plan.NeedsAssignment)
                {
                    if (!await AssignAsync(service, plan, cancellationToken))
                    {
                        failed.Add(plan.Intent.RuleId);
                        if (plan.NeedsPublish)
                        {
                            console.MarkupLine($"[red]{plan.Label.EscapeMarkup()}: publish skipped because owner assignment failed[/]");
                        }

                        continue;
                    }

                    completed++;
                }

                if (!plan.NeedsPublish)
                {
                    continue;
                }

                if (occupancy.TryGetValue(plan.BaseTable, out var occupied) &&
                    occupied >= DuplicateRulePreflight.MaximumPublishedRulesPerTable)
                {
                    failed.Add(plan.Intent.RuleId);
                    console.MarkupLine(
                        $"[red]{plan.Label.EscapeMarkup()}: publish not submitted; no publication slot available because an expected unpublish did not complete[/]");
                    continue;
                }

                var jobId = await SubmitPublishAsync(service, plan, cancellationToken);
                if (jobId is null)
                {
                    failed.Add(plan.Intent.RuleId);
                    continue;
                }

                completed++;
                occupancy[plan.BaseTable] = occupied + 1;
                submitted.Add(new SubmittedPublish(plan, jobId.Value));
            }

            if (submitted.Count > 0 && options.NoWait)
            {
                foreach (var publish in submitted)
                {
                    console.MarkupLine(
                        $"[green]{publish.Plan.Label.EscapeMarkup()}: publish submitted (job {publish.JobId:D}); completion not verified[/]");
                }
            }
            else if (submitted.Count > 0)
            {
                foreach (var failure in await ObserveAsync(service, submitted, options.Timeout, cancellationToken))
                {
                    failed.Add(failure);
                }
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            console.MarkupLine(
                $"[yellow]Cancelled after {completed} completed operation(s). Submitted publish jobs are not cancelled and may still run on the server.[/]");
            return false;
        }

        console.MarkupLine($"Duplicate-rule apply completed {completed} operation(s); {failed.Count} rule(s) failed.");
        if (options.NoWait && submitted.Count > 0)
        {
            console.MarkupLine("[yellow]Submitted publishes were not observed; later server-side outcomes are unverified.[/]");
        }

        return failed.Count == 0;
    }

    private async Task<bool> UnpublishAsync(IOrganizationServiceAsync2 service, DuplicateRulePlan plan, CancellationToken cancellationToken)
    {
        try
        {
            await service.ExecuteAsync(new UnpublishDuplicateRuleRequest { DuplicateRuleId = plan.Intent.RuleId }, cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            console.MarkupLine($"[red]{plan.Label.EscapeMarkup()}: unpublish failed; its slot remains occupied and dependent work is skipped: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        Entity readBack;
        try
        {
            readBack = await service.RetrieveAsync(
                DuplicateRule.EntityLogicalName,
                plan.Intent.RuleId,
                new ColumnSet("statecode", "statuscode"),
                cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            console.MarkupLine($"[red]{plan.Label.EscapeMarkup()}: unpublish could not be confirmed; its slot is treated as occupied: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        if (DuplicateRuleStates.Classify(readBack) != DuplicateRulePublicationState.Unpublished)
        {
            console.MarkupLine(
                $"[red]{plan.Label.EscapeMarkup()}: unpublish was not confirmed (observed {DuplicateRuleStates.Describe(readBack).EscapeMarkup()}); its slot is treated as occupied[/]");
            return false;
        }

        console.MarkupLine($"[green]{plan.Label.EscapeMarkup()}: unpublished[/]");
        return true;
    }

    private async Task<bool> AssignAsync(IOrganizationServiceAsync2 service, DuplicateRulePlan plan, CancellationToken cancellationToken)
    {
        try
        {
            await service.ExecuteAsync(
                new AssignRequest
                {
                    Assignee = plan.DesiredOwner!.Principal,
                    Target = new EntityReference(DuplicateRule.EntityLogicalName, plan.Intent.RuleId)
                },
                cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            console.MarkupLine($"[red]{plan.Label.EscapeMarkup()}: owner assignment failed: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        console.MarkupLine($"[green]{plan.Label.EscapeMarkup()}: owner assigned to '{plan.DesiredOwner.LogicalKey.EscapeMarkup()}'[/]");
        return true;
    }

    private async Task<Guid?> SubmitPublishAsync(IOrganizationServiceAsync2 service, DuplicateRulePlan plan, CancellationToken cancellationToken)
    {
        try
        {
            var response = (PublishDuplicateRuleResponse)await service.ExecuteAsync(
                new PublishDuplicateRuleRequest { DuplicateRuleId = plan.Intent.RuleId },
                cancellationToken);
            console.MarkupLine($"[grey]{plan.Label.EscapeMarkup()}: publish accepted (job {response.JobId:D})[/]");
            return response.JobId;
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            console.MarkupLine($"[red]{plan.Label.EscapeMarkup()}: publish failed: {DuplicateRuleFaults.DescribePublishFailure(exception).EscapeMarkup()}[/]");
            return null;
        }
    }

    private async Task<IReadOnlyList<Guid>> ObserveAsync(
        IOrganizationServiceAsync2 service,
        IReadOnlyList<SubmittedPublish> submitted,
        TimeSpan timeout,
        CancellationToken cancellationToken)
    {
        var failures = new List<Guid>();
        var pending = submitted.ToDictionary(publish => publish.Plan.Intent.RuleId);
        var lastObserved = pending.Keys.ToDictionary(id => id, _ => DuplicateRulePublicationState.Publishing.ToString());
        var deadline = timeProvider.GetUtcNow() + timeout;
        while (pending.Count > 0)
        {
            var remaining = deadline - timeProvider.GetUtcNow();
            if (remaining <= TimeSpan.Zero)
            {
                break;
            }

            await Task.Delay(remaining < PollInterval ? remaining : PollInterval, timeProvider, cancellationToken);
            Dictionary<Guid, Entity> rules;
            Dictionary<Guid, Entity> jobs;
            try
            {
                rules = await RetrieveRulesAsync(service, pending.Keys, cancellationToken);
                jobs = await RetrieveJobsAsync(service, pending.Values.Select(publish => publish.JobId), cancellationToken);
            }
            catch (Exception exception) when (exception is not OperationCanceledException)
            {
                foreach (var ruleId in pending.Keys)
                {
                    lastObserved[ruleId] = $"unavailable ({exception.Message})";
                }

                continue;
            }

            foreach (var publish in pending.Values.ToArray())
            {
                var ruleId = publish.Plan.Intent.RuleId;
                if (!rules.TryGetValue(ruleId, out var rule))
                {
                    Fail(publish, "the rule no longer exists");
                    continue;
                }

                var state = DuplicateRuleStates.Classify(rule);
                lastObserved[ruleId] = DuplicateRuleStates.Describe(rule);
                jobs.TryGetValue(publish.JobId, out var job);
                var jobState = job?.GetAttributeValue<OptionSetValue>("statecode")?.Value;
                var jobStatus = job?.GetAttributeValue<OptionSetValue>("statuscode")?.Value;
                if (state == DuplicateRulePublicationState.Published)
                {
                    pending.Remove(ruleId);
                    console.MarkupLine($"[green]{publish.Plan.Label.EscapeMarkup()}: published (job {publish.JobId:D})[/]");
                }
                else if (jobStatus is JobFailed or JobCanceled)
                {
                    var detail = job!.GetAttributeValue<string>("friendlymessage") ?? job.GetAttributeValue<string>("message");
                    Fail(publish, $"publish job {(jobStatus == JobFailed ? "failed" : "was canceled")}" +
                                  (string.IsNullOrWhiteSpace(detail) ? string.Empty : $": {detail}"));
                }
                else if (state != DuplicateRulePublicationState.Publishing && (job is null || jobState == JobCompleted))
                {
                    Fail(publish, $"publish ended without Published state (job status {jobStatus?.ToString(CultureInfo.InvariantCulture) ?? "unavailable"})");
                }
            }
        }

        foreach (var publish in pending.Values)
        {
            failures.Add(publish.Plan.Intent.RuleId);
            console.MarkupLine(
                $"[red]{publish.Plan.Label.EscapeMarkup()}: publish not confirmed within {timeout.TotalSeconds:0} second(s); " +
                $"last observed {lastObserved[publish.Plan.Intent.RuleId].EscapeMarkup()}; job {publish.JobId:D} may still run on the server[/]");
        }

        return failures;

        void Fail(SubmittedPublish publish, string reason)
        {
            pending.Remove(publish.Plan.Intent.RuleId);
            failures.Add(publish.Plan.Intent.RuleId);
            console.MarkupLine($"[red]{publish.Plan.Label.EscapeMarkup()}: {reason.EscapeMarkup()} (job {publish.JobId:D})[/]");
        }
    }

    private static async Task<Dictionary<Guid, Entity>> RetrieveRulesAsync(
        IOrganizationServiceAsync2 service,
        IEnumerable<Guid> ruleIds,
        CancellationToken cancellationToken)
    {
        var query = new QueryExpression(DuplicateRule.EntityLogicalName)
        {
            ColumnSet = new ColumnSet("duplicateruleid", "statecode", "statuscode")
        };
        query.Criteria.AddCondition("duplicateruleid", ConditionOperator.In, ruleIds.Cast<object>().ToArray());
        var rules = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        return rules.DistinctBy(rule => rule.Id).ToDictionary(rule => rule.Id);
    }

    private static async Task<Dictionary<Guid, Entity>> RetrieveJobsAsync(
        IOrganizationServiceAsync2 service,
        IEnumerable<Guid> jobIds,
        CancellationToken cancellationToken)
    {
        var ids = jobIds.Where(id => id != Guid.Empty).Distinct().Cast<object>().ToArray();
        if (ids.Length == 0)
        {
            return [];
        }

        var query = new QueryExpression("asyncoperation")
        {
            ColumnSet = new ColumnSet("asyncoperationid", "statecode", "statuscode", "message", "friendlymessage")
        };
        query.Criteria.AddCondition("asyncoperationid", ConditionOperator.In, ids);
        var jobs = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        return jobs.DistinctBy(job => job.Id).ToDictionary(job => job.Id);
    }

    private static string Describe(DuplicateRulePlan plan)
    {
        var changes = new List<string>();
        if (plan.NeedsUnpublish)
        {
            changes.Add("unpublish");
        }

        if (plan.NeedsAssignment)
        {
            changes.Add($"assign owner '{plan.DesiredOwner!.LogicalKey}' ({plan.DesiredOwner.Kind})");
        }

        if (plan.NeedsPublish)
        {
            changes.Add("publish");
        }

        return changes.Count == 0 ? $"unchanged ({plan.CurrentState})" : string.Join(" -> ", changes);
    }

    private sealed record SubmittedPublish(DuplicateRulePlan Plan, Guid JobId);
}
