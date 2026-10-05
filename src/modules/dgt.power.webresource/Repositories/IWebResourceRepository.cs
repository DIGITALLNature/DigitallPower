// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Local;
using dgt.power.webresource.Remote;

namespace dgt.power.webresource.Repositories;

public interface IWebResourceRepository
{
    // Retain the contract for planned constructor injection; commands currently construct repositories directly.
    // ReSharper disable once UnusedMemberInSuper.Global
    Task<IReadOnlyList<RemoteWebResource>> FindByNamesAsync(
        IReadOnlyCollection<string> names,
        CancellationToken cancellationToken = default);

    // Retain the contract for planned constructor injection; commands currently construct repositories directly.
    // ReSharper disable once UnusedMemberInSuper.Global
    Task<Guid> CreateAsync(LocalWebResource resource, CancellationToken cancellationToken = default);

    // Retain the contract for planned constructor injection; commands currently construct repositories directly.
    // ReSharper disable once UnusedMemberInSuper.Global
    Task UpdateAsync(LocalWebResource resource, Guid id, CancellationToken cancellationToken = default);

    // Retain the contract for planned constructor injection; commands currently construct repositories directly.
    // ReSharper disable once UnusedMemberInSuper.Global
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    // Retain the contract for planned constructor injection; commands currently construct repositories directly.
    // ReSharper disable once UnusedMemberInSuper.Global
    Task PublishAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken = default);
}
