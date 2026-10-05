// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
