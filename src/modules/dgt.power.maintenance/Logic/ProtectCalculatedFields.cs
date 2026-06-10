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
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : BaseMaintenance(tracer, connection, configResolver, console)
{
    protected override Task<bool> InvokeAsync(MaintenanceVerb args, CancellationToken cancellationToken) =>
        Task.FromResult(InvokeCore());

    #pragma warning disable S1135 // Async migration tracked in todo.md
    // TODO(async): migrate to IOrganizationServiceAsync2
    #pragma warning restore S1135
    private bool InvokeCore()
    {
        Tracer.Start(this);
        var updated = 0;

        Console.Status()
            .Start("Retrieve Metadata...", ctx =>
            {
                ctx.Spinner(Spinner.Known.Smiley);
                ctx.SpinnerStyle(Style.Parse("green"));

                Console.MarkupLine("Retrieve Metadata");

                var request = new RetrieveAllEntitiesRequest
                {
                    EntityFilters = EntityFilters.Attributes
                };
                var response = (RetrieveAllEntitiesResponse)Connection.Execute(request);
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
                        Connection.Execute(new UpdateAttributeRequest
                        {
                            Attribute = attributeMetadata,
                            EntityName = attributeMetadata.EntityLogicalName,
                            MergeLabels = false
                        });
                        updated++;
                    }
                }
            });


        Console.MarkupLine($"Protected {updated} fields");


        return true;
    }
}