// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dto;

namespace dgt.power.import.Base;

internal static class ImportAssignee
{
    public static string GetAssignee(string assignee, Assignee owner)
    {
        ArgumentNullException.ThrowIfNull(owner);
        return !string.IsNullOrWhiteSpace(owner.Owner) ? owner.Owner : assignee;
    }
}
