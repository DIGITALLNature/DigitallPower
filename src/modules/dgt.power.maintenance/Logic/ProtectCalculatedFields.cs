// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.maintenance.Base;
using Microsoft.PowerPlatform.Dataverse.Client;
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
        var service = await connection.GetServiceAsync();
        return await RunAsync(service, cancellationToken);
    }

    private async Task<bool> RunAsync(IOrganizationServiceAsync2 service, CancellationToken cancellationToken)
    {
        var updated = 0;

        await Console.Status()
            .StartAsync("Retrieve Metadata...", async ctx =>
            {
                ctx.Spinner(Spinner.Known.Smiley);
                ctx.SpinnerStyle(Style.Parse("green"));

                Console.MarkupLine("Retrieve Metadata");

                var request = new RetrieveAllEntitiesRequest
                {
                    EntityFilters = EntityFilters.Attributes
                };
                var response = (RetrieveAllEntitiesResponse)await service.ExecuteAsync(request, cancellationToken);
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
                        Console.MarkupLine($"Protect  <{attributeMetadata.EntityLogicalName}> {attributeMetadata.LogicalName}");
                        attributeMetadata.IsCustomizable = new BooleanManagedProperty(false);
                        await service.ExecuteAsync(new UpdateAttributeRequest
                        {
                            Attribute = attributeMetadata,
                            EntityName = attributeMetadata.EntityLogicalName,
                            MergeLabels = false
                        }, cancellationToken);
                        updated++;
                    }
                }
            });


        Console.MarkupLine($"Protected {updated} fields");


        return true;
    }
}