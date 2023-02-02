using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.push.Base;

public abstract class BasePush : PowerLogic<PushVerb>
{
    protected BasePush(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
    }
}
