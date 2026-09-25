// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Execution;

public sealed class PluginTypeDeploymentExecutor(
    IPluginTypeRepository typeRepository,
    ISdkMessageProcessingStepRepository stepRepository,
    ISdkMessageProcessingStepImageRepository imageRepository,
    ICustomApiRepository customApiRepository,
    ISolutionComponentRepository solutionRepository)
{
    public Task<IReadOnlyDictionary<string, Guid>> ApplyAsync(
        PluginTypeDeployment plan,
        Guid assemblyId,
        Action<PluginDeploymentProgress>? reportProgress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(plan);

        return ApplyCoreAsync(plan, assemblyId, reportProgress, cancellationToken);
    }

    private async Task<IReadOnlyDictionary<string, Guid>> ApplyCoreAsync(
        PluginTypeDeployment plan,
        Guid assemblyId,
        Action<PluginDeploymentProgress>? reportProgress,
        CancellationToken cancellationToken)
    {
        var typeIds = new Dictionary<string, Guid>(StringComparer.Ordinal);
        foreach (var item in plan.Types)
        {
            var typeId = item.Comparison.RequiresCreate
                ? await typeRepository.CreateAsync(
                    assemblyId,
                    item.Comparison.Local.TypeName,
                    item.Comparison.Local.Name,
                    cancellationToken)
                : item.Comparison.Remote!.Id;
            if (item.Comparison.RequiresCreate)
            {
                Report(reportProgress, PluginDeploymentOperation.Created, "plugin type", item.Comparison.Local.TypeName);
            }

            typeIds.Add(item.Comparison.Local.TypeName, typeId);

            foreach (var step in item.Steps)
            {
                var stepId = await ApplyStepAsync(step, typeId, reportProgress, cancellationToken);
                await ApplyImagesAsync(step, stepId, reportProgress, cancellationToken);
                if (step.Solution is { } solution)
                {
                    await solutionRepository.AddToSolutionAsync(
                        solution.ComponentType,
                        stepId,
                        solution.SolutionUniqueName,
                        cancellationToken);
                    Report(
                        reportProgress,
                        PluginDeploymentOperation.Linked,
                        "step",
                        $"{solution.ComponentName} to solution {solution.SolutionUniqueName}");
                }
            }

            foreach (var step in item.StepDeletions)
            {
                await stepRepository.DeleteAsync(step.Step.Id, cancellationToken);
                Report(reportProgress, PluginDeploymentOperation.Deleted, "step", step.Step.Name);
            }

            foreach (var customApiId in item.CustomApi.UnlinkIds)
            {
                await customApiRepository.UnlinkPluginTypeAsync(customApiId, cancellationToken);
                Report(reportProgress, PluginDeploymentOperation.Unlinked, "Custom API", customApiId.ToString());
            }

            if (item.CustomApi.Link)
            {
                await customApiRepository.LinkPluginTypeAsync(
                    item.CustomApi.DesiredId!.Value,
                    typeId,
                    cancellationToken);
                Report(reportProgress, PluginDeploymentOperation.Linked, "Custom API", item.CustomApi.UniqueName);
            }
        }

        foreach (var deletion in plan.Deletions)
        {
            foreach (var stepId in deletion.DependentStepIds)
            {
                await stepRepository.DeleteAsync(stepId, cancellationToken);
                Report(reportProgress, PluginDeploymentOperation.Deleted, "step", stepId.ToString());
            }

            await typeRepository.DeleteAsync(deletion.Type.Id, cancellationToken);
            Report(reportProgress, PluginDeploymentOperation.Deleted, "plugin type", deletion.Type.TypeName);
        }

        return typeIds;
    }

    private async Task<Guid> ApplyStepAsync(
        PluginStepDeployment deployment,
        Guid pluginTypeId,
        Action<PluginDeploymentProgress>? reportProgress,
        CancellationToken cancellationToken)
    {
        if (deployment.MigrationSource is { } migration)
        {
            var migratedLocal = deployment.Comparison.Local;
            if (deployment.Comparison.RequiresUpdate)
            {
                var migratedMessage = deployment.Message
                    ?? throw new InvalidOperationException("An updated step requires a resolved SDK message.");
                await stepRepository.UpdateAsync(
                    migration.Step.Id,
                    new PluginStepData(
                        migratedLocal.Name,
                        pluginTypeId,
                        migratedMessage.MessageId,
                        migratedMessage.MessageFilterId,
                        migratedLocal.Stage,
                        migratedLocal.Mode,
                        migratedLocal.ExecutionOrder,
                        migratedLocal.FilterAttributes),
                    cancellationToken);
            }
            else
            {
                await stepRepository.ReassignPluginTypeAsync(
                    migration.Step.Id,
                    pluginTypeId,
                    cancellationToken);
            }

            Report(reportProgress, PluginDeploymentOperation.Migrated, "step", migratedLocal.Name);
            return migration.Step.Id;
        }

        if (deployment.Comparison is { RequiresCreate: false, RequiresUpdate: false })
        {
            return deployment.Comparison.Remote!.Id;
        }

        var message = deployment.Message
            ?? throw new InvalidOperationException("A create or update step operation requires a resolved SDK message.");
        var local = deployment.Comparison.Local;
        var data = new PluginStepData(
            local.Name,
            pluginTypeId,
            message.MessageId,
            message.MessageFilterId,
            local.Stage,
            local.Mode,
            local.ExecutionOrder,
            local.FilterAttributes);

        if (deployment.Comparison.RequiresCreate)
        {
            var stepId = await stepRepository.CreateAsync(data, cancellationToken);
            Report(reportProgress, PluginDeploymentOperation.Created, "step", local.Name);
            return stepId;
        }

        await stepRepository.UpdateAsync(deployment.Comparison.Remote!.Id, data, cancellationToken);
        Report(reportProgress, PluginDeploymentOperation.Updated, "step", local.Name);
        return deployment.Comparison.Remote.Id;
    }

    private async Task ApplyImagesAsync(
        PluginStepDeployment deployment,
        Guid stepId,
        Action<PluginDeploymentProgress>? reportProgress,
        CancellationToken cancellationToken)
    {
        foreach (var image in deployment.Images.Comparisons)
        {
            if (image.RequiresCreate)
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
                Report(reportProgress, PluginDeploymentOperation.Created, "step image", local.Name);
            }
            else if (image.RequiresUpdate)
            {
                await imageRepository.UpdateAsync(
                    image.Remote!.Id,
                    image.Local.Attributes,
                    cancellationToken);
                Report(reportProgress, PluginDeploymentOperation.Updated, "step image", image.Local.Name);
            }
        }

        foreach (var image in deployment.Images.Deletions)
        {
            await imageRepository.DeleteAsync(image.Id, cancellationToken);
            Report(reportProgress, PluginDeploymentOperation.Deleted, "step image", image.Name);
        }
    }

    private static void Report(
        Action<PluginDeploymentProgress>? reportProgress,
        PluginDeploymentOperation operation,
        string resource,
        string name) =>
        reportProgress?.Invoke(new PluginDeploymentProgress(operation, resource, name));
}
