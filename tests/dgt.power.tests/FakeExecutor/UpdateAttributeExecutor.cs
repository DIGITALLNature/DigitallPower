using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace dgt.power.tests.FakeExecutor;

public class UpdateAttributeExecutor : IFakeMessageExecutor
{
    public bool CanExecute(OrganizationRequest request)
    {
        return request is UpdateAttributeRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(UpdateAttributeRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        var typed = (UpdateAttributeRequest)request;
        //var meta = typed.Attribute;
        return new UpdateAttributeResponse();
    }
}