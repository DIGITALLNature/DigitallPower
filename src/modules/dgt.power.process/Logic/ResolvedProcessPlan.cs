// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.process.Configuration;
using dgt.power.common.Ownership;

namespace dgt.power.process.Logic;

public sealed record ResolvedProcessPlan(
    ProcessIntent Intent,
    ResolvedOwner? DesiredOwner,
    Guid? CallerId,
    bool NeedsDeactivation,
    bool NeedsAssignment,
    bool NeedsActivation);
