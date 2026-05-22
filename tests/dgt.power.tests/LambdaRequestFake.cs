// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests;

/// <summary>
/// An <see cref="IOrganizationRequestFake"/> that wraps a lambda for inline execution mocks.
/// </summary>
internal class LambdaRequestFake<TRequest> : IOrganizationRequestFake
    where TRequest : OrganizationRequest
{
    private readonly Func<OrganizationRequest, OrganizationResponse> _handler;

    public LambdaRequestFake(Func<OrganizationRequest, OrganizationResponse> handler)
    {
        _handler = handler;
    }

    public Type ForType => typeof(TRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService state)
    {
        return _handler(organizationRequest);
    }
}
