// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.ts;

internal class OptionField
{
    public OptionField(AttributeMetadata attributeMetadata)
    {
        SchemaName = attributeMetadata.SchemaName;
        LogicalName = attributeMetadata.LogicalName;
        Options = new List<Option>();

        if (attributeMetadata.AttributeType == AttributeTypeCode.Picklist)
        {
            foreach (var option in ((PicklistAttributeMetadata) attributeMetadata).OptionSet.Options.Where(o =>
                         o.Label.UserLocalizedLabel != null))
                Options.Add(new Option(option.Value, option.Label.UserLocalizedLabel.Label));
        }
        else if (attributeMetadata.AttributeType == AttributeTypeCode.Virtual)
        {
            foreach (var option in ((MultiSelectPicklistAttributeMetadata) attributeMetadata).OptionSet.Options
                     .Where(o => o.Label.UserLocalizedLabel != null))
                Options.Add(new Option(option.Value, option.Label.UserLocalizedLabel.Label));
        }
        else if (attributeMetadata.AttributeType == AttributeTypeCode.Status)
        {
            foreach (var option in ((StatusAttributeMetadata) attributeMetadata).OptionSet.Options.Where(o =>
                         o.Label.UserLocalizedLabel != null))
                Options.Add(new Option(option.Value, option.Label.UserLocalizedLabel.Label));
        }
        else if (attributeMetadata.AttributeType == AttributeTypeCode.State)
        {
            foreach (var option in ((StateAttributeMetadata) attributeMetadata).OptionSet.Options.Where(o =>
                         o.Label.UserLocalizedLabel != null))
                Options.Add(new Option(option.Value, option.Label.UserLocalizedLabel.Label));
        }
        else // boolean
        {
            Options.Add(new Option(1, "True"));
            Options.Add(new Option(0, "False"));
        }
    }

    public string SchemaName { get;  }
       
    public string LogicalName { get;  }

    public List<Option> Options { get; }
}