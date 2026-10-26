// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.duplicaterules.Logic;

public sealed record DuplicateRuleApplyOptions(bool DryRun, bool NoWait, TimeSpan Timeout);
