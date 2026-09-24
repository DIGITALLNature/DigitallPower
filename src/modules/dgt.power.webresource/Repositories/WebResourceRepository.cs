// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.webresource.Local;
using dgt.power.webresource.Remote;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.webresource.Repositories;

public sealed class WebResourceRepository(IOrganizationServiceAsync2 service) : IWebResourceRepository
{
    public Task<IReadOnlyList<RemoteWebResource>> FindByNamesAsync(
        IReadOnlyCollection<string> names,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(names);
        if (names.Count == 0)
        {
            return Task.FromResult<IReadOnlyList<RemoteWebResource>>([]);
        }

        return FindByNamesCoreAsync(names, cancellationToken);
    }

    private async Task<IReadOnlyList<RemoteWebResource>> FindByNamesCoreAsync(
        IReadOnlyCollection<string> names,
        CancellationToken cancellationToken)
    {
        var query = new QueryExpression(WebResource.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                WebResource.LogicalNames.WebResourceId,
                WebResource.LogicalNames.Name,
                WebResource.LogicalNames.WebResourceType,
                WebResource.LogicalNames.Content,
                WebResource.LogicalNames.IsManaged),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(WebResource.LogicalNames.Name, ConditionOperator.In, names.ToArray())
                }
            }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        return result.Entities
            .Select(entity => ToRemote(entity.ToEntity<WebResource>()))
            .ToList();
    }

    public Task<Guid> CreateAsync(LocalWebResource resource, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(resource);
        return CreateCoreAsync(resource, cancellationToken);
    }

    private async Task<Guid> CreateCoreAsync(
        LocalWebResource resource,
        CancellationToken cancellationToken)
    {
        return await service.CreateAsync(new WebResource
        {
            Name = resource.Name,
            DisplayName = resource.DisplayName,
            Content = resource.Content,
            WebResourceType = new OptionSetValue(resource.Type),
            Description = $"Upserted with DGTP: {resource.Hash}"
        }, cancellationToken);
    }

    public Task UpdateAsync(LocalWebResource resource, Guid id, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(resource);
        return UpdateCoreAsync(resource, id, cancellationToken);
    }

    private async Task UpdateCoreAsync(
        LocalWebResource resource,
        Guid id,
        CancellationToken cancellationToken)
    {
        await service.UpdateAsync(new WebResource(id)
        {
            Content = resource.Content,
            Description = $"Upserted with DGTP: {resource.Hash}"
        }, cancellationToken);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        service.DeleteAsync(WebResource.EntityLogicalName, id, cancellationToken);

    public Task PublishAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ids);
        return ids.Count == 0
            ? Task.CompletedTask
            : PublishCoreAsync(ids, cancellationToken);
    }

    private async Task PublishCoreAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        var webResources = string.Concat(ids.Select(id => $"<webresource>{id}</webresource>"));
        await service.ExecuteAsync(
            new Microsoft.Crm.Sdk.Messages.PublishXmlRequest
            {
                ParameterXml =
                    $"<importexportxml><webresources>{webResources}</webresources></importexportxml>"
            },
            cancellationToken);
    }

    private static RemoteWebResource ToRemote(WebResource entity)
    {
        var name = entity.Name;
        var type = entity.WebResourceType?.Value;
        if (string.IsNullOrWhiteSpace(name) || type is null)
        {
            throw new InvalidOperationException("Dataverse returned a webresource without name or type.");
        }

        return new RemoteWebResource(
            entity.Id,
            type.Value,
            name,
            entity.Content,
            entity.IsManaged ?? false);
    }
}
