// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.dto;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.import.Base;

public abstract class BaseImport(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<ImportVerb>(tracer, connection, configResolver, console)
{
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
