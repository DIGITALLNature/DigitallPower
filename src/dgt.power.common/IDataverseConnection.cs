// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common;

/// <summary>
/// Creates authenticated connections to Dataverse and manages their authentication state.
/// </summary>
public interface IDataverseConnection
{
    /// <summary>
    /// Creates a connection to the selected Dataverse environment.
    /// </summary>
    Task<IOrganizationServiceAsync2> ConnectAsync();

    /// <summary>
    /// Checks whether the current connection can acquire a token silently without opening a browser.
    /// Returns <c>true</c> if authentication is valid, <c>false</c> if interactive login is required.
    /// For ad-hoc connection strings (no token-based auth) this always returns <c>true</c>.
    /// For Azure DevOps Workload Identity Federation connections, this performs a real OIDC token
    /// exchange and can return <c>false</c> if it fails.
    /// Never opens a browser or prompts the user.
    /// </summary>
    Task<bool> CheckAuthAsync();

    /// <summary>
    /// Forces an interactive login for the active user connection and persists its authentication record.
    /// For connection types without user authentication this is a no-op.
    /// </summary>
    Task RefreshAuthAsync();
}
