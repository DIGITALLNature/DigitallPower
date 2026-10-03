// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin CRUD access to <c>sdkmessageprocessingstepsecureconfig</c> records.
/// </summary>
public interface ISdkMessageProcessingStepSecureConfigRepository
{
    /// <summary>Well-known solution component type code for <c>sdkmessageprocessingstepsecureconfig</c>.</summary>
    public const int ComponentType = 93;

    /// <summary>
    /// Gets the secure config id for a given step, or null if none exists.
    /// </summary>
    Task<Guid?> GetIdByStepIdAsync(Guid stepId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new secure config record, links it to the step, and returns the new id.
    /// </summary>
    Task<Guid> CreateAsync(Guid stepId, string secureConfig, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing secure config record.
    /// </summary>
    Task UpdateAsync(Guid secureConfigId, string secureConfig, CancellationToken cancellationToken = default);
}
