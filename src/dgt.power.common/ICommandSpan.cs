// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common;

/// <summary>
/// The telemetry span of one command run. Disposing ends the span.
/// </summary>
public interface ICommandSpan : IDisposable
{
    /// <summary>
    /// Records the outcome of the run.
    /// </summary>
    void Complete(bool success);

    /// <summary>
    /// Records an exception that escapes the run and marks it so it is not reported a second time.
    /// </summary>
    void Record(Exception exception);
}
