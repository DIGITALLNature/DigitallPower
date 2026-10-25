// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.process.Configuration;
using dgt.power.common.Ownership;
using Microsoft.Xrm.Sdk;

namespace dgt.power.process.Logic;

public sealed record ResolvedProcessPlan(
    ProcessIntent Intent,
    Entity Current,
    bool IsActive,
    EntityReference CurrentOwner,
    ResolvedOwner? DesiredOwner,
    Guid? CallerId,
    bool NeedsDeactivation,
    bool NeedsAssignment,
    bool NeedsActivation);
