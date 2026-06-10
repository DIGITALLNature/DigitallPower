// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.common;

public abstract class PowerLogic<TConfig>(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : AsyncCommand<TConfig>, IPowerLogic
    where TConfig : BaseProgramSettings
{
    protected const int PageSize = 5000;

    protected IAnsiConsole Console { get; } = console;
    protected IConfigResolver ConfigResolver { get; } = configResolver;

    protected IOrganizationService Connection { get; } = connection;
    protected ITracer Tracer { get; } = tracer;

    protected override async Task<int> ExecuteAsync(CommandContext context, [NotNull] TConfig settings, CancellationToken cancellationToken) =>
        await ExecuteAsync(settings, cancellationToken) ? 0 : 1;

    private async Task<bool> ExecuteAsync(TConfig args, CancellationToken cancellationToken)
    {
        return await InvokeAsync(args, cancellationToken);
    }

    protected virtual Task<bool> InvokeAsync(TConfig args, CancellationToken cancellationToken) => Task.FromResult(Invoke(args));

    protected abstract bool Invoke(TConfig args);
}
