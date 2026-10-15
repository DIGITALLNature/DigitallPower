// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.export.Logic;

public sealed class SlaConfigExport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IFileService fileService,
    IAnsiConsole console)
    : PowerCommand<ExportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync([NotNull] ExportVerb args, CancellationToken cancellationToken)
    {
        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "slaconfig.json" : args.FileName;


        var configs = new List<SlaConfig>();

        var service = (IOrganizationService)await connection.GetServiceAsync();
        using (var context = new DataContext(service))
        {
            configs.AddRange(context.SLASet.Select(s => new SlaConfig
            {
                Name = s.Name ?? string.Empty,
                SlaId = s.SLAId,
                BusinessHours = s.BusinessHoursId != null ? s.BusinessHoursId.Id : null,
                Active = s.StatusCode != null && s.StatusCode.Value == SLA.Options.StatusCode.Active
            }).ToList());
        }

        var json = ExportJson.GetJson(configs);

        Console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");

        return true;
    }
}
