// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Model;

public class QuickViewFormControl : IComparable<QuickViewFormControl>
{
    public required string ControlName { get; init; }
    public required string QuickViewFormId { get; init; }

    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public required string QuickViewFormClass { get; set; }
    // ReSharper restore UnusedAutoPropertyAccessor.Global

    public int CompareTo(QuickViewFormControl? other)
    {
        if (other == null)
        {
            return -1;
        }
        return string.Compare(ControlName, other.ControlName, StringComparison.OrdinalIgnoreCase);
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
        return ControlName.GetHashCode(StringComparison.OrdinalIgnoreCase);
    }

    public static bool operator ==(QuickViewFormControl? left, QuickViewFormControl? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(QuickViewFormControl? left, QuickViewFormControl? right)
    {
        return !(left == right);
    }

    public static bool operator <(QuickViewFormControl? left, QuickViewFormControl? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(QuickViewFormControl? left, QuickViewFormControl? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(QuickViewFormControl? left, QuickViewFormControl? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(QuickViewFormControl? left, QuickViewFormControl? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
