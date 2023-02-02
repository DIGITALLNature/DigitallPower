using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace dgt.power.push.Model;

[DataContract]
public class WorkflowType
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

    [IgnoreDataMember]
    public Guid Id { get; set; }

    [IgnoreDataMember]
    public string TypeCode { get; set; } = dataverse.PluginType.EntityLogicalName;

    [IgnoreDataMember]
    public Guid ParentId { get; set; }

    [IgnoreDataMember]
    public string ParentTypeCode { get; set; } = dataverse.PluginAssembly.EntityLogicalName;

    [IgnoreDataMember]
    public string? ParentName { get; set; }

    [DataMember(Name = "workflow_activity_group_name", IsRequired = true)]
    [Required]
    public string WorkflowActivityGroupName { get; set; } = null!;
}
