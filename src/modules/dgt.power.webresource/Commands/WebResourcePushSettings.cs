// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.webresource.Base;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.webresource.Commands;

public class WebResourcePushSettings : WebResourceSettings
{
    [CommandArgument(0, "<Target>")]
    [Description("Path to a webresource directory or a single webresource file")]
    public required string Target { get; init; }

    [CommandOption("--solution")]
    [Description("Add resources to this solution and scope obsolete-resource deletion")]
    public string? Solution { get; init; }

    [CommandOption("--publisher-prefix")]
    [Description("Publisher customization prefix used for directory resource names")]
    public string? PublisherPrefix { get; init; }

    [CommandOption("--mapping-file")]
    [Description("JSON file containing directory-relative source-to-Dataverse name mappings")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global -- bound by Spectre.Console.Cli via reflection
    public string? MappingFile { get; init; }

    [CommandOption("--name")]
    [Description("Dataverse logical name; required when Target is a single file")]
    public string? Name { get; init; }

    [CommandOption("--delete-obsolete")]
    [Description("Delete unmanaged webresources in the solution that are absent from the target directory")]
    public bool DeleteObsolete { get; init; }

    [CommandOption("--dry-run")]
    [Description("Report planned changes without writing to Dataverse")]
    public bool DryRun { get; init; }

    [CommandOption("--confirm")]
    [Description("Prompt for confirmation after rendering the deployment plan")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global -- bound by Spectre.Console.Cli via reflection
    public bool Confirm { get; init; }

    public override ValidationResult Validate()
    {
        var isFile = File.Exists(Target);
        var isDirectory = Directory.Exists(Target);
        if (!isFile && !isDirectory)
        {
            return ValidationResult.Error($"WebResource target was not found: '{Target}'.");
        }

        if (isFile)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return ValidationResult.Error("A single-file target requires --name.");
            }

            if (MappingFile is not null)
            {
                return ValidationResult.Error("--mapping-file is only valid when Target is a directory.");
            }

            if (PublisherPrefix is not null)
            {
                return ValidationResult.Error("--publisher-prefix is only valid when Target is a directory.");
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(PublisherPrefix))
            {
                return ValidationResult.Error("A directory target requires --publisher-prefix.");
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                return ValidationResult.Error("--name is only valid when Target is a single file.");
            }
        }

        if (DeleteObsolete && (!isDirectory || string.IsNullOrWhiteSpace(Solution)))
        {
            return ValidationResult.Error("--delete-obsolete requires a directory Target and --solution.");
        }

        return ValidationResult.Success();
    }
}
