// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Logic;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Model;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;

namespace dgt.power.common.tests.Logic;

public class DataverseConnectionVerifierTests
{
    [Test]
    public async Task VerifyAsync_ClientNotReady_ThrowsConnectionException()
    {
        using var service = new ServiceClient(new ConnectionOptions(), deferConnection: true);

        await Assert.That(DataverseConnectionVerifier.VerifyAsync(service, CancellationToken.None))
            .Throws<DataverseConnectionException>()
            .WithMessageContaining("Dataverse connection failed:");
    }

    [Test]
    public async Task VerifyAsync_Cancelled_PropagatesCancellationBeforeCheckingReadiness()
    {
        using var service = new ServiceClient(new ConnectionOptions(), deferConnection: true);
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        await Assert.That(DataverseConnectionVerifier.VerifyAsync(service, cancellation.Token))
            .Throws<OperationCanceledException>();
    }
}
