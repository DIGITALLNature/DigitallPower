// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.plugin.Repositories;

public interface IEntityDataProviderRepository
{
    Task<IReadOnlyList<RemoteDataProvider>> ListAsync(CancellationToken cancellationToken = default);
    Task<EntityMetadata?> FindDataSourceAsync(string logicalName, CancellationToken cancellationToken = default);
    Task<int> GetBaseLanguageCodeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates that the environment can provision a data-source configuration table and that an existing table is compatible.
    /// </summary>
    /// <remarks>
    /// The configuration table is itself a virtual table, distinct from the business virtual tables
    /// served by the custom provider. Its <see cref="EntityMetadata.DataProviderId"/> must reference
    /// JsonConverter, not the custom provider. Resolution uses the exact provider name in the target
    /// environment rather than assuming a platform GUID; successful results are cached by the repository.
    /// A null <paramref name="metadata"/> checks the backing provider's availability for a new table.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// JsonConverter is missing or ambiguous.
    /// </exception>
    /// <exception cref="InvalidDataSourceException">
    /// The existing table is not organization-owned,
    /// is backed by another provider, or has no metadata ID.
    /// </exception>
    Task ValidateDataSourceAsync(string logicalName, EntityMetadata? metadata, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates and publishes an organization-owned virtual table for the custom provider's data-source configuration.
    /// </summary>
    /// <remarks>
    /// The table is backed by the built-in JsonConverter data provider, not the custom provider's handlers.
    /// This creates the configuration-table schema only, not configuration records or business virtual tables.
    /// </remarks>
    Task<Guid> CreateDataSourceAsync(LocalDataProvider provider, string? solution, CancellationToken cancellationToken = default);
    Task UpdateDataSourceAsync(EntityMetadata metadata, LocalDataProvider provider, CancellationToken cancellationToken = default);
    Task<Guid> ApplyAsync(Guid? id, LocalDataProvider? provider, IReadOnlyDictionary<DataProviderOperation, Guid> handlers, CancellationToken cancellationToken = default);
}
