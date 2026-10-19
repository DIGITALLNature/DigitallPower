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
using Queue = dgt.power.dto.Queue;

namespace dgt.power.import.Logic;

public sealed class QueueImport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<ImportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ImportVerb args, CancellationToken cancellationToken)
    {
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "queue.json" : args.FileName;


        if (!configResolver.TryGetConfigFile<Queues>(args.FileDir, fileName, out var queuesToTransport))
        {
            Console.MarkupLine("[red]Queue configuration not found.[/]");
            return false;
        }

        //anything to do?
        if (queuesToTransport.QueuesToTransport.Count == 0)
        {
            Console.MarkupLine("[red]Queue configuration is empty.[/]");
            return false;
        }

        var result = true;

        var service = await connection.GetServiceAsync();
        var alternativeOwner = await GetAlternativeOwnerAsync(service, args, queuesToTransport, cancellationToken);

        var queues = await GetQueuesAsync(service, cancellationToken);

        //TODO: delete vs. deactivate
        //find queues which need to be removed
        Console.WriteLine("delete check");
        foreach (var deleteQueue in queues.Where(e => queuesToTransport.QueuesToTransport.All(c => e.Id != c.QueueId)))
        {
            DeleteQueue(deleteQueue);
        }

        //find queues need to be updated
        Console.WriteLine("update check");
        foreach (var updateQueue in queuesToTransport.QueuesToTransport.Where(c => queues.Exists(e => e.Id == c.QueueId)))
        {
            result = await UpdateQueueAsync(service, queues, updateQueue, alternativeOwner, cancellationToken) && result;
        }

        //find queues which need to be created
        //create
        Console.WriteLine("create check");
        foreach (var newQueue in queuesToTransport.QueuesToTransport.Where(c => queues.TrueForAll(e => e.Id != c.QueueId)))
        {
            var isCreated = await CreateQueueAsync(service, newQueue, cancellationToken);
            var id = newQueue.QueueId;
            if (!isCreated)
            {
                result = false;
                continue;
            }

            var assigned = true;
            if (alternativeOwner != null)
            {
                (assigned, _) = await service.TryExecuteAsync<AssignRequest, AssignResponse>(new AssignRequest
                {
                    Target = new EntityReference(dataverse.Queue.EntityLogicalName, id),
                    Assignee = alternativeOwner.ToEntityReference()
                }, cancellationToken);
            }

            if (!assigned)
            {
                result = false;
            }
        }

        return result;
    }

    private async Task<bool> CreateQueueAsync(IOrganizationServiceAsync2 service, Queue newQueue, CancellationToken cancellationToken)
    {
        Console.MarkupLine("[grey]--->[/]");
        //create queue
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]create queue: {newQueue.Name}[/]");
        return await service.TryCreateAsync(new dataverse.Queue(newQueue.QueueId)
        {
            Name = newQueue.Name,
            QueueViewType = new OptionSetValue((int)newQueue.ViewType),
            IncomingEmailFilteringMethod = new OptionSetValue((int)newQueue.IncomingEmailFiltering),
            IncomingEmailDeliveryMethod = new OptionSetValue((int)newQueue.IncomingEmailDelivery),
            OutgoingEmailDeliveryMethod = new OptionSetValue((int)newQueue.OutgoingEmailDelivery),
            Description = newQueue.Description
        }, cancellationToken);
    }

    private void DeleteQueue(dataverse.Queue deleteQueue)
    {
        Console.MarkupLine("[grey]--->[/]");
        //delete queue
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]obsolete/unknown queue: {deleteQueue.Name}[/]");
        // Deleting obsolete queues is intentionally deactivated.
    }

    private async Task<bool> UpdateQueueAsync(IOrganizationServiceAsync2 service, IEnumerable<dataverse.Queue> queues, Queue updateQueue,
        SystemUser? alternativeOwner, CancellationToken cancellationToken)
    {
        Console.MarkupLine("[grey]--->[/]");
        var existingQueue = queues.Single(e => e.Id == updateQueue.QueueId);

        //check the owner
        if (alternativeOwner != null && existingQueue.OwnerId!.Id != alternativeOwner.Id)
        {
            var (assigned, _) = await service.TryExecuteAsync<AssignRequest, AssignResponse>(new AssignRequest
            {
                Target = existingQueue.ToEntityReference(),
                Assignee = alternativeOwner.ToEntityReference()
            }, cancellationToken);
            if (!assigned)
            {
                return false;
            }
        }

        var unchangedRule = Unchanged(existingQueue, updateQueue);
        var result = true;

        if (unchangedRule)
        {
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]unchanged queue: {updateQueue.Name}[/]");
        }
        else
        {
            //update queue
            Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]update queue: {updateQueue.Name}[/]");
            result = await service.TryUpdateAsync(new dataverse.Queue(existingQueue.Id)
            {
                Name = updateQueue.Name,
                QueueViewType = new OptionSetValue((int)updateQueue.ViewType),
                IncomingEmailFilteringMethod = new OptionSetValue((int)updateQueue.IncomingEmailFiltering),
                IncomingEmailDeliveryMethod = new OptionSetValue((int)updateQueue.IncomingEmailDelivery),
                OutgoingEmailDeliveryMethod = new OptionSetValue((int)updateQueue.OutgoingEmailDelivery),
                Description = updateQueue.Description
            }, cancellationToken);
        }

        return result;
    }

    private static async Task<SystemUser?> GetAlternativeOwnerAsync(IOrganizationServiceAsync2 service, ImportVerb args, Queues queuesToTransport,
        CancellationToken cancellationToken)
    {
        var alternativeOwner = default(SystemUser);
        var owner = ImportAssignee.GetAssignee(args.Assignee, queuesToTransport);
        if (!string.IsNullOrWhiteSpace(owner))
        {
            var query = new QueryExpression(SystemUser.EntityLogicalName)
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
            alternativeOwner = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
                .SingleOrDefault()?.ToEntity<SystemUser>();
        }

        return alternativeOwner;
    }

    private static async Task<List<dataverse.Queue>> GetQueuesAsync(IOrganizationServiceAsync2 service, CancellationToken cancellationToken)
    {
        var query = new QueryExpression
        {
            EntityName = dataverse.Queue.EntityLogicalName,
            ColumnSet = new ColumnSet(true),
            Distinct = false,
            NoLock = true
        };
        var startsWith = new ConditionExpression
        {
            AttributeName = dataverse.Queue.LogicalNames.Name,
            Operator = ConditionOperator.DoesNotBeginWith,
            Values = { "<" }
        };
        var endsWith = new ConditionExpression
        {
            AttributeName = dataverse.Queue.LogicalNames.Name,
            Operator = ConditionOperator.DoesNotEndWith,
            Values = { ">" }
        };
        var order = new OrderExpression
        {
            AttributeName = dataverse.Queue.LogicalNames.Name,
            OrderType = OrderType.Ascending
        };
        query.Criteria.AddCondition(startsWith);
        query.Criteria.AddCondition(endsWith);
        query.Orders.Add(order);
        return (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Select(entity => entity.ToEntity<dataverse.Queue>()).ToList();
    }

    private static bool Unchanged(dataverse.Queue existing, Queue config)
    {
        if (!IsSameText(existing.Name, config.Name))
        {
            return false;
        }

        if (existing.QueueViewType?.Value != (int)config.ViewType)
        {
            return false;
        }

        if (existing.IncomingEmailFilteringMethod?.Value != (int)config.IncomingEmailFiltering)
        {
            return false;
        }

        // Incoming/Outgoing delivery method checks stay intentionally disabled.
        return IsSameText(existing.Description, config.Description);
    }

    private static bool IsSameText(string? current, string? expected) =>
        string.Equals(current, expected, StringComparison.Ordinal);
}
