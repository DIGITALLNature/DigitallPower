// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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
    private readonly FakeOrganizationServiceAsync _service;
    private LocalPluginPackage _target;
    private readonly bool _materializeOnUpload;
    private readonly Dictionary<Guid, byte[]> _packageFiles = [];

    public int UploadCount { get; private set; }
    public bool FailUpload { get; set; }
    public bool FailProviderWrites { get; set; }

    public PackageUploadService(FakeOrganizationServiceAsync service, LocalPluginPackage target,
        bool materializeOnUpload = true, LocalPluginPackage? uploadedPackage = null)
    {
        _service = service;
        _target = uploadedPackage ?? target;
        _materializeOnUpload = materializeOnUpload;
        service.AddRequests(new InitializePackageFileDownloadExecutor(_packageFiles));
        service.AddRequests(new DownloadPackageFileBlockExecutor(_packageFiles));
    }

    public byte[] PackageContent(Guid packageId) => _packageFiles[packageId];

    public void RetargetUpload(LocalPluginPackage package) => _target = package;

    public Guid SeedPackage(string name, LocalPluginPackage package, byte[] bytes)
    {
        var fileId = Guid.NewGuid();
        var id = _service.Create(new PluginPackage
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
            var existing = _service.RetrieveMultiple(new QueryExpression(PluginAssembly.EntityLogicalName)
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
            var assemblyId = existing?.Id ?? _service.Create(new PluginAssembly
            {
                Name = assembly.Name,
                Version = assembly.Version.ToString(),
                SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
                IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox),
                PackageId = new EntityReference(PluginPackage.EntityLogicalName, packageId)
            });

            var existingTypes = _service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName)
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
                DeleteType(typeName, typeId);
            }

            foreach (var pluginType in assembly.PluginTypes.Where(pluginType => !existingTypes.ContainsKey(pluginType.TypeName)))
            {
                _service.Create(new PluginType
                {
                    PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, assemblyId),
                    TypeName = pluginType.TypeName,
                    Name = pluginType.Name,
                    FriendlyName = pluginType.Name
                });
            }
        }

        // Assumed platform behavior (to be confirmed live): an assembly missing from the uploaded package is removed with its types.
        var declaredAssemblies = package.Assemblies.Select(assembly => assembly.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var packageAssemblies = _service.RetrieveMultiple(new QueryExpression(PluginAssembly.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(PluginAssembly.LogicalNames.Name),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(PluginAssembly.LogicalNames.PackageId, ConditionOperator.Equal, packageId) }
            }
        }).Entities;
        foreach (var removed in packageAssemblies.Where(entity => !declaredAssemblies.Contains(entity.GetAttributeValue<string>(PluginAssembly.LogicalNames.Name))))
        {
            var types = _service.RetrieveMultiple(new QueryExpression(PluginType.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(PluginType.LogicalNames.TypeName),
                Criteria = new FilterExpression
                {
                    Conditions = { new ConditionExpression(PluginType.LogicalNames.PluginAssemblyId, ConditionOperator.Equal, removed.Id) }
                }
            }).Entities;
            foreach (var type in types)
            {
                DeleteType(type.GetAttributeValue<string>(PluginType.LogicalNames.TypeName), type.Id);
            }

            _service.Delete(PluginAssembly.EntityLogicalName, removed.Id);
        }
    }

    private void DeleteType(string typeName, Guid typeId)
    {
        var steps = _service.RetrieveMultiple(new QueryExpression(SdkMessageProcessingStep.EntityLogicalName)
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

        _service.Delete(PluginType.EntityLogicalName, typeId);
    }

    public Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken) =>
        _service.AssociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);
    public Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
        _service.AssociateAsync(entityName, entityId, relationship, relatedEntities);
    public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
        _service.Associate(entityName, entityId, relationship, relatedEntities);
    public Guid Create(Entity entity) => _service.Create(entity);
    public Task<Guid> CreateAsync(Entity entity, CancellationToken cancellationToken)
    {
        EnsureProviderWriteAllowed(entity);
        if (entity is not PluginPackage package)
        {
            return _service.CreateAsync(entity, cancellationToken);
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
        var id = await _service.CreateAsync(package, cancellationToken);
        UploadCount++;
        _packageFiles.Add(id, content);
        if (_materializeOnUpload)
        {
            Materialize(id, _target);
        }

        return id;
    }

    public Task<Entity> CreateAndReturnAsync(Entity entity, CancellationToken cancellationToken) => _service.CreateAndReturnAsync(entity, cancellationToken);
    public Task DeleteAsync(string entityName, Guid id, CancellationToken cancellationToken) => _service.DeleteAsync(entityName, id, cancellationToken);
    public Task DeleteAsync(string entityName, Guid id) => _service.DeleteAsync(entityName, id);
    public void Delete(string entityName, Guid id) => _service.Delete(entityName, id);
    public Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken) =>
        _service.DisassociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);
    public Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
        _service.DisassociateAsync(entityName, entityId, relationship, relatedEntities);
    public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities) =>
        _service.Disassociate(entityName, entityId, relationship, relatedEntities);
    public Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request, CancellationToken cancellationToken) =>
        _service.ExecuteAsync(request, cancellationToken);
    public Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request) => _service.ExecuteAsync(request);
    public OrganizationResponse Execute(OrganizationRequest request) => _service.Execute(request);
    public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet) => _service.Retrieve(entityName, id, columnSet);
    public Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet, CancellationToken cancellationToken) =>
        _service.RetrieveAsync(entityName, id, columnSet, cancellationToken);
    public Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet) => _service.RetrieveAsync(entityName, id, columnSet);
    public EntityCollection RetrieveMultiple(QueryBase query) => _service.RetrieveMultiple(query);
    public Task<EntityCollection> RetrieveMultipleAsync(QueryBase query, CancellationToken cancellationToken) =>
        _service.RetrieveMultipleAsync(query, cancellationToken);
    public Task<EntityCollection> RetrieveMultipleAsync(QueryBase query) => _service.RetrieveMultipleAsync(query);
    public void Update(Entity entity) => _service.Update(entity);

    public async Task UpdateAsync(Entity entity, CancellationToken cancellationToken)
    {
        EnsureProviderWriteAllowed(entity);
        if (entity is not PluginPackage package)
        {
            await _service.UpdateAsync(entity, cancellationToken);
            return;
        }

        var fileId = Guid.NewGuid();
        var content = Convert.FromBase64String(package.Content!);
        EnsureUploadAllowed();
        await _service.UpdateAsync(package, cancellationToken);
        UploadCount++;
        await _service.UpdateAsync(new PluginPackage(package.Id) { Attributes = { [PluginPackage.LogicalNames.Package] = fileId } }, cancellationToken);
        _packageFiles[package.Id] = content;
        if (_materializeOnUpload)
        {
            Materialize(package.Id, _target);
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
