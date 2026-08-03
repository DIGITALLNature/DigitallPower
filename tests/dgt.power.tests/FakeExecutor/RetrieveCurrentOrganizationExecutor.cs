// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Organization;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveCurrentOrganizationExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(RetrieveCurrentOrganizationRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var detail = new OrganizationDetail
        {
            EnvironmentId = "any-unique-name",
            Geo = "EMEA",
            OrganizationId = Guid.NewGuid(),
            OrganizationVersion = "9.1",
            TenantId = Guid.NewGuid().ToString("D"),
            UrlName = "url",
            State = OrganizationState.Enabled,
            FriendlyName = "XUnitOrg",
            UniqueName = "any-unique-name"
        };

        return new RetrieveCurrentOrganizationResponse
        {
            Results =
            {
                ["Detail"] = detail
            }
        };
    }
}
