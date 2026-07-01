// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common;

public interface IXrmConnection
{
    Task<IOrganizationServiceAsync2> ConnectAsync();

    /// <summary>
    /// Checks whether the current connection can acquire a token silently (no browser).
    /// Returns <c>true</c> if authentication is valid, <c>false</c> if interactive login is required.
    /// For non-MSAL connections this always returns <c>true</c>.
    /// Never opens a browser or prompts the user.
    /// </summary>
    Task<bool> CheckAuthAsync();

    /// <summary>
    /// Forces an interactive MSAL browser login for the active connection and persists the refreshed token.
    /// For non-MSAL connections this is a no-op.
    /// </summary>
    Task RefreshAuthAsync();
}
