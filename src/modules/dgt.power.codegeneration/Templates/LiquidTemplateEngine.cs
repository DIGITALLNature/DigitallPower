// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Collections.Concurrent;
using Fluid;

namespace dgt.power.codegeneration.Templates;

/// <summary>
/// Shared Liquid template engine providing parsing, caching, and core filter registration.
/// Domain-specific renderers delegate to this engine for template loading and rendering.
/// </summary>
public static class LiquidTemplateEngine
{
    private static readonly FluidParser s_parser = new(new FluidParserOptions { AllowParentheses = true });

    private static readonly ConcurrentDictionary<string, IFluidTemplate> s_templateCache = new();

    /// <summary>
    /// Registers the core filters (camelcase, sanitize, unique) that are shared across all renderers.
    /// </summary>
    public static void RegisterCoreFilters(TemplateOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        options.Filters.AddFilter("camelcase", CustomLiquidFilters.CamelCase);
        options.Filters.AddFilter("sanitize", CustomLiquidFilters.Sanitize);
        options.Filters.AddFilter("unique", CustomLiquidFilters.Unique);
    }

    /// <summary>
    /// Loads and caches a parsed Liquid template from an embedded resource.
    /// </summary>
    /// <param name="resourcePrefix">The embedded resource namespace prefix (e.g. "dgt.power.codegeneration.Templates.dotnet")</param>
    /// <param name="templateName">The template file name (e.g. "Entity.dotnet.liquid")</param>
    public static IFluidTemplate LoadTemplate(string resourcePrefix, string templateName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resourcePrefix);
        ArgumentException.ThrowIfNullOrWhiteSpace(templateName);

        var cacheKey = $"{resourcePrefix}.{templateName}";
        return s_templateCache.GetOrAdd(cacheKey, _ => ParseEmbeddedTemplate(resourcePrefix, templateName));
    }

    /// <summary>
    /// Renders a cached template with the given context.
    /// </summary>
    public static string Render(string resourcePrefix, string templateName, TemplateContext context)
    {
        var template = LoadTemplate(resourcePrefix, templateName);
        return template.Render(context);
    }

    /// <summary>
    /// Creates a new TemplateContext with the given options.
    /// </summary>
    public static TemplateContext CreateContext(TemplateOptions options)
    {
        return new TemplateContext(options);
    }

    /// <summary>
    /// Creates a new TemplateContext with a model as root object.
    /// </summary>
    public static TemplateContext CreateContext(object model, TemplateOptions options)
    {
        return new TemplateContext(model, options);
    }

    private static IFluidTemplate ParseEmbeddedTemplate(string resourcePrefix, string templateName)
    {
        var resourceName = $"{resourcePrefix}.{templateName}";
        using var stream = typeof(LiquidTemplateEngine).Assembly.GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            throw new InvalidOperationException($"Liquid template resource '{resourceName}' was not found.");
        }

        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd();

        if (!s_parser.TryParse(content, out var template, out var error))
        {
            throw new InvalidOperationException($"Failed to parse liquid template '{resourceName}': {error}");
        }

        return template ?? throw new InvalidOperationException($"Template parser returned no template for '{resourceName}'.");
    }
}
