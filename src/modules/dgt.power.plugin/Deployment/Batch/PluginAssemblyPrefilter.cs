// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>
/// Cheap metadata-only check that rules out assemblies that cannot be plugins. Plugins implement
/// <c>IPlugin</c> from <c>Microsoft.Xrm.Sdk</c>, so an assembly without that reference (including
/// native and other non-.NET DLLs) can neither declare a plugin nor hide a deployment conflict.
/// No types are loaded and no references are resolved.
/// </summary>
internal static class PluginAssemblyPrefilter
{
    private const string SdkAssemblyName = "Microsoft.Xrm.Sdk";

    internal static bool CanContainPlugins(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        try
        {
            using var stream = File.OpenRead(path);
            using var peReader = new PEReader(stream);
            if (!peReader.HasMetadata)
            {
                return false;
            }

            var reader = peReader.GetMetadataReader();
            return reader.AssemblyReferences
                .Select(handle => reader.GetString(reader.GetAssemblyReference(handle).Name))
                .Any(name => string.Equals(name, SdkAssemblyName, StringComparison.OrdinalIgnoreCase));
        }
        catch (BadImageFormatException)
        {
            return false;
        }
    }
}
