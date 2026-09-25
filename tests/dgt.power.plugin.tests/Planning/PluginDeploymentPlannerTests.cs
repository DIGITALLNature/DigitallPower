// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;

namespace dgt.power.plugin.tests.Planning;

public class PluginDeploymentPlannerTests
{
    private static PluginDeploymentPlanner CreatePlanner(FakeOrganizationServiceAsync service) =>
        new(new PluginPlanningRepositories
        {
            Assemblies = new PluginAssemblyRepository(service),
            Packages = new PluginPackageRepository(service),
            Types = new PluginTypeRepository(service),
            Steps = new SdkMessageProcessingStepRepository(service),
            Images = new SdkMessageProcessingStepImageRepository(service),
            Messages = new SdkMessageRepository(service),
            CustomApis = new CustomApiRepository(service),
            Solutions = new SolutionComponentRepository(service)
        });

    [Test]
    public async Task BuildPackageAsync_SolutionComponentTypeMissing_Throws()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        var planner = CreatePlanner(service);
        var package = new LocalPluginPackage(
            new LocalPackage("Contoso.Plugins", "1.0.0", string.Empty, string.Empty),
            []);

        await Assert.That(async () => await planner.BuildPackageAsync(
                package,
                new PluginPushOptions("contoso_solution", DryRun: false, PublisherPrefix: "contoso")))
            .ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task BuildAssemblyAsync_MultipleStandaloneVersionTrains_ThrowsBeforePlanning()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        service.Create(new PluginAssembly(Guid.NewGuid())
        {
            Name = "Contoso.Plugins",
            Version = "1.0.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });
        service.Create(new PluginAssembly(Guid.NewGuid())
        {
            Name = "Contoso.Plugins",
            Version = "2.0.0.0",
            SourceType = new OptionSetValue(PluginAssembly.Options.SourceType.Database),
            IsolationMode = new OptionSetValue(PluginAssembly.Options.IsolationMode.Sandbox)
        });
        var assembly = new LocalAssembly
        {
            Name = "Contoso.Plugins",
            Version = Version.Parse("3.0.0.0"),
            Content = "Y29udGVudA==",
            ContentHash = "hash"
        };

        await Assert.That(async () => await CreatePlanner(service).BuildAssemblyAsync(
                assembly,
                new PluginPushOptions(null, DryRun: false)))
            .ThrowsExactly<InvalidOperationException>();
    }
}
