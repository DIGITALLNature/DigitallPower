// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Diagnostics.CodeAnalysis;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.common.Extensions;

// The C# 14 extension block is compiled into a nested type, which CA1034 does not recognise.
[SuppressMessage("Design", "CA1034", Justification = "Compiler-generated type for the C# 14 extension block")]
public static class DataverseQueryExtensions
{
    extension(IOrganizationServiceAsync2 service)
    {
        /// <summary>
        /// Retrieves every page of a query, following Dataverse paging cookies as needed.
        /// This method replaces the query's existing <see cref="QueryExpression.PageInfo"/> and owns paging.
        /// </summary>
        public Task<IReadOnlyList<Entity>> RetrieveAllAsync(
            QueryExpression query,
            int pageSize = 5000,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(service);
            ArgumentNullException.ThrowIfNull(query);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);

            return service.RetrieveAllCoreAsync(query, pageSize, cancellationToken);
        }

        private async Task<IReadOnlyList<Entity>> RetrieveAllCoreAsync(
            QueryExpression query,
            int pageSize,
            CancellationToken cancellationToken)
        {
            query.PageInfo = new PagingInfo { Count = pageSize, PageNumber = 1 };

            var entities = new List<Entity>();
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var page = await service.RetrieveMultipleAsync(query, cancellationToken);
                entities.AddRange(page.Entities);

                if (!page.MoreRecords)
                {
                    return entities;
                }

                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = page.PagingCookie;
            }
        }
    }
}
