// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Commands;
using dgt.power.webresource.Local;
using dgt.power.webresource.Planning;
using dgt.power.webresource.Remote;
using Spectre.Console.Testing;

namespace dgt.power.webresource.tests.Commands;

public class WebResourcePushConfirmationTests
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
        var result = WebResourcePushConfirmation.ShouldPrompt(confirm, nonInteractive, isCiAgent);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("y", true)]
    [Arguments("n", false)]
    public async Task Confirm_UserResponse_ReturnsSelection(string response, bool expected)
    {
        using var console = new TestConsole();
        console.Input.PushTextWithEnter(response);

        var result = WebResourcePushConfirmation.Confirm(console, "webresources");

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task HasChanges_PlanWithoutOperations_ReturnsFalse()
    {
        var plan = new WebResourcePushPlan(
            [new WebResourcePlanItem(CreateLocal(), WebResourceAction.Unchanged, null, false)],
            [],
            null);

        await Assert.That(WebResourcePushConfirmation.HasChanges(plan)).IsFalse();
    }

    [Test]
    public async Task HasChanges_ObsoleteResource_ReturnsTrue()
    {
        var obsolete = new RemoteSolutionWebResource(Guid.NewGuid(), 3, "contoso_/old.js", false);
        var plan = new WebResourcePushPlan([], [obsolete], "ContosoCore");

        await Assert.That(WebResourcePushConfirmation.HasChanges(plan)).IsTrue();
    }

    [Test]
    public async Task HasChanges_MissingSolutionMembership_ReturnsTrue()
    {
        var plan = new WebResourcePushPlan(
            [new WebResourcePlanItem(CreateLocal(), WebResourceAction.Unchanged, null, true)],
            [],
            "ContosoCore");

        await Assert.That(WebResourcePushConfirmation.HasChanges(plan)).IsTrue();
    }

    private static LocalWebResource CreateLocal() =>
        new(3, "contoso_/main.js", "main.js", "Y29udGVudA==", "hash", "main.js");
}
