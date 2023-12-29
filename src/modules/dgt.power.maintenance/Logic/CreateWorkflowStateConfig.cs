// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
                var loadModernFlowTask = ctx.AddTask("Loading modern flows").IsIndeterminate();
                var loadActionTask = ctx.AddTask("Loading actions", false).IsIndeterminate();
                var loadBusinessRulesTask = ctx.AddTask("Loading direct business rules", false).IsIndeterminate();
                var loadIndirectBusinessRulesTask = ctx.AddTask("Loading indirect business rules", false).IsIndeterminate();
                var prepareConfigTask = ctx.AddTask("Preparing config", false).IsIndeterminate();

                // Load flows (modern, classic)
                var flows = await LoadWorkflows(solutions, publishers, Workflow.Options.Category.ModernFlow, Workflow.Options.Category.Workflow_);
                loadModernFlowTask.Value = loadModernFlowTask.MaxValue;
                loadModernFlowTask.StopTask();

                // Load actions
                loadActionTask.StartTask();
                var actions = await LoadWorkflows(solutions, publishers, Workflow.Options.Category.Action, Workflow.Options.Category.BusinessProcessFlow);
                loadActionTask.Value = loadActionTask.MaxValue;
                loadActionTask.StopTask();

                // Load direct business rules
                loadBusinessRulesTask.StartTask();
                var businessRules = await LoadWorkflows(solutions, publishers, Workflow.Options.Category.BusinessRule);
                loadBusinessRulesTask.Value = loadBusinessRulesTask.MaxValue;
                loadBusinessRulesTask.StopTask();

                // Load indirect business rules
                loadIndirectBusinessRulesTask.StartTask();
                var tables = await LoadFullTables(solutions, publishers);
                var tableNames = tables.Distinct().Select(async t => await ResolveTableName(t)).ToList();
                await Task.WhenAll(tableNames);

                var indirectBusinessRules = await LoadIndirectBusinessRules(tableNames.Select(t => t.Result).ToArray());
                businessRules.AddRange(indirectBusinessRules);

                loadIndirectBusinessRulesTask.Value = loadIndirectBusinessRulesTask.MaxValue;
                loadIndirectBusinessRulesTask.StopTask();

                // Group all workflows by owner and select max occurence as default owner
                var groupedFlows = flows
                    .Concat(actions)
                    .Concat(businessRules)
                    .GroupBy(f => f.OwnerId!.Id).ToList();
                var defaultOwnerId = groupedFlows.OrderByDescending(g => g.Count()).FirstOrDefault()?.Key;
                var defaultOwner = defaultOwnerId != null ? await ResolveSystemUserAsync(defaultOwnerId.Value) : null;

                // Prepare config
                prepareConfigTask.StartTask();

                var config = new WorkflowConfig
                {
                    DefaultOwner = defaultOwner,
                    Flows = [],
                    Actions = [],
                    BusinessRules = [],
                    SolutionFilter = solutions,
                    PublisherFilter = publishers,
                };

                // Collect modern flow config
                foreach (var flow in flows.OrderBy(f => f.Name))
                {
                    var disabled = flow.StateCode?.Value != Workflow.Options.StateCode.Activated;
                    var owner = flow.OwnerId?.Id != defaultOwnerId ? await ResolveSystemUserAsync(flow.OwnerId!.Id) : null;

                    if (disabled || owner != default)
                    {
                        config.Flows.Add(flow.Name!, new WorkflowConfig.FlowConfig
                        {
                            Disabled = disabled,
                            Owner = owner,
                        });
                    }
                }

                // Collect action config
                foreach (var action in actions.OrderBy(w => w.UniqueName))
                {
                    var disabled = action.StateCode?.Value != Workflow.Options.StateCode.Activated;
                    var owner = action.OwnerId?.Id != defaultOwnerId ? await ResolveSystemUserAsync(action.OwnerId!.Id) : null;

                    if (disabled || owner != default)
                    {
                        config.Actions.Add(action.UniqueName!, new WorkflowConfig.BaseWorkflowConfig
                        {
                            Disabled = disabled,
                            Owner = owner,
                        });
                    }
                }

                // Collect business rule config
                var businessRuleTables = businessRules.GroupBy(b => b.PrimaryEntity).ToList();
                foreach (var table in businessRuleTables.OrderBy(t => t.Key))
                {
                    var rules = table
                        .OrderBy(r => r.Name)
                        .ToDictionary(r => r.Name!, r => new WorkflowConfig.BaseWorkflowConfig
                        {
                            Disabled = r.StateCode?.Value != Workflow.Options.StateCode.Activated,
                            Owner = r.OwnerId?.Id != defaultOwnerId ? ResolveSystemUserAsync(r.OwnerId!.Id).Result : null,
                        });

                    config.BusinessRules.Add(table.Key!, rules);
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
            Tracer.Log($"[orange3]Flow owner '{systemUser.FullName}' ({userId}) has no domain name![/]", TraceEventType.Warning);
        }

        _userTable.TryAdd(userId, domainname!);
        return domainname;
    }

    private async Task<string> ResolveTableName(Guid tableId)
    {
        var orgServiceAsync = Connection as IOrganizationServiceAsync;

        var metadataRequest = new RetrieveEntityRequest
        {
            RetrieveAsIfPublished = true,
            MetadataId = tableId,
            EntityFilters = EntityFilters.Entity
        };
        var metadataResponse = await orgServiceAsync!.ExecuteAsync(metadataRequest);

        return ((RetrieveEntityResponse)metadataResponse).EntityMetadata.LogicalName;
    }

    private Task<List<Workflow>> LoadWorkflows(string[]? solutions, string[]? publishers, params int[] category)
    {
        // Prepare query expression to load workflows
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
        filter.AddCondition(Workflow.LogicalNames.Category, ConditionOperator.In, category.Select(c => (object)c).ToArray());

        // If either solution or publisher filter exist add them
        if (solutions?.Length > 0 || publishers?.Length > 0)
        {
            // Prepare solution link and filter (this is needed for solutions and publisher)
            var linkFilter = new FilterExpression(LogicalOperator.Or);
            filter.AddFilter(linkFilter);

            var componentLink = query.AddLink(SolutionComponent.EntityLogicalName, Workflow.LogicalNames.WorkflowId, SolutionComponent.LogicalNames.ObjectId);
            var solutionLink = componentLink.AddLink(Solution.EntityLogicalName, SolutionComponent.LogicalNames.SolutionId, Solution.LogicalNames.SolutionId);
            solutionLink.EntityAlias = "solution";

            // Add solution filters if any exist
            if (solutions?.Length > 0)
            {
                foreach (var solution in solutions)
                {
                    Tracer.Log($"[grey] {Emoji.Known.RightArrow} Adding solution condition '{solution.EscapeMarkup()}'[/]", TraceEventType.Verbose);
                    // Add condition to link filter with unique comparison which allows fetch xml patterns (%)
                    linkFilter.AddCondition("solution", Solution.LogicalNames.UniqueName, ConditionOperator.Like, solution);
                }
            }

            // Add publisher filters if any exist
            if (publishers?.Length > 0)
            {
                var publisherLink = solutionLink.AddLink(Publisher.EntityLogicalName, Solution.LogicalNames.PublisherId, Publisher.LogicalNames.PublisherId, JoinOperator.LeftOuter);
                publisherLink.EntityAlias = "publisher";

                foreach (var publisher in publishers)
                {
                    Tracer.Log($"[grey] {Emoji.Known.RightArrow} Adding publisher name condition '{publisher.EscapeMarkup()}'[/]", TraceEventType.Verbose);
                    linkFilter.AddCondition("publisher", Publisher.LogicalNames.UniqueName, ConditionOperator.Like, publisher);
                }
            }
        }

        var workflows = Connection.RetrieveMultiple(query);
        var categories = string.Join(",", category.Order());
        Tracer.Log($"Found {workflows.Entities.Count} workflows for categories {categories}", TraceEventType.Verbose);

        // Check if we have reached the limit of query expressions
        // Assumption is that there is only a slim chance we ever hit that limit so pagination is not worth it here
        if (workflows.Entities.Count == 5000)
        {
            Tracer.Log("Found workflows that correspond to the max limit of query expressions. Possibly not all modern flows were found.", TraceEventType.Warning);
        }

        return Task.FromResult(workflows.Entities.Cast<Workflow>().ToList());
    }

    private Task<List<Workflow>> LoadIndirectBusinessRules(string[] tables)
    {
        // Prepare query expression to load workflows
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
        filter.AddCondition(Workflow.LogicalNames.Category, ConditionOperator.Equal, Workflow.Options.Category.BusinessRule);
        filter.AddCondition(Workflow.LogicalNames.PrimaryEntity, ConditionOperator.In, tables.Select(t => (object)t).ToArray());

        var workflows = Connection.RetrieveMultiple(query);
        Tracer.Log($"Found {workflows.Entities.Count} indirect business rules", TraceEventType.Verbose);

        // Check if we have reached the limit of query expressions
        // Assumption is that there is only a slim chance we ever hit that limit so pagination is not worth it here
        if (workflows.Entities.Count == 5000)
        {
            Tracer.Log("Found workflows that correspond to the max limit of query expressions. Possibly not all indirect business rules were found.", TraceEventType.Warning);
        }

        return Task.FromResult(workflows.Entities.Cast<Workflow>().ToList());
    }

    /// <summary>
    /// Load and return workflows that are included by any of the filters
    /// Comparisons are done with the fetch xml like filter so wildcards (%) are possible to be used
    /// The result includes any workflow that is in any solution with a given name or publisher regardless whether the solution is unmanged
    /// or the workflow is included in multiple (e.g. temp solutions) where only one fits
    /// </summary>
    /// <param name="category">Category of the workflows to load</param>"
    /// <param name="solutions">List of uniquenames for solutions to consider</param>
    /// <param name="publishers">List of publishers of solutions to consider</param>
    /// <returns></returns>
    private Task<List<Guid>> LoadFullTables(string[]? solutions, string[]? publishers, params int[] category)
    {
        var query = new QueryExpression(SolutionComponent.EntityLogicalName);
        query.ColumnSet.AddColumns(SolutionComponent.LogicalNames.ObjectId);

        var filter = new FilterExpression();
        query.Criteria.AddFilter(filter);
        filter.AddCondition(SolutionComponent.LogicalNames.ComponentType, ConditionOperator.Equal, SolutionComponent.Options.ComponentType.Entity);
        filter.AddCondition(SolutionComponent.LogicalNames.RootComponentBehavior, ConditionOperator.Equal, SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents);

        // If either solution or publisher filter exist add them
        if (solutions?.Length > 0 || publishers?.Length > 0)
        {
            // Prepare solution filter (this is needed for solutions and publisher)
            var solutionFilter = new FilterExpression(LogicalOperator.Or);
            filter.AddFilter(solutionFilter);

            var solutionLink = query.AddLink(Solution.EntityLogicalName, SolutionComponent.LogicalNames.SolutionId, Solution.LogicalNames.SolutionId);
            solutionLink.EntityAlias = "solution";

            // Add solution filters if any exist
            if (solutions?.Length > 0)
            {
                foreach (var solution in solutions)
                {
                    Tracer.Log($"[grey] {Emoji.Known.RightArrow} Adding solution condition '{solution.EscapeMarkup()}'[/]", TraceEventType.Verbose);
                    // Add condition to link filter with unique comparison which allows fetch xml patterns (%)
                    solutionFilter.AddCondition("solution", Solution.LogicalNames.UniqueName, ConditionOperator.Like, solution);
                }
            }

            // Add publisher filters if any exist
            if (publishers?.Length > 0)
            {
                var publisherLink = solutionLink.AddLink(Publisher.EntityLogicalName, Solution.LogicalNames.PublisherId, Publisher.LogicalNames.PublisherId, JoinOperator.LeftOuter);
                publisherLink.EntityAlias = "publisher";

                foreach (var publisher in publishers)
                {
                    Tracer.Log($"[grey] {Emoji.Known.RightArrow} Adding publisher name condition '{publisher.EscapeMarkup()}'[/]", TraceEventType.Verbose);
                    solutionFilter.AddCondition("publisher", Publisher.LogicalNames.UniqueName, ConditionOperator.Like, publisher);
                }
            }
        }

        var solutionComponents = Connection.RetrieveMultiple(query);
        Tracer.Log($"Found {solutionComponents.Entities.Count} tables with all components included", TraceEventType.Verbose);

        // Check if we have reached the limit of query expressions
        // Assumption is that there is only a slim chance we ever hit that limit so pagination is not worth it here
        if (solutionComponents.Entities.Count == 5000)
        {
            Tracer.Log("Found workflows that correspond to the max limit of query expressions. Possibly not all tables were found.", TraceEventType.Warning);
        }

        return Task.FromResult(solutionComponents.Entities.Cast<SolutionComponent>().Select(t => t.ObjectId!.Value).ToList());
    }
}
