// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.duplicaterules.Configuration;

public sealed record DuplicateRuleIntent(Guid RuleId, string? Name, bool? Published, string? Owner);
