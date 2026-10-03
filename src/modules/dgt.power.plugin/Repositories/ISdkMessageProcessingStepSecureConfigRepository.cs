// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin CRUD access to <c>sdkmessageprocessingstepsecureconfig</c> records.
/// </summary>
public interface ISdkMessageProcessingStepSecureConfigRepository
{
    /// <summary>
    /// Creates or updates the secure config for a step. Returns the secure config ID.
    /// </summary>
    Task<Guid> UpsertAsync(Guid stepId, string secureConfig, CancellationToken cancellationToken = default);
}
