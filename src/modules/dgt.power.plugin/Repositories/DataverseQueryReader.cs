// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Repositories;

internal static class DataverseQueryReader
{
    internal static async Task<IReadOnlyList<Entity>> ReadAllAsync(IOrganizationServiceAsync2 service, QueryExpression query, CancellationToken cancellationToken)
    {
        query.PageInfo = new PagingInfo { Count = 5000, PageNumber = 1 };
        var entities = new List<Entity>();
        EntityCollection page;
        do
        {
            cancellationToken.ThrowIfCancellationRequested();
            page = await service.RetrieveMultipleAsync(query, cancellationToken);
            entities.AddRange(page.Entities);
            query.PageInfo.PageNumber++;
            query.PageInfo.PagingCookie = page.PagingCookie;
        } while (page.MoreRecords);

        return entities;
    }
}
