// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Connections;

public interface ISecretStore
{
    string? ReadSecret(string connectionName, string key);

    void WriteSecret(string connectionName, string key, string value);

    void DeleteSecret(string connectionName, string key);

    void Delete(string connectionName);
}
