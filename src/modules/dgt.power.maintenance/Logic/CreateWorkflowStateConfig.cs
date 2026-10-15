// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Base.Config;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class CreateWorkflowStateConfig(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    JsonSerializerOptions jsonSerializerOptions,
    IAnsiConsole console)
    : PowerCommand<CreateWorkflowStateConfigSettings>(spans, console)
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new(jsonSerializerOptions) { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
    private readonly WorkflowStateTracker _workflowStateTracker = new();

    protected override async Task<bool> RunAsync([NotNull] CreateWorkflowStateConfigSettings args, CancellationToken cancellationToken)
    {
        var solutions = args.Solutions?.Split(',');
        var publishers = args.Publishers?.Split(',');

        var service = await connection.GetServiceAsync();

        // Do the actual work
        await CollectWorkflowStatesAsync(service, solutions, publishers, args.Config, args.Detailed, args.Overwrite);

        // Print table report if requested
        if (args.TableReport)
        {
            _workflowStateTracker.WriteToConsole(Console);
        }

        return true;
    }

    private async Task CollectWorkflowStatesAsync(IOrganizationServiceAsync2 service, string[]? solutions, string[]? publishers, string configFile, bool detailed, bool overwrite)
    {
        await Console.Progress()
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
                    progressTask.IsIndeterminate(max > 0);
                    progressTask.MaxValue(max > 0 ? max.Value : 100);
                    progressTask.Value(current);

                    ctx.Refresh();
                };

                var workflowStateManager = new WorkflowStateManager(service, solutions ?? [], publishers ?? [], Console, progress);

                Console.WriteLine("Loading all workflows");
                var workflows = await workflowStateManager.LoadAllWorkflowsAsync();
                _workflowStateTracker.AddWorkflows(workflows);

                Console.WriteLine($"Found {workflows.Length} workflows");

                // group all workflows by owner and select max occurence as default owner
                var groupedFlows = workflows.GroupBy(f => f.OwnerId!.Id).ToList();
                var defaultOwnerId = groupedFlows.OrderByDescending(g => g.Count()).FirstOrDefault()?.Key;
                var defaultOwner = defaultOwnerId != null ? workflows.First(w => w.OwnerId!.Id == defaultOwnerId).GetAttributeValue<AliasedValue>("owner.domainname").Value.ToString() : null;

                Console.WriteLine($"Default owner will be '{defaultOwner}' ({defaultOwnerId})");

                // prepare config
                var prepareConfigTask = ctx.AddTask("Preparing config", false);
                prepareConfigTask.IsIndeterminate().StartTask();

                var config = new WorkflowConfig
                {
                    DefaultOwner = defaultOwner,
                    Flows = [],
                    Actions = [],
                    BusinessRules = [],
                    SolutionFilter = solutions,
                    PublisherFilter = publishers
                };

                // collect modern flow and legacy workflow configs (both identified by name)
                foreach (var flow in workflows.Where(w => w.Category!.Value == Workflow.Options.Category.ModernFlow || w.Category!.Value == Workflow.Options.Category.Workflow).OrderBy(f => f.Name))
                {
                    var disabled = flow.StateCode?.Value != Workflow.Options.StateCode.Activated;
                    var owner = flow.OwnerId?.Id != defaultOwnerId ? flow.GetAttributeValue<AliasedValue>("owner.domainname").Value.ToString() : null;

                    if (detailed || disabled || owner != null)
                    {
                        config.Flows.TryAdd(flow.Name!, new WorkflowFlowConfig
                        {
                            Disabled = disabled,
                            Owner = owner
                        });
                    }
                }

                // collect action and business process flow configs (both identified by unique name)
                foreach (var action in workflows.Where(w => w.Category!.Value == Workflow.Options.Category.Action || w.Category!.Value == Workflow.Options.Category.BusinessProcessFlow).OrderBy(w => w.UniqueName))
                {
                    var disabled = action.StateCode?.Value != Workflow.Options.StateCode.Activated;
                    var owner = action.OwnerId?.Id != defaultOwnerId ? action.GetAttributeValue<AliasedValue>("owner.domainname").Value.ToString() : null;

                    if (detailed || disabled || owner != null)
                    {
                        config.Actions.TryAdd(action.UniqueName!, new WorkflowBaseConfig
                        {
                            Disabled = disabled,
                            Owner = owner
                        });
                    }
                }

                // collect business rule configs
                var businessRuleTables = workflows.Where(w => w.Category!.Value == Workflow.Options.Category.BusinessRule).GroupBy(b => b.PrimaryEntity).ToList();
                foreach (var table in businessRuleTables.OrderBy(t => t.Key))
                {
                    var tableEntries = new Dictionary<string, WorkflowBaseConfig>();
                    foreach (var rule in table)
                    {
                        var disabled = rule.StateCode?.Value != Workflow.Options.StateCode.Activated;
                        var owner = rule.OwnerId?.Id != defaultOwnerId ? rule.GetAttributeValue<AliasedValue>("owner.domainname").Value.ToString() : null;

                        if (detailed || disabled || owner != null)
                        {
                            tableEntries.TryAdd(rule.Name!, new WorkflowBaseConfig
                            {
                                Disabled = disabled,
                                Owner = owner
                            });
                        }
                    }

                    config.BusinessRules.Add(table.Key!, tableEntries);
                }

                // Write config to file
                Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]Writing config to {configFile}[/]");

                var fileMode = overwrite ? FileMode.Create : FileMode.CreateNew;
                using var fileStream = new FileStream(configFile, fileMode, FileAccess.Write, FileShare.None);
                await JsonSerializer.SerializeAsync(fileStream, config, _jsonSerializerOptions);

                prepareConfigTask.Value = prepareConfigTask.MaxValue;
                prepareConfigTask.StopTask();
            });
    }
}
