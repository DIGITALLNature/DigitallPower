// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor
{
    public class AddSolutionComponentExecutor : IFakeMessageExecutor
    {
        public Type GetResponsibleRequestType() => typeof(AddSolutionComponentRequest);

        public bool CanExecute(OrganizationRequest request) => request is AddSolutionComponentRequest;

        public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
        {
            var typed = (AddSolutionComponentRequest)request;


            return new AddSolutionComponentResponse();
        }

    }
}
