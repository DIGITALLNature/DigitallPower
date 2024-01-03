// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Net;
using dgt.power.common.Exceptions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.common.Logic;

public class XrmConnection : IXrmConnection
{
    private readonly IConfiguration _configuration;
    private readonly IProfileManager _profileManager;

    public XrmConnection(IProfileManager profileManager, IConfiguration configuration)
    {
        _profileManager = profileManager;
        _configuration = configuration;
    }


    public IOrganizationService Connect()
    {
        var xrmConfiguration = _configuration.GetSection("xrm").GetChildren().ToList();

        if (xrmConfiguration.Count != 0)
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
#pragma warning disable S4830
            ServicePointManager.ServerCertificateValidationCallback = (_, _, _, _) => true;
#pragma warning restore S4830
#pragma warning restore CA5359
        }

        AnsiConsole.MarkupLine("Connect to given configuration.");
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
#pragma warning disable S4830
            ServicePointManager.ServerCertificateValidationCallback = (_, _, _, _) => true;
#pragma warning restore S4830
#pragma warning restore CA5359
        }

        IConnector connector;
        if (_profileManager.CurrentIdentity is TokenIdentity tokenIdentity)
        {
            AnsiConsole.MarkupLine($"Connect to {_profileManager.Current} via MSAL connection");
            connector = new TokenConnector(tokenIdentity, _profileManager);
        }
        else
        {
            AnsiConsole.MarkupLine($"Connect to {_profileManager.Current} via classic connection");
            connector = new CrmConnector(identity.ConnectionString);
        }

        try
        {
            var service = connector.GetOrganizationServiceProxy();
            CheckWhoAmI(service);
            return service;
        }
        catch (Exception exception)
        {
            throw new FailedConnectionException(_profileManager.Current, exception);
        }
    }

    private static void CheckWhoAmI(IOrganizationService service)
    {
        var userId = ((WhoAmIResponse)service.Execute(new WhoAmIRequest())).UserId;
        AnsiConsole.MarkupLine($"WhoAmI: [bold]{userId:D}[/]");
    }
}
