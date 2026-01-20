// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.


namespace dgt.power.codegeneration.Model
{
    public class TabDetail : IComparable<TabDetail>
    {
        public required string TabName { get; init; }

        public List<KeyValuePair<string, SectionDetail>> Sections { get; } = [];

        public int CompareTo(TabDetail? other)
        {
            if (other == null)
            {
                return -1;
            }
            return string.Compare(TabName, other.TabName, StringComparison.Ordinal);
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
            return TabName.GetHashCode();
        }

        public static bool operator ==(TabDetail? left, TabDetail? right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public static bool operator !=(TabDetail? left, TabDetail? right)
        {
            return !(left == right);
        }

        public static bool operator <(TabDetail? left, TabDetail? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        public static bool operator <=(TabDetail? left, TabDetail? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >(TabDetail? left, TabDetail? right)
        {
            return left is not null && left.CompareTo(right) > 0;
        }

        public static bool operator >=(TabDetail? left, TabDetail? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }
}
