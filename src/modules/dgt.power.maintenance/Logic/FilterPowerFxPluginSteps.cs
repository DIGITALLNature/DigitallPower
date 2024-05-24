// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Base.Config;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public class FilterPowerFxPluginSteps : BaseMaintenance
{
    public FilterPowerFxPluginSteps(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) :
        base(tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(MaintenanceVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);

        // Inline arguments are not yet supported so throw error if supplied
        if (!string.IsNullOrWhiteSpace(args.InlineData))
        {
            throw new NotSupportedException("Inline arguments are not yet supported");
        }

        if (!ConfigResolver.TryGetConfigFile<PowerFxPluginsConfigs>(args.Config, out var powerfxpluginConfig))
        {
            return Tracer.End(this, false);
        }

        AnsiConsole.Status()
            .Start("Working on PowerFX Plugin Steps...", ctx =>
            {
                ctx.Spinner(Spinner.Known.Smiley);
                ctx.SpinnerStyle(Style.Parse("green"));

                AnsiConsole.MarkupLine("Working on PowerFX Plugin Steps...");
                // Do the actual work
                foreach (var config in powerfxpluginConfig)
                {
                    ctx.Status($"Search {config.Name}");
                    var step = SearchPowerFxPluginStep(config.Name,config.MessageName);

                    ctx.Status($"Build filter for {config.Name}");
                    var filter = config.FilterAttributes.Any()
                        ? string.Join(",", config.FilterAttributes.Order()): null;

                    step.FilteringAttributesField = filter;

                    ctx.Status($"Update {config.Name}");
                    Connection.Update(step);
                    AnsiConsole.MarkupLine($"Filtered {config.Name} <{config.Message}> | {step.Id}");
                }

                ctx.Status("Finishing");
            });


        return Tracer.End(this, true);
    }


    private SdkMessageProcessingStep SearchPowerFxPluginStep(string name, string message)
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

        var result = Connection.RetrieveMultiple(query);

        if (result.Entities.Count != 1)
        {
            throw new ArgumentException("Sdk Message not found or unique");
        }
        return result.Entities.Single().ToEntity<SdkMessageProcessingStep>();
    }
}
