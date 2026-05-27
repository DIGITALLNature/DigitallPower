// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace dgt.power.codegeneration.Base.Config;


/// <summary>
/// TypeScript only (shrink imports, e.g. odata query or form open)
/// </summary>
public class EntityFormFilter
{
    private readonly HashSet<string> _attributes = new();
    private readonly HashSet<string> _optionsets = new();
    private readonly HashSet<string> _tabs = new();
    private readonly HashSet<string> _sections = new();
    private readonly HashSet<string> _grids = new();

    public required string EntityForm { get; init; }

    public string[] Attributes
    {
        get => _attributes.ToArray();
        init => _attributes = new HashSet<string>(value);
    }

    public string[] Optionsets
    {
        get => _optionsets.ToArray();
        init => _optionsets = new HashSet<string>(value);
    }

    public string[] Tabs
    {
        get => _tabs.ToArray();
        init => _tabs = new HashSet<string>(value);
    }

    public string[] Sections
    {
        get => _sections.ToArray();
        init => _sections = new HashSet<string>(value);
    }

    public string[] Grids
    {
        get => _grids.ToArray();
        init => _grids = new HashSet<string>(value);
    }
}
