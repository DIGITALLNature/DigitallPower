// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Commands;
using dgt.power.plugin.StepManagement;
using dgt.power.tests.Extensions;
using dgt.power.tests;

namespace dgt.power.plugin.tests.Commands;

public class PluginStepStateSetCommandTests : CommandTestsBase<PluginStepStateSetCommand, PluginStepStateSetSettings>
{
    private readonly Guid _firstId = Guid.NewGuid();
    private readonly Guid _secondId = Guid.NewGuid();

    protected override CommandTestContextBuilder<PluginStepStateSetCommand, PluginStepStateSetSettings> GetBuilder()
    {
        var builder = base.GetBuilder();
        foreach (var metadata in PluginStepStateTestData.Metadata())
        {
            builder = builder.WithMetaData(metadata);
        }

        return builder;
    }

    private List<Microsoft.Xrm.Sdk.Entity> TwoSteps(bool firstEnabled, bool secondEnabled)
    {
        var entities = PluginStepStateTestData.CreateStep(_firstId, "First", firstEnabled, pluginTypeName: "First.Plugin");
        entities.AddRange(PluginStepStateTestData.CreateStep(_secondId, "Second", secondEnabled, pluginTypeName: "Second.Plugin"));
        return entities;
    }

    private PluginStepStateSetSettings ConfigFile(params PluginStepStateConfig[] steps) =>
        new() { Config = WriteConfigurationArtifact(new PluginStepStateConfigRoot { Steps = steps }).FullName };

    [Test]
    public async Task Execute_MixedEntries_AppliesStatesAndPrintsSummary()
    {
        var context = GetBuilder().WithData(TwoSteps(firstEnabled: false, secondEnabled: true)).Build();

        await context.Execute(ConfigFile(
            new PluginStepStateConfig { StepId = _firstId, Enabled = true, Reason = "[bold]go[/]" },
            new PluginStepStateConfig { PluginType = "Second.Plugin", Message = "Create", Stage = 20, Entity = "account", Enabled = false })).Succeed();

        var first = context.GetById<SdkMessageProcessingStep>(_firstId);
        await Assert.That(PluginStepStateTestData.IsEnabled(first)).IsTrue();
        await Assert.That(first.Description).IsEqualTo("[bold]go[/]");
        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_secondId))).IsFalse();
        await Assert.That(TestConsole.Output).Contains("Enabled First - [bold]go[/]");
        await Assert.That(TestConsole.Output).Contains("1 steps enabled");
        await Assert.That(TestConsole.Output).Contains("1 steps disabled");
    }

    [Test]
    public async Task Execute_MissingEnabled_RejectsEntryAndLeavesStepUntouched()
    {
        var context = GetBuilder().WithData(TwoSteps(firstEnabled: true, secondEnabled: true)).Build();

        await context.Execute(ConfigFile(new PluginStepStateConfig { StepId = _firstId })).Fail();

        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_firstId))).IsTrue();
        await Assert.That(TestConsole.Output).Contains("'enabled' is required.");
        await Assert.That(TestConsole.Output).Contains("1 steps failed");
    }

    [Test]
    public async Task Execute_StepIdWithCompositeFields_RejectsEntry()
    {
        var context = GetBuilder().WithData(TwoSteps(firstEnabled: true, secondEnabled: true)).Build();

        await context.Execute(ConfigFile(new PluginStepStateConfig { StepId = _firstId, PluginType = "Second.Plugin", Enabled = false })).Fail();

        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_firstId))).IsTrue();
        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_secondId))).IsTrue();
        await Assert.That(TestConsole.Output).Contains("cannot be combined with composite key fields");
    }

    [Test]
    public async Task Execute_IncompleteCompositeKey_RejectsEntryInsteadOfMatchingBroadly()
    {
        var context = GetBuilder().WithData(TwoSteps(firstEnabled: true, secondEnabled: true)).Build();

        await context.Execute(ConfigFile(new PluginStepStateConfig { Message = "Create", Enabled = false })).Fail();

        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_firstId))).IsTrue();
        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_secondId))).IsTrue();
        await Assert.That(TestConsole.Output).Contains("Either 'stepId' or all of");
    }

    [Test]
    public async Task Execute_UnresolvableEntry_ContinuesWithRemainingEntries()
    {
        var context = GetBuilder().WithData(TwoSteps(firstEnabled: true, secondEnabled: true)).Build();

        await context.Execute(ConfigFile(
            new PluginStepStateConfig { StepId = Guid.NewGuid(), Enabled = false },
            new PluginStepStateConfig { StepId = _secondId, Enabled = false })).Fail();

        await Assert.That(PluginStepStateTestData.IsEnabled(context.GetById<SdkMessageProcessingStep>(_secondId))).IsFalse();
        await Assert.That(TestConsole.Output).Contains("1 steps failed");
        await Assert.That(TestConsole.Output).Contains("1 steps disabled");
    }

    [Test]
    public async Task Execute_DryRun_DoesNotWrite()
    {
        var context = GetBuilder().WithData(TwoSteps(firstEnabled: true, secondEnabled: true)).Build();
        var settings = ConfigFile(new PluginStepStateConfig { StepId = _firstId, Enabled = false, Reason = "why" });

        await context.Execute(new PluginStepStateSetSettings { Config = settings.Config, DryRun = true }).Succeed();

        var first = context.GetById<SdkMessageProcessingStep>(_firstId);
        await Assert.That(PluginStepStateTestData.IsEnabled(first)).IsTrue();
        await Assert.That(first.Description).IsNull();
        await Assert.That(TestConsole.Output).Contains("Would disable First");
        await Assert.That(TestConsole.Output).Contains("1 steps would be disabled");
    }

    [Test]
    public async Task Execute_NoEntries_Succeeds()
    {
        var context = GetBuilder().Build();

        await context.Execute(ConfigFile()).Succeed();

        await Assert.That(TestConsole.Output).Contains("No steps defined in config file.");
    }

    [Test]
    public async Task Execute_UnreadableConfig_Fails()
    {
        var context = GetBuilder().Build();

        await context.Execute(new PluginStepStateSetSettings { Config = "does-not-exist.json" }).Fail();

        await Assert.That(TestConsole.Output).Contains("Failed to load or parse config file.");
    }

    [Test]
    [Arguments(null, "'enabled' is required.")]
    [Arguments(0, "Invalid stage value: 0.")]
    [Arguments(25, "Invalid stage value: 25.")]
    public async Task Validate_InvalidEntries_ReturnError(int? stage, string expected)
    {
        var entry = new PluginStepStateConfig
        {
            PluginType = "Type", Message = "Create", Stage = stage ?? 20, Entity = "account", Enabled = stage is null ? null : true
        };

        await Assert.That(entry.Validate()).IsEqualTo(expected);
    }

    [Test]
    public async Task Validate_EmptyStepId_ReturnsError()
    {
        await Assert.That(new PluginStepStateConfig { StepId = Guid.Empty, Enabled = true }.Validate()).Contains("empty GUID");
    }

    [Test]
    public async Task Validate_StepIdOrFullCompositeKey_IsValid()
    {
        await Assert.That(new PluginStepStateConfig { StepId = Guid.NewGuid(), Enabled = false }.Validate()).IsNull();
        await Assert.That(new PluginStepStateConfig { PluginType = "T", Message = "Create", Stage = 40, Entity = "account", Enabled = true }.Validate()).IsNull();
    }
}
