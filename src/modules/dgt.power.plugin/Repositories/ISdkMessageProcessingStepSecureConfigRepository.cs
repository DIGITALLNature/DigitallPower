// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Thin CRUD access to <c>sdkmessageprocessingstepsecureconfig</c> records.
/// </summary>
public interface ISdkMessageProcessingStepSecureConfigRepository
{
    /// <summary>
    /// Creates or updates the secure config for a step.
    /// </summary>
    // Retain the contract for planned constructor injection; commands currently construct repositories directly.
    // ReSharper disable once UnusedMemberInSuper.Global
    Task UpsertAsync(Guid stepId, string secureConfig, CancellationToken cancellationToken = default);
}
