// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Base.Config;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public sealed class AutoNumberFormatAction(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<MaintenanceVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(MaintenanceVerb args, CancellationToken cancellationToken)
    {
        //read config
        if (!configResolver.TryGetConfigFile<List<AutoNumberFormat>>(args.Config, out var autoNumberFormats))
        {
            return false;
        }

        //anything to do?
        if (autoNumberFormats.Count == 0)
        {
            Console.MarkupLine("[red]Not configured.[/]");
            return false;
        }

        var service = (IOrganizationService)await connection.GetServiceAsync();

        //process
        var result = true;
        foreach (var autoNumberFormat in autoNumberFormats)
        {
            result = Process(service, autoNumberFormat) && result;
        }

        return result;
    }

    private bool Process(IOrganizationService service, AutoNumberFormat autoNumberFormat)
    {
        Console.MarkupLineInterpolated(CultureInfo.InvariantCulture,
            $"[grey]Check the auto number format for {autoNumberFormat.Entity}.{autoNumberFormat.Field}[/]");

        var attributeRequest = new RetrieveAttributeRequest
        {
            EntityLogicalName = autoNumberFormat.Entity,
            LogicalName = autoNumberFormat.Field,
            RetrieveAsIfPublished = true
        };
        var attributeResponse = (RetrieveAttributeResponse)service.Execute(attributeRequest);

        var format = attributeResponse.AttributeMetadata.AutoNumberFormat;

        if (autoNumberFormat.Format.Equals(format, StringComparison.Ordinal))
        {
            return true;
        }

        Console.WriteLine(
            $"Update the auto number format for {autoNumberFormat.Entity}.{autoNumberFormat.Field} from {format} to {autoNumberFormat.Format}");

        // Modify the retrieved attribute
        var retrievedAttributeMetadata = attributeResponse.AttributeMetadata;
        retrievedAttributeMetadata.AutoNumberFormat = autoNumberFormat.Format;

        var updateRequest = new UpdateAttributeRequest
        {
            Attribute = retrievedAttributeMetadata,
            EntityName = autoNumberFormat.Entity,
            MergeLabels = false
        };

        return service.TryExecute<UpdateAttributeRequest, UpdateAttributeResponse>(updateRequest, out _);
    }
}
