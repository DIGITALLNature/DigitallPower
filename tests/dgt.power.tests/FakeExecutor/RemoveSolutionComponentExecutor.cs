// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor;

public class RemoveSolutionComponentExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(RemoveSolutionComponentRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        Thread.Sleep(TestFixtures.FakeCallDurations);
        return new RemoveSolutionComponentResponse();
    }
}
