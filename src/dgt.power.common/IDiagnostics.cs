// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;

#pragma warning disable CA1716

namespace dgt.power.common;

/// <summary>
/// Reports messages and handled exceptions to the console and to the current telemetry span.
/// </summary>
public interface IDiagnostics
{
    void Log(string message, TraceEventType type);

    /// <summary>
    /// Writes the exception to the console and records it on the current span, if any.
    /// </summary>
    void Exception(Exception exception, TraceEventType type);
}
