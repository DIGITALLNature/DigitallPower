// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Net;
using dgt.power.common.Exceptions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.common.Logic;

public class XrmConnection(IProfileManager profileManager, IConfiguration configuration)
    : IXrmConnection
{
    public IOrganizationService Connect()
    {
        var xrmConfiguration = configuration.GetSection("xrm").GetChildren().ToList();

        if (xrmConfiguration.Count != 0)
        {
            return ConnectWithConfiguration();
        }

        if (profileManager.CurrentIdentity != null)
        {
            return ConnectWithProfile(profileManager.CurrentIdentity);
        }

        throw new MissingConnectionException();
    }

    private  IOrganizationService ConnectWithConfiguration()
    {
        var protocol = configuration.GetValue<SecurityProtocolType>("xrm:securityprotocol");

        ServicePointManager.SecurityProtocol = protocol;
        if (configuration.GetValue<bool>("xrm:insecure"))
        {
#pragma warning disable CA5359
#pragma warning disable S4830
            ServicePointManager.ServerCertificateValidationCallback = (_, _, _, _) => true;
#pragma warning restore S4830
#pragma warning restore CA5359
        }

        AnsiConsole.MarkupLine("Connect to given configuration.");
        var connector = new CrmConnector(configuration.GetValue<string>("xrm:connection"));
        try
        {
            return connector.CreateOrganizationServiceProxy();
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
        if (profileManager.CurrentIdentity is TokenIdentity tokenIdentity)
        {
            AnsiConsole.MarkupLine($"Connect to {profileManager.Current} via MSAL connection");
            connector = new TokenConnector(tokenIdentity, profileManager);
        }
        else
        {
            AnsiConsole.MarkupLine($"Connect to {profileManager.Current} via classic connection");
            connector = new CrmConnector(identity.ConnectionString);
        }

        try
        {
            var service = connector.CreateOrganizationServiceProxy();
            CheckWhoAmI(service);
            return service;
        }
        catch (Exception exception)
        {
            throw new FailedConnectionException(profileManager.Current, exception);
        }
    }

    private static void CheckWhoAmI(IOrganizationService service)
    {
        var userId = ((WhoAmIResponse)service.Execute(new WhoAmIRequest())).UserId;
        AnsiConsole.MarkupLine($"WhoAmI: [bold]{userId:D}[/]");
    }
}
