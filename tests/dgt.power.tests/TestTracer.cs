// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;

namespace dgt.power.tests;

public class TestTracer : ITracer
{
    public void Log(string message, TraceEventType type)
    {
        Console.WriteLine($"{type}: {message}");
    }

    public void Start<T>(T action) where T : IPowerLogic
    {
        Console.WriteLine($"{TraceEventType.Information}: ************** {action.GetType().Name} - Start *****************");
    }

    public bool NotConfigured<T>(T action) where T : IPowerLogic
    {
        Console.WriteLine($"{TraceEventType.Information}: ************** {action.GetType().Name} - Not Configured *****************");
        return false;
    }

    public bool Skipped<T>(T action) where T : IPowerLogic
    {
        Console.WriteLine($"{TraceEventType.Information}: ************** {action.GetType().Name} - Skipped *****************");
        return true;
    }

    public bool End<T>(T action, bool result) where T : IPowerLogic
    {
        Console.WriteLine(result ?
            $"{TraceEventType.Information}: ************** {action.GetType().Name} - End:OK *****************" :
            $"{TraceEventType.Error}: ************** {action.GetType().Name} - End:ERROR *****************");
        return result;
    }

    public void Exception(Exception e, TraceEventType type)
    {
        var error = e.RootException();
        Console.WriteLine($"{type}: {error.Message}");
        Console.WriteLine($"{type}: {error.StackTrace}");
    }
}