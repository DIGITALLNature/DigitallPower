// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
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
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "routingruleconfig.json" : args.FileName;


        if (!configResolver.TryGetConfigFile<List<RoutingRuleConfig>>(args.FileDir, fileName, out var rules))
        {
            Console.MarkupLine("[red]Routing rule configuration not found.[/]");
            return false;
        }

        //anything to do?
        if (rules.Count == 0)
        {
            Console.MarkupLine("[red]Routing rule configuration is empty.[/]");
            return false;
        }

        var result = true;

        var service = await connection.GetServiceAsync();

        foreach (var rule in rules)
        {
            result = await HandleRoutingRuleAsync(service, args, rule, cancellationToken) && result;
        }

        return result;
    }

    private async Task<bool> HandleRoutingRuleAsync(IOrganizationServiceAsync2 service, ImportVerb args, RoutingRuleConfig rule,
        CancellationToken cancellationToken)
    {
        var ruleQuery = new QueryExpression(RoutingRule.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(RoutingRule.LogicalNames.RoutingRuleId, RoutingRule.LogicalNames.Name,
                RoutingRule.LogicalNames.StatusCode, RoutingRule.LogicalNames.StateCode, RoutingRule.LogicalNames.OwnerId),
            Criteria =
            {
                Conditions =
                {
                    new ConditionExpression(RoutingRule.LogicalNames.RoutingRuleId, ConditionOperator.Equal, rule.RoutingRuleId)
                }
            }
        };
        var ruleInTarget = (await service.RetrieveAllAsync(ruleQuery, cancellationToken: cancellationToken))
            .SingleOrDefault()?.ToEntity<RoutingRule>();

        Console.WriteLine($"Check Routing Rule {rule.Name}({rule.RoutingRuleId:D})");

        var isDeactivatedToUpdate = false;

        if (ruleInTarget == null)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]Routing Rule {rule.Name}({rule.RoutingRuleId:D}) does not exist[/]");
            return false;
        }

        var result = true;
        var itemQuery = new QueryExpression(RoutingRuleItem.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                RoutingRuleItem.LogicalNames.RoutingRuleItemId,
                RoutingRuleItem.LogicalNames.RoutingRuleId,
                RoutingRuleItem.LogicalNames.RoutedQueueId,
                RoutingRuleItem.LogicalNames.AssignObjectId,
                "msdyn_routeto"),
            Criteria =
            {
                Conditions =
                {
                    new ConditionExpression(RoutingRuleItem.LogicalNames.RoutingRuleId, ConditionOperator.Equal, rule.RoutingRuleId)
                }
            }
        };
        var ruleItemsInTarget = (await service.RetrieveAllAsync(itemQuery, cancellationToken: cancellationToken))
            .Select(entity => entity.ToEntity<RoutingRuleItem>()).ToList();

        var owner = ImportAssignee.GetAssignee(args.Assignee, rule);

        if (!string.IsNullOrWhiteSpace(owner))
        {
            var ownerQuery = new QueryExpression(SystemUser.EntityLogicalName)
            {
                NoLock = true,
                ColumnSet = new ColumnSet(SystemUser.LogicalNames.SystemUserId, SystemUser.LogicalNames.DomainName),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(SystemUser.LogicalNames.SystemUserId, ConditionOperator.Equal,
                            ruleInTarget.OwnerId!.Id)
                    }
                }
            };
            var systemUser = (await service.RetrieveAllAsync(ownerQuery, cancellationToken: cancellationToken))
                .SingleOrDefault()?.ToEntity<SystemUser>();

            if (systemUser?.DomainName != owner)
            {
                var (assignResult, deactivated) = await AssignRuleAsync(service, ruleInTarget, owner, isDeactivatedToUpdate,
                    cancellationToken);
                result = assignResult && result;
                isDeactivatedToUpdate = deactivated;
            }
        }

        foreach (var item in rule.RoutingRuleItems)
        {
            var (itemResult, deactivated) = await HandleRoutingRuleItemAsync(service, rule, ruleItemsInTarget, item,
                ruleInTarget, isDeactivatedToUpdate, cancellationToken);
            result = itemResult && result;
            isDeactivatedToUpdate = deactivated;
        }


        if ((isDeactivatedToUpdate && rule.Active) ||
            (rule.Active && ruleInTarget.StatusCode?.Value != RoutingRule.Options.StatusCode.Active))
        {
            Console.WriteLine($"Activate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})");

            result = await SetRoutingRuleStateAsync(service, ruleInTarget, RoutingRule.Options.StateCode.Active,
                RoutingRule.Options.StatusCode.Active, cancellationToken) && result;
        }
        else if (!isDeactivatedToUpdate && !rule.Active &&
                 ruleInTarget.StatusCode?.Value == RoutingRule.Options.StatusCode.Active)
        {
            Console.WriteLine($"Deactivate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})");

            result = await SetRoutingRuleStateAsync(service, ruleInTarget, RoutingRule.Options.StateCode.Draft,
                RoutingRule.Options.StatusCode.Draft, cancellationToken) && result;
        }

        return result;
    }

    private async Task<(bool Result, bool IsDeactivatedToUpdate)> HandleRoutingRuleItemAsync(
        IOrganizationServiceAsync2 service, RoutingRuleConfig rule, List<RoutingRuleItem> ruleItemsInTarget,
        common.DTO.RoutingRuleItem item, RoutingRule ruleInTarget, bool isDeactivatedToUpdate,
        CancellationToken cancellationToken)
    {
        var ruleItemInTarget = ruleItemsInTarget.SingleOrDefault(riit => riit.Id == item.RoutingRuleItemId);

        if (ruleItemInTarget == null)
        {
            Console.MarkupLineInterpolated(
                CultureInfo.InvariantCulture,
                $"[yellow]Routing Rule  {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D}) does not exist[/]");
            return (false, isDeactivatedToUpdate);
        }

        if (item.MsdynRouteto == RoutingRuleItem.Options.MsdynRouteto.Queue)
        {
            if (item.RoutedQueueId != ruleItemInTarget.RoutedQueueId?.Id ||
                item.MsdynRouteto != ruleItemInTarget.MsdynRouteto?.Value)
            {
                return await UpdateRoutingRuleItemAsync(service, new RoutingRuleItem(item.RoutingRuleItemId)
                {
                    RoutedQueueId = new EntityReference(Queue.EntityLogicalName, item.RoutedQueueId ?? Guid.Empty),
                    MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.Queue),
                    AssignObjectId = null
                }, ruleInTarget, isDeactivatedToUpdate, cancellationToken);
            }
        }
        else
        {
            if (item.AssignObjectIdType == SystemUser.EntityLogicalName)
            {
                var userQuery = new QueryExpression(SystemUser.EntityLogicalName)
                {
                    NoLock = true,
                    ColumnSet = new ColumnSet(SystemUser.LogicalNames.SystemUserId, SystemUser.LogicalNames.DomainName),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(SystemUser.LogicalNames.DomainName, ConditionOperator.Equal, item.AssignObjectIdName)
                        }
                    }
                };
                var systemuser = (await service.RetrieveAllAsync(userQuery, cancellationToken: cancellationToken))
                    .SingleOrDefault()?.ToEntity<SystemUser>();

                if (systemuser == null)
                {
                    Console.MarkupLineInterpolated(
                        CultureInfo.InvariantCulture,
                        $"[yellow]Routing Rule {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D}) for User {item.AssignObjectIdName} does not exist[/]");
                    return (true, isDeactivatedToUpdate);
                }

                if (systemuser.Id != ruleItemInTarget.AssignObjectId?.Id ||
                    item.MsdynRouteto != ruleItemInTarget.MsdynRouteto?.Value)
                {
                    return await UpdateRoutingRuleItemAsync(service, new RoutingRuleItem(item.RoutingRuleItemId)
                    {
                        AssignObjectId = systemuser.ToEntityReference(),
                        MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.User_Team),
                        RoutedQueueId = null
                    }, ruleInTarget, isDeactivatedToUpdate, cancellationToken);
                }
            }
            else if (item.AssignObjectIdType == Team.EntityLogicalName)
            {
                var teamQuery = new QueryExpression(Team.EntityLogicalName)
                {
                    NoLock = true,
                    ColumnSet = new ColumnSet(Team.LogicalNames.TeamId, Team.LogicalNames.Name),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(Team.LogicalNames.Name, ConditionOperator.Equal, item.AssignObjectIdName)
                        }
                    }
                };
                var team = (await service.RetrieveAllAsync(teamQuery, cancellationToken: cancellationToken))
                    .SingleOrDefault()?.ToEntity<Team>();

                if (team == null)
                {
                    Console.MarkupLineInterpolated(
                        CultureInfo.InvariantCulture,
                        $"[yellow]Routing Rule {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D}) for Team {item.AssignObjectIdName} does not exist[/]");
                    return (true, isDeactivatedToUpdate);
                }

                if (team.Id != ruleItemInTarget.AssignObjectId?.Id ||
                    item.MsdynRouteto != ruleItemInTarget.MsdynRouteto?.Value)
                {
                    return await UpdateRoutingRuleItemAsync(service, new RoutingRuleItem(item.RoutingRuleItemId)
                    {
                        AssignObjectId = team.ToEntityReference(),
                        MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.User_Team),
                        RoutedQueueId = null
                    }, ruleInTarget, isDeactivatedToUpdate, cancellationToken);
                }
            }
        }

        return (true, isDeactivatedToUpdate);
    }

    private async Task<(bool Result, bool IsDeactivatedToUpdate)> UpdateRoutingRuleItemAsync(
        IOrganizationServiceAsync2 service, RoutingRuleItem item, RoutingRule rule, bool isDeactivatedToUpdate,
        CancellationToken cancellationToken)
    {
        var result = true;

        if (rule.StatusCode?.Value == RoutingRule.Options.StatusCode.Active && !isDeactivatedToUpdate)
        {
            Console.WriteLine($"Deactivate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})");

            result = await SetRoutingRuleStateAsync(service, rule, RoutingRule.Options.StateCode.Draft,
                RoutingRule.Options.StatusCode.Draft, cancellationToken) && result;

            isDeactivatedToUpdate = true;
        }

        Console.WriteLine(
            $"Update: Routing Rule {rule.Name}({rule.RoutingRuleId:D}) -> Item {item.Name}({item.RoutingRuleItemId:D})");

        result = await service.TryUpdateAsync(item, cancellationToken) && result;

        return (result, isDeactivatedToUpdate);
    }

    private async Task<(bool Result, bool IsDeactivatedToUpdate)> AssignRuleAsync(
        IOrganizationServiceAsync2 service, RoutingRule rule, string owner, bool isDeactivatedToUpdate,
        CancellationToken cancellationToken)
    {
        var result = true;
        var ownerQuery = new QueryExpression(SystemUser.EntityLogicalName)
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
        var ownerSystemUser = (await service.RetrieveAllAsync(ownerQuery, cancellationToken: cancellationToken))
            .SingleOrDefault()?.ToEntity<SystemUser>();

        if (ownerSystemUser == null)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]Cannot find Assignee: {owner}[/]");
            return (true, isDeactivatedToUpdate);
        }

        if (rule.StatusCode?.Value == RoutingRule.Options.StatusCode.Active)
        {
            Console.WriteLine($"Deactivate: Routing Rule {rule.Name}({rule.RoutingRuleId:D})");

            result = await SetRoutingRuleStateAsync(service, rule, RoutingRule.Options.StateCode.Draft,
                RoutingRule.Options.StatusCode.Draft, cancellationToken) && result;

            isDeactivatedToUpdate = true;
        }

        Console.WriteLine($"Assign: {rule.Name}({rule.RoutingRuleId:D}) to {ownerSystemUser.DomainName}");
        var (assigned, _) = await service.TryExecuteAsync<AssignRequest, AssignResponse>(new AssignRequest
        {
            Target = rule.ToEntityReference(),
            Assignee = ownerSystemUser.ToEntityReference()
        }, cancellationToken);
        return (assigned && result, isDeactivatedToUpdate);
    }

    private static Task<bool> SetRoutingRuleStateAsync(IOrganizationServiceAsync2 service, RoutingRule rule, int state, int status,
        CancellationToken cancellationToken) =>
        service.TryUpdateAsync(new RoutingRule(rule.Id)
        {
            StateCode = new OptionSetValue(state),
            StatusCode = new OptionSetValue(status)
        }, cancellationToken);
}
