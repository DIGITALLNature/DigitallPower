// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Commands;
using dgt.power.tests;
using dgt.power.tests.Extensions;

namespace dgt.power.plugin.tests.Commands;

public class PluginStepDisableCommandTests : CommandTestsBase<PluginStepDisableCommand, PluginStepEnableDisableSettings>
{
    private readonly Guid _stepId = Guid.NewGuid();

    protected override CommandTestContextBuilder<PluginStepDisableCommand, PluginStepEnableDisableSettings> GetBuilder()
    {
        var builder = base.GetBuilder();
        foreach (var metadata in PluginStepStateTestData.Metadata())
        {
            builder = builder.WithMetaData(metadata);
        }

        return builder;
    }

    [Test]
    public async Task Execute_EnabledStepWithReason_DisablesStepAndPersistsReason()
    {
        var context = GetBuilder().WithData(PluginStepStateTestData.CreateStep(_stepId, "TestStep", enabled: true)).Build();

        await context.Execute(new PluginStepEnableDisableSettings { StepId = _stepId, Reason = "hotfix" }).Succeed();

        var step = context.GetById<SdkMessageProcessingStep>(_stepId);
        await Assert.That(PluginStepStateTestData.IsEnabled(step)).IsFalse();
        await Assert.That(step.StatusCode?.Value).IsEqualTo(SdkMessageProcessingStep.Options.StatusCode.Disabled);
        await Assert.That(step.Description).IsEqualTo("hotfix");
        await Assert.That(TestConsole.Output).Contains("Disabled TestStep - hotfix");
    }

    [Test]
    public async Task Execute_AlreadyDisabledStep_ReportsUnchanged()
    {
        var context = GetBuilder().WithData(PluginStepStateTestData.CreateStep(_stepId, "TestStep", enabled: false)).Build();

        await context.Execute(new PluginStepEnableDisableSettings { StepId = _stepId }).Succeed();

        await Assert.That(TestConsole.Output).Contains("Already disabled TestStep");
    }
}
