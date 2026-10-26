// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Diagnostics;

namespace dgt.power.common.Extensions;

public static class TypeExtensions
{
    public static IEnumerable<Type> GetBaseTypes(this Type type)
    {
        Debug.Assert(type != null, nameof(type) + " != null");
        if (type.BaseType == null)
        {
            return Enumerable.Empty<Type>();
        }

        return Enumerable.Repeat(type.BaseType, 1)
            .Concat(type.BaseType.GetBaseTypes());
    }
}
