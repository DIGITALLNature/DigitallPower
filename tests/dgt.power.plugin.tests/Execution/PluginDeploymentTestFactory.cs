// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Execution;
using dgt.power.plugin.Planning;
using dgt.power.plugin.Repositories;
using dgt.power.plugin.tests.Planning;
using Digitall.Dataverse.Testing;

namespace dgt.power.plugin.tests.Execution;

internal sealed class PluginDeploymentTestFactory
{
    internal PluginDeploymentTestFactory(FakeOrganizationServiceAsync service, IEntityDataProviderRepository? providers = null)
    {
        var assemblies = new PluginAssemblyRepository(service);
        var packages = new PluginPackageRepository(service);
        var types = new PluginTypeRepository(service);
        var steps = new SdkMessageProcessingStepRepository(service);
        var images = new SdkMessageProcessingStepImageRepository(service);
        var customApis = new CustomApiRepository(service);
        var solutions = new SolutionComponentRepository(service);
        var identities = new ManagedIdentityRepository(service);
        providers ??= new ProviderTestRepository();

        Planner = new PluginDeploymentPlanner(new PluginPlanningRepositories
        {
            Assemblies = assemblies, Packages = packages, Types = types, Steps = steps, Images = images,
            Messages = new SdkMessageRepository(service), CustomApis = customApis,
            ManagedIdentities = identities, Solutions = solutions, DataProviders = providers
        });
        TypeExecutor = new PluginTypeDeploymentExecutor(types, steps, images, customApis, solutions, providers);
        Migrator = new OutdatedAssemblyMigrator(assemblies, types, steps, customApis);
        PushExecutor = new PluginPushExecutor(assemblies, packages, solutions, identities, TypeExecutor, Migrator);
    }

    internal PluginDeploymentPlanner Planner { get; }
    internal PluginTypeDeploymentExecutor TypeExecutor { get; }
    internal OutdatedAssemblyMigrator Migrator { get; }
    internal PluginPushExecutor PushExecutor { get; }
}
