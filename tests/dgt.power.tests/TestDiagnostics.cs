// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;

namespace dgt.power.tests;

public sealed class TestDiagnostics : IDiagnostics
{
    public void Log(string message, TraceEventType type) => Console.WriteLine($"{type}: {message}");

    public void Exception(Exception exception, TraceEventType type)
    {
        ArgumentNullException.ThrowIfNull(exception);
        var error = exception.RootException();
        Console.WriteLine($"{type}: {error.Message}");
        Console.WriteLine($"{type}: {error.StackTrace}");
    }
}
