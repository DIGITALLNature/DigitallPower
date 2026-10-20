// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
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
    protected override async Task<bool> RunAsync(ExportVerb args, CancellationToken cancellationToken)
    {
        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "routingruleconfig.json" : args.FileName;


        var export = new List<RoutingRuleConfig>();

        var service = await connection.GetServiceAsync();
        var rulesQuery = new QueryExpression(RoutingRule.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(RoutingRule.LogicalNames.RoutingRuleId, RoutingRule.LogicalNames.Name,
                RoutingRule.LogicalNames.StatusCode, RoutingRule.LogicalNames.StateCode)
        };
        var rules = await service.RetrieveAllAsync(rulesQuery, cancellationToken: cancellationToken);
        var userNames = new Dictionary<Guid, string?>();
        foreach (var entity in rules)
        {
            var rule = entity.ToEntity<RoutingRule>();
            var ruleId = rule.RoutingRuleId ?? Guid.Empty;
            export.Add(new RoutingRuleConfig
            {
                Name = rule.Name!,
                RoutingRuleId = ruleId,
                Active = rule.StatusCode?.Value == RoutingRule.Options.StatusCode.Active,
                RoutingRuleItems = await GetRuleItemsAsync(service, ruleId, userNames, cancellationToken)
            });
        }

        var json = ExportJson.GetJson(export);

        Console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");
        return true;
    }

    private static async Task<List<RoutingRuleItem>> GetRuleItemsAsync(IOrganizationServiceAsync2 service, Guid routingRuleId,
        Dictionary<Guid, string?> userNames, CancellationToken cancellationToken)
    {
        var query = new QueryExpression(dataverse.RoutingRuleItem.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                dataverse.RoutingRuleItem.LogicalNames.Name,
                dataverse.RoutingRuleItem.LogicalNames.RoutingRuleItemId,
                dataverse.RoutingRuleItem.LogicalNames.RoutingRuleId,
                "msdyn_routeto",
                dataverse.RoutingRuleItem.LogicalNames.RoutedQueueId,
                dataverse.RoutingRuleItem.LogicalNames.AssignObjectId),
            Criteria =
            {
                Conditions =
                {
                    new ConditionExpression(dataverse.RoutingRuleItem.LogicalNames.RoutingRuleId, ConditionOperator.Equal, routingRuleId)
                }
            }
        };
        var entities = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        var items = new List<RoutingRuleItem>(entities.Count);
        foreach (var entity in entities)
        {
            var ruleItem = entity.ToEntity<dataverse.RoutingRuleItem>();
            items.Add(new RoutingRuleItem
            {
                Name = ruleItem.Name!,
                RoutingRuleItemId = ruleItem.RoutingRuleItemId ?? Guid.Empty,
                RoutingRuleId = ruleItem.RoutingRuleId!.Id,
                MsdynRouteto = ResolveRouteTarget(ruleItem),
                RoutedQueueId = ruleItem.RoutedQueueId?.Id,
                AssignObjectIdType = ruleItem.AssignObjectId?.LogicalName,
                AssignObjectIdName = await ResolveAssignedObjectNameAsync(service, ruleItem.AssignObjectId, userNames, cancellationToken)
            });
        }

        return items;
    }

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

    private static async Task<string?> ResolveAssignedObjectNameAsync(IOrganizationServiceAsync2 service, EntityReference? assignObjectId,
        Dictionary<Guid, string?> userNames, CancellationToken cancellationToken)
    {
        if (assignObjectId == null)
        {
            return null;
        }

        if (assignObjectId.LogicalName == Team.EntityLogicalName)
        {
            return assignObjectId.Name;
        }

        if (!userNames.TryGetValue(assignObjectId.Id, out var domainName))
        {
            var user = await service.RetrieveAsync(SystemUser.EntityLogicalName, assignObjectId.Id,
                new ColumnSet(SystemUser.LogicalNames.DomainName), cancellationToken);
            domainName = user.GetAttributeValue<string>(SystemUser.LogicalNames.DomainName);
            userNames.Add(assignObjectId.Id, domainName);
        }

        return domainName;
    }
}
