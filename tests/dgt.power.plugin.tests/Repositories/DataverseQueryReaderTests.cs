// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Repositories;

public class DataverseQueryReaderTests
{
    [Test]
    public async Task ReadAllAsync_MultiplePages_ForwardsCookieAndCollectsAllResults()
    {
        var service = new FakeOrganizationServiceAsync();
        var executor = new PagedQueryExecutor();
        service.AddRequests(executor);

        var result = await DataverseQueryReader.ReadAllAsync(service, new QueryExpression("plugintype"), CancellationToken.None);

        await Assert.That(result).Count().IsEqualTo(2);
        await Assert.That(executor.Requests).Count().IsEqualTo(2);
        await Assert.That(executor.Requests[0]).IsEqualTo((1, (string?)null, 5000));
        await Assert.That(executor.Requests[1]).IsEqualTo((2, (string?)"next-page", 5000));
    }

    [Test]
    public async Task ReadAllAsync_Cancelled_DoesNotQuery()
    {
        var service = new FakeOrganizationServiceAsync();
        var executor = new PagedQueryExecutor();
        service.AddRequests(executor);
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();
        var token = cancellation.Token;

        await Assert.That(async () => await DataverseQueryReader.ReadAllAsync(service, new QueryExpression("plugintype"), token))
            .Throws<OperationCanceledException>();
        await Assert.That(executor.Requests).Count().IsEqualTo(0);
    }
}
