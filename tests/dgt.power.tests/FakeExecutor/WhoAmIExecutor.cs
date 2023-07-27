// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor;

public class WhoAmIExecutor : IFakeMessageExecutor
{
    public bool CanExecute(OrganizationRequest request)
    {
        return request is WhoAmIRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(WhoAmIRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        var typed = (WhoAmIRequest)request;
        //var meta = typed.Attribute;
        var response = new WhoAmIResponse
        {
            Results = {
                ["UserId"] = Guid.Parse("f4e8821a-97d2-4938-8b73-8744431e59c8")
            }
        };
        return response;
    }
}