using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates
{
    internal class OptionField
    {
        public OptionField(AttributeMetadata attributeMetadata, bool useBaseLanguage, int systemLanguage)
        {
            SchemaName = attributeMetadata.SchemaName;
            LogicalName = attributeMetadata.LogicalName;
            AttributeType = attributeMetadata.AttributeType;
            Options = new List<Option>();

            if (attributeMetadata.AttributeType == AttributeTypeCode.Picklist)
            {
                foreach (var option in ((PicklistAttributeMetadata)attributeMetadata).OptionSet.Options.Where(o =>
                             o.Label.UserLocalizedLabel != null).OrderBy(o => o.Value))
                {
                    Options.Add(new Option(option.Value, Formatter.GetLocalizedLabel(option.Label, useBaseLanguage, systemLanguage)));
                }
            }
            else if (attributeMetadata.AttributeType == AttributeTypeCode.Virtual)
            {
                foreach (var option in ((MultiSelectPicklistAttributeMetadata)attributeMetadata).OptionSet.Options
                         .Where(o => o.Label.UserLocalizedLabel != null).OrderBy(o => o.Value))
                {
                    Options.Add(new Option(option.Value, Formatter.GetLocalizedLabel(option.Label, useBaseLanguage, systemLanguage)));
                }
            }
            else if (attributeMetadata.AttributeType == AttributeTypeCode.Status)
            {
                foreach (var option in ((StatusAttributeMetadata)attributeMetadata).OptionSet.Options.Where(o =>
                             o.Label.UserLocalizedLabel != null).OrderBy(o => o.Value))
                {
                    Options.Add(new Option(option.Value, Formatter.GetLocalizedLabel(option.Label, useBaseLanguage, systemLanguage)));
                }
            }
            else if (attributeMetadata.AttributeType == AttributeTypeCode.State)
            {
                foreach (var option in ((StateAttributeMetadata)attributeMetadata).OptionSet.Options.Where(o =>
                             o.Label.UserLocalizedLabel != null).OrderBy(o => o.Value))
                {
                    Options.Add(new Option(option.Value,
                        Formatter.GetLocalizedLabel(option.Label, useBaseLanguage, systemLanguage)));
                }
            }
            else // boolean
            {
                var falseOptionLabel = ((BooleanAttributeMetadata)attributeMetadata).OptionSet
                                       ?.FalseOption
                                       ?.Label
                                       // default false option if no label available
                                       ?? new Label(Labels.DefaultFalse, systemLanguage);

                var falseLabel = Formatter.GetLocalizedLabel(falseOptionLabel, useBaseLanguage, systemLanguage);
                Options.Add(new Option(0, falseLabel));

                var trueOptionLabel = ((BooleanAttributeMetadata)attributeMetadata).OptionSet
                                      ?.TrueOption
                                      ?.Label
                                      // default true option if no label available
                                      ?? new Label(Labels.DefaultTrue, systemLanguage);

                var trueLabel = Formatter.GetLocalizedLabel(trueOptionLabel, useBaseLanguage, systemLanguage);
                Options.Add(new Option(1, trueLabel));
            }
        }

        public string SchemaName { get; }

        public string LogicalName { get; }

        public AttributeTypeCode? AttributeType { get; }

        public List<Option> Options { get; }
    }
}
