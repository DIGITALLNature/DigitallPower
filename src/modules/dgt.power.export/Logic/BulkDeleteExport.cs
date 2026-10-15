// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;
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

        var service = (IOrganizationService)await connection.GetServiceAsync();
        IList<AsyncOperation> operations;
        using (var context = new DataContext(service))
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            operations = (from ao in context.AsyncOperationSet
                where ao.OperationType != null
                where ao.OperationType.Value == AsyncOperation.Options.OperationType.BulkDelete
                where ao.RecurrenceStartTime != null
                orderby ao.Name
                select ao).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

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
