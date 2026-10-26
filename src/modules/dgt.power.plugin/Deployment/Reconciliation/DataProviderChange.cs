// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Reconciliation;

internal sealed record DataProviderChange(
    LocalDataProvider? Desired,
    RemoteDataProvider? Existing,
    IReadOnlyDictionary<DataProviderOperation, PluginTypeReference?> Handlers)
{
    internal string Name => Desired?.ProviderName ?? Existing!.Name;

    internal int DestructiveCount => Handlers.Keys.Count(operation => Existing?.Handlers.ContainsKey(operation) == true);
}
