// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.codegeneration.Base;

/// <summary>
///     Target framework for generated .NET code.
/// </summary>
public enum DotNetTarget
{
    /// <summary>
    ///     Modern .NET (net8.0+): implicit usings, nullable reference types.
    /// </summary>
    Modern,

    /// <summary>
    ///     .NET Framework 4.6.2 (Dataverse plugins): explicit usings, no nullable annotations.
    /// </summary>
    Framework
}

