// Copyright (c) DIGITALL Nature. All rights reserved

using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Ownership;

public sealed record ResolvedOwner(
    string LogicalKey,
    EntityReference Principal,
    OwnerKind Kind,
    bool? IsDisabled = null,
    OwnerTeamType? TeamType = null,
    OwnerMembershipType? MembershipType = null);
