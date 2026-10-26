// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.documenttemplates.Logic;

public static class DocumentTemplateReader
{
    public static Task<IReadOnlyList<Entity>> ReadAllAsync(IOrganizationServiceAsync2 service,
        QueryExpression query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(query);
        return ReadPagesAsync(service, query, cancellationToken);
    }

    private static async Task<IReadOnlyList<Entity>> ReadPagesAsync(IOrganizationServiceAsync2 service,
        QueryExpression query, CancellationToken cancellationToken)
    {
        var entities = new List<Entity>();
        query.PageInfo = new PagingInfo { Count = 5000, PageNumber = 1 };
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var page = await service.RetrieveMultipleAsync(query, cancellationToken);
            entities.AddRange(page.Entities);
            if (!page.MoreRecords)
                return entities;
            query.PageInfo.PageNumber++;
            query.PageInfo.PagingCookie = page.PagingCookie;
        }
    }

    public static QueryExpression Query(bool content) => new("documenttemplate")
    {
        ColumnSet = content
            ? new ColumnSet("name", "documenttype", "associatedentitytypecode", "languagecode", "description", "status", "content")
            : new ColumnSet("name", "documenttype", "associatedentitytypecode", "status"),
        Orders = { new OrderExpression("documenttemplateid", OrderType.Ascending) }
    };
}
