using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using dgt.power.dataverse;

#pragma warning disable CA1067

namespace dgt.power.push.Model;

[DataContract]
public class PluginStep : IEquatable<PluginStep>
{
    /// <summary>
    ///     Mode of plugin for this registration
    /// </summary>
    [DataMember(Name = "name", IsRequired = true)]
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Mode of plugin for this registration
    /// </summary>
    [DataMember(Name = "mode", IsRequired = true)]
    [Required]
    public int Mode { get; init; }

    /// <summary>
    ///     Message of plugin for this registration
    /// </summary>
    [DataMember(Name = "message_name", IsRequired = true)]
    [Required]
    public string MessageName { get; init; } = null!;

    /// <summary>
    ///     Stage of plugin for this registration
    /// </summary>
    [DataMember(Name = "stage", IsRequired = true)]
    [Required]
    public int Stage { get; init; }

    /// <summary>
    ///     Entity which the Plugin expects
    /// </summary>
    [DataMember(Name = "primary_entity_name", IsRequired = false)]
    public string PrimaryEntityName { get; init; } = "none";

    /// <summary>
    ///     Entity which the Plugin expects
    /// </summary>
    [DataMember(Name = "secondary_entity_name", IsRequired = false)]
    public string SecondaryEntityName { get; init; } = "none";

    /// <summary>
    ///     Attributes which atleast one needs contained in the target
    /// </summary>
    [DataMember(Name = "filter_attributes", IsRequired = false)]
    public string[]? FilterAttributes { get; set; }

    /// <summary>
    /// </summary>
    [DataMember(Name = "execution_order", IsRequired = false)]
    public int ExecutionOrder { get; set; } = 100;

    [DataMember(Name = "plugin_step_imagess", IsRequired = false, EmitDefaultValue = false)]
    public List<PluginStepImage> PluginStepImages { get; init; } = new();

    [DataMember(Name = "configuration", IsRequired = false, EmitDefaultValue = false)]
    public string? Configuration { get; set; }

    public bool Equals(PluginStep? other)
    {
        if (other == null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return ((PrimaryEntityName == "none" && other.PrimaryEntityName == "none") || PrimaryEntityName == other.PrimaryEntityName) &&
               Mode == other.Mode &&
               MessageName == other.MessageName &&
               Stage == other.Stage;
    }

    #region IgnoreDataMember

    [IgnoreDataMember] public Guid MessageId { get; set; }

    [IgnoreDataMember] public string MessageTypeCode { get; init; } = SdkMessage.EntityLogicalName;

    [IgnoreDataMember] public Guid MessageFilterId { get; set; }

    [IgnoreDataMember] public string MessageFilterTypeCode { get; init; } = SdkMessageFilter.EntityLogicalName;

    [IgnoreDataMember] public Guid Id { get; init; }

    [IgnoreDataMember] public string TypeCode { get; init; } = SdkMessageProcessingStep.EntityLogicalName;


    [IgnoreDataMember] public Guid ParentId { get; init; }

    [IgnoreDataMember] public string ParentTypeCode { get; init; } = dataverse.PluginType.EntityLogicalName;

    [IgnoreDataMember] public string? ParentName { get; init; }

    #endregion
}
