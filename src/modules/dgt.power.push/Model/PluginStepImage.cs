using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using dgt.power.dataverse;

#pragma warning disable CA1067

namespace dgt.power.push.Model;

[DataContract]
public class PluginStepImage : IEquatable<PluginStepImage>
{
    [DataMember(Name = "image_type", IsRequired = true)]
    [Required]
    public int ImageType { get; set; }

    [DataMember(Name = "name", IsRequired = true)]
    [Required]
    public string Name { get; set; } = null!;

    [DataMember(Name = "entity_alias", IsRequired = true)]
    [Required]
    public string EntityAlias { get; set; } = null!;

    [DataMember(Name = "message_property_name", IsRequired = true)]
    [Required]
    public string MessagePropertyName { get; set; } = null!;

    /// <summary>
    ///     Attributes which at least one needs contained in the image
    /// </summary>
    [DataMember(Name = "attributes", IsRequired = false)]
    public string[]? Attributes { get; set; }

    [IgnoreDataMember] public Guid Id { get; set; }

    [IgnoreDataMember] public string TypeCode { get; set; } = SdkMessageProcessingStepImage.EntityLogicalName;

    [IgnoreDataMember] public Guid ParentId { get; set; }

    [IgnoreDataMember] public string ParentTypeCode { get; set; } = SdkMessageProcessingStep.EntityLogicalName;

    [IgnoreDataMember] public string? ParentName { get; set; }

    public bool Equals(PluginStepImage? other)
    {
        if (other == null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Name == other.Name &&
               ImageType == other.ImageType;
    }
}
