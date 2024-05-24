// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

#pragma warning disable CA1716
namespace dgt.power.codegeneration.Templates
{
    public struct Option
    {
        public int? Value { get; }
        public string Label { get; }

        public Option(int? value, string label)
        {
            Value = value;
            Label = label;
        }
    }
}
