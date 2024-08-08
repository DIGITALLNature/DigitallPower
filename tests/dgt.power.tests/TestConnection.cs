// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests;

public class TestConnection : IXrmConnection
{
    private readonly IOrganizationServiceAsync2 _service;

    public TestConnection(IOrganizationServiceAsync2 service)
    {
        _service = service;
    }

    public IOrganizationServiceAsync2 Connect()
    {
        var userId = ((WhoAmIResponse)_service.Execute(new WhoAmIRequest())).UserId;
        Console.WriteLine($"WhoAmI: [bold]{userId:D}[/]");
        return _service;
    }
}
