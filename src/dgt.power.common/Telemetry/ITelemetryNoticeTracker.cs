// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.common.Telemetry;

/// <summary>
/// Tracks whether the telemetry notice has been displayed during the current
/// invocation. Callers use this to avoid rendering the notice more than once.
/// </summary>
public interface ITelemetryNoticeTracker
{
    /// <summary>True when the telemetry notice has already been shown in this invocation.</summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    bool IsNoticeShown { get; set; }
}
