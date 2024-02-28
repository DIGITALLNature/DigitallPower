// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;
using Spectre.Console;

namespace dgt.power.common.Logic;

internal class CrmConnector: IConnector
{
    private readonly string _connectionString;

    internal CrmConnector(string connectionString) => _connectionString = connectionString;

    public IOrganizationServiceAsync2 CreateOrganizationServiceProxy()
    {
        if (!Regex.IsMatch(_connectionString, "SkipDiscovery=True", RegexOptions.IgnoreCase))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "[italic]Connection String: It's recommended to use 'SkipDiscovery=true'![/]");
        }

        if (!Regex.IsMatch(_connectionString, "RequireNewInstance=True", RegexOptions.IgnoreCase))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "[italic]Connection String: It's recommended to use 'RequireNewInstance=true'![/]");
        }

        var serviceClient = new ServiceClient(_connectionString);

        return GetOrganizationService(serviceClient);
    }

    private static IOrganizationServiceAsync2 GetOrganizationService(ServiceClient serviceClient)
    {
        if (!serviceClient.IsReady)
        {
            throw new DataverseConnectionException($"XRM Connection Failed: {serviceClient.LastError}", serviceClient.LastException);
        }

        return serviceClient;
    }
}
