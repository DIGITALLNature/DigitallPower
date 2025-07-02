// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.dto;

namespace dgt.power.import.Base;

public abstract class BaseImport : PowerLogic<ImportVerb>
{
    protected BaseImport(ITracer tracer, IXrmConnectionFactory xrmConnectionFactory, IConfigResolver configResolver) : base(tracer, xrmConnectionFactory, configResolver)
    {
    }


    protected static string GetAssignee(string assignee, Assignee owner)
    {
        Debug.Assert(owner != null, nameof(owner) + " != null");
        if (!string.IsNullOrWhiteSpace(owner.Owner))
        {
            return owner.Owner;
        }

        return assignee;
    }
}
