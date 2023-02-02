using dgt.power.codegeneration.Base.Config;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable CollectionNeverUpdated.Global


namespace dgt.power.codegeneration.Base;

public class CodeGenerationConfig
{
    private HashSet<string> _actions;
    private HashSet<string> _customApis;

    private HashSet<string> _entities;
    private HashSet<string> _forms;
    private HashSet<string> _sdkMessages;
    private HashSet<string> _solutions;

    public CodeGenerationConfig()
    {
        _entities = new HashSet<string>();
        _actions = new HashSet<string>();
        _customApis = new HashSet<string>();
        _sdkMessages = new HashSet<string>();
        _solutions = new HashSet<string>();
        _forms = new HashSet<string>();
    }

    public string NameSpace { get; init; } = "D365.Extension.Model";

    public ICollection<string> Entities
    {
        get => _entities.ToHashSet();
        set
        {
            _entities = new HashSet<string>(value);
            _entities.TrimExcess();
        }
    }

    public string[] Actions
    {
        get => _actions.ToArray();
        init
        {
            _actions = new HashSet<string>(value);
            _actions.TrimExcess();
        }
    }

    // ReSharper disable once InconsistentNaming
    public string[] CustomAPIs
    {
        get => _customApis.ToArray();
        init
        {
            _customApis = new HashSet<string>(value);
            _customApis.TrimExcess();
        }
    }

    //additional sdk messages
    public string[] AdditionalSdkMessages
    {
        get => _sdkMessages.ToArray();
        init
        {
            _sdkMessages = new HashSet<string>(value);
            _sdkMessages.TrimExcess();
        }
    }

    public string[] Solutions
    {
        get => _solutions.ToArray();
        init
        {
            _solutions = new HashSet<string>(value);
            _solutions.TrimExcess();
        }
    }

    public string[] Forms
    {
        get => _forms.ToArray();
        set
        {
            _forms = new HashSet<string>(value);
            _forms.TrimExcess();
        }
    }

    public string? EntityMask { get; init; }

    public bool SuppressDotNet { get; init; }

    public bool SuppressTypeScript { get; init; }

    public bool SuppressMetaData { get; init; }

    public bool Hints { get; init; } = true;

    /// <summary>
    ///     DotNet & TypeScript
    /// </summary>
    public bool SuppressOptions { get; init; }

    /// <summary>
    ///     DotNet only
    /// </summary>
    public bool SuppressLogicalNames { get; init; }

    /// <summary>
    ///     DotNet only
    /// </summary>
    public bool SuppressContext { get; init; }

    /// <summary>
    ///     DotNet only
    /// </summary>
    public bool SuppressActions { get; init; }

    /// <summary>
    ///     DotNet & TypeScript
    /// </summary>
    public bool SuppressSdkMessages { get; init; }

    /// <summary>
    ///     DotNet only
    /// </summary>
    public bool SuppressRelations { get; init; }

    /// <summary>
    ///     DotNet only
    /// </summary>
    public bool SuppressNavigationProperties { get; init; }

    /// <summary>
    ///     DotNet only
    /// </summary>
    public bool SuppressEntityTypeCode { get; init; }

    /// <summary>
    ///     DotNet only
    /// </summary>
    public bool SuppressAlternateKeys { get; init; }

    /// <summary>
    ///     DotNet only; Make generated properties virtual
    /// </summary>
    public bool Virtual { get; init; }

    /// <summary>
    ///     DotNet only
    /// </summary>
    public bool UseBaseLanguage { get; init; }

    /// <summary>
    ///     DotNet only; Skip annotation of generated properties with 'DebuggerNonUserCode'
    /// </summary>
    public bool NonDebuggerNonUserCode { get; init; }

    /// <summary>
    ///     DotNet only; Make generated readonly properties editable
    /// </summary>
    public bool EditableReadOnlyProperties { get; init; }

    /// <summary>
    ///     TypeScript only
    /// </summary>
    public string TypingPath { get; init; } = "\"\"../../Typings/Xrm/index.d.ts\"\"";

    /// <summary>
    ///     TypeScript only
    /// </summary>
    public bool OnlyFormsFromSolutions { get; init; }

    /// <summary>
    ///     TypeScript only
    /// </summary>
    public HashSet<EntityFilter> EntityFilters { get; init; } = new ();

    /// <summary>
    ///     TypeScript only
    /// </summary>
    public HashSet<EntityRefFilter> EntityRefFilters { get; init; } = new();

    /// <summary>
    ///     TypeScript only
    /// </summary>
    public HashSet<EntityFormFilter> EntityFormFilters { get; init; } = new();

    /// <summary>
    ///     DotNet & TypeScript
    /// </summary>
    public HashSet<string> SdkMessageFilters { get; init; } = new();

    /// <summary>
    ///     DotNet & TypeScript
    /// </summary>
    public HashSet<string> GlobalOptionSets { get; init; } = new();

    /// <summary>
    ///     TypeScript only
    /// </summary>
    public HashSet<string> BusinessProcessFlows { get; init; } = new();


    /// <summary>
    ///     DotNet only; Make generated properties virtual
    /// </summary>
    public bool SuppressNullableSupport { get; init; }
}
