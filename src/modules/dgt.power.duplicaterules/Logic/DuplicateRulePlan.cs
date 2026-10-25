// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Ownership;
using dgt.power.duplicaterules.Configuration;
using Microsoft.Xrm.Sdk;

namespace dgt.power.duplicaterules.Logic;

public sealed record DuplicateRulePlan(
    DuplicateRuleIntent Intent,
    string Name,
    string BaseTable,
    DuplicateRulePublicationState CurrentState,
    EntityReference CurrentOwner,
    ResolvedOwner? DesiredOwner,
    bool NeedsUnpublish,
    bool NeedsAssignment,
    bool NeedsPublish)
{
    public bool HasChanges => NeedsUnpublish || NeedsAssignment || NeedsPublish;

    public string Label => $"{Intent.RuleId:D} ('{Name}', {BaseTable})";
}
