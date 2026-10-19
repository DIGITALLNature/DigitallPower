// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using Calendar = dgt.power.dataverse.Calendar;

namespace dgt.power.import.Logic;

public class SlaConfigImport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<ImportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ImportVerb args, CancellationToken cancellationToken)
    {
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "slaconfig.json" : args.FileName;


        if (!configResolver.TryGetConfigFile<List<SlaConfig>>(args.FileDir, fileName, out var slaConfigs))
        {
            Console.MarkupLine("[red]SLA configuration not found.[/]");
            return false;
        }

        //anything to do?
        if (slaConfigs.Count == 0)
        {
            Console.MarkupLine("[red]SLA configuration is empty.[/]");
            return false;
        }

        var result = true;

        var service = await connection.GetServiceAsync();
        foreach (var sla in slaConfigs)
        {
            var slaQuery = new QueryExpression(SLA.EntityLogicalName)
            {
                NoLock = true,
                ColumnSet = new ColumnSet(SLA.LogicalNames.SLAId, SLA.LogicalNames.Name, SLA.LogicalNames.BusinessHoursId,
                    SLA.LogicalNames.StatusCode, SLA.LogicalNames.OwnerId),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(SLA.LogicalNames.SLAId, ConditionOperator.Equal, sla.SlaId)
                    }
                }
            };
            var slaCrm = (await service.RetrieveAllAsync(slaQuery, cancellationToken: cancellationToken))
                .SingleOrDefault()?.ToEntity<SLA>();

            Console.WriteLine($"Check Sla {sla.Name}({sla.SlaId:D})");

            if (slaCrm == null)
            {
                Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]Sla {sla.Name}({sla.SlaId:D}) is not in the system --> skip[/]");
                continue;
            }

            var owner = ImportAssignee.GetAssignee(args.Assignee, sla);
            SystemUser? systemuser = null;
            if (!string.IsNullOrWhiteSpace(owner))
            {
                var userQuery = new QueryExpression(SystemUser.EntityLogicalName)
                {
                    NoLock = true,
                    ColumnSet = new ColumnSet(SystemUser.LogicalNames.SystemUserId, SystemUser.LogicalNames.DomainName),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(SystemUser.LogicalNames.DomainName, ConditionOperator.Equal, owner)
                        }
                    }
                };
                systemuser = (await service.RetrieveAllAsync(userQuery, cancellationToken: cancellationToken))
                    .SingleOrDefault()?.ToEntity<SystemUser>();
            }

            if (RequiresUpdate(sla, slaCrm, systemuser))
            {
                Console.WriteLine($"Update Sla {sla.Name}({sla.SlaId:D})");

                if (slaCrm.StatusCode?.Value != SLA.Options.StatusCode.Draft)
                {
                    result = await service.TryUpdateAsync(new SLA(slaCrm.Id)
                    {
                        StateCode = new OptionSetValue(SLA.Options.StateCode.Draft),
                        StatusCode = new OptionSetValue(SLA.Options.StatusCode.Draft)
                    }, cancellationToken) && result;
                }

                var slatoUpdate = new SLA
                {
                    Id = sla.SlaId ?? Guid.Empty,
                    BusinessHoursId = sla.BusinessHours == null ? null : new EntityReference(Calendar.EntityLogicalName, sla.BusinessHours ?? Guid.Empty)
                };

                if (systemuser != null)
                {
                    slatoUpdate.OwnerId = systemuser.ToEntityReference();
                }

                result = await service.TryUpdateAsync(slatoUpdate, cancellationToken) && result;

                if (sla.Active == true)
                {
                    result = await service.TryUpdateAsync(new SLA(slaCrm.Id)
                    {
                        StateCode = new OptionSetValue(SLA.Options.StateCode.Active),
                        StatusCode = new OptionSetValue(SLA.Options.StatusCode.Active)
                    }, cancellationToken) && result;
                }
            }
        }

        return result;
    }

    private static bool RequiresUpdate(SlaConfig sla, SLA slaCrm, SystemUser? systemuser)
    {
        if (sla.BusinessHours != slaCrm.BusinessHoursId?.Id)
        {
            return true;
        }

        if (sla.Active == true && slaCrm.StatusCode?.Value == SLA.Options.StatusCode.Draft)
        {
            return true;
        }

        if (sla.Active == false && slaCrm.StatusCode?.Value == SLA.Options.StatusCode.Active)
        {
            return true;
        }

        return systemuser != null && systemuser.Id != slaCrm.OwnerId?.Id;
    }
}
