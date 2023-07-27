// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Extensions;

public static class TypeExtensions
{
    public static IEnumerable<Type> GetBaseTypes(this Type type)
    {
        if (type.BaseType == null)
        {
            return Enumerable.Empty<Type>();
        }

        return Enumerable.Repeat(type.BaseType, 1)
            .Concat(type.BaseType.GetBaseTypes());
    }
}
