// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Reflection;
using System.Runtime.InteropServices;

namespace dgt.power.plugin.Deployment.Local;

internal static class MetadataLoadContextFactory
{
    public static MetadataLoadContext Create(string targetDirectory)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(targetDirectory);

        var resolverPaths = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll")
            .Concat(Directory.GetFiles(targetDirectory, "*.dll"))
            .Concat(Directory.GetFiles(Path.GetDirectoryName(typeof(MetadataLoadContextFactory).Assembly.Location)!, "*.dll"))
            .GroupBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase)
            .Select(group => group.First())
            .ToList();
        return new MetadataLoadContext(new PathAssemblyResolver(resolverPaths));
    }
}
