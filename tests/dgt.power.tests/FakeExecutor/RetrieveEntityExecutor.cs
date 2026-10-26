// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveEntityExecutor(Dictionary<string, int> map) : IOrganizationRequestFake
{
    public RetrieveEntityExecutor() : this(new Dictionary<string, int>())
    {
    }

    public Type ForType => typeof(RetrieveEntityRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var typed = (RetrieveEntityRequest)organizationRequest;
        var entityMetadata = string.IsNullOrEmpty(typed.LogicalName)
            ? fakeOrganizationService.State.EntityMetadata.Values.First(entity => entity.MetadataId == typed.MetadataId)
            : fakeOrganizationService.State.EntityMetadata[typed.LogicalName];

        if (map.TryGetValue(entityMetadata.LogicalName, out var objectTypeCode))
        {
            entityMetadata.GetType().GetProperty("ObjectTypeCode")!.SetValue(entityMetadata, objectTypeCode, null);
        }

        return new RetrieveEntityResponse
        {
            Results =
            {
                ["EntityMetadata"] = entityMetadata
            }
        };
    }
}
