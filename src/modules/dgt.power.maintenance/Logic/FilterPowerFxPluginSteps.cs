// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Base.Config;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public class FilterPowerFxPluginSteps(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<MaintenanceVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(MaintenanceVerb args, CancellationToken cancellationToken)
    {
        // Inline arguments are not yet supported so throw error if supplied
        if (!string.IsNullOrWhiteSpace(args.InlineData))
        {
            throw new NotSupportedException("Inline arguments are not yet supported");
        }

        if (!configResolver.TryGetConfigFile<List<PowerFxPluginsConfig>>(args.Config, out var powerfxpluginConfig))
        {
            return false;
        }

        var service = await connection.GetServiceAsync();
        return await RunAsync(service, powerfxpluginConfig, cancellationToken);
    }

    private async Task<bool> RunAsync(
        IOrganizationServiceAsync2 service,
        List<PowerFxPluginsConfig> powerfxpluginConfig,
        CancellationToken cancellationToken)
    {
        await Console.Status()
            .StartAsync("Working on PowerFX Plugin Steps...", async ctx =>
            {
                ctx.Spinner(Spinner.Known.Smiley);
                ctx.SpinnerStyle(Style.Parse("green"));

                Console.MarkupLine("Working on PowerFX Plugin Steps...");
                // Do the actual work
                foreach (var config in powerfxpluginConfig)
                {
                    ctx.Status($"Search {config.Name}");
                    var step = await SearchPowerFxPluginStepAsync(
                        service,
                        config.Name,
                        config.MessageName,
                        cancellationToken);

                    ctx.Status($"Build filter for {config.Name}");
                    var filter = config.FilterAttributes.Count > 0
                        ? string.Join(",", config.FilterAttributes.OrderBy(attribute => attribute, StringComparer.Ordinal))
                        : null;

                    step.FilteringAttributesField = filter;

                    ctx.Status($"Update {config.Name}");
                    await service.ExecuteAsync(new UpdateRequest { Target = step }, cancellationToken);
                    Console.MarkupLine($"Filtered {config.Name} <{config.Message}> | {step.Id}");
                }

                ctx.Status("Finishing");
            });

        return true;
    }

    private static async Task<SdkMessageProcessingStep> SearchPowerFxPluginStepAsync(
        IOrganizationServiceAsync2 service,
        string name,
        string? message,
        CancellationToken cancellationToken)
    {
        var query = new QueryExpression("sdkmessageprocessingstep")
        {
            NoLock = true,
            TopCount = 2
        };

        query.ColumnSet.AddColumn("name");
        query.Criteria.AddCondition("name", ConditionOperator.Equal, name);
        query.Criteria.AddCondition("sdkmessage", "name", ConditionOperator.Equal, message);
        query.AddLink("fxexpression", "fxexpressionid", "fxexpressionid");
        query.AddLink("sdkmessage", "sdkmessageid", "sdkmessageid");

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);

        if (result.Entities.Count != 1)
        {
            throw new ArgumentException("Sdk Message not found or unique");
        }
        return result.Entities.Single().ToEntity<SdkMessageProcessingStep>();
    }
}
