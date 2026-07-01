// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveOptionSetExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(RetrieveOptionSetRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService state)
    {
        var request = (RetrieveOptionSetRequest)organizationRequest;
        var name = request.Name;

        // Search metadata for a global option set matching the requested name
        foreach (var entityMetadata in state.State.EntityMetadata.Values)
        {
            if (entityMetadata.Attributes == null) continue;
            foreach (var attribute in entityMetadata.Attributes)
            {
                if (attribute is PicklistAttributeMetadata { OptionSet: { IsGlobal: true, Name: not null } optionSet })
                {
                    if (optionSet.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        return new RetrieveOptionSetResponse
                        {
                            Results = new ParameterCollection
                            {
                                { nameof(RetrieveOptionSetResponse.OptionSetMetadata), optionSet }
                            }
                        };
                    }
                }
            }
        }

        throw new InvalidOperationException($"Global option set '{name}' not found in metadata.");
    }
}
