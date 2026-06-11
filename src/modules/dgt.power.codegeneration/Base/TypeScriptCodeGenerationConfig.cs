// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Base;

/// <summary>
///     Configuration for TypeScript code generation (Light mode only).
///     Produces TypeScript entity definitions, form typings, OptionSet constants,
///     SDK message names, and Custom API wrappers.
///     The legacy Full generator mode is only available via V1 configs.
/// </summary>
public class TypeScriptCodeGenerationConfig : CodeGenerationConfigBase
{
    /// <summary>
    ///     Generate XrmMock form test helpers.
    /// </summary>
    public bool XrmMockFormHelpers { get; init; }

    /// <summary>
    ///     Only generate forms that belong to the configured solutions.
    /// </summary>
    public bool OnlyFormsFromSolutions { get; init; }

    /// <summary>
    ///     Controls which optional sections are included in generated TypeScript code.
    /// </summary>
    public TypeScriptInclude Include { get; init; } = new();

    /// <summary>
    ///     Form names to include (format: "entity.formname.form").
    ///     When empty, all forms are generated.
    /// </summary>
    public IReadOnlyCollection<string> Forms { get; set; } = new HashSet<string>();
}
