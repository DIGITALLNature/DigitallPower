// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.Telemetry;
using Spectre.Console;

namespace dgt.power;

internal sealed class Tracer(bool telemetryEnabled = false, string? installId = null, IAnsiConsole? console = null, TelemetryState? state = null) : ITracer
{
    private Activity? _currentActivity;
    private readonly IAnsiConsole _console = console ?? AnsiConsole.Console;
    private readonly TelemetryState _state = state ?? new TelemetryState(telemetryEnabled, installId);

    internal void EnableTelemetry(string id) => _state.Enable(id);

    internal static bool SuppressForInvocation
    {
        get => TelemetryState.SuppressForInvocation;
        set => TelemetryState.SuppressForInvocation = value;
    }

    private bool IsActive => _state.IsActive;

    public void Log(string message, TraceEventType type) => _console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[underline red]{type}:[/]  {message}");

    public void Start<T>(T action) where T : IPowerLogic
    {
        _console.Write(new Rule($"[lime]{action.GetType().Name} - Start[/]"));

        if (IsActive)
        {
            _currentActivity = DgtpActivitySource.Instance.StartActivity(
                $"command.{action.GetType().Name}");
            _currentActivity?.SetTag("dgtp.command", action.GetType().Name);
            if (_currentActivity is not null)
            {
                ActivityExceptions.SetCommonTags(_currentActivity, _state.InstallId);
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
            ActivityExceptions.Record(_currentActivity, e, escaped: false);
        }
    }

    public void TrackFatalException(Exception e)
    {
        if (!IsActive || ActivityExceptions.IsRecorded(e)) return;

        using var activity = DgtpActivitySource.Instance.StartActivity();
        if (activity != null)
        {
            ActivityExceptions.SetCommonTags(activity, _state.InstallId);
            ActivityExceptions.Record(activity, e, escaped: true);
        }
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
