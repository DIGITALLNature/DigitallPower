// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using Queue = dgt.power.dataverse.Queue;

namespace dgt.power.export.Logic;

public sealed class QueueExport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IFileService fileService,
    IAnsiConsole console)
    : PowerCommand<ExportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ExportVerb args, CancellationToken cancellationToken)
    {
        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "queue.json" : args.FileName;

        var service = await connection.GetServiceAsync();

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
        var queues = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Select(entity => entity.ToEntity<Queue>()).ToList();

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

        var json = ExportJson.GetJson(export);

        Console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");
        return true;
    }
}
