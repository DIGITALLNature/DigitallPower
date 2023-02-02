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
