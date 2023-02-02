// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace dgt.power.codegeneration.Base.Config;


/// <summary>
/// TypeScript only (shrink imports, e.g. odata query or form open)
/// </summary>
public class EntityFilter
{
    private readonly HashSet<string> _attributes;
    private readonly HashSet<string> _optionsets;

    public EntityFilter()
    {
        _attributes = new HashSet<string>();
        _optionsets = new HashSet<string>();
    }

    public required string Entity { get; init; }

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
