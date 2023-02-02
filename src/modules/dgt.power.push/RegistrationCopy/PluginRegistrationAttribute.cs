// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace D365.Extension.Registration;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class PluginRegistrationAttribute : Attribute
{
    private readonly PluginExecutionMode _mode;
    private readonly PluginExecutionStage _stage;

    public PluginRegistrationAttribute(PluginExecutionMode mode, string messageName, PluginExecutionStage stage)
    {
        _mode = mode;
        MessageName = messageName;
        _stage = stage;
    }

    /// <summary>
    ///  Mode of plugin for this registration
    /// </summary>
    public int Mode => (int)_mode;

    /// <summary>
    /// Message of plugin for this registration
    /// </summary>
    public string MessageName { get; }

    /// <summary>
    /// Stage of plugin for this registration
    /// </summary>
    public int Stage => (int)_stage;

    /// <summary>
    /// Entity which the Plugin expects
    /// </summary>
    public string? PrimaryEntityName { get; set; }

    /// <summary>
    /// Entity which the Plugin expects
    /// </summary>
    public string? SecondaryEntityName { get; set; }

    /// <summary>
    /// Attributes which at least one needs contained in the target
    /// </summary>
    public string[]? FilterAttributes { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int ExecutionOrder { get; set; } = 100;

    /// <summary>
    /// Plugin expects a PreImage
    /// </summary>
    public bool PreEntityImage { get; set; }

    /// <summary>
    /// Attributes for PreEntityImage - leave null for all
    /// </summary>
    public string[]? PreEntityImageAttributes { get; set; }

    /// <summary>
    /// Plugin expects a PostImage
    /// </summary>
    public bool PostEntityImage { get; set; }

    /// <summary>
    /// Attributes for PostEntityImage - leave null for all
    /// </summary>
    public string[]? PostEntityImageAttributes { get; set; }

    /// <summary>
    /// Unsecure configuration of the Plugin Step
    /// </summary>
    public string? Configuration { get; set; }
}
