// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Base;

/// <summary>Form generation configuration for TypeScript code generation.</summary>
public class TypeScriptFormsOutput
{
    /// <summary>
    ///     Form names to include (format: "entity.formname.form"). Empty = all forms for scoped entities.
    /// </summary>
    public IReadOnlyCollection<string> Filter { get; init; } = new HashSet<string>();

    /// <summary>Only generate forms that belong to the solutions listed in the entity scope.</summary>
    public bool FromSolutions { get; init; }

    /// <summary>
    ///     Generate XrmMock form test helpers (per-form *.form.testhelper.ts plus shared support files).
    ///     Opt-in, defaults to false.
    /// </summary>
    public bool TestHelpers { get; init; }
}
