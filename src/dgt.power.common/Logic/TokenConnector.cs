// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;
using Microsoft.Identity.Client;
using Microsoft.PowerPlatform.Dataverse.Client;
using Spectre.Console;

namespace dgt.power.common.Logic;

internal sealed class TokenConnector : IConnector
{
    private readonly TokenIdentity _identity;
    private readonly IProfileManager _profileManager;
    private readonly IPublicClientApplication _application;
    private          IAccount? _account;
    private readonly string[] _scopes;
    private readonly Uri _uri;
    private readonly bool _nonInteractive;
    private readonly IAnsiConsole _console;

    internal TokenConnector(TokenIdentity identity, IProfileManager profileManager, IAnsiConsole console, bool nonInteractive = false)
    {
        _identity = identity;
        _profileManager = profileManager;
        _console = console;
        _nonInteractive = nonInteractive;
        _application = PublicClientApplicationBuilder
                    .Create("51f81489-12ee-4a9e-aaae-a2591f45987d")
                    .WithRedirectUri("http://localhost")
                    .Build();

        _application.UserTokenCache.SetBeforeAccess(BeforeAccessNotification);
        _application.UserTokenCache.SetAfterAccess(AfterAccessNotification);

        _uri = new Uri(identity.ConnectionString);
        _scopes = [$"{_uri.Scheme}{Uri.SchemeDelimiter}{_uri.Authority}/.default"];
    }

    public async Task<IOrganizationServiceAsync2> CreateOrganizationServiceProxyAsync()
    {
        if (!string.IsNullOrWhiteSpace(_identity.Username))
        {
            _account = await _application.GetAccountAsync(_identity.Username);
        }

        return new ServiceClient(_uri, GetTokenAsync);
    }

    /// <summary>
    /// Attempts a silent-only token acquire. Returns <c>true</c> if a valid cached token exists,
    /// <c>false</c> if interactive login is required. Never opens a browser.
    /// </summary>
    internal async Task<bool> TryAcquireTokenSilentAsync()
    {
        if (!string.IsNullOrWhiteSpace(_identity.Username))
        {
            _account = await _application.GetAccountAsync(_identity.Username);
        }

        try
        {
            await _application.AcquireTokenSilent(_scopes, _account).ExecuteAsync();
            return true;
        }
        catch (MsalUiRequiredException)
        {
            return false;
        }
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
            if (_nonInteractive)
            {
                _console.MarkupLine("[red]AUTH_REQUIRED: Silent token acquisition failed and non-interactive mode is active.[/]");
                _console.MarkupLine("[red]             Ask the user to re-authenticate, then retry.[/]");
                throw new InteractiveLoginRequiredException(_uri.Authority, ex);
            }

            _console.MarkupLine("[yellow]AUTH: Silent token acquisition failed — opening browser for interactive login...[/]");
            var interactive = await _application.AcquireTokenInteractive(_scopes)
                .WithClaims(ex.Claims)
                .ExecuteAsync();

            _identity.Username = interactive.Account.HomeAccountId.Identifier;
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
            // reflect changes in the persistent store
            _identity.Token = Convert.ToBase64String(args.TokenCache.SerializeMsalV3());
            _profileManager.Save();
        }
    }

}
