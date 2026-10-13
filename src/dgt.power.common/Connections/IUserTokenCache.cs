// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Azure.Identity;

namespace dgt.power.common.Connections;

public interface IUserTokenCache
{
    Task<bool> RemoveAccountAsync(
        AuthenticationRecord authenticationRecord,
        bool allowUnencryptedStorage,
        CancellationToken cancellationToken);
}
