// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Data.Common;
using Azure.Core;
using Azure.Identity;
using dgt.power.common.Connections;
using dgt.power.common.Exceptions;
using dgt.power.common.Logic;
using Microsoft.PowerPlatform.Dataverse.Client;
using Spectre.Console.Testing;

namespace dgt.power.common.tests.Logic;

public class ConnectorIsolationTests
{
    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task TokenCallback_AuthenticationRequired_PreservesNonInteractiveBehavior(bool nonInteractive)
    {
        var failure = new AuthenticationRequiredException("private-auth-details", new TokenRequestContext([]));
        var credential = new TestTokenCredential { Failure = failure };
        Func<string, Task<string>>? callback = null;
        var connector = new CredentialConnector(() => credential, SdkTestClient.EnvironmentUrl,
            nonInteractive, (_, tokenProvider, _) =>
            {
                callback = tokenProvider;
                return SdkTestClient.Create();
            });
        using var client = await connector.CreateServiceClientAsync();
        var attempt = callback!(SdkTestClient.EnvironmentUrl);

        if (nonInteractive)
        {
            await Assert.That(attempt).Throws<InteractiveLoginRequiredException>();
            await Assert.That(ReferenceEquals(attempt.Exception?.InnerException?.InnerException, failure)).IsTrue();
            await Assert.That(attempt.Exception?.InnerException?.Message).DoesNotContain("private-auth-details");
        }
        else
        {
            await Assert.That(attempt).Throws<AuthenticationRequiredException>();
            await Assert.That(ReferenceEquals(attempt.Exception?.InnerException, failure)).IsTrue();
        }
    }

    [Test]
    public async Task TokenCallbackPath_ForcesUniqueConnections_ReusesCredentialAndIsolatesSdkCallerState()
    {
        var credential = new TestTokenCredential();
        var credentialCreations = 0;
        var uniqueConnections = new List<bool>();
        var callbacks = new List<Func<string, Task<string>>>();
        var connector = new CredentialConnector(() =>
        {
            credentialCreations++;
            return credential;
        }, SdkTestClient.EnvironmentUrl, true, (_, tokenProvider, unique) =>
        {
            uniqueConnections.Add(unique);
            callbacks.Add(tokenProvider);
            return SdkTestClient.Create();
        });

        await VerifySdkCallerIsolation(connector);

        await Assert.That(uniqueConnections.Count).IsEqualTo(3);
        await Assert.That(uniqueConnections.All(unique => unique)).IsTrue();
        await Assert.That(credentialCreations).IsEqualTo(1);
        foreach (var callback in callbacks)
        {
            await Assert.That(await callback(SdkTestClient.EnvironmentUrl))
                .IsEqualTo("test-token-not-for-display");
        }
        await Assert.That(credential.Scopes.Count).IsEqualTo(3);
        await Assert.That(credential.Scopes.All(scope => scope == "https://example.crm.dynamics.com/.default")).IsTrue();
    }

    [Test]
    [Arguments("")]
    [Arguments(";RequireNewInstance=false")]
    [Arguments(";RequireNewInstance=true")]
    [Arguments(";RequireNewInstance=true;requirenewinstance=false")]
    public async Task ConnectionStringPath_OverridesReuseAndPreservesQuotedValues_WithIndependentSdkCallerState(
        string reuseSetting)
    {
        var createdStrings = new List<string>();
        var connector = new ConnectionStringConnector(
            "AuthType=ClientSecret;Url=https://example.crm.dynamics.com;ClientSecret=\"secret;with=delimiters\"" + reuseSetting,
            value =>
            {
                createdStrings.Add(value);
                return SdkTestClient.Create();
            });

        await VerifySdkCallerIsolation(connector);

        await Assert.That(createdStrings.Count).IsEqualTo(3);
        foreach (var value in createdStrings)
        {
            var parsed = new DbConnectionStringBuilder { ConnectionString = value };
            await Assert.That(bool.Parse((string)parsed["RequireNewInstance"])).IsTrue();
            await Assert.That((string)parsed["ClientSecret"]).IsEqualTo("secret;with=delimiters");
            await Assert.That((string)parsed["Url"]).IsEqualTo("https://example.crm.dynamics.com");
        }
    }

    private static async Task VerifySdkCallerIsolation(IDataverseConnector connector)
    {
        using var console = new TestConsole();
        using var connection = new DataverseConnection(Mock.Of<IConnectionStore>().Object,
            new CredentialFactory(Mock.Of<ISecretStore>().Object, console),
            new ConnectionInvocationContext(), console);
        var standard = await connector.CreateServiceClientAsync();
        SdkTestClient.SetField(connection, "_service", standard);
        SdkTestClient.SetField(connection, "_connector", connector);
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();
        var first = (ServiceClient)await connection.GetServiceAsync(firstId);
        var second = (ServiceClient)await connection.GetServiceAsync(secondId);

        await Assert.That(standard.IsReady && first.IsReady && second.IsReady).IsTrue();
        await Assert.That(standard.CallerId).IsEqualTo(Guid.Empty);
        await Assert.That(first.CallerId).IsEqualTo(firstId);
        await Assert.That(second.CallerId).IsEqualTo(secondId);
        first.CallerId = Guid.NewGuid();
        await Assert.That(standard.CallerId).IsEqualTo(Guid.Empty);
        await Assert.That(second.CallerId).IsEqualTo(secondId);
        standard.CallerId = Guid.NewGuid();
        await Assert.That(second.CallerId).IsEqualTo(secondId);
        await Assert.That(console.Output).DoesNotContain("Connected");
        await Assert.That(console.Output).DoesNotContain("test-token");
    }
}
