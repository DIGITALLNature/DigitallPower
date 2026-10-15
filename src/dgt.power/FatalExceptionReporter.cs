// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Telemetry;

namespace dgt.power;

/// <summary>
/// Records exceptions that escape a command run (unhandled, unobserved or thrown by the command app)
/// as a standalone span, unless the run already recorded them.
/// </summary>
internal sealed class FatalExceptionReporter(TelemetryState state)
{
    public void Report(Exception exception)
    {
        if (!state.IsActive || ActivityExceptions.IsRecorded(exception))
        {
            return;
        }

        using var activity = DgtpActivitySource.Instance.StartActivity();
        if (activity == null)
        {
            return;
        }

        ActivityExceptions.SetCommonTags(activity, state.InstallId);
        ActivityExceptions.Record(activity, exception, escaped: true);
    }
}
