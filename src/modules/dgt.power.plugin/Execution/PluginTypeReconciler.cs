// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Repositories;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using System.Globalization;
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
    public async Task<IReadOnlyList<PluginPlanNode>> BuildPlanAsync(
        Guid assemblyId,
        IReadOnlyList<LocalPluginType> localTypes,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(localTypes);

        var nodes = new List<PluginPlanNode>();
        var remoteTypes = await typeRepository.ListByAssemblyAsync(assemblyId, cancellationToken);
        var ignoredTypeNames = localTypes
            .Where(type => !type.HasRegistrationAttribute)
            .Select(type => type.TypeName)
            .ToHashSet(StringComparer.Ordinal);
        var registeredTypes = localTypes.Where(type => type.HasRegistrationAttribute).ToList();
        var managedRemoteTypes = remoteTypes.Where(type => !ignoredTypeNames.Contains(type.TypeName)).ToList();
        var (typePlans, orphanedTypes) = PluginPushPlanner.PlanPluginTypes(registeredTypes, managedRemoteTypes);

        foreach (var typePlan in typePlans)
        {
            var typeNode = new PluginPlanNode(typePlan.Local.TypeName, typePlan.Action == PluginTypeAction.Create ? "Create" : "Reconcile");
            nodes.Add(typeNode);
            var typeId = typePlan.Existing?.Id ?? Guid.Empty;

            if (typePlan.Local.HasRegistrationAttribute)
            {
                var remoteSteps = await stepRepository.ListByPluginTypeAsync(typeId, cancellationToken);
                var (stepPlans, orphanedSteps) = PluginPushPlanner.PlanPluginSteps(typePlan.Local.Steps, remoteSteps);
                foreach (var stepPlan in stepPlans)
                {
                    if (stepPlan.Action is PluginStepAction.Create or PluginStepAction.Update)
                    {
                        var resolved = await messageRepository.ResolveAsync(
                            stepPlan.Local.MessageName,
                            stepPlan.Local.PrimaryEntityName,
                            stepPlan.Local.SecondaryEntityName,
                            cancellationToken);
                        if (resolved is null)
                        {
                            throw new UnresolvedPluginStepMessageException(
                                stepPlan.Local.Name,
                                stepPlan.Local.MessageName,
                                stepPlan.Local.PrimaryEntityName);
                        }
                    }

                    var stepNode = new PluginPlanNode(stepPlan.Local.Name, stepPlan.Action.ToString());
                    typeNode.AddChild(stepNode);
                    var stepId = stepPlan.Existing?.Id ?? Guid.Empty;
                    var remoteImages = await imageRepository.ListByStepAsync(stepId, cancellationToken);
                    var imagePlans = PluginPushPlanner.PlanPluginStepImages(stepPlan.Local.Images, remoteImages);
                    foreach (var imagePlan in imagePlans.Plans)
                    {
                        stepNode.AddChild(new PluginPlanNode(imagePlan.Local.Name, imagePlan.Action.ToString()));
                    }

                    foreach (var image in imagePlans.Purge)
                    {
                        stepNode.AddChild(new PluginPlanNode(image.Name, "Delete"));
                    }

                    foreach (var image in stepPlan.Local.Images.Where(image =>
                                 imagePlans.Plans.All(plan => plan.Local.Name != image.Name) &&
                                 imagePlans.Purge.All(purge => purge.Name != image.Name)))
                    {
                        stepNode.AddChild(new PluginPlanNode(image.Name, "Keep"));
                    }
                }

                foreach (var step in orphanedSteps)
                {
                    typeNode.AddChild(new PluginPlanNode(step.Name, "Delete"));
                }
            }

            await AddCustomApiPlanAsync(typeNode, typeId, typePlan.Local.CustomApi, cancellationToken);
        }

        foreach (var type in orphanedTypes)
        {
            nodes.Add(new PluginPlanNode(type.TypeName, "Delete"));
        }

        return nodes;
    }

    private async Task AddCustomApiPlanAsync(
        PluginPlanNode typeNode,
        Guid typeId,
        string customApi,
        CancellationToken cancellationToken)
    {
        Guid? desiredId = null;
        if (!string.IsNullOrEmpty(customApi))
        {
            desiredId = await customApiRepository.FindIdByUniqueNameAsync(customApi, cancellationToken);
            if (desiredId is null)
            {
                throw new MissingCustomApiException(customApi);
            }
        }

        var linked = await customApiRepository.ListLinkedToPluginTypeAsync(typeId, cancellationToken);
        foreach (var linkedId in linked.Where(id => id != desiredId))
        {
            typeNode.AddChild(new PluginPlanNode(linkedId.ToString(), "Unlink"));
        }

        if (desiredId is { } customApiId && !linked.Contains(customApiId))
        {
            typeNode.AddChild(new PluginPlanNode(customApi, "Link"));
        }
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

        foreach (var typePlan in typePlans)
        {
            var typeId = await ApplyTypePlanAsync(typePlan, assemblyId, options, cancellationToken);

            if (typePlan.Local.HasRegistrationAttribute)
            {
                await ReconcileStepsAsync(typeId, typePlan.Local.Steps, options, cancellationToken);
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

    private async Task ReconcileStepsAsync(
        Guid pluginTypeId, IReadOnlyList<LocalPluginStep> localSteps, PluginPushOptions options, CancellationToken cancellationToken)
    {
        var remoteSteps = await stepRepository.ListByPluginTypeAsync(pluginTypeId, cancellationToken);
        var (stepPlans, orphanedSteps) = PluginPushPlanner.PlanPluginSteps(localSteps, remoteSteps);

        foreach (var stepPlan in stepPlans)
        {
            var stepId = await ApplyStepPlanAsync(stepPlan, pluginTypeId, options, cancellationToken);
            await ReconcileImagesAsync(stepId, stepPlan.Local.Images, options, cancellationToken);
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
        Guid? desiredCustomApiId = null;
        if (!string.IsNullOrEmpty(customApi))
        {
            desiredCustomApiId = await customApiRepository.FindIdByUniqueNameAsync(customApi, cancellationToken);
            if (desiredCustomApiId is null)
            {
                throw new MissingCustomApiException(customApi);
            }
        }

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
