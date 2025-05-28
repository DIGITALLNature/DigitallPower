// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.push.Base;

public abstract class BasePush : PowerLogic<PushVerb>
{
    protected BasePush(ITracer tracer, IXrmConnectionFactory xrmConnectionFactory, IConfigResolver configResolver) : base(tracer, xrmConnectionFactory, configResolver)
    {
    }
}
