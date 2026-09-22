// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Local;
using dgt.power.webresource.Remote;

namespace dgt.power.webresource.Repositories;

public interface IWebResourceRepository
{
    Task<IReadOnlyList<RemoteWebResource>> FindByNamesAsync(
        IReadOnlyCollection<string> names,
        CancellationToken cancellationToken = default);

    Task<Guid> CreateAsync(LocalWebResource resource, CancellationToken cancellationToken = default);

    Task UpdateAsync(LocalWebResource resource, Guid id, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task PublishAsync(Guid id, CancellationToken cancellationToken = default);
}
