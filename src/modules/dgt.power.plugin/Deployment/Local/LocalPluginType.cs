// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Local;

/// <summary>
/// A plugin type (a class implementing <c>IPlugin</c>) discovered in a local assembly, together
/// with the steps declared on it via registration attributes.
/// </summary>
/// <param name="Name">Friendly name; defaults to the plugin's full type name.</param>
/// <param name="TypeName">Full CLR type name.</param>
/// <param name="CustomApi">Custom API message name; otherwise empty.</param>
/// <param name="HasRegistrationAttribute">True when the type carries one of the known registration attributes (PluginRegistration/CustomApiRegistration/CustomDataProviderRegistration).</param>
/// <param name="Steps">Steps declared for this plugin type.</param>
public sealed record LocalPluginType(
    string Name,
    string TypeName,
    string CustomApi,
    bool HasRegistrationAttribute,
    IReadOnlyList<LocalPluginStep> Steps)
{
    public IReadOnlyList<LocalDataProviderRegistration> DataProviders { get; init; } = [];
}
