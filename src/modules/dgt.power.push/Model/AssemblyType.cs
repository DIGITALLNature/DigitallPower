using System.Runtime.Serialization;

namespace dgt.power.push.Model;

[DataContract]
[Flags]
public enum AssemblyType
{
    [EnumMember] Undefined = 0,
    [EnumMember] Workflow =1,
    [EnumMember] Plugin = 2,
    [EnumMember] PowerPlugin = 4
}
