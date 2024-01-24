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
        }

        protected override bool Invoke(Settings args) => InvokeAsync(args).GetAwaiter().GetResult();

        private async Task<bool> InvokeAsync(Settings args)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(args.Solution);

            await AnsiConsole.Status()
                .StartAsync("Loading sdk steps", async ctx =>
                {
                    var sdkSteps = await RetrieveSdkSteps(args.Solution);

                    var table = new Table()
                        .AddColumn("Plugin Type")
                        .AddColumn("Step Name")
                        .AddColumn("Status")
                        .AddColumn("Step Id");

                    foreach (var sdkStep in sdkSteps.OrderBy(s => s.PluginTypeId?.Id).ThenBy(s => s.Name))
                    {
                        var pluginType = sdkStep.PluginTypeId?.Id.ToString() ?? string.Empty;
                        var stepName = sdkStep.Name.EscapeMarkup();
                        var stepId = sdkStep.Id.ToString();

                        var status = sdkStep.StateCode?.Value switch
                        {
                            SdkMessageProcessingStep.Options.StateCode.Enabled => "[green]Enabled[/]",
                            SdkMessageProcessingStep.Options.StateCode.Disabled => "[grey]Disabled[/]",
                            _ => "[red]Unknown[/]",
                        };

                        table.AddRow(pluginType, stepName, status, stepId);
                    }

                    AnsiConsole.Write(table);
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
