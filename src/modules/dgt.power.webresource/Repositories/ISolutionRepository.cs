// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Remote;

namespace dgt.power.webresource.Repositories;

public interface ISolutionRepository
{
    Task<IReadOnlyList<RemoteSolutionWebResource>> ListWebResourcesAsync(
        string solutionUniqueName,
        CancellationToken cancellationToken = default);

    Task AddWebResourceAsync(
        Guid webresourceId,
        string solutionUniqueName,
        CancellationToken cancellationToken = default);
}
