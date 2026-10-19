// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common.Logic;

internal sealed class ConnectionStringConnector : IDataverseConnector
{
    private readonly string _connectionString;

    internal ConnectionStringConnector(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task<ServiceClient> CreateServiceClientAsync() =>
        Task.FromResult(new ServiceClient(_connectionString));
}
