// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace dgt.power.codegeneration.Base.Config;


/// <summary>
/// TypeScript only (shrink imports, e.g. odata query or form open)
/// </summary>
public class EntityFormFilter
{
    private readonly HashSet<string> _attributes = [];
    private readonly HashSet<string> _optionsets = [];
    private readonly HashSet<string> _tabs = [];
    private readonly HashSet<string> _sections = [];
    private readonly HashSet<string> _grids = [];

    public required string EntityForm { get; init; }

    public IReadOnlyCollection<string> Attributes
    {
        get => _attributes;
        init => _attributes = [..value];
    }

    public IReadOnlyCollection<string> Optionsets
    {
        get => _optionsets;
        init => _optionsets = [..value];
    }

    public IReadOnlyCollection<string> Tabs
    {
        get => _tabs;
        init => _tabs = [..value];
    }

    public IReadOnlyCollection<string> Sections
    {
        get => _sections;
        init => _sections = [..value];
    }

    public IReadOnlyCollection<string> Grids
    {
        get => _grids;
        init => _grids = [..value];
    }
}
