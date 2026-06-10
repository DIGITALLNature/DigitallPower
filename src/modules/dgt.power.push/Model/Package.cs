// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace dgt.power.push.Model;

[DataContract]
public record Package
{
    [DataMember(Name = "name", IsRequired = true)]
    [Required]
    public string Name { get; init; } = null!;

    [DataMember(Name = "version", IsRequired = true)]
    [Required]
    public string Version { get; init; } = null!;

    [DataMember(Name = "solutions", IsRequired = false)]
    public IReadOnlyList<string> Solutions { get; init; } = [];

    [IgnoreDataMember] public string? Content { get; init; }

    [IgnoreDataMember] public AssemblyState State { get; init; } = AssemblyState.Undefined;

    [IgnoreDataMember] public Guid Id { get; init; }

    // Equality is based on identity (Name + Version) and content — not on runtime state (Id, State, Solutions)
    public virtual bool Equals(Package? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Name == other.Name
               && Version == other.Version
               && Content == other.Content;
    }

    public override int GetHashCode() => HashCode.Combine(Name, Version, Content);
}
