// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.plugin.Output;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Execution;

public class PluginPushExecutorTests
{
    private const string ClientId = "12345678-1234-1234-1234-123456789abc";

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Reliability", "CA2000", Justification = "TestConsole ownership is transferred to the executor and returned for assertions.")]
    private static (FakeOrganizationServiceAsync Service, PluginPushExecutor Executor, TestConsole Console) CreateExecutorWithConsole()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new AddSolutionComponentExecutor());
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();

        var console = new TestConsole();
        var executor = new PluginPushExecutor(
            new PluginAssemblyRepository(service),
            new PluginPackageRepository(service),
            new SolutionComponentRepository(service),
            new ManagedIdentityRepository(service),
            new PluginTypeReconciler(
                new PluginTypeRepository(service),
                new SdkMessageProcessingStepRepository(service),
                new SdkMessageProcessingStepImageRepository(service),
                new SdkMessageRepository(service),
                new CustomApiRepository(service),
                console),
            new OutdatedAssemblyMigrator(
                new PluginAssemblyRepository(service),
                new PluginTypeRepository(service),
                new SdkMessageProcessingStepRepository(service),
                new CustomApiRepository(service),
                console),
            new PluginPlanRenderer(console),
            console);

        return (service, executor, console);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Reliability", "CA2000", Justification = "TestConsole ownership is transferred to the executor.")]
    private static (FakeOrganizationServiceAsync Service, PluginPushExecutor Executor) CreateExecutor()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new AddSolutionComponentExecutor());
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();

        var executor = new PluginPushExecutor(
            new PluginAssemblyRepository(service),
            new PluginPackageRepository(service),
            new SolutionComponentRepository(service),
            new ManagedIdentityRepository(service),
            new PluginTypeReconciler(
                new PluginTypeRepository(service),
                new SdkMessageProcessingStepRepository(service),
                new SdkMessageProcessingStepImageRepository(service),
                new SdkMessageRepository(service),
                new CustomApiRepository(service),
                new TestConsole()),
            new OutdatedAssemblyMigrator(
                new PluginAssemblyRepository(service),
                new PluginTypeRepository(service),
                new SdkMessageProcessingStepRepository(service),
                new CustomApiRepository(service),
                new TestConsole()),
            new PluginPlanRenderer(new TestConsole()),
            new TestConsole());

        return (service, executor);
    }

    private static LocalAssembly Assembly(string name = "MyPlugins", string version = "1.0.0.0",
        string? managedIdentityClientId = null) => new()
    {
        Name = name,
        Version = Version.Parse(version),
        Content = "base64",
        Kind = LocalAssemblyKind.Plugin,
        ManagedIdentityClientId = managedIdentityClientId
    };

    [Test]
    public async Task ProcessAssemblyAsync_NoRemote_CreatesNewAssembly()
    {
        var (service, executor) = CreateExecutor();

        var id = await executor.ProcessAssemblyAsync(Assembly(), new PluginPushOptions(null, DryRun: false));

        var created = service.Retrieve(PluginAssembly.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginAssembly>();
        await Assert.That(created.Name).IsEqualTo("MyPlugins");
        await Assert.That(created.Content).IsEqualTo("base64");
    }

    [Test]
    public async Task ProcessAssemblyAsync_DryRun_DoesNotCreateAnything()
    {
        var (service, executor) = CreateExecutor();

        var id = await executor.ProcessAssemblyAsync(Assembly(), new PluginPushOptions(null, DryRun: true));

        await Assert.That(id).IsEqualTo(Guid.Empty);
        var all = service.RetrieveMultiple(new QueryExpression(PluginAssembly.EntityLogicalName) { ColumnSet = new ColumnSet(true) });
        await Assert.That(all.Entities.Count).IsEqualTo(0);
    }

    [Test]
    public async Task ProcessAssemblyAsync_DryRun_PreviewsPluginTypesForBrandNewAssembly()
    {
        var (_, executor, console) = CreateExecutorWithConsole();
        var assembly = Assembly() with { PluginTypes = [new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [])] };

        var id = await executor.ProcessAssemblyAsync(assembly, new PluginPushOptions(null, DryRun: true));

        await Assert.That(id).IsEqualTo(Guid.Empty);
        await Assert.That(console.Output).Contains("MyPlugin Create");
        await Assert.That(console.Output).Contains(Spectre.Console.Emoji.Known.PuzzlePiece);
    }

    [Test]
    public async Task ProcessAssemblyAsync_SameMajorMinor_UpdatesContentInPlace()
    {
        var (service, executor) = CreateExecutor();
        var existingId = Guid.NewGuid();
        service.Create(new PluginAssembly(existingId)
        {
            Name = "MyPlugins",
            Version = "1.0.5.0",
            Content = "old",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });

        var id = await executor.ProcessAssemblyAsync(Assembly(version: "1.0.9.0"), new PluginPushOptions(null, DryRun: false));

        await Assert.That(id).IsEqualTo(existingId);
        var updated = service.Retrieve(PluginAssembly.EntityLogicalName, existingId, new ColumnSet(true)).ToEntity<PluginAssembly>();
        await Assert.That(updated.Content).IsEqualTo("base64");
    }

    [Test]
    public async Task ProcessAssemblyAsync_DifferentMajorMinor_CreatesNewAssemblyAndPurgesOldOne()
    {
        var (service, executor) = CreateExecutor();
        var existingId = Guid.NewGuid();
        service.Create(new PluginAssembly(existingId)
        {
            Name = "MyPlugins",
            Version = "1.0.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });

        var id = await executor.ProcessAssemblyAsync(Assembly(version: "2.0.0.0"), new PluginPushOptions(null, DryRun: false));

        await Assert.That(id).IsNotEqualTo(existingId);
        var all = service.RetrieveMultiple(new QueryExpression(PluginAssembly.EntityLogicalName) { ColumnSet = new ColumnSet(true) });
        await Assert.That(all.Entities.Count).IsEqualTo(1);
        await Assert.That(all.Entities[0].Id).IsEqualTo(id);
    }

    [Test]
    public async Task ProcessAssemblyAsync_OwnedByPackage_SkipsContentPush()
    {
        var (service, executor) = CreateExecutor();
        var packageId = Guid.NewGuid();
        var existingId = Guid.NewGuid();
        service.Create(new PluginAssembly(existingId)
        {
            Name = "MyPlugins",
            Version = "9.9.9.9",
            Content = "unchanged",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox),
            PackageId = new EntityReference(PluginPackage.EntityLogicalName, packageId)
        });

        var id = await executor.ProcessAssemblyAsync(Assembly(), new PluginPushOptions(null, DryRun: false));

        await Assert.That(id).IsEqualTo(existingId);
        var untouched = service.Retrieve(PluginAssembly.EntityLogicalName, existingId, new ColumnSet(true)).ToEntity<PluginAssembly>();
        await Assert.That(untouched.Content).IsEqualTo("unchanged");
    }

    [Test]
    public async Task ProcessAssemblyAsync_OwnedByPackage_SkipsStandaloneTypeReconciliation()
    {
        var (service, executor, console) = CreateExecutorWithConsole();
        var packageId = Guid.NewGuid();
        var existingId = Guid.NewGuid();
        service.Create(new PluginAssembly(existingId)
        {
            Name = "MyPlugins",
            Version = "9.9.9.9",
            Content = "unchanged",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox),
            PackageId = new EntityReference(PluginPackage.EntityLogicalName, packageId)
        });

        var assembly = Assembly() with
        {
            PluginTypes = [new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [])]
        };
        await executor.ProcessAssemblyAsync(assembly, new PluginPushOptions(null, DryRun: false));

        using (Assert.Multiple())
        {
            await Assert.That(console.Output).Contains("already owned by a plugin package");
            await Assert.That(console.Output).DoesNotContain("Create PluginType");
            await Assert.That(console.Output).DoesNotContain("Checked");
        }
    }

    [Test]
    public async Task ProcessAssemblyAsync_ManagedIdentityClientId_LinksIdentity()
    {
        var (service, executor) = CreateExecutor();

        var id = await executor.ProcessAssemblyAsync(Assembly(managedIdentityClientId: ClientId), new PluginPushOptions(null, DryRun: false));

        var created = service.Retrieve(PluginAssembly.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginAssembly>();
        await Assert.That(created.ManagedIdentityId).IsNotNull();
    }

    [Test]
    public async Task ProcessPackageAsync_NoRemote_CreatesPackageAndBundledAssemblies()
    {
        var (service, executor) = CreateExecutor();
        var package = new LocalPluginPackage(new LocalPackage("MyPackage", "1.0.0", "pkgcontent"), [Assembly()]);

        var id = await executor.ProcessPackageAsync(package, new PluginPushOptions("TestSolution", DryRun: false, PublisherPrefix: "new"));

        var created = service.Retrieve(PluginPackage.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginPackage>();
        await Assert.That(created.Name).IsEqualTo("new_MyPackage");
        var assemblies = service.RetrieveMultiple(new QueryExpression(PluginAssembly.EntityLogicalName) { ColumnSet = new ColumnSet(true) });
        await Assert.That(assemblies.Entities.Count).IsEqualTo(1);
    }

    [Test]
    public async Task ProcessPackageAsync_Existing_UpdatesContentOnly()
    {
        var (service, executor) = CreateExecutor();
        var packageId = Guid.NewGuid();
        service.Create(new PluginPackage(packageId) { Name = "new_MyPackage", Version = "1.0.0", Content = "old" });
        var package = new LocalPluginPackage(new LocalPackage("MyPackage", "2.0.0", "newcontent"), []);

        var id = await executor.ProcessPackageAsync(package, new PluginPushOptions(null, DryRun: false, PublisherPrefix: "new"));

        await Assert.That(id).IsEqualTo(packageId);
        var updated = service.Retrieve(PluginPackage.EntityLogicalName, packageId, new ColumnSet(true)).ToEntity<PluginPackage>();
        await Assert.That(updated.Content).IsEqualTo("newcontent");
        await Assert.That(updated.Version).IsEqualTo("1.0.0"); // version never changes
    }

    [Test]
    public async Task ProcessPackageAsync_OnlyFirstAssemblyWithManagedIdentity_LinksPackageIdentity()
    {
        var (service, executor) = CreateExecutor();
        var package = new LocalPluginPackage(
            new LocalPackage("MyPackage", "1.0.0", "pkgcontent"),
            [Assembly("First", managedIdentityClientId: ClientId), Assembly("Second", managedIdentityClientId: ClientId)]);

        var id = await executor.ProcessPackageAsync(package, new PluginPushOptions(null, DryRun: false, PublisherPrefix: "new"));

        var created = service.Retrieve(PluginPackage.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginPackage>();
        await Assert.That(created.Managedidentityid).IsNotNull();
        var identities = service.RetrieveMultiple(new QueryExpression(ManagedIdentity.EntityLogicalName) { ColumnSet = new ColumnSet(true) });
        await Assert.That(identities.Entities.Count).IsEqualTo(1);
    }
}
