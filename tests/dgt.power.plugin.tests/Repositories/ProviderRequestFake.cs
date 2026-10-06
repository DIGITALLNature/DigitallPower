// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;

namespace dgt.power.plugin.tests.Repositories;

internal sealed class ProviderRequestFake(Type requestType, Func<OrganizationRequest, OrganizationResponse> execute) : IOrganizationRequestFake
{
    public Type ForType => requestType;
    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService) => execute(organizationRequest);
}
