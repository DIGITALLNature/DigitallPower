using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using dgt.power.dataverse;

// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CA1067

namespace dgt.power.push.Model;

[DataContract]
public class PluginType : IEquatable<PluginType>
{
    [DataMember(Name = "name", IsRequired = true)]
    [Required]
    public string Name { get; set; } = null!;

    [DataMember(Name = "type_name", IsRequired = true)]
    [Required]
    public string TypeName { get; set; } = null!;

    [DataMember(Name = "friendly_name", IsRequired = true)]
    [Required]
    public string FriendlyName { get; set; } = null!;

    [IgnoreDataMember] public Guid Id { get; set; }

    [IgnoreDataMember] public string TypeCode { get; set; } = dataverse.PluginType.EntityLogicalName;

    [IgnoreDataMember] public Guid ParentId { get; set; }

    [IgnoreDataMember] public string ParentTypeCode { get; set; } = PluginAssembly.EntityLogicalName;

    [IgnoreDataMember] public string? ParentName { get; set; }

    [IgnoreDataMember] public string CustomApi { get; set; } = string.Empty;

    [DataMember(Name = "plugin_steps", IsRequired = true)]
    [Required]
    public List<PluginStep> PluginSteps { get; set; } = new();

    public bool Equals(PluginType? other)
    {
        if (null == other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Name == other.Name &&
               TypeName == other.TypeName &&
               ParentTypeCode == other.ParentTypeCode &&
               ParentName == other.ParentName;
    }
}
