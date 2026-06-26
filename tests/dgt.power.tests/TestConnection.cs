// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.tests;

public class TestConnection(IOrganizationServiceAsync2 service) : IXrmConnection
{
    public Task<IOrganizationServiceAsync2> ConnectAsync()
    {
        var userId = ((WhoAmIResponse)service.Execute(new WhoAmIRequest())).UserId;
        Console.WriteLine($"WhoAmI: [bold]{userId:D}[/]");
        return Task.FromResult(service);
    }

    public Task<bool> CheckAuthAsync() => Task.FromResult(true);
}
