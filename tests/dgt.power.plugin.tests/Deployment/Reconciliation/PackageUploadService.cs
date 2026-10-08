// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.tests.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

internal sealed class PackageUploadService : IOrganizationServiceAsync2
{
    private readonly FakeOrganizationServiceAsync service;
    private LocalPluginPackage target;
    private readonly bool materializeOnUpload;
    private readonly Dictionary<Guid, byte[]> _packageFiles = [];

    public int UploadCount { get; private set; }
    public bool FailUpload { get; set; }
    public bool FailProviderWrites { get; set; }

    public PackageUploadService(FakeOrganizationServiceAsync service, LocalPluginPackage target,
        bool materializeOnUpload = true, LocalPluginPackage? uploadedPackage = null)
    {
        this.service = service;
        this.target = uploadedPackage ?? target;
        this.materializeOnUpload = materializeOnUpload;
        service.AddRequests(new InitializePackageFileDownloadExecutor(_packageFiles));
        service.AddRequests(new DownloadPackageFileBlockExecutor(_packageFiles));
    }

    public byte[] PackageContent(Guid packageId) => _packageFiles[packageId];

    public void RetargetUpload(LocalPluginPackage package) => target = package;

    public Guid SeedPackage(string name, LocalPluginPackage package, byte[] bytes)
    {
        var fileId = Guid.NewGuid();
        var id = service.Create(new PluginPackage
        {
            Name = name,
            Version = package.Package.Version,
            Attributes = { [PluginPackage.LogicalNames.Package] = fileId }
        });
        _packageFiles.Add(id, bytes);
        return id;
    }

    public void MaterializeAssemblies(Guid packageId, LocalPluginPackage package) => Materialize(packageId, package);

    private void Materialize(Guid packageId, LocalPluginPackage package)
    {
        foreach (var assembly in package.Assemblies)
        {
            var existing = service.RetrieveMultiple(new QueryExpression(PluginAssembly.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(PluginAssembly.LogicalNames.PluginAssemblyId),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression(PluginAssembly.LogicalNames.PackageId, ConditionOperator.Equal, packageId),
                        new ConditionExpression(PluginAssembly.LogicalNames.Name, ConditionOperator.Equal, assembly.Name)
                    }
                }
            }).Entities.SingleOrDefault();
            var assemblyId = existing?.Id ?? service.Create(new PluginAssembly
            {
                Name = assembly.Name,
                Version = assembly.Version.ToString(),
                SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
                IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox),
                PackageId = new EntityReference(PluginPackage.EntityLogicalName, packageId)
            });

            var existingTypes = service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(PluginType.LogicalNames.TypeName),
                Criteria = new FilterExpression
                {
                    Conditions = { new ConditionExpression(PluginType.LogicalNames.PluginAssemblyId, ConditionOperator.Equal, assemblyId) }
                }
            }).Entities.ToDictionary(entity => entity.GetAttributeValue<string>(PluginType.LogicalNames.TypeName), entity => entity.Id, StringComparer.Ordinal);
            var declaredTypes = assembly.PluginTypes.Select(pluginType => pluginType.TypeName).ToHashSet(StringComparer.Ordinal);
            foreach (var (typeName, typeId) in existingTypes.Where(pair => !declaredTypes.Contains(pair.Key)))
            {
                var steps = service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName)
                {
                    Criteria = new FilterExpression
                    {
                        Conditions = { new ConditionExpression(SdkMessageProcessingStep.LogicalNames.EventHandler, ConditionOperator.Equal, typeId) }
                    }
                }).Entities;
                if (steps.Count > 0)
                {
                    throw new InvalidOperationException($"Unable to delete '{typeName}' plugintype due to {steps.Count} step(s) registered on it.");
                }

                service.Delete(PluginType.EntityLogicalName, typeId);
            }

            foreach (var pluginType in assembly.PluginTypes.Where(pluginType => !existingTypes.ContainsKey(pluginType.TypeName)))
            {
                service.Create(new PluginType
                {
                    PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId),
                    TypeName = pluginType.TypeName,
                    Name = pluginType.Name,
                    FriendlyName = pluginType.Name
                });
            }
        }
    }

    public Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken) =>
        service.AssociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);
    public Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
        service.AssociateAsync(entityName, entityId, relationship, relatedEntities);
    public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
        service.Associate(entityName, entityId, relationship, relatedEntities);
    public Guid Create(Entity entity) => service.Create(entity);
    public Task<Guid> CreateAsync(Entity entity, CancellationToken cancellationToken)
    {
        EnsureProviderWriteAllowed(entity);
        if (entity is not PluginPackage package)
        {
            return service.CreateAsync(entity, cancellationToken);
        }

        var fileId = Guid.NewGuid();
        var content = Convert.FromBase64String(package.Content!);
        package[PluginPackage.LogicalNames.Package] = fileId;
        return CreatePackageAsync(package, content, cancellationToken);
    }

    public Task<Guid> CreateAsync(Entity entity) => CreateAsync(entity, CancellationToken.None);
    private async Task<Guid> CreatePackageAsync(PluginPackage package, byte[] content, CancellationToken cancellationToken)
    {
        EnsureUploadAllowed();
        var id = await service.CreateAsync(package, cancellationToken);
        UploadCount++;
        _packageFiles.Add(id, content);
        if (materializeOnUpload)
        {
            Materialize(id, target);
        }

        return id;
    }

    public Task<Entity> CreateAndReturnAsync(Entity entity, CancellationToken cancellationToken) => service.CreateAndReturnAsync(entity, cancellationToken);
    public Task DeleteAsync(string entityName, Guid id, CancellationToken cancellationToken) => service.DeleteAsync(entityName, id, cancellationToken);
    public Task DeleteAsync(string entityName, Guid id) => service.DeleteAsync(entityName, id);
    public void Delete(string entityName, Guid id) => service.Delete(entityName, id);
    public Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken) =>
        service.DisassociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);
    public Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
        service.DisassociateAsync(entityName, entityId, relationship, relatedEntities);
    public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
        service.Disassociate(entityName, entityId, relationship, relatedEntities);
    public Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request, CancellationToken cancellationToken) =>
        service.ExecuteAsync(request, cancellationToken);
    public Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request) => service.ExecuteAsync(request);
    public OrganizationResponse Execute(OrganizationRequest request) => service.Execute(request);
    public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet) => service.Retrieve(entityName, id, columnSet);
    public Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet, CancellationToken cancellationToken) =>
        service.RetrieveAsync(entityName, id, columnSet, cancellationToken);
    public Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet) => service.RetrieveAsync(entityName, id, columnSet);
    public EntityCollection RetrieveMultiple(QueryBase query) => service.RetrieveMultiple(query);
    public Task<EntityCollection> RetrieveMultipleAsync(QueryBase query, CancellationToken cancellationToken) =>
        service.RetrieveMultipleAsync(query, cancellationToken);
    public Task<EntityCollection> RetrieveMultipleAsync(QueryBase query) => service.RetrieveMultipleAsync(query);
    public void Update(Entity entity) => service.Update(entity);

    public async Task UpdateAsync(Entity entity, CancellationToken cancellationToken)
    {
        EnsureProviderWriteAllowed(entity);
        if (entity is not PluginPackage package)
        {
            await service.UpdateAsync(entity, cancellationToken);
            return;
        }

        var fileId = Guid.NewGuid();
        var content = Convert.FromBase64String(package.Content!);
        EnsureUploadAllowed();
        await service.UpdateAsync(package, cancellationToken);
        UploadCount++;
        await service.UpdateAsync(new PluginPackage(package.Id) { Attributes = { [PluginPackage.LogicalNames.Package] = fileId } }, cancellationToken);
        _packageFiles[package.Id] = content;
        if (materializeOnUpload)
        {
            Materialize(package.Id, target);
        }
    }

    public Task UpdateAsync(Entity entity) => UpdateAsync(entity, CancellationToken.None);

    private void EnsureProviderWriteAllowed(Entity entity)
    {
        if (FailProviderWrites && entity.LogicalName == EntityDataProvider.EntityLogicalName)
        {
            throw new InvalidOperationException("Simulated provider failure before write.");
        }
    }

    private void EnsureUploadAllowed()
    {
        if (FailUpload)
        {
            throw new InvalidOperationException("Simulated upload failure before the package write.");
        }
    }
}
