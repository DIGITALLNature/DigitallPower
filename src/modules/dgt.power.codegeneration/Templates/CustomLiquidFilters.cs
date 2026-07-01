// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using dgt.power.codegeneration.Extensions;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using Fluid;
using Fluid.Values;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.codegeneration.Templates;

// Fluid callback names are part of the template DSL and must stay stable.
[SuppressMessage(
    "Naming",
    "VSTHRD200:Use Async suffix for async methods",
    Justification = "Filter callback names map to Liquid DSL tokens and are intentionally non-Async.")]
public static class CustomLiquidFilters
{
    private const string UniqueTokensStateKey = "dgt.power.codegeneration.uniqueTokens";
    private const string ConsoleStateKey = "dgt.power.codegeneration.console";
    private const string CurrentFilterStateKey = "dgt.power.codegeneration.currentFilter";

    public static void ConfigureTemplateContext(TemplateContext context, IAnsiConsole console)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(console);
        context.AmbientValues[ConsoleStateKey] = console;
    }

    public static bool TryGetCurrentFilter(TemplateContext context, out string? currentFilter)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (context.AmbientValues.TryGetValue(CurrentFilterStateKey, out var filterName) &&
            filterName is string filterAsString &&
            !string.IsNullOrWhiteSpace(filterAsString))
        {
            currentFilter = filterAsString;
            return true;
        }

        currentFilter = null;
        return false;
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> CamelCase(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        SetCurrentFilter(context, "camelcase");
        ArgumentNullException.ThrowIfNull(input);
        return new StringValue(Formatter.CamelCase(input.ToStringValue()));
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> Sanitize(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        SetCurrentFilter(context, "sanitize");
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(arguments);
        var allowWhiteSpace = arguments.HasNamed("allowWhiteSpace") && arguments["allowWhiteSpace"].ToBooleanValue();
        var allowSafeStringChars = arguments.HasNamed("allowSafeStringChars") &&
                                   arguments["allowSafeStringChars"].ToBooleanValue();
        var allowFirstNumber = arguments.HasNamed("allowFirstNumber") && arguments["allowFirstNumber"].ToBooleanValue();

        return new StringValue(Formatter.Sanitize(input.ToStringValue(), allowWhiteSpace, allowSafeStringChars,
            allowFirstNumber));
    }

    public static ValueTask<FluidValue> Unique(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        SetCurrentFilter(context, "unique");
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(arguments);
        ArgumentNullException.ThrowIfNull(context);
        var scope = arguments.At(0).ToStringValue();
        var uniqueValue = input.ToStringValue();
        var usedTokensByScope = GetOrCreateUniqueTokensByScope(context);

        if (!usedTokensByScope.TryGetValue(scope, out var usedTokens))
        {
            usedTokens = new HashSet<string>(StringComparer.Ordinal);
            usedTokensByScope[scope] = usedTokens;
        }

        var sb = new StringBuilder(uniqueValue);
        while (!usedTokens.Add(sb.ToString()))
        {
            sb.Append('_');
        }

        return new StringValue(sb.ToString());
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> Controltype(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        SetCurrentFilter(context, "controltype");
        ArgumentNullException.ThrowIfNull(input);
        return new StringValue(GetAttributeByLogicalName(input, arguments, context).DefinitelyTypedControlType);
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> Attributetype(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        SetCurrentFilter(context, "attributetype");
        ArgumentNullException.ThrowIfNull(input);
        return new StringValue(GetAttributeByLogicalName(input, arguments, context).DefinitelyTypedAttributeType);
    }

    public static ValueTask<FluidValue> GetControlXrmMockFormFromAttribute(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        SetCurrentFilter(context, "xrmMockControlType");
        ArgumentNullException.ThrowIfNull(input);
        return new StringValue(GetAttributeByLogicalName(input, arguments, context).XrmMockControlType);
    }

    public static ValueTask<FluidValue> GetRequiredAttributeLevel(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        SetCurrentFilter(context, "requiredAttributeLevel");
        ArgumentNullException.ThrowIfNull(input);
        return new StringValue(GetAttributeByLogicalName(input, arguments, context).RequiredLevel);
    }
    public static ValueTask<FluidValue> XrmMockAttributetype(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        SetCurrentFilter(context, "xrmMockAttributeType");
        ArgumentNullException.ThrowIfNull(input);
        return new StringValue(GetAttributeByLogicalName(input, arguments, context).XrmMockTypeAttributeType);
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> Localize(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        SetCurrentFilter(context, "localize");
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(arguments);
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
        SetCurrentFilter(context, "formControlType");
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(arguments);
        return new StringValue(GetControlByControlName(input, arguments)?.DefinitelyTypedControlType);
    }

    public static ValueTask<FluidValue> IsAttributeInList(FluidValue input, FilterArguments arguments, TemplateContext context)
    {
        SetCurrentFilter(context, "isAttributeInList");
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(arguments);
        var value = input.ToStringValue();
        var result = QueryAttributeByLogicalName(value, arguments);
        return BooleanValue.Create(result != null);
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public static ValueTask<FluidValue> GetAttributeItem(FluidValue input, FilterArguments arguments, TemplateContext context)
#pragma warning disable IDE0060 // Remove unused parameter
    {
        SetCurrentFilter(context, "getAttributeItem");
        ArgumentNullException.ThrowIfNull(input);
        return new ObjectValue(GetAttributeByLogicalName(input, arguments, context));
    }


    private static Dictionary<string, HashSet<string>> GetOrCreateUniqueTokensByScope(TemplateContext context)
    {
        if (context.AmbientValues.TryGetValue(UniqueTokensStateKey, out var currentTokensByScope) &&
            currentTokensByScope is Dictionary<string, HashSet<string>> usedTokensByScope)
        {
            return usedTokensByScope;
        }

        var uniqueTokensByScope = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);
        context.AmbientValues[UniqueTokensStateKey] = uniqueTokensByScope;
        return uniqueTokensByScope;
    }

    private static void SetCurrentFilter(TemplateContext context, string filterName)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(filterName);
        context.AmbientValues[CurrentFilterStateKey] = filterName;
    }

    private static FormControlViewModel? GetControlByControlName(FluidValue input, FilterArguments arguments)
    {
        var scope = arguments.At(0).ToObjectValue();
        var value = input.ToStringValue();

        var controlList = new List<FormControlViewModel>(((object[])scope).Cast<FormControlViewModel>());
        return controlList.SingleOrDefault(s => s.ControlName == value);
    }

    private static AttributeMetadataViewModel GetAttributeByLogicalName(FluidValue input, FilterArguments arguments, TemplateContext context)
    {
        var value = input.ToStringValue();
        var result = QueryAttributeByLogicalName(value, arguments);
        if (result != null)
        {
            return result;
        }
        GetConsole(context).MarkupLine($"[red]Warning:[/] cant find Attributemetadata for: {value}");
        return new AttributeMetadataViewModel(new AttributeMetadata{LogicalName = value, RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None)});
    }

    private static AttributeMetadataViewModel? QueryAttributeByLogicalName(string? value, FilterArguments arguments)
    {
        var scope = arguments.At(0).ToObjectValue();

        var attr = new List<AttributeMetadataViewModel>(((object[])scope).Cast<AttributeMetadataViewModel>());
        var result = attr.SingleOrDefault(s => s.LogicalName == value);
        if (result != null)
        {
            return result;
        }
        return null;
    }

    private static IAnsiConsole GetConsole(TemplateContext context)
    {
        if (context.AmbientValues.TryGetValue(ConsoleStateKey, out var contextConsole) &&
            contextConsole is IAnsiConsole console)
        {
            return console;
        }

        return AnsiConsole.Console;
    }
}
