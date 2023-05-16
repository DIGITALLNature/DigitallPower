// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Reflection;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Base.Config;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public class UpdateWorkflowState : BaseMaintenance
{
    private readonly Dictionary<string, SystemUser> _userTable;

    public UpdateWorkflowState(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
        _userTable = new Dictionary<string, SystemUser>();
    }

    protected override bool Invoke(MaintenanceVerb args)
    {
        Tracer.Start(this);

        // Inline arguments are not yet supported so throw error if supplied
        if (!string.IsNullOrWhiteSpace(args.InlineData))
        {
            throw new NotImplementedException("Inline arguments are not yet supported");
        }

        // Check if required config is available
        if (!ConfigResolver.GetConfigFile<WorkflowConfig>(args.Config, out var workflowConfig))
        {
            return Tracer.End(this, false);
        }

        // Do the actual work
        var task = UpdateWorkflowStates(workflowConfig);
        task.Wait();

        return Tracer.End(this, task.Result);
    }

    /// <summary>
    /// Take a given workflow config and update corresponding flows
    /// </summary>
    /// <param name="workflowConfig">The underlying workflow config</param>
    /// <returns>True if no errors occured otherwise False</returns>
    private async Task<bool> UpdateWorkflowStates(WorkflowConfig workflowConfig)
    {
        return await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var loadModernFlowTask = ctx.AddTask("Loading modern flows").IsIndeterminate();
                var updateModernFlowTask = ctx.AddTask("Update modern flows", false);

                // Load modern flows
                var flows = await LoadModernFlows(workflowConfig.SolutionFilter, workflowConfig.PublisherFilter);
                loadModernFlowTask.Value = loadModernFlowTask.MaxValue;
                loadModernFlowTask.StopTask();

                // Go through found flows and update state and owner
                updateModernFlowTask.MaxValue = flows.Count + 1;
                updateModernFlowTask.Increment(1);
                updateModernFlowTask.StartTask();

                var result = await UpdateModernFlows(flows, workflowConfig, updateModernFlowTask);

                updateModernFlowTask.StopTask();
                return result;
            });
    }

    /// <summary>
    /// Take a list of given flows and update them so they fit the given config
    /// </summary>
    /// <param name="flows">List of modern flows</param>
    /// <param name="workflowConfig">Workflow Config</param>
    /// <param name="progressTask">Progress task to report progress to the calling method</param>
    /// <returns></returns>
    private async Task<bool> UpdateModernFlows(List<Workflow> flows, WorkflowConfig workflowConfig, ProgressTask? progressTask)
    {
        // Introduce rounds because we might need multiple turns if child flows exist. Instead of figuring out the hierarchy we just repeat as long as each round has some successes
        // Keep track of failures in a dictionary
        var round = 0;
        var updateResults = flows.ToDictionary(f => f, _ => false);

        int previousFailures, currentFailures = updateResults.Count;
        do
        {
            previousFailures = currentFailures;
            Tracer.Log($"Updating Flows - Round {round++} ({previousFailures} failures)", TraceEventType.Information);

            // Traverse all workflows that were marked as failures in the last round (initially all are marked as failures)
            foreach (var workflow in updateResults.Where(r => !r.Value))
            {
                if (await TryUpdateModernFlow(workflow.Key, workflowConfig))
                {
                    updateResults[workflow.Key] = true;
                    progressTask?.Increment(1);
                }
            }

            currentFailures = updateResults.Count(r => !r.Value);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        } while (currentFailures > 0 && currentFailures < previousFailures);

        return currentFailures <= 0;
    }

    /// <summary>
    /// Updates a single flow to fit a given config
    /// </summary>
    /// <param name="workflow">Given flow</param>
    /// <param name="workflowConfig">Workflow config</param>
    /// <returns></returns>
    private async Task<bool> TryUpdateModernFlow(Workflow workflow, WorkflowConfig workflowConfig)
    {
        var flow = workflow;
        var flowName = flow.Name;

        if (string.IsNullOrWhiteSpace(flowName))
        {
            Tracer.Log($"[red] - Unable to process flow without name ({workflow.Id})[/]", TraceEventType.Error);
            return false;
        }

        Tracer.Log($"[grey] - Checking flow '{flowName.EscapeMarkup()}'[/]", TraceEventType.Verbose);

        // Check if flow has an existing config
        WorkflowConfig.FlowConfig? flowConfig = default;
        workflowConfig.Flows?.TryGetValue(flowName, out flowConfig);

        var disabled = flowConfig?.Disabled ?? false; // Default to enable flow if nothing is set in config
        var ownerText = flowConfig?.Owner ?? workflowConfig.DefaultOwner; // Owner specified on flow level overrides default (empty string also triggers override)
        var impersonateText = flowConfig?.Impersonate ?? workflowConfig.DefaultImpersonate; // Impersonate specified on flow level overrides default (empty string also triggers override)

        var owner = await ResolveSystemUser(ownerText);
        var impersonate = await ResolveSystemUser(impersonateText);

        // Prepare flow object to update (but only if necessary)
        var updateFlow = new Workflow(flow.Id);
        var updateNeeded = false;

        Tracer.Log($"[grey]   > Desired State: disabled={disabled}; Current: disabled={flow.StateCode?.Value != Workflow.Options.StateCode.Activated}[/]", TraceEventType.Verbose);
        switch (disabled)
        {
            // If flow should be disabled but is activated try deactivate it
            case true when flow.StateCode?.Value == Workflow.Options.StateCode.Activated:
                updateFlow.StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft);
                updateFlow.StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft);
                updateNeeded = true;
                break;
            // If flow should be enabled but is disabled try activate it
            case false when flow.StateCode?.Value != Workflow.Options.StateCode.Activated:
                updateFlow.StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated);
                updateFlow.StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated);
                updateNeeded = true;
                break;
        }

        flow.FormattedValues.TryGetValue(Workflow.LogicalNames.OwnerId, out var currentOwnerText);
        Tracer.Log($"[grey]   > Desired Owner: {owner?.Id} ({owner?.FullName.EscapeMarkup()}); Current: {flow.OwnerId?.Id} ({currentOwnerText.EscapeMarkup()})[/]", TraceEventType.Verbose);
        if (owner?.Id != default && owner.Id != flow.OwnerId?.Id)
        {
            updateFlow.OwnerId = owner.ToEntityReference();
            updateNeeded = true;
        }

        if (!updateNeeded)
        {
            Tracer.Log($" > Flow already up to date: [blue]'{flow.Name.EscapeMarkup()}'[/]", TraceEventType.Information);
            return true;
        }

        Tracer.Log($"[grey]   > Update needed. Performing update[/]", TraceEventType.Verbose);
        try
        {
            if (impersonate != default)
            {
                var callerId = Connection.GetType().GetProperty("CallerId", BindingFlags.Instance | BindingFlags.Public);

                // Check if used service supports impersonation. This should be the case in real scenarios, but might fail with unit tests
                if (callerId == default)
                {
                    Tracer.Log($"[orange3]   > Service '{Connection.GetType().Name}' does not support impersonation. Continuing without[/]", TraceEventType.Warning);
                    Connection.Update(updateFlow);
                }
                else
                {
                    Tracer.Log($"[grey]   > Using impersonation of {impersonate.Id}[/]", TraceEventType.Verbose);

                    // Store current caller id and restore it since we want to reuse the connection
                    var oldCaller = callerId.GetValue(Connection);
                    callerId.SetValue(Connection, impersonate.Id);
                    Connection.Update(updateFlow);
                    callerId.SetValue(Connection, oldCaller);
                }
            }
            else
            {
                Connection.Update(updateFlow);
            }

            Tracer.Log($" > Updated flow successfully: [green]'{flow.Name.EscapeMarkup()}'[/]", TraceEventType.Information);
            return true;
        }
        catch (Exception e)
        {
            Tracer.Log($"[red] > Unable to update flow '{flow.Name.EscapeMarkup()}':[/] [grey]{e.Message}[/]", TraceEventType.Error);
            return false;
        }
    }

    /// <summary>
    /// Take a username and look for a systemuser with a corresponding domain name
    /// </summary>
    /// <param name="username">Domain name to look for</param>
    /// <returns>Found user or default object if not found</returns>
    private Task<SystemUser?> ResolveSystemUser(string? username)
    {
        // If no username is given return default
        if (string.IsNullOrWhiteSpace(username))
        {
            Tracer.Log("[grey]   > No username given[/]", TraceEventType.Verbose);
            return Task.FromResult<SystemUser?>(default);
        }
        Tracer.Log($"[grey]   > Trying to resolve '{username}'[/]", TraceEventType.Verbose);

        // Check if we seen the username already and if so grab it from the table
        if (!string.IsNullOrWhiteSpace(username) && _userTable.TryGetValue(username, out var user))
        {
            Tracer.Log($"[grey]   > Resolved user '{username.EscapeMarkup()}' ({user.Id})[/]", TraceEventType.Verbose);
            return Task.FromResult<SystemUser?>(user);
        }

        // Check for existing systemuser with domain name
        var query = new QueryExpression
        {
            EntityName = SystemUser.EntityLogicalName,
            ColumnSet = new ColumnSet(SystemUser.LogicalNames.SystemUserId, SystemUser.LogicalNames.DomainName, SystemUser.LogicalNames.FullName),
            Distinct = true,
            NoLock = true,
            TopCount = 1,
            Criteria = new FilterExpression(LogicalOperator.And)
            {
                Conditions = { new ConditionExpression(SystemUser.LogicalNames.DomainName, ConditionOperator.Equal, username) },
            },
        };

        user = Connection.RetrieveMultiple(query).Entities.Cast<SystemUser>().SingleOrDefault();

        // If we do not find a user fall return default
        if (user == default)
        {
            Tracer.Log($"[orange3]   > No user found for '{username.EscapeMarkup()}'[/]", TraceEventType.Warning);
            return Task.FromResult<SystemUser?>(default);
        }

        _userTable.TryAdd(username, user);
        return Task.FromResult<SystemUser?>(user);
    }

    /// <summary>
    /// Load and return modern flows that are included by any of the filters
    /// Comparisons are done with the fetch xml like filter so wildcards (%) are possible to be used
    /// The result includes any flow that is in any solution with a given name or publisher regardless whether the solution is unmanged
    /// or the flow is included in multiple (e.g. temp solutions) where only one fits
    /// </summary>
    /// <param name="solutions">List of uniquenames for solutions to consider</param>
    /// <param name="publishers">List of publishers of solutions to consider</param>
    /// <returns></returns>
    private Task<List<Workflow>> LoadModernFlows(string[]? solutions, string[]? publishers)
    {
        // Prepare query expression to load modern flows
        var query = new QueryExpression(Workflow.EntityLogicalName);
        query.ColumnSet.AddColumns(Workflow.LogicalNames.Category,
            Workflow.LogicalNames.StatusCode,
            Workflow.LogicalNames.Name,
            Workflow.LogicalNames.UniqueName,
            Workflow.LogicalNames.StateCode,
            Workflow.LogicalNames.PrimaryEntity,
            Workflow.LogicalNames.OwnerId);

        var filter = new FilterExpression();
        query.Criteria.AddFilter(filter);
        filter.AddCondition(Workflow.LogicalNames.Type, ConditionOperator.Equal, Workflow.Options.Type.Definition);
        filter.AddCondition(Workflow.LogicalNames.Category, ConditionOperator.Equal, Workflow.Options.Category.ModernFlow);

        // If either solution or publisher filter exist add them
        if (solutions?.Any() == true || publishers?.Any() == true)
        {
            // Prepare solution link and filter (this is needed for solutions and publisher)
            var linkFilter = new FilterExpression(LogicalOperator.Or);
            filter.AddFilter(linkFilter);

            var componentLink = query.AddLink(SolutionComponent.EntityLogicalName, Workflow.LogicalNames.WorkflowId, SolutionComponent.LogicalNames.ObjectId);
            var solutionLink = componentLink.AddLink(Solution.EntityLogicalName, SolutionComponent.LogicalNames.SolutionId, Solution.LogicalNames.SolutionId);
            solutionLink.EntityAlias = "solution";

            // Add solution filters if any exist
            if (solutions?.Any() == true)
            {
                foreach (var solution in solutions)
                {
                    Tracer.Log($"[grey] -> Adding solution condition '{solution.EscapeMarkup()}'[/]", TraceEventType.Verbose);
                    // Add condition to link filter with unique comparison which allows fetch xml patterns (%)
                    linkFilter.AddCondition("solution", Solution.LogicalNames.UniqueName, ConditionOperator.Like, solution);
                }
            }

            // Add publisher filters if any exist
            if (publishers?.Any() == true)
            {
                var publisherLink = solutionLink.AddLink(Publisher.EntityLogicalName, Solution.LogicalNames.PublisherId, Publisher.LogicalNames.PublisherId, JoinOperator.LeftOuter);
                publisherLink.EntityAlias = "publisher";

                foreach (var publisher in publishers)
                {
                    Tracer.Log($"[grey] -> Adding publisher name condition '{publisher.EscapeMarkup()}'[/]", TraceEventType.Verbose);
                    linkFilter.AddCondition("publisher", Publisher.LogicalNames.UniqueName, ConditionOperator.Like, publisher);
                }
            }
        }

        var workflows = Connection.RetrieveMultiple(query);
        Tracer.Log($"Found {workflows.Entities.Count} modern flows", TraceEventType.Verbose);

        // Check if we have reached the limit of query expressions
        // Assumption is that there is only a slim chance we ever hit that limit so pagination is not worth it here
        if (workflows.Entities.Count == 5000)
        {
            Tracer.Log("Found workflows that correspond to the max limit of query expressions. Possibly not all modern flows were found.", TraceEventType.Warning);
        }

        return Task.FromResult(workflows.Entities.Cast<Workflow>().ToList());
    }
}
