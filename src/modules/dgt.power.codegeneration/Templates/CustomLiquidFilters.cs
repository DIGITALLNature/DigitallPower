// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.codegeneration.Extensions;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using Fluid;
using Fluid.Values;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.codegeneration.Templates;

public static class CustomLiquidFilters
{
    private static readonly Dictionary<int, Dictionary<string, List<string>>> s_usedTokens = new();

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> CamelCase(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        return new StringValue(Formatter.CamelCase(input.ToStringValue()));
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> Sanitize(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        var allowWhiteSpace = arguments.HasNamed("allowWhiteSpace") && arguments["allowWhiteSpace"].ToBooleanValue();
        var allowSafeStringChars = arguments.HasNamed("allowSafeStringChars") &&
                                   arguments["allowSafeStringChars"].ToBooleanValue();
        var allowFirstNumber = arguments.HasNamed("allowFirstNumber") && arguments["allowFirstNumber"].ToBooleanValue();

        return new StringValue(Formatter.Sanitize(input.ToStringValue(), allowWhiteSpace, allowSafeStringChars,
            allowFirstNumber));
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> Unique(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
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

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> Controltype(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        return new StringValue(GetAttributeByLogicalName(input, arguments).DefinitelyTypedControlType);
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> Attributetype(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        return new StringValue(GetAttributeByLogicalName(input, arguments).DefinitelyTypedAttributeType);
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> GetBpfControltype(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        return new StringValue(GetAttributeByLogicalName(input, arguments).DefinitelyTypedControlType);
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> GetBpfAttributetype(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        return new StringValue(GetAttributeByLogicalName(input, arguments).DefinitelyTypedAttributeType);
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> Localize(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        var label = (Label)input.ToObjectValue();
        var languageCode = arguments.At(0).ToObjectValue();

        return languageCode == null
            ? new StringValue(label.GetLocalizedLabel())
            : new StringValue(label.GetLocalizedLabel(Convert.ToInt32(languageCode, CultureInfo.InvariantCulture)));
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> GetControlTypeFromFormControl(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        return new StringValue(GetControlByControlName(input, arguments).DefinitelyTypedControlType);
    }

    private static FormControlViewModel? GetControlByControlName(FluidValue input, FilterArguments arguments)
    {
        var scope = arguments.At(0).ToObjectValue();
        var value = input.ToStringValue();

        var controlList = new List<FormControlViewModel>(((object[])scope).Cast<FormControlViewModel>());
        return controlList.SingleOrDefault(s => s.ControlName == value);
    }

    private static AttributeMetadataViewModel GetAttributeByLogicalName(FluidValue input, FilterArguments arguments)
    {
        var scope = arguments.At(0).ToObjectValue();
        var value = input.ToStringValue();

        var attr = new List<AttributeMetadataViewModel>(((object[])scope).Cast<AttributeMetadataViewModel>());
        var result = attr.SingleOrDefault(s => s.LogicalName == value);
        if (result != null)
        {
            return result;
        }

        AnsiConsole.MarkupLine($"[red]Warning:[/] cant find Attributemetadata for: {value}");
        return new AttributeMetadataViewModel(new AttributeMetadata{LogicalName = value});
    }
}
