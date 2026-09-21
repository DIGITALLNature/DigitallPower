// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using Spectre.Console;

namespace dgt.power.plugin.Execution;

/// <summary>
/// Reconciles the plugin types (and, for regular plugins, their steps/images) declared on an
/// assembly against a Dataverse environment. Custom API handler types (<see cref="LocalPluginType.CustomApi"/>
/// set) have no steps - they are linked to the matching <c>customapi</c> record instead.
/// </summary>
public sealed class PluginTypeReconciler(
    IPluginTypeRepository typeRepository,
    ISdkMessageProcessingStepRepository stepRepository,
    ISdkMessageProcessingStepImageRepository imageRepository,
    ISdkMessageRepository messageRepository,
    ICustomApiRepository customApiRepository,
    IAnsiConsole console)
{
    public async Task ReconcileAsync(
        Guid assemblyId, IReadOnlyList<LocalPluginType> localTypes, PluginPushOptions options, CancellationToken cancellationToken = default)
    {
        var remoteTypes = await typeRepository.ListByAssemblyAsync(assemblyId, cancellationToken);
        var (typePlans, orphanedTypes) = PluginPushPlanner.PlanPluginTypes(localTypes, remoteTypes);

        foreach (var typePlan in typePlans)
        {
            var typeId = await ApplyTypePlanAsync(typePlan, assemblyId, options, cancellationToken);
            if (typeId is null)
            {
                continue; // dry-run Create: nothing to reconcile further without a real id.
            }

            if (typePlan.Local.Steps.Count > 0)
            {
                await ReconcileStepsAsync(typeId.Value, typePlan.Local.Steps, options, cancellationToken);
            }

            await ReconcileCustomApiLinkAsync(typeId.Value, typePlan.Local.CustomApi, options, cancellationToken);
        }

        foreach (var orphanedType in orphanedTypes)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "  Delete PluginType [bold red]{0}[/]", orphanedType.TypeName);
            if (options.DryRun)
            {
                continue;
            }

            foreach (var dependentStepId in await typeRepository.GetDependentStepIdsAsync(orphanedType.Id, cancellationToken))
            {
                await stepRepository.DeleteAsync(dependentStepId, cancellationToken);
            }

            await typeRepository.DeleteAsync(orphanedType.Id, cancellationToken);
        }
    }

    private async Task<Guid?> ApplyTypePlanAsync(PluginTypePlan plan, Guid assemblyId, PluginPushOptions options, CancellationToken cancellationToken)
    {
        if (plan.Action == PluginTypeAction.Create)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "  Create PluginType [bold green]{0}[/]", plan.Local.TypeName);
            return options.DryRun
                ? null
                : await typeRepository.CreateAsync(assemblyId, plan.Local.TypeName, plan.Local.Name, cancellationToken);
        }

        return plan.Existing!.Id;
    }

    private async Task ReconcileStepsAsync(
        Guid pluginTypeId, IReadOnlyList<LocalPluginStep> localSteps, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var remoteSteps = await stepRepository.ListByPluginTypeAsync(pluginTypeId, cancellationToken);
        var (stepPlans, orphanedSteps) = PluginPushPlanner.PlanPluginSteps(localSteps, remoteSteps);

        foreach (var stepPlan in stepPlans)
        {
            var stepId = await ApplyStepPlanAsync(stepPlan, pluginTypeId, options, cancellationToken);
            if (stepId is null)
            {
                continue; // dry-run Create: nothing to reconcile further without a real id.
            }

            await ReconcileImagesAsync(stepId.Value, stepPlan.Local.Images, options, cancellationToken);
        }

        foreach (var orphanedStep in orphanedSteps)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "    Delete Step [bold red]{0}[/]", orphanedStep.Name);
            if (!options.DryRun)
            {
                await stepRepository.DeleteAsync(orphanedStep.Id, cancellationToken);
            }
        }
    }

    private async Task<Guid?> ApplyStepPlanAsync(PluginStepPlan plan, Guid pluginTypeId, PluginPushOptions options, CancellationToken cancellationToken)
    {
        if (plan.Action == PluginStepAction.Keep)
        {
            return plan.Existing!.Id;
        }

        var resolved = await messageRepository.ResolveAsync(
            plan.Local.MessageName, plan.Local.PrimaryEntityName, plan.Local.SecondaryEntityName, cancellationToken);
        if (resolved is null)
        {
            throw new UnresolvedPluginStepMessageException(plan.Local.Name, plan.Local.MessageName, plan.Local.PrimaryEntityName);
        }

        var data = new PluginStepData(
            plan.Local.Name, pluginTypeId, resolved.MessageId, resolved.MessageFilterId, plan.Local.Stage, plan.Local.Mode,
            plan.Local.ExecutionOrder, plan.Local.FilterAttributes, plan.Local.Configuration);

        if (plan.Action == PluginStepAction.Create)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "    Create Step [bold green]{0}[/]", plan.Local.Name);
            return options.DryRun ? null : await stepRepository.CreateAsync(data, cancellationToken);
        }

        console.MarkupLine(CultureInfo.InvariantCulture, "    Update Step [bold green]{0}[/]", plan.Local.Name);
        if (!options.DryRun)
        {
            await stepRepository.UpdateAsync(plan.Existing!.Id, data, cancellationToken);
        }

        return plan.Existing!.Id;
    }

    private async Task ReconcileImagesAsync(
        Guid stepId, IReadOnlyList<LocalPluginStepImage> localImages, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var remoteImages = await imageRepository.ListByStepAsync(stepId, cancellationToken);
        var (imagePlans, orphanedImages) = PluginPushPlanner.PlanPluginStepImages(localImages, remoteImages);

        foreach (var imagePlan in imagePlans)
        {
            if (imagePlan.Action == PluginStepImageAction.Create)
            {
                console.MarkupLine(CultureInfo.InvariantCulture, "      Create Image [bold green]{0}[/]", imagePlan.Local.Name);
                if (options.DryRun)
                {
                    continue;
                }

                var data = new PluginStepImageData(
                    stepId, imagePlan.Local.Name, imagePlan.Local.EntityAlias, imagePlan.Local.ImageType,
                    imagePlan.Local.MessagePropertyName, imagePlan.Local.Attributes);
                await imageRepository.CreateAsync(data, cancellationToken);
            }
            else
            {
                console.MarkupLine(CultureInfo.InvariantCulture, "      Update Image [bold green]{0}[/]", imagePlan.Local.Name);
                if (!options.DryRun)
                {
                    await imageRepository.UpdateAsync(imagePlan.Existing!.Id, imagePlan.Local.Attributes, cancellationToken);
                }
            }
        }

        foreach (var orphanedImage in orphanedImages)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "      Delete Image [bold red]{0}[/]", orphanedImage.Name);
            if (!options.DryRun)
            {
                await imageRepository.DeleteAsync(orphanedImage.Id, cancellationToken);
            }
        }
    }

    private async Task ReconcileCustomApiLinkAsync(Guid pluginTypeId, string customApi, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var desiredCustomApiId = string.IsNullOrEmpty(customApi)
            ? (Guid?)null
            : await customApiRepository.FindIdByUniqueNameAsync(customApi, cancellationToken);

        var currentlyLinked = await customApiRepository.ListLinkedToPluginTypeAsync(pluginTypeId, cancellationToken);

        foreach (var linkedId in currentlyLinked.Where(id => id != desiredCustomApiId))
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "    Unlink Custom API [bold red]{0}[/]", linkedId);
            if (!options.DryRun)
            {
                await customApiRepository.UnlinkPluginTypeAsync(linkedId, cancellationToken);
            }
        }

        if (desiredCustomApiId is { } id && !currentlyLinked.Contains(id))
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "    Link Custom API [bold green]{0}[/]", customApi);
            if (!options.DryRun)
            {
                await customApiRepository.LinkPluginTypeAsync(id, pluginTypeId, cancellationToken);
            }
        }
    }
}
