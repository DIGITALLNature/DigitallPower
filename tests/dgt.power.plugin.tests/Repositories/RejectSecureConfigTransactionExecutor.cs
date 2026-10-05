// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace dgt.power.plugin.tests.Repositories;

public sealed class RejectSecureConfigTransactionExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(ExecuteTransactionRequest);

    public ExecuteTransactionRequest? Request { get; private set; }

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        Request = (ExecuteTransactionRequest)organizationRequest;
        throw new InvalidOperationException("Transaction rejected.");
    }
}
