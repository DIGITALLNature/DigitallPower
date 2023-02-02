using System.Diagnostics;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;
using RoutingRuleItem = dgt.power.dto.RoutingRuleItem;

namespace dgt.power.export.Logic;

public sealed class RoutingRuleConfigExport : BaseExport
{
    public RoutingRuleConfigExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(
        tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(ExportVerb args)
    {
        Tracer.Start(this);


        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "routingruleconfig.json" : args.FileName;


        var export = new RoutingRuleConfigs();

        using var context = new DataContext(Connection);
        context.RoutingRuleSet
            .Select(rr => new RoutingRule
                {
                    RoutingRuleId = rr.RoutingRuleId,
                    StatusCode = rr.StatusCode,
                    StateCode = rr.StateCode
                }
            ).ToList()
            .ForEach(rule =>
                {
                    export.Add(new RoutingRuleConfig
                        {
                            Name = rule.Name!,
                            RoutingRuleId = rule.RoutingRuleId ?? default,
                            Active = rule.StatusCode?.Value == RoutingRule.Options.StatusCode.Active,
                            RoutingRuleItems = GetRuleItems(context, rule)
                        }
                    );
                }
            );

        var json = GetJson(export);

        Tracer.Log($"Export {HandleExportFile(fileDir, fileName, json)} ", TraceEventType.Information);
        return Tracer.End(this, true);
    }

    private List<RoutingRuleItem> GetRuleItems(DataContext context, RoutingRule rule) =>
        context.RoutingRuleItemSet
            .Where(rri => rri.RoutingRuleId != null && rri.RoutingRuleId.Id == rule.RoutingRuleId)
            .Select(rri => new RoutingRuleItem
                {
                    Name = rri.Name!,
                    RoutingRuleItemId = rri.RoutingRuleItemId ?? default,
                    RoutingRuleId = rri.RoutingRuleId!.Id,
                    MsdynRouteto = rri.MsdynRouteto != null ? rri.MsdynRouteto.Value :
                        rri.RoutedQueueId != null ? dataverse.RoutingRuleItem.Options.MsdynRouteto.Queue :
                        dataverse.RoutingRuleItem.Options.MsdynRouteto.User_Team,
                    RoutedQueueId = rri.RoutedQueueId != null ? rri.RoutedQueueId.Id : default(Guid?),
                    AssignObjectIdType =
                        rri.AssignObjectId != null ? rri.AssignObjectId.LogicalName : default,
                    AssignObjectIdName = rri.AssignObjectId != null
                        ? rri.AssignObjectId.LogicalName == Team.EntityLogicalName
                            ? rri.AssignObjectId.Name
                            : GetSystemUserDomainName(Connection, rri.AssignObjectId.Id)
                        : default
                }
            ).ToList();

    private static string? GetSystemUserDomainName(IOrganizationService service, Guid systemuserId)
    {
        using var context = new DataContext(service);
        return context.SystemUserSet.SingleOrDefault(sysU => sysU.Id == systemuserId)?.DomainName;
    }
}
