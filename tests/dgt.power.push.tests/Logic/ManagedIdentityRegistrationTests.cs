// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.push.Logic;
using dgt.power.tests.Extensions;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.push.tests.Logic;

public class ManagedIdentityRegistrationTests
{
    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();

        // PluginAssembly entity with managedidentityid attribute
        var pluginAssemblyMeta = new EntityMetadata();
        pluginAssemblyMeta.GetType().GetProperty("LogicalName")!.SetValue(pluginAssemblyMeta, "pluginassembly");
        var miIdAttr = new LookupAttributeMetadata { LogicalName = "managedidentityid" };
        pluginAssemblyMeta.SetAttributeCollection([miIdAttr]);
        service.AddMetadata(pluginAssemblyMeta);

        // PluginPackage entity with managedidentityid attribute
        var pluginPackageMeta = new EntityMetadata();
        pluginPackageMeta.GetType().GetProperty("LogicalName")!.SetValue(pluginPackageMeta, PluginPackage.EntityLogicalName);
        var pkgMiIdAttr = new LookupAttributeMetadata { LogicalName = "managedidentityid" };
        pluginPackageMeta.SetAttributeCollection([pkgMiIdAttr]);
        service.AddMetadata(pluginPackageMeta);

        // ManagedIdentity entity with attributes
        var managedIdentityMeta = new EntityMetadata();
        managedIdentityMeta.GetType().GetProperty("LogicalName")!.SetValue(managedIdentityMeta, ManagedIdentity.EntityLogicalName);
        var appIdAttr = new UniqueIdentifierAttributeMetadata { LogicalName = ManagedIdentity.LogicalNames.ApplicationId };
        var tenantIdAttr = new UniqueIdentifierAttributeMetadata { LogicalName = ManagedIdentity.LogicalNames.TenantId };
        var credSrcAttr = new PicklistAttributeMetadata { LogicalName = ManagedIdentity.LogicalNames.CredentialSource };
        var scopeAttr = new PicklistAttributeMetadata { LogicalName = ManagedIdentity.LogicalNames.SubjectScope };
        managedIdentityMeta.SetAttributeCollection([appIdAttr, tenantIdAttr, credSrcAttr, scopeAttr]);
        service.AddMetadata(managedIdentityMeta);

        return service;
    }

    [Test]
    public async Task LinkManagedIdentity_CreatesNewIdentityWhenNotExists()
    {
        // Arrange
        var service = CreateService();
        var console = new TestConsole();
        using var processor = new AssemblyProcessor(service, console);

        var assemblyId = Guid.NewGuid();
        var assembly = new PluginAssembly(assemblyId) { Name = "TestAssembly", Content = "base64" };
        service.Create(assembly);

        var clientId = "12345678-1234-1234-1234-123456789abc";

        // Act
        processor.LinkManagedIdentity(assemblyId, clientId, null);

        // Assert - verify a managedidentity was created
        var identities = service.RetrieveMultiple(new QueryExpression(ManagedIdentity.EntityLogicalName) { ColumnSet = new ColumnSet(true) });
        await Assert.That(identities.Entities.Count).IsEqualTo(1);
        await Assert.That(identities.Entities[0].GetAttributeValue<Guid?>(ManagedIdentity.LogicalNames.ApplicationId)).IsEqualTo(Guid.Parse(clientId));

        // Assert - verify assembly was updated with the link
        var updatedAssembly = service.Retrieve("pluginassembly", assemblyId, new ColumnSet("managedidentityid"));
        var miRef = updatedAssembly.GetAttributeValue<EntityReference>("managedidentityid");
        await Assert.That(miRef).IsNotNull();
        await Assert.That(miRef!.LogicalName).IsEqualTo(ManagedIdentity.EntityLogicalName);
    }

    [Test]
    public async Task LinkManagedIdentity_ReusesExistingIdentity()
    {
        // Arrange
        var service = CreateService();
        var console = new TestConsole();
        using var processor = new AssemblyProcessor(service, console);

        var assemblyId = Guid.NewGuid();
        var assembly = new PluginAssembly(assemblyId) { Name = "TestAssembly", Content = "base64" };
        service.Create(assembly);

        var clientId = "12345678-1234-1234-1234-123456789abc";
        var existingMiId = Guid.NewGuid();
        var existingMi = new ManagedIdentity(existingMiId)
        {
            ApplicationId = Guid.Parse(clientId),
            CredentialSource = new OptionSetValue(ManagedIdentity.Options.CredentialSource.IsManaged),
            SubjectScope = new OptionSetValue(ManagedIdentity.Options.SubjectScope.EnviornmentScope)
        };
        service.Create(existingMi);

        // Act
        processor.LinkManagedIdentity(assemblyId, clientId, null);

        // Assert - no new identity created (still just 1)
        var identities = service.RetrieveMultiple(new QueryExpression(ManagedIdentity.EntityLogicalName) { ColumnSet = new ColumnSet(true) });
        await Assert.That(identities.Entities.Count).IsEqualTo(1);

        // Assert - assembly was linked to existing identity
        var updatedAssembly = service.Retrieve("pluginassembly", assemblyId, new ColumnSet("managedidentityid"));
        var miRef = updatedAssembly.GetAttributeValue<EntityReference>("managedidentityid");
        await Assert.That(miRef).IsNotNull();
        await Assert.That(miRef!.Id).IsEqualTo(existingMiId);
    }

    [Test]
    public async Task LinkManagedIdentity_WithTenantId_SetsTenantOnCreatedIdentity()
    {
        // Arrange
        var service = CreateService();
        var console = new TestConsole();
        using var processor = new AssemblyProcessor(service, console);

        var assemblyId = Guid.NewGuid();
        var assembly = new PluginAssembly(assemblyId) { Name = "TestAssembly", Content = "base64" };
        service.Create(assembly);

        var clientId = "12345678-1234-1234-1234-123456789abc";
        var tenantId = "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee";

        // Act
        processor.LinkManagedIdentity(assemblyId, clientId, tenantId);

        // Assert - verify tenantid was set
        var identities = service.RetrieveMultiple(new QueryExpression(ManagedIdentity.EntityLogicalName) { ColumnSet = new ColumnSet(true) });
        await Assert.That(identities.Entities.Count).IsEqualTo(1);
        await Assert.That(identities.Entities[0].GetAttributeValue<Guid?>(ManagedIdentity.LogicalNames.TenantId)).IsEqualTo(Guid.Parse(tenantId));
    }

    [Test]
    public async Task LinkManagedIdentity_OutputsLinkingInfo()
    {
        // Arrange
        var service = CreateService();
        var console = new TestConsole();
        using var processor = new AssemblyProcessor(service, console);

        var assemblyId = Guid.NewGuid();
        var assembly = new PluginAssembly(assemblyId) { Name = "TestAssembly", Content = "base64" };
        service.Create(assembly);

        var clientId = "12345678-1234-1234-1234-123456789abc";

        // Act
        processor.LinkManagedIdentity(assemblyId, clientId, null);

        // Assert - console output
        var output = console.Output;
        await Assert.That(output).Contains("ManagedIdentity");
        await Assert.That(output).Contains(clientId);
        await Assert.That(output).Contains("Linked");
    }

    [Test]
    public async Task LinkManagedIdentityToPackage_CreatesNewIdentityAndLinksToPackage()
    {
        // Arrange
        var service = CreateService();
        var console = new TestConsole();
        using var processor = new AssemblyProcessor(service, console);

        var packageId = Guid.NewGuid();
        var package = new PluginPackage(packageId) { Name = "TestPackage", Version = "1.0.0.0" };
        service.Create(package);

        var clientId = "12345678-1234-1234-1234-123456789abc";

        // Act
        processor.LinkManagedIdentityToPackage(packageId, clientId, null);

        // Assert - verify a managedidentity was created
        var identities = service.RetrieveMultiple(new QueryExpression(ManagedIdentity.EntityLogicalName) { ColumnSet = new ColumnSet(true) });
        await Assert.That(identities.Entities.Count).IsEqualTo(1);

        // Assert - verify package was updated with the link
        var updatedPackage = service.Retrieve(PluginPackage.EntityLogicalName, packageId, new ColumnSet("managedidentityid"));
        var miRef = updatedPackage.GetAttributeValue<EntityReference>("managedidentityid");
        await Assert.That(miRef).IsNotNull();
        await Assert.That(miRef!.LogicalName).IsEqualTo(ManagedIdentity.EntityLogicalName);
    }

    [Test]
    public async Task LinkManagedIdentityToPackage_ReusesExistingIdentity()
    {
        // Arrange
        var service = CreateService();
        var console = new TestConsole();
        using var processor = new AssemblyProcessor(service, console);

        var packageId = Guid.NewGuid();
        var package = new PluginPackage(packageId) { Name = "TestPackage", Version = "1.0.0.0" };
        service.Create(package);

        var clientId = "12345678-1234-1234-1234-123456789abc";
        var existingMiId = Guid.NewGuid();
        var existingMi = new ManagedIdentity(existingMiId)
        {
            ApplicationId = Guid.Parse(clientId),
            CredentialSource = new OptionSetValue(ManagedIdentity.Options.CredentialSource.IsManaged),
            SubjectScope = new OptionSetValue(ManagedIdentity.Options.SubjectScope.EnviornmentScope)
        };
        service.Create(existingMi);

        // Act
        processor.LinkManagedIdentityToPackage(packageId, clientId, null);

        // Assert - no new identity created (still just 1)
        var identities = service.RetrieveMultiple(new QueryExpression(ManagedIdentity.EntityLogicalName) { ColumnSet = new ColumnSet(true) });
        await Assert.That(identities.Entities.Count).IsEqualTo(1);

        // Assert - package was linked to existing identity
        var updatedPackage = service.Retrieve(PluginPackage.EntityLogicalName, packageId, new ColumnSet("managedidentityid"));
        var miRef = updatedPackage.GetAttributeValue<EntityReference>("managedidentityid");
        await Assert.That(miRef).IsNotNull();
        await Assert.That(miRef!.Id).IsEqualTo(existingMiId);
    }
}
