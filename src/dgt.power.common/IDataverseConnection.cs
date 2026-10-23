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
    /// Returns the service for the selected Dataverse environment. The first call connects and
    /// authenticates; later calls in the same process reuse the connected service.
    /// </summary>
    /// <remarks>
    /// Acquisition does not start a live console display, but authentication can require user
    /// interaction. Complete the first acquisition before starting a work status or progress display.
    /// </remarks>
    Task<IOrganizationServiceAsync2> GetServiceAsync();

    /// <summary>
    /// Returns an invocation-cached service acting as the specified Dataverse systemuser ID,
    /// not an Entra object ID. The selected environment and authentication remain unchanged.
    /// </summary>
    /// <remarks>
    /// Services are borrowed: the connection owns disposal. Consumers must not dispose them
    /// or mutate caller state. Acquisition guarantees a ready, caller-configured service,
    /// not caller existence, enabled status, or permission for subsequent operations.
    /// No caller-specific identity or privilege probe is performed.
    /// </remarks>
    /// <param name="callerId">A non-empty Dataverse systemuser ID.</param>
    /// <exception cref="ArgumentException">The caller ID is empty, checked before authentication.</exception>
    /// <exception cref="NotSupportedException">The implementation does not support caller services.</exception>
    Task<IOrganizationServiceAsync2> GetServiceAsync(Guid callerId);

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
