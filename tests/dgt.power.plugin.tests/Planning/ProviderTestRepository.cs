// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;
using dgt.power.plugin.Repositories;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.plugin.tests.Planning;

internal sealed class ProviderTestRepository : IEntityDataProviderRepository
{
    internal static readonly Guid s_backingProviderId = Guid.NewGuid();
    public List<RemoteDataProvider> Providers { get; } = [];
    public EntityMetadata? DataSource { get; init; }
    public int PlatformValidations { get; private set; }
    public Action<string, EntityMetadata?>? BeforeValidate { get; init; }
    public int TableCreates { get; private set; }
    public int TableUpdates { get; private set; }
    public int ProviderWrites { get; private set; }
    public IReadOnlyDictionary<DataProviderOperation, Guid> AppliedHandlers { get; private set; } = new Dictionary<DataProviderOperation, Guid>();
    public Action? BeforeApply { get; set; }

    public Task<IReadOnlyList<RemoteDataProvider>> ListAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<RemoteDataProvider>>(Providers);

    public Task<EntityMetadata?> FindDataSourceAsync(string logicalName, CancellationToken cancellationToken = default) => Task.FromResult(DataSource);
    public Task<int> GetBaseLanguageCodeAsync(CancellationToken cancellationToken = default) => Task.FromResult(1033);

    public Task ValidateDataSourceAsync(string logicalName, EntityMetadata? metadata, CancellationToken cancellationToken = default)
    {
        PlatformValidations++;
        BeforeValidate?.Invoke(logicalName, metadata);
        return Task.CompletedTask;
    }

    public Task<Guid> CreateDataSourceAsync(LocalDataProvider provider, string? solution, CancellationToken cancellationToken = default)
    {
        TableCreates++;
        return Task.FromResult(Guid.NewGuid());
    }

    public Task UpdateDataSourceAsync(EntityMetadata metadata, LocalDataProvider provider, CancellationToken cancellationToken = default)
    {
        TableUpdates++;
        return Task.CompletedTask;
    }

    public Task<Guid> ApplyAsync(Guid? id, LocalDataProvider? provider, IReadOnlyDictionary<DataProviderOperation, Guid> handlers, CancellationToken cancellationToken = default)
    {
        BeforeApply?.Invoke();
        ProviderWrites++;
        AppliedHandlers = handlers;
        return Task.FromResult(id ?? Guid.NewGuid());
    }
}
