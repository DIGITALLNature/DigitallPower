// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;
using System.Text;
using dgt.power.dataverse;
using dgt.power.plugin.Deployment.Execution;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Deployment.Planning.Plans;
using dgt.power.plugin.Deployment.Reconciliation;
using dgt.power.plugin.tests.Repositories;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Deployment.Reconciliation;

public class PackageDeploymentTests
{
    private static readonly byte[] s_initialPackage = "initial-package"u8.ToArray();

    private static LocalPluginType CreateLocalType(string typeName, params LocalPluginStep[] steps) =>
        new(typeName, typeName, string.Empty, true, steps);

    private static LocalAssembly Assembly(string name, params LocalPluginType[] types) => new()
    {
        Name = name,
        Version = new Version(1, 0, 0, 0),
        Content = Convert.ToBase64String(Encoding.UTF8.GetBytes($"assembly-{name}")),
        ContentHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes($"assembly-{name}"))),
        PluginTypes = types
    };

    private static LocalPluginStep Step(string name) => new(name, 0, "Update", 20, "none", "none", null, 100, []);

    private static LocalPluginPackage Package(string content, params LocalAssembly[] assemblies)
    {
        var bytes = Convert.FromBase64String(content);
        return new LocalPluginPackage(
            new LocalPackage("Contoso.Plugins", "1.0.0", content, Convert.ToHexString(SHA256.HashData(bytes))),
            assemblies);
    }

    private static FakeOrganizationServiceAsync CreateStore()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.Create(new SdkMessage(Guid.NewGuid()) { Name = "Update" });
        return service;
    }

    private static IReadOnlyList<Entity> All(FakeOrganizationServiceAsync service, string entityName) =>
        service.RetrieveMultiple(new QueryExpression(entityName) { ColumnSet = new ColumnSet(true) }).Entities;

    [Test]
    public async Task PrepareAndApply_NewPackage_ResolvesServerOwnedAssembliesAndTypes()
    {
        var store = CreateStore();
        var firstType = CreateLocalType("Shared.Plugin", Step("first"));
        var secondType = CreateLocalType("Shared.Plugin", Step("second"));
        var desired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", firstType), Assembly("Second", secondType));
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PreparePackageAsync(desired, "new");
        await Assert.That(changes.HasChanges).IsTrue();
        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(0);

        var packageId = await deployment.ApplyPackageAsync(changes);
        var assemblies = All(store, PluginAssembly.EntityLogicalName);
        var types = All(store, PluginType.EntityLogicalName);
        var steps = All(store, SdkMessageProcessingStep.EntityLogicalName);

        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(assemblies).Count().IsEqualTo(2);
        await Assert.That(assemblies.All(assembly =>
            assembly.GetAttributeValue<EntityReference>(PluginAssembly.LogicalNames.PackageId).Id == packageId)).IsTrue();
        await Assert.That(types).Count().IsEqualTo(2);
        await Assert.That(types.Select(type => type.GetAttributeValue<EntityReference>(PluginType.LogicalNames.PluginAssemblyId).Id).Distinct()).Count().IsEqualTo(2);
        await Assert.That(steps).Count().IsEqualTo(2);
        await Assert.That(steps.Select(step => step.GetAttributeValue<EntityReference>(SdkMessageProcessingStep.LogicalNames.EventHandler).Id).Distinct()).Count().IsEqualTo(2);
    }

    [Test]
    public async Task PrepareAndApply_IdenticalPackage_IsUnchanged()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", desired, s_initialPackage);
        service.MaterializeAssemblies(packageId, desired);
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PreparePackageAsync(desired, "new");
        var progress = new List<PluginDeploymentProgress>();
        await deployment.ApplyPackageAsync(changes, progress.Add);

        await Assert.That(changes.HasChanges).IsFalse();
        await Assert.That(progress).Count().IsEqualTo(0);
        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Apply_UpdatedPackage_AddsRegistrationWithoutReplacingExistingType()
    {
        var oldDesired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var newBytes = "updated-package"u8.ToArray();
        var desired = Package(Convert.ToBase64String(newBytes), Assembly("First", CreateLocalType("First.Plugin", Step("new step"))));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", oldDesired, s_initialPackage);
        service.MaterializeAssemblies(packageId, oldDesired);
        var originalAssembly = All(store, PluginAssembly.EntityLogicalName).Single();
        var originalType = All(store, PluginType.EntityLogicalName).Single();
        var deployment = new PluginDeployment(service);

        var changes = await deployment.PreparePackageAsync(desired, "new");
        await Assert.That(changes.UpdateContent).IsTrue();
        await deployment.ApplyPackageAsync(changes);

        await Assert.That(All(store, PluginPackage.EntityLogicalName).Single().Id).IsEqualTo(packageId);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName).Single().Id).IsEqualTo(originalAssembly.Id);
        await Assert.That(All(store, PluginType.EntityLogicalName).Single().Id).IsEqualTo(originalType.Id);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(service.PackageContent(packageId)).IsEquivalentTo(newBytes);
    }

    [Test]
    public async Task Apply_WhenServerDoesNotCreatePackageAssemblies_FailsWithoutStandaloneFallback()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired, materializeOnUpload: false);
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");

        await Assert.That(async () => await deployment.ApplyPackageAsync(changes)).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(0);
        await Assert.That(All(store, PluginType.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Prepare_ExistingPackageAtDifferentImmutableVersion_FailsBeforeWrites()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        service.SeedPackage("new_Contoso.Plugins", desired with
        {
            Package = desired.Package with { Version = "2.0.0" }
        }, s_initialPackage);
        var deployment = new PluginDeployment(service);

        await Assert.That(async () => await deployment.PreparePackageAsync(desired, "new")).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(0);
    }

    [Test]
    public async Task Prepare_PackageAssemblyRemoval_FailsBeforeUpload()
    {
        var oldDesired = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin")), Assembly("Second", CreateLocalType("Second.Plugin")));
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var packageId = service.SeedPackage("new_Contoso.Plugins", oldDesired, s_initialPackage);
        service.MaterializeAssemblies(packageId, oldDesired);
        var deployment = new PluginDeployment(service);

        await Assert.That(async () => await deployment.PreparePackageAsync(desired, "new")).ThrowsExactly<NotSupportedException>();

        await Assert.That(All(store, PluginAssembly.EntityLogicalName)).Count().IsEqualTo(2);
    }

    [Test]
    public async Task Prepare_PackageStepRemoval_FailsBeforeUpload()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin", Step("registered"))));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired);
        var deployment = new PluginDeployment(service);
        await deployment.ApplyPackageAsync(await deployment.PreparePackageAsync(desired, "new"));
        var removal = desired with { Assemblies = [Assembly("First", CreateLocalType("First.Plugin"))] };

        await Assert.That(async () => await deployment.PreparePackageAsync(removal, "new")).ThrowsExactly<NotSupportedException>();

        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Apply_UnexpectedServerPluginType_FailsBeforeRegistrationWrites()
    {
        var desired = Package(Convert.ToBase64String(s_initialPackage), Assembly("First", CreateLocalType("First.Plugin", Step("new"))));
        var serverPackage = Package(Convert.ToBase64String(s_initialPackage),
            Assembly("First", CreateLocalType("First.Plugin", Step("new")), CreateLocalType("Unexpected.Plugin")));
        var store = CreateStore();
        var service = new PackageUploadService(store, desired, uploadedPackage: serverPackage);
        var deployment = new PluginDeployment(service);
        var changes = await deployment.PreparePackageAsync(desired, "new");

        await Assert.That(async () => await deployment.ApplyPackageAsync(changes)).ThrowsExactly<InvalidOperationException>();

        await Assert.That(All(store, PluginPackage.EntityLogicalName)).Count().IsEqualTo(1);
        await Assert.That(All(store, PluginType.EntityLogicalName)).Count().IsEqualTo(2);
        await Assert.That(All(store, SdkMessageProcessingStep.EntityLogicalName)).Count().IsEqualTo(0);
    }

    private sealed class PackageUploadService : IOrganizationServiceAsync2
    {
        private readonly FakeOrganizationServiceAsync service;
        private readonly LocalPluginPackage target;
        private readonly bool materializeOnUpload;
        private readonly Dictionary<Guid, byte[]> _packageFiles = [];

        public PackageUploadService(FakeOrganizationServiceAsync service, LocalPluginPackage target,
            bool materializeOnUpload = true, LocalPluginPackage? uploadedPackage = null)
        {
            this.service = service;
            this.target = uploadedPackage ?? target;
            this.materializeOnUpload = materializeOnUpload;
            service.AddRequests(new InitializePackageFileDownloadExecutor(_packageFiles));
            service.AddRequests(new DownloadPackageFileBlockExecutor(_packageFiles));
        }

        public byte[] PackageContent(Guid packageId)
            => _packageFiles[packageId];

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

        public void MaterializeAssemblies(Guid packageId, LocalPluginPackage package) =>
            Materialize(packageId, package);

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
                }).Entities.Select(entity => entity.GetAttributeValue<string>(PluginType.LogicalNames.TypeName)).ToHashSet(StringComparer.Ordinal);
                foreach (var pluginType in assembly.PluginTypes.Where(pluginType => !existingTypes.Contains(pluginType.TypeName)))
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
            var id = await service.CreateAsync(package, cancellationToken);
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
            if (entity is not PluginPackage package)
            {
                await service.UpdateAsync(entity, cancellationToken);
                return;
            }

            var fileId = Guid.NewGuid();
            var content = Convert.FromBase64String(package.Content!);
            await service.UpdateAsync(package, cancellationToken);
            await service.UpdateAsync(new PluginPackage(package.Id) { Attributes = { [PluginPackage.LogicalNames.Package] = fileId } }, cancellationToken);
            _packageFiles[package.Id] = content;
            if (materializeOnUpload)
            {
                Materialize(package.Id, target);
            }
        }

        public Task UpdateAsync(Entity entity) => UpdateAsync(entity, CancellationToken.None);
    }
}
