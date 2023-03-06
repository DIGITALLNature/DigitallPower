// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
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
    public UpdateWorkflowState(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver) { }

    protected override bool Invoke(MaintenanceVerb args)
    {
        Tracer.Start(this);

        // Inline arguments are not yet supported so throw error if supplied
        if (string.IsNullOrWhiteSpace(args?.InlineData))
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
                updateModernFlowTask.MaxValue = flows.Count;
                updateModernFlowTask.StartTask();

                await UpdateModernFlows(flows, workflowConfig.Flows, updateModernFlowTask);

                updateModernFlowTask.StopTask();
                return true;
            });
    }

    private Task<bool> UpdateModernFlows(List<Workflow> flows, IDictionary<string, WorkflowConfig.FlowConfig>? flowConfigs, ProgressTask progressTask)
    {
        // Introduce rounds because we might need multiple turns if child flows exist. Instead of figuring out the hierarchy we just repeat as long as each round has some successes
        // Keep track of failues in a dictionary
        var round = 0;
        var updateResults = flows.ToDictionary(f => f, _ => false);

        int previousFailures = 0, currentFailures = 0;
        do
        {
            previousFailures = currentFailures;
            Tracer.Log($"Updating Flows - Round {round} ({previousFailures} failures)", TraceEventType.Information);

            // Traverse all workflows that were markes as failures in the last round (initially all are marked as failures)
            foreach (var workflow in updateResults.Where(r => !r.Value))
            {
                if (TryUpdateModernFlow(workflow, flowConfigs))
                {
                    updateResults[workflow.Key] = true;
                    progressTask.Increment(1);
                }
            }

            currentFailures = updateResults.Count(r => !r.Value);
        } while (currentFailures > 0 && currentFailures < previousFailures);

        return Task.FromResult(previousFailures > 0);
    }

    private bool TryUpdateModernFlow(KeyValuePair<Workflow,bool> workflow, IDictionary<string, WorkflowConfig.FlowConfig>? flowConfigs)
    {
        var flow = workflow.Key;
        var flowName = flow.Name;

        if (string.IsNullOrWhiteSpace(flowName))
        {
            Tracer.Log($"[red] - Unable to process flow without name ({workflow.Key.Id})", TraceEventType.Error);
            return false;
        }

        Tracer.Log($"[grey] - Checking flow '{flowName.EscapeMarkup()}'", TraceEventType.Verbose);

        // Check if flow has an existing config
        WorkflowConfig.FlowConfig? flowConfig = default;
        flowConfigs?.TryGetValue(flowName, out flowConfig);

        var disabled = flowConfig?.Disabled ?? false; // Default to enable flow if nothing is set in config

        // TODO: Extend for owner and impersonate

        // Prepare flow object to update (but only if necessary)
        var updateFlow = new Workflow(flow.Id);
        var updateNeeded = false;

        Tracer.Log($"[grey]   > Desired: disabled={disabled}; Current: disabled={flow.StateCode?.Value != Workflow.Options.StateCode.Activated}[/]", TraceEventType.Verbose);
        switch (disabled)
        {
            // If flow should be disaled but is activated try deactivate it
            case true when flow.StateCode?.Value == Workflow.Options.StateCode.Activated:
                updateFlow.StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft);
                updateFlow.StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft);
                break;
            // If flow should be enabled but is disabled try activate it
            case false when flow.StateCode?.Value != Workflow.Options.StateCode.Activated:
                updateFlow.StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated);
                updateFlow.StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated);
                break;
        }

        if (!updateNeeded)
        {
            Tracer.Log($" > Flow already up to date: [blue]'{flow.Name.EscapeMarkup()}'[/]", TraceEventType.Information);
            return true;
        }

        Tracer.Log($"[grey]   > Update needed. Performing update[/]", TraceEventType.Verbose);
        try
        {
            Connection.Update(updateFlow);
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
                    // Add condition to link filter with unique comparison wich allows fetch xml patterns (%)
                    linkFilter.AddCondition("solution", Solution.LogicalNames.UniqueName, ConditionOperator.Like, solution);
                }
            }

            // Add publisher filters if any exist
            if (publishers?.Any() == true)
            {
                var publisherLink = solutionLink.AddLink(Publisher.EntityLogicalName, Publisher.LogicalNames.PublisherId, Solution.LogicalNames.PublisherId, JoinOperator.LeftOuter);
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
