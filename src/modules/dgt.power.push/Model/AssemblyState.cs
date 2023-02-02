using System.Runtime.Serialization;

namespace dgt.power.push.Model;

[DataContract]
public enum AssemblyState
{
    [EnumMember]
    Undefined,
    [EnumMember]
    Create,
    [EnumMember]
    Update,
    [EnumMember]
    Upgrade
}
