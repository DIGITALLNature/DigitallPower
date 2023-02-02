using System.Runtime.Serialization;

namespace dgt.power.push.Model;

[DataContract]
public enum AssemblyType
{
    [EnumMember]
    Undefined,
    [EnumMember]
    Workflow,
    [EnumMember]
    Plugin
}
