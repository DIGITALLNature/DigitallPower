// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Repositories;

internal sealed class PagedQueryExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(RetrieveMultipleRequest);
    internal List<(int Page, string? Cookie, int Count)> Requests { get; } = [];

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var query = (QueryExpression)((RetrieveMultipleRequest)organizationRequest).Query;
        Requests.Add((query.PageInfo.PageNumber, query.PageInfo.PagingCookie, query.PageInfo.Count));
        var page = new EntityCollection([new Entity(query.EntityName, Guid.NewGuid())])
        {
            MoreRecords = query.PageInfo.PageNumber == 1,
            PagingCookie = query.PageInfo.PageNumber == 1 ? "next-page" : null
        };
        return new RetrieveMultipleResponse { [nameof(RetrieveMultipleResponse.EntityCollection)] = page };
    }
}
