// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.maintenance.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public sealed class ProtectCalculatedFields(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IAnsiConsole console)
    : PowerCommand<MaintenanceVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(MaintenanceVerb args, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(args);

        var service = (IOrganizationService)await connection.GetServiceAsync();
        return Run(service);
    }

    #pragma warning disable S1135 // Async migration tracked in todo.md
    // TODO(async): migrate to IOrganizationServiceAsync2
    #pragma warning restore S1135
    private bool Run(IOrganizationService service)
    {
        var updated = 0;

        console.Status()
            .Start("Retrieve Metadata...", ctx =>
            {
                ctx.Spinner(Spinner.Known.Smiley);
                ctx.SpinnerStyle(Style.Parse("green"));

                console.MarkupLine("Retrieve Metadata");

                var request = new RetrieveAllEntitiesRequest
                {
                    EntityFilters = EntityFilters.Attributes
                };
                var response = (RetrieveAllEntitiesResponse)service.Execute(request);
                var attributeMetadatas = response.EntityMetadata.SelectMany(a => a.Attributes).Where(a => a.SourceType == 1 && a.IsManaged == false);

                foreach (var attributeMetadata in attributeMetadatas)
                {
                    ctx.Status($"Check <{attributeMetadata.EntityLogicalName}> {attributeMetadata.LogicalName}");
                    if (attributeMetadata is MoneyAttributeMetadata { IsBaseCurrency: true })
                    {
                        continue;
                    }

                    if (attributeMetadata.LogicalName == "bpf_duration")
                    {
                        continue;
                    }

                    if (attributeMetadata.IsCustomizable.Value && attributeMetadata.IsCustomizable.CanBeChanged)
                    {
                        console.MarkupLine($"Protect  <{attributeMetadata.EntityLogicalName}> {attributeMetadata.LogicalName}");
                        attributeMetadata.IsCustomizable = new BooleanManagedProperty(false);
                        service.Execute(new UpdateAttributeRequest
                        {
                            Attribute = attributeMetadata,
                            EntityName = attributeMetadata.EntityLogicalName,
                            MergeLabels = false
                        });
                        updated++;
                    }
                }
            });


        console.MarkupLine($"Protected {updated} fields");


        return true;
    }
}