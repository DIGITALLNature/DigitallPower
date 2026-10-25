// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
