using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.common;

public interface IXrmConnection
{
    IOrganizationService Connect();
}
