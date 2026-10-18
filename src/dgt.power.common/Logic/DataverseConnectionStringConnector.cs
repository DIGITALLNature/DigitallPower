// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;
using Spectre.Console;

namespace dgt.power.common.Logic;

internal sealed class DataverseConnectionStringConnector : IConnector
{
    private readonly string _connectionString;
    private readonly IAnsiConsole _console;

    internal DataverseConnectionStringConnector(string connectionString, IAnsiConsole? console = null)
    {
        _connectionString = connectionString;
        _console = console ?? AnsiConsole.Console;
    }

    public Task<IOrganizationServiceAsync2> CreateOrganizationServiceProxyAsync()
    {
        var serviceClient = new ServiceClient(_connectionString);

        return Task.FromResult(GetOrganizationService(serviceClient));
    }

    [SuppressMessage("Performance", "CA1859:Verwenden Sie nach Möglichkeit konkrete Typen, um die Leistung zu verbessern.")]
    private static IOrganizationServiceAsync2 GetOrganizationService(ServiceClient serviceClient)
    {
        if (!serviceClient.IsReady)
        {
            throw new DataverseConnectionException($"Dataverse connection failed: {serviceClient.LastError}", serviceClient.LastException);
        }

        return serviceClient;
    }
}
