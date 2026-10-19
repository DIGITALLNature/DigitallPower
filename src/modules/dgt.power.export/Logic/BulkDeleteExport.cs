// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.export.Logic;

public sealed class BulkDeleteExport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IFileService fileService,
    IAnsiConsole console)
    : PowerCommand<ExportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ExportVerb args, CancellationToken cancellationToken)
    {
        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "bulkdelete.json" : args.FileName;


        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.MarkupLine("[red]No export file name configured.[/]");
            return false;
        }

        var service = await connection.GetServiceAsync();
        var query = new QueryExpression(AsyncOperation.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                AsyncOperation.LogicalNames.Name,
                AsyncOperation.LogicalNames.Data,
                AsyncOperation.LogicalNames.RecurrencePattern,
                AsyncOperation.LogicalNames.RecurrenceStartTime,
                AsyncOperation.LogicalNames.OperationType),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(AsyncOperation.LogicalNames.OperationType, ConditionOperator.Equal,
                        AsyncOperation.Options.OperationType.BulkDelete),
                    new ConditionExpression(AsyncOperation.LogicalNames.RecurrenceStartTime, ConditionOperator.NotNull)
                }
            },
            Orders = { new OrderExpression(AsyncOperation.LogicalNames.Name, OrderType.Ascending) }
        };
        var operations = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Select(entity => entity.ToEntity<AsyncOperation>()).ToList();

        var export = new BulkDeletes
        {
            Deletes = new List<BulkDelete>()
        };

        foreach (var operation in operations)
        {
            var data = operation.Data!;
            var start = data.IndexOf("<string>&lt;fetch", StringComparison.InvariantCultureIgnoreCase) + 8;
            var end = data.IndexOf("fetch&gt;</string>", StringComparison.InvariantCultureIgnoreCase) + 9;
            var fetchXml = data.Substring(start, end - start).Replace("&lt;", "<", StringComparison.Ordinal).Replace("&gt;", ">", StringComparison.Ordinal);
            var bulkDelete = new BulkDelete
            {
                Name = operation.Name!,
                RecurrencePattern = operation.RecurrencePattern!,
                RecurrenceStartTime = $"{operation.RecurrenceStartTime:HH:mm}",
                FetchXml = fetchXml
            };
            export.Deletes.Add(bulkDelete);
        }

        var json = ExportJson.GetJson(export);

        Console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");
        return true;
    }
}
