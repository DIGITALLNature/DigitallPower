// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.dataverse;
using dgt.power.plugin.tests.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

internal static class ProviderTableFixture
{
    internal static void Seed(FakeOrganizationServiceAsync service, bool missing = false, bool incompatible = false)
    {
        var backingId = service.Create(new EntityDataProvider { Name = "JsonConverter" });
        var tableId = Guid.NewGuid();
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveMetadataChangesRequest), _ =>
        {
            var metadata = new EntityMetadataCollection();
            if (!missing)
            {
                metadata.Add(new EntityMetadata
                {
                    MetadataId = tableId, LogicalName = "dgt_source", SchemaName = "dgt_Source",
                    DataProviderId = incompatible ? Guid.NewGuid() : backingId,
                    OwnershipType = OwnershipTypes.OrganizationOwned
                });
            }

            return new RetrieveMetadataChangesResponse { Results = { ["EntityMetadata"] = metadata } };
        }));
    }
}
