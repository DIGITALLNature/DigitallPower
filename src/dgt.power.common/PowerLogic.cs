﻿// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Spectre.Console.Cli;

namespace dgt.power.common;

public abstract class PowerLogic<TConfig>(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver)
    : Command<TConfig>, IPowerLogic
    where TConfig : BaseProgramSettings
{
    protected const int PageSize = 5000;

    protected IConfigResolver ConfigResolver { get; } = configResolver;

    protected IOrganizationService Connection { get; } = connection;
    protected ITracer Tracer { get; } = tracer;

    public override int Execute(CommandContext context, [NotNull] TConfig settings) => Execute(settings) ? 0 : 1;

    private bool Execute(TConfig args)
    {
        try
        {
            return Invoke(args);
        }
        catch (Exception e)
        {
            Tracer.Exception(e, TraceEventType.Error);
            return false;
        }
    }

    protected abstract bool Invoke(TConfig args);
}
