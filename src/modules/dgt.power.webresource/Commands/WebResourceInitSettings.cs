// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.ComponentModel;
using System.Text.RegularExpressions;
using dgt.power.webresource.Base;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.webresource.Commands;

public sealed partial class WebResourceInitSettings : WebResourceSettings
{
    private static readonly HashSet<string> ReservedJavaScriptWords = new(StringComparer.Ordinal)
    {
        "await", "break", "case", "catch", "class", "const", "continue", "debugger", "default",
        "delete", "do", "else", "enum", "export", "extends", "false", "finally", "for", "function",
        "if", "implements", "import", "in", "instanceof", "interface", "let", "new", "null",
        "package", "private", "protected", "public", "return", "static", "super", "switch", "this",
        "throw", "true", "try", "typeof", "var", "void", "while", "with", "yield", "arguments", "eval"
    };

    [CommandArgument(0, "<Directory>")]
    [Description("Directory in which to create the webresource project")]
    public required string Destination { get; init; }

    [CommandOption("--name <PackageName>", true)]
    [Description("Unscoped lowercase npm package name")]
    public required string Name { get; init; }

    [CommandOption("--global <Identifier>", true)]
    [Description("Single JavaScript identifier exposed by the generated bundles")]
    public required string Global { get; init; }

    [CommandOption("--template-version <TagOrCommit>")]
    [Description("Template tag or full commit SHA (default: current main)")]
    public string? TemplateVersion { get; init; }

    [CommandOption("--dgtp-connection <Name>")]
    [Description("Optional shared dgtp connection name for generated scripts (unlike --connection)")]
    public string? DgtpConnection { get; init; }

    [CommandOption("--solution <UniqueName>")]
    [Description("Optional default Dataverse solution unique name for deployment scripts")]
    public string? Solution { get; init; }

    [CommandOption("--publisher-prefix <Prefix>")]
    [Description("Optional default Dataverse publisher prefix for deployment scripts")]
    public string? PublisherPrefix { get; init; }

    public override ValidationResult Validate()
    {
        var error = GetValidationError();
        return error is null ? ValidationResult.Success() : ValidationResult.Error(error);
    }

    internal string? GetValidationError()
    {
        if (string.IsNullOrWhiteSpace(Destination))
        {
            return "A destination directory is required.";
        }

        if (Name.Length > 214 || !PackageNameRegex().IsMatch(Name))
        {
            return "--name must be an unscoped lowercase npm-compatible package name.";
        }

        if (Name is "." or ".." or "node_modules" or "favicon.ico")
        {
            return "--name must be an unscoped lowercase npm-compatible package name.";
        }

        if (!JavaScriptIdentifierRegex().IsMatch(Global) ||
            ReservedJavaScriptWords.Contains(Global))
        {
            return "--global must be a non-reserved single JavaScript identifier.";
        }

        if (TemplateVersion is not null && string.IsNullOrWhiteSpace(TemplateVersion))
        {
            return "--template-version must be a non-empty tag or full commit SHA.";
        }

        if (DgtpConnection is not null && string.IsNullOrWhiteSpace(DgtpConnection))
        {
            return "--dgtp-connection must be a non-empty connection name.";
        }

        if (Solution is not null && !DeploymentValueRegex().IsMatch(Solution))
        {
            return "--solution must contain only letters, digits, periods, underscores, or hyphens.";
        }

        if (PublisherPrefix is not null && !DeploymentValueRegex().IsMatch(PublisherPrefix))
        {
            return "--publisher-prefix must contain only letters, digits, periods, underscores, or hyphens.";
        }

        return null;
    }

    [GeneratedRegex("^[a-z0-9][a-z0-9._-]{0,213}$", RegexOptions.CultureInvariant)]
    private static partial Regex PackageNameRegex();

    [GeneratedRegex("^[A-Za-z_$][A-Za-z0-9_$]*$", RegexOptions.CultureInvariant)]
    private static partial Regex JavaScriptIdentifierRegex();

    [GeneratedRegex("^[A-Za-z0-9._-]+$", RegexOptions.CultureInvariant)]
    private static partial Regex DeploymentValueRegex();
}
