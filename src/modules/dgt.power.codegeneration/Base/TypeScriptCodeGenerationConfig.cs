// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base.Config;

namespace dgt.power.codegeneration.Base;

/// <summary>
///     Configuration for TypeScript code generation. Produces TypeScript entity definitions,
///     form typings, OptionSet constants, SDK message names, and Custom API wrappers.
/// </summary>
public class TypeScriptCodeGenerationConfig : CodeGenerationConfigBase
{
    /// <summary>
    ///     TypeScript generator version (Light or Full).
    /// </summary>
    public TypescriptGeneratorVersion GeneratorVersion { get; init; } = TypescriptGeneratorVersion.Light;

    /// <summary>
    ///     Path to Xrm TypeScript typings.
    /// </summary>
    public string TypingPath { get; init; } = "../../Typings/Xrm/index.d.ts";

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
    ///     Form names to include (format: "entity|formtype|formname").
    /// </summary>
    public IReadOnlyCollection<string> Forms { get; set; } = new HashSet<string>();

    /// <summary>
    ///     Business process flow unique names to generate.
    /// </summary>
    public IReadOnlyCollection<string> BusinessProcessFlows { get; init; } = new HashSet<string>();

    /// <summary>
    ///     Entity-level attribute filters for TypeScript generation.
    /// </summary>
    public HashSet<EntityFilter> EntityFilters { get; init; } = [];

    /// <summary>
    ///     Entity reference attribute filters for TypeScript generation.
    /// </summary>
    public HashSet<EntityRefFilter> EntityRefFilters { get; init; } = [];

    /// <summary>
    ///     Entity form filters for TypeScript generation.
    /// </summary>
    public HashSet<EntityFormFilter> EntityFormFilters { get; init; } = [];
}

