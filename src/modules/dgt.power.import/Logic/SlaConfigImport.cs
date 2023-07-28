// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
using Calendar = dgt.power.dataverse.Calendar;

namespace dgt.power.import.Logic;

public class SlaConfigImport : BaseImport
{
    public SlaConfigImport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(ImportVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "slaconfig.json" : args.FileName;


        if (!ConfigResolver.TryGetConfigFile<SlaConfigs>(args.FileDir, fileName, out var slaConfigs))
        {
            return Tracer.NotConfigured(this);
        }

        //anything to do?
        if (!slaConfigs.Any())
        {
            return Tracer.NotConfigured(this);
        }

        var result = true;

        using var context = new DataContext(Connection);
        foreach (var sla in slaConfigs)
        {
            var slaCrm = context.SLASet.SingleOrDefault(s => s.Id == sla.SlaId);

            Tracer.Log($"Check Sla {sla.Name}({sla.SlaId:D})", TraceEventType.Information);

            if (slaCrm == null)
            {
                Tracer.Log($"Sla {sla.Name}({sla.SlaId:D}) is not in the system --> skip", TraceEventType.Warning);
                continue;
            }

            var owner = GetAssignee(args.Assignee, sla);
            var systemuser = default(SystemUser);
            if (!string.IsNullOrWhiteSpace(owner))
            {
                systemuser = context.SystemUserSet.SingleOrDefault(s => s.DomainName != null && s.DomainName == owner);
            }

            if (sla.BusinessHours != slaCrm.BusinessHoursId?.Id ||
                (sla.Active == true && slaCrm.StatusCode?.Value == SLA.Options.StatusCode.Draft) ||
                (sla.Active == false && slaCrm.StatusCode?.Value == SLA.Options.StatusCode.Active) ||
                (systemuser != default(SystemUser) && systemuser.Id != slaCrm.OwnerId?.Id))
            {
                Tracer.Log($"Update Sla {sla.Name}({sla.SlaId:D})", TraceEventType.Information);

                if (slaCrm.StatusCode?.Value != SLA.Options.StatusCode.Draft)
                {
                    result &= Connection.TrySetState(slaCrm.ToEntityReference(), SLA.Options.StateCode.Draft, SLA.Options.StatusCode.Draft);
                }

                var slatoUpdate = new SLA
                {
                    Id = sla.SlaId ?? Guid.Empty,
                    BusinessHoursId = sla.BusinessHours == default ? null : new EntityReference(Calendar.EntityLogicalName, sla.BusinessHours ?? Guid.Empty)
                };

                if (systemuser != default(SystemUser))
                {
                    slatoUpdate.OwnerId = systemuser.ToEntityReference();
                }

                result &= Connection.TryUpdate(slatoUpdate);

                if (sla.Active == true)
                {
                    result &= Connection.TrySetState(slaCrm.ToEntityReference(), SLA.Options.StateCode.Active, SLA.Options.StatusCode.Active);
                }
            }
        }

        return Tracer.End(this, result);
    }
}
