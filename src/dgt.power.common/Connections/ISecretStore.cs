// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.common.Connections;

public interface ISecretStore
{
    string? ReadSecret(string connectionName, string key);

    void WriteSecret(string connectionName, string key, string value);

    void DeleteSecret(string connectionName, string key);

    void Delete(string connectionName);
}
