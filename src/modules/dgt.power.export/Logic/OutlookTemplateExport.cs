// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Sdk;
using Spectre.Console;
using SavedQuery = dgt.power.dataverse.SavedQuery;

namespace dgt.power.export.Logic;

public sealed class OutlookTemplateExport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IFileService fileService,
    IAnsiConsole console)
    : PowerCommand<ExportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ExportVerb args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);

        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "outlooktemplate.json" : args.FileName;

        var service = (IOrganizationService)await connection.GetServiceAsync();
        IList<SavedQuery> savedQueries;
        using (var context = new DataContext(service))
        {
            savedQueries = (from sq in context.SavedQuerySet
                where sq.QueryType == SavedQueryQueryType.OutlookTemplate
                orderby sq.Name
                select sq).ToList();
        }

        var export = new dto.SavedQuery();

        foreach (var savedQuery in savedQueries)
        {
            if (savedQuery.IsDefault ?? false)
            {
                var outlookTemplate = new OutlookTemplate
                {
                    Name = savedQuery.Name!,
                    Entity = savedQuery.ReturnedTypeCode!,
                    IsDefault = true,
                    FetchXml = savedQuery.FetchXml!,
                    Description = savedQuery.Description!
                };
                export.OutlookTemplates.Add(outlookTemplate);
            }
            else
            {
                export.DisabledOutlookTemplates.Add(savedQuery.Name!);
            }
        }

        var json = ExportJson.GetJson(export);

        console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");
        return true;
    }
}
