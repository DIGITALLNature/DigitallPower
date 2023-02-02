// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace dgt.power.codegeneration.Base.Config;


/// <summary>
/// TypeScript only (shrink imports, e.g. odata query or form open)
/// </summary>
public class EntityFormFilter
{
    private readonly HashSet<string> _attributes;
    private readonly HashSet<string> _optionsets;
    private readonly HashSet<string> _tabs;
    private readonly HashSet<string> _sections;
    private readonly HashSet<string> _grids;

    public EntityFormFilter()
    {
        _attributes = new HashSet<string>();
        _optionsets = new HashSet<string>();
        _tabs = new HashSet<string>();
        _sections = new HashSet<string>();
        _grids = new HashSet<string>();
    }

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
