using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.common.Logic;

internal class CrmConnector
{
    private readonly string _connectionString;

    internal CrmConnector(string connectionString) => _connectionString = connectionString;

    internal IOrganizationService GetOrganizationServiceProxy()
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

        var service = GetOrganizationService(serviceClient);

        CheckWhoAmI(service);

        return service;
    }

    private static void CheckWhoAmI(IOrganizationService service)
    {
        var userId = ((WhoAmIResponse)service.Execute(new WhoAmIRequest())).UserId;
        AnsiConsole.MarkupLine($"WhoAmI: [bold]{userId:D}[/]");
    }

    private static IOrganizationService GetOrganizationService(ServiceClient serviceClient)
    {
        if (!serviceClient.IsReady)
        {
            throw new DataverseConnectionException($"XRM Connection Failed: {serviceClient.LastError}", serviceClient.LastException);
        }

        return serviceClient;
    }
}
