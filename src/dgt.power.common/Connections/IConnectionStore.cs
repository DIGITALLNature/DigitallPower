// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.common.Connections;

public interface IConnectionStore
{
    string? Current { get; }

    IReadOnlyDictionary<string, ConnectionDefinition> GetAll();

    ConnectionDefinition? Find(string name);

    void Upsert(string name, ConnectionDefinition connection, bool makeCurrent = true);

    void SetCurrent(string name);

    bool Remove(string name);

    void RemoveAll();
}
