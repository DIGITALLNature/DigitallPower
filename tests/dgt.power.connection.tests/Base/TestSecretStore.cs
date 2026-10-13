// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;

namespace dgt.power.connection.tests.Base;

public sealed class TestSecretStore : ISecretStore
{
    private readonly Dictionary<(string Connection, string Key), string> _secrets = new();

    public string? ReadSecret(string connectionName, string key) =>
        _secrets.GetValueOrDefault((connectionName, key));

    public void WriteSecret(string connectionName, string key, string value) =>
        _secrets[(connectionName, key)] = value;

    public void DeleteSecret(string connectionName, string key) =>
        _secrets.Remove((connectionName, key));

    public void Delete(string connectionName)
    {
        foreach (var key in _secrets.Keys.Where(key => key.Connection == connectionName).ToList())
        {
            _secrets.Remove(key);
        }
    }
}
