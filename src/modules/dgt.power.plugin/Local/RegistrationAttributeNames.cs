// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

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
    public const string ManagedIdentityRegistration = "ManagedIdentityRegistrationAttribute";

    /// <summary>Attribute names that mark a type as a Power Plugin type.</summary>
    public static readonly string[] KnownPluginAttributes =
    [
        PluginRegistration, CustomApiRegistration, CustomDataProviderRegistration
    ];
}
