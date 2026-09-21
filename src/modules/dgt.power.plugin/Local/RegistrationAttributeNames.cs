// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

/// <summary>
/// Well-known type and namespace names for the registration attributes (<c>Digitall.Plugins.Registration</c>
/// and its historical aliases) that declare plugin types, steps, Custom APIs, and managed identities.
/// Matched purely by name via reflection metadata in <see cref="AssemblyReflectionReader"/> - this
/// module has no compile-time dependency on the attributes package itself.
/// </summary>
internal static class RegistrationAttributeNames
{
    /// <summary>Namespaces the registration attributes have shipped under across renames.</summary>
    public static readonly string[] KnownNamespaces =
    [
        "D365.Extension.Registration", "DGT.Registrations", "dgt.registration",
        "Digitall.APower.Registration", "Digitall.Plugins.Registration"
    ];

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
