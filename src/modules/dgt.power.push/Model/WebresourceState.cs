using System.Runtime.Serialization;

namespace dgt.power.push.Model;

[DataContract]
public enum WebresourceState
{
    [EnumMember] Undefined = 0,
    [EnumMember] Create,
    [EnumMember] Update,
    [EnumMember] Delete,
    [EnumMember] Up2date
}
