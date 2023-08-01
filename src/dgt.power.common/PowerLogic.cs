// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Spectre.Console.Cli;

namespace dgt.power.common;

public abstract class PowerLogic<TConfig> : Command<TConfig>, IPowerLogic where TConfig : BaseProgramSettings
{
    protected const int PageSize = 5000;

    protected PowerLogic(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver)
    {
        Tracer = tracer;
        Connection = connection;
        ConfigResolver = configResolver;
    }

    protected IConfigResolver ConfigResolver { get; }

    protected IOrganizationService Connection { get; }
    protected ITracer Tracer { get; }

    public override int Execute([NotNull] CommandContext context, [NotNull] TConfig settings) => Execute(settings) ? 0 : 1;

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
