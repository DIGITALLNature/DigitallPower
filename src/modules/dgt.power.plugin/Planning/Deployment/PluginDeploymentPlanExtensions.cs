// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Planning.Comparison;

namespace dgt.power.plugin.Planning.Deployment;

public static class PluginDeploymentPlanExtensions
{
    public static bool HasChanges(this PluginDeploymentPlan plan)
    {
        ArgumentNullException.ThrowIfNull(plan);

        return plan switch
        {
            AssemblyDeploymentPlan assembly => AssemblyHasChanges(assembly),
            PackageDeploymentPlan package => PackageHasChanges(package),
            _ => throw new ArgumentOutOfRangeException(nameof(plan), plan.GetType(), "Unknown plugin deployment plan type.")
        };
    }

    private static bool PackageHasChanges(PackageDeploymentPlan plan)
    {
        if (plan.Comparison.RequiresCreate || plan.Comparison.RequiresUpdate)
        {
            return true;
        }

        return plan.Assemblies.Any(AssemblyHasChanges) ||
               plan.LinkManagedIdentity ||
               plan.Solution is not null;
    }

    private static bool AssemblyHasChanges(AssemblyDeploymentPlan plan)
    {
        if (plan.Comparison.RequiresCreate ||
            plan.Comparison.RequiresUpgrade ||
            plan.Comparison.RequiresUpdate)
        {
            return true;
        }

        if (plan.LinkManagedIdentity ||
            plan.Solution is not null ||
            plan.OutdatedAssemblies.Assemblies.Count > 0)
        {
            return true;
        }

        return plan.PluginTypes is { } pluginTypes && PluginTypesHaveChanges(pluginTypes);
    }

    private static bool PluginTypesHaveChanges(PluginTypeDeployment plan) =>
        plan.Deletions.Count > 0 ||
        plan.Types.Any(TypeHasChanges);

    private static bool TypeHasChanges(PluginTypeDeploymentItem plan)
    {
        if (plan.Comparison.RequiresCreate ||
            plan.StepDeletions.Count > 0 ||
            plan.CustomApi.UnlinkIds.Count > 0 ||
            plan.CustomApi.Link)
        {
            return true;
        }

        return plan.Steps.Any(StepHasChanges);
    }

    private static bool StepHasChanges(PluginStepDeployment plan)
    {
        if (plan.MigrationSource is not null ||
            plan.Comparison.RequiresCreate ||
            plan.Comparison.RequiresUpdate)
        {
            return true;
        }

        if (plan.Images.Deletions.Count > 0 || plan.Solution is not null)
        {
            return true;
        }

        return plan.Images.Comparisons.Any(ImageHasChanges);
    }

    private static bool ImageHasChanges(PluginStepImageComparison plan) =>
        plan.RequiresCreate || plan.RequiresUpdate;
}
