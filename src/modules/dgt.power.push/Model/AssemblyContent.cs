// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Serialization;

namespace dgt.power.push.Model;

[DataContract]
public class AssemblyContent
{
    [DataMember(Name = "plugin_types", IsRequired = false, EmitDefaultValue = false)]
    public List<PluginType> PluginTypes { get; set; } = new();

    [IgnoreDataMember] public Guid Id { get; set; }
}
