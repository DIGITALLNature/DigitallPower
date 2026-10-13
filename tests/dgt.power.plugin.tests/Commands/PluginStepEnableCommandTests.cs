// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Commands;
using dgt.power.tests;
using dgt.power.tests.Extensions;

namespace dgt.power.plugin.tests.Commands;

public class PluginStepEnableCommandTests : CommandTestsBase<PluginStepEnableCommand, PluginStepEnableDisableSettings>
{
    private readonly Guid _stepId = Guid.NewGuid();

    protected override CommandTestContextBuilder<PluginStepEnableCommand, PluginStepEnableDisableSettings> GetBuilder()
    {
        var builder = base.GetBuilder();
        foreach (var metadata in PluginStepStateTestData.Metadata())
        {
            builder = builder.WithMetaData(metadata);
        }

        return builder;
    }

    [Test]
    public async Task Execute_DisabledStep_EnablesStep()
    {
        var context = GetBuilder().WithData(PluginStepStateTestData.CreateStep(_stepId, "TestStep", enabled: false)).Build();

        await context.Execute(new PluginStepEnableDisableSettings { StepId = _stepId }).Succeed();

        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_stepId))).IsTrue();
        await Assert.That(TestConsole.Output).Contains("Enabled TestStep");
    }

    [Test]
    public async Task Execute_CompositeKey_EnablesResolvedStep()
    {
        var context = GetBuilder().WithData(PluginStepStateTestData.CreateStep(_stepId, "TestStep", enabled: false)).Build();

        await context.Execute(new PluginStepEnableDisableSettings
        {
            PluginType = "TestPluginType", Message = "Create", Stage = PluginStepStage.PreOperation, Entity = "account"
        }).Succeed();

        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_stepId))).IsTrue();
    }

    [Test]
    public async Task Execute_AlreadyEnabledStep_DoesNotWriteReasonOrState()
    {
        var context = GetBuilder().WithData(PluginStepStateTestData.CreateStep(_stepId, "TestStep", enabled: true, description: "original")).Build();

        await context.Execute(new PluginStepEnableDisableSettings { StepId = _stepId, Reason = "new reason" }).Succeed();

        await Assert.That(context.GetById<SdkMessageProcessingStep>(_stepId).Description).IsEqualTo("original");
        await Assert.That(TestConsole.Output).Contains("Already enabled TestStep");
    }

    [Test]
    public async Task Execute_WithReason_PersistsReasonAndPrintsItLiterally()
    {
        var context = GetBuilder().WithData(PluginStepStateTestData.CreateStep(_stepId, "TestStep", enabled: false)).Build();

        await context.Execute(new PluginStepEnableDisableSettings { StepId = _stepId, Reason = "[red]maintenance[/] done" }).Succeed();

        await Assert.That(context.GetById<SdkMessageProcessingStep>(_stepId).Description).IsEqualTo("[red]maintenance[/] done");
        await Assert.That(TestConsole.Output).Contains("Enabled TestStep - [red]maintenance[/] done");
    }

    [Test]
    public async Task Execute_DryRun_ReportsWithoutWriting()
    {
        var context = GetBuilder().WithData(PluginStepStateTestData.CreateStep(_stepId, "TestStep", enabled: false)).Build();

        await context.Execute(new PluginStepEnableDisableSettings { StepId = _stepId, Reason = "reason", DryRun = true }).Succeed();

        var step = context.GetById<SdkMessageProcessingStep>(_stepId);
        await Assert.That(PluginStepStateTestData.IsEnabled(step)).IsFalse();
        await Assert.That(step.Description).IsNull();
        await Assert.That(TestConsole.Output).Contains("DRY RUN");
        await Assert.That(TestConsole.Output).Contains("Would enable TestStep");
    }

    [Test]
    public async Task Execute_UnknownStepId_Fails()
    {
        var context = GetBuilder().Build();

        await context.Execute(new PluginStepEnableDisableSettings { StepId = Guid.NewGuid() }).Fail();

        await Assert.That(TestConsole.Output).Contains("No plugin step matches the supplied criteria.");
    }

    [Test]
    public async Task Execute_AmbiguousCompositeKey_FailsWithoutWriting()
    {
        var entities = PluginStepStateTestData.CreateStep(_stepId, "First", enabled: false);
        var otherId = Guid.NewGuid();
        entities.AddRange(PluginStepStateTestData.CreateStep(otherId, "Second", enabled: false, rank: 2));
        var context = GetBuilder().WithData(entities).Build();

        await context.Execute(new PluginStepEnableDisableSettings
        {
            PluginType = "TestPluginType", Message = "Create", Stage = PluginStepStage.PreOperation, Entity = "account"
        }).Fail();

        await Assert.That(TestConsole.Output).Contains("Multiple plugin steps match.");
        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_stepId))).IsFalse();
        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(otherId))).IsFalse();
    }

    [Test]
    public async Task Execute_Solution_EnablesOnlyStepsOfThatSolutionAndSkipsEnabledOnes()
    {
        var inSolutionDisabled = Guid.NewGuid();
        var inSolutionEnabled = Guid.NewGuid();
        var outsideSolution = Guid.NewGuid();
        var entities = PluginStepStateTestData.CreateStep(inSolutionDisabled, "A", enabled: false);
        entities.AddRange(PluginStepStateTestData.CreateStep(inSolutionEnabled, "B", enabled: true));
        entities.AddRange(PluginStepStateTestData.CreateStep(outsideSolution, "C", enabled: false));
        entities.AddRange(PluginStepStateTestData.CreateSolution("my_solution", inSolutionDisabled, inSolutionEnabled));
        var context = GetBuilder().WithData(entities).Build();

        await context.Execute(new PluginStepEnableDisableSettings { Solution = "my_solution" }).Succeed();

        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(inSolutionDisabled))).IsTrue();
        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(outsideSolution))).IsFalse();
        await Assert.That(TestConsole.Output).Contains("1 steps enabled");
        await Assert.That(TestConsole.Output).Contains("1 steps already in the desired state");
    }

    [Test]
    public async Task Execute_SolutionDryRun_DoesNotWrite()
    {
        var entities = PluginStepStateTestData.CreateStep(_stepId, "A", enabled: false);
        entities.AddRange(PluginStepStateTestData.CreateSolution("my_solution", _stepId));
        var context = GetBuilder().WithData(entities).Build();

        await context.Execute(new PluginStepEnableDisableSettings { Solution = "my_solution", DryRun = true }).Succeed();

        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_stepId))).IsFalse();
        await Assert.That(TestConsole.Output).Contains("1 steps would be enabled");
    }

    [Test]
    [Arguments("missing_solution")]
    [Arguments("%")]
    [Arguments("my_solutio_")]
    public async Task Execute_SolutionNameIsMatchedExactly_UnknownSolutionFails(string solutionName)
    {
        var entities = PluginStepStateTestData.CreateStep(_stepId, "A", enabled: false);
        entities.AddRange(PluginStepStateTestData.CreateSolution("my_solution", _stepId));
        var context = GetBuilder().WithData(entities).Build();

        await context.Execute(new PluginStepEnableDisableSettings { Solution = solutionName }).Fail();

        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_stepId))).IsFalse();
    }

    [Test]
    public async Task Execute_SolutionWithoutSteps_SucceedsWithHint()
    {
        var context = GetBuilder().WithData(PluginStepStateTestData.CreateSolution("empty_solution")).Build();

        await context.Execute(new PluginStepEnableDisableSettings { Solution = "empty_solution" }).Succeed();

        await Assert.That(TestConsole.Output).Contains("No plugin steps found in solution 'empty_solution'");
    }
}
