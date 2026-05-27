// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Telemetry;
using Spectre.Console.Cli;

namespace dgt.power.telemetry.tests;

public class CompositeInterceptorTests
{
    [Test]
    public async Task Intercept_CallsAllInnerInterceptors()
    {
        var called = new List<int>();
        var interceptor1 = new TrackingInterceptor(1, called);
        var interceptor2 = new TrackingInterceptor(2, called);
        var interceptor3 = new TrackingInterceptor(3, called);

        var composite = new CompositeInterceptor(interceptor1, interceptor2, interceptor3);
        var context = new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "test", null);
        var settings = new TestSettings();

        composite.Intercept(context, settings);

        await Assert.That(called).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Intercept_CallsInterceptorsInOrder()
    {
        var called = new List<int>();
        var interceptor1 = new TrackingInterceptor(1, called);
        var interceptor2 = new TrackingInterceptor(2, called);

        var composite = new CompositeInterceptor(interceptor1, interceptor2);
        var context = new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "test", null);
        var settings = new TestSettings();

        composite.Intercept(context, settings);

        await Assert.That(called[0]).IsEqualTo(1);
        await Assert.That(called[1]).IsEqualTo(2);
    }

    [Test]
    public void Intercept_WorksWithNoInterceptors()
    {
        var composite = new CompositeInterceptor();
        var context = new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "test", null);
        var settings = new TestSettings();

        composite.Intercept(context, settings);
    }

    private sealed class TrackingInterceptor(int id, List<int> callLog) : ICommandInterceptor
    {
        public void Intercept(CommandContext context, CommandSettings settings) => callLog.Add(id);
    }

    private sealed class TestSettings : CommandSettings;

    private sealed class EmptyRemainingArguments : IRemainingArguments
    {
        public ILookup<string, string?> Parsed =>
            Array.Empty<KeyValuePair<string, string?>>().ToLookup(x => x.Key, x => x.Value);

        public IReadOnlyList<string> Raw => Array.Empty<string>();
    }
}
