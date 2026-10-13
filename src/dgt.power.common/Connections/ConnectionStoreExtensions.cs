// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Connections;

public static class ConnectionStoreExtensions
{
    /// <summary>
    /// Returns the persisted spelling of a connection name. Connection names are case-insensitive,
    /// but <see cref="ISecretStore"/> keys are case-sensitive, so secrets must always be addressed
    /// by the persisted name.
    /// </summary>
    public static string ResolveName(this IConnectionStore store, string name)
    {
        ArgumentNullException.ThrowIfNull(store);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return store.GetAll().Keys.FirstOrDefault(key => string.Equals(key, name, StringComparison.OrdinalIgnoreCase))
               ?? name;
    }
}
