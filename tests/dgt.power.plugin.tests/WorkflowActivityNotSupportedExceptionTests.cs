// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Exceptions;

namespace dgt.power.plugin.tests;

public class WorkflowActivityNotSupportedExceptionTests
{
    [Test]
    public async Task Message_ContainsAssemblyNameTypeNamesAndFallbackGuidance()
    {
        var exception = new WorkflowActivityNotSupportedException("MyPlugins", ["MyPlugins.MyWorkflowActivity"]);

        await Assert.That(exception.Message).Contains("MyPlugins");
        await Assert.That(exception.Message).Contains("MyPlugins.MyWorkflowActivity");
        await Assert.That(exception.Message).Contains("Custom API");
        await Assert.That(exception.Message).Contains("push");
        await Assert.That(exception.Message).StartsWith("NOT_SUPPORTED:");
    }
}
