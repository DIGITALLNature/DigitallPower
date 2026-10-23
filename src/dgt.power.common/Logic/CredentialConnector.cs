// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Azure.Core;
using Azure.Identity;
using dgt.power.common.Connections;
using dgt.power.common.Exceptions;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common.Logic;

internal sealed class CredentialConnector : IDataverseConnector
{
    private readonly Func<TokenCredential> _createCredential;
    private readonly Func<Uri, Func<string, Task<string>>, bool, ServiceClient> _createClient;
    private readonly string _url;
    private readonly bool _nonInteractive;
    private TokenCredential? _credential;

    internal CredentialConnector(CredentialFactory credentialFactory, ResolvedConnection connection,
        ConnectionInvocationContext invocationContext)
        : this(CreateCredentialFactory(credentialFactory, connection,
                invocationContext.NonInteractive, invocationContext.AllowUnencryptedStorage),
            connection.Definition.Url, invocationContext.NonInteractive,
            static (uri, tokenProvider, unique) => new ServiceClient(uri, tokenProvider, useUniqueInstance: unique))
    {
    }

    internal CredentialConnector(Func<TokenCredential> createCredential, string url, bool nonInteractive,
        Func<Uri, Func<string, Task<string>>, bool, ServiceClient> createClient)
    {
        _createCredential = createCredential;
        _url = url;
        _nonInteractive = nonInteractive;
        _createClient = createClient;
    }

    private static Func<TokenCredential> CreateCredentialFactory(CredentialFactory credentialFactory,
        ResolvedConnection connection, bool nonInteractive, bool allowUnencryptedStorage) =>
        () => credentialFactory.Create(connection.Name, connection.Definition, nonInteractive, allowUnencryptedStorage);

    public Task<ServiceClient> CreateServiceClientAsync()
    {
        var credential = _credential ??= _createCredential();
        var scope = new TokenRequestContext([GetScope(_url)]);
        return Task.FromResult(_createClient(new Uri(_url),
            async _ => await GetTokenAsync(credential, scope), true));
    }

    private async Task<string> GetTokenAsync(TokenCredential credential, TokenRequestContext context)
    {
        try
        {
            return (await credential.GetTokenAsync(context, CancellationToken.None)).Token;
        }
        catch (AuthenticationRequiredException exception) when (_nonInteractive)
        {
            throw new InteractiveLoginRequiredException(new Uri(_url).Authority, exception);
        }
    }

    public static string GetScope(string url)
    {
        var uri = new Uri(url);
        return $"{uri.GetLeftPart(UriPartial.Authority)}/.default";
    }
}
