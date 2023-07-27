// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using Xunit.Abstractions;

namespace dgt.power.tests;

public class TestTracer : ITracer
{
    private readonly ITestOutputHelper? _testOutputHelper;

    public TestTracer(ITestOutputHelper? testOutputHelper = null)
    {
        _testOutputHelper = testOutputHelper;
    }

    public void Log(string message, TraceEventType type)
    {
        _testOutputHelper?.WriteLine($"{type}: {message}");
    }

    public void Start<T>(T action) where T : IPowerLogic
    {
        _testOutputHelper?.WriteLine($"{TraceEventType.Information}: ************** {action.GetType().Name} - Start *****************");
    }

    public bool NotConfigured<T>(T action) where T : IPowerLogic
    {
        _testOutputHelper?.WriteLine($"{TraceEventType.Information}: ************** {action.GetType().Name} - Not Configured *****************");
        return false;
    }

    public bool Skipped<T>(T action) where T : IPowerLogic
    {
        _testOutputHelper?.WriteLine($"{TraceEventType.Information}: ************** {action.GetType().Name} - Skipped *****************");
        return true;
    }

    public bool End<T>(T action, bool result) where T : IPowerLogic
    {
        _testOutputHelper?.WriteLine(result ? 
            $"{TraceEventType.Information}: ************** {action.GetType().Name} - End:OK *****************" : 
            $"{TraceEventType.Error}: ************** {action.GetType().Name} - End:ERROR *****************");
        return result;
    }

    public void Exception(Exception e, TraceEventType type)
    {
        var error = e.RootException();
        _testOutputHelper?.WriteLine($"{type}: {error.Message}");
        _testOutputHelper?.WriteLine($"{type}: {error.StackTrace}");
    }
}