// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Execution;
using dgt.power.plugin.Local;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;

namespace dgt.power.plugin.tests.Planning;

public class PluginDeploymentPlannerTests
{
    [Test]
    public async Task BuildPackageAsync_SolutionComponentTypeMissing_Throws()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        var planner = new PluginDeploymentPlanner(new PluginPlanningRepositories
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
        var package = new LocalPluginPackage(
            new LocalPackage("Contoso.Plugins", "1.0.0", string.Empty, string.Empty),
            []);

        await Assert.That(async () => await planner.BuildPackageAsync(
                package,
                new PluginPushOptions("contoso_solution", DryRun: false, PublisherPrefix: "contoso")))
            .ThrowsExactly<InvalidOperationException>();
    }
}
