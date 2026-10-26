// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Local;

/// <summary>
/// Type and namespace names for the supported v3 registration attributes. Matched purely by name
/// via reflection metadata in <see cref="AssemblyReflectionReader"/> - this module has no
/// compile-time dependency on the attributes package itself.
/// </summary>
internal static class RegistrationAttributeNames
{
    public const string Namespace = "Digitall.Plugins.Registration";

    public const string PluginRegistration = "PluginRegistrationAttribute";
    public const string CustomApiRegistration = "CustomApiRegistrationAttribute";
    public const string CustomDataProviderRegistration = "CustomDataProviderRegistrationAttribute";

    /// <summary>Attribute names that mark a type as a Power Plugin type.</summary>
    public static readonly string[] KnownPluginAttributes =
    [
        PluginRegistration, CustomApiRegistration, CustomDataProviderRegistration
    ];
}
