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
    private readonly record struct ReconciliationCounts(int Created, int Updated, int Unchanged, int Deleted)
    {
        public bool IsEmpty => Created == 0 && Updated == 0 && Unchanged == 0 && Deleted == 0;

        public static ReconciliationCounts operator +(ReconciliationCounts left, ReconciliationCounts right) =>
            new(
                left.Created + right.Created,
                left.Updated + right.Updated,
                left.Unchanged + right.Unchanged,
                left.Deleted + right.Deleted);
    }

    public Task ReconcileAsync(
        Guid assemblyId, IReadOnlyList<LocalPluginType> localTypes, PluginPushOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(localTypes);
        ArgumentNullException.ThrowIfNull(options);

        return ReconcileCoreAsync(assemblyId, localTypes, options, cancellationToken);
    }

    private async Task ReconcileCoreAsync(
        Guid assemblyId, IReadOnlyList<LocalPluginType> localTypes, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var remoteTypes = await typeRepository.ListByAssemblyAsync(assemblyId, cancellationToken);
        var ignoredTypeNames = localTypes
            .Where(type => !type.HasRegistrationAttribute)
            .Select(type => type.TypeName)
            .ToHashSet(StringComparer.Ordinal);
        var registeredTypes = localTypes.Where(type => type.HasRegistrationAttribute).ToList();
        var managedRemoteTypes = remoteTypes.Where(type => !ignoredTypeNames.Contains(type.TypeName)).ToList();
        var (typePlans, orphanedTypes) = PluginPushPlanner.PlanPluginTypes(registeredTypes, managedRemoteTypes);

        var steps = default(ReconciliationCounts);
        var images = default(ReconciliationCounts);

        foreach (var typePlan in typePlans)
        {
            var typeId = await ApplyTypePlanAsync(typePlan, assemblyId, options, cancellationToken);

            if (typePlan.Local.HasRegistrationAttribute)
            {
                var stepSummary = await ReconcileStepsAsync(typeId, typePlan.Local.Steps, options, cancellationToken);
                steps += stepSummary.Steps;
                images += stepSummary.Images;
            }

            await ReconcileCustomApiLinkAsync(typeId, typePlan.Local.CustomApi, options, cancellationToken);
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

        var createdTypes = typePlans.Count(p => p.Action == PluginTypeAction.Create);
        var existingTypes = typePlans.Count - createdTypes;
        console.MarkupLine(CultureInfo.InvariantCulture,
            "  [grey]Checked {0} plugin type(s): {1} new, {2} existing, {3} removed[/]",
            typePlans.Count, createdTypes, existingTypes, orphanedTypes.Count);

        if (!steps.IsEmpty)
        {
            console.MarkupLine(CultureInfo.InvariantCulture,
                "    [grey]Steps: {0} created, {1} updated, {2} unchanged, {3} deleted[/]",
                steps.Created, steps.Updated, steps.Unchanged, steps.Deleted);
        }

        if (!images.IsEmpty)
        {
            console.MarkupLine(CultureInfo.InvariantCulture,
                "    [grey]Images: {0} created, {1} updated, {2} unchanged, {3} deleted[/]",
                images.Created, images.Updated, images.Unchanged, images.Deleted);
        }
    }

    /// <summary>
    /// Applies the plan for a single plugin type and returns its (real or, in dry-run for a not-yet-created
    /// type, <see cref="Guid.Empty"/> placeholder) id, so steps/images/Custom API links declared on it can
    /// still be previewed - every downstream lookup keyed by this id safely returns "nothing exists yet" for
    /// <see cref="Guid.Empty"/>, which is exactly what a not-yet-created type's steps/images actually are.
    /// </summary>
    private async Task<Guid> ApplyTypePlanAsync(PluginTypePlan plan, Guid assemblyId, PluginPushOptions options, CancellationToken cancellationToken)
    {
        if (plan.Action == PluginTypeAction.Create)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "  Create PluginType [bold green]{0}[/]", plan.Local.TypeName);
            return options.DryRun
                ? Guid.Empty
                : await typeRepository.CreateAsync(assemblyId, plan.Local.TypeName, plan.Local.Name, cancellationToken);
        }

        return plan.Existing!.Id;
    }

    private async Task<(ReconciliationCounts Steps, ReconciliationCounts Images)> ReconcileStepsAsync(
        Guid pluginTypeId, IReadOnlyList<LocalPluginStep> localSteps, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var remoteSteps = await stepRepository.ListByPluginTypeAsync(pluginTypeId, cancellationToken);
        var (stepPlans, orphanedSteps) = PluginPushPlanner.PlanPluginSteps(localSteps, remoteSteps);

        var images = default(ReconciliationCounts);
        foreach (var stepPlan in stepPlans)
        {
            var stepId = await ApplyStepPlanAsync(stepPlan, pluginTypeId, options, cancellationToken);
            images += await ReconcileImagesAsync(stepId, stepPlan.Local.Images, options, cancellationToken);
        }

        foreach (var orphanedStep in orphanedSteps)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "    Delete Step [bold red]{0}[/]", orphanedStep.Name);
            if (!options.DryRun)
            {
                await stepRepository.DeleteAsync(orphanedStep.Id, cancellationToken);
            }
        }

        var steps = new ReconciliationCounts(
            stepPlans.Count(p => p.Action == PluginStepAction.Create),
            stepPlans.Count(p => p.Action == PluginStepAction.Update),
            stepPlans.Count(p => p.Action == PluginStepAction.Keep),
            orphanedSteps.Count);
        return (steps, images);
    }

    private async Task<Guid> ApplyStepPlanAsync(PluginStepPlan plan, Guid pluginTypeId, PluginPushOptions options, CancellationToken cancellationToken)
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
            return options.DryRun ? Guid.Empty : await stepRepository.CreateAsync(data, cancellationToken);
        }

        console.MarkupLine(CultureInfo.InvariantCulture, "    Update Step [bold green]{0}[/]", plan.Local.Name);
        if (!options.DryRun)
        {
            await stepRepository.UpdateAsync(plan.Existing!.Id, data, cancellationToken);
        }

        return plan.Existing!.Id;
    }

    private async Task<ReconciliationCounts> ReconcileImagesAsync(
        Guid stepId, IReadOnlyList<LocalPluginStepImage> localImages, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var remoteImages = await imageRepository.ListByStepAsync(stepId, cancellationToken);
        var (imagePlans, orphanedImages) = PluginPushPlanner.PlanPluginStepImages(localImages, remoteImages);

        var created = 0;
        var updated = 0;
        foreach (var imagePlan in imagePlans)
        {
            if (imagePlan.Action == PluginStepImageAction.Create)
            {
                created++;
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
                updated++;
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

        var unchanged = localImages.Count - imagePlans.Count;
        return new ReconciliationCounts(created, updated, unchanged, orphanedImages.Count);
    }

    private async Task ReconcileCustomApiLinkAsync(Guid pluginTypeId, string customApi, PluginPushOptions options, CancellationToken cancellationToken)
    {
        Guid? desiredCustomApiId = string.IsNullOrEmpty(customApi)
            ? null
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

        if (desiredCustomApiId is { } customApiId && !currentlyLinked.Contains(customApiId))
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "    Link Custom API [bold green]{0}[/]", customApi);
            if (!options.DryRun)
            {
                await customApiRepository.LinkPluginTypeAsync(customApiId, pluginTypeId, cancellationToken);
            }
        }
    }
}
