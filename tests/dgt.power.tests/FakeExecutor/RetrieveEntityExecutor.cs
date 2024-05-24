// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveEntityExecutor : IFakeMessageExecutor
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

    public bool CanExecute(OrganizationRequest request)
    {
        return request is RetrieveEntityRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(PublishDuplicateRuleRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        var typed = (RetrieveEntityRequest)request;
        var entityMetadata = ctx.GetEntityMetadataByName(TestEntity.EntityLogicalName);
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
