// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using dgt.power.plugin.Remote;
using System.Security.Cryptography;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="IPluginAssemblyRepository" />
public sealed class PluginAssemblyRepository(IOrganizationServiceAsync2 service) : IPluginAssemblyRepository
{
    public async Task<RemoteAssembly?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(PluginAssembly.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                PluginAssembly.LogicalNames.PluginAssemblyId,
                PluginAssembly.LogicalNames.Version,
                PluginAssembly.LogicalNames.PackageId,
                PluginAssembly.LogicalNames.Content),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(PluginAssembly.LogicalNames.Name, ConditionOperator.Equal, name),
                    new ConditionExpression(PluginAssembly.LogicalNames.SourceType, ConditionOperator.In,
                        PluginAssembly.Options.SourceType.Database, PluginAssembly.Options.SourceType.FileStore),
                    new ConditionExpression(PluginAssembly.LogicalNames.IsolationMode, ConditionOperator.Equal,
                        PluginAssembly.Options.IsolationMode.Sandbox)
                }
            },
            Orders = { new OrderExpression(PluginAssembly.LogicalNames.Version, OrderType.Descending) }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        var entity = result.Entities.FirstOrDefault()?.ToEntity<PluginAssembly>();
        if (entity is null)
        {
            return null;
        }

        var contentHash = entity.PackageId is not null || entity.Content is null
            ? null
            : Convert.ToHexString(SHA256.HashData(Convert.FromBase64String(entity.Content)));
        return new RemoteAssembly(entity.Id, Version.Parse(entity.Version!), entity.PackageId?.Id, contentHash);
    }

    public async Task<IReadOnlyList<RemoteAssembly>> ListOutdatedAsync(string name, Guid excludeId, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(PluginAssembly.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                PluginAssembly.LogicalNames.PluginAssemblyId,
                PluginAssembly.LogicalNames.Version,
                PluginAssembly.LogicalNames.PackageId),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(PluginAssembly.LogicalNames.Name, ConditionOperator.Equal, name),
                    new ConditionExpression(PluginAssembly.LogicalNames.PluginAssemblyId, ConditionOperator.NotEqual, excludeId),
                    new ConditionExpression(PluginAssembly.LogicalNames.SourceType, ConditionOperator.In,
                        PluginAssembly.Options.SourceType.Database, PluginAssembly.Options.SourceType.FileStore),
                    new ConditionExpression(PluginAssembly.LogicalNames.IsolationMode, ConditionOperator.Equal,
                        PluginAssembly.Options.IsolationMode.Sandbox),
                    new ConditionExpression(PluginAssembly.LogicalNames.PackageId, ConditionOperator.Null)
                }
            },
            Orders = { new OrderExpression(PluginAssembly.LogicalNames.Version, OrderType.Descending) }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        return result.Entities
            .Select(e => e.ToEntity<PluginAssembly>())
            .Select(entity => new RemoteAssembly(entity.Id, Version.Parse(entity.Version!), entity.PackageId?.Id))
            .ToList();
    }

    public async Task<Guid> CreateAsync(string name, string content, CancellationToken cancellationToken = default)
    {
        var assembly = new PluginAssembly
        {
            Name = name,
            Content = content,
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        };

        return await service.CreateAsync(assembly, cancellationToken);
    }

    public async Task UpdateContentAsync(Guid id, string content, CancellationToken cancellationToken = default)
    {
        var assembly = new PluginAssembly(id) { Content = content };
        await service.UpdateAsync(assembly, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await service.DeleteAsync(PluginAssembly.EntityLogicalName, id, cancellationToken);
}
