// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning.Changes;
using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Execution;

public sealed class PluginTypeDeploymentExecutor(
    IPluginTypeRepository typeRepository,
    ISdkMessageProcessingStepRepository stepRepository,
    ISdkMessageProcessingStepImageRepository imageRepository,
    ICustomApiRepository customApiRepository)
{
    public Task<IReadOnlyDictionary<string, Guid>> ApplyAsync(
        PluginTypeDeployment plan,
        Guid assemblyId,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(plan);

        return ApplyCoreAsync(plan, assemblyId, cancellationToken);
    }

    private async Task<IReadOnlyDictionary<string, Guid>> ApplyCoreAsync(
        PluginTypeDeployment plan,
        Guid assemblyId,
        CancellationToken cancellationToken)
    {
        var typeIds = new Dictionary<string, Guid>(StringComparer.Ordinal);
        foreach (var item in plan.Types)
        {
            var typeId = item.Change.Action == PluginTypeAction.Create
                ? await typeRepository.CreateAsync(
                    assemblyId,
                    item.Change.Local.TypeName,
                    item.Change.Local.Name,
                    cancellationToken)
                : item.Change.Existing!.Id;
            typeIds.Add(item.Change.Local.TypeName, typeId);

            foreach (var step in item.Steps)
            {
                var stepId = await ApplyStepAsync(step, typeId, cancellationToken);
                await ApplyImagesAsync(step, stepId, cancellationToken);
            }

            foreach (var step in item.StepDeletions)
            {
                await stepRepository.DeleteAsync(step.Step.Id, cancellationToken);
            }

            foreach (var customApiId in item.CustomApi.UnlinkIds)
            {
                await customApiRepository.UnlinkPluginTypeAsync(customApiId, cancellationToken);
            }

            if (item.CustomApi.Link)
            {
                await customApiRepository.LinkPluginTypeAsync(
                    item.CustomApi.DesiredId!.Value,
                    typeId,
                    cancellationToken);
            }
        }

        foreach (var deletion in plan.Deletions)
        {
            foreach (var stepId in deletion.DependentStepIds)
            {
                await stepRepository.DeleteAsync(stepId, cancellationToken);
            }

            await typeRepository.DeleteAsync(deletion.Type.Id, cancellationToken);
        }

        return typeIds;
    }

    private async Task<Guid> ApplyStepAsync(
        PluginStepDeployment deployment,
        Guid pluginTypeId,
        CancellationToken cancellationToken)
    {
        if (deployment.Change.Action == PluginStepAction.Unchanged)
        {
            return deployment.Change.Existing!.Id;
        }

        var message = deployment.Message
            ?? throw new InvalidOperationException("A create or update step operation requires a resolved SDK message.");
        var local = deployment.Change.Local;
        var data = new PluginStepData(
            local.Name,
            pluginTypeId,
            message.MessageId,
            message.MessageFilterId,
            local.Stage,
            local.Mode,
            local.ExecutionOrder,
            local.FilterAttributes,
            local.Configuration);

        if (deployment.Change.Action == PluginStepAction.Create)
        {
            return await stepRepository.CreateAsync(data, cancellationToken);
        }

        await stepRepository.UpdateAsync(deployment.Change.Existing!.Id, data, cancellationToken);
        return deployment.Change.Existing.Id;
    }

    private async Task ApplyImagesAsync(
        PluginStepDeployment deployment,
        Guid stepId,
        CancellationToken cancellationToken)
    {
        foreach (var image in deployment.Images.Changes)
        {
            if (image.Action == PluginStepImageAction.Create)
            {
                var local = image.Local;
                await imageRepository.CreateAsync(
                    new PluginStepImageData(
                        stepId,
                        local.Name,
                        local.EntityAlias,
                        local.ImageType,
                        local.MessagePropertyName,
                        local.Attributes),
                    cancellationToken);
            }
            else
            {
                await imageRepository.UpdateAsync(
                    image.Existing!.Id,
                    image.Local.Attributes,
                    cancellationToken);
            }
        }

        foreach (var image in deployment.Images.Deletions)
        {
            await imageRepository.DeleteAsync(image.Id, cancellationToken);
        }
    }
}
