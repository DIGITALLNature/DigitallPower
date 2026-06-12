// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace dgt.power.codegeneration.Base.Config;


/// <summary>
/// TypeScript only (shrink imports, e.g., odata query or form open)
/// </summary>
public class EntityFilter
{
    private readonly HashSet<string> _attributes = [];
    private readonly HashSet<string> _optionsets = [];

    public required string Entity { get; init; }

    public IReadOnlyCollection<string> Attributes
    {
        get => _attributes;
        init
        {
            _attributes = [..value];
            _attributes.TrimExcess();
        }
    }

    public IReadOnlyCollection<string> Optionsets
    {
        get => _optionsets;
        init
        {
            _optionsets = [..value];
            _optionsets.TrimExcess();
        }
    }
}
