// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor;

public class RemoveSolutionComponentExecutor : IFakeMessageExecutor
{
    public bool CanExecute(OrganizationRequest request)
    {
        return request is RemoveSolutionComponentRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(RemoveSolutionComponentRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        //var typed = (RemoveSolutionComponentRequest)request;
        Thread.Sleep(2000);
        return new RemoveSolutionComponentResponse();
    }
}