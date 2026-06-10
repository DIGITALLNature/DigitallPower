// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.Telemetry;

namespace dgt.power.telemetry.tests;

[NotInParallel(nameof(TracerTests))]
public class TracerTests
{
    [Before(Test)]
    public Task ResetSuppression()
    {
        Tracer.SuppressForInvocation = false;
        return Task.CompletedTask;
    }

    [After(Test)]
    public Task CleanupSuppression()
    {
        Tracer.SuppressForInvocation = false;
        return Task.CompletedTask;
    }

    [Test]
    public async Task Start_CreatesActivity_WhenTelemetryEnabled()
    {
        var activities = new List<Activity>();
        using var listener = CreateListener(started: activities);

        var tracer = new Tracer(telemetryEnabled: true, installId: "test-id-123");
        var action = new FakeAction();

        tracer.Start(action);

        await Assert.That(activities).Count().IsEqualTo(1);
        await Assert.That(activities[0].OperationName).IsEqualTo("command.FakeAction");

        // Cleanup
        tracer.End(action, true);
    }

    [Test]
    public async Task Start_DoesNotCreateActivity_WhenTelemetryDisabled()
    {
        var activities = new List<Activity>();
        using var listener = CreateListener(started: activities);

        var tracer = new Tracer();
        var action = new FakeAction();

        tracer.Start(action);

        await Assert.That(activities).IsEmpty();

        tracer.End(action, true);
    }

    [Test]
    public async Task Start_DoesNotCreateActivity_WhenSuppressed()
    {
        var activities = new List<Activity>();
        using var listener = CreateListener(started: activities);

        Tracer.SuppressForInvocation = true;
        var tracer = new Tracer(telemetryEnabled: true, installId: "test-id");
        var action = new FakeAction();

        tracer.Start(action);

        await Assert.That(activities).IsEmpty();

        tracer.End(action, true);
    }

    [Test]
    public async Task Start_SetsExpectedTags()
    {
        var activities = new List<Activity>();
        using var listener = CreateListener(started: activities);

        var tracer = new Tracer(telemetryEnabled: true, installId: "my-install-id");
        var action = new FakeAction();

        tracer.Start(action);

        var activity = activities[0];
        await Assert.That(activity.GetTagItem("dgtp.command") as string).IsEqualTo("FakeAction");
        await Assert.That(activity.GetTagItem("dgtp.os") as string).IsNotNull();
        await Assert.That(activity.GetTagItem("dgtp.version") as string).IsNotNull();
        await Assert.That(activity.GetTagItem("dgtp.install_id") as string).IsEqualTo("my-install-id");

        tracer.End(action, true);
    }

    [Test]
    public async Task End_SetsSuccessTag_WhenResultIsTrue()
    {
        var stoppedActivities = new List<Activity>();
        using var listener = CreateListener(stopped: stoppedActivities);

        var tracer = new Tracer(telemetryEnabled: true, installId: "test-id");
        var action = new FakeAction();

        tracer.Start(action);
        tracer.End(action, true);

        await Assert.That(stoppedActivities).Count().IsEqualTo(1);
        await Assert.That((bool?)stoppedActivities[0].GetTagItem("dgtp.success")).IsTrue();
        await Assert.That(stoppedActivities[0].Status).IsEqualTo(ActivityStatusCode.Ok);
    }

    [Test]
    public async Task End_SetsErrorStatus_WhenResultIsFalse()
    {
        var stoppedActivities = new List<Activity>();
        using var listener = CreateListener(stopped: stoppedActivities);

        var tracer = new Tracer(telemetryEnabled: true, installId: "test-id");
        var action = new FakeAction();

        tracer.Start(action);
        tracer.End(action, false);

        await Assert.That(stoppedActivities).Count().IsEqualTo(1);
        await Assert.That((bool?)stoppedActivities[0].GetTagItem("dgtp.success")).IsFalse();
        await Assert.That(stoppedActivities[0].Status).IsEqualTo(ActivityStatusCode.Error);
    }

    [Test]
    public async Task NotConfigured_SetsErrorStatus()
    {
        var stoppedActivities = new List<Activity>();
        using var listener = CreateListener(stopped: stoppedActivities);

        var tracer = new Tracer(telemetryEnabled: true, installId: "test-id");
        var action = new FakeAction();

        tracer.Start(action);
        var result = tracer.NotConfigured(action);

        await Assert.That(result).IsFalse();
        await Assert.That(stoppedActivities).Count().IsEqualTo(1);
        await Assert.That((bool?)stoppedActivities[0].GetTagItem("dgtp.success")).IsFalse();
    }

    [Test]
    public async Task Skipped_SetsOkStatus()
    {
        var stoppedActivities = new List<Activity>();
        using var listener = CreateListener(stopped: stoppedActivities);

        var tracer = new Tracer(telemetryEnabled: true, installId: "test-id");
        var action = new FakeAction();

        tracer.Start(action);
        var result = tracer.Skipped(action);

        await Assert.That(result).IsTrue();
        await Assert.That(stoppedActivities).Count().IsEqualTo(1);
        await Assert.That((bool?)stoppedActivities[0].GetTagItem("dgtp.success")).IsTrue();
    }

    [Test]
    public async Task End_ReturnsPassedResult()
    {
        using var listener = CreateListener();
        var tracer = new Tracer(telemetryEnabled: true, installId: "test-id");
        var action = new FakeAction();

        tracer.Start(action);
        var resultTrue = tracer.End(action, true);

        await Assert.That(resultTrue).IsTrue();

        tracer.Start(action);
        var resultFalse = tracer.End(action, false);

        await Assert.That(resultFalse).IsFalse();
    }

    [Test]
    public async Task Exception_SetsErrorStatusOnActivity()
    {
        var stoppedActivities = new List<Activity>();
        using var listener = CreateListener(stopped: stoppedActivities);

        var tracer = new Tracer(telemetryEnabled: true, installId: "test-id");
        var action = new FakeAction();

        tracer.Start(action);
        tracer.Exception(new InvalidOperationException("Something failed"), TraceEventType.Error);
        tracer.End(action, false);

        await Assert.That(stoppedActivities).Count().IsEqualTo(1);
        await Assert.That(stoppedActivities[0].Status).IsEqualTo(ActivityStatusCode.Error);
        await Assert.That(stoppedActivities[0].StatusDescription).IsEqualTo("Something failed");
    }

    private static ActivityListener CreateListener(List<Activity>? started = null, List<Activity>? stopped = null)
    {
        var listener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == DgtpActivitySource.Name,
            Sample = (ref _) => ActivitySamplingResult.AllDataAndRecorded,
            ActivityStarted = activity => started?.Add(activity),
            ActivityStopped = activity => stopped?.Add(activity)
        };
        ActivitySource.AddActivityListener(listener);
        return listener;
    }

    private sealed class FakeAction : IPowerLogic;
}
