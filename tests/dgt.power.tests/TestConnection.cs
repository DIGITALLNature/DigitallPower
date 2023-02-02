using dgt.power.common;
using dgt.power.dataverse;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.tests;

public class TestConnection : IXrmConnection
{
    private readonly IOrganizationService _service;

    public TestConnection(IOrganizationService service)
    {
        _service = service;
    }

    public IOrganizationService Connect()
    {
        var userId = ((WhoAmIResponse)_service.Execute(new WhoAmIRequest())).UserId;
        Console.WriteLine($"WhoAmI: [bold]{userId:D}[/]");
        return _service;
    }
}
