// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Data.Common;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common.Logic;

internal sealed class ConnectionStringConnector : IDataverseConnector
{
    private readonly string _connectionString;
    private readonly Func<string, ServiceClient> _createClient;

    internal ConnectionStringConnector(string connectionString)
        : this(connectionString, static value => new ServiceClient(value))
    {
    }

    internal ConnectionStringConnector(string connectionString, Func<string, ServiceClient> createClient)
    {
        _connectionString = connectionString;
        _createClient = createClient;
    }

    private string GetUniqueConnectionString()
    {
        var builder = new DbConnectionStringBuilder
        {
            ConnectionString = _connectionString,
            ["RequireNewInstance"] = true
        };
        return builder.ConnectionString;
    }

    public Task<ServiceClient> CreateServiceClientAsync() =>
        Task.FromResult(_createClient(GetUniqueConnectionString()));
}
