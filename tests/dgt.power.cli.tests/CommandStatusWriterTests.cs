// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Logic;
using Spectre.Console.Testing;

namespace dgt.power.cli.tests;

public class CommandStatusWriterTests
{
    [Test]
    public async Task WriteStart_PrintsSymbolAndCommandName()
    {
        var console = new TestConsole();

        new CommandStatusWriter(console).WriteStart("lint");

        await Assert.That(console.Output.Trim()).IsEqualTo("▶ lint");
    }

    [Test]
    public async Task WriteEnd_Success_PrintsCheckmarkNameAndDuration()
    {
        var console = new TestConsole();

        new CommandStatusWriter(console).WriteEnd("lint", true, TimeSpan.FromSeconds(3.24));

        await Assert.That(console.Output).StartsWith("── ✔ lint · 3.2s ");
    }

    [Test]
    public async Task WriteEnd_Failure_PrintsFailedAndSubSecondDuration()
    {
        var console = new TestConsole();

        new CommandStatusWriter(console).WriteEnd("lint", false, TimeSpan.FromMilliseconds(420));

        await Assert.That(console.Output).StartsWith("── ✖ lint failed · 420ms ");
    }

    [Test]
    public async Task WriteEnd_ConsoleWithoutUnicode_UsesAsciiOnly()
    {
        var console = new TestConsole();
        console.Profile.Capabilities.Unicode = false;
        var writer = new CommandStatusWriter(console);

        writer.WriteStart("lint");
        writer.WriteEnd("lint", false, TimeSpan.FromSeconds(1));

        await Assert.That(console.Output).Contains("> lint");
        await Assert.That(console.Output.Where(c => c >= 128).ToArray()).IsEmpty();
        await Assert.That(console.Output).Contains("FAIL lint failed - 1.0s");
    }

    [Test]
    public async Task WriteEnd_NoAnsiSupport_ProducesReadableText()
    {
        var console = new TestConsole().EmitAnsiSequences();
        console.Profile.Capabilities.Ansi = false;

        new CommandStatusWriter(console).WriteEnd("lint", true, TimeSpan.FromSeconds(1));

        await Assert.That(console.Output).DoesNotContain("\u001b");
        await Assert.That(console.Output).Contains("✔ lint");
    }
}
