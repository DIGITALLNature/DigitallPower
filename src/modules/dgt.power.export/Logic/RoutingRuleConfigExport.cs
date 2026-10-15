// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;
using Spectre.Console;
using RoutingRuleItem = dgt.power.common.DTO.RoutingRuleItem;

namespace dgt.power.export.Logic;

public sealed class RoutingRuleConfigExport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IFileService fileService,
    IAnsiConsole console)
    : PowerCommand<ExportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync([NotNull] ExportVerb args, CancellationToken cancellationToken)
    {
        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "routingruleconfig.json" : args.FileName;


        var export = new List<RoutingRuleConfig>();

        var service = (IOrganizationService)await connection.GetServiceAsync();
        using var context = new DataContext(service);
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
                            RoutingRuleId = rule.RoutingRuleId ?? Guid.Empty,
                            Active = rule.StatusCode?.Value == RoutingRule.Options.StatusCode.Active,
                            RoutingRuleItems = GetRuleItems(context, service, rule)
                        }
                    );
                }
            );

        var json = ExportJson.GetJson(export);

        Console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");
        return true;
    }

    private static List<RoutingRuleItem> GetRuleItems(DataContext context, IOrganizationService service, RoutingRule rule) =>
        context.RoutingRuleItemSet
            .Where(rri => rri.RoutingRuleId != null && rri.RoutingRuleId.Id == rule.RoutingRuleId)
            .AsEnumerable()
            .Select(rri => new RoutingRuleItem
                {
                    Name = rri.Name!,
                    RoutingRuleItemId = rri.RoutingRuleItemId ?? Guid.Empty,
                    RoutingRuleId = rri.RoutingRuleId!.Id,
                    MsdynRouteto = ResolveRouteTarget(rri),
                    RoutedQueueId = rri.RoutedQueueId?.Id,
                    AssignObjectIdType = rri.AssignObjectId?.LogicalName,
                    AssignObjectIdName = ResolveAssignedObjectName(service, rri.AssignObjectId)
                }
            ).ToList();

    private static int ResolveRouteTarget(dataverse.RoutingRuleItem ruleItem)
    {
        if (ruleItem.MsdynRouteto != null)
        {
            return ruleItem.MsdynRouteto.Value;
        }

        return ruleItem.RoutedQueueId != null
            ? dataverse.RoutingRuleItem.Options.MsdynRouteto.Queue
            : dataverse.RoutingRuleItem.Options.MsdynRouteto.User_Team;
    }

    private static string? ResolveAssignedObjectName(IOrganizationService service, EntityReference? assignObjectId)
    {
        if (assignObjectId == null)
        {
            return null;
        }

        if (assignObjectId.LogicalName == Team.EntityLogicalName)
        {
            return assignObjectId.Name;
        }

        return GetSystemUserDomainName(service, assignObjectId.Id);
    }

    private static string? GetSystemUserDomainName(IOrganizationService service, Guid systemuserId)
    {
        using var context = new DataContext(service);
        return context.SystemUserSet.SingleOrDefault(sysU => sysU.Id == systemuserId)?.DomainName;
    }
}
