﻿// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Sdk;
using SavedQuery = dgt.power.dataverse.SavedQuery;

namespace dgt.power.export.Logic;

public sealed class OutlookTemplateExport : BaseExport
{
    public OutlookTemplateExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IFileService fileService)
        : base(tracer, connection, configResolver, fileService)
    {
    }

    protected override bool Invoke(ExportVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);

        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "outlooktemplate.json" : args.FileName;

        IList<SavedQuery> savedQueries;
        using (var context = new DataContext(Connection))
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

        var json = GetJson(export);

        Tracer.Log($"Export {HandleExportFile(fileDir, fileName, json)} ", TraceEventType.Information);
        return Tracer.End(this, true);
    }
}
