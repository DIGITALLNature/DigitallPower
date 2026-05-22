// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveAttributeExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(RetrieveAttributeRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService state)
    {
        var request = (RetrieveAttributeRequest)organizationRequest;

        var attributeMetadata = new StringAttributeMetadata(request.LogicalName)
        {
            AutoNumberFormat = string.Empty
        };

        return new RetrieveAttributeResponse
        {
            Results = new ParameterCollection
            {
                { nameof(RetrieveAttributeResponse.AttributeMetadata), attributeMetadata }
            }
        };
    }
}
