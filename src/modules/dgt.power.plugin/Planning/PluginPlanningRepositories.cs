// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Repositories;

namespace dgt.power.plugin.Planning;

public sealed class PluginPlanningRepositories
{
    public required IPluginAssemblyRepository Assemblies { get; init; }

    public required IPluginPackageRepository Packages { get; init; }

    public required IPluginTypeRepository Types { get; init; }

    public required ISdkMessageProcessingStepRepository Steps { get; init; }

    public required ISdkMessageProcessingStepImageRepository Images { get; init; }

    public required ISdkMessageRepository Messages { get; init; }

    public required ICustomApiRepository CustomApis { get; init; }

    public required ISolutionComponentRepository Solutions { get; init; }
}
