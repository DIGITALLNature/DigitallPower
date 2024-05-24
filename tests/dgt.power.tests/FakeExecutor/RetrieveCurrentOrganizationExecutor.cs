// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Organization;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveCurrentOrganizationExecutor : IFakeMessageExecutor
{
    public bool CanExecute(OrganizationRequest request)
    {
        return request is RetrieveCurrentOrganizationRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(RetrieveCurrentOrganizationRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        var typed = (RetrieveCurrentOrganizationRequest)request;

        var detail = new OrganizationDetail
        {
            Endpoints = { },
            EnvironmentId = "any-unique-name",
            Geo = "EMEA",
            OrganizationId = Guid.NewGuid(),
            OrganizationVersion = "9.1",
            TenantId = Guid.NewGuid().ToString("D"),
            UrlName = "url",
            State = OrganizationState.Enabled,
            FriendlyName = "XUnitOrg",
            UniqueName = "any-unique-name"
        };

        var response = new RetrieveCurrentOrganizationResponse
        {
            Results =
            {
                ["Detail"] = detail
            }
        };

        return response;
    }
}