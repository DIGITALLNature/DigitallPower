// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.Telemetry;
using Spectre.Console.Cli;

namespace dgt.power.cli.tests;

[NotInParallel(nameof(TracerTests))]
public class TelemetryInterceptorTests
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
    public async Task Intercept_SetsSuppression_WhenNoTelemetryIsTrue()
    {
        var interceptor = new TelemetryInterceptor();
        var settings = new TestSettings { NoTelemetry = true };
        var context = CreateCommandContext();

        interceptor.Intercept(context, settings);

        await Assert.That(Tracer.SuppressForInvocation).IsTrue();
    }

    [Test]
    public async Task Intercept_DoesNotSetSuppression_WhenNoTelemetryIsFalse()
    {
        var interceptor = new TelemetryInterceptor();
        var settings = new TestSettings { NoTelemetry = false };
        var context = CreateCommandContext();

        interceptor.Intercept(context, settings);

        await Assert.That(Tracer.SuppressForInvocation).IsFalse();
    }

    [Test]
    public async Task Intercept_DoesNotSetSuppression_WhenSettingsIsNotBaseProgramSettings()
    {
        var interceptor = new TelemetryInterceptor();
        var settings = new NonBaseSettings();
        var context = CreateCommandContext();

        interceptor.Intercept(context, settings);

        await Assert.That(Tracer.SuppressForInvocation).IsFalse();
    }

    private static CommandContext CreateCommandContext()
    {
        return new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "test", null);
    }

    private sealed class TestSettings : BaseProgramSettings;

    private sealed class NonBaseSettings : CommandSettings;

    private sealed class EmptyRemainingArguments : IRemainingArguments
    {
        public ILookup<string, string?> Parsed =>
            Array.Empty<KeyValuePair<string, string?>>().ToLookup(x => x.Key, x => x.Value);

        public IReadOnlyList<string> Raw => Array.Empty<string>();
    }
}
