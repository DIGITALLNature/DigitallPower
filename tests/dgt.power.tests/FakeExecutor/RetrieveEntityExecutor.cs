// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveEntityExecutor : IOrganizationRequestFake
{
    private readonly Dictionary<string, int> _map;

    public RetrieveEntityExecutor()
    {
        _map = new Dictionary<string, int>();
    }
    public RetrieveEntityExecutor(Dictionary<string, int> map)
    {
        _map = map;
    }

    public Type ForType => typeof(RetrieveEntityRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService state)
    {
        var typed = (RetrieveEntityRequest)organizationRequest;
        var entityMetadata = state.State.EntityMetadata[TestEntity.EntityLogicalName];
        entityMetadata.GetType().GetProperty("ObjectTypeCode")!.SetValue(entityMetadata, _map[typed.LogicalName], null);
        return new RetrieveEntityResponse
        {
            Results =
            {
                ["EntityMetadata"] = entityMetadata
            }
        };
    }
}
