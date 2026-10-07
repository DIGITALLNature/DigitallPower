// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Storage;

namespace dgt.power.Telemetry;

/// <summary>
/// Central telemetry configuration. Determines whether telemetry is active
/// and provides helper methods for CI detection and anonymous install IDs.
/// </summary>
internal static class TelemetryConfig
{
    private const string OptOutEnvVar = "DGTP_TELEMETRY_OPTOUT";
    private const string DoNotTrackEnvVar = "DO_NOT_TRACK";

    /// <summary>
    /// True when the user has opted out of telemetry via environment variable.
    /// </summary>
    public static bool IsOptedOut =>
        ExecutionEnvironment.IsTruthy(Environment.GetEnvironmentVariable(OptOutEnvVar))
        || ExecutionEnvironment.IsTruthy(Environment.GetEnvironmentVariable(DoNotTrackEnvVar));

    public static bool IsEnabled(StateStore stateStore) => !IsOptedOut && stateStore.TelemetryEnabled;

    /// <summary>
    /// True when running on a known CI/CD build agent.
    /// </summary>
    public static bool IsCi => ExecutionEnvironment.IsCiAgent;

    /// <summary>
    /// Retrieves or creates a persistent anonymous install ID from the stable state file.
    /// </summary>
    public static string GetOrCreateInstallId(StateStore stateStore) =>
        stateStore.GetOrCreateTelemetryInstallId();
}
