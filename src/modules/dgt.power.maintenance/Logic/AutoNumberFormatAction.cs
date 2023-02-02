using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Base.Config;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace dgt.power.maintenance.Logic;

public sealed class AutoNumberFormatAction : BaseMaintenance
{
    public AutoNumberFormatAction(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(
        tracer, connection, configResolver)
    {
    }

    protected override bool Invoke(MaintenanceVerb args)
    {
        Tracer.Start(this);
        //read config
        if (!ConfigResolver.GetConfigFile<AutoNumberFormats>(args.Config, out var autoNumberFormats))
        {
            return Tracer.End(this, false);
        }

        //anything to do?
        if (!autoNumberFormats.Any())
        {
            return Tracer.NotConfigured(this);
        }

        //process
        var result = true;
        foreach (var autoNumberFormat in autoNumberFormats)
        {
            result = Process(autoNumberFormat) & result;
        }

        return Tracer.End(this, result);
    }

    private bool Process(AutoNumberFormat autoNumberFormat)
    {
        Tracer.Log($"Check the auto number format for {autoNumberFormat.Entity}.{autoNumberFormat.Field}",
            TraceEventType.Verbose);

        var attributeRequest = new RetrieveAttributeRequest
        {
            EntityLogicalName = autoNumberFormat.Entity,
            LogicalName = autoNumberFormat.Field,
            RetrieveAsIfPublished = true
        };
        var attributeResponse = (RetrieveAttributeResponse)Connection.Execute(attributeRequest);

        var format = attributeResponse.AttributeMetadata.AutoNumberFormat;

        if (autoNumberFormat.Format.Equals(format, StringComparison.Ordinal))
        {
            return true;
        }

        Tracer.Log(
            $"Update the auto number format for {autoNumberFormat.Entity}.{autoNumberFormat.Field} from {format} to {autoNumberFormat.Format}",
            TraceEventType.Information);

        // Modify the retrieved attribute
        var retrievedAttributeMetadata = attributeResponse.AttributeMetadata;
        retrievedAttributeMetadata.AutoNumberFormat = autoNumberFormat.Format;

        var updateRequest = new UpdateAttributeRequest
        {
            Attribute = retrievedAttributeMetadata,
            EntityName = autoNumberFormat.Entity,
            MergeLabels = false
        };

        return Connection.TryExecute<UpdateAttributeRequest, UpdateAttributeResponse>(updateRequest, out _);
    }
}
