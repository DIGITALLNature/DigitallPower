// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Reflection;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Base.Config;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class UpdateWorkflowState(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<UpdateWorkflowStateSettings>(spans, console)
{
    private readonly Dictionary<string, SystemUser> _userTable = new();
    private readonly WorkflowStateTracker _workflowStateTracker = new();

    protected override async Task<bool> RunAsync(UpdateWorkflowStateSettings args, CancellationToken cancellationToken)
    {
        // Check if required config is available
        if (!configResolver.TryGetConfigFile<WorkflowConfig>(args.Config, out var workflowConfig))
        {
            return false;
        }

        var service = await connection.GetServiceAsync();

        // Do the actual work
        var result = await UpdateWorkflowStatesAsync(
            service,
            workflowConfig,
            workflowConfig.SolutionFilter,
            workflowConfig.PublisherFilter,
            cancellationToken);

        // Print table report if requested
        if (args.TableReport)
        {
            _workflowStateTracker.WriteToConsole(Console);
        }

        return result;
    }

    /// <summary>
    /// Take a given workflow config and update corresponding flows
    /// </summary>
    /// <param name="service">The asynchronous organization service</param>
    /// <param name="workflowConfig">The underlying workflow config</param>
    /// <param name="solutions">Optional solution filter</param>
    /// <param name="publishers">Optional publisher filter</param>
    /// <param name="cancellationToken">Cancels workflow retrieval and updates.</param>
    /// <returns>True if no errors occured otherwise False</returns>
    private async Task<bool> UpdateWorkflowStatesAsync(
        IOrganizationServiceAsync2 service,
        WorkflowConfig workflowConfig,
        IReadOnlyList<string>? solutions,
        IReadOnlyList<string>? publishers,
        CancellationToken cancellationToken)
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
                    service,
                    solutions?.ToArray() ?? [],
                    publishers?.ToArray() ?? [],
                    Console,
                    progress);

                Console.WriteLine("Loading all workflows");
                var workflows = await workflowStateManager.LoadAllWorkflowsAsync(cancellationToken);
                _workflowStateTracker.AddWorkflows(workflows);

                Console.WriteLine($"Found {workflows.Length} workflows");

                Console.WriteLine("Updating workflows");

                var result = await TryUpdateWorkflowsAsync(service, workflows, workflowConfig, progress, cancellationToken);

                return result;
            });
    }

    private async Task<bool> TryUpdateWorkflowsAsync(
        IOrganizationServiceAsync2 service,
        Workflow[] workflows,
        WorkflowConfig workflowConfig,
        WorkflowStateManager.TaskStatusCallback progress,
        CancellationToken cancellationToken)
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
            Console.WriteLine($"Updating workflows - round {round} ({previousFailures} failures)");
            progress?.Invoke($"Updating workflows - round {round}", $"{currentFailures} failures", 0, previousFailures);

            // traverse all workflows that were marked as failures in the last round (initially all are marked as failures)
            var index = 0;
            foreach (var workflow in updateResults.Where(r => !r.Value))
            {
                cancellationToken.ThrowIfCancellationRequested();
                bool updateResult;

                try
                {
                    updateResult = await TryUpdateWorkflowAsync(service, workflow.Key, workflowConfig, cancellationToken);
                }
                catch (InvalidDataException e)
                {
                    Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Unable to update workflow '{workflow.Key.WorkflowId}': {e.Message}[/]");
                    updateResult = true;
                }
                catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException and not OperationCanceledException)
                {
                    Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Unable to update workflow '{workflow.Key.WorkflowId}': {e.Message}[/]");
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
            Console.WriteLine($"Updating workflows - round {round} end: current_failures={currentFailures};previous_failures:{previousFailures}");
        } while (currentFailures > 0 && currentFailures < previousFailures);

        return currentFailures <= 0;
    }

    private async Task<bool> TryUpdateWorkflowAsync(
        IOrganizationServiceAsync2 service,
        Workflow workflow,
        WorkflowConfig workflowConfig,
        CancellationToken cancellationToken)
    {
        var workflowCategory = workflow.Category?.Value;
        if (workflowCategory is null)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Unable to process workflow {workflow.Id} category: <null>[/]");
            return false;
        }

        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Workflow {workflow.Id} (category={workflowCategory}): checking against config[/]");

        string workflowName;
        WorkflowBaseConfig? baseWorkflowConfig = null;
        // get name and config if exists based on workflow type
        switch (workflowCategory.Value)
        {
            case Workflow.Options.Category.ModernFlow:
            case Workflow.Options.Category.Workflow:
            case Workflow.Options.Category.AIFlow:
                workflowName = workflow.Name ?? throw new InvalidDataException($"Workflow {workflow.Id} (category={workflowCategory}) has no name");
                if (workflowConfig.Flows?.TryGetValue(workflowName, out var flowConfig) == true)
                {
                    var configLabel = $"Workflow {workflow.Id} (category={workflowCategory};name='{workflowName}')";
                    Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
                        $"[grey]{configLabel}: found config entries - disabled={flowConfig.Disabled};owner={flowConfig.Owner};impersonate={flowConfig.Impersonate};[/]");
                    baseWorkflowConfig = flowConfig;
                }
                break;
            case Workflow.Options.Category.Action:
            case Workflow.Options.Category.BusinessProcessFlow:
                workflowName = workflow.UniqueName ?? throw new InvalidDataException($"Workflow {workflow.Id} (category={workflowCategory}) has no unique name");
                if (workflowConfig.Actions?.TryGetValue(workflowName, out baseWorkflowConfig) == true)
                {
                    var configLabel = $"Workflow {workflow.Id} (category={workflowCategory};name='{workflowName}')";
                    Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
                        $"[grey]{configLabel}: found config entries - disabled={baseWorkflowConfig.Disabled};owner={baseWorkflowConfig.Owner};[/]");
                }
                break;
            case Workflow.Options.Category.BusinessRule:
                workflowName = workflow.Name ?? throw new InvalidDataException($"Workflow {workflow.Id} (category={workflowCategory};table={workflow.PrimaryEntity}) has no name");
                if (workflowConfig.BusinessRules?.TryGetValue(workflow.PrimaryEntity!, out var table) == true && table.TryGetValue(workflowName, out baseWorkflowConfig))
                {
                    var configLabel = $"Workflow {workflow.Id} (category={workflowCategory};table={workflow.PrimaryEntity};name='{workflowName}')";
                    Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
                        $"[grey]{configLabel}: found config entries - disabled={baseWorkflowConfig.Disabled};owner={baseWorkflowConfig.Owner};[/]");
                }
                break;
            default:
                Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Unable to process workflow {workflow.Id} category: {workflowCategory}[/]");
                return false;
        }

        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
            $"[grey]Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}'): collecting desired values[/]");
        var desiredDisabled = baseWorkflowConfig?.Disabled ?? false; // default to enable workflow if nothing is set in config
        var desiredOwnerDomainName = baseWorkflowConfig?.Owner ?? workflowConfig.DefaultOwner; // owner specified on workflow level overrides default (empty string also triggers override)
        // impersonate specified on workflow level overrides default (empty string also triggers override)
        var desiredImpersonate = (baseWorkflowConfig as WorkflowFlowConfig)?.Impersonate ?? workflowConfig.DefaultImpersonate;

        _workflowStateTracker.TrackDisabled(workflow, desiredDisabled);

        var desiredOwner = await ResolveSystemUserAsync(service, desiredOwnerDomainName, cancellationToken);
        var desiredImpersonateUser = await ResolveSystemUserAsync(service, desiredImpersonate, cancellationToken);

        _workflowStateTracker.TrackOwner(workflow, desiredOwner);

        // prepare workflow object to update (but only if necessary)
        var updateWorkflow = new Workflow(workflow.Id);
        var updateNeeded = false;

        var currentDisabled = workflow.StateCode?.Value != Workflow.Options.StateCode.Activated;
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
            $"[grey]Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}'): desired disabled - desired={desiredDisabled};current={currentDisabled};[/]");

        var currentOwner = workflow.OwnerId;
        workflow.TryGetAttributeValue<AliasedValue>("owner.domainname", out var currentOwnerDomainNameAliased);
        var currentOwnerDomainName = currentOwnerDomainNameAliased?.Value as string;
        var workflowLabel = $"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}')";
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
            $"[grey]{workflowLabel}: desired owner - desired={desiredOwnerDomainName}({desiredOwner?.Id});current={currentOwnerDomainName}'({workflow.OwnerId?.Id});[/]");

        // if owner needs to be changed we first want to disable the workflow to avoid any issues
        // this is not necessary for modern flows but also does not hurt
        // > additionally this might help to detect issues with connection references
        if (desiredOwner != null && desiredOwner.SystemUserId != currentOwner?.Id && !currentDisabled)
        {
            Console.WriteLine($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}'): disabling workflow for owner update");
            await service.UpdateAsync(new Workflow(workflow.Id)
            {
                StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
                StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft)
            }, cancellationToken);

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

                Console.WriteLine($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}'): marking update - disabled={desiredDisabled}");
                break;
            // workflow should be enabled but is disabled try activate it
            case false when currentDisabled:
                updateWorkflow.StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated);
                updateWorkflow.StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated);
                updateNeeded = true;

                Console.WriteLine($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}'): marking update - disabled={desiredDisabled}");
                break;
        }

        // check if owner needs to be changed and also skip any update if workflow is still active
        if (desiredOwner != null && desiredOwner.SystemUserId != currentOwner?.Id)
        {
            Console.WriteLine(
                $"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}'): " +
                $"chaging owner from {currentOwnerDomainName}'({currentOwner?.Id}) to owner={desiredOwnerDomainName}({desiredOwner.Id})");
            await service.ExecuteAsync(
                new AssignRequest { Assignee = desiredOwner.ToEntityReference(), Target = updateWorkflow.ToEntityReference() },
                cancellationToken);
        }

        if (!updateNeeded)
        {
            Console.WriteLine($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}'): no update needed");
            return true;
        }

        Console.WriteLine($"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}'): performing update");
        if (desiredImpersonateUser != null)
        {
            var callerId = service.GetType().GetProperty("CallerId", BindingFlags.Instance | BindingFlags.Public);

            // Check if used service supports impersonation. This should be the case in real scenarios, but might fail with unit tests
            if (callerId == null)
            {
                var noImpersonationLabel = $"Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}')";
                Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
                    $"[yellow]{noImpersonationLabel}: Service '{service.GetType().Name}' does not support impersonation. Continuing without[/]");
                await service.UpdateAsync(updateWorkflow, cancellationToken);
            }
            else
            {
                Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
                    $"[grey]Workflow {workflow.Id} (category={workflow.Category?.Value};name='{workflowName}'): Using impersonation of {desiredImpersonateUser.Id}[/]");

                // Store current caller id and restore it since we want to reuse the connection
                var oldCaller = callerId.GetValue(service);
                callerId.SetValue(service, desiredImpersonateUser.Id);

                try
                {
                    await service.UpdateAsync(updateWorkflow, cancellationToken);
                }
                finally
                {
                    callerId.SetValue(service, oldCaller);
                }
            }
        }
        else
        {
            await service.UpdateAsync(updateWorkflow, cancellationToken);
        }

        _workflowStateTracker.TrackDisabled(workflow, desiredDisabled, desiredDisabled);
        _workflowStateTracker.TrackOwner(workflow, desiredOwner, desiredOwner);

        return true;
    }

    /// <summary>
    /// Take a username and look for a systemuser with a corresponding domain name
    /// </summary>
    /// <param name="service">Organization service to query with</param>
    /// <param name="username">Domain name to look for</param>
    /// <param name="cancellationToken">Cancels the user lookup.</param>
    /// <returns>Found user or default object if not found</returns>
    private async Task<SystemUser?> ResolveSystemUserAsync(
        IOrganizationServiceAsync2 service,
        string? username,
        CancellationToken cancellationToken)
    {
        // If no username is given return default
        if (string.IsNullOrWhiteSpace(username))
        {
            return null;
        }

        // Check if we seen the username already and if so grab it from the table
        if (_userTable.TryGetValue(username, out var user))
        {
            return user;
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

        var response = await service.RetrieveMultipleAsync(query, cancellationToken);
        user = response.Entities.Cast<SystemUser>().SingleOrDefault();

        // If we do not find a user return default
        if (user == null)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]No user found for '{username}'[/]");
            return null;
        }

        _userTable.TryAdd(username, user);
        return user;
    }
}
