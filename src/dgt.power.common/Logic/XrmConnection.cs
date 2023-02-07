using System.Net;
using System.Security.Principal;
using dgt.power.common.Exceptions;
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


    public IOrganizationService Connect()
    {
        var xrmConfiguration = _configuration.GetSection("xrm").GetChildren().ToList();

        if (xrmConfiguration.Any())
        {
            return ConnectWithConfiguration();
        }

        if (_profileManager.CurrentIdentity != null)
        {
            return ConnectWithProfile(_profileManager.CurrentIdentity);
        }

        throw new MissingConnectionException();
    }

    private IOrganizationService ConnectWithConfiguration()
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
        try
        {
            return connector.GetOrganizationServiceProxy();
        }
        catch (Exception e)
        {
            throw new FailedConnectionException("xrm:connection", e);
        }
    }

    private IOrganizationService ConnectWithProfile(Identity identity)
    {
        Enum.TryParse(identity.SecurityProtocol, true, out SecurityProtocolType value);
        ServicePointManager.SecurityProtocol = value;
        if (identity.Insecure)
        {
#pragma warning disable CA5359
            ServicePointManager.ServerCertificateValidationCallback = (_, _, _, _) => true;
#pragma warning restore CA5359
        }

        AnsiConsole.MarkupLine($"Connect to {_profileManager.Current}");
        var connector = new CrmConnector(identity.ConnectionString);
        try
        {
            return connector.GetOrganizationServiceProxy();
        }
        catch (Exception exception)
        {
            throw new FailedConnectionException(_profileManager.Current, exception);
        }
    }
}
