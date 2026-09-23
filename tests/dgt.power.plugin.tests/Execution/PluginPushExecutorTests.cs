// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.tests.Repositories;
using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.plugin.Output;
using dgt.power.plugin.Planning;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using Spectre.Console.Testing;
using System.Security.Cryptography;

namespace dgt.power.plugin.tests.Execution;

public class PluginPushExecutorTests
{
    private const string ClientId = "12345678-1234-1234-1234-123456789abc";

    private sealed class DeploymentTestHarness(
        PluginDeploymentPlanner planner,
        PluginPlanRenderer renderer,
        PluginPushExecutor executor,
        TestConsole console)
    {
        public async Task<Guid> ProcessAssemblyAsync(LocalAssembly assembly, PluginPushOptions options)
        {
            console.MarkupLine("[bold blue]Plan[/]");
            var plan = await planner.BuildAssemblyAsync(assembly, options);
            renderer.Render(plan);
            if (options.DryRun)
            {
                return Guid.Empty;
            }
            console.MarkupLine("[bold green]Execution[/]");
            var completedOperationCount = 0;
            var id = await executor.ExecuteAsync(plan, progress =>
            {
                completedOperationCount++;
                console.MarkupLine(
                    $"[green]{Emoji.Known.CheckMark}[/] {progress.Operation} {Markup.Escape(progress.Resource)} {Markup.Escape(progress.Name)}");
            });
            console.MarkupLine(completedOperationCount == 0
                ? $"[green]{Emoji.Known.CheckMark}[/] No changes applied"
                : $"[green]{Emoji.Known.CheckMark}[/] Deployment completed");
            return id;
        }

        public async Task<Guid> ProcessPackageAsync(LocalPluginPackage package, PluginPushOptions options)
        {
            console.MarkupLine("[bold blue]Plan[/]");
            var plan = await planner.BuildPackageAsync(package, options);
            renderer.Render(plan);
            if (options.DryRun)
            {
                return Guid.Empty;
            }
            console.MarkupLine("[bold green]Execution[/]");
            var completedOperationCount = 0;
            var id = await executor.ExecuteAsync(plan, progress =>
            {
                completedOperationCount++;
                console.MarkupLine(
                    $"[green]{Emoji.Known.CheckMark}[/] {progress.Operation} {Markup.Escape(progress.Resource)} {Markup.Escape(progress.Name)}");
            });
            console.MarkupLine(completedOperationCount == 0
                ? $"[green]{Emoji.Known.CheckMark}[/] No changes applied"
                : $"[green]{Emoji.Known.CheckMark}[/] Deployment completed");
            return id;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Reliability", "CA2000", Justification = "TestConsole ownership is transferred to the executor and returned for assertions.")]
    private static (FakeOrganizationServiceAsync Service, DeploymentTestHarness Executor, TestConsole Console) CreateExecutorWithConsole(
        IReadOnlyDictionary<Guid, byte[]>? packageFiles = null)
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new AddSolutionComponentExecutor());
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        if (packageFiles is not null)
        {
            service.AddRequests(new InitializePackageFileDownloadExecutor(packageFiles));
            service.AddRequests(new DownloadPackageFileBlockExecutor(packageFiles));
        }

        service.AddDefaultRequests();

        var console = new TestConsole();
        var assemblyRepository = new PluginAssemblyRepository(service);
        var packageRepository = new PluginPackageRepository(service);
        var solutionRepository = new SolutionComponentRepository(service);
        var typeRepository = new PluginTypeRepository(service);
        var stepRepository = new SdkMessageProcessingStepRepository(service);
        var imageRepository = new SdkMessageProcessingStepImageRepository(service);
        var customApiRepository = new CustomApiRepository(service);
        var planner = new PluginDeploymentPlanner(new PluginPlanningRepositories
        {
            Assemblies = assemblyRepository,
            Packages = packageRepository,
            Types = typeRepository,
            Steps = stepRepository,
            Images = imageRepository,
            Messages = new SdkMessageRepository(service),
            CustomApis = customApiRepository,
            Solutions = solutionRepository
        });
        var executor = new DeploymentTestHarness(
            planner,
            new PluginPlanRenderer(console),
            new PluginPushExecutor(
                assemblyRepository,
                packageRepository,
                solutionRepository,
                new ManagedIdentityRepository(service),
                new PluginTypeDeploymentExecutor(typeRepository, stepRepository, imageRepository, customApiRepository),
                new OutdatedAssemblyMigrator(
                    assemblyRepository,
                    typeRepository,
                    stepRepository,
                    customApiRepository)),
            console);

        return (service, executor, console);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Reliability", "CA2000", Justification = "TestConsole ownership is transferred to the executor.")]
    private static (FakeOrganizationServiceAsync Service, DeploymentTestHarness Executor) CreateExecutor()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new AddSolutionComponentExecutor());
        service.AddRequests(new RetrieveDependenciesForDeleteExecutor());
        service.AddDefaultRequests();

        var console = new TestConsole();
        var assemblyRepository = new PluginAssemblyRepository(service);
        var packageRepository = new PluginPackageRepository(service);
        var solutionRepository = new SolutionComponentRepository(service);
        var typeRepository = new PluginTypeRepository(service);
        var stepRepository = new SdkMessageProcessingStepRepository(service);
        var imageRepository = new SdkMessageProcessingStepImageRepository(service);
        var customApiRepository = new CustomApiRepository(service);
        var planner = new PluginDeploymentPlanner(new PluginPlanningRepositories
        {
            Assemblies = assemblyRepository,
            Packages = packageRepository,
            Types = typeRepository,
            Steps = stepRepository,
            Images = imageRepository,
            Messages = new SdkMessageRepository(service),
            CustomApis = customApiRepository,
            Solutions = solutionRepository
        });
        var executor = new DeploymentTestHarness(
            planner,
            new PluginPlanRenderer(console),
            new PluginPushExecutor(
                assemblyRepository,
                packageRepository,
                solutionRepository,
                new ManagedIdentityRepository(service),
                new PluginTypeDeploymentExecutor(typeRepository, stepRepository, imageRepository, customApiRepository),
                new OutdatedAssemblyMigrator(
                    assemblyRepository,
                    typeRepository,
                    stepRepository,
                    customApiRepository)),
            console);

        return (service, executor);
    }

    private static LocalAssembly Assembly(string name = "MyPlugins", string version = "1.0.0.0",
        string? managedIdentityClientId = null) => new()
    {
        Name = name,
        Version = Version.Parse(version),
        Content = "YmFzZTY0",
        ContentHash = Convert.ToHexString(SHA256.HashData("base64"u8.ToArray())),
        Kind = LocalAssemblyKind.Plugin,
        ManagedIdentityClientId = managedIdentityClientId
    };

    private static LocalPackage Package(string name, string version, string content) =>
        new(name, version, content, Convert.ToHexString(SHA256.HashData(Convert.FromBase64String(content))));

    [Test]
    public async Task ProcessAssemblyAsync_NoRemote_CreatesNewAssembly()
    {
        var (service, executor) = CreateExecutor();

        var id = await executor.ProcessAssemblyAsync(Assembly(), new PluginPushOptions(null, DryRun: false));

        var created = service.Retrieve(PluginAssembly.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginAssembly>();
        await Assert.That(created.Name).IsEqualTo("MyPlugins");
        await Assert.That(created.Content).IsEqualTo("YmFzZTY0");
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
        await Assert.That(console.Output).Contains("MyPlugins Create");
        await Assert.That(console.Output).Contains("MyPlugin Create");
        await Assert.That(console.Output).Contains(Spectre.Console.Emoji.Known.PuzzlePiece);
    }

    [Test]
    public async Task ProcessAssemblyAsync_DryRun_RendersPlanWithoutExecutionPhase()
    {
        var (_, executor, console) = CreateExecutorWithConsole();

        await executor.ProcessAssemblyAsync(Assembly(), new PluginPushOptions(null, DryRun: true));

        using (Assert.Multiple())
        {
            await Assert.That(console.Output).Contains("Plan");
            await Assert.That(console.Output).DoesNotContain("Execution");
        }
    }

    [Test]
    public async Task ProcessAssemblyAsync_ExecutesAfterRenderingPlan()
    {
        var (_, executor, console) = CreateExecutorWithConsole();

        await executor.ProcessAssemblyAsync(Assembly(), new PluginPushOptions(null, DryRun: false));

        using (Assert.Multiple())
        {
            await Assert.That(console.Output).Contains("Plan");
            await Assert.That(console.Output).Contains("Execution");
            await Assert.That(console.Output).Contains("Created assembly MyPlugins");
            await Assert.That(console.Output).Contains("Deployment completed");
            await Assert.That(console.Output.IndexOf("Plan", StringComparison.Ordinal))
                .IsLessThan(console.Output.IndexOf("Execution", StringComparison.Ordinal));
        }
    }

    [Test]
    public async Task ProcessAssemblyAsync_DryRunUpgrade_PlansReplacementAndCurrentAssemblyPurge()
    {
        var (service, executor, console) = CreateExecutorWithConsole();
        var existingAssemblyId = Guid.NewGuid();
        service.Create(new PluginAssembly(existingAssemblyId)
        {
            Name = "MyPlugins",
            Version = "1.0.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });
        service.Create(new PluginType(Guid.NewGuid())
        {
            TypeName = "MyPlugin",
            PluginAssemblyId = new EntityReference(PluginAssembly.EntityLogicalName, existingAssemblyId)
        });
        var assembly = Assembly(version: "2.0.0.0") with
        {
            PluginTypes = [new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [])]
        };

        await executor.ProcessAssemblyAsync(assembly, new PluginPushOptions(null, DryRun: true));

        using (Assert.Multiple())
        {
            await Assert.That(console.Output).Contains("MyPlugins Upgrade");
            await Assert.That(console.Output).Contains("MyPlugin Create");
            await Assert.That(console.Output).Contains("Outdated assemblies");
            await Assert.That(console.Output).Contains(existingAssemblyId.ToString());
            await Assert.That(console.Output).Contains("Purge");
        }
    }

    [Test]
    public async Task ProcessAssemblyAsync_DryRunWithSolution_IncludesSolutionLink()
    {
        var (_, executor, console) = CreateExecutorWithConsole();

        await executor.ProcessAssemblyAsync(
            Assembly(),
            new PluginPushOptions("TestSolution", DryRun: true));

        await Assert.That(console.Output).Contains("Solution TestSolution Link");
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
            Content = "b2xk",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });

        var id = await executor.ProcessAssemblyAsync(Assembly(version: "1.0.9.0"), new PluginPushOptions(null, DryRun: false));

        await Assert.That(id).IsEqualTo(existingId);
        var updated = service.Retrieve(PluginAssembly.EntityLogicalName, existingId, new ColumnSet(true)).ToEntity<PluginAssembly>();
        await Assert.That(updated.Content).IsEqualTo("YmFzZTY0");
    }

    [Test]
    public async Task ProcessAssemblyAsync_IdenticalContent_DoesNotUpdateAssembly()
    {
        var (service, executor, console) = CreateExecutorWithConsole();
        var existingId = Guid.NewGuid();
        service.Create(new PluginAssembly(existingId)
        {
            Name = "MyPlugins",
            Version = "1.0.0.0",
            Content = "YmFzZTY0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });

        var id = await executor.ProcessAssemblyAsync(Assembly(), new PluginPushOptions(null, DryRun: false));

        using (Assert.Multiple())
        {
            await Assert.That(id).IsEqualTo(existingId);
            await Assert.That(console.Output).Contains("MyPlugins Unchanged");
            await Assert.That(console.Output).Contains("No changes applied");
            await Assert.That(console.Output).DoesNotContain("Updated assembly");
        }
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

        var assembly = Assembly(managedIdentityClientId: ClientId) with
        {
            PluginTypes = [new LocalPluginType("MyPlugin", "MyPlugin", string.Empty, true, [])]
        };
        await executor.ProcessAssemblyAsync(assembly, new PluginPushOptions(null, DryRun: false));

        using (Assert.Multiple())
        {
            await Assert.That(console.Output).Contains(Spectre.Console.Emoji.Known.PuzzlePiece);
            await Assert.That(console.Output).DoesNotContain("Managed by package");
            await Assert.That(console.Output).DoesNotContain("└── MyPlugin");
            await Assert.That(console.Output).DoesNotContain("Managed identity");
            await Assert.That(console.Output).DoesNotContain("Checked");
            await Assert.That(console.Output).Contains("No changes applied");
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
        var package = new LocalPluginPackage(Package("MyPackage", "1.0.0", "cGtnY29udGVudA=="), [Assembly()]);

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
        var package = new LocalPluginPackage(Package("MyPackage", "2.0.0", "bmV3Y29udGVudA=="), []);

        var id = await executor.ProcessPackageAsync(package, new PluginPushOptions(null, DryRun: false, PublisherPrefix: "new"));

        await Assert.That(id).IsEqualTo(packageId);
        var updated = service.Retrieve(PluginPackage.EntityLogicalName, packageId, new ColumnSet(true)).ToEntity<PluginPackage>();
        await Assert.That(updated.Content).IsEqualTo("bmV3Y29udGVudA==");
        await Assert.That(updated.Version).IsEqualTo("1.0.0"); // version never changes
    }

    [Test]
    public async Task ProcessPackageAsync_IdenticalContent_DoesNotUpdatePackage()
    {
        var packageId = Guid.NewGuid();
        var packageFile = "pkgcontent"u8.ToArray();
        var (service, executor, console) = CreateExecutorWithConsole(
            new Dictionary<Guid, byte[]> { [packageId] = packageFile });
        var remotePackage = new PluginPackage(packageId)
        {
            Name = "new_MyPackage",
            Version = "1.0.0"
        };
        remotePackage.Attributes[PluginPackage.LogicalNames.Package] = Guid.NewGuid();
        service.Create(remotePackage);
        var package = new LocalPluginPackage(
            Package("MyPackage", "2.0.0", Convert.ToBase64String(packageFile)),
            []);

        var id = await executor.ProcessPackageAsync(
            package,
            new PluginPushOptions(null, DryRun: false, PublisherPrefix: "new"));

        using (Assert.Multiple())
        {
            await Assert.That(id).IsEqualTo(packageId);
            await Assert.That(console.Output).Contains("MyPackage Unchanged");
            await Assert.That(console.Output).Contains("No changes applied");
            await Assert.That(console.Output).DoesNotContain("Updated package");
        }
    }

    [Test]
    public async Task ProcessPackageAsync_OnlyFirstAssemblyWithManagedIdentity_LinksPackageIdentity()
    {
        var (service, executor) = CreateExecutor();
        var package = new LocalPluginPackage(
            Package("MyPackage", "1.0.0", "cGtnY29udGVudA=="),
            [Assembly("First", managedIdentityClientId: ClientId), Assembly("Second", managedIdentityClientId: ClientId)]);

        var id = await executor.ProcessPackageAsync(package, new PluginPushOptions(null, DryRun: false, PublisherPrefix: "new"));

        var created = service.Retrieve(PluginPackage.EntityLogicalName, id, new ColumnSet(true)).ToEntity<PluginPackage>();
        await Assert.That(created.Managedidentityid).IsNotNull();
        var identities = service.RetrieveMultiple(new QueryExpression(ManagedIdentity.EntityLogicalName) { ColumnSet = new ColumnSet(true) });
        await Assert.That(identities.Entities.Count).IsEqualTo(1);
    }
}
