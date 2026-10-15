// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.common;

/// <summary>
/// Base class of all commands that run as one traced unit: one telemetry span plus one start and one
/// end line on the console. Subclasses implement <see cref="RunAsync" /> and acquire everything else
/// themselves.
/// </summary>
public abstract class PowerCommand<TSettings>(ICommandSpanFactory spans, IAnsiConsole console)
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
        console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[grey]{Emoji.Known.RightArrow}[/] {context.Name}");
        // The span name is telemetry and keeps the type name; the console label is the command name.
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
            WriteEnd(context.Name, success, stopwatch.Elapsed);
        }
    }

    /// <summary>
    /// Runs the command. Returns <c>true</c> on success and <c>false</c> for a handled failure (exit code 1).
    /// Throw for unexpected failures; <c>InteractiveLoginRequiredException</c> maps to exit code 2.
    /// </summary>
    protected abstract Task<bool> RunAsync(TSettings settings, CancellationToken cancellationToken);

    private void WriteEnd(string name, bool success, TimeSpan duration)
    {
        var (symbol, color, word) = success
            ? (Emoji.Known.CheckMark, "lime", string.Empty)
            : (Emoji.Known.CrossMark, "red", " failed");
        var elapsed = duration.TotalSeconds < 1
            ? string.Create(CultureInfo.InvariantCulture, $"{duration.TotalMilliseconds:0}ms")
            : string.Create(CultureInfo.InvariantCulture, $"{duration.TotalSeconds:0.0}s");
        var title = $"{symbol} {Markup.Escape(name)}{word} · {elapsed}";
        console.Write(new Rule($"[{color}]{title}[/]") { Justification = Justify.Left });
    }
}
