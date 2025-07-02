// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;

namespace dgt.power.export.Logic;

public sealed class SlaConfigExport : BaseExport
{
    public SlaConfigExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IFileService fileService)
        : base(tracer, connection, configResolver, fileService)
    {
    }

    protected override bool Invoke(ExportVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);

        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "slaconfig.json" : args.FileName;


        var configs = new SlaConfigs();

        using (var context = new DataContext(Connection))
        {
            configs.AddRange(context.SLASet.Select(s => new SlaConfig
            {
                Name = s.Name,
                SlaId = s.SLAId,
                BusinessHours = s.BusinessHoursId != null ? s.BusinessHoursId.Id : default(Guid?),
                Active = s.StatusCode != null && s.StatusCode.Value == SLA.Options.StatusCode.Active
            }).ToList());
        }

        var json = GetJson(configs);

        Tracer.Log($"Export {HandleExportFile(fileDir, fileName, json)} ", TraceEventType.Information);

        return Tracer.End(this, true);
    }
}
