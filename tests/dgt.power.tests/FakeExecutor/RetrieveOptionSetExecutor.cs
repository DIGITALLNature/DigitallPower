// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveOptionSetExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(RetrieveOptionSetRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var request = (RetrieveOptionSetRequest)organizationRequest;
        var name = request.Name;

        // Search metadata for a global option set matching the requested name
        foreach (var entityMetadata in fakeOrganizationService.State.EntityMetadata.Values)
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
