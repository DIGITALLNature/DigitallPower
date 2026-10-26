// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.plugin.tests.Commands;

internal static class PluginStepStateTestData
{
    private const int StepComponentType = 92;

    public static IEnumerable<EntityMetadata> Metadata() =>
    [
        new() { LogicalName = SdkMessage.EntityLogicalName },
        new() { LogicalName = SdkMessageProcessingStep.EntityLogicalName },
        new() { LogicalName = PluginType.EntityLogicalName },
        new() { LogicalName = SdkMessageFilter.EntityLogicalName },
        new() { LogicalName = Solution.EntityLogicalName },
        new() { LogicalName = SolutionComponent.EntityLogicalName },
        new() { LogicalName = SolutionComponentDefinition.EntityLogicalName }
    ];

    public static List<Entity> CreateStep(Guid stepId, string name, bool enabled, string? description = null, int rank = 1, string pluginTypeName = "TestPluginType")
    {
        var message = new SdkMessage(Guid.NewGuid()) { Name = "Create" };
        var pluginType = new PluginType(Guid.NewGuid()) { Name = "Plugin display name", TypeName = pluginTypeName };
        var filter = new SdkMessageFilter(Guid.NewGuid())
        {
            Attributes = { [SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode] = "account" }
        };
        var step = new SdkMessageProcessingStep(stepId)
        {
            Name = name,
            Description = description,
            EventHandler = pluginType.ToEntityReference(),
            SdkMessageId = message.ToEntityReference(),
            SdkMessageFilterId = filter.ToEntityReference(),
            Mode = new OptionSetValue(SdkMessageProcessingStep.Options.Mode.Synchronous),
            Stage = new OptionSetValue(SdkMessageProcessingStep.Options.Stage.PreOperation),
            StateCode = new OptionSetValue(enabled ? SdkMessageProcessingStep.Options.StateCode.Enabled : SdkMessageProcessingStep.Options.StateCode.Disabled),
            StatusCode = new OptionSetValue(enabled ? SdkMessageProcessingStep.Options.StatusCode.Enabled : SdkMessageProcessingStep.Options.StatusCode.Disabled),
            Rank = rank
        };

        return [message, pluginType, filter, step];
    }

    public static List<Entity> CreateSolution(string uniqueName, params Guid[] stepIds)
    {
        var solution = new Solution(Guid.NewGuid()) { UniqueName = uniqueName };
        var entities = new List<Entity>
        {
            solution,
            new SolutionComponentDefinition(Guid.NewGuid())
            {
                PrimaryEntityName = SdkMessageProcessingStep.EntityLogicalName, SolutionComponentType = StepComponentType
            }
        };
        entities.AddRange(stepIds.Select(stepId => new SolutionComponent(Guid.NewGuid())
        {
            ["objectid"] = stepId,
            ["componenttype"] = new OptionSetValue(StepComponentType),
            ["solutionid"] = solution.ToEntityReference()
        }));
        return entities;
    }

    public static bool IsEnabled(SdkMessageProcessingStep step) =>
        step.StateCode?.Value == SdkMessageProcessingStep.Options.StateCode.Enabled;
}
