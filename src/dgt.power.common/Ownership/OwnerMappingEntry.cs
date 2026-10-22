// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.common.Ownership;

public sealed class OwnerMappingEntry
{
    public OwnerKind Kind { get; init; }

    public Guid? EntraObjectId { get; init; }

    public string? DomainName { get; init; }

    public Guid? ApplicationId { get; init; }

    public string? NamePattern { get; init; }

    public string? BusinessUnitName { get; init; }

    public OwnerMembershipType? MembershipType { get; init; }
}
