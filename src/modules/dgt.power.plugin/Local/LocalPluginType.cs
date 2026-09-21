// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

/// <summary>
/// A plugin type (a class implementing <c>IPlugin</c>) discovered in a local assembly, together
/// with the steps declared on it via registration attributes.
/// </summary>
/// <param name="Name">Friendly name; defaults to the plugin's full type name.</param>
/// <param name="TypeName">Full CLR type name.</param>
/// <param name="CustomApi">Custom API message name, when the type is registered as a Custom API/data provider handler; otherwise empty.</param>
/// <param name="HasRegistrationAttribute">True when the type carries one of the known registration attributes (PluginRegistration/CustomApiRegistration/CustomDataProviderRegistration).</param>
/// <param name="Steps">Steps declared for this plugin type.</param>
public sealed record LocalPluginType(
    string Name,
    string TypeName,
    string CustomApi,
    bool HasRegistrationAttribute,
    IReadOnlyList<LocalPluginStep> Steps);
