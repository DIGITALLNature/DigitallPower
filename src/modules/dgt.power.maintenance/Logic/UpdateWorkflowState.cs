// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Reflection;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Base.Config;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class UpdateWorkflowState(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<UpdateWorkflowStateSettings>(tracer, connection, configResolver, console)
{
    private readonly Dictionary<string, SystemUser> _userTable = new();
    private readonly WorkflowStateTracker _workflowStateTracker = new();

    protected override bool Invoke(UpdateWorkflowStateSettings args)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentException.ThrowIfNullOrWhiteSpace(args.Config);

        throw new NotSupportedException("This command requires async execution. Use InvokeAsync.");
    }

    protected override Task<bool> InvokeAsync(UpdateWorkflowStateSettings args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);

        return InvokeCoreAsync(args, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(UpdateWorkflowStateSettings args, CancellationToken cancellationToken)
    {
        _ = cancellationToken;
        Tracer.Start(this);

        // Check if required config is available
        if (!ConfigResolver.TryGetConfigFile<WorkflowConfig>(args.Config, out var workflowConfig))
        {
            return Tracer.End(this, false);
        }

        // Do the actual work
        var result = await UpdateWorkflowStatesAsync(workflowConfig, workflowConfig.SolutionFilter, workflowConfig.PublisherFilter);

        // Print table report if requested
        if (args.TableReport)
        {
            _workflowStateTracker.WriteToConsole(Console);
        }

        return Tracer.End(this, result);
    }

    /// <summary>
    /// Take a given workflow config and update corresponding flows
    /// </summary>
    /// <param name="workflowConfig">The underlying workflow config</param>
    /// <param name="solutions">Optional solution filter</param>
    /// <param name="publishers">Optional publisher filter</param>
    /// <returns>True if no errors occured otherwise False</returns>
    private async Task<bool> UpdateWorkflowStatesAsync(
        WorkflowConfig workflowConfig,
        IReadOnlyList<string>? solutions,
        IReadOnlyList<string>? publishers)
    {
        return await Console.Progress()
            .StartAsync(async ctx =>
            {
                Dictionary<string, ProgressTask> progressTasks = new();
                WorkflowStateManager.TaskStatusCallback progress = (taskName, message, current, max) =>
                {
                    if (!progressTasks.TryGetValue(taskName, out var progressTask))
                    {
                        progressTask = ctx.AddTask($"{taskName.EscapeMarkup()} [grey]({message.EscapeMarkup()})[/]");
                        progressTasks.Add(taskName, progressTask);
                    }

                    progressTask.Description($"{taskName.EscapeMarkup()} [grey]({message.EscapeMarkup()})[/]");
                    progressTask.IsIndeterminate(max <= 0);
                    progressTask.MaxValue(max > 0 ? max.Value : 100);
                    progressTask.Value(current);

                    ctx.Refresh();
                };

                var workflowStateManager = new WorkflowStateManager(
                    (IOrganizationServiceAsync2)Connection,
                    solutions?.ToArray() ?? [],
                    publishers?.ToArray() ?? [],
                    Tracer,
                    progress);

                Tracer.Log("Loading all workflows", TraceEventType.Information);
                var workflows = await workflowStateManager.LoadAllWorkflowsAsync();
                _workflowStateTracker.AddWorkflows(workflows);

                Tracer.Log($"Found {workflows.Length} workflows", TraceEventType.Information);

                Tracer.Log("Updating workflows", TraceEventType.Information);

                var result = await TryUpdateWorkflowsAsync(workflows, workflowConfig, progress);

                return result;
            });
    }

    private async Task<bool> TryUpdateWorkflowsAsync(Workflow[] workflows, WorkflowConfig workflowConfig, WorkflowStateManager.TaskStatusCallback progress)
    {
        // introduce rounds because we might need multiple turns if child flows exist. Instead of figuring out the hierarchy we just repeat as long as each round has some successes
        // keep track of failures in a dictionary
        var round = 0;
        var updateResults = workflows.ToDictionary(f => f, _ => false);

        int previousFailures;
        var currentFailures = updateResults.Count;
        do
        {
            previousFailures = currentFailures;
            currentFailures = 0;
            Tracer?.Log($"Updating workflows - round {round} ({previousFailures} failures)", TraceEventType.Information);
            progress?.Invoke($"Updating workflows - round {round}", $"{currentFailures} failures", 0, previousFailures);

            // traverse all workflows that were marked as failures in the last round (initially all are marked as failures)
            var index = 0;
            foreach (var workflow in updateResults.Where(r => !r.Value))
            {
                bool updateResult;

                try
                {
                    updateResult = await TryUpdateWorkflowAsync(workflow.Key, workflowConfig);
                }
                catch (InvalidDataException e)
                {
                    Tracer?.Log($"Unable to update workflow '{workflow.Key.WorkflowId}': {e.Message.EscapeMarkup()}", TraceEventType.Error);
                    updateResult = true;
                }
                catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
                {
                     Tracer?.Log($"Unable to update workflow '{workflow.Key.WorkflowId}': {e.Message.EscapeMarkup()}", TraceEventType.Error);
                    updateResult = false;
                }

                if (updateResult)
                {
                    updateResults[workflow.Key] = true;
                }
                else
                {
                    currentFailures++;
                }

                progress?.Invoke($"Updating workflows - round {round}", $"{currentFailures} failures", ++index, previousFailures);
            }

            round++;
            Tracer?.Log($"Updating workflows - round {round} end: current_failures={currentFailures};previous_failures:{previousFailures}", TraceEventType.Information);
        } while (currentFailures > 0 && currentFailures < previousFailures);

        return currentFailures <= 0;
    }

    private async Task<bool> TryUpdateWorkflowAsync(Workflow workflow, WorkflowConfig workflowConfig)
    {
        var workflowCategory = workflow.Category?.Value;
        if (workflowCategory is null)
        {
            Tracer?.Log($"Unable to process workflow {workflow.Id} category: <null>", TraceEventType.Error);
            return false;
        }

        Tracer.Log($"Workflow {workflow.Id} (category={workflowCategory}): checking against config", TraceEventType.Verbose);

        string workflowName;
        WorkflowBaseConfig? baseWorkflowConfig = null;
        // get name and config if exists based on workflow type
        switch (workflowCategory.Value)
        {
            case Workflow.Options.Category.ModernFlow:
            case Workflow.Options.Category.Workflow_:
            case Workflow.Options.Category.WebClientAPIFlow:
                workflowName = workflow.Name ?? throw new InvalidDataException($"Workflow {workflow.Id} (category={workflowCategory}) has no name");
                if (workflowConfig.Flows?.TryGetValue(workflowName, out var flowConfig) == true)
                {
                    Tracer?.Log($"Workflow {workflow.Id} (category={workflowCategory};" +
                                $"name='{workflowName.EscapeMarkup()}'): found config entries - disabled={flowConfig.Disabled};" +
                                $"owner={flowConfig.Owner};impersonate={flowConfig.Impersonate};", TraceEventType.Verbose);
                    baseWorkflowConfig = flowConfig;
                }
                break;
            case Workflow.Options.Category.Action:
            case Workflow.Options.Category.BusinessProcessFlow:
                workflowName = workflow.UniqueName ?? throw new InvalidDataException($"Workflow {workflow.Id} (category={workflowCategory}) has no unique name");
                if (workflowConfig.Actions?.TryGetValue(workflowName, out baseWorkflowConfig) == true)
                {
                    Tracer?.Log($"Workflow {workflow.Id} (category={workflowCategory};" +
                                $"name='{workflowName.EscapeMarkup()}'): found config entries - disabled={baseWorkflowConfig.Disabled};" +
                                $"owner={baseWorkflowConfig.Owner};", TraceEventType.Verbose);
                }
                break;
            case Workflow.Options.Category.BusinessRule:
                workflowName = workflow.Name ?? throw new InvalidDataException($"Workflow {workflow.Id} (category={workflowCategory};table={workflow.PrimaryEntity}) has no name");
                if (workflowConfig.BusinessRules?.TryGetValue(workflow.PrimaryEntity!, out var table) == true && table.TryGetValue(workflowName, out baseWorkflowConfig))
                {
                    Tracer?.Log($"Workflow {workflow.Id} (category={workflowCategory};" +
                                $"table={workflow.PrimaryEntity};" +
                                $"name='{workflowName.EscapeMarkup()}'): found config entries - disabled={baseWorkflowConfig.Disabled};" +
                                $"owner={baseWorkflowConfig.Owner};", TraceEventType.Verbose);
                }
                break;
            default:
                Tracer.Log($"Unable to process workflow {workflow.Id} category: {workflowCategory}", TraceEventType.Error);
                return false;
        }

        Tracer?.Log($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName.EscapeMarkup()}'): collecting desired values", TraceEventType.Verbose);
        var desiredDisabled = baseWorkflowConfig?.Disabled ?? false; // default to enable workflow if nothing is set in config
        var desiredOwnerDomainName = baseWorkflowConfig?.Owner ?? workflowConfig.DefaultOwner; // owner specified on workflow level overrides default (empty string also triggers override)
        // impersonate specified on workflow level overrides default (empty string also triggers override)
        var desiredImpersonate = (baseWorkflowConfig as WorkflowFlowConfig)?.Impersonate ?? workflowConfig.DefaultImpersonate;

        _workflowStateTracker.TrackDisabled(workflow, desiredDisabled);

        var desiredOwner = await ResolveSystemUserAsync(desiredOwnerDomainName);
        var desiredImpersonateUser = await ResolveSystemUserAsync(desiredImpersonate);

        _workflowStateTracker.TrackOwner(workflow, desiredOwner);

        // prepare workflow object to update (but only if necessary)
        var updateWorkflow = new Workflow(workflow.Id);
        var updateNeeded = false;

        var currentDisabled = workflow.StateCode?.Value != Workflow.Options.StateCode.Activated;
        Tracer?.Log($"Workflow {workflow.Id} (category={workflow.Category?.Value};" +
                    $"name='{workflowName.EscapeMarkup()}'): desired disabled - desired={desiredDisabled};current={currentDisabled};", TraceEventType.Verbose);

        var currentOwner = workflow.OwnerId;
        workflow.TryGetAttributeValue<AliasedValue>("owner.domainname", out var currentOwnerDomainNameAliased);
        var currentOwnerDomainName = currentOwnerDomainNameAliased?.Value as string;
        Tracer?.Log($"Workflow {workflow.Id} (category={workflow.Category?.Value};" +
                    $"name='{workflowName.EscapeMarkup()}'): desired owner - desired={desiredOwnerDomainName}({desiredOwner?.Id});" +
                    $"current={currentOwnerDomainName}'({workflow.OwnerId?.Id});", TraceEventType.Verbose);

        // if owner needs to be changed we first want to disable the workflow to avoid any issues
        // this is not necessary for modern flows but also does not hurt
        // > additionally this might help to detect issues with connection references
        if (desiredOwner != null && desiredOwner.SystemUserId != currentOwner?.Id && !currentDisabled)
        {
            Tracer?.Log($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName.EscapeMarkup()}'): disabling workflow for owner update", TraceEventType.Information);
            var disableWorkflowRequest = new UpdateRequest
            {
                Target = new Workflow(workflow.Id)
                {
                    StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
                    StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft)
                }
            };
            Connection.Execute(disableWorkflowRequest);

            // reset current disabled value to reflect the change
            currentDisabled = true;
            _workflowStateTracker.TrackDisabled(workflow, desiredDisabled, true);
        }

        switch (desiredDisabled)
        {
            // workflow should be disabled but is activated try deactivate it
            // also deactivate if owner should be different
            case true when !currentDisabled:
                updateWorkflow.StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft);
                updateWorkflow.StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft);
                updateNeeded = true;

                Tracer?.Log($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName.EscapeMarkup()}'): marking update - disabled={desiredDisabled}", TraceEventType.Information);
                break;
            // workflow should be enabled but is disabled try activate it
            case false when currentDisabled:
                updateWorkflow.StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated);
                updateWorkflow.StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated);
                updateNeeded = true;

                Tracer?.Log($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName.EscapeMarkup()}'): marking update - disabled={desiredDisabled}", TraceEventType.Information);
                break;
        }

        // check if owner needs to be changed and also skip any update if workflow is still active
        if (desiredOwner != null && desiredOwner.SystemUserId != currentOwner?.Id)
        {
            Tracer?.Log(
                $"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName.EscapeMarkup()}'): " +
                $"chaging owner from {currentOwnerDomainName}'({currentOwner?.Id}) to owner={desiredOwnerDomainName}({desiredOwner.Id})",
                TraceEventType.Information);
            Connection.Execute(new AssignRequest{ Assignee = desiredOwner.ToEntityReference(), Target = updateWorkflow.ToEntityReference() });
        }

        if (!updateNeeded)
        {
            Tracer?.Log($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName.EscapeMarkup()}'): no update needed", TraceEventType.Information);
            return true;
        }

        Tracer?.Log($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName.EscapeMarkup()}'): performing update", TraceEventType.Information);
        var updateRequest = new UpdateRequest { Target = updateWorkflow };

        if (desiredImpersonateUser != null)
        {
            var callerId = Connection.GetType().GetProperty("CallerId", BindingFlags.Instance | BindingFlags.Public);

            // Check if used service supports impersonation. This should be the case in real scenarios, but might fail with unit tests
            if (callerId == null)
            {
                Tracer?.Log($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName.EscapeMarkup()}'): Service '{Connection.GetType().Name.EscapeMarkup()}' does not support impersonation. Continuing without", TraceEventType.Warning);
                Connection.Execute(updateRequest);
            }
            else
            {
                Tracer?.Log($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName.EscapeMarkup()}'): Using impersonation of {desiredImpersonateUser.Id}", TraceEventType.Verbose);

                // Store current caller id and restore it since we want to reuse the connection
                var oldCaller = callerId.GetValue(Connection);
                callerId.SetValue(Connection, desiredImpersonateUser.Id);

                try
                {
                    Connection.Execute(updateRequest);
                }
                finally
                {
                    callerId.SetValue(Connection, oldCaller);
                }
            }
        }
        else
        {
            Connection.Execute(updateRequest);
        }

        _workflowStateTracker.TrackDisabled(workflow, desiredDisabled, desiredDisabled);
        _workflowStateTracker.TrackOwner(workflow, desiredOwner, desiredOwner);

        return true;
    }

    /// <summary>
    /// Take a username and look for a systemuser with a corresponding domain name
    /// </summary>
    /// <param name="username">Domain name to look for</param>
    /// <returns>Found user or default object if not found</returns>
    private Task<SystemUser?> ResolveSystemUserAsync(string? username)
    {
        // If no username is given return default
        if (string.IsNullOrWhiteSpace(username))
        {
            return Task.FromResult<SystemUser?>(null);
        }

        // Check if we seen the username already and if so grab it from the table
        if (_userTable.TryGetValue(username, out var user))
        {
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
                Conditions = { new ConditionExpression(SystemUser.LogicalNames.DomainName, ConditionOperator.Equal, username) }
            }
        };

        user = Connection.RetrieveMultiple(query).Entities.Cast<SystemUser>().SingleOrDefault();

        // If we do not find a user return default
        if (user == null)
        {
            Tracer.Log($"No user found for '{username.EscapeMarkup()}'", TraceEventType.Warning);
            return Task.FromResult<SystemUser?>(null);
        }

        _userTable.TryAdd(username, user);
        return Task.FromResult<SystemUser?>(user);
    }
}
