using System.Diagnostics;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;

namespace dgt.power.export.Logic;

public sealed class SlaConfigExport : BaseExport
{
    public SlaConfigExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(ExportVerb args)
    {
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
