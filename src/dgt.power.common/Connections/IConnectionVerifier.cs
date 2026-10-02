// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Connections;

public interface IConnectionVerifier
{
    Task VerifyAsync(
        string connectionName,
        ConnectionDefinition connection,
        bool allowUnencryptedStorage,
        CancellationToken cancellationToken);
}
