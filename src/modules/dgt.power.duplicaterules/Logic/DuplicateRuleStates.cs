// Copyright (c) DIGITALL Nature. All rights reserved

using System.Globalization;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;

namespace dgt.power.duplicaterules.Logic;

public static class DuplicateRuleStates
{
    public static readonly string[] Columns =
        ["duplicateruleid", "name", "baseentityname", "statecode", "statuscode", "ownerid"];

    public static DuplicateRulePublicationState Classify(Entity rule)
    {
        ArgumentNullException.ThrowIfNull(rule);
        var state = rule.GetAttributeValue<OptionSetValue>("statecode")?.Value;
        var status = rule.GetAttributeValue<OptionSetValue>("statuscode")?.Value;
        return (state, status) switch
        {
            (DuplicateRule.Options.StateCode.Inactive, DuplicateRule.Options.StatusCode.Unpublished) => DuplicateRulePublicationState.Unpublished,
            (DuplicateRule.Options.StateCode.Inactive, DuplicateRule.Options.StatusCode.Publishing) => DuplicateRulePublicationState.Publishing,
            (DuplicateRule.Options.StateCode.Active, DuplicateRule.Options.StatusCode.Published) => DuplicateRulePublicationState.Published,
            _ => DuplicateRulePublicationState.Unknown
        };
    }

    public static string Describe(Entity rule)
    {
        ArgumentNullException.ThrowIfNull(rule);
        var state = rule.GetAttributeValue<OptionSetValue>("statecode")?.Value;
        var status = rule.GetAttributeValue<OptionSetValue>("statuscode")?.Value;
        var classified = Classify(rule);
        return classified == DuplicateRulePublicationState.Unknown
            ? $"unknown (statecode={state?.ToString(CultureInfo.InvariantCulture) ?? "null"}, statuscode={status?.ToString(CultureInfo.InvariantCulture) ?? "null"})"
            : classified.ToString();
    }
}
