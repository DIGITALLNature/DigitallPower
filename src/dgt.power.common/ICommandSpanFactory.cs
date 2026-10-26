// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.common;

/// <summary>
/// The only place that starts telemetry spans for command runs.
/// </summary>
public interface ICommandSpanFactory
{
    /// <summary>
    /// Starts the span for a run. Returns a no-op span when telemetry is disabled or suppressed.
    /// </summary>
    ICommandSpan Start(string commandName);
}
