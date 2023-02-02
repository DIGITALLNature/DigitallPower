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
