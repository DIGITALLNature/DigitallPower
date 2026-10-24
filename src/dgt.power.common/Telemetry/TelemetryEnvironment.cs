// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Telemetry;

/// <summary>
/// Environment-based telemetry checks shared across modules.
/// </summary>
public static class TelemetryEnvironment
{
    private const string OptOutEnvVar = "DGTP_TELEMETRY_OPTOUT";
    private const string DoNotTrackEnvVar = "DO_NOT_TRACK";

    /// <summary>
    /// True when the user has opted out of telemetry via environment variable.
    /// </summary>
    public static bool IsOptedOut =>
        ExecutionEnvironment.IsTruthy(Environment.GetEnvironmentVariable(OptOutEnvVar))
        || ExecutionEnvironment.IsTruthy(Environment.GetEnvironmentVariable(DoNotTrackEnvVar));
}
