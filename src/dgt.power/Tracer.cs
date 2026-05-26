// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.Telemetry;
using Spectre.Console;

namespace dgt.power;

internal sealed class Tracer(bool telemetryEnabled = false, string? installId = null) : ITracer
{
    private Activity? _currentActivity;

    /// <summary>
    /// Set to true by the TelemetryInterceptor when --no-telemetry is passed.
    /// </summary>
    internal static bool SuppressForInvocation { get; set; }

    private bool IsActive => telemetryEnabled && !SuppressForInvocation;

    public void Log(string message, TraceEventType type) => AnsiConsole.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[underline red]{type}:[/]  {message}");

    public void Start<T>(T action) where T : IPowerLogic
    {
        AnsiConsole.Write(new Rule($"[lime]{action.GetType().Name} - Start[/]"));

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
        AnsiConsole.Write(new Rule($"[red]{action.GetType().Name} - Not Configured[/]"));
        EndActivity(false);
        return false;
    }

    public bool Skipped<T>(T action) where T : IPowerLogic
    {
        AnsiConsole.Write(new Rule($"[yellow]{action.GetType().Name} - Skipped[/]"));
        EndActivity(true);
        return true;
    }

    public bool End<T>(T action, bool result) where T : IPowerLogic
    {
        AnsiConsole.Write(result ? new Rule($"[lime]{action.GetType().Name} - End:OK[/]") : new Rule($"[red]{action.GetType().Name} - End:ERROR[/]"));
        EndActivity(result);
        return result;
    }

    public void Exception(Exception e, TraceEventType type)
    {
        var error = e.RootException();
        AnsiConsole.WriteException(error);
        _currentActivity?.SetStatus(ActivityStatusCode.Error, error.Message);
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
