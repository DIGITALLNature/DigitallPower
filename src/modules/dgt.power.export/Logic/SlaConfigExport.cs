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

public sealed class SlaConfigExport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IFileService fileService,
    IAnsiConsole console)
    : PowerCommand<ExportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ExportVerb args, CancellationToken cancellationToken)
    {
        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "slaconfig.json" : args.FileName;


        var configs = new List<SlaConfig>();

        var service = await connection.GetServiceAsync();
        var query = new QueryExpression(SLA.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(SLA.LogicalNames.Name, SLA.LogicalNames.SLAId,
                SLA.LogicalNames.BusinessHoursId, SLA.LogicalNames.StatusCode)
        };
        var records = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        configs.AddRange(records.Select(entity => entity.ToEntity<SLA>()).Select(s => new SlaConfig
        {
            Name = s.Name ?? string.Empty,
            SlaId = s.SLAId,
            BusinessHours = s.BusinessHoursId?.Id,
            Active = s.StatusCode?.Value == SLA.Options.StatusCode.Active
        }));

        var json = ExportJson.GetJson(configs);

        Console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");

        return true;
    }
}
