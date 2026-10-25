// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Storage;
using dgt.power.common.Telemetry;
using dgt.power.tests;
using Spectre.Console.Testing;

namespace dgt.power.telemetry.tests;

public abstract class TelemetryTestBase : IDisposable
{
    private readonly string _directory;
    private readonly Dictionary<string, string?> _savedEnvVars = new(StringComparer.Ordinal);

    protected TelemetryTestBase()
    {
        _directory = Path.Combine(Path.GetTempPath(), $"dgtp-telemetry-tests-{Guid.NewGuid():N}");
        Home = new DgtpHome(_directory);
        StateStore = new StateStore(Home);
        TelemetryState = new TelemetryStateProvider(StateStore);
        NoticeTracker = new TestNoticeTracker();
        TestConsole = new TestConsole();
        SpanFactory = new TestSpanFactory();
    }

    protected DgtpHome Home { get; }

    protected StateStore StateStore { get; }

    protected TelemetryStateProvider TelemetryState { get; }

    protected TestNoticeTracker NoticeTracker { get; }

    protected TestConsole TestConsole { get; }

    protected TestSpanFactory SpanFactory { get; }

    protected sealed class TestNoticeTracker : ITelemetryNoticeTracker
    {
        public bool IsNoticeShown { get; set; }
    }

    protected void SetEnvVar(string name, string? value)
    {
        if (!_savedEnvVars.ContainsKey(name))
        {
            _savedEnvVars[name] = Environment.GetEnvironmentVariable(name);
        }

        Environment.SetEnvironmentVariable(name, value);
    }

    protected void ClearTelemetryEnvVars()
    {
        SetEnvVar("DGTP_TELEMETRY_OPTOUT", null);
        SetEnvVar("DO_NOT_TRACK", null);
    }

    public virtual void Dispose()
    {
        foreach (var (name, value) in _savedEnvVars)
        {
            Environment.SetEnvironmentVariable(name, value);
        }

        TestConsole.Dispose();

        if (Directory.Exists(_directory))
        {
            Directory.Delete(_directory, recursive: true);
        }

        GC.SuppressFinalize(this);
    }
}
