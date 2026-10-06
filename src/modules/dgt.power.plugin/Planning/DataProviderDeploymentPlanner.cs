// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning.Deployment;
using dgt.power.plugin.Remote;
using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Planning;

internal sealed class DataProviderDeploymentPlanner(IEntityDataProviderRepository repository, ISolutionComponentRepository solutions,
    Func<int, Guid?, string, string?, CancellationToken, Task<SolutionLink?>> planSolution)
{
    /// <param name="localTypes">All plugin types that will exist after the push.</param>
    /// <param name="remoteTypes">Remote types compared against <paramref name="localTypes"/>, including those scheduled for deletion.</param>
    /// <param name="replacedTypes">Types of outdated assemblies and deleted types whose handler references must be moved or released.</param>
    internal async Task<IReadOnlyList<DataProviderDeployment>> BuildAsync(IReadOnlyList<LocalPluginType> localTypes, IReadOnlyList<RemotePluginType> remoteTypes,
        IReadOnlyList<RemotePluginType> replacedTypes, string? solution, CancellationToken cancellationToken)
    {
        var localProviders = DataProviderRegistrationGrouper.Group(localTypes);
        if (localProviders.Count == 0 && replacedTypes.Count == 0 && remoteTypes.All(remote => localTypes.Any(local => local.TypeName == remote.TypeName)))
        {
            return [];
        }

        var remoteProviders = await repository.ListAsync(cancellationToken);
        var planned = await PlanDeclaredAsync(localProviders, localTypes, remoteTypes, remoteProviders, solution, cancellationToken);
        return AddMigrations(planned, replacedTypes, localTypes, remoteProviders);
    }

    private async Task<IReadOnlyList<DataProviderDeployment>> PlanDeclaredAsync(IReadOnlyList<LocalDataProvider> localProviders, IReadOnlyList<LocalPluginType> localTypes,
        IReadOnlyList<RemotePluginType> remoteTypes, IReadOnlyList<RemoteDataProvider> remoteProviders, string? solution, CancellationToken cancellationToken)
    {
        var deletedIds = remoteTypes.Where(remote => localTypes.All(local => local.TypeName != remote.TypeName)).Select(type => type.Id).ToHashSet();
        var blocked = remoteProviders.FirstOrDefault(provider => provider.Handlers.Any(handler => deletedIds.Contains(handler.Value) &&
            !localProviders.Any(local => string.Equals(local.DataSourceLogicalName, provider.DataSourceLogicalName, StringComparison.OrdinalIgnoreCase) && local.Handlers.ContainsKey(handler.Key))));
        if (blocked is not null)
        {
            throw new InvalidOperationException($"Cannot remove a plugin type still referenced by data provider '{blocked.Name}'. Reassign its provider handlers before removing the type.");
        }

        var result = new List<DataProviderDeployment>();
        foreach (var local in localProviders)
        {
            var matches = remoteProviders.Where(provider => string.Equals(provider.DataSourceLogicalName, local.DataSourceLogicalName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matches.Count > 1)
            {
                throw new InvalidOperationException($"Multiple data providers reference '{local.DataSourceLogicalName}'. Provider registration requires an unambiguous match.");
            }

            var remote = matches.SingleOrDefault();
            var dataSource = await repository.FindDataSourceAsync(local.DataSourceLogicalName, cancellationToken);
            await repository.ValidateDataSourceAsync(local.DataSourceLogicalName, dataSource, cancellationToken);

            var languageCode = dataSource is not null && (local.DataSourceDisplayName is not null || local.DataSourcePluralName is not null)
                ? await repository.GetBaseLanguageCodeAsync(cancellationToken)
                : 0;
            var singularChanged = local.DataSourceDisplayName is not null &&
                local.DataSourceDisplayName != dataSource?.DisplayName?.LocalizedLabels.FirstOrDefault(label => label.LanguageCode == languageCode)?.Label;
            var pluralChanged = local.DataSourcePluralName is not null &&
                local.DataSourcePluralName != dataSource?.DisplayCollectionName?.LocalizedLabels.FirstOrDefault(label => label.LanguageCode == languageCode)?.Label;
            var updateTable = dataSource is null || singularChanged || pluralChanged;
            var metadataChanged = remote?.Name != local.ProviderName ||
                local.Description is not null && (remote?.Description ?? string.Empty) != local.Description;
            var handlersChanged = remote is null || local.Handlers.Any(handler => !remote.Handlers.TryGetValue(handler.Key, out var id) ||
                remoteTypes.All(type => type.TypeName != handler.Value || type.Id != id));
            var updateProvider = metadataChanged || handlersChanged;
            SolutionLink? providerSolution = null;
            SolutionLink? tableSolution = null;
            if (!string.IsNullOrWhiteSpace(solution))
            {
                // Some environments define no solution component type for entitydataprovider; the provider is then not added to the solution.
                var componentType = await solutions.GetComponentTypeAsync(EntityDataProvider.EntityLogicalName, cancellationToken);
                if (componentType is not null)
                {
                    providerSolution = await planSolution(componentType.Value, remote?.Id, local.ProviderName, solution, cancellationToken);
                    if (providerSolution is not null)
                    {
                        providerSolution = providerSolution with { Resource = "Data provider" };
                    }
                }

                tableSolution = await planSolution(1, dataSource?.MetadataId, local.DataSourceLogicalName, solution, cancellationToken);
            }

            result.Add(new DataProviderDeployment(local, remote, dataSource, updateTable, updateProvider, local.Handlers, providerSolution, tableSolution));
        }

        return result;
    }

    private static IReadOnlyList<DataProviderDeployment> AddMigrations(IReadOnlyList<DataProviderDeployment> deployments, IReadOnlyList<RemotePluginType> oldTypes,
        IReadOnlyList<LocalPluginType> replacementTypes, IReadOnlyList<RemoteDataProvider> providers)
    {
        if (oldTypes.Count == 0)
        {
            return deployments;
        }

        var result = deployments.ToList();
        foreach (var provider in providers)
        {
            var handlers = new Dictionary<DataProviderOperation, string>();
            foreach (var (field, id) in provider.Handlers)
            {
                var oldType = oldTypes.FirstOrDefault(type => type.Id == id);
                if (oldType is null)
                {
                    continue;
                }

                if (replacementTypes.All(type => type.TypeName != oldType.TypeName))
                {
                    if (deployments.Any(deployment => deployment.Remote?.Id == provider.Id && deployment.Handlers.ContainsKey(field)))
                    {
                        continue;
                    }

                    throw new InvalidOperationException($"Cannot remove '{oldType.TypeName}': data provider '{provider.Name}' still references it.");
                }

                handlers[field] = oldType.TypeName;
            }

            if (handlers.Count == 0)
            {
                continue;
            }

            var existing = result.FindIndex(deployment => deployment.Remote?.Id == provider.Id);
            if (existing >= 0)
            {
                var deployment = result[existing];
                // Explicit new declarations take precedence; undeclared operations retain their handler across upgrades.
                foreach (var (field, typeName) in deployment.Handlers)
                {
                    handlers[field] = typeName;
                }

                result[existing] = deployment with { Handlers = handlers, UpdateProvider = true };
            }
            else
            {
                result.Add(DataProviderDeployment.Migration(provider, handlers));
            }
        }

        return result;
    }
}
