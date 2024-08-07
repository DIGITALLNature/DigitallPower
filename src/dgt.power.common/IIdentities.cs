﻿// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Logic;

namespace dgt.power.common;

public interface IIdentities
{
    IEnumerable<IdentityInfo> Infos { get; }
    void Upsert(string key, Identity identity);
    void Remove(string key);
    void SetCurrent(string key);
    bool Contains(string key);
}

public record IdentityInfo(string Name, string Type);
