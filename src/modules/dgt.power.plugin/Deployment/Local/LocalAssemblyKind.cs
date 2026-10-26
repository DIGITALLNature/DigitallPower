// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Local;

/// <summary>
/// Classifies the kind of content found in a local plugin assembly. A single assembly can contain
/// any combination of these (hence <see cref="FlagsAttribute"/>).
/// </summary>
[Flags]
public enum LocalAssemblyKind
{
    None = 0,
    Plugin = 1,

    /// <summary>At least one plugin type carries a registration attribute (see <see cref="LocalPluginType.HasRegistrationAttribute"/>).</summary>
    DeclarativePlugin = 2
}
