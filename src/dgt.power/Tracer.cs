// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.Telemetry;
using Spectre.Console;

namespace dgt.power;

internal sealed class Tracer(bool telemetryEnabled = false, string? installId = null, IAnsiConsole? console = null) : ITracer
{
    private Activity? _currentActivity;
    private readonly IAnsiConsole _console = console ?? AnsiConsole.Console;

    /// <summary>
    /// Set to true by the TelemetryInterceptor when --no-telemetry is passed.
    /// </summary>
    internal static bool SuppressForInvocation { get; set; }

    private bool IsActive => telemetryEnabled && !SuppressForInvocation;

    public void Log(string message, TraceEventType type) => _console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[underline red]{type}:[/]  {message}");

    public void Start<T>(T action) where T : IPowerLogic
    {
        _console.Write(new Rule($"[lime]{action.GetType().Name} - Start[/]"));

        if (IsActive)
        {
            _currentActivity = DgtpActivitySource.Instance.StartActivity(
                $"command.{action.GetType().Name}");
            _currentActivity?.SetTag("dgtp.command", action.GetType().Name);
            _currentActivity?.SetTag("dgtp.is_ci", TelemetryConfig.IsCi);
            _currentActivity?.SetTag("dgtp.os", Environment.OSVersion.Platform.ToString());
            _currentActivity?.SetTag("dgtp.version", DgtpActivitySource.Instance.Version);
            if (installId != null)
            {
                _currentActivity?.SetTag("dgtp.install_id", installId);
            }
        }
    }

    public bool NotConfigured<T>(T action) where T : IPowerLogic
    {
        _console.Write(new Rule($"[red]{action.GetType().Name} - Not Configured[/]"));
        EndActivity(false);
        return false;
    }

    public bool Skipped<T>(T action) where T : IPowerLogic
    {
        _console.Write(new Rule($"[yellow]{action.GetType().Name} - Skipped[/]"));
        EndActivity(true);
        return true;
    }

    public bool End<T>(T action, bool result) where T : IPowerLogic
    {
        _console.Write(result ? new Rule($"[lime]{action.GetType().Name} - End:OK[/]") : new Rule($"[red]{action.GetType().Name} - End:ERROR[/]"));
        EndActivity(result);
        return result;
    }

    public void Exception(Exception e, TraceEventType type)
    {
        var error = e.RootException();
        _console.WriteException(error);
        if (IsActive && _currentActivity != null)
        {
            var anonymizedMessage = TelemetryAnonymizer.Anonymize(error.Message);
            var anonymizedStackTrace = TelemetryAnonymizer.AnonymizeStackTrace(error.StackTrace);

            _currentActivity.SetStatus(ActivityStatusCode.Error, anonymizedMessage);
            RecordException(_currentActivity, error, anonymizedMessage, anonymizedStackTrace, escaped: false);
        }
    }

    public void TrackFatalException(Exception e)
    {
        if (!IsActive) return;

        var error = e.RootException();
        using var activity = DgtpActivitySource.Instance.StartActivity("fatal_exception");
        if (activity != null)
        {
            activity.SetTag("dgtp.is_ci", TelemetryConfig.IsCi);
            activity.SetTag("dgtp.os", Environment.OSVersion.Platform.ToString());
            activity.SetTag("dgtp.version", DgtpActivitySource.Instance.Version);
            if (installId != null)
            {
                activity.SetTag("dgtp.install_id", installId);
            }

            var anonymizedMessage = TelemetryAnonymizer.Anonymize(error.Message);
            var anonymizedStackTrace = TelemetryAnonymizer.AnonymizeStackTrace(error.StackTrace);

            activity.SetStatus(ActivityStatusCode.Error, anonymizedMessage);
            RecordException(activity, error, anonymizedMessage, anonymizedStackTrace, escaped: true);
        }
    }

    /// <summary>
    /// Records the exception both as tags (for quick filtering on the enclosing span/dependency via
    /// customDimensions) and as an OpenTelemetry "exception" event following the standard semantic
    /// conventions (<c>exception.type</c>, <c>exception.message</c>, <c>exception.stacktrace</c>,
    /// <c>exception.escaped</c>). The Azure Monitor exporter turns exception *events* - but not plain
    /// tags - into proper entries in the Application Insights "exceptions" table / Failures blade.
    /// </summary>
    private static void RecordException(Activity activity, Exception error, string anonymizedMessage, string anonymizedStackTrace, bool escaped)
    {
        activity.SetTag("exception.type", error.GetType().FullName);
        activity.SetTag("exception.message", anonymizedMessage);
        activity.SetTag("exception.stacktrace", anonymizedStackTrace);

        var tags = new ActivityTagsCollection
        {
            { "exception.type", error.GetType().FullName },
            { "exception.message", anonymizedMessage },
            { "exception.stacktrace", anonymizedStackTrace },
            { "exception.escaped", escaped }
        };
        activity.AddEvent(new ActivityEvent("exception", tags: tags));
    }

    private void EndActivity(bool success)
    {
        if (_currentActivity == null) return;
        _currentActivity.SetTag("dgtp.success", success);
        // Only update status if not already set to Error (preserves Exception's description)
        if (_currentActivity.Status != ActivityStatusCode.Error)
        {
            _currentActivity.SetStatus(success ? ActivityStatusCode.Ok : ActivityStatusCode.Error);
        }
        _currentActivity.Dispose();
        _currentActivity = null;
    }
}
