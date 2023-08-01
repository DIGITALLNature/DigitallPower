// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using dgt.power.dataverse;

#pragma warning disable CA1067

namespace dgt.power.push.Model;

[DataContract]
public class Assembly : IEquatable<Assembly>
{
    [DataMember(Name = "name", IsRequired = true)]
    [Required]
    public string Name { get; set; } = null!;

    [DataMember(Name = "version", IsRequired = true)]
    [Required]
    public string Version { get; set; } = null!;

    [DataMember(Name = "source_type", IsRequired = true)]
    [Required]
    public int SourceType { get; set; } = PluginAssembly.Options.SourceType.Database;

    [DataMember(Name = "isolation_mode", IsRequired = true)]
    [Required]
    public int IsolationMode { get; set; } = PluginAssembly.Options.IsolationMode.Sandbox;

    [IgnoreDataMember] public string? Content { get; set; }

    [DataMember(Name = "plugin_types", IsRequired = false, EmitDefaultValue = false)]
    public List<PluginType> PluginTypes { get; set; } = new();

    [DataMember(Name = "workflow_types", IsRequired = false, EmitDefaultValue = false)]
    public List<WorkflowType> WorkflowTypes { get; set; } = new();

    [DataMember(Name = "solutions", IsRequired = false)]
    public List<string> Solutions { get; set; } = new();

    [IgnoreDataMember] public Guid Id { get; set; }

    [IgnoreDataMember] public string TypeCode { get; set; } = PluginAssembly.EntityLogicalName;

    [IgnoreDataMember] public AssemblyState State { get; set; } = AssemblyState.Undefined;

    [IgnoreDataMember] public AssemblyType Type { get; set; } = AssemblyType.Undefined;

    public bool Equals(Assembly? other)
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
               Version == other.Version &&
               Content == other.Content;
    }
}
