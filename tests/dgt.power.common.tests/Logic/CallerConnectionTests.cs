// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.common.Exceptions;
using dgt.power.common.Logic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Model;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;
using Spectre.Console.Testing;

namespace dgt.power.common.tests.Logic;

public class CallerConnectionTests
{
    [Test]
    public async Task StandardVerification_ExecutesWhoAmIOnce_WhileCallerAcquisitionAddsNoRequestsOrFeedback()
    {
        using var console = new TestConsole();
        console.Profile.Width = 180;
        using var connection = CreateConnection(console);
        var standardTransport = new SdkTestTransport();
        var firstTransport = new SdkTestTransport();
        var secondTransport = new SdkTestTransport();
        var standard = SdkTestClient.Create(standardTransport);
        var first = SdkTestClient.Create(firstTransport);
        var second = SdkTestClient.Create(secondTransport);
        var connector = Mock.Of<IDataverseConnector>();
        var calls = 0;
        connector.CreateServiceClientAsync().Returns(() => ++calls == 1 ? standard : calls == 2 ? first : second);
        SdkTestClient.SetField(connection, "_connector", connector.Object);
        SdkTestClient.SetField(connection, "_selectedUrl", "https://example.crm.dynamics.com");
        SdkTestClient.SetField(connection, "_selectedType", "Interactive");

        await connection.GetServiceAsync(Guid.NewGuid());
        await connection.GetServiceAsync(Guid.NewGuid());

        await Assert.That(standardTransport.Requests).IsEqualTo(1);
        await Assert.That(firstTransport.Requests).IsEqualTo(0);
        await Assert.That(secondTransport.Requests).IsEqualTo(0);
        await Assert.That(console.Output.Split("Connected to").Length - 1).IsEqualTo(1);
        await Assert.That(console.Output).Contains("https://example.crm.dynamics.com (Interactive)");
        await first.ExecuteAsync(new WhoAmIRequest(), CancellationToken.None);
        await Assert.That(firstTransport.Requests).IsEqualTo(1);
        await Assert.That(standardTransport.Requests).IsEqualTo(1);
        await Assert.That(secondTransport.Requests).IsEqualTo(0);
    }

    [Test]
    public async Task ConcurrentPendingCaller_IsNotPublishedUntilReadyAndConfigured()
    {
        using var console = new TestConsole();
        using var connection = CreateConnection(console);
        var standard = SdkTestClient.Create();
        var caller = SdkTestClient.Create();
        var pending = new TaskCompletionSource<ServiceClient>(TaskCreationOptions.RunContinuationsAsynchronously);
        var connector = new DeferredDataverseConnector(pending.Task);
        Seed(connection, standard, connector);
        var callerId = Guid.NewGuid();
        var first = connection.GetServiceAsync(callerId);
        var second = connection.GetServiceAsync(callerId);

        await Assert.That(first.IsCompleted).IsFalse();
        await Assert.That(second.IsCompleted).IsFalse();
        await Assert.That(connector.Calls).IsEqualTo(1);
        await Assert.That(caller.CallerId).IsEqualTo(Guid.Empty);
        pending.SetResult(caller);
        var acquired = await Task.WhenAll(first, second);

        await Assert.That(acquired.All(service => ReferenceEquals(service, caller))).IsTrue();
        await Assert.That(caller.CallerId).IsEqualTo(callerId);
        await Assert.That(connector.Calls).IsEqualTo(1);
        await Assert.That(standard.CallerId).IsEqualTo(Guid.Empty);
    }

    [Test]
    public async Task EmptyCaller_RejectsBeforeSelectionOrAuthentication()
    {
        using var console = new TestConsole();
        using var connection = CreateConnection(console);

        await Assert.That(connection.GetServiceAsync(Guid.Empty)).Throws<ArgumentException>();
        await Assert.That(console.Output).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Callers_ReuseAndConcurrentAcquisition_PreserveActualSdkCallerState()
    {
        using var console = new TestConsole();
        using var connection = CreateConnection(console);
        var standard = SdkTestClient.Create();
        var first = SdkTestClient.Create();
        var second = SdkTestClient.Create();
        var connector = Mock.Of<IDataverseConnector>();
        var calls = 0;
        connector.CreateServiceClientAsync().Returns(() => ++calls == 1 ? first : second);
        Seed(connection, standard, connector.Object);
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();

        var acquisitions = await Task.WhenAll(
            Enumerable.Range(0, 20).Select(_ => connection.GetServiceAsync(firstId)));
        var other = await connection.GetServiceAsync(secondId);

        await Assert.That(acquisitions.All(service => ReferenceEquals(service, first))).IsTrue();
        await Assert.That(ReferenceEquals(other, second)).IsTrue();
        await Assert.That(calls).IsEqualTo(2);
        await Assert.That(ReferenceEquals(await connection.GetServiceAsync(), standard)).IsTrue();
        await Assert.That(standard.CallerId).IsEqualTo(Guid.Empty);
        await Assert.That(first.CallerId).IsEqualTo(firstId);
        await Assert.That(second.CallerId).IsEqualTo(secondId);
        first.CallerId = Guid.NewGuid();
        await Assert.That(standard.CallerId).IsEqualTo(Guid.Empty);
        await Assert.That(second.CallerId).IsEqualTo(secondId);
        await Assert.That(console.Output).IsEqualTo(string.Empty);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task FailedInitializationOrConfiguration_DisposesUnfinishedClientAndRetries(bool configurationFailure)
    {
        using var console = new TestConsole();
        using var connection = CreateConnection(console);
        var standard = SdkTestClient.Create();
        var unfinished = new ServiceClient(new ConnectionOptions(), deferConnection: true);
        if (configurationFailure)
        {
            typeof(ServiceClient).GetProperty(nameof(ServiceClient.IsReady))!.SetValue(unfinished, true);
        }
        var successful = SdkTestClient.Create();
        var connector = Mock.Of<IDataverseConnector>();
        var calls = 0;
        connector.CreateServiceClientAsync().Returns(() => ++calls == 1 ? unfinished : successful);
        Seed(connection, standard, connector.Object);
        var callerId = Guid.NewGuid();

        await Assert.That(connection.GetServiceAsync(callerId)).Throws<DataverseConnectionException>();
        await Assert.That(unfinished.IsReady).IsFalse();
        await Assert.That(ReferenceEquals(await connection.GetServiceAsync(callerId), successful)).IsTrue();
        await Assert.That(successful.CallerId).IsEqualTo(callerId);
        await Assert.That(ReferenceEquals(await connection.GetServiceAsync(callerId), successful)).IsTrue();
        await Assert.That(calls).IsEqualTo(2);
        await Assert.That(standard.CallerId).IsEqualTo(Guid.Empty);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task AuthenticationFailureOrCancellation_PropagatesOriginalAndAllowsRetry(bool cancelled)
    {
        using var console = new TestConsole();
        using var connection = CreateConnection(console);
        var standard = SdkTestClient.Create();
        var successful = SdkTestClient.Create();
        Exception failure = cancelled
            ? new OperationCanceledException()
            : new InteractiveLoginRequiredException("private-auth-diagnostic");
        var connector = Mock.Of<IDataverseConnector>();
        var calls = 0;
        connector.CreateServiceClientAsync().Returns(() =>
        {
            if (++calls == 1)
            {
                throw failure;
            }
            return successful;
        });
        Seed(connection, standard, connector.Object);
        var callerId = Guid.NewGuid();
        Exception? actual = null;
        try
        {
            await connection.GetServiceAsync(callerId);
        }
        catch (Exception exception)
        {
            actual = exception;
        }
        await Assert.That(ReferenceEquals(actual, failure)).IsTrue();
        await Assert.That(ReferenceEquals(await connection.GetServiceAsync(callerId), successful)).IsTrue();
        await Assert.That(calls).IsEqualTo(2);
        await Assert.That(console.Output).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ResolverDisposal_DisposesStandardAndEveryCaller()
    {
        using var console = new TestConsole();
        var connection = CreateConnection(console);
        var standard = SdkTestClient.Create();
        var first = SdkTestClient.Create();
        var second = SdkTestClient.Create();
        var connector = Mock.Of<IDataverseConnector>();
        var calls = 0;
        connector.CreateServiceClientAsync().Returns(() => ++calls == 1 ? first : second);
        Seed(connection, standard, connector.Object);
        var services = new ServiceCollection();
        services.AddSingleton<IDataverseConnection>(_ => connection);
        var resolver = (TypeResolver)new TypeRegistrar(services).Build();
        var resolved = (IDataverseConnection)resolver.Resolve(typeof(IDataverseConnection))!;
        await resolved.GetServiceAsync(Guid.NewGuid());
        await resolved.GetServiceAsync(Guid.NewGuid());

        resolver.Dispose();

        await Assert.That(standard.IsReady).IsFalse();
        await Assert.That(first.IsReady).IsFalse();
        await Assert.That(second.IsReady).IsFalse();
        connection.Dispose();
    }

    private static DataverseConnection CreateConnection(TestConsole console) =>
        new(Mock.Of<IConnectionStore>().Object,
            new CredentialFactory(Mock.Of<ISecretStore>().Object, console),
            new ConnectionInvocationContext(), console);

    private static void Seed(DataverseConnection connection, ServiceClient standard, IDataverseConnector connector)
    {
        SdkTestClient.SetField(connection, "_service", standard);
        SdkTestClient.SetField(connection, "_connector", connector);
    }
}
