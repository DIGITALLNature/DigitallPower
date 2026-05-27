// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests;

public class TestConnection(IOrganizationService service) : IXrmConnection
{
    public IOrganizationService Connect()
    {
        var userId = ((WhoAmIResponse)service.Execute(new WhoAmIRequest())).UserId;
        Console.WriteLine($"WhoAmI: [bold]{userId:D}[/]");
        return service;
    }
}
