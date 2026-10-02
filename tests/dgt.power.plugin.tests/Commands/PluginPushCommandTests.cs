// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Commands;

namespace dgt.power.plugin.tests.Commands;

public class PluginPushCommandTests
{
    [Test]
    public async Task ShouldContinueAfterTargetFailure_ExpectedValidationFailure_ReturnsTrue()
    {
        await Assert.That(PluginPushCommand.ShouldContinueAfterTargetFailure(new MissingCustomApiException("new_MissingApi"))).IsTrue();
    }

    [Test]
    public async Task ShouldContinueAfterTargetFailure_Cancellation_ReturnsFalse()
    {
        await Assert.That(PluginPushCommand.ShouldContinueAfterTargetFailure(new OperationCanceledException())).IsFalse();
    }
}
