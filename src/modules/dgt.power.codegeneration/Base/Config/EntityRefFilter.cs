// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Base.Config;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable CollectionNeverUpdated.Global

/// <summary>
/// TypeScript only (shrink imports, e.g. odata query or form open)
/// </summary>
public class EntityRefFilter
{
    private readonly HashSet<string> _attributes;
    private readonly HashSet<string> _optionsets;

    public EntityRefFilter()
    {
        _attributes = new HashSet<string>();
        _optionsets = new HashSet<string>();
    }

    public required string Entity { get; set; }

    public string[] Attributes
    {
        get => _attributes.ToArray();
        init
        {
            _attributes = new HashSet<string>(value);
            _attributes.TrimExcess();
        }
    }

    public string[] Optionsets
    {
        get => _optionsets.ToArray();
        init
        {
            _optionsets = new HashSet<string>(value);
            _optionsets.TrimExcess();
        }
    }
}
