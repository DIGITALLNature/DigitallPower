// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power;
using dgt.power.common.Commands;
using Spectre.Console.Cli;
using Spectre.Console.Testing;

namespace dgt.power.cli.tests;

public class DeprecationInterceptorTests
{
    [Test]
    public async Task Intercept_PrintsWarningWithReplacement_WhenSettingsAreDeprecatedWithUseInstead()
    {
        var testConsole = new TestConsole();
        var interceptor = new DeprecationInterceptor(testConsole);

        interceptor.Intercept(CreateCommandContext(), new DeprecatedWithReplacementSettings());

        await Assert.That(testConsole.Output).Contains("DEPRECATED");
        await Assert.That(testConsole.Output).Contains("Use 'replacement' instead.");
    }

    [Test]
    public async Task Intercept_PrintsWarningWithoutReplacementHint_WhenSettingsAreDeprecatedWithoutUseInstead()
    {
        var testConsole = new TestConsole();
        var interceptor = new DeprecationInterceptor(testConsole);

        interceptor.Intercept(CreateCommandContext(), new DeprecatedWithoutReplacementSettings());

        await Assert.That(testConsole.Output).Contains("DEPRECATED");
        await Assert.That(testConsole.Output).DoesNotContain("Use '");
    }

    [Test]
    public async Task Intercept_PrintsWarning_WhenSubclassInheritsDeprecationFromBaseSettings()
    {
        var testConsole = new TestConsole();
        var interceptor = new DeprecationInterceptor(testConsole);

        // The leaf settings type has no attribute of its own - it is inherited from the deprecated base,
        // mirroring how every 'profile' command's settings type derives from the deprecated ProfileSettings.
        interceptor.Intercept(CreateCommandContext(), new InheritedDeprecationSettings());

        await Assert.That(testConsole.Output).Contains("DEPRECATED");
        await Assert.That(testConsole.Output).Contains("Use 'replacement' instead.");
    }

    [Test]
    public async Task Intercept_DoesNothing_WhenSettingsAreNotDeprecated()
    {
        var testConsole = new TestConsole();
        var interceptor = new DeprecationInterceptor(testConsole);

        interceptor.Intercept(CreateCommandContext(), new NotDeprecatedSettings());

        await Assert.That(testConsole.Output).IsEmpty();
    }

    private static CommandContext CreateCommandContext() =>
        new(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "test", null);

    [DeprecatedCommand("replacement")]
    private sealed class DeprecatedWithReplacementSettings : CommandSettings;

    [DeprecatedCommand]
    private sealed class DeprecatedWithoutReplacementSettings : CommandSettings;

    [DeprecatedCommand("replacement")]
    private class DeprecatedBaseSettings : CommandSettings;

    private sealed class InheritedDeprecationSettings : DeprecatedBaseSettings;

    private sealed class NotDeprecatedSettings : CommandSettings;

    private sealed class EmptyRemainingArguments : IRemainingArguments
    {
        public ILookup<string, string?> Parsed =>
            Array.Empty<KeyValuePair<string, string?>>().ToLookup(x => x.Key, x => x.Value);

        public IReadOnlyList<string> Raw => Array.Empty<string>();
    }
}
