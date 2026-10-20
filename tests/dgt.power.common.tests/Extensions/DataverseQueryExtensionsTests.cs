// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Extensions;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.common.tests.Extensions;

public class DataverseQueryExtensionsTests
{
    [Test]
    public async Task RetrieveAllAsync_MultiplePages_UsesPageSizeAndCookieAndCollectsResults()
    {
        var service = new FakeOrganizationServiceAsync();
        var executor = new PagingExecutor(2);
        service.AddRequests(executor);
        var query = new QueryExpression("account")
        {
            PageInfo = new PagingInfo { Count = 7, PageNumber = 4, PagingCookie = "old-cookie" }
        };

        var result = await service.RetrieveAllAsync(query, 25);

        await Assert.That(result).Count().IsEqualTo(2);
        await Assert.That(executor.Requests).Count().IsEqualTo(2);
        await Assert.That(executor.Requests[0]).IsEqualTo((1, (string?)null, 25));
        await Assert.That(executor.Requests[1]).IsEqualTo((2, (string?)"next-page", 25));
    }

    [Test]
    public async Task RetrieveAllAsync_CancelledBeforeFirstPage_DoesNotQuery()
    {
        using var cancellation = new CancellationTokenSource();
        var token = cancellation.Token;
        var service = new FakeOrganizationServiceAsync();
        var executor = new PagingExecutor(2);
        service.AddRequests(executor);
        await cancellation.CancelAsync();

        await Assert.That(async () =>
                await service.RetrieveAllAsync(new QueryExpression("account"), cancellationToken: token))
            .Throws<OperationCanceledException>();
        await Assert.That(executor.Requests).Count().IsEqualTo(0);
    }

    [Test]
    public async Task RetrieveAllAsync_NonPositivePageSize_ThrowsWithoutQuery()
    {
        var service = new FakeOrganizationServiceAsync();
        var executor = new PagingExecutor(2);
        service.AddRequests(executor);

        await Assert.That(async () => await service.RetrieveAllAsync(new QueryExpression("account"), 0))
            .Throws<ArgumentOutOfRangeException>();
        await Assert.That(executor.Requests).Count().IsEqualTo(0);
    }

    [Test]
    public async Task RetrieveAllAsync_CancelledBetweenPages_DoesNotQueryNextPage()
    {
        using var cancellation = new CancellationTokenSource();
        var token = cancellation.Token;
        var service = new FakeOrganizationServiceAsync();
        var executor = new PagingExecutor(2, page =>
        {
            if (page == 1)
            {
                // ReSharper disable once AccessToDisposedClosure -- invoked during the awaited query, before disposal
                cancellation.Cancel();
            }
        });
        service.AddRequests(executor);

        await Assert.That(async () =>
                await service.RetrieveAllAsync(new QueryExpression("account"), cancellationToken: token))
            .Throws<OperationCanceledException>();
        await Assert.That(executor.Requests).Count().IsEqualTo(1);
    }

    private sealed class PagingExecutor(int pageCount, Action<int>? onPageRetrieved = null) : IOrganizationRequestFake
    {
        public Type ForType => typeof(RetrieveMultipleRequest);

        public List<(int Page, string? Cookie, int Count)> Requests { get; } = [];

        public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
        {
            var query = (QueryExpression)((RetrieveMultipleRequest)organizationRequest).Query;
            var pageNumber = query.PageInfo.PageNumber;
            Requests.Add((pageNumber, query.PageInfo.PagingCookie, query.PageInfo.Count));
            onPageRetrieved?.Invoke(pageNumber);

            var page = new EntityCollection([new Entity(query.EntityName, Guid.NewGuid())])
            {
                MoreRecords = pageNumber < pageCount,
                PagingCookie = pageNumber < pageCount ? "next-page" : null
            };
            return new RetrieveMultipleResponse { [nameof(RetrieveMultipleResponse.EntityCollection)] = page };
        }
    }
}
