// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Base.Config;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
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
        _jsonSerializerOptions = jsonSerializerOptions;
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
            .StartAsync(async ctx => {
                var loadModernFlowTask = ctx.AddTask("Loading modern flows").IsIndeterminate();
                var prepareConfigTask = ctx.AddTask("Preparing config", false);

                // Load modern flows
                var flows = await LoadModernFlowsAsync(solutions, publishers);
                loadModernFlowTask.Value = loadModernFlowTask.MaxValue;
                loadModernFlowTask.StopTask();

                // Group flows by owner and select max occurence as default owner
                var groupedFlows = flows.GroupBy(f => f.OwnerId!.Id).ToList();
                var defaultOwnerId = groupedFlows.OrderByDescending(g => g.Count()).FirstOrDefault()?.Key;
                var defaultOwner = defaultOwnerId != null ? await ResolveSystemUserAsync(defaultOwnerId.Value) : null;

                // Prepare config
                prepareConfigTask.MaxValue = groupedFlows.Count + 1;
                prepareConfigTask.StartTask();

                var config = new WorkflowConfig{
                    DefaultOwner = defaultOwner,
                    Flows = [],
                    SolutionFilter = solutions,
                    PublisherFilter = publishers,
                };
                foreach(var flow in flows)
                {
                    var disabled = flow.StateCode?.Value != Workflow.Options.StateCode.Activated;
                    var owner = flow.OwnerId?.Id != defaultOwnerId ? await ResolveSystemUserAsync(flow.OwnerId!.Id) : null;

                    if (disabled || owner != default)
                    {
                        config.Flows.Add(flow.Name!, new WorkflowConfig.FlowConfig{
                            Disabled = disabled,
                            Owner = owner,
                        });
                    }

                    prepareConfigTask.Increment(1);
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
            return null;
        }

        _userTable.TryAdd(userId, domainname!);
        return domainname;
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
    private Task<List<Workflow>> LoadModernFlowsAsync(string[]? solutions, string[]? publishers)
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
