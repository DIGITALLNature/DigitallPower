// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Collections.Concurrent;
using System.Reflection;
using Fluid;

namespace dgt.power.codegeneration.Templates;

/// <summary>
/// Shared Liquid template engine providing parsing, caching, and core filter registration.
/// Domain-specific renderers delegate to this engine for template loading and rendering.
/// </summary>
public static class LiquidTemplateEngine
{
    private static readonly FluidParser Parser = new(new FluidParserOptions { AllowParentheses = true });

    private static readonly ConcurrentDictionary<string, IFluidTemplate> TemplateCache = new();

    /// <summary>
    /// Registers the core filters (camelcase, sanitize, unique) that are shared across all renderers.
    /// </summary>
    public static void RegisterCoreFilters(TemplateOptions options)
    {
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
        var cacheKey = $"{resourcePrefix}.{templateName}";
        return TemplateCache.GetOrAdd(cacheKey, _ =>
        {
            using var reader = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"{resourcePrefix}.{templateName}")!);
            return Parser.Parse(reader.ReadToEnd());
        });
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
}
