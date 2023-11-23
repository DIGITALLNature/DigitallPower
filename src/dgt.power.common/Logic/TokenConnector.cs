// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Identity.Client;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Logic;

internal class TokenConnector : IConnector
{
    private readonly TokenIdentity _identity;
    private readonly IProfileManager _profileManager;
    private readonly IPublicClientApplication application;
    private             IAccount account;
    private         string[] scopes;
    private         Uri uri;

    public TokenConnector(TokenIdentity identity, IProfileManager profileManager)
    {
        _identity = identity;
        _profileManager = profileManager;
        application = PublicClientApplicationBuilder
                    .Create("51f81489-12ee-4a9e-aaae-a2591f45987d")
                    .WithRedirectUri("http://localhost")
                    .Build();

        application.UserTokenCache.SetBeforeAccess(BeforeAccessNotification);
        application.UserTokenCache.SetAfterAccess(AfterAccessNotification);

        uri = new Uri(identity.ConnectionString);
        scopes = new [] { $"{uri.Scheme}{Uri.SchemeDelimiter}{uri.Authority}/.default" };
    }

    public IOrganizationService GetOrganizationServiceProxy()
    {
        if (!string.IsNullOrWhiteSpace(_identity.Username))
        {
            account = application.GetAccountAsync(_identity.Username).Result;
        }

        return new ServiceClient(uri, GetToken);
    }

    public async Task<string> GetToken(string instanceUri)
    {

        try
        {
            var silent = await application.AcquireTokenSilent(scopes, account)
                .ExecuteAsync();

            return silent.AccessToken;
        }
        catch (MsalUiRequiredException ex)
        {
            var interactive = await application.AcquireTokenInteractive(scopes)
                .WithClaims(ex.Claims)
                .ExecuteAsync();

            _identity.Username = interactive.Account.Username;
            account = interactive.Account;
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
