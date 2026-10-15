// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
using Spectre.Console;
using Queue = dgt.power.dataverse.Queue;
using RoutingRuleItem = dgt.power.dataverse.RoutingRuleItem;

namespace dgt.power.import.Logic;

public sealed class RoutingRuleConfigImport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<ImportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ImportVerb args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "routingruleconfig.json" : args.FileName;


        if (!configResolver.TryGetConfigFile<List<RoutingRuleConfig>>(args.FileDir, fileName, out var rules))
        {
            console.MarkupLine("[red]Routing rule configuration not found.[/]");
            return false;
        }

        //anything to do?
        if (rules.Count == 0)
        {
            console.MarkupLine("[red]Routing rule configuration is empty.[/]");
            return false;
        }

        var result = true;

        var service = (IOrganizationService)await connection.GetServiceAsync();
        using var context = new DataContext(service);

        foreach (var rule in rules)
        {
            result = HandleRoutingRule(service, args, context, rule) && result;
        }

        return result;
    }

    private bool HandleRoutingRule(IOrganizationService service, ImportVerb args, DataContext context, RoutingRuleConfig rule)
    {
        var ruleInTarget = context.RoutingRuleSet.SingleOrDefault(rr => rr.Id == rule.RoutingRuleId);

        console.WriteLine($"Check Routing Rule {rule.Name}({rule.RoutingRuleId:D})");

        var isDeactivatedToUpdate = false;

        if (ruleInTarget == null)
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]Routing Rule {rule.Name}({rule.RoutingRuleId:D}) does not exist[/]");
            return false;
        }

        var result = true;
        var ruleItemsInTarget = context.RoutingRuleItemSet
            .Where(rri =>
                rri.RoutingRuleId != null && rri.RoutingRuleId.Id == rule.RoutingRuleId)
            .Select(rri => new RoutingRuleItem
            {
                RoutingRuleItemId = rri.RoutingRuleItemId,
                RoutingRuleId = rri.RoutingRuleId,
                MsdynRouteto = rri.MsdynRouteto,
                RoutedQueueId = rri.RoutedQueueId,
                AssignObjectIdType = rri.AssignObjectIdType,
                AssignObjectId = rri.AssignObjectId
            });

        var owner = ImportAssignee.GetAssignee(args.Assignee, rule);

        if (!string.IsNullOrWhiteSpace(owner))
        {
            var systemUser =
                context.SystemUserSet.SingleOrDefault(sysU => sysU.Id == ruleInTarget.OwnerId!.Id);

            if (systemUser?.DomainName != owner)
            {
                result = AssignRule(service, ruleInTarget, owner, ref isDeactivatedToUpdate) && result;
            }
        }

        foreach (var item in rule.RoutingRuleItems)
        {
            result = HandleRoutingRuleItem(service, context, rule, ruleItemsInTarget, item, ruleInTarget,
                ref isDeactivatedToUpdate) && result;
        }


        if ((isDeactivatedToUpdate && rule.Active) ||
            (rule.Active && ruleInTarget.StatusCode?.Value != RoutingRule.Options.StatusCode.Active))
        {
            console.WriteLine($"Activate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})");

            result &= service.TrySetState(ruleInTarget.ToEntityReference(), RoutingRule.Options.StateCode.Active,
                RoutingRule.Options.StatusCode.Active);
        }
        else if (!isDeactivatedToUpdate && !rule.Active &&
                 ruleInTarget.StatusCode?.Value == RoutingRule.Options.StatusCode.Active)
        {
            console.WriteLine($"Deactivate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})");

            result &= service.TrySetState(ruleInTarget.ToEntityReference(), RoutingRule.Options.StateCode.Draft,
                RoutingRule.Options.StatusCode.Draft);
        }

        return result;
    }

    private bool HandleRoutingRuleItem(IOrganizationService service, DataContext context, RoutingRuleConfig rule,
        IQueryable<RoutingRuleItem> ruleItemsInTarget, common.DTO.RoutingRuleItem item, RoutingRule ruleInTarget,
        ref bool isDeactivatedToUpdate)
    {
        var ruleItemInTarget =
            ruleItemsInTarget.SingleOrDefault(riit => riit.RoutingRuleItemId == item.RoutingRuleItemId);

        if (ruleItemInTarget == null)
        {
            console.MarkupLineInterpolated(
                CultureInfo.InvariantCulture,
                $"[yellow]Routing Rule  {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D}) does not exist[/]");
            return false;
        }

        var result = true;
        if (item.MsdynRouteto == RoutingRuleItem.Options.MsdynRouteto.Queue)
        {
            if (item.RoutedQueueId != ruleItemInTarget.RoutedQueueId?.Id ||
                item.MsdynRouteto != ruleItemInTarget.MsdynRouteto?.Value)
            {
                result = UpdateRoutingRuleItem(service, new RoutingRuleItem(item.RoutingRuleItemId)
                    {
                        RoutedQueueId = new EntityReference(Queue.EntityLogicalName,
                            item.RoutedQueueId ?? Guid.Empty),
                        MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.Queue),
                        AssignObjectId = null
                    }, ruleInTarget,
                    ref isDeactivatedToUpdate);
            }
        }
        else
        {
            if (item.AssignObjectIdType == SystemUser.EntityLogicalName)
            {
                var systemuser = context.SystemUserSet.SingleOrDefault(sysU =>
                    sysU.DomainName != null && sysU.DomainName == item.AssignObjectIdName);

                if (systemuser == null)
                {
                    console.MarkupLineInterpolated(
                        CultureInfo.InvariantCulture,
                        $"[yellow]Routing Rule {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D}) for User {item.AssignObjectIdName} does not exist[/]");
                    return result;
                }

                if (systemuser.Id != ruleItemInTarget.AssignObjectId?.Id ||
                    item.MsdynRouteto != ruleItemInTarget.MsdynRouteto?.Value)
                {
                    result = UpdateRoutingRuleItem(service, new RoutingRuleItem(item.RoutingRuleItemId)
                        {
                            AssignObjectId = systemuser.ToEntityReference(),
                            MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.User_Team),
                            RoutedQueueId = null
                        }, ruleInTarget,
                        ref isDeactivatedToUpdate);
                }
            }
            else if (item.AssignObjectIdType == Team.EntityLogicalName)
            {
                var team = context.TeamSet.SingleOrDefault(t =>
                    t.Name != null && t.Name == item.AssignObjectIdName);

                if (team == null)
                {
                    console.MarkupLineInterpolated(
                        CultureInfo.InvariantCulture,
                        $"[yellow]Routing Rule {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D}) for Team {item.AssignObjectIdName} does not exist[/]");
                    return result;
                }

                if (team.Id != ruleItemInTarget.AssignObjectId?.Id ||
                    item.MsdynRouteto != ruleItemInTarget.MsdynRouteto?.Value)
                {
                    result = UpdateRoutingRuleItem(service, new RoutingRuleItem(item.RoutingRuleItemId)
                        {
                            AssignObjectId = team.ToEntityReference(),
                            MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.User_Team),
                            RoutedQueueId = null
                        }, ruleInTarget,
                        ref isDeactivatedToUpdate);
                }
            }
        }

        return result;
    }

    private bool UpdateRoutingRuleItem(IOrganizationService service, RoutingRuleItem item, RoutingRule rule,
        ref bool isDeactivatedToUpdate)
    {
        var result = true;

        if (rule.StatusCode?.Value == RoutingRule.Options.StatusCode.Active && !isDeactivatedToUpdate)
        {
            console.WriteLine($"Deactivate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})");

            result &= service.TrySetState(rule.ToEntityReference(), RoutingRule.Options.StateCode.Draft,
                RoutingRule.Options.StatusCode.Draft);

            isDeactivatedToUpdate = true;
        }

        console.WriteLine(
            $"Update: Routing Rule {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D})");

        result &= service.TryUpdate(item);

        return result;
    }

    private bool AssignRule(IOrganizationService service, RoutingRule rule, string owner, ref bool isDeactivatedToUpdate)
    {
        var result = true;

        SystemUser? ownerSystemUser;

        using (var context = new DataContext(service))
        {
            ownerSystemUser =
                context.SystemUserSet.SingleOrDefault(sysU => sysU.DomainName != null && sysU.DomainName == owner);
        }

        if (ownerSystemUser == null)
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]Cannot find Assignee: {owner}[/]");
            return true;
        }

        if (rule.StatusCode?.Value == RoutingRule.Options.StatusCode.Active)
        {
            console.WriteLine($"Deactivate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})");

            result &= service.TrySetState(rule.ToEntityReference(), RoutingRule.Options.StateCode.Draft,
                RoutingRule.Options.StatusCode.Draft);

            isDeactivatedToUpdate = true;
        }

        console.WriteLine($"Assign: {rule.Name}({rule.RoutingRuleId:D}) to {ownerSystemUser.DomainName}");
        result &= service.TryAssign(rule.ToEntityReference(), ownerSystemUser);
        return result;
    }
}
