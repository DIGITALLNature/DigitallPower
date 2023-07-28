// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Queue = dgt.power.dto.Queue;

namespace dgt.power.import.Logic;

public sealed class QueueImport : BaseImport
{
    public QueueImport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(ImportVerb args)
    {
        Tracer.Start(this);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "queue.json" : args.FileName;


        if (!ConfigResolver.TryGetConfigFile<Queues>(args.FileDir, fileName, out var queuesToTransport))
        {
            return Tracer.NotConfigured(this);
        }

        //anything to do?
        if (!queuesToTransport.QueuesToTransport.Any())
        {
            return Tracer.NotConfigured(this);
        }

        var result = true;

        var alternativeOwner = GetAlternativeOwner(args, queuesToTransport);

        var queues = GetQueues();

        //TODO: delete vs. deactivate
        //find queues which need to be removed
        Tracer.Log("delete check", TraceEventType.Information);
        foreach (var deleteQueue in queues.Where(e => queuesToTransport.QueuesToTransport.All(c => e.Id != c.QueueId)))
        {
            DeleteQueue(deleteQueue);
        }

        //find queues need to be updated
        Tracer.Log("update check", TraceEventType.Information);
        foreach (var updateQueue in queuesToTransport.QueuesToTransport.Where(c => queues.Any(e => e.Id == c.QueueId)))
        {
            result = UpdateQueue(queues, updateQueue, alternativeOwner, result);
        }

        //find queues which need to be created
        //create
        Tracer.Log("create check", TraceEventType.Information);
        foreach (var newQueue in queuesToTransport.QueuesToTransport.Where(c => queues.All(e => e.Id != c.QueueId)))
        {
            var isCreated = CreateQueue(newQueue, out var id);
            if (!isCreated)
            {
                result = false;
                continue;
            }

            if (alternativeOwner != null)
            {
                if (!Connection.TryAssign(new EntityReference(dataverse.Queue.EntityLogicalName, id), alternativeOwner))
                {
                    result = false;
                }
            }
        }

        return Tracer.End(this, result);
    }

    private bool CreateQueue(Queue newQueue, out Guid id)
    {
        Tracer.Log("--->", TraceEventType.Verbose);
        //create queue
        Tracer.Log($"create queue: {newQueue.Name}", TraceEventType.Verbose);
        var isCreated = Connection.TryCreate(new dataverse.Queue(newQueue.QueueId)
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
        Tracer.Log("--->", TraceEventType.Verbose);
        //delete queue
        Tracer.Log($"obsolete/unknown queue: {deleteQueue.Name}", TraceEventType.Verbose);
        //tracer.Log($"delete queue: {deleteQueue.Name}", TraceEventType.Verbose);
        // TODO: Why is queue deletion deactivated?
        //result = XrmWorker.Delete(tracer, service, Model.Queue.EntityLogicalName, deleteQueue.Id, dryRun) & result;
    }

    private bool UpdateQueue(IList<dataverse.Queue> queues, Queue updateQueue, SystemUser? alternativeOwner, bool result)
    {
        Tracer.Log("--->", TraceEventType.Verbose);
        var existingQueue = queues.Single(e => e.Id == updateQueue.QueueId);

        //check the owner
        if (alternativeOwner != null && existingQueue.OwnerId!.Id != alternativeOwner.Id)
        {
            if (!Connection.TryAssign(existingQueue.ToEntityReference(), alternativeOwner))
            {
                return false;
            }
        }

        var unchangedRule = Unchanged(existingQueue, updateQueue);

        if (unchangedRule)
        {
            Tracer.Log($"unchanged queue: {updateQueue.Name}", TraceEventType.Verbose);
        }
        else
        {
            //update queue
            Tracer.Log($"update queue: {updateQueue.Name}", TraceEventType.Verbose);
            result = Connection.TryUpdate(new dataverse.Queue(existingQueue.Id)
            {
                Name = updateQueue.Name,
                QueueViewType = new OptionSetValue((int)updateQueue.ViewType),
                IncomingEmailFilteringMethod = new OptionSetValue((int)updateQueue.IncomingEmailFiltering),
                IncomingEmailDeliveryMethod = new OptionSetValue((int)updateQueue.IncomingEmailDelivery),
                OutgoingEmailDeliveryMethod = new OptionSetValue((int)updateQueue.OutgoingEmailDelivery),
                Description = updateQueue.Description
            }) & result;
        }

        return result;
    }

    private SystemUser? GetAlternativeOwner(ImportVerb args, Queues queuesToTransport)
    {
        var alternativeOwner = default(SystemUser);
        var owner = GetAssignee(args.Assignee, queuesToTransport);
        if (!string.IsNullOrWhiteSpace(owner))
        {
            using var context = new DataContext(Connection);
            alternativeOwner = (from su in context.SystemUserSet
                where su.DomainName == owner
                select su).SingleOrDefault();
        }

        return alternativeOwner;
    }

    private IList<dataverse.Queue> GetQueues()
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
        IList<dataverse.Queue> queues = new List<dataverse.Queue>();
        while (true)
        {
            // Retrieve the page.
            var results = Connection.RetrieveMultiple(query);
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
            else
            {
                // If no more records are in the result nodes, exit the loop.
                break;
            }
        }

        return queues;
    }

    private static bool Unchanged(dataverse.Queue existing, Queue config)
    {
        if (config == null)
        {
            return false;
        }

        return
            (
                existing.Name == config.Name ||
                (existing.Name != null &&
                 existing.Name.Equals(config.Name, StringComparison.Ordinal))
            ) &&
            existing.QueueViewType?.Value == (int)config.ViewType
            &&
            existing.IncomingEmailFilteringMethod?.Value == (int)config.IncomingEmailFiltering
            &&
            //(
            //    existing.IncomingEmailDeliveryMethod?.Value == (int)config.IncomingEmailDelivery
            //) &&
            //(
            //    existing.OutgoingEmailDeliveryMethod?.Value == (int)config.OutgoingEmailDelivery
            //) &&
            (
                existing.Description == config.Description ||
                (existing.Description != null &&
                 existing.Description.Equals(config.Description, StringComparison.Ordinal))
            );
    }
}
