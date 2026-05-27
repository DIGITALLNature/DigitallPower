// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.maintenance.Base;

public abstract class BaseMaintenance(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<MaintenanceVerb>(tracer, connection, configResolver, console);
