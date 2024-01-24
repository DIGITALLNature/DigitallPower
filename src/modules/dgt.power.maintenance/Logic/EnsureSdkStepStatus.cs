// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.common;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.Logic
{
    public class EnsureSdkStepStatus(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver)
        : PowerLogic<EnsureSdkStepStatus.Settings>(tracer, connection, configResolver)
    {
        public class Settings : BaseProgramSettings
        {
            [CommandOption("-s|--solution")]
            [Description("unique name of the solution to work on")]
            [DefaultValue("assemblies")]
            public string? Solution { get; set; }

            [CommandOption("-d|--disabled")]
            [Description("true if steps should be disabled, false otherwise")]
            [DefaultValue(false)]
            public bool Disabled { get; set; }
        }

        protected override bool Invoke(Settings args) => InvokeAsync(args).GetAwaiter().GetResult();

        private async Task<bool> InvokeAsync(Settings args)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(args.Solution);

            var sdkSteps = await AnsiConsole.Status()
                .StartAsync("Loading sdk steps", async _ => await RetrieveSdkSteps(args.Solution));

            var table = new Table()
                .AddColumn("Plugin Type")
                .AddColumn("Step Name")
                .AddColumn("Status")
                .AddColumn("Step Id");

            await AnsiConsole.Live(table)
                .StartAsync(async ctx =>
                {
                    var orderedSdkSteps = sdkSteps
                        .OrderBy(s => s.PluginTypeId?.Id)
                        .ThenBy(s => s.Name)
                        .ToArray();

                    var rowCount = 0;
                    await Parallel.ForEachAsync(orderedSdkSteps, async (sdkStep, _) =>
                    {
                        var pluginType = sdkStep.PluginTypeId?.Id.ToString() ?? string.Empty;
                        var stepName = sdkStep.Name.EscapeMarkup();
                        var stepId = sdkStep.Id.ToString();

                        var status = (sdkStep.StateCode?.Value, args.Disabled) switch
                        {
                            (SdkMessageProcessingStep.Options.StateCode.Enabled, false) => "[green]Enabled[/]",
                            (SdkMessageProcessingStep.Options.StateCode.Enabled, true) => "[red]Enabled[/]",
                            (SdkMessageProcessingStep.Options.StateCode.Disabled, false) => "[red]Disabled[/]",
                            (SdkMessageProcessingStep.Options.StateCode.Disabled, true) => "[grey]Disabled[/]",
                            _ => "[red]Unknown[/]",
                        };

                        table.AddRow(pluginType, stepName, status, stepId);
                        var rowIndex = rowCount++;
                        ctx.Refresh();

                        var desiredStateValue = args.Disabled
                            ? SdkMessageProcessingStep.Options.StateCode.Disabled
                            : SdkMessageProcessingStep.Options.StateCode.Enabled;

                        if (sdkStep.StateCode?.Value != desiredStateValue)
                        {
                            var updateRequest = new UpdateRequest
                            {
                                Target = new SdkMessageProcessingStep(sdkStep.Id)
                                {
                                    StateCode = new OptionSetValue(desiredStateValue),
                                },
                            };
                            await ((IOrganizationServiceAsync2)Connection).ExecuteAsync(updateRequest);

                            status = desiredStateValue == SdkMessageProcessingStep.Options.StateCode.Disabled
                                ? "[grey]Disabled[/]"
                                : "[green]Enabled[/]";

                            table.UpdateCell(rowIndex, 2, status);
                            ctx.Refresh();
                        }
                    });
                });

            return true;
        }

        private async Task<SdkMessageProcessingStep[]> RetrieveSdkSteps(string solutionName)
        {
            var query = new QueryExpression(SdkMessageProcessingStep.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(
                    SdkMessageProcessingStep.LogicalNames.StateCode,
                    SdkMessageProcessingStep.LogicalNames.Name,
                    SdkMessageProcessingStep.LogicalNames.PluginTypeId
                ),
            };

            var componentLink = query.AddLink(SolutionComponent.EntityLogicalName, SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepId, SolutionComponent.LogicalNames.ObjectId);
            var solutionLink = componentLink.AddLink(Solution.EntityLogicalName, SolutionComponent.LogicalNames.SolutionId, Solution.LogicalNames.SolutionId);
            solutionLink.EntityAlias = "solution";

            query.Criteria.AddCondition("solution", Solution.LogicalNames.UniqueName, ConditionOperator.Like, solutionName);

            var retrieveMultipleRequest = new RetrieveMultipleRequest
            {
                Query = query,
            };
            var orgResponse = await ((IOrganizationServiceAsync2)Connection).ExecuteAsync(retrieveMultipleRequest);

            var sdkSteps = (orgResponse as RetrieveMultipleResponse)?.EntityCollection.Entities.Cast<SdkMessageProcessingStep>().ToArray() ?? [];

            return sdkSteps;
        }
    }
}
