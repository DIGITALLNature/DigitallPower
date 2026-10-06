// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using dgt.power.plugin.Remote;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.plugin.Planning.Deployment;

public sealed record DataProviderDeployment(
    LocalDataProvider? Local,
    RemoteDataProvider? Remote,
    EntityMetadata? DataSource,
    bool UpdateDataSource,
    bool UpdateProvider,
    IReadOnlyDictionary<DataProviderOperation, string> Handlers,
    SolutionLink? ProviderSolution,
    SolutionLink? DataSourceSolution)
{
    // Migration-only entries carry no local declaration: they only keep undeclared handlers alive when their plugin type is replaced.
    public static DataProviderDeployment Migration(RemoteDataProvider remote, IReadOnlyDictionary<DataProviderOperation, string> handlers) =>
        new(Local: null, remote, DataSource: null, UpdateDataSource: false, UpdateProvider: true, handlers, ProviderSolution: null, DataSourceSolution: null);

    public string Name => Local?.ProviderName ?? Remote!.Name;
    public bool HasChanges => UpdateProvider || UpdateDataSource || DataSourceSolution is not null || ProviderSolution is not null;
}
