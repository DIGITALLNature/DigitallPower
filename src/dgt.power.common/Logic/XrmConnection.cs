// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Spectre.Console;

namespace dgt.power.common.Logic;

public class XrmConnection(IProfileManager profileManager, IConfiguration configuration, IAnsiConsole console)
    : IXrmConnection
{
    public async Task<IOrganizationServiceAsync2> ConnectAsync()
    {
        var xrmConfiguration = configuration.GetSection("xrm").GetChildren().ToList();

        var profile = configuration.GetValue<string>("profile");

        if (xrmConfiguration.Count != 0)
        {
            return await ConnectWithConfigurationAsync();
        }

        if (!string.IsNullOrEmpty(profile))
        {
            return await ConnectWithProfileNameAsync(profile);
        }

        if (profileManager.CurrentIdentity != null)
        {
            return await ConnectWithProfileAsync(profileManager.CurrentIdentity);
        }

        throw new MissingConnectionException();
    }

    /// <inheritdoc/>
    public async Task<bool> CheckAuthAsync()
    {
        if (profileManager.CurrentIdentity is not TokenIdentity tokenIdentity)
        {
            // Connection-string profiles do not use MSAL — no interactive login required.
            return true;
        }

        var connector = new TokenConnector(tokenIdentity, profileManager, console);
        return await connector.TryAcquireTokenSilentAsync();
    }

    /// <inheritdoc/>
    public async Task RefreshAuthAsync()
    {
        if (profileManager.CurrentIdentity is not TokenIdentity tokenIdentity)
        {
            // Connection-string profiles do not use MSAL — nothing to refresh.
            return;
        }

        var connector = new TokenConnector(tokenIdentity, profileManager, console);
        await connector.ForceInteractiveLoginAsync();
    }

    private async Task<IOrganizationServiceAsync2> ConnectWithConfigurationAsync()
    {
        console.MarkupLine("Connect to given configuration.");
        var connector = new CrmConnector(configuration.GetValue<string>("xrm:connection")!, console);
        try
        {
            return await connector.CreateOrganizationServiceProxyAsync();
        }
#pragma warning disable CA1031 // Intentional: any connector exception is wrapped into a domain exception
        catch (Exception e)
        {
            throw new FailedConnectionException("xrm:connection", e);
        }
#pragma warning restore CA1031
    }

    public async Task<IOrganizationServiceAsync2> ConnectWithProfileNameAsync(string profileName)
    {
        ArgumentNullException.ThrowIfNull(profileName);
        var identities = profileManager.LoadIdentities();
        if (!identities.Contains(profileName.ToUpperInvariant()))
        {
            throw new MissingConnectionException($"Profile {profileName} not found!");
        }

        identities.SetCurrent(profileName.ToUpperInvariant());
        return await ConnectWithProfileAsync(profileManager.CurrentIdentity!);
    }

    private async Task<IOrganizationServiceAsync2> ConnectWithProfileAsync(Identity identity)
    {
        IConnector connector;
        if (profileManager.CurrentIdentity is TokenIdentity tokenIdentity)
        {
            console.MarkupLine($"Connect to {profileManager.Current} via MSAL connection");
            connector = new TokenConnector(tokenIdentity, profileManager, console, nonInteractive: IsNonInteractive());
        }
        else
        {
            console.MarkupLine($"Connect to {profileManager.Current} via connection string");
            connector = new CrmConnector(identity.ConnectionString, console);
        }

        try
        {
            var service = await connector.CreateOrganizationServiceProxyAsync();
            await CheckWhoAmIAsync(service);
            return service;
        }
#pragma warning disable CA1031 // Intentional: any connector exception is wrapped into a domain exception
        catch (Exception exception)
        {
            throw new FailedConnectionException(profileManager.Current, exception);
        }
#pragma warning restore CA1031
    }

    private async Task CheckWhoAmIAsync(IOrganizationServiceAsync2 service)
    {
        var userId = ((WhoAmIResponse)await service.ExecuteAsync(new WhoAmIRequest())).UserId;
        console.MarkupLine($"WhoAmI: [bold]{userId:D}[/]");
    }

    /// <summary>
    /// Returns <c>true</c> when the caller has requested non-interactive mode via either:
    /// <list type="bullet">
    ///   <item><description><c>--non-interactive true</c> CLI flag (passed through IConfiguration)</description></item>
    ///   <item><description><c>DGTP_NON_INTERACTIVE=true</c> environment variable</description></item>
    /// </list>
    /// </summary>
    private bool IsNonInteractive()
    {
        if (configuration.GetValue<bool>("non-interactive"))
        {
            return true;
        }

        var envVar = Environment.GetEnvironmentVariable("DGTP_NON_INTERACTIVE");
        return string.Equals(envVar, "true", StringComparison.OrdinalIgnoreCase)
               || envVar == "1";
    }
}
