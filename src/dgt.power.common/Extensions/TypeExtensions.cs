// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
