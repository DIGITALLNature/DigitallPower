// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;
using Spectre.Console;

namespace dgt.power.common.Logic;

internal partial class CrmConnector: IConnector
{
    private readonly string _connectionString;
    private readonly IAnsiConsole _console;

    internal CrmConnector(string connectionString, IAnsiConsole? console = null)
    {
        _connectionString = connectionString;
        _console = console ?? AnsiConsole.Console;
    }

    public IOrganizationServiceAsync2 CreateOrganizationServiceProxy()
    {
        if (!SkipDiscoveryRegex().IsMatch(_connectionString))
        {
            _console.MarkupLine(CultureInfo.InvariantCulture, "[italic]Connection String: It's recommended to use 'SkipDiscovery=true'![/]");
        }

        if (!RequireNewInstanceRegex().IsMatch(_connectionString))
        {
            _console.MarkupLine(CultureInfo.InvariantCulture, "[italic]Connection String: It's recommended to use 'RequireNewInstance=true'![/]");
        }

        var serviceClient = new ServiceClient(_connectionString);

        return GetOrganizationService(serviceClient);
    }

    [SuppressMessage("Performance", "CA1859:Verwenden Sie nach Möglichkeit konkrete Typen, um die Leistung zu verbessern.")]
    private static IOrganizationServiceAsync2 GetOrganizationService(ServiceClient serviceClient)
    {
        if (!serviceClient.IsReady)
        {
            throw new DataverseConnectionException($"XRM Connection Failed: {serviceClient.LastError}", serviceClient.LastException);
        }

        return serviceClient;
    }

    [GeneratedRegex("SkipDiscovery=True", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex SkipDiscoveryRegex();
    [GeneratedRegex("RequireNewInstance=True", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex RequireNewInstanceRegex();
}
