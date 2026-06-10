// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Fluid;

namespace dgt.power.codegeneration.Templates;

internal static class TslRenderDiagnostics
{
    private const string NotAvailable = "<n/a>";

    public static string Render(
        IFluidTemplate template,
        TemplateContext context,
        string templateName,
        string? entityKey = null,
        string? formKey = null,
        string? artifact = null)
    {
        ArgumentNullException.ThrowIfNull(template);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(templateName);

        try
        {
            return template.Render(context);
        }
        catch (Exception ex)
        {
            var filterPath = CustomLiquidFilters.TryGetCurrentFilter(context, out var currentFilter)
                ? currentFilter
                : NotAvailable;

#pragma warning disable S2302
            var templateKey = templateName;
#pragma warning restore S2302

            throw new InvalidOperationException(
                $"TSL template render failed. template='{templateKey}', " +
                $"entity='{entityKey ?? NotAvailable}', " +
                $"form='{formKey ?? NotAvailable}', " +
                $"artifact='{artifact ?? NotAvailable}', " +
                $"filter='{filterPath}'.",
                ex);
        }
    }
}
