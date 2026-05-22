// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor;

public class WhoAmIExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(WhoAmIRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService state)
    {
        return new WhoAmIResponse
        {
            Results = {
                ["UserId"] = Guid.Parse("f4e8821a-97d2-4938-8b73-8744431e59c8")
            }
        };
    }
}