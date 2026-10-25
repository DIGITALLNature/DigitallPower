// Copyright (c) DIGITALL Nature. All rights reserved

using System.ComponentModel;
using dgt.power.common;
using dgt.power.common.Ownership;
using dgt.power.duplicaterules.Configuration;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.duplicaterules.Commands;

public sealed class DuplicateRuleApplySettings : BaseProgramSettings
{
    public const int DefaultTimeoutSeconds = 300;

    [CommandArgument(0, "<File>")]
    [Description("Duplicate-rule configuration input path.")]
    public string? File { get; init; }

    [CommandOption("--owner-mapping <File>")]
    [Description("Target environment owner mapping.")]
    public string? OwnerMapping { get; init; }

    [CommandOption("--dry-run")]
    [Description("Resolve and report planned changes without writing to Dataverse.")]
    public bool DryRun { get; init; }

    [CommandOption("--no-wait")]
    [Description("Report accepted publish submissions without waiting for publication to complete.")]
    public bool NoWait { get; init; }

    [CommandOption("--timeout <Seconds>")]
    [Description("Shared time budget in seconds for observing submitted publishes.")]
    [DefaultValue(DefaultTimeoutSeconds)]
    public int Timeout { get; init; } = DefaultTimeoutSeconds;

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(File))
        {
            return ValidationResult.Error("A duplicate-rule configuration file path is required.");
        }

        if (Timeout <= 0)
        {
            return ValidationResult.Error("--timeout must be a positive number of seconds.");
        }

        DuplicateRuleConfiguration configuration;
        try
        {
            configuration = DuplicateRuleConfiguration.Load(File);
        }
        catch (Exception exception) when (exception is DuplicateRuleConfigurationException or ArgumentException)
        {
            return ValidationResult.Error(exception.Message);
        }

        if (configuration.Rules.Any(rule => rule.Owner is not null) && string.IsNullOrWhiteSpace(OwnerMapping))
        {
            return ValidationResult.Error("--owner-mapping is required for owner changes.");
        }

        if (!string.IsNullOrWhiteSpace(OwnerMapping))
        {
            try
            {
                OwnerMappingLoader.Load(OwnerMapping);
            }
            catch (Exception exception) when (exception is OwnerResolutionException or ArgumentException)
            {
                return ValidationResult.Error(exception.Message);
            }
        }

        return ValidationResult.Success();
    }
}
