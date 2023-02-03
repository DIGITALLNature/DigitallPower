using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.common.Logic;

public class XrmConnection : IXrmConnection
{
    private readonly IProfileManager _profileManager;
    private readonly IConfiguration _configuration;

    public XrmConnection(IProfileManager profileManager, IConfiguration configuration)
    {
        _profileManager = profileManager;
        _configuration = configuration;
    }


    // TODO: Shouldn't we handle exceptions with error code?
    public IOrganizationService Connect()
    {
        if (!_configuration.GetSection("xrm").GetChildren().Any() && _profileManager.CurrentIdentity == null)
        {
            throw new InvalidOperationException("Connect is only possible with set Current Identity");
        }

        if (_configuration.GetSection("xrm").GetChildren().Any())
        {
            return ConnectViaEnviroment();
        }

        return ConnectViaProfile();
    }

    private IOrganizationService ConnectViaEnviroment()
    {
        var protocol = _configuration.GetValue<SecurityProtocolType>("xrm:securityprotocol");

        ServicePointManager.SecurityProtocol = protocol;
        if (_configuration.GetValue<bool>("xrm:insecure"))
        {
#pragma warning disable CA5359
            ServicePointManager.ServerCertificateValidationCallback = (_, _, _, _) => true;
#pragma warning restore CA5359
        }

        AnsiConsole.MarkupLine($"Connect to given configuration.");
        var connector = new CrmConnector(_configuration.GetValue<string>("xrm:connection"));
        return connector.GetOrganizationServiceProxy();
    }

    private IOrganizationService ConnectViaProfile()
    {
        Enum.TryParse(_profileManager.CurrentIdentity!.SecurityProtocol, true, out SecurityProtocolType value);
        ServicePointManager.SecurityProtocol = value;
        if (_profileManager.CurrentIdentity.Insecure)
        {
#pragma warning disable CA5359
            ServicePointManager.ServerCertificateValidationCallback = (_, _, _, _) => true;
#pragma warning restore CA5359
        }

        AnsiConsole.MarkupLine($"Connect to {_profileManager.Current}");
        var connector = new CrmConnector(_profileManager.CurrentConnectionString);
        return connector.GetOrganizationServiceProxy();
    }
}
