// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common.Connections;
using dgt.power.common.Exceptions;
using dgt.power.common.Logic;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Model;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;
using Spectre.Console.Testing;

namespace dgt.power.common.tests.Logic;

public class DataverseConnectionTests
{
    private const string Target = "test-environment";

    [Test]
    public async Task ConnectUsingAsync_Cancelled_DoesNotReportFailure()
    {
        using var console = new TestConsole();
        using var connection = CreateConnection(console);
        var connector = Mock.Of<IDataverseConnector>();
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();
        connector.CreateServiceClientAsync().Throws(new OperationCanceledException(cancellation.Token));

        await Assert.That(connection.ConnectUsingAsync(connector.Object, Target, "Interactive"))
            .Throws<OperationCanceledException>();

        await Assert.That(console.Output).DoesNotContain("failed");
        await Assert.That(console.Output).DoesNotContain("Connected to");
    }

    [Test]
    public async Task ConnectUsingAsync_AuthenticationFails_PreservesExceptionAndDoesNotPrintItsMessage()
    {
        using var console = new TestConsole();
        console.Profile.Width = 160;
        using var connection = CreateConnection(console);
        var connector = Mock.Of<IDataverseConnector>();
        var failure = new InteractiveLoginRequiredException("private-diagnostic-detail");
        connector.CreateServiceClientAsync().Throws(failure);
        var attempt = connection.ConnectUsingAsync(connector.Object, Target, "Device Code");

        await Assert.That(attempt).Throws<InteractiveLoginRequiredException>();

        await Assert.That(ReferenceEquals(attempt.Exception?.InnerException, failure)).IsTrue();
        await Assert.That(console.Output).Contains("Connection to test-environment (Device Code) failed");
        await Assert.That(console.Output).DoesNotContain("private-diagnostic-detail");
        await Assert.That(console.Output).DoesNotContain("Connected to");
    }

    [Test]
    public async Task ConnectUsingAsync_ClientNotReady_ReportsVerificationFailure()
    {
        using var console = new TestConsole();
        console.Profile.Width = 160;
        using var connection = CreateConnection(console);
        using var service = new ServiceClient(new ConnectionOptions(), deferConnection: true);
        var connector = Mock.Of<IDataverseConnector>();
        connector.CreateServiceClientAsync().Returns(service);

        await Assert.That(connection.ConnectUsingAsync(connector.Object, Target, "Client Secret"))
            .Throws<DataverseConnectionException>();

        await Assert.That(console.Output).Contains("Connection to test-environment (Client Secret) failed");
        await Assert.That(console.Output).DoesNotContain("Connected to");
    }

    private static DataverseConnection CreateConnection(TestConsole console) =>
        new(
            Mock.Of<IConnectionStore>().Object,
            new CredentialFactory(Mock.Of<ISecretStore>().Object, console),
            new ConnectionInvocationContext(),
            console);
}
