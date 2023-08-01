// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Logic;

namespace dgt.power.common;

public interface IProfileManager
{
    string Current { get; }
    Identity? CurrentIdentity { get; }
    string CurrentConnectionString { get; }
    IIdentities GetIdentities();
    void Save();
    void Purge();
}
