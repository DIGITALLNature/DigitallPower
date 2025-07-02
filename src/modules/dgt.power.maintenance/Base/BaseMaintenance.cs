// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;

namespace dgt.power.maintenance.Base;

public abstract class BaseMaintenance : PowerLogic<MaintenanceVerb>
{
    protected BaseMaintenance(ITracer tracer, IXrmConnectionFactory xrmConnectionFactory, IConfigResolver configResolver) : base(tracer, xrmConnectionFactory, configResolver)
    {
    }
}
