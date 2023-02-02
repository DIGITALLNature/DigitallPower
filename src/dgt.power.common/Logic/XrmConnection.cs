using System.Net;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.common.Logic;

public class XrmConnection : IXrmConnection
{
    private readonly IProfileManager _profileManager;

    public XrmConnection(IProfileManager profileManager)
    {
        _profileManager = profileManager;
    }


    // TODO: Shouldn't we handle exceptions with error code?
    public IOrganizationService Connect()
    {
        if (_profileManager.CurrentIdentity == null)
        {
            throw new InvalidOperationException("Connect is only possible with set Current Identity");
        }

        Enum.TryParse(_profileManager.CurrentIdentity.SecurityProtocol, true, out SecurityProtocolType value);
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
