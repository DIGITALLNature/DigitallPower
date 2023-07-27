// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Queue = dgt.power.dataverse.Queue;

namespace dgt.power.export.Logic;

public sealed class QueueExport : BaseExport
{
    public QueueExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IFileService fileService)
        : base(tracer, connection, configResolver, fileService)
    {
    }

    protected override bool Invoke(ExportVerb args)
    {
        Tracer.Start(this);

        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "queue.json" : args.FileName;


        var query = new QueryExpression
        {
            EntityName = Queue.EntityLogicalName,
            ColumnSet = new ColumnSet(true),
            Distinct = false,
            NoLock = true
        };
        var startsWith = new ConditionExpression
        {
            AttributeName = Queue.LogicalNames.Name,
            Operator = ConditionOperator.DoesNotBeginWith,
            Values = { "<" }
        };
        var endsWith = new ConditionExpression
        {
            AttributeName = Queue.LogicalNames.Name,
            Operator = ConditionOperator.DoesNotEndWith,
            Values = { ">" }
        };
        var order = new OrderExpression
        {
            AttributeName = Queue.LogicalNames.Name,
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
        IList<Queue> queues = new List<Queue>();
        while (true)
        {
            // Retrieve the page.
            var results = Connection.RetrieveMultiple(query);
            foreach (var entity in results.Entities)
            {
                queues.Add(entity.ToEntity<Queue>());
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

        var export = new Queues
        {
            QueuesToTransport = queues.Select(queue => new dto.Queue
            {
                QueueId = queue.Id,
                Name = queue.Name!,
                Description = queue.Description,
                ViewType = (dto.Queue.QueueViewType)queue.QueueViewType!.Value,
                IncomingEmailDelivery = (dto.Queue.IncomingEmailDeliveryMethod)queue.IncomingEmailDeliveryMethod!.Value,
                IncomingEmailFiltering = (dto.Queue.IncomingEmailFilteringMethod)queue.IncomingEmailFilteringMethod!.Value,
                OutgoingEmailDelivery = (dto.Queue.OutgoingEmailDeliveryMethod)queue.OutgoingEmailDeliveryMethod!.Value
            }).ToList()
        };

        var json = GetJson(export);

        Tracer.Log($"Export {HandleExportFile(fileDir, fileName, json)} ", TraceEventType.Information);
        return Tracer.End(this, true);
    }
}
