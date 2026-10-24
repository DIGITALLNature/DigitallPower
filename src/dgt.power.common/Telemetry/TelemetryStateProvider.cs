// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Storage;

namespace dgt.power.common.Telemetry;

/// <summary>
/// Single source of truth for telemetry state: combines the persisted preference
/// in <see cref="StateStore"/> with environment opt-out variables.
/// </summary>
public sealed class TelemetryStateProvider(StateStore stateStore)
{
    private const string OptOutEnvVar = "DGTP_TELEMETRY_OPTOUT";
    private const string DoNotTrackEnvVar = "DO_NOT_TRACK";

    /// <summary>
    /// True when an opt-out environment variable (<c>DGTP_TELEMETRY_OPTOUT</c> or
    /// <c>DO_NOT_TRACK</c>) is set to a truthy value.
    /// </summary>
    public static bool HasOptOutEnvVar =>
        ExecutionEnvironment.IsTruthy(Environment.GetEnvironmentVariable(OptOutEnvVar))
        || ExecutionEnvironment.IsTruthy(Environment.GetEnvironmentVariable(DoNotTrackEnvVar));

    /// <summary>
    /// True when telemetry is effectively enabled: the persisted preference is enabled
    /// and no opt-out environment variable is set.
    /// </summary>
    public bool IsEnabled => stateStore.TelemetryEnabled && !HasOptOutEnvVar;

    /// <summary>
    /// Persists the telemetry preference in <c>state.json</c>.
    /// </summary>
    public void SetEnabled(bool enabled) => stateStore.SetTelemetryEnabled(enabled);
}
