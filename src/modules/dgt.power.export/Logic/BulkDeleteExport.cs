using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;

namespace dgt.power.export.Logic;

public sealed class BulkDeleteExport : BaseExport
{
    public BulkDeleteExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IFileService fileService)
        : base(tracer, connection, configResolver, fileService)
    {
    }

    protected override bool Invoke(ExportVerb args)
    {
        Tracer.Start(this);

        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "bulkdelete.json" : args.FileName;


        if (string.IsNullOrWhiteSpace(fileName))
        {
            return Tracer.NotConfigured(this);
        }

        IList<AsyncOperation> operations;
        using (var context = new DataContext(Connection))
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
            var fetchXml = data.Substring(start, end - start).Replace("&lt;", "<").Replace("&gt;", ">");
            var bulkDelete = new BulkDelete
            {
                Name = operation.Name!,
                RecurrencePattern = operation.RecurrencePattern!,
                RecurrenceStartTime = $"{operation.RecurrenceStartTime:HH:mm}",
                FetchXml = fetchXml
            };
            export.Deletes.Add(bulkDelete);
        }

        var json = GetJson(export);

        Tracer.Log($"Export {HandleExportFile(fileDir, fileName, json)} ", TraceEventType.Information);
        return Tracer.End(this, true);
    }
}
