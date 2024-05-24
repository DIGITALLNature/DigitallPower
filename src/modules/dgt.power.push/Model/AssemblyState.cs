// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Serialization;

namespace dgt.power.push.Model;

[DataContract]
public enum AssemblyState
{
    [EnumMember] Undefined,
    [EnumMember] Create,
    [EnumMember] Update,
    [EnumMember] Upgrade,
    [EnumMember] Package
}
