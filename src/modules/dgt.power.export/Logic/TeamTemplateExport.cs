// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.export.Logic;

public sealed class TeamTemplateExport : BaseExport
{
    public TeamTemplateExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IFileService fileService)
        : base(tracer, connection, configResolver, fileService)
    {
    }

    protected override bool Invoke(ExportVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);

        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "teamtemplate.json" : args.FileName;

        var request = new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity
        };
        var response = (RetrieveAllEntitiesResponse)Connection.Execute(request);
        var records = response.EntityMetadata.ToList();

        IList<TeamTemplate> teamTemplates;
        using (var context = new DataContext(Connection))
        {
            teamTemplates = (from tt in context.TeamTemplateSet
                orderby tt.TeamTemplateName
                select tt).ToList();
        }

        var export = new List<dto.TeamTemplate>();

        foreach (var teamTemplate in teamTemplates)
        {
            var entity = records.First(e => e.ObjectTypeCode == teamTemplate.ObjectTypeCode);
            var id = teamTemplate.TeamTemplateId!.Value;
            var template = new dto.TeamTemplate
            {
                TeamTemplateId = id,
                TeamTemplateName = teamTemplate.TeamTemplateName!,
                Description = teamTemplate.Description,
                IsSystem = teamTemplate.IsSystem,
                DefaultAccessRightsMask = teamTemplate.DefaultAccessRightsMask,
                Entity = entity.LogicalName
            };
            export.Add(template);
        }

        var json = GetJson(export);

        Tracer.Log($"Export {HandleExportFile(fileDir, fileName, json)} ", TraceEventType.Information);
        return Tracer.End(this, true);
    }
}
