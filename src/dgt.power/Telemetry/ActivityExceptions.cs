// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;

namespace dgt.power.Telemetry;

internal static class ActivityExceptions
{
    private const string RecordedKey = "dgtp.recorded";

    internal static void Mark(Exception exception) => exception.Data[RecordedKey] = true;

    internal static bool IsRecorded(Exception exception)
    {
        for (var current = exception; current is not null; current = current.InnerException)
        {
            if (current.Data.Contains(RecordedKey))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Sets the error status and records the exception both as tags (for quick filtering via
    /// customDimensions) and as an OpenTelemetry "exception" event following the standard semantic
    /// conventions (<c>exception.type</c>, <c>exception.message</c>, <c>exception.stacktrace</c>,
    /// <c>exception.escaped</c>). The Azure Monitor exporter turns exception *events* - but not plain
    /// tags - into proper entries in the Application Insights "exceptions" table / Failures blade.
    /// </summary>
    internal static void Record(Activity activity, Exception exception, bool escaped)
    {
        var error = exception.RootException();
        var anonymizedMessage = TelemetryAnonymizer.Anonymize(error.Message);
        var anonymizedStackTrace = TelemetryAnonymizer.AnonymizeStackTrace(error.StackTrace);

        activity.SetStatus(ActivityStatusCode.Error, anonymizedMessage);
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

    internal static void SetCommonTags(Activity activity, string? installId)
    {
        activity.SetTag("dgtp.is_ci", ExecutionEnvironment.IsCiAgent);
        activity.SetTag("dgtp.os", Environment.OSVersion.Platform.ToString());
        activity.SetTag("dgtp.version", DgtpActivitySource.Instance.Version);
        if (installId != null)
        {
            activity.SetTag("dgtp.install_id", installId);
        }
    }
}
