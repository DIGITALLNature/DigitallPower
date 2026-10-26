// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveAllEntitiesExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(RetrieveAllEntitiesRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var knownMetadata = fakeOrganizationService.State.EntityMetadata.Values.ToArray();
        return new RetrieveAllEntitiesResponse
        {
            Results =
            {
                ["EntityMetadata"] = knownMetadata
            }
        };
    }
}
