// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.duplicaterules.Logic;

public sealed record DuplicateRuleCapacity(string BaseTable, int Occupied, int Released, int Added)
{
    public int Projected => Occupied - Released + Added;
}
