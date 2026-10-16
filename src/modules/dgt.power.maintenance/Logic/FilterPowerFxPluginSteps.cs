// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Base.Config;
using Microsoft.Xrm.Sdk;
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

        var service = (IOrganizationService)await connection.GetServiceAsync();
        return Run(service, powerfxpluginConfig);
    }

    private bool Run(IOrganizationService service, List<PowerFxPluginsConfig> powerfxpluginConfig)
    {
        Console.Status()
            .Start("Working on PowerFX Plugin Steps...", ctx =>
            {
                ctx.Spinner(Spinner.Known.Smiley);
                ctx.SpinnerStyle(Style.Parse("green"));

                Console.MarkupLine("Working on PowerFX Plugin Steps...");
                // Do the actual work
                foreach (var config in powerfxpluginConfig)
                {
                    ctx.Status($"Search {config.Name}");
                    var step = SearchPowerFxPluginStep(service, config.Name,config.MessageName);

                    ctx.Status($"Build filter for {config.Name}");
                    var filter = config.FilterAttributes.Count > 0
                        ? string.Join(",", config.FilterAttributes.OrderBy(attribute => attribute, StringComparer.Ordinal))
                        : null;

                    step.FilteringAttributesField = filter;

                    ctx.Status($"Update {config.Name}");
                    service.Update(step);
                    Console.MarkupLine($"Filtered {config.Name} <{config.Message}> | {step.Id}");
                }

                ctx.Status("Finishing");
            });

        return true;
    }

    private static SdkMessageProcessingStep SearchPowerFxPluginStep(IOrganizationService service, string name, string? message)
    {
// Instantiate QueryExpression query
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

        var result = service.RetrieveMultiple(query);

        if (result.Entities.Count != 1)
        {
            throw new ArgumentException("Sdk Message not found or unique");
        }
        return result.Entities.Single().ToEntity<SdkMessageProcessingStep>();
    }
}
