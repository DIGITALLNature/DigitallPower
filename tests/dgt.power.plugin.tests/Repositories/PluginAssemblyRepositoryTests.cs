// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using System.Security.Cryptography;

namespace dgt.power.plugin.tests.Repositories;

public class PluginAssemblyRepositoryTests
{
    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }

    [Test]
    public async Task FindByNameAsync_NoMatch_ReturnsNull()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);

        var result = await repository.FindByNameAsync("MyPlugins");

        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task FindByNameAsync_SandboxedMatch_ReturnsRemoteAssembly()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);
        var id = Guid.NewGuid();
        service.Create(new PluginAssembly(id)
        {
            Name = "MyPlugins",
            Version = "1.2.3.4",
            Content = "YmFzZTY0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });

        var result = await repository.FindByNameAsync("MyPlugins");

        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Id).IsEqualTo(id);
        await Assert.That(result.Version).IsEqualTo(Version.Parse("1.2.3.4"));
        await Assert.That(result.PackageId).IsNull();
        await Assert.That(result.ContentHash)
            .IsEqualTo(Convert.ToHexString(SHA256.HashData("base64"u8.ToArray())));
    }

    [Test]
    public async Task FindByNameAsync_IgnoresNonSandboxedAssembly()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);
        service.Create(new PluginAssembly(Guid.NewGuid())
        {
            Name = "MyPlugins",
            Version = "1.2.3.4",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.None)
        });

        var result = await repository.FindByNameAsync("MyPlugins");

        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task FindByNameAsync_AssemblyOwnedByPackage_ReturnsPackageId()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);
        var packageId = Guid.NewGuid();
        service.Create(new PluginAssembly(Guid.NewGuid())
        {
            Name = "MyPlugins",
            Version = "1.2.3.4",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox),
            PackageId = new EntityReference(PluginPackage.EntityLogicalName, packageId)
        });

        var result = await repository.FindByNameAsync("MyPlugins");

        await Assert.That(result!.PackageId).IsEqualTo(packageId);
    }

    [Test]
    public async Task FindByNameAsync_MultipleAssemblies_ReturnsHighestSemanticVersion()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);
        var versionNineId = Guid.NewGuid();
        var versionTenId = Guid.NewGuid();
        service.Create(new PluginAssembly(versionNineId)
        {
            Name = "MyPlugins",
            Version = "9.9.9.9",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });
        service.Create(new PluginAssembly(versionTenId)
        {
            Name = "MyPlugins",
            Version = "10.0.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });

        var result = await repository.FindByNameAsync("MyPlugins");

        await Assert.That(result!.Id).IsEqualTo(versionTenId);
        await Assert.That(result.Version).IsEqualTo(Version.Parse("10.0.0.0"));
    }

    [Test]
    public async Task FindForDeploymentAsync_MatchingVersionTrainExists_ReturnsMatchingAssembly()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);
        var versionTwoId = Guid.NewGuid();
        var versionThreeId = Guid.NewGuid();
        service.Create(new PluginAssembly(versionTwoId)
        {
            Name = "MyPlugins",
            Version = "2.1.1.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });
        service.Create(new PluginAssembly(versionThreeId)
        {
            Name = "MyPlugins",
            Version = "3.0.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });

        var result = await repository.FindForDeploymentAsync("MyPlugins", Version.Parse("2.1.2.0"));

        await Assert.That(result!.Id).IsEqualTo(versionTwoId);
    }

    [Test]
    public async Task CreateAsync_CreatesSandboxedDatabaseAssembly()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);

        var id = await repository.CreateAsync("MyPlugins", "base64content");

        var created = service.Retrieve(PluginAssembly.EntityLogicalName, id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<PluginAssembly>();
        await Assert.That(created.Name).IsEqualTo("MyPlugins");
        await Assert.That(created.Content).IsEqualTo("base64content");
        await Assert.That(created.SourceType!.Value).IsEqualTo(PluginAssembly.Options.SourceType.Database);
        await Assert.That(created.IsolationMode!.Value).IsEqualTo(PluginAssembly.Options.IsolationMode.Sandbox);
    }

    [Test]
    public async Task UpdateContentAsync_UpdatesOnlyContent()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);
        var id = Guid.NewGuid();
        service.Create(new PluginAssembly(id) { Name = "MyPlugins", Content = "old" });

        await repository.UpdateContentAsync(id, "new");

        var updated = service.Retrieve(PluginAssembly.EntityLogicalName, id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<PluginAssembly>();
        await Assert.That(updated.Content).IsEqualTo("new");
        await Assert.That(updated.Name).IsEqualTo("MyPlugins");
    }

    [Test]
    public async Task DeleteAsync_RemovesAssembly()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);
        var id = Guid.NewGuid();
        service.Create(new PluginAssembly(id) { Name = "MyPlugins" });

        await repository.DeleteAsync(id);

        await Assert.That(() => service.Retrieve(PluginAssembly.EntityLogicalName, id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true)))
            .Throws<Exception>();
    }

    [Test]
    public async Task ListOutdatedAsync_ExcludesGivenIdAndNonMatchingNames()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);
        var newId = Guid.NewGuid();
        var oldId = Guid.NewGuid();
        service.Create(new PluginAssembly(newId)
        {
            Name = "MyPlugins",
            Version = "2.0.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });
        service.Create(new PluginAssembly(oldId)
        {
            Name = "MyPlugins",
            Version = "1.0.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });
        service.Create(new PluginAssembly(Guid.NewGuid())
        {
            Name = "OtherPlugins",
            Version = "1.0.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });
        service.Create(new PluginAssembly(Guid.NewGuid())
        {
            Name = "MyPlugins",
            Version = "1.5.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox),
            PackageId = new EntityReference(PluginPackage.EntityLogicalName, Guid.NewGuid())
        });

        var result = await repository.ListOutdatedAsync("MyPlugins", newId);

        await Assert.That(result).Count().IsEqualTo(1);
        await Assert.That(result[0].Id).IsEqualTo(oldId);
    }

    [Test]
    public async Task ListOutdatedAsync_NoOtherVersions_ReturnsEmpty()
    {
        var service = CreateService();
        var repository = new PluginAssemblyRepository(service);
        var id = Guid.NewGuid();
        service.Create(new PluginAssembly(id)
        {
            Name = "MyPlugins",
            Version = "1.0.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });

        var result = await repository.ListOutdatedAsync("MyPlugins", id);

        await Assert.That(result).IsEmpty();
    }
}
