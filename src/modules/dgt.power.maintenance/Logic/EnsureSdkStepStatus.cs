// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class EnsureSdkStepStatus(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IAnsiConsole console)
    : PowerLogic<EnsureSdkStepStatusSettings>(tracer, connection, configResolver, console)
{
    protected override Task<bool> InvokeAsync(EnsureSdkStepStatusSettings args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentException.ThrowIfNullOrWhiteSpace(args.Solution);

        return InvokeCoreAsync(args, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(EnsureSdkStepStatusSettings args, CancellationToken cancellationToken)
    {
        var sdkSteps = await Console.Status()
            .StartAsync("Loading sdk steps", async _ => await RetrieveSdkStepsAsync(args.Solution!));

        var table = new Table()
            .AddColumn("Plugin Type")
            .AddColumn("Step Name")
            .AddColumn("Status")
            .AddColumn("Step Id");

        await Console.Live(table)
            .StartAsync(async ctx =>
            {
                var orderedSdkSteps = sdkSteps
                    .OrderBy(s => s.PluginTypeId?.Id)
                    .ThenBy(s => s.Name)
                    .ToArray();

                var rowCount = 0;
                await Parallel.ForEachAsync(orderedSdkSteps, cancellationToken, async (sdkStep, _) =>
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
                        _ => "[red]Unknown[/]"
                    };

                    table.AddRow(pluginType, stepName, status, stepId);
                    var rowIndex = rowCount++;
                    ctx.Refresh();

                    var desiredStateValue = args.Disabled
                        ? SdkMessageProcessingStep.Options.StateCode.Disabled
                        : SdkMessageProcessingStep.Options.StateCode.Enabled;

                    if (!args.DryRun && sdkStep.StateCode?.Value != desiredStateValue)
                    {
                        var updateRequest = new UpdateRequest
                        {
                            Target = new SdkMessageProcessingStep(sdkStep.Id)
                            {
                                StateCode = new OptionSetValue(desiredStateValue)
                            }
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

    private async Task<SdkMessageProcessingStep[]> RetrieveSdkStepsAsync(string solutionName)
    {
        var query = new QueryExpression(SdkMessageProcessingStep.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(
                SdkMessageProcessingStep.LogicalNames.StateCode,
                SdkMessageProcessingStep.LogicalNames.Name,
                SdkMessageProcessingStep.LogicalNames.PluginTypeId
            )
        };

        var componentLink = query.AddLink(SolutionComponent.EntityLogicalName, SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepId, SolutionComponent.LogicalNames.ObjectId);
        var solutionLink = componentLink.AddLink(Solution.EntityLogicalName, SolutionComponent.LogicalNames.SolutionId, Solution.LogicalNames.SolutionId);
        solutionLink.EntityAlias = "solution";

        query.Criteria.AddCondition("solution", Solution.LogicalNames.UniqueName, ConditionOperator.Like, solutionName);

        var retrieveMultipleRequest = new RetrieveMultipleRequest
        {
            Query = query
        };
        var orgResponse = await ((IOrganizationServiceAsync2)Connection).ExecuteAsync(retrieveMultipleRequest);

        var sdkSteps = (orgResponse as RetrieveMultipleResponse)?.EntityCollection.Entities.Cast<SdkMessageProcessingStep>().ToArray() ?? [];

        return sdkSteps;
    }
}
