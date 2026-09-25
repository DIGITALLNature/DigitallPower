// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Reflection;
using System.Runtime.InteropServices;

namespace dgt.power.plugin.Local;

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
