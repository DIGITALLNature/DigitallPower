// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
