// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Base;

/// <summary>
///     Controls which optional sections are included in generated TypeScript code.
/// </summary>
public class TypeScriptInclude
{
    /// <summary>
    ///     Generate SDK message name constants.
    /// </summary>
    public bool SdkMessages { get; init; } = true;
}

