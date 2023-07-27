// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dto;
using Microsoft.Xrm.Sdk;

namespace dgt.power.import.Base;

public abstract class BaseImport : PowerLogic<ImportVerb>
{
    public BaseImport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer, connection, configResolver)
    {
    }


    protected static string GetAssignee(string assignee, Assignee owner)
    {
        if (!string.IsNullOrWhiteSpace(owner.Owner))
        {
            return owner.Owner;
        }

        return assignee;
    }
}
