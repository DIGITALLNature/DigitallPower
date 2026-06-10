// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License

namespace dgt.power.codegeneration.Model;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class FormAttributeData : IComparable<FormAttributeData>
{
    public required string DataFieldName { get; init; }

    public required bool IsOptionalAttribute { get; set; }

    public int CompareTo(FormAttributeData? otherAttribute)
    {
        if (otherAttribute == null)
        {
            return -1;
        }
        return string.Compare(DataFieldName, otherAttribute.DataFieldName, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return DataFieldName.GetHashCode(StringComparison.Ordinal);
    }

    public static bool operator ==(FormAttributeData? left, FormAttributeData? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(FormAttributeData? left, FormAttributeData? right)
    {
        return !(left == right);
    }

    public static bool operator <(FormAttributeData? left, FormAttributeData? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(FormAttributeData? left, FormAttributeData? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(FormAttributeData? left, FormAttributeData? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(FormAttributeData? left, FormAttributeData? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
