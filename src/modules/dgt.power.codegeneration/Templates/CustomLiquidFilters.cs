// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.codegeneration.Extensions;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using Fluid;
using Fluid.Values;
using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Templates;

public static class CustomLiquidFilters
{
    private static readonly Dictionary<int, Dictionary<string, List<string>>> s_usedTokens = new();

    public static ValueTask<FluidValue> CamelCase(FluidValue input, FilterArguments arguments, TemplateContext context)
    {
        return new StringValue(Formatter.CamelCase(input.ToStringValue()));
    }

    public static ValueTask<FluidValue> Sanitize(FluidValue input, FilterArguments arguments, TemplateContext context)
    {
        var allowWhiteSpace = arguments.HasNamed("allowWhiteSpace") && arguments["allowWhiteSpace"].ToBooleanValue();
        var allowSafeStringChars = arguments.HasNamed("allowSafeStringChars") &&
                                   arguments["allowSafeStringChars"].ToBooleanValue();
        var allowFirstNumber = arguments.HasNamed("allowFirstNumber") && arguments["allowFirstNumber"].ToBooleanValue();

        return new StringValue(Formatter.Sanitize(input.ToStringValue(), allowWhiteSpace, allowSafeStringChars,
            allowFirstNumber));
    }

    public static ValueTask<FluidValue> Unique(FluidValue input, FilterArguments arguments, TemplateContext context)
    {
        var contextId = context.GetHashCode();
        var scope = arguments.At(0).ToStringValue();
        var value = input.ToStringValue();

        if (!s_usedTokens.ContainsKey(contextId)) s_usedTokens.Add(contextId, new Dictionary<string, List<string>>());
        if (!s_usedTokens[contextId].ContainsKey(scope)) s_usedTokens[contextId].Add(scope, new List<string>());


        if (s_usedTokens[contextId][scope].Contains(value))
            return Unique(new StringValue(value + "_"), arguments, context);

        s_usedTokens[contextId][scope].Add(value);
        return new StringValue(value);
    }

    public static ValueTask<FluidValue> Controltype(FluidValue input, FilterArguments arguments,
        TemplateContext context)
    {
        return new StringValue(GetAttributeByLogicalName(input, arguments, context).DefinitelyTypedControlType);
    }

    public static ValueTask<FluidValue> Attributetype(FluidValue input, FilterArguments arguments,
       TemplateContext context)
    {
        return new StringValue(GetAttributeByLogicalName(input, arguments, context).DefinitelyTypedAttributeType);
    }

    public static ValueTask<FluidValue> Localize(FluidValue input, FilterArguments arguments, TemplateContext context)
    {
        var label = (Label)input.ToObjectValue();
        var languageCode = arguments.At(0).ToObjectValue();

        return languageCode == null
            ? new StringValue(label.GetLocalizedLabel())
            : new StringValue(label.GetLocalizedLabel(Convert.ToInt32(languageCode, CultureInfo.InvariantCulture)));
    }

    private static AttributeMetadataViewModel GetAttributeByLogicalName(FluidValue input, FilterArguments arguments,
       TemplateContext context)
    {
        var contextId = context.GetHashCode();
        var scope = arguments.At(0).ToObjectValue();
        var value = input.ToStringValue();

        var attr = new List<AttributeMetadataViewModel>(((object[])scope).Cast<AttributeMetadataViewModel>());
        return attr.Single(s => s.LogicalName == value);
    }
}
