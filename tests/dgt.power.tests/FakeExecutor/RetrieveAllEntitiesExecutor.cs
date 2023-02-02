using dgt.power.dataverse;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveAllEntitiesExecutor : IFakeMessageExecutor
{
    public bool CanExecute(OrganizationRequest request)
    {
        return request is RetrieveAllEntitiesRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(RetrieveAllEntitiesRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        var knownMetadata = ctx.CreateMetadataQuery().ToArray();
        //var typed = ((RetrieveAllEntitiesRequest)request);
        var entityMetadata = ctx.GetEntityMetadataByName(TestEntity.EntityLogicalName);
        var response = new RetrieveAllEntitiesResponse
        {
            Results =
            {
                ["EntityMetadata"] = knownMetadata
            }
        };
        return response;
    }
}
