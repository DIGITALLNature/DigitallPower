// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
using Queue = dgt.power.dataverse.Queue;
using RoutingRuleItem = dgt.power.dataverse.RoutingRuleItem;

namespace dgt.power.import.Logic;

public sealed class RoutingRuleConfigImport : BaseImport
{
    public RoutingRuleConfigImport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(
        tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(ImportVerb args)
    {
        Tracer.Start(this);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "routingruleconfig.json" : args.FileName;


        if (!ConfigResolver.GetConfigFile<RoutingRuleConfigs>(args.FileDir, fileName, out var rules))
        {
            return Tracer.NotConfigured(this);
        }

        //anything to do?
        if (!rules.Any())
        {
            return Tracer.NotConfigured(this);
        }

        var result = true;

        using var context = new DataContext(Connection);

        foreach (var rule in rules)
        {
            result = HandleRoutingRule(args, context, rule, result);
        }

        return Tracer.End(this, result);
    }

    private bool HandleRoutingRule(ImportVerb args, DataContext context, RoutingRuleConfig rule, bool result)
    {
        var ruleInTarget = context.RoutingRuleSet.SingleOrDefault(rr => rr.Id == rule.RoutingRuleId);

        Tracer.Log($"Check Routing Rule {rule.Name}({rule.RoutingRuleId:D})", TraceEventType.Information);

        var isDeactivatedToUpdate = false;

        if (ruleInTarget == null)
        {
            Tracer.Log($"Routing Rule {rule.Name}({rule.RoutingRuleId:D}) does not exist", TraceEventType.Warning);
            return result & false;
        }

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

        var owner = GetAssignee(args.Assignee, rule);

        if (!string.IsNullOrWhiteSpace(owner))
        {
            var systemUser =
                context.SystemUserSet.SingleOrDefault(sysU => sysU.Id == ruleInTarget.OwnerId!.Id);

            if (systemUser?.DomainName != owner)
            {
                result &= AssignRule(ruleInTarget, owner, ref isDeactivatedToUpdate);
            }
        }

        foreach (var item in rule.RoutingRuleItems)
        {
            result = HandleRoutingRuleItem(context, rule, result, ruleItemsInTarget, item, ruleInTarget,
                ref isDeactivatedToUpdate);
        }


        if ((isDeactivatedToUpdate && rule.Active) ||
            (rule.Active && ruleInTarget.StatusCode?.Value != RoutingRule.Options.StatusCode.Active))
        {
            Tracer.Log($"Activate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})", TraceEventType.Information);

            result &= Connection.TrySetState(ruleInTarget.ToEntityReference(), RoutingRule.Options.StateCode.Active,
                RoutingRule.Options.StatusCode.Active);
        }
        else if (!isDeactivatedToUpdate && !rule.Active &&
                 ruleInTarget.StatusCode?.Value == RoutingRule.Options.StatusCode.Active)
        {
            Tracer.Log($"Deactivate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})", TraceEventType.Information);

            result &= Connection.TrySetState(ruleInTarget.ToEntityReference(), RoutingRule.Options.StateCode.Draft,
                RoutingRule.Options.StatusCode.Draft);
        }

        return result;
    }

    private bool HandleRoutingRuleItem(DataContext context, RoutingRuleConfig rule, bool result,
        IQueryable<RoutingRuleItem> ruleItemsInTarget, dto.RoutingRuleItem item, RoutingRule ruleInTarget,
        ref bool isDeactivatedToUpdate)
    {
        var ruleItemInTarget =
            ruleItemsInTarget.SingleOrDefault(riit => riit.RoutingRuleItemId == item.RoutingRuleItemId);

        if (ruleItemInTarget == null)
        {
            Tracer.Log(
                $"Routing Rule  {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D}) does not exist",
                TraceEventType.Warning);
            return false & result;
        }

        if (item.MsdynRouteto == RoutingRuleItem.Options.MsdynRouteto.Queue)
        {
            if (item.RoutedQueueId != ruleItemInTarget.RoutedQueueId?.Id ||
                item.MsdynRouteto != ruleItemInTarget.MsdynRouteto?.Value)
            {
                result &= UpdateRoutingRuleItem(new RoutingRuleItem(item.RoutingRuleItemId)
                    {
                        RoutedQueueId = new EntityReference(Queue.EntityLogicalName,
                            item.RoutedQueueId ?? default),
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
                    Tracer.Log(
                        $"Routing Rule {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D}) for User {item.AssignObjectIdName} does not exist",
                        TraceEventType.Warning);
                    return result;
                }

                if (systemuser.Id != ruleItemInTarget.AssignObjectId?.Id ||
                    item.MsdynRouteto != ruleItemInTarget.MsdynRouteto?.Value)
                {
                    result &= UpdateRoutingRuleItem(new RoutingRuleItem(item.RoutingRuleItemId)
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
                    Tracer.Log(
                        $"Routing Rule {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D}) for Team {item.AssignObjectIdName} does not exist",
                        TraceEventType.Warning);
                    return result;
                }

                if (team.Id != ruleItemInTarget.AssignObjectId?.Id ||
                    item.MsdynRouteto != ruleItemInTarget.MsdynRouteto?.Value)
                {
                    result &= UpdateRoutingRuleItem(new RoutingRuleItem(item.RoutingRuleItemId)
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

    private bool UpdateRoutingRuleItem(RoutingRuleItem item, RoutingRule rule,
        ref bool isDeactivatedToUpdate)
    {
        var result = true;

        if (rule.StatusCode?.Value == RoutingRule.Options.StatusCode.Active && !isDeactivatedToUpdate)
        {
            Tracer.Log($"Deactivate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})", TraceEventType.Information);

            result &= Connection.TrySetState(rule.ToEntityReference(), RoutingRule.Options.StateCode.Draft,
                RoutingRule.Options.StatusCode.Draft);

            isDeactivatedToUpdate = true;
        }

        Tracer.Log(
            $"Update: Routing Rule {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D})",
            TraceEventType.Information);

        result &= Connection.TryUpdate(item);

        return result;
    }

    private bool AssignRule(RoutingRule rule, string owner, ref bool isDeactivatedToUpdate)
    {
        var result = true;

        SystemUser? ownerSystemUser;

        using (var context = new DataContext(Connection))
        {
            ownerSystemUser =
                context.SystemUserSet.SingleOrDefault(sysU => sysU.DomainName != null && sysU.DomainName == owner);
        }

        if (ownerSystemUser == null)
        {
            Tracer.Log($"Cannot find Assignee: {owner}", TraceEventType.Warning);
            return true;
        }

        if (rule.StatusCode?.Value == RoutingRule.Options.StatusCode.Active)
        {
            Tracer.Log($"Deactivate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})",
                TraceEventType.Information);

            result &= Connection.TrySetState(rule.ToEntityReference(), RoutingRule.Options.StateCode.Draft,
                RoutingRule.Options.StatusCode.Draft);

            isDeactivatedToUpdate = true;
        }

        Tracer.Log($"Assign: {rule.Name}({rule.RoutingRuleId:D}) to {ownerSystemUser.DomainName}",
            TraceEventType.Information);
        result &= Connection.TryAssign(rule.ToEntityReference(), ownerSystemUser);
        return result;
    }
}
