// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
