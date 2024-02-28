// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Identity.Client;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common.Logic;

internal class TokenConnector : IConnector
{
    private readonly TokenIdentity _identity;
    private readonly IProfileManager _profileManager;
    private readonly IPublicClientApplication _application;
    private          IAccount _account;
    private readonly string[] _scopes;
    private readonly Uri _uri;

    internal TokenConnector(TokenIdentity identity, IProfileManager profileManager)
    {
        _identity = identity;
        _profileManager = profileManager;
        _application = PublicClientApplicationBuilder
                    .Create("51f81489-12ee-4a9e-aaae-a2591f45987d")
                    .WithRedirectUri("http://localhost")
                    .Build();

        _application.UserTokenCache.SetBeforeAccess(BeforeAccessNotification);
        _application.UserTokenCache.SetAfterAccess(AfterAccessNotification);

        _uri = new Uri(identity.ConnectionString);
        _scopes = new [] { $"{_uri.Scheme}{Uri.SchemeDelimiter}{_uri.Authority}/.default" };
    }

    public IOrganizationServiceAsync2 CreateOrganizationServiceProxy()
    {
        if (!string.IsNullOrWhiteSpace(_identity.Username))
        {
            _account = _application.GetAccountAsync(_identity.Username).Result;
        }

        return new ServiceClient(_uri, GetTokenAsync);
    }

    public async Task<string> GetTokenAsync(string instanceUri)
    {

        try
        {
            var silent = await _application.AcquireTokenSilent(_scopes, _account)
                .ExecuteAsync();

            return silent.AccessToken;
        }
        catch (MsalUiRequiredException ex)
        {
            var interactive = await _application.AcquireTokenInteractive(_scopes)
                .WithClaims(ex.Claims)
                .ExecuteAsync();

            _identity.Username = interactive.Account.Username;
            _account = interactive.Account;
            _profileManager.Save();

            return interactive.AccessToken;
        }
    }

    void BeforeAccessNotification(TokenCacheNotificationArgs args)
    {
        args.TokenCache.DeserializeMsalV3(string.IsNullOrWhiteSpace(_identity.Token)? null: Convert.FromBase64String(_identity.Token));
    }

    void AfterAccessNotification(TokenCacheNotificationArgs args)
    {
        // if the access operation resulted in a cache update
        if (args.HasStateChanged)
        {
            // reflect changesgs in the persistent store
            _identity.Token = Convert.ToBase64String(args.TokenCache.SerializeMsalV3());
            _profileManager.Save();
        }
    }

}
