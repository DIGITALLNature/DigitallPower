// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Crm.Sdk;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
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
        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "outlooktemplate.json" : args.FileName;

        var service = await connection.GetServiceAsync();
        var query = new QueryExpression(SavedQuery.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                SavedQuery.LogicalNames.Name,
                SavedQuery.LogicalNames.ReturnedTypeCode,
                SavedQuery.LogicalNames.IsDefault,
                SavedQuery.LogicalNames.FetchXml,
                SavedQuery.LogicalNames.Description),
            Criteria =
            {
                Conditions =
                {
                    new ConditionExpression(SavedQuery.LogicalNames.QueryType, ConditionOperator.Equal,
                        (int)SavedQueryQueryType.OutlookTemplate)
                }
            },
            Orders = { new OrderExpression(SavedQuery.LogicalNames.Name, OrderType.Ascending) }
        };
        var savedQueries = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .Select(entity => entity.ToEntity<SavedQuery>()).ToList();

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

        Console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");
        return true;
    }
}
