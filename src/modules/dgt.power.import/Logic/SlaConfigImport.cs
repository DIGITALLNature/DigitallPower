// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
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
    protected override async Task<bool> RunAsync([NotNull] ImportVerb args, CancellationToken cancellationToken)
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

        var service = (IOrganizationService)await connection.GetServiceAsync();
        using var context = new DataContext(service);
        foreach (var sla in slaConfigs)
        {
            var slaCrm = context.SLASet.SingleOrDefault(s => s.Id == sla.SlaId);

            Console.WriteLine($"Check Sla {sla.Name}({sla.SlaId:D})");

            if (slaCrm == null)
            {
                Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]Sla {sla.Name}({sla.SlaId:D}) is not in the system --> skip[/]");
                continue;
            }

            var owner = ImportAssignee.GetAssignee(args.Assignee, sla);
            var systemuser = default(SystemUser);
            if (!string.IsNullOrWhiteSpace(owner))
            {
                systemuser = context.SystemUserSet.SingleOrDefault(s => s.DomainName != null && s.DomainName == owner);
            }

            if (RequiresUpdate(sla, slaCrm, systemuser))
            {
                Console.WriteLine($"Update Sla {sla.Name}({sla.SlaId:D})");

                if (slaCrm.StatusCode?.Value != SLA.Options.StatusCode.Draft)
                {
                    result &= service.TrySetState(slaCrm.ToEntityReference(), SLA.Options.StateCode.Draft, SLA.Options.StatusCode.Draft);
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

                result &= service.TryUpdate(slatoUpdate);

                if (sla.Active == true)
                {
                    result &= service.TrySetState(slaCrm.ToEntityReference(), SLA.Options.StateCode.Active, SLA.Options.StatusCode.Active);
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
