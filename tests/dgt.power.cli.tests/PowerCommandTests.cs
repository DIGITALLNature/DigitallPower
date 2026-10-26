// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Diagnostics;
using dgt.power.common;
using dgt.power.Telemetry;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Testing;

namespace dgt.power.cli.tests;

[NotInParallel(nameof(PowerCommandTests))]
public class PowerCommandTests
{
    [Before(Test)]
    [After(Test)]
    public Task ResetSuppression()
    {
        TelemetryState.SuppressForInvocation = false;
        return Task.CompletedTask;
    }

    [Test]
    public async Task Execute_ReturningTrue_ReturnsZeroAndClosesSuccessfulSpan()
    {
        var stopped = new List<Activity>();
        using var listener = CreateListener(stopped);
        var (command, console) = Create(_ => Task.FromResult(true));

        var exitCode = await Execute(command);

        await Assert.That(exitCode).IsEqualTo(0);
        await Assert.That(stopped).Count().IsEqualTo(1);
        await Assert.That(stopped[0].OperationName).IsEqualTo("command.ProbeCommand");
        await Assert.That((bool?)stopped[0].GetTagItem("dgtp.success")).IsTrue();
        await Assert.That(stopped[0].Status).IsEqualTo(ActivityStatusCode.Ok);
        await Assert.That(console.Output).Contains($"{Emoji.Known.CheckMark} Command finished in");
    }

    [Test]
    public async Task Execute_ReturningFalse_ReturnsOneAndClosesFailedSpan()
    {
        var stopped = new List<Activity>();
        using var listener = CreateListener(stopped);
        var (command, console) = Create(_ => Task.FromResult(false));

        var exitCode = await Execute(command);

        await Assert.That(exitCode).IsEqualTo(1);
        await Assert.That(stopped).Count().IsEqualTo(1);
        await Assert.That((bool?)stopped[0].GetTagItem("dgtp.success")).IsFalse();
        await Assert.That(stopped[0].Status).IsEqualTo(ActivityStatusCode.Error);
        await Assert.That(console.Output).Contains($"{Emoji.Known.CrossMark} Command failed in");
    }

    [Test]
    public async Task Execute_Throwing_RethrowsRecordsOnSpanAndPrintsEndLine()
    {
        var stopped = new List<Activity>();
        using var listener = CreateListener(stopped);
        var (command, console) = Create(_ => throw new InvalidOperationException("boom"));

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await Execute(command));

        await Assert.That(stopped).Count().IsEqualTo(1);
        await Assert.That(stopped[0].Status).IsEqualTo(ActivityStatusCode.Error);
        await Assert.That(stopped[0].StatusDescription).IsEqualTo("boom");
        await Assert.That(stopped[0].Events.Any(e => e.Name == "exception")).IsTrue();
        await Assert.That(console.Output).Contains($"{Emoji.Known.CrossMark} Command failed in");
    }

    [Test]
    public async Task TrackFatalException_AfterRecordedRunException_DoesNotCreateSecondSpan()
    {
        var stopped = new List<Activity>();
        using var listener = CreateListener(stopped);
        var state = new TelemetryState(true, "id");
        var (command, _) = Create(_ => throw new InvalidOperationException("boom"), state);
        Exception? escaped = null;
        try
        {
            await Execute(command);
        }
        catch (InvalidOperationException exception)
        {
            escaped = exception;
        }

        new FatalExceptionReporter(state).Report(escaped!);

        await Assert.That(stopped).Count().IsEqualTo(1);
    }

    [Test]
    public async Task TrackFatalException_ForUnrecordedException_StillCreatesSpan()
    {
        var stopped = new List<Activity>();
        using var listener = CreateListener(stopped);

        new FatalExceptionReporter(new TelemetryState(true, "id")).Report(new InvalidOperationException("boom"));

        await Assert.That(stopped).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Execute_TelemetrySuppressed_StillRunsAndRecordsNothing()
    {
        var stopped = new List<Activity>();
        using var listener = CreateListener(stopped);
        TelemetryState.SuppressForInvocation = true;
        var (command, console) = Create(_ => Task.FromResult(true));

        var exitCode = await Execute(command);

        await Assert.That(exitCode).IsEqualTo(0);
        await Assert.That(stopped).IsEmpty();
        await Assert.That(console.Output).Contains($"{Emoji.Known.CheckMark} Command finished in");
    }

    private static (ProbeCommand Command, TestConsole Console) Create(
        Func<ProbeSettings, Task<bool>> run,
        TelemetryState? state = null)
    {
        var console = new TestConsole();
        var command = new ProbeCommand(
            new CommandSpanFactory(state ?? new TelemetryState(true, "id")),
            console,
            run);
        return (command, console);
    }

    private static Task<int> Execute(ProbeCommand command) =>
        command.ExecuteAsync(
            new CommandContext([], new RemainingArguments(), "probe", null),
            new ProbeSettings(),
            CancellationToken.None);

    private static ActivityListener CreateListener(List<Activity> stopped)
    {
        var listener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == DgtpActivitySource.Name,
            Sample = (ref _) => ActivitySamplingResult.AllDataAndRecorded,
            ActivityStopped = stopped.Add
        };
        ActivitySource.AddActivityListener(listener);
        return listener;
    }

    private sealed class ProbeSettings : BaseProgramSettings;

    private sealed class ProbeCommand(
        ICommandSpanFactory spans,
        IAnsiConsole console,
        Func<ProbeSettings, Task<bool>> run) : PowerCommand<ProbeSettings>(spans, console)
    {
        protected override Task<bool> RunAsync(ProbeSettings settings, CancellationToken cancellationToken) => run(settings);
    }

    private sealed class RemainingArguments : IRemainingArguments
    {
        public ILookup<string, string?> Parsed { get; } = Array.Empty<string>().ToLookup(_ => string.Empty, _ => (string?)null);

        public IReadOnlyList<string> Raw { get; } = [];
    }
}
