// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk;

namespace dgt.power.common;

/// <summary>
/// Base class for internal worker logic that does not participate in the Spectre.Console.Cli
/// command pipeline. Use this for components that are orchestrated by another command and
/// therefore must not be invoked via the Spectre runner.
/// </summary>
/// <typeparam name="TConfig">The settings/verb type passed to <see cref="Invoke"/>.</typeparam>
public abstract class PowerWorker<TConfig>(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver) : IPowerLogic
    where TConfig : BaseProgramSettings
{
    protected const int PageSize = 5000;

    protected IConfigResolver ConfigResolver { get; } = configResolver;

    protected IOrganizationService Connection { get; } = connection;

    protected ITracer Tracer { get; } = tracer;

    /// <summary>
    /// Executes the worker logic for the given configuration.
    /// </summary>
    /// <param name="args">The worker configuration/verb.</param>
    /// <returns><c>true</c> on success, <c>false</c> on failure.</returns>
    public bool Invoke(TConfig args) => InvokeCore(args);

    protected abstract bool InvokeCore(TConfig args);
}
