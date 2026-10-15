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
    protected override async Task<bool> RunAsync([NotNull] ImportVerb args, CancellationToken cancellationToken)
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

        var service = (IOrganizationService)await connection.GetServiceAsync();
        var alternativeOwner = GetAlternativeOwner(service, args, queuesToTransport);

        var queues = GetQueues(service);

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
            result = UpdateQueue(service, queues, updateQueue, alternativeOwner) && result;
        }

        //find queues which need to be created
        //create
        Console.WriteLine("create check");
        foreach (var newQueue in queuesToTransport.QueuesToTransport.Where(c => queues.TrueForAll(e => e.Id != c.QueueId)))
        {
            var isCreated = CreateQueue(service, newQueue, out var id);
            if (!isCreated)
            {
                result = false;
                continue;
            }

            if (alternativeOwner != null && !service.TryAssign(new EntityReference(dataverse.Queue.EntityLogicalName, id), alternativeOwner))
            {
                result = false;
            }
        }

        return result;
    }

    private bool CreateQueue(IOrganizationService service, Queue newQueue, out Guid id)
    {
        Console.MarkupLine("[grey]--->[/]");
        //create queue
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]create queue: {newQueue.Name}[/]");
        var isCreated = service.TryCreate(new dataverse.Queue(newQueue.QueueId)
        {
            Name = newQueue.Name,
            QueueViewType = new OptionSetValue((int)newQueue.ViewType),
            IncomingEmailFilteringMethod = new OptionSetValue((int)newQueue.IncomingEmailFiltering),
            IncomingEmailDeliveryMethod = new OptionSetValue((int)newQueue.IncomingEmailDelivery),
            OutgoingEmailDeliveryMethod = new OptionSetValue((int)newQueue.OutgoingEmailDelivery),
            Description = newQueue.Description
        }, out id);
        return isCreated;
    }

    private void DeleteQueue(dataverse.Queue deleteQueue)
    {
        Console.MarkupLine("[grey]--->[/]");
        //delete queue
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]obsolete/unknown queue: {deleteQueue.Name}[/]");
        // Deleting obsolete queues is intentionally deactivated.
    }

    private bool UpdateQueue(IOrganizationService service, IEnumerable<dataverse.Queue> queues, Queue updateQueue, SystemUser? alternativeOwner)
    {
        Console.MarkupLine("[grey]--->[/]");
        var existingQueue = queues.Single(e => e.Id == updateQueue.QueueId);

        //check the owner
        if (alternativeOwner != null && existingQueue.OwnerId!.Id != alternativeOwner.Id)
        {
            if (!service.TryAssign(existingQueue.ToEntityReference(), alternativeOwner))
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
            result = service.TryUpdate(new dataverse.Queue(existingQueue.Id)
            {
                Name = updateQueue.Name,
                QueueViewType = new OptionSetValue((int)updateQueue.ViewType),
                IncomingEmailFilteringMethod = new OptionSetValue((int)updateQueue.IncomingEmailFiltering),
                IncomingEmailDeliveryMethod = new OptionSetValue((int)updateQueue.IncomingEmailDelivery),
                OutgoingEmailDeliveryMethod = new OptionSetValue((int)updateQueue.OutgoingEmailDelivery),
                Description = updateQueue.Description
            });
        }

        return result;
    }

    private static SystemUser? GetAlternativeOwner(IOrganizationService service, ImportVerb args, Queues queuesToTransport)
    {
        var alternativeOwner = default(SystemUser);
        var owner = ImportAssignee.GetAssignee(args.Assignee, queuesToTransport);
        if (!string.IsNullOrWhiteSpace(owner))
        {
            using var context = new DataContext(service);
            alternativeOwner = (from su in context.SystemUserSet
                where su.DomainName == owner
                select su).SingleOrDefault();
        }

        return alternativeOwner;
    }

    private static List<dataverse.Queue> GetQueues(IOrganizationService service)
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
        query.PageInfo = new PagingInfo
        {
            Count = 5000,
            PageNumber = 1,
            PagingCookie = null
        };
        List<dataverse.Queue> queues = new List<dataverse.Queue>();
        var moreRecords = true;
        while (moreRecords)
        {
            // Retrieve the page.
            var results = service.RetrieveMultiple(query);
            foreach (var entity in results.Entities)
            {
                queues.Add(entity.ToEntity<dataverse.Queue>());
            }

            if (results.MoreRecords)
            {
                // Increment the page number to retrieve the next page.
                query.PageInfo.PageNumber++;
                // Set the paging cookie to the paging cookie returned from current results.
                query.PageInfo.PagingCookie = results.PagingCookie;
            }

            moreRecords = results.MoreRecords;
        }

        return queues;
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
