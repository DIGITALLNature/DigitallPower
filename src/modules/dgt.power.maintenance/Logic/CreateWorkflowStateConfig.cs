// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Base.Config;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.Logic;

public class CreateWorkflowStateConfig : PowerLogic<CreateWorkflowStateConfig.Settings>
{
    public class Settings : BaseProgramSettings
    {
        [CommandOption("-o|--output")]
        [Description("Full path to the output file, e.g. C:\\temp\\config.json")]
        [DefaultValue("config.json")]
        public required string Config { get; init; }

        [CommandOption("--overwrite")]
        [Description("If set to true the output file will be overwritten if it exists")]
        [DefaultValue(false)]
        public bool Overwrite { get; init; }

        [CommandOption("-s|--solutions")]
        [Description("Comma separated list of solution uniquenames to consider. Wildcards (%) are allowed")]
        public string? Solutions { get; set; }

        [CommandOption("-p|--publishers")]
        [Description("Comma separated list of publisher names to consider. Wildcards (%) are allowed")]
        public string? Publishers { get; set; }
    }

    private readonly Dictionary<Guid, string> _userTable;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public CreateWorkflowStateConfig(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, JsonSerializerOptions jsonSerializerOptions) : base(tracer, connection, configResolver)
    {
        _userTable = new Dictionary<Guid, string>();
        _jsonSerializerOptions = new JsonSerializerOptions(jsonSerializerOptions) { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
    }

    protected override bool Invoke(Settings args)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(args.Config, nameof(args.Config));
        Tracer.Start(this);

        // Check if output file exists and if so abort if overwrite is not set
        if (File.Exists(args.Config) && !args.Overwrite)
        {
            throw new InvalidOperationException($"Output file '{args.Config}' already exists. Use --overwrite flag to overwrite the existng file");
        }

        // Ensure at least one filter option is set
        if (string.IsNullOrWhiteSpace(args.Solutions) && string.IsNullOrWhiteSpace(args.Publishers))
        {
            throw new InvalidOperationException("At least one of the filter options --solutions or --publishers must be set");
        }

        var solutions = args.Solutions?.Split(',');
        var publishers = args.Publishers?.Split(',');

        // Do the actual work
        var task = CollectWorkflowStates(solutions, publishers, args.Config);
        task.Wait();

        return Tracer.End(this, true);
    }

    private async Task CollectWorkflowStates(string[]? solutions, string[]? publishers, string configFile)
    {
        await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var prepareConfigTask = ctx.AddTask("Preparing config", false);

                Dictionary<string, ProgressTask> progressTasks = new();
                WorkflowStateManager.TaskStatusCallback progress = (taskName, message, current, max) =>
                {
                    if (!progressTasks.TryGetValue(taskName, out var progressTask))
                    {
                        progressTask = ctx.AddTask($"{taskName.EscapeMarkup()} [grey]({message.EscapeMarkup()})[/]");
                        progressTasks.Add(taskName, progressTask);
                    }

                    progressTask.Description($"{taskName.EscapeMarkup()} [grey]({message.EscapeMarkup()})[/]");
                    progressTask.Value(current);
                    progressTask.IsIndeterminate(max > 0);
                    progressTask.MaxValue(max > 0 ? max.Value : 100);

                    ctx.Refresh();
                };

                var workflowStateManager = new WorkflowStateManager((IOrganizationServiceAsync2)Connection, solutions ?? [], publishers ?? [], Tracer, progress);

                var workflows = await workflowStateManager.LoadAllWorkflows();

                // group all workflows by owner and select max occurence as default owner
                var groupedFlows = workflows.GroupBy(f => f.OwnerId!.Id).ToList();
                var defaultOwnerId = groupedFlows.OrderByDescending(g => g.Count()).FirstOrDefault()?.Key;
                var defaultOwner = defaultOwnerId != null ? await ResolveSystemUserAsync(defaultOwnerId.Value) : null;

                // prepare config
                prepareConfigTask.IsIndeterminate().StartTask();

                var config = new WorkflowConfig
                {
                    DefaultOwner = defaultOwner,
                    Flows = [],
                    Actions = [],
                    BusinessRules = [],
                    SolutionFilter = solutions,
                    PublisherFilter = publishers,
                };

                // collect modern flow and legacy workflow configs (both identified by name)
                foreach (var flow in workflows.Where(w => w.Category!.Value == Workflow.Options.Category.ModernFlow || w.Category!.Value == Workflow.Options.Category.Workflow_).OrderBy(f => f.Name))
                {
                    var disabled = flow.StateCode?.Value != Workflow.Options.StateCode.Activated;
                    var owner = flow.OwnerId?.Id != defaultOwnerId ? await ResolveSystemUserAsync(flow.OwnerId!.Id) : null;

                    if (disabled || owner != default)
                    {
                        config.Flows.TryAdd(flow.Name!, new WorkflowConfig.FlowConfig
                        {
                            Disabled = disabled,
                            Owner = owner,
                        });
                    }
                }

                // collect action and business process flow configs (both identified by unique name)
                foreach (var action in workflows.Where(w => w.Category!.Value == Workflow.Options.Category.Action || w.Category!.Value == Workflow.Options.Category.BusinessProcessFlow).OrderBy(w => w.UniqueName))
                {
                    var disabled = action.StateCode?.Value != Workflow.Options.StateCode.Activated;
                    var owner = action.OwnerId?.Id != defaultOwnerId ? await ResolveSystemUserAsync(action.OwnerId!.Id) : null;

                    if (disabled || owner != default)
                    {
                        config.Actions.TryAdd(action.UniqueName!, new WorkflowConfig.BaseWorkflowConfig
                        {
                            Disabled = disabled,
                            Owner = owner,
                        });
                    }
                }

                // collect business rule configs
                var businessRuleTables = workflows.Where(w => w.Category!.Value == Workflow.Options.Category.BusinessRule).GroupBy(b => b.PrimaryEntity).ToList();
                foreach (var table in businessRuleTables.OrderBy(t => t.Key))
                {
                    var tableEntries = new Dictionary<string, WorkflowConfig.BaseWorkflowConfig>();
                    foreach (var rule in table)
                    {
                        var disabled = rule.StateCode?.Value != Workflow.Options.StateCode.Activated;
                        var owner = rule.OwnerId?.Id != defaultOwnerId ? await ResolveSystemUserAsync(rule.OwnerId!.Id) : null;

                        if (disabled || owner != default)
                        {
                            tableEntries.TryAdd(rule.Name!, new WorkflowConfig.BaseWorkflowConfig
                            {
                                Disabled = disabled,
                                Owner = owner,
                            });
                        }
                    }

                    config.BusinessRules.Add(table.Key!, tableEntries);
                }

                // Write config to file
                AnsiConsole.MarkupLine($"Writing config to [blue]{configFile.EscapeMarkup()}[/]");

                using var fileStream = new FileStream(configFile, FileMode.Create, FileAccess.Write, FileShare.None);
                await JsonSerializer.SerializeAsync(fileStream, config, _jsonSerializerOptions);

                prepareConfigTask.Value = prepareConfigTask.MaxValue;
                prepareConfigTask.StopTask();
            });
    }

    /// <summary>
    /// Resolve a given user id to a domain name
    /// </summary>
    /// <param name="userId">User id to resolve</param>
    /// <returns>Domain name of the user</returns>
    /// <remarks>
    /// This method caches the results in a dictionary to avoid multiple calls to the server
    /// </remarks>
    private async Task<string?> ResolveSystemUserAsync(Guid userId)
    {
        // Check if we seen the user already and if so grab it from the table
        if (_userTable.TryGetValue(userId, out var domainname))
        {
            return domainname;
        }

        // Retrieve user by id
        var orgServiceAsync = Connection as IOrganizationServiceAsync;
        var user = await orgServiceAsync!.RetrieveAsync(SystemUser.EntityLogicalName, userId, new ColumnSet(SystemUser.LogicalNames.DomainName, SystemUser.LogicalNames.FullName));
        var systemUser = user.ToEntity<SystemUser>();
        domainname = user.ToEntity<SystemUser>().DomainName;

        if (string.IsNullOrWhiteSpace(domainname))
        {
            Tracer.Log($"Flow owner '{systemUser.FullName}' ({userId}) has no domain name!", TraceEventType.Warning);
        }

        _userTable.TryAdd(userId, domainname!);
        return domainname;
    }
}
