// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace dgt.power.push.Model;

[DataContract]
public class Package : IEquatable<Package>
{
    [DataMember(Name = "name", IsRequired = true)]
    [Required]
    public string Name { get; set; } = null!;

    [DataMember(Name = "version", IsRequired = true)]
    [Required]
    public string Version { get; set; } = null!;

    [IgnoreDataMember] public string? Content { get; set; }

    [IgnoreDataMember] public AssemblyState State { get; set; } = AssemblyState.Undefined;

    [IgnoreDataMember] public Guid Id { get; set; }

    public bool Equals(Package? other)
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
