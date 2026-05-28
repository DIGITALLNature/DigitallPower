// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Model;

public class FormXmlControlData: IComparable<FormXmlControlData>
{
    public required bool IsSubgrid { get; init; }
    public required bool IsWebResource { get; init; }
    public required bool IsVisible { get; init; }
    public required bool IsDisabled { get; init; }
    public required string ClassId { get; init; }

    public string CustomControlClass { get; init; } = string.Empty;

    public required string ControlId { get; set; }
    public required string DataFieldName { get; init; }
    public required int RepeatedControl { get; set; }

    public int CompareTo(FormXmlControlData? otherForm)
    {
        if (otherForm == null)
        {
            return -1;
        }
        return string.Compare(ControlId, otherForm.ControlId, StringComparison.OrdinalIgnoreCase);
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
        return ControlId.GetHashCode();
    }

    public static bool operator ==(FormXmlControlData? left, FormXmlControlData? right)
    {
        if (left is null)
        {
            return right is null;
        }
        return left.Equals(right);
    }

    public static bool operator !=(FormXmlControlData? left, FormXmlControlData? right)
    {
        return !(left == right);
    }

    public static bool operator <(FormXmlControlData? left, FormXmlControlData? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(FormXmlControlData? left, FormXmlControlData? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(FormXmlControlData? left, FormXmlControlData? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(FormXmlControlData? left, FormXmlControlData? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
