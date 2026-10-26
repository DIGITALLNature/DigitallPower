// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.webresource.Remote;

namespace dgt.power.webresource.Repositories;

public interface ISolutionRepository
{
    // Retain the contract for planned constructor injection; commands currently construct repositories directly.
    // ReSharper disable once UnusedMemberInSuper.Global
    Task<IReadOnlyList<RemoteSolutionWebResource>> ListWebResourcesAsync(
        string solutionUniqueName,
        CancellationToken cancellationToken = default);

    // Retain the contract for planned constructor injection; commands currently construct repositories directly.
    // ReSharper disable once UnusedMemberInSuper.Global
    Task AddWebResourceAsync(
        Guid webresourceId,
        string solutionUniqueName,
        CancellationToken cancellationToken = default);
}
