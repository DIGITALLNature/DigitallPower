// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;

#pragma warning disable CA1716

namespace dgt.power.common;

public interface ITracer
{
    void Log(string message, TraceEventType type);

    void Start<T>(T action) where T : IPowerLogic;

    bool NotConfigured<T>(T action) where T : IPowerLogic;

    bool Skipped<T>(T action) where T : IPowerLogic;

    bool End<T>(T action, bool result) where T : IPowerLogic;

    void Exception(Exception e, TraceEventType type);
}
