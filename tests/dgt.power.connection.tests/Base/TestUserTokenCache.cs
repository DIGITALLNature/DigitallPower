// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Azure.Identity;
using dgt.power.common.Connections;

namespace dgt.power.connection.tests.Base;

public sealed class TestUserTokenCache : IUserTokenCache
{
    public List<string> RemovedHomeAccountIds { get; } = [];

    public Task<bool> RemoveAccountAsync(
        AuthenticationRecord authenticationRecord,
        bool allowUnencryptedStorage,
        CancellationToken cancellationToken)
    {
        RemovedHomeAccountIds.Add(authenticationRecord.HomeAccountId);
        return Task.FromResult(true);
    }
}
