// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.common.Connections;

public interface IConnectionVerifier
{
    Task VerifyAsync(
        string connectionName,
        ConnectionDefinition connection,
        bool allowUnencryptedStorage,
        CancellationToken cancellationToken);
}
