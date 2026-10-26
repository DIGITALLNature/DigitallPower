// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Local;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>A successfully read local artifact: exactly one of <see cref="Assembly"/> or <see cref="Package"/> is set.</summary>
internal sealed record LocalDeploymentTarget(string FilePath, LocalAssembly? Assembly, LocalPluginPackage? Package)
{
    internal string DisplayName => Path.GetFileName(FilePath);

    internal IReadOnlyList<LocalAssembly> Assemblies => Package?.Assemblies ?? (Assembly is null ? [] : [Assembly]);
}
