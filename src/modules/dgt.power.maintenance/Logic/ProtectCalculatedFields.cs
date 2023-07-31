// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.maintenance.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.maintenance.Logic
{
    public sealed class ProtectCalculatedFields : BaseMaintenance
    {
        public ProtectCalculatedFields(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
        {
        }

        protected override bool Invoke(MaintenanceVerb args)
        {
            Debug.Assert(args != null, nameof(args) + " != null");
            Tracer.Start(this);
            var updated = 0;

            AnsiConsole.Status()
                .Start("Retrieve Metadata...", ctx =>
                {
                    ctx.Spinner(Spinner.Known.Smiley);
                    ctx.SpinnerStyle(Style.Parse("green"));

                    AnsiConsole.MarkupLine("Retrieve Metadata");

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
                            AnsiConsole.MarkupLine($"Protect  <{attributeMetadata.EntityLogicalName}> {attributeMetadata.LogicalName}");
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


            AnsiConsole.MarkupLine($"Protected {updated} fields");


            return true;
        }
    }
}
