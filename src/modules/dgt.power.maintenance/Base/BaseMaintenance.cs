using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.maintenance.Base;

public abstract class BaseMaintenance : PowerLogic<MaintenanceVerb>
{
    protected BaseMaintenance(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
    }
}
