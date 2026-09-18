// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

/// <summary>
/// Classifies the kind of content found in a local plugin assembly. A single assembly can contain
/// any combination of these (hence <see cref="FlagsAttribute"/>).
/// </summary>
[Flags]
public enum LocalAssemblyKind
{
    Undefined = 0,
    Plugin = 1,
    PowerPlugin = 2
}
