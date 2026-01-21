// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Model
{
    public class BpfControlDetail: IComparable<BpfControlDetail>
    {
        public required string EntityName { get; set; }
        public required string WorkflowName { get; set; }
        public required string ClassId { get; set; }

        public required string DataFieldName { get; set; }

        public string? DefinitelyTypedControlType { get; set; }
        public string? DefinitelyTypedAttributeType { get; set; }

        public int CompareTo(BpfControlDetail? other)
        {
            if(other == null)
            {
                return -1;
            }
            return string.Compare(DataFieldName, other.DataFieldName, StringComparison.OrdinalIgnoreCase);
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
            return DataFieldName.GetHashCode();
        }

        public static bool operator ==(BpfControlDetail? left, BpfControlDetail? right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public static bool operator !=(BpfControlDetail? left, BpfControlDetail? right)
        {
            return !(left == right);
        }

        public static bool operator <(BpfControlDetail? left, BpfControlDetail? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        public static bool operator <=(BpfControlDetail? left, BpfControlDetail? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >(BpfControlDetail? left, BpfControlDetail? right)
        {
            return left is not null && left.CompareTo(right) > 0;
        }

        public static bool operator >=(BpfControlDetail? left, BpfControlDetail? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }
}
