// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console.Cli;

namespace dgt.power.common;

/// <summary>
/// Owns the lifecycle of one command run: one telemetry span, one start and one end status line.
/// Subclasses implement <see cref="RunAsync" /> and acquire everything else themselves.
/// </summary>
public abstract class TracedCommand<TSettings>(ICommandSpanFactory spans, ICommandStatusWriter status)
    : AsyncCommand<TSettings>
    where TSettings : BaseProgramSettings
{
    public sealed override async Task<int> ExecuteAsync(
        CommandContext context,
        [NotNull] TSettings settings,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        var stopwatch = Stopwatch.StartNew();
        var success = false;
        status.WriteStart(context.Name);
        // The span name is telemetry and keeps the type name; the console label is the command path.
        using var span = spans.Start(GetType().Name);
        try
        {
            success = await RunAsync(settings, cancellationToken);
            span.Complete(success);
            return success ? 0 : 1;
        }
        catch (Exception exception)
        {
            span.Record(exception);
            throw;
        }
        finally
        {
            status.WriteEnd(context.Name, success, stopwatch.Elapsed);
        }
    }

    /// <summary>
    /// Runs the command. Returns <c>true</c> on success and <c>false</c> for a handled failure (exit code 1).
    /// Throw for unexpected failures; <c>InteractiveLoginRequiredException</c> maps to exit code 2.
    /// </summary>
    protected abstract Task<bool> RunAsync(TSettings settings, CancellationToken cancellationToken);
}
