// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;

namespace dgt.power.Telemetry;

internal sealed class CommandSpanFactory(TelemetryState state) : ICommandSpanFactory
{
    public ICommandSpan Start(string commandName)
    {
        if (!state.IsActive)
        {
            return NoOpSpan.Instance;
        }

        var activity = DgtpActivitySource.Instance.StartActivity($"command.{commandName}");
        if (activity is null)
        {
            return NoOpSpan.Instance;
        }

        activity.SetTag("dgtp.command", commandName);
        ActivityExceptions.SetCommonTags(activity, state.InstallId);
        return new CommandSpan(activity);
    }

    private sealed class CommandSpan(Activity activity) : ICommandSpan
    {
        public void Complete(bool success)
        {
            activity.SetTag("dgtp.success", success);
            // Keep an Error status (and its description) set by an earlier recorded exception.
            if (activity.Status != ActivityStatusCode.Error)
            {
                activity.SetStatus(success ? ActivityStatusCode.Ok : ActivityStatusCode.Error);
            }
        }

        public void Record(Exception exception)
        {
            activity.SetTag("dgtp.success", false);
            ActivityExceptions.Record(activity, exception, escaped: true);
            ActivityExceptions.Mark(exception);
        }

        public void Dispose() => activity.Dispose();
    }

    private sealed class NoOpSpan : ICommandSpan
    {
        internal static readonly NoOpSpan Instance = new();

        public void Complete(bool success)
        {
        }

        public void Record(Exception exception) => ActivityExceptions.Mark(exception);

        public void Dispose()
        {
        }
    }
}
