// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Azure.Identity;

namespace dgt.power.common.Connections;

public interface IUserTokenCache
{
    Task<bool> RemoveAccountAsync(
        AuthenticationRecord authenticationRecord,
        bool allowUnencryptedStorage,
        CancellationToken cancellationToken);
}
