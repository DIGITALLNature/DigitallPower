// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Commands;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Commands;

public class PluginPushConfirmationTests
{
    [Test]
    [Arguments(false, false, false, false)]
    [Arguments(true, true, false, false)]
    [Arguments(true, false, true, false)]
    [Arguments(true, false, false, true)]
    public async Task ShouldPrompt_ExecutionMode_ExpectedResult(
        bool confirm,
        bool nonInteractive,
        bool isCiAgent,
        bool expected)
    {
        var result = PluginPushConfirmation.ShouldPrompt(confirm, nonInteractive, isCiAgent);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("y", true)]
    [Arguments("n", false)]
    public async Task Confirm_UserResponse_ReturnsSelection(string response, bool expected)
    {
        using var console = new TestConsole();
        console.Input.PushTextWithEnter(response);

        var result = PluginPushConfirmation.Confirm(console, "MyPlugin.dll");

        await Assert.That(result).IsEqualTo(expected);
    }
}
