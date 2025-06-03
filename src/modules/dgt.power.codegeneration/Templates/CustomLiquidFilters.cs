// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Logic;
using Fluid;
using Fluid.Values;

namespace dgt.power.codegeneration.Templates;

public static class CustomLiquidFilters
{
    private static readonly Dictionary<int,Dictionary<string, List<string>>> s_usedTokens = new();

    public static ValueTask<FluidValue> CamelCase(FluidValue input, FilterArguments arguments, TemplateContext context)
    {
        return new StringValue(Formatter.CamelCase(input.ToStringValue()));
    }

    public static ValueTask<FluidValue> Sanitize(FluidValue input, FilterArguments arguments, TemplateContext context)
    {
        var allowWhiteSpace = arguments.HasNamed("allowWhiteSpace") && arguments["allowWhiteSpace"].ToBooleanValue();
        var allowSafeStringChars = arguments.HasNamed("allowSafeStringChars") && arguments["allowSafeStringChars"].ToBooleanValue();
        var allowFirstNumber = arguments.HasNamed("allowFirstNumber") && arguments["allowFirstNumber"].ToBooleanValue();

        return new StringValue(Formatter.Sanitize(input.ToStringValue(),allowWhiteSpace, allowSafeStringChars, allowFirstNumber));
    }

    public static ValueTask<FluidValue> Unique(FluidValue input, FilterArguments arguments, TemplateContext context)
    {
        var contextId = context.GetHashCode();
        var scope = arguments.At(0).ToStringValue();
        var value = input.ToStringValue();

        if (!s_usedTokens.ContainsKey(contextId)) s_usedTokens.Add(contextId, new Dictionary<string, List<string>>());
        if (!s_usedTokens[contextId].ContainsKey(scope)) s_usedTokens[contextId].Add(scope, new List<string>());


        if (s_usedTokens[contextId][scope].Contains(value))
            return Unique(new StringValue(value+"_"),arguments, context);

        s_usedTokens[contextId][scope].Add(value);
        return new StringValue(value);
    }
}
